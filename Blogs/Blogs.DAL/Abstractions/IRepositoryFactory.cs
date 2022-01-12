using System.Data.Entity;

namespace Blogs.DAL.Abstractions
{
    public interface IRepositoryFactory
    {
        IGenericRepository<Entity> CreateInstance<Entity>(DbContext context) where Entity : class;
    }
}
