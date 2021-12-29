using Blogs.BL.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Blogs.BL.ProcessManagers
{
    public class EventedFileManager<DTOEntity> : BaseFileManager<DTOEntity>, IFileProcessManager<DTOEntity>
    {
        protected FileSystemWatcher Watcher { get; set; }
        protected IDataSourceFactory<DTOEntity> DataSourceFactory { get; set; }

        //protected ConcurrentBag 
        public EventedFileManager(
              IDataSourceHandleBuilder<DTOEntity> dataSourceHandleBuilder,
              IParallelismHandler parallelismHandler,
              FileSystemWatcher watcher,
              IDataSourceFactory<DTOEntity> dataSourceFactory
              ) : base(dataSourceHandleBuilder, parallelismHandler)
        {
            Watcher = watcher;
            DataSourceFactory = dataSourceFactory;
        }

        protected override void RunUnsafe()
        {
            Watcher.Created += Watcher_Created;
            Watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            lock (ParallelismHandler)
            {
                this.CreateTask(DataSourceFactory.CreateInstance(e.FullPath));
            }
        }

        public void OnStopHandler(object sender, EventArgs args)
        {
            Watcher.EnableRaisingEvents = false;
            Watcher.Created -= Watcher_Created;
            ParallelismHandler.WaitForCompletion().Wait();
        }


    }
}
