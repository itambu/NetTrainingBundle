using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Threading;

namespace Blogs.BL.ProcessManagers
{
    public class BaseDataSourceAdapter<DTOEntity> : IDataSourceHandlerAdapter<DTOEntity>
    {
        protected IDataSourceHandlerFactory<DTOEntity> DataSourceHandlerFactory { get; set; }
        public event EventHandler<IDataSource<DTOEntity>> TaskCompleted;
        public event EventHandler<IDataSource<DTOEntity>> TaskFailed;
        public event EventHandler<IDataSource<DTOEntity>> TaskInterrupted;

        public BaseDataSourceAdapter(IDataSourceHandlerFactory<DTOEntity> dataSourceHandlerFactory)
        {
            DataSourceHandlerFactory = dataSourceHandlerFactory;
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

        public virtual void PendingTask(object sender, IDataSource<DTOEntity> dataSource)
        {
            TaskCompletionStatus status = TaskCompletionStatus.Success;
            using (dataSource)
            {
                try
                {
                    using (var handler = DataSourceHandlerFactory.CreateInstance(dataSource))
                    {
                        handler.Start();
                    }
                }
                catch (HandlerException)
                {
                    status = TaskCompletionStatus.Failed;
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
            Callback(status, dataSource);
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

