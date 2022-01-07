using Blogs.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Infrastructure
{
    public class AsyncControlPanel<DTOEntity> : IDisposable, IAsyncControlPanel<DTOEntity>
    {
        private bool isDisposed = false;
        protected Task _stoppingTask;
        public TokenSourceSet TokenSources { get; private set; }
        public TaskBlocker TaskBlocker { get; private set; }
        protected int _timeout;
        protected ICollection<IAsyncHandler<DTOEntity>> AsyncHandlers { get; set; }

        public AsyncControlPanel(
            ICollection<IAsyncHandler<DTOEntity>> collection,
            TokenSourceSet tokenSourceSet,
            TaskBlocker taskBlocker,
            int timeout)
        {
            TokenSources = tokenSourceSet;
            TaskBlocker = taskBlocker;
            AsyncHandlers = collection;
            _timeout = timeout;
        }

        public IAsyncControlPanel<DTOEntity> Add(IAsyncHandler<DTOEntity> handler)
        {
            AsyncHandlers.Add(handler);
            return this;
        }

        private void ThrowIfDisposed()
        {
            if (isDisposed)
                throw new InvalidOperationException("object was disposed");
        }

        public Task SignalStop()
        {
            ThrowIfDisposed();
            if (!TokenSources.Stop.IsCancellationRequested)
            {
                TaskBlocker.Stop();
                TokenSources.Stop.Cancel();
                Task.WhenAll(AsyncHandlers.Select(x => x.WhenMainProcess())).Wait();
                return _stoppingTask = Task.WhenAll(AsyncHandlers.Select(x => x.WhenAll()));
            }
            else
            {
                return Task.CompletedTask;
            }
        }
        public Task SignalCancel()
        {
            ThrowIfDisposed();
            if (!TokenSources.Cancel.IsCancellationRequested)
            {
                SignalStop();
                if (!_stoppingTask.Wait(_timeout))
                {
                    TokenSources.Cancel.Cancel();
                }
                return _stoppingTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        public Task StartAsync()
        {
            ThrowIfDisposed();
            return Task.WhenAll(AsyncHandlers.Select(x => x.StartMainProcess()));
        }

        public virtual event EventHandler<IDataSource<DTOEntity>> TaskCompleted
        {
            add { foreach (var i in AsyncHandlers) { i.TaskCompleted += value; }; }
            remove { foreach (var i in AsyncHandlers) { i.TaskCompleted -= value; }; }
        }
        public virtual event EventHandler<IDataSource<DTOEntity>> TaskFailed
        {
            add { foreach (var i in AsyncHandlers) { i.TaskFailed += value; }; }
            remove { foreach (var i in AsyncHandlers) { i.TaskFailed -= value; }; }
        }
        public virtual event EventHandler<IDataSource<DTOEntity>> TaskInterrupted
        {
            add { foreach (var i in AsyncHandlers) { i.TaskInterrupted += value; }; }
            remove { foreach (var i in AsyncHandlers) { i.TaskInterrupted -= value; }; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AsyncControlPanel()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed) return;
            if (isDisposing)
            {
                if (TokenSources != null)
                {
                    TokenSources.Dispose();
                    TokenSources = null;
                }

                if (TaskBlocker != null)
                {
                    TaskBlocker.Dispose();
                    TaskBlocker = null;
                }

                foreach (var i in AsyncHandlers)
                {
                    IDisposable obj = i as IDisposable;
                    obj?.Dispose();
                }

                AsyncHandlers.Clear();
                AsyncHandlers = null;
            }
            isDisposed = true;
        }
    }
}
