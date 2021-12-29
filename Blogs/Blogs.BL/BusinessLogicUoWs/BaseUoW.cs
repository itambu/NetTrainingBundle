using Blogs.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.BusinessLogicUoWs
{
    public class BaseUoW<Entity> : IDisposable where Entity: class
    {
        protected bool isDisposed = false;

        public IGenericRepository<Entity> Repository { get; protected set; }
        public BaseUoW(IGenericRepository<Entity> repository)
        {
            Repository = repository;
        }

        public virtual void Dispose()
        {
            if (isDisposed) return;

            if (Repository != null)
            {
                Repository.Dispose();
                Repository = null;
                isDisposed = true;
            }
            GC.SuppressFinalize(this);
        }

        ~BaseUoW()
        {
            Dispose();
        }
    }
}
