using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.Infrastructure
{
    public class TaskBlocker 
    {
        private bool isDisposed = false;
        private FileSystemWatcher _watcher;
        private ManualResetEvent stopThreadEvent = new ManualResetEvent(false);

        public TaskBlocker(FileSystemWatcher watcher)
        {
            _watcher = watcher;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            stopThreadEvent.Reset();
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            stopThreadEvent.Set();
        }

        public void Dispose(bool isDisposing)
        {
            if (isDisposed) return;
            if(isDisposing)
            {
                if (stopThreadEvent != null)
                {
                    stopThreadEvent.Dispose();
                    stopThreadEvent = null;
                }
                if (_watcher!=null)
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

        ~TaskBlocker() => Dispose(false);

        public event FileSystemEventHandler ActionConnector
        {
            add { _watcher.Created += value;  }
            remove { _watcher.Created -= value; }
        }

        public void WaitForStop()
        {
            stopThreadEvent.WaitOne();
        }
    }
}
