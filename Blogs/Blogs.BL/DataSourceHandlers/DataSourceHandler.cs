using Blogs.BL.Abstractions;
using Blogs.BL.BaseHandlers;
using System;
using System.Threading;
using System.Transactions;

namespace Blogs.BL.DataSourceHandlers
{
    public class DataSourceHandler<DTOEntity> : IDataSourceHandler
    {
        private bool isDisposed = false;

        protected readonly CancellationToken CancelToken;
        protected IDataSource<DTOEntity> DataSource { get; private set; }
        protected IDataItemHandler<DTOEntity> ItemHandler { get; private set; }

        protected IConsistencyHandler ConsistancyHandler { get; private set; }

        public DataSourceHandler(
            IDataSource<DTOEntity> dataSource, 
            IDataItemHandler<DTOEntity> itemHandler, 
            CancellationToken cancelToken,
            IConsistencyHandler consistancyHandler
            ) 
        {
            DataSource = dataSource;
            ItemHandler = itemHandler;
            ConsistancyHandler = consistancyHandler;
            CancelToken = cancelToken;
        }

        protected TransactionScope CreateTransaction()
        {
            return new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Start( )
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
                    catch (Exception e)
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

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed) return;

            if (isDisposing)
            {
                if (ItemHandler != null)
                {
                    ItemHandler.Dispose();
                    ItemHandler = null;
                    isDisposed = true;
                }
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~DataSourceHandler()
        {
            Dispose(false);
        }
    }
}
