using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blogs.BL.Abstractions;
using Blogs.DAL.Abstractions;

namespace Blogs.BL.BusinessLogicUoWs
{
    public class FetchOrInsertUoW<Entity> : BaseUoW<Entity>, IFetchOrInsertUnitOfWork<Entity> where Entity : class
    {

        //public Entity PerformAction(Expression<Func<Entity, bool>> filter, Entity forInsert)
        //{
        //    ReaderWriterLockSlim locker = SyncEntityHandler.GetLocker<Entity>();
        //    Entity item = null;
        //    try
        //    {
        //        locker.EnterUpgradeableReadLock();
        //        item = Repository.First(filter);
        //        if (item == null)
        //        {
        //            try
        //            {
        //                locker.EnterWriteLock();

        //                item = Repository.First(filter);
        //                if (item == null)
        //                {

        //                    Repository.Add(forInsert);
        //                    Repository.Context.SaveChanges();
        //                }
        //            }
        //            finally
        //            {
        //                locker.ExitWriteLock();
        //            }
        //            return forInsert;
        //        }
        //        else
        //            return item;
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        Exception temp = e;
        //        while(temp.InnerException!=null)
        //        {
        //            temp = temp.InnerException;
        //            var c = temp.Data!=null? temp.Data["HelpLink.EvtID"] : null;
        //            if (c!=null &&  c.ToString() == "51001")
        //            {
        //                return Repository.First(filter);
        //            }
        //        }
        //        throw;
        //    }
        //    catch(Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        locker.ExitUpgradeableReadLock();
        //    }

        //}

        public Entity PerformAction(Expression<Func<Entity, bool>> filter, Entity forInsert)
        {
            ReaderWriterLockSlim locker = SyncEntityHandler.GetLocker<Entity>();
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
            catch (DbUpdateException e)
            {
                //Exception temp = e;
                //while (temp.InnerException != null)
                //{
                //    temp = temp.InnerException;
                //    var c = temp.Data != null ? temp.Data["HelpLink.EvtID"] : null;
                //    if (c != null && c.ToString() == "51001")
                //    {
                //        return Repository.First(filter);
                //    }
                //}
                throw;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                locker.ExitUpgradeableReadLock();
            }
        }

        public FetchOrInsertUoW(IGenericRepository<Entity> repo) : base(repo)
        {
        }
    }
}
