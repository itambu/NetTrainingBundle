using Blogs.BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncHandler<DTOEntity> : ITaskEventable<DTOEntity>
    {
        void PendingTask(IDataSource<DTOEntity> source);
        Task WhenAll();
        Task StartMainProcess();
        Task WhenMainProcess();
    }
}
