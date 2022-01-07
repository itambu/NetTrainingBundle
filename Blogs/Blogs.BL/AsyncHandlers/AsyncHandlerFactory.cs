using Blogs.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.AsyncHandlers
{
    public class AsyncHandlerFactory<DTOEntity>
    {
        public IAsyncHandler<DTOEntity> CreateInstance(
            IProcessHandler<DTOEntity> processHandler, 
            AsyncHandlerOptions options)
        {
            return new BaseAsyncHandler<DTOEntity>(processHandler, options);
        }
    }
}
