using Blogs.BL.Infrastructure;
using System;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncControlPanel<DTOEntity> : IAsyncStartable, IAsyncStoppable, IAsyncCancelable, IDisposable
    {
        TokenSourceSet TokenSources { get; }
        IAsyncControlPanel<DTOEntity> Add(IAsyncAdapter<DTOEntity> handler);
        event EventHandler StopRequested;
    }
}