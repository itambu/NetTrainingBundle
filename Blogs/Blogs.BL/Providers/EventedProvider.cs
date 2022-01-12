using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.IO;
using System.Threading;

namespace Blogs.BL.Providers
{
    public class EventedProvider<DTOEntity> : BaseProvider<DTOEntity>, IDataSourceProvider<DTOEntity>, ISyncStoppable, IDisposable
    {
        private bool isDisposed = false;
        private bool _haveToBlockStartAction;
        //protected TaskBlocker TaskBlocker { get; private set; }

        private FileSystemWatcher _watcher;
        private ManualResetEvent stopThreadEvent = new ManualResetEvent(false);

        public EventedProvider(
            AppFolderOptions appFolderOptions,
            IDataSourceFactory<DTOEntity> dataSourceFactory,
            ActionTokenSet tokenSet,
            //            TaskBlocker taskBlocker,
            bool haveToBlockStartAction = true
            ) : base(appFolderOptions, dataSourceFactory, tokenSet)
        {
            //            TaskBlocker = taskBlocker;
            _watcher = new FileSystemWatcher(AppFolderOptions.Source, AppFolderOptions.Pattern);
            _watcher.Created += Watcher_Created;
            _haveToBlockStartAction = haveToBlockStartAction;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            OnNew(this, DataSourceFactory.CreateInstance(e.FullPath));
        }

        protected void StartWatching()
        {
            _watcher.EnableRaisingEvents = true;
            stopThreadEvent.Reset();
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            stopThreadEvent.Set();
        }

        protected override void StartAction()
        {
            StartWatching();
            if (_haveToBlockStartAction)
            {
                stopThreadEvent.WaitOne();
            }
        }

        public void Dispose(bool isDisposing)
        {
            if (isDisposed) return;
            if (isDisposing)
            {
                if (stopThreadEvent != null)
                {
                    stopThreadEvent.Dispose();
                    stopThreadEvent = null;
                }
                if (_watcher != null)
                {
                    _watcher.Dispose();
                    _watcher = null;
                }
            }
            isDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EventedProvider() => Dispose(false);
    }
}
