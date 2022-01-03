using Blogs.BL.Abstractions;
using Blogs.BL.BaseHandlers;
using Blogs.BL.Infrastructure;
using System;
using System.Threading;

namespace Blogs.BL.ProcessManagers
{
    public abstract class BaseFileManager<DTOEntity>
    {
        protected ActionTokenSet Tokens { get; private set; }
        protected IDataSourceHandleBuilder<DTOEntity> DataSourceHandlerBuilder { get; set; }
        
        public event EventHandler<IDataSource<DTOEntity>> TaskCompleted;
        public event EventHandler<IDataSource<DTOEntity>> TaskFailed;
        public event EventHandler<IDataSource<DTOEntity>> TaskInterrupted;

        public BaseFileManager(IDataSourceHandleBuilder<DTOEntity> dataSourceHandleBuilder,
            ActionTokenSet tokens)
        {
            DataSourceHandlerBuilder = dataSourceHandleBuilder;
            Tokens = tokens;
        }

        protected virtual void OnTaskCompleted(object sender, IDataSource<DTOEntity> source)
        {
            EventHandler<IDataSource<DTOEntity>> temp = null;
            Interlocked.Exchange(ref temp, TaskCompleted);
            temp?.Invoke(sender, source);
        }

        protected virtual void OnTaskInterrupted(object sender, IDataSource<DTOEntity> source)
        {
            EventHandler<IDataSource<DTOEntity>> temp = null;
            Interlocked.Exchange(ref temp, TaskInterrupted);
            temp?.Invoke(sender, source);
        }

        protected virtual void OnTaskFailed(object sender, IDataSource<DTOEntity> source)
        {
            EventHandler<IDataSource<DTOEntity>> temp = null;
            Interlocked.Exchange(ref temp, TaskFailed);
            temp?.Invoke(sender, source);
        }

        public abstract void StartProcess(Action<IDataSource<DTOEntity>> pendingTask);

        public virtual void PendingTask(
            IDataSource<DTOEntity> source
            )
        {
            TaskCompletionStatus status = TaskCompletionStatus.Success;
            using (source)
            {
                try
                {
                    using (var handler = DataSourceHandlerBuilder.Build(source))
                    {
                        handler.Run();
                    }
                }
                catch (HandlerException)
                {
                    status =  TaskCompletionStatus.Failed;
                }
                catch (OperationCanceledException)
                {
                    status = TaskCompletionStatus.Interrupted;
                }
                catch (Exception)
                {
                    status = TaskCompletionStatus.Failed;
                }
            }
            Callback(status, source);
        }

        public virtual void Callback(TaskCompletionStatus status, IDataSource<DTOEntity> source)
        {
            switch (status)
            {
                case TaskCompletionStatus.Success:
                    OnTaskCompleted(this, source);
                    break;
                case TaskCompletionStatus.Failed:
                    OnTaskFailed(this, source);
                    break;
                case TaskCompletionStatus.Interrupted:
                    OnTaskInterrupted(this, source);
                    break;
            }
        }
    }
}

