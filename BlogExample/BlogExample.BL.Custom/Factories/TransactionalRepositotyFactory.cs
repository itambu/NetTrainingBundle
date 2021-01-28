﻿using BlogExample.DAL.Contexts;
using BlogExample.DAL.Repositories;
using BlogExample.DAL.Repositories.Factories;
using BlogExample.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.Factories
{
    public class TransactionalRepositotyFactory : IRepositoryFactory
    {
        IDbContextFactory _factory;
        IDictionary<Type, Type> _container;

        public TransactionalRepositotyFactory()
        {
            _container = new Dictionary<Type, Type>();
            Register<User, ConcurentAddGenericRepositoty<User>>();
            Register<Blog, GenericRepository<Blog>>();
        }

        public TransactionalRepositotyFactory(IDbContextFactory factory) : this()
        {
            _factory = factory;
        }

        public IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class
        {
            var instance = Activator.CreateInstance(
                _container[typeof(TEntity)], 
                context ?? _factory.CreateInstance());
            return instance as IGenericRepository<TEntity>;
        }
        public void Register<TEntity, RepositoryType>() 
            where TEntity : class where RepositoryType : IGenericRepository<TEntity>
        {
            _container.Add(typeof(TEntity), typeof(RepositoryType));
        }
    }
}
