using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
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

        protected virtual TransactionScope CreateTransaction()
        {
            return new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Start()
        {
            try
            {
                ProceedAllDataItems();
                AfterDataSourceItemsProceeded();
            }
            catch (OperationCanceledException)
            {
                HandleError();
                throw;
            }
            catch (Exception e)
            {
                HandleError();
                throw new HandlerException(e);
            }
        }

        protected virtual void HandleError()
        {
            ConsistancyHandler.Rollback(DataSource.Id);
        }

        protected virtual void AfterDataSourceItemsProceeded()
        {
            using (TransactionScope scope = CreateTransaction())
            {
                ApplyActionOnDataSource();
                scope.Complete();
            }
        }

        protected virtual void ApplyActionOnDataSource()
        {
            ConsistancyHandler.Commit(DataSource.Id);
            DataSource.Close();
        }

        protected virtual void ProceedAllDataItems()
        {
            foreach (var item in DataSource)
            {
                CancelToken.ThrowIfCancellationRequested();
                ItemHandler.SaveItem(item);
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
                }
                ConsistancyHandler = null;
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
