using Blogs.BL.Abstractions;
using Blogs.BL.BaseHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.ProcessManagers
{
    public abstract class BaseFileManager<DTOEntity>
    {
        protected IDataSourceHandleBuilder<DTOEntity> DataSourceHandlerBuilder { get; set; }
        protected IParallelismHandler ParallelismHandler { get; set; }

        public event EventHandler<IBlogDataSource<DTOEntity>> TaskCompleted;
        public event EventHandler<IBlogDataSource<DTOEntity>> TaskFailed;
        public event EventHandler<IBlogDataSource<DTOEntity>> TaskInterrupted;

        public BaseFileManager(IDataSourceHandleBuilder<DTOEntity> dataSourceHandleBuilder, IParallelismHandler parallelismHandler)
        {
            DataSourceHandlerBuilder = dataSourceHandleBuilder;
            ParallelismHandler = parallelismHandler;
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

        protected virtual TaskCompletionStatus MainAction(IBlogDataSource<DTOEntity> source)
        {
            using (source)
            {
                try
                {
                    using (var handler = DataSourceHandlerBuilder.Build(source))
                    {
                        handler.Run();
                        return TaskCompletionStatus.Success;
                    }
                }
                catch (HandlerException)
                {
                    return TaskCompletionStatus.Failed;
                }
                catch (OperationCanceledException)
                {
                    return TaskCompletionStatus.Interrupted;
                }
                catch (Exception)
                {
                    return TaskCompletionStatus.Failed;
                }
            }
        }
        protected Task CreateTask(IBlogDataSource<DTOEntity> source)
        {
            Task<TaskCompletionStatus> temp = Task.Factory.StartNew<TaskCompletionStatus>(() =>
                MainAction(source),
                ParallelismHandler.CancelTokenSource.Token,
                TaskCreationOptions.None,
                ParallelismHandler.TaskScheduler);
                ParallelismHandler.Add(temp);
            temp.ContinueWith(t =>
            {
                switch (t.Result)
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
                lock (ParallelismHandler)
                {
                    ParallelismHandler.Remove(t);
                }
            });
            return temp;
        }
    }
}

