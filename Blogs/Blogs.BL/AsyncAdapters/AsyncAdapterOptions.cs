using Blogs.BL.Infrastructure;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Blogs.BL.AsyncAdapters
{
    public class AsyncAdapterOptions
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
