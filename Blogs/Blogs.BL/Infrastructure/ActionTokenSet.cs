using System.Threading;

namespace Blogs.BL.Infrastructure
{
    public struct ActionTokenSet
    {
        public CancellationToken Stop { get; set; }
        public CancellationToken Cancel { get; set; }
        public bool IsCancelled { get => Cancel.IsCancellationRequested; }
        public bool IsStopped { get => Stop.IsCancellationRequested; }
    }
}
