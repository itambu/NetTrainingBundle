using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using Blogs.DAL.Abstractions;
using System;
using System.Threading.Tasks;

namespace Blogs.BL.StartApp
{
    public class BaseAsyncApp<DTOEntity> : IAsyncApp<DTOEntity>
    {
        private bool isDisposed = false;
        protected IAsyncControlPanel<DTOEntity> _controlPanel;
        public event EventHandler Stopped;
        public event EventHandler Cancelled;
        public event EventHandler Started;

        protected AppOptions _appOptions;

        protected IBlogContextFactory _contextFactory;

        public BaseAsyncApp(IAsyncControlPanel<DTOEntity> controlPanel, IBlogContextFactory contextFactory, AppOptions appOptions)
        {
            _appOptions = appOptions;
            _controlPanel = controlPanel;
            _contextFactory = contextFactory;
        }

        protected virtual void EnsureDataBase()
        {
            using (var context = _contextFactory.CreateInstance())
            {
                context.Database.CreateIfNotExists();
            }
        }
        protected void ThrowIfDisposed()
        {
            if (isDisposed)
                throw new InvalidOperationException("object was disposed");
        }

        public virtual void OnStartedEvent(object sender, EventArgs args)
        {
            Started?.Invoke(this, args);
        }

        protected virtual void OnStopEvent(object sender, EventArgs args)
        {
            Stopped?.Invoke(this, args);
        }

        protected virtual void OnCancelEvent(object sender, EventArgs args)
        {
            Cancelled?.Invoke(this, args);
        }

        public Task Cancel()
        {
            ThrowIfDisposed();
            _controlPanel.Cancel().Wait();
            OnCancelEvent(this, null);
            return Task.CompletedTask;
        }

        public Task Start()
        {
            ThrowIfDisposed();
            return _controlPanel.Start()
                .ContinueWith(t =>
                {
                    if (t.Exception == null)
                    {
                        OnStartedEvent(this, null);
                    }
                    else
                        throw t.Exception;
                });
        }

        public Task Stop()
        {
            ThrowIfDisposed();
            _controlPanel.Stop().Wait();
            OnStopEvent(this, null);
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed) return;
            if (isDisposing)
            {
                if (_controlPanel != null)
                {
                    _controlPanel.Dispose();
                    _controlPanel = null;
                }
                _contextFactory = null;
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseAsyncApp()
        {
            Dispose(false);
        }
    }
}
