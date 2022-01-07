using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using Blogs.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.StartApp
{
    public class BaseAsyncApp<DTOEntity> : IAsyncApp<DTOEntity>
    {
        private bool isDisposed = false;
        protected IAsyncControlPanel<DTOEntity> _controlPanel;
        public event EventHandler OnStopped;
        public event EventHandler OnCancelled;

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

        protected virtual void OnStopEvent(object sender, EventArgs args)
        {
            OnStopped?.Invoke(this, args);
        }

        protected virtual void OnCancelEvent(object sender, EventArgs args)
        {
            OnCancelled?.Invoke(this, args);
        }

        public Task CancelAsync()
        {
            ThrowIfDisposed();
            _controlPanel.SignalCancel().Wait();
            OnCancelEvent(this, null);
            return Task.CompletedTask;
        }

        public Task StartAsync()
        {
            ThrowIfDisposed();
            return _controlPanel.StartAsync();
        }

        public Task StopAsync()
        {
            ThrowIfDisposed();
            _controlPanel.SignalStop().Wait();
            OnStopEvent(this, null);
            return Task.CompletedTask;
        }

        public virtual event EventHandler<IDataSource<DTOEntity>> TaskCompleted
        {
            add => _controlPanel.TaskCompleted += value;
            remove => _controlPanel.TaskCompleted -= value;
        }
        public virtual event EventHandler<IDataSource<DTOEntity>> TaskFailed
        {
            add => _controlPanel.TaskFailed += value;
            remove => _controlPanel.TaskFailed -= value;
        }
        public virtual event EventHandler<IDataSource<DTOEntity>> TaskInterrupted
        {
            add => _controlPanel.TaskInterrupted += value;
            remove => _controlPanel.TaskInterrupted -= value;
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
