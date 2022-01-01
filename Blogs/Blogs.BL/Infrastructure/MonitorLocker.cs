using System.Threading;

namespace Blogs.BL.Infrastructure
{
    public class MonitorLocker : ILocker
    {
        object syncObj = new object();
        public void ReleaseLockForStart()
        {
            Monitor.Exit(syncObj);
        }

        public bool TryLockForStart()
        {
            return Monitor.TryEnter(syncObj, 0);
        }
    }
}
