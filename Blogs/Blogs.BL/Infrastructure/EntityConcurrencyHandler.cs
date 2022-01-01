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

        private readonly ReaderWriterLockSlim userLocker = new ReaderWriterLockSlim();
        private readonly ReaderWriterLockSlim blogLocker = new ReaderWriterLockSlim();
        private readonly ReaderWriterLockSlim commentLocker = new ReaderWriterLockSlim();

        Dictionary<Type, ReaderWriterLockSlim> internalDictionary;

        public EntityConcurrencyHandler()
        {
            internalDictionary = new Dictionary<Type, ReaderWriterLockSlim>();
            internalDictionary.Add(typeof(User), new ReaderWriterLockSlim());
            internalDictionary.Add(typeof(Blog), new ReaderWriterLockSlim());
            internalDictionary.Add(typeof(Comment), new ReaderWriterLockSlim());
        }

        public void Dispose()
        {
            if (isDisposed) return;

            userLocker.Dispose();
            blogLocker.Dispose();
            commentLocker.Dispose();
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        ~EntityConcurrencyHandler()
        {
            Dispose();
        }

        public ReaderWriterLockSlim GetLocker<Entity>() where Entity: class
        {
            return internalDictionary[typeof(Entity)];
        }
    }
}
