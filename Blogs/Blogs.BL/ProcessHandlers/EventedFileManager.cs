using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.IO;
using System.Threading;

namespace Blogs.BL.ProcessManagers
{
    public class EventedFileManager<DTOEntity> : BaseFileManager<DTOEntity>, IProcessHandler<DTOEntity>, IDisposable
    {
        protected bool isDisposed = false;
        protected FileSystemWatcher Watcher { get; set; }
        protected IDataSourceFactory<DTOEntity> DataSourceFactory { get; set; }

        protected AutoResetEvent StopListeningEvent = new AutoResetEvent(false);

        public EventedFileManager(
              IDataSourceHandleBuilder<DTOEntity> dataSourceHandleBuilder,
              ActionTokenSet tokens,
              IDataSourceFactory<DTOEntity> dataSourceFactory,
              FileSystemWatcher watcher
              ) : base(dataSourceHandleBuilder,  tokens)
        {
            Watcher = watcher;
            DataSourceFactory = dataSourceFactory;
        }

        /// <summary>
        /// starts listening in sync model app
        /// </summary>
        /// 

        protected void ThrowIfDisposed()
        {
            if (isDisposed)
                throw new InvalidOperationException("object was disposed");
        }

        public void Start()
        {
            ThrowIfDisposed();
            StartWatcher();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            ThrowIfDisposed();
            this.PendingTask(DataSourceFactory.CreateInstance(e.FullPath));
        }

        public void OnStopHandler(object sender, EventArgs args)
        {
            ThrowIfDisposed();
            Watcher.EnableRaisingEvents = false;
            Watcher.Created -= Watcher_Created;
            StopListeningEvent.Set();
        }


        /// <summary>
        /// starts listening and wait. only for async call
        /// </summary>
        /// <param name="pendingTask"></param>
        public override void StartProcess(Action<IBlogDataSource<DTOEntity>> pendingTask)
        {
            ThrowIfDisposed();
            StartWatcher();
            StopListeningEvent.WaitOne();
        }

        protected void StartWatcher()
        {
            ThrowIfDisposed();
            Watcher.Created += Watcher_Created;
            Watcher.EnableRaisingEvents = true;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed) return;
            if (isDisposing)
            {
                if (StopListeningEvent != null) 
                { 
                    StopListeningEvent.Dispose(); 
                    StopListeningEvent = null; 
                }
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~EventedFileManager()
        {
            Dispose();
        }
    }
}
