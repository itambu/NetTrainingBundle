using System;
using System.Threading;

namespace Blogs.BL.Infrastructure
{
    public class TokenSourceSet : IDisposable
    {
        private bool isDisposed = false;
        public CancellationTokenSource Stop { get; protected set; }
        public CancellationTokenSource Cancel { get; protected set; }

        public TokenSourceSet(CancellationTokenSource stop, CancellationTokenSource cancel)
        {
            Stop = stop;
            Cancel = cancel;
        }

        public ActionTokenSet Tokens { get => new ActionTokenSet() { Stop = Stop.Token, Cancel = Cancel.Token }; }
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed) return;
            if (isDisposing)
            {
                if (Stop != null) Stop.Dispose();
                if (Cancel != null) Cancel.Dispose();
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TokenSourceSet()
        {
            Dispose(false);
        }
    }
}
