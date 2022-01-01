using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.StartApp
{
    public abstract class BaseApp : IAsyncApp
    {
        protected bool isDisposed;
        protected IDictionary<Type, IAsyncHandler<BlogDataSourceDTO>> AsyncHandlers = new Dictionary<Type, IAsyncHandler<BlogDataSourceDTO>>();
        public event EventHandler OnStop;
        public event EventHandler OnCancel;
        protected TokenSourceSet TokenSources = new TokenSourceSet(stop: new CancellationTokenSource(), cancel: new CancellationTokenSource());

        protected virtual void OnStopEvent(object sender, EventArgs args)
        {
            TokenSources.Stop.Cancel();
            OnStop?.Invoke(this, args);
        }

        protected virtual void OnCancelEvent(object sender, EventArgs args)
        {
            OnCancel?.Invoke(this, args);
        }

        public Task CancelAsync()
        {
            TokenSources.Stop.Cancel();
            TokenSources.Cancel.Cancel();
            OnCancelEvent(this, null);
            var temp = AsyncHandlers.Values;
            return Task.WhenAll(temp.Select(x => x.WhenAll()).Concat(temp.Select(x => x.WhenMainProcess())));
        }

        public Task StartAsync()
        {
            return Task.WhenAll(AsyncHandlers.Values.Select(x => x.StartMainProcess()));
        }

        public Task StopAsync()
        {
            OnStopEvent(this, null);
            var temp = AsyncHandlers.Values;
            return Task.WhenAll(temp.Select(x => x.WhenAll()).Concat(temp.Select(x => x.WhenMainProcess())));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
            }
            isDisposed = true;
        }
    }
}
