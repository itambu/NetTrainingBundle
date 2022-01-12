using System;

namespace Blogs.BL.Abstractions
{
    public interface IAsyncApp<DTOEntity> : IDisposable, IAsyncStartable, IAsyncStoppable, IAsyncCancelable
    {
        event EventHandler Started;
        event EventHandler Stopped;
        event EventHandler Cancelled;
    }
}
