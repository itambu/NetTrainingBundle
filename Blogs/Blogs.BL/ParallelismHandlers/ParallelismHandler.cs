using Blogs.BL.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.ParallelismHandlers
{
    public class ParallelismHandler : IParallelismHandler
    {
        public CancellationTokenSource CancelTokenSource { get; protected set; }
        public CancellationTokenSource StopTokenSource { get; protected set; }
        public TaskScheduler TaskScheduler { get; protected set; }
        protected IProducerConsumerCollection<Task> TaskCollection { get; set; }

        public ParallelismHandler(
            CancellationTokenSource cancelTokenSource,
            CancellationTokenSource stopTokenSource,
            TaskScheduler taskScheduler,
            IProducerConsumerCollection<Task>  taskCollection
            )
        {
            CancelTokenSource = cancelTokenSource;
            StopTokenSource = stopTokenSource;
            TaskScheduler = taskScheduler;
            TaskCollection = taskCollection;
        }

        public void Add(Task task)
        {
            if (!TaskCollection.TryAdd(task))
            {
                throw new InvalidOperationException("Cannot handle task");
            }
        }

        public void Remove(Task task)
        {
            Task temp = task;
            if (!TaskCollection.TryTake(out temp))
            {
                throw new InvalidOperationException("Cannot handle task");
            }
        }

        public async Task RequestCancel()
        {
            StopTokenSource.Cancel();
            CancelTokenSource.Cancel();
            await WaitForCompletion();
        }

        public async Task RequestStop()
        {
            StopTokenSource.Cancel();
            await WaitForCompletion();
        }

        public async Task WaitForCompletion()
        {
            await Task.WhenAll(TaskCollection);
        }
    }
}
