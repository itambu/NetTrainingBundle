using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.IO;
using System.Threading;

namespace Blogs.BL.ProcessManagers
{
    public class EventedFileManager<DTOEntity> : BaseFileManager<DTOEntity>, IProcessHandler<DTOEntity>, ISyncStartable //,IDisposable, IStopEventBinding
    {
        private bool isDisposed = false;
        protected IDataSourceFactory<DTOEntity> DataSourceFactory { get; set; }

        protected TaskBlocker _taskBlocker;

        public EventedFileManager(
              IDataSourceHandlerFactory<DTOEntity> dataSourceHandlerFactory,
              IDataSourceFactory<DTOEntity> dataSourceFactory,
              ActionTokenSet tokens
              ) : base(dataSourceHandlerFactory, tokens)
        {
            DataSourceFactory = dataSourceFactory;
        }

        public EventedFileManager<DTOEntity> Bind(TaskBlocker taskBlocker)
        {
            _taskBlocker = taskBlocker;
            _taskBlocker.ActionConnector += Watcher_Created;
            return this;
        }

        protected void ThrowIfDisposed()
        {
            if (isDisposed)
                throw new InvalidOperationException("object was disposed");
        }

        public void Start()
        {
            ThrowIfDisposed();
        }

        //public EventedFileManager<DTOEntity> Bind()
        //{
        //    return this;
        //}

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            ThrowIfDisposed();
            this.PendingTask(DataSourceFactory.CreateInstance(e.FullPath));
        }

        //public void OnStopHandler(object sender, EventArgs args)
        //{
        //    ThrowIfDisposed();
        //    _appController.ProviderStateChanged -= Watcher_Created;
        //}


        /// <summary>
        /// starts listening and wait. only for async call
        /// </summary>
        /// <param name="pendingTask"></param>
        public override void StartProcess(Action<IDataSource<DTOEntity>> pendingTask)
        {
            ThrowIfDisposed();
            if (_taskBlocker != null)
            {
                _taskBlocker.Start();
                _taskBlocker.WaitForStop();
            }
        }

        //protected void StartWatcher()
        //{
        //    ThrowIfDisposed();
        //    Watcher.EnableRaisingEvents = true;
        //}

        //protected virtual void Dispose(bool isDisposing)
        //{
        //    if (isDisposed) return;
        //    if (isDisposing)
        //    {
        //        if (StopListeningEvent != null) 
        //        { 
        //            StopListeningEvent.Dispose(); 
        //            StopListeningEvent = null; 
        //        }

        //        if (Watcher!=null)
        //        {
        //            Watcher.Dispose();
        //            Watcher = null;
        //        }
        //    }
        //    isDisposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //~EventedFileManager()
        //{
        //    Dispose(false);
        //}
    }
}
