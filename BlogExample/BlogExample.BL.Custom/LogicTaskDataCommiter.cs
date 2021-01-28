using BlogExample.DAL.Contexts;
using BlogExample.DAL.Repositories;
using BlogExample.DAL.Repositories.Factories;
using BlogExample.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom
{
    public class LogicTaskDataCommiter
    {
        readonly IDbContextFactory ContextFactory;
        readonly IRepositoryFactory RepositoryFactory;
        protected Exception PostPhaseException { get; set; } = null;


        public LogicTaskDataCommiter(
            IDbContextFactory contextFactory,
            IRepositoryFactory repositoryFactory)
        {
            ContextFactory = contextFactory;
            RepositoryFactory = repositoryFactory;
        }
        public void TryPostCsvFileData(CustomLogicTaskContext taskContext)
        {
            PostPhaseException = null;
            using (var context = ContextFactory.CreateInstance())
            {
                IGenericRepository<Blog> blogRepo = null;
                IEnumerable<Blog> items = null;
                try
                {
                    blogRepo = RepositoryFactory.CreateInstance<Blog>(context);
                    items = blogRepo.Get(x => x.Session == taskContext.Session);
                    foreach (var item in items)
                    {
                        item.Session = null;
                    }
                    blogRepo.Save();
                }
                catch (Exception postException)
                {
                    PostPhaseException = postException;
                    RollBack(taskContext);
                }
            }
        }

        public void RollBack(CustomLogicTaskContext taskContext)
        {
            using (var context = ContextFactory.CreateInstance())
            {
                IGenericRepository<Blog> blogRepo = null;
                IEnumerable<Blog> items = null;
                try
                {
                    blogRepo = RepositoryFactory.CreateInstance<Blog>(context);
                    items = blogRepo.Get(x => x.Session == taskContext.Session);
                    foreach (var item in items)
                    {
                        blogRepo.Remove(item);
                    }
                    blogRepo.Save();
                    if (PostPhaseException != null)
                    {
                        throw new InvalidOperationException("Commit failed", PostPhaseException);
                    }
                }
                catch (Exception rollbackException)
                {
                    throw new InvalidOperationException("Rollback failed", rollbackException);
                }
            }
        }
    }
}
