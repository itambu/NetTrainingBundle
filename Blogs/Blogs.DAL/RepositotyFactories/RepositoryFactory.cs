using Blogs.DAL.Abstractions;
using Blogs.DAL.Repositories;
using System.Data.Entity;

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
