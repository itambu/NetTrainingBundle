using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.ControlPanels
{
    public class AsyncControlPanel<DTOEntity> : IAsyncControlPanel<DTOEntity>
    {
        private bool isDisposed = false;
        protected Task _stoppingTask;
        public TokenSourceSet TokenSources { get; private set; }
        protected int _timeout;
        public event EventHandler StopRequested;
        protected ICollection<IAsyncAdapter<DTOEntity>> AsyncHandlers { get; set; }

        public AsyncControlPanel(
            ICollection<IAsyncAdapter<DTOEntity>> collection,
            TokenSourceSet tokenSourceSet,
            int timeout)
        {
            TokenSources = tokenSourceSet;
            AsyncHandlers = collection;
            _timeout = timeout;
        }

        public IAsyncControlPanel<DTOEntity> Add(IAsyncAdapter<DTOEntity> handler)
        {
            AsyncHandlers.Add(handler);
            return this;
        }

        private void ThrowIfDisposed()
        {
            if (isDisposed)
                throw new InvalidOperationException("object was disposed");
        }

        public Task Stop()
        {
            ThrowIfDisposed();
            if (!TokenSources.Stop.IsCancellationRequested)
            {
                TokenSources.Stop.Cancel();
                OnStopRequested(this, null);
                Task.WhenAll(AsyncHandlers.Select(x => x.WhenMainProcess())).Wait();
                return _stoppingTask = Task.WhenAll(AsyncHandlers.Select(x => x.WhenAll()));
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        protected virtual void OnStopRequested(object sender, EventArgs args)
        {
            var temp = StopRequested;
            Interlocked.Exchange(ref temp, StopRequested);
            temp?.Invoke(sender, args);
        }

        public Task Cancel()
        {
            ThrowIfDisposed();
            if (!TokenSources.Cancel.IsCancellationRequested)
            {
                Stop();
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

        public Task Start()
        {
            ThrowIfDisposed();
            return Task.WhenAll(AsyncHandlers.Select(x => x.Start()));
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
