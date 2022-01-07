using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncApp<DTOEntity> : ITaskEventable<DTOEntity>, IDisposable
    {
        Task CancelAsync();
        Task StopAsync();
        Task StartAsync();
        event EventHandler OnStopped;
        event EventHandler OnCancelled;
    }
}
