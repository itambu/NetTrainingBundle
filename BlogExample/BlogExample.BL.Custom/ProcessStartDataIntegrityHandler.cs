using BlogExample.DAL.Contexts;
using BlogExample.DAL.Repositories;
using BlogExample.DAL.Repositories.Factories;
using BlogExample.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom
{
    public class ProcessStartDataIntegrityHandler
    {
        readonly IDbContextFactory ContextFactory;
        readonly IRepositoryFactory RepositoryFactory;

        public ProcessStartDataIntegrityHandler(
            IDbContextFactory contextFactory,
            IRepositoryFactory repositoryFactory)
        {
            ContextFactory = contextFactory;
            RepositoryFactory = repositoryFactory;
        }
        public virtual void OnProcessStartRecoveryHandler(object sender, EventArgs args)
        {
            using (var context = ContextFactory.CreateInstance())
            {
                IGenericRepository<Blog> blogRepo = null;
                IEnumerable<Blog> items = null;
                try
                {
                    blogRepo = RepositoryFactory.CreateInstance<Blog>(context);
                    items = blogRepo.Get(x => x.Session != null);
                    foreach (var item in items)
                    {
                        blogRepo.Remove(item);
                    }
                    blogRepo.Save();
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Recovery failed", e);
                }
            }
        }

    }
}
