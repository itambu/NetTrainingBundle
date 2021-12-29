using BlogExample.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.DAL.Repositories.Factories
{
    public interface IRepositoryFactory
    {
        IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context = null) where TEntity : class;
    }
}
