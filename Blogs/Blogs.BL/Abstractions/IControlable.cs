using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IControlable
    {
        Task Start();
        Task Stop();
        Task Pause();
        Task Resume();
        Task Cancel();

        event EventHandler OnStop;
        event EventHandler OnCancel;
    }
}
