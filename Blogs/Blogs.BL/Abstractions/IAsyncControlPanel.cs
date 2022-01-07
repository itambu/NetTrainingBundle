using Blogs.BL.Abstractions;
using Blogs.BL.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncControlPanel<DTOEntity> : ITaskEventable<DTOEntity>, IDisposable
    {
        TaskBlocker TaskBlocker { get; }
        TokenSourceSet TokenSources { get; }

        IAsyncControlPanel<DTOEntity> Add(IAsyncHandler<DTOEntity> handler);
        Task SignalCancel();
        Task SignalStop();
        Task StartAsync();
    }
}