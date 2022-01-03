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
        private bool isDisposed = false;

        public IGenericRepository<Entity> Repository { get; protected set; }
        public BaseUoW(IGenericRepository<Entity> repository)
        {
            Repository = repository;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed) return;

            if (isDisposing)
            {
                if (Repository != null)
                {
                    Repository.Dispose();
                    Repository = null;
                }
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseUoW()
        {
            Dispose(false);
        }
    }
}
