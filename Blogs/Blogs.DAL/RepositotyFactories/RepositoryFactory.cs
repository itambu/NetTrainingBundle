using Blogs.DAL.Abstractions;
using Blogs.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.DAL.RepositotyFactories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IGenericRepository<Entity> CreateInstance<Entity>(DbContext context) where Entity : class
        {
            return new GenericRepository<Entity>(context);
        }
    }
}
