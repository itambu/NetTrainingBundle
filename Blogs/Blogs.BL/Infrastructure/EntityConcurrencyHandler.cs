using Blogs.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.Infrastructure
{
    public class EntityConcurrencyHandler : IDisposable
    {
        private bool isDisposed;

        private ReaderWriterLockSlim userLocker = new ReaderWriterLockSlim();
        private ReaderWriterLockSlim blogLocker = new ReaderWriterLockSlim();
        private ReaderWriterLockSlim commentLocker = new ReaderWriterLockSlim();

        private Dictionary<Type, ReaderWriterLockSlim> internalDictionary;

        public EntityConcurrencyHandler()
        {
            internalDictionary = new Dictionary<Type, ReaderWriterLockSlim>();
            internalDictionary.Add(typeof(User), new ReaderWriterLockSlim());
            internalDictionary.Add(typeof(Blog), new ReaderWriterLockSlim());
            internalDictionary.Add(typeof(Comment), new ReaderWriterLockSlim());
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed) return;
            if (isDisposing)
            {
                if (userLocker != null) userLocker.Dispose();
                if (blogLocker != null) blogLocker.Dispose();
                if (commentLocker != null) commentLocker.Dispose();
                userLocker = null;
                blogLocker = null;
                commentLocker = null;
                internalDictionary = null;
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EntityConcurrencyHandler()
        {
            Dispose(false);
        }

        public ReaderWriterLockSlim GetLocker<Entity>() where Entity: class
        {
            return internalDictionary[typeof(Entity)];
        }
    }
}
