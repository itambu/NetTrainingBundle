using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.AsyncAdapters
{
    public class BaseAsyncAdapter<DTOEntity> : IAsyncAdapter<DTOEntity>
    {
        private IDataSourceHandlerAdapter<DTOEntity> _handler;
        private Task _mainProcessTask;
        protected AsyncAdapterOptions _options;
        //        private bool isDisposed = false;
        protected ISyncStartable _syncStartable;

        //public virtual event EventHandler<IDataSource<DTOEntity>> TaskCompleted
        //{
        //    add => _handler.TaskCompleted += value;
        //    remove => _handler.TaskCompleted -= value;
        //}
        //public virtual event EventHandler<IDataSource<DTOEntity>> TaskFailed
        //{
        //    add => _handler.TaskFailed += value;
        //    remove => _handler.TaskFailed -= value;
        //}
        //public virtual event EventHandler<IDataSource<DTOEntity>> TaskInterrupted
        //{
        //    add => _handler.TaskInterrupted += value;
        //    remove => _handler.TaskInterrupted -= value;
        //}

        public BaseAsyncAdapter(
            IDataSourceHandlerAdapter<DTOEntity> handler,
            AsyncAdapterOptions options,
            ISyncStartable syncStartable)
        {
            _handler = handler;
            _options = options;
            _syncStartable = syncStartable;
        }

        public virtual void PendingTask(object sender, IDataSource<DTOEntity> source)
        {
            var temp = Task.Factory.StartNew(
                 () => _handler.PendingTask(sender, source),
                 CancellationToken.None,
                 TaskCreationOptions.None,
                 _options.Scheduler);
            if (temp == null || !_options.TaskCollection.TryAdd(temp))
            {
                throw new InvalidOperationException("cannot pending task");
            }
        }

        public virtual Task Start()
        {
            try
            {
                return Task.Factory.StartNew(
                    () => Interlocked.Exchange(
                        ref _mainProcessTask,
                        Task.Factory.StartNew(() => _syncStartable.Start())))
                    .ContinueWith(t => Interlocked.Exchange(ref _mainProcessTask, null));
            }
            catch (Exception e)
            {
                throw new HandlerException(e);
            }
        }

        public Task WhenAll()
        {
            return Task.WhenAll(_options.TaskCollection);
        }

        public Task WhenMainProcess()
        {
            Task temp = _mainProcessTask;
            Interlocked.Exchange(ref temp, _mainProcessTask);
            return temp ?? Task.CompletedTask;
        }

        //public void Dispose(bool isDisposing)
        //{
        //    if (isDisposed) return;
        //    if (isDisposing)
        //    {
        //        TaskCompleted = null;
        //    }
        //    isDisposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
