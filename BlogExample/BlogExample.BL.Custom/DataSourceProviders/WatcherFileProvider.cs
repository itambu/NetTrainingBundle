using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BlogExample.BL.Absractions;
using BlogExample.BL.FileProviders;

namespace BlogExample.BL.Custom.DataSourceProviders
{
    public class WatcherFileProvider : BaseFileProvider<CSVDTO>, IDataSourceProvider<CSVDTO>, IProcessHandler, IDisposable
    {
        protected FileSystemWatcher Watcher { get; set; }
        public WatcherFileProvider(FileSystemWatcher watcher)
            : base()
        {
            Watcher = watcher;
            Watcher.Path = SourceFolder;
            Watcher.Filter = this.SearchPattern;
            Watcher.Created += OnFileSystemEvent;
        }

        public void Start()
        {
            if (Watcher != null)
            {
                Watcher.EnableRaisingEvents = true;
            }
        }

        public void Stop()
        {
            if (Watcher != null)
            {
                Watcher.EnableRaisingEvents = false;
            }
        }

        public void Dispose()
        {
            if (Watcher!=null)
            {
                Watcher.Dispose();
                GC.SuppressFinalize(this);
                Watcher = null;
            }
        }

        ~WatcherFileProvider()
        {
            Dispose();
        }

        protected void OnFileSystemEvent(object sender, FileSystemEventArgs e)
        {
            OnNew(this, new CsvDTOParser(e.FullPath, this.DestFolder) );
        }

        public void Cancel()
        {
            Stop();
        }
    }
}
