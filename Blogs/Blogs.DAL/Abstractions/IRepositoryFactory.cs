using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.DAL.Abstractions
{
    public interface IRepositoryFactory 
    {
        IGenericRepository<Entity> CreateInstance<Entity>(DbContext context) where Entity:class;
    }
}
