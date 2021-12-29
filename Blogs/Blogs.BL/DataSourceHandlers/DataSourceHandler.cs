using Blogs.BL.Abstractions;
using Blogs.BL.BaseHandlers;
using Blogs.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Blogs.BL.DataSourceHandlers
{
    public class DataSourceHandler<DTOEntity> : BaseHandler,  IDataSourceHandler
    {
        protected IBlogDataSource<DTOEntity> DataSource { get; private set; }
        protected IDataItemHandler<DTOEntity> ItemHandler { get; private set; }

        protected IConsistencyHandler ConsistancyHandler { get; private set; }

        public DataSourceHandler(
            IBlogDataSource<DTOEntity> dataSource, 
            IDataItemHandler<DTOEntity> itemHandler, 
            CancellationToken cancelToken,
            IConsistencyHandler consistancyHandler
            ) : base(cancelToken)
        {
            DataSource = dataSource;
            ItemHandler = itemHandler;
            ConsistancyHandler = consistancyHandler;
        }

        protected TransactionScope CreateTransaction()
        {
            return new TransactionScope(
                                TransactionScopeOption.RequiresNew,
                                new TransactionOptions()
                                {
                                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                                },
                                TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Run( )
        {
            try
            {
                foreach (var item in DataSource)
                {
                    CancelToken.ThrowIfCancellationRequested();
                    ItemHandler.SaveItem(item);
                }
                using (TransactionScope scope = CreateTransaction())
                {
                    try
                    {
                        ConsistancyHandler.Commit(DataSource.Id);
                        DataSource.Backup();
                        scope.Complete();
                    }
                    catch(Exception e)
                    {
                        ConsistancyHandler.Rollback(DataSource.Id);
                        throw new HandlerException(e);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                ConsistancyHandler.Rollback(DataSource.Id);
                throw;
            }
            catch (Exception e)
            {
                ConsistancyHandler.Rollback(DataSource.Id);
                throw new HandlerException(e);
            }
        }

        public virtual void Dispose()
        {
            if (isDisposed) return;

            if (ItemHandler != null)
            {
                ItemHandler.Dispose();
                ItemHandler = null;
                isDisposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
