using Blogs.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected bool isDisposed = false;

        protected DbContext Context;
        public GenericRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        DbContext IGenericRepository<T>.Context => Context;

        public void Add(T item)
        {
            Context.Set<T>().Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public void Attach(T item)
        {
            Context.Set<T>().Attach(item);
        }

        public void Detach(T item)
        {
            Context.Entry(item).State = EntityState.Detached;
        }

        public void Dispose()
        {
            if (isDisposed) return;

            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }

            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        ~GenericRepository()
        {
            Dispose();
        }

        public T First(Expression<Func<T, bool>> filter)
        {
            return filter != null ? Context.Set<T>().FirstOrDefault(filter) : Context.Set<T>().FirstOrDefault();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            return filter!=null? Context.Set<T>().Where(filter) : Context.Set<T>();
        }

        public void Remove(T item)
        {
            Context.Set<T>().Remove(item);
        }

        public void Remove(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            var entry = Context.Entry<T>(item);
            if (entry.State == EntityState.Detached)
            {
                Attach(item);
            }
            entry.State = EntityState.Modified;
        }
    }
}
