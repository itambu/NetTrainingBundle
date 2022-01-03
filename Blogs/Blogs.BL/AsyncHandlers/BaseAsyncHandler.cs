using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using Blogs.BL.BaseHandlers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.AsyncHandlers
{
    public class BaseAsyncHandler<DTOEntity> : IAsyncHandler<DTOEntity>
    {
        private IProcessHandler<DTOEntity> _handler;
        private Task _mainProcessTask;
        protected TaskScheduler Scheduler { get; set; }
        public ActionTokenSet Tokens { get; protected set; }
        public ILocker Locker { get; protected set; }
        protected IProducerConsumerCollection<Task> TaskCollection { get; set; }

        public BaseAsyncHandler(
            IProcessHandler<DTOEntity> handler,
            IProducerConsumerCollection<Task> taskCollection,
            ActionTokenSet tokens,
            ILocker locker,
            TaskScheduler scheduler)
        {
            _handler = handler;
            TaskCollection = taskCollection;
            Tokens = tokens;
            Locker = locker;
            Scheduler = scheduler;
        }

        public void PendingTask(IDataSource<DTOEntity> source)
        {
            var temp = Task.Factory.StartNew(
                 () => _handler.PendingTask(source),
                 CancellationToken.None,
                 TaskCreationOptions.None,
                 Scheduler);

            if (temp == null || !TaskCollection.TryAdd(temp))
            {
                throw new InvalidOperationException("cannot pending task");
            }
        }

        protected Task MainTask 
        {
            set
            {
                try
                {
                    if (Locker.TryLockForStart())
                    {
                        _mainProcessTask = value;
                    }
                }
                finally
                {
                    Locker.ReleaseLockForStart();
                }
            }
        }

        public Task StartMainProcess()
        {
            try
            {
                return Task.Factory.StartNew(
                    () => MainTask = Task.Factory.StartNew(() => _handler.StartProcess(PendingTask))
                    .ContinueWith(t => MainTask = null)
                    );
            }
            catch (Exception e)
            {
                throw new HandlerException(e);
            }
        }

        public Task WhenAll()
        {
            return Task.WhenAll(TaskCollection);
        }

        public Task WhenMainProcess()
        {
            return _mainProcessTask ?? Task.Delay(0);
        }
    }
}
