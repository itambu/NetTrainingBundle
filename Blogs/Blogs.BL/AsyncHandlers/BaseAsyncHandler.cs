using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using Blogs.BL.BaseHandlers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.AsyncHandlers
{
    public class BaseAsyncHandler<DTOEntity> : IAsyncHandler<DTOEntity>
    {
        private IProcessHandler<DTOEntity> _handler;
        private Task _mainProcessTask;
        protected AsyncHandlerOptions _options;
//        private bool isDisposed = false;

        public virtual event EventHandler<IDataSource<DTOEntity>> TaskCompleted
        {
            add => _handler.TaskCompleted += value;
            remove => _handler.TaskCompleted -= value;
        }
        public virtual event EventHandler<IDataSource<DTOEntity>> TaskFailed
        {
            add => _handler.TaskFailed += value;
            remove => _handler.TaskFailed -= value;
        }
        public virtual event EventHandler<IDataSource<DTOEntity>> TaskInterrupted
        {
            add => _handler.TaskInterrupted += value;
            remove => _handler.TaskInterrupted -= value;
        }

        public BaseAsyncHandler(
            IProcessHandler<DTOEntity> handler,
            AsyncHandlerOptions options)
        {
            _handler = handler;
            _options = options;
        }

        public void PendingTask(IDataSource<DTOEntity> source)
        {
            var temp = Task.Factory.StartNew(
                 () => _handler.PendingTask(source),
                 CancellationToken.None,
                 TaskCreationOptions.None,
                 _options.Scheduler);

            if (temp == null || !_options.TaskCollection.TryAdd(temp))
            {
                throw new InvalidOperationException("cannot pending task");
            }
        }

        public Task StartMainProcess()
        {
            try
            {
                return Task.Factory.StartNew(
                    () => Interlocked.Exchange(
                        ref _mainProcessTask,
                        Task.Factory.StartNew(() => _handler.StartProcess(PendingTask))))
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
