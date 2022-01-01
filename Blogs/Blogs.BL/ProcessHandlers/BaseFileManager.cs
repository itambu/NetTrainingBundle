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
        
        public event EventHandler<IBlogDataSource<DTOEntity>> TaskCompleted;
        public event EventHandler<IBlogDataSource<DTOEntity>> TaskFailed;
        public event EventHandler<IBlogDataSource<DTOEntity>> TaskInterrupted;

        public BaseFileManager(IDataSourceHandleBuilder<DTOEntity> dataSourceHandleBuilder,
            ActionTokenSet tokens)
        {
            DataSourceHandlerBuilder = dataSourceHandleBuilder;
            Tokens = tokens;
        }

        protected virtual void OnTaskCompleted(object sender, IBlogDataSource<DTOEntity> source)
        {
            EventHandler<IBlogDataSource<DTOEntity>> temp = null;
            Interlocked.Exchange(ref temp, TaskCompleted);
            temp?.Invoke(sender, source);
        }

        protected virtual void OnTaskInterrupted(object sender, IBlogDataSource<DTOEntity> source)
        {
            EventHandler<IBlogDataSource<DTOEntity>> temp = null;
            Interlocked.Exchange(ref temp, TaskInterrupted);
            temp?.Invoke(sender, source);
        }

        protected virtual void OnTaskFailed(object sender, IBlogDataSource<DTOEntity> source)
        {
            EventHandler<IBlogDataSource<DTOEntity>> temp = null;
            Interlocked.Exchange(ref temp, TaskFailed);
            temp?.Invoke(sender, source);
        }

        public abstract void StartProcess(Action<IBlogDataSource<DTOEntity>> pendingTask);

        public virtual void PendingTask(
            IBlogDataSource<DTOEntity> source
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

        public virtual void Callback(TaskCompletionStatus status, IBlogDataSource<DTOEntity> source)
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

