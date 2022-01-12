using Blogs.DAL.Abstractions;
using System;

namespace Blogs.BL.Abstractions
{
    public interface ISingleEntityUoW<Entity> : IDisposable where Entity : class
    {
        IGenericRepository<Entity> Repository { get; }
    }
}
