using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public DbContext Context { get; private set; }
        internal DbSet<TEntity> EntitySet { get; private set; }

        public GenericRepository(DbContext context)
        {
            this.Context = context;
            this.EntitySet = context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            EntitySet.Add(entity);
        }

        public void Attach(TEntity entity)
        {
            EntitySet.Attach(entity);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate=null)
        {
            return (predicate != null) ? EntitySet.Where(predicate) : EntitySet;
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate=null)
        {
            return EntitySet.Where(predicate).SingleOrDefault();
        }

        public void Remove(TEntity entity)
        {
            EntitySet.Remove(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
