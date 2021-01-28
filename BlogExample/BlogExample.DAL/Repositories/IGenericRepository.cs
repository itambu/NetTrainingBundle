﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Attach(TEntity entity);
        void Save();
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
