using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IParallelismHandler
    {
        Task RequestCancel();
        void Add(Task task);
        void Remove(Task task);
        Task RequestStop();
        CancellationTokenSource CancelTokenSource { get; }
        CancellationTokenSource StopTokenSource { get; }
        TaskScheduler TaskScheduler { get; }
        Task WaitForCompletion();
    }
}
