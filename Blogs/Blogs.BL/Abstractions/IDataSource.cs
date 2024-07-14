using System;
using System.Collections.Generic;

namespace Blogs.BL.Abstractions
{
    public interface IDataSource<DTOEntity> : IDisposable, IEnumerable<DTOEntity>
    {
        void Close();
        Guid Id { get; }
    }
}
