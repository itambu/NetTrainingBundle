using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.BaseHandlers
{
    public abstract class BaseHandler
    {
        
        protected readonly CancellationToken CancelToken;
        public BaseHandler(CancellationToken cancelToken)
        {
            CancelToken = cancelToken;
        }

        protected abstract void Dispose(bool isDisposing);
    }
}
