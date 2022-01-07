using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Infrastructure
{
    public interface IAppController : IDisposable
    {
        void SignalStop();
        void SignalCancel();
        event FileSystemEventHandler ProviderStateChanged;
        public void WaitForStop();
        public TokenSourceSet TokenSources { get; }
    }
}
