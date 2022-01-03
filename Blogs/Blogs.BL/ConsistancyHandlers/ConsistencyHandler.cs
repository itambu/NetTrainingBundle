using Blogs.BL.Abstractions;
using Blogs.BL.BaseHandlers;
using Blogs.DAL.Abstractions;
using Blogs.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.ConsistancyHandlers
{
    //thread-safe
    public class ConsistencyHandler : IConsistencyHandler
    {
        private IBlogContextFactory ContextFactory { get; set; }
        private IRepositoryFactory RepositoryFactory { get; set; }
        private IConnectionFactory ConnectionFactory { get; set; }
        

        public ConsistencyHandler(
            IConnectionFactory connectionFactory,
            IBlogContextFactory contextFactory,
            IRepositoryFactory repositoryFactory
            )
        {
            ContextFactory = contextFactory;
            RepositoryFactory = repositoryFactory;
            ConnectionFactory = connectionFactory;
        }

        public bool IsConsisted
        {
            get
            {
                return UseClosure((c, r) => r.Get(x => x.Session != null).Count() > 0);
            }
        }

        public void Commit(Guid session)
        {
            UseClosure((c, r) =>
            {
                var results = r.Get(x => x.Session == session);
                foreach (var e in results)
                {
                    e.Session = null;
                    r.Update(e);
                }
                c.SaveChanges();
            });
        }

        public void Rollback(Guid session)
        {
            UseClosure((c, r) =>
            {
                var results = r.Get(x => x.Session == session);
                foreach (var e in results)
                {
                    r.Remove(e);
                }
                c.SaveChanges();
            });
        }

        public void RollbackAll()
        {
            UseClosure((c, r) =>
            {
                var results = r.Get(x => x.Session != null );
                foreach (var e in results)
                {
                    r.Remove(e);
                }
                c.SaveChanges();
            });
        }

        protected bool UseClosure( Func<DbContext, IGenericRepository<Comment>, bool> func  )
        {
            DbContext context = null;
            IGenericRepository<Comment> repository= null;
            try
            {
                CreateClosure(out context, out repository);
                return func?.Invoke(context, repository) ?? false;
            }
            catch(Exception e)
            {
                throw new HandlerException(e);
            }
            finally
            {
                if (repository != null) repository.Dispose();
                if (context != null) context.Dispose();
            }
        }

        protected void UseClosure(Action<DbContext, IGenericRepository<Comment>> action)
        {
            DbContext context = null;
            IGenericRepository<Comment> repository = null;
            try
            {
                CreateClosure(out context, out repository);
                action?.Invoke(context, repository);
            }
            catch (Exception e)
            {
                throw new HandlerException(e);
            }
            finally
            {
                if (repository != null) repository.Dispose();
                if (context != null) context.Dispose();
            }
        }

        protected void CreateClosure(out DbContext context, out IGenericRepository<Comment> repository)
        {
            context = ContextFactory.CreateInstance(ConnectionFactory.CreateInstance());
            repository = RepositoryFactory.CreateInstance<Comment>(context);
        }
    }
}
