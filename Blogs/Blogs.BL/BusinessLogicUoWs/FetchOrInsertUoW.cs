using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using Blogs.DAL.Abstractions;

namespace Blogs.BL.BusinessLogicUoWs
{
    public class FetchOrInsertUoW<Entity> : BaseUoW<Entity>, IFetchOrInsertUnitOfWork<Entity> where Entity : class
    {
        //private bool _isDisposed = false;
        private EntityConcurrencyHandler _lockHandler;
        public Entity PerformAction(Expression<Func<Entity, bool>> filter, Entity forInsert)
        {
            ReaderWriterLockSlim locker = _lockHandler.GetLocker<Entity>();
            Entity item = null;
            try
            {
                locker.EnterUpgradeableReadLock();
                item = Repository.First(filter);
                if (item == null)
                {
                    try
                    {
                        locker.EnterWriteLock();

                        item = Repository.First(filter);
                        if (item == null)
                        {
                            Repository.Add(forInsert);
                            Repository.Context.SaveChanges();
                            return forInsert;
                        }
                        else
                        {
                            return item;
                        }
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
                else
                    return item;
            }
            finally
            {
                locker.ExitUpgradeableReadLock();
            }
        }

        //protected override void Dispose(bool isDisposing)
        //{
        //    if (_isDisposed) return;

        //    if (isDisposing)
        //    {
        //        if (_lockHandler!=null)
        //        {
        //            _lockHandler.Dispose();
        //            _lockHandler = null;
        //        }
        //    }

        //    _isDisposed = true;
        //    base.Dispose(isDisposing);
        //}

        public FetchOrInsertUoW(IGenericRepository<Entity> repo, EntityConcurrencyHandler lockHandler) : base(repo)
        {
            _lockHandler = lockHandler;
        }
    }
}
