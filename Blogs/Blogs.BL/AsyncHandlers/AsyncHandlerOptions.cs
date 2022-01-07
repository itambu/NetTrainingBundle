using Blogs.BL.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.AsyncHandlers
{
    public class AsyncHandlerOptions
    {
        private IProducerConsumerCollection<Task> _taskCollection;
        private ActionTokenSet _tokens;
        private TaskScheduler _scheduler;

        public virtual IProducerConsumerCollection<Task> TaskCollection 
        {
            get => _taskCollection ?? new ConcurrentBag<Task>();
            set => _taskCollection = value; 
        }
        public virtual ActionTokenSet Tokens 
        { 
            get => _tokens; 
            set => _tokens = value; 
        }
        //public virtual ILocker Locker 
        //{ 
        //    get => _locker; 
        //    set => _locker = value; 
        //}
        public virtual TaskScheduler Scheduler 
        { 
            get => _scheduler ?? TaskScheduler.Default; 
            set => _scheduler = value; 
        }
    }
}
