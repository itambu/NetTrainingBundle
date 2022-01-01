using Blogs.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.Infrastructure
{
    public class TokenSourceSet : IDisposable
    {
        bool isDisposed = false;
        public CancellationTokenSource Stop { get; protected set; }
        public CancellationTokenSource Cancel { get; protected set; }

        public TokenSourceSet(CancellationTokenSource stop, CancellationTokenSource cancel)
        {
            Stop = stop;
            Cancel = cancel;
        }

        public ActionTokenSet Tokens { get => new ActionTokenSet() { Stop = Stop.Token, Cancel = Cancel.Token }; }

        public void Dispose()
        {
            if (isDisposed) return;

            if (Stop != null) Stop.Dispose();
            if (Cancel != null) Cancel.Dispose();
            GC.SuppressFinalize(this);
            isDisposed = true;
        }

        ~TokenSourceSet()
        {
            Dispose();
        }
    }
}
