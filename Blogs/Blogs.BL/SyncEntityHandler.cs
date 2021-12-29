using Blogs.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL
{
    public static class SyncEntityHandler
    {
        public readonly static ReaderWriterLockSlim userLocker = new ReaderWriterLockSlim();
        public readonly static ReaderWriterLockSlim blogLocker = new ReaderWriterLockSlim();
        public readonly static ReaderWriterLockSlim commentLocker = new ReaderWriterLockSlim();

        static Dictionary<Type, ReaderWriterLockSlim> internalDictionary;

        static SyncEntityHandler()
        {
            internalDictionary = new Dictionary<Type, ReaderWriterLockSlim>();
            internalDictionary.Add(typeof(User), new ReaderWriterLockSlim());
            internalDictionary.Add(typeof(Blog), new ReaderWriterLockSlim());
            internalDictionary.Add(typeof(Comment), new ReaderWriterLockSlim());
        }

        public static ReaderWriterLockSlim GetLocker<Entity>() where Entity: class
        {
            return internalDictionary[typeof(Entity)];
        }
    }
}
