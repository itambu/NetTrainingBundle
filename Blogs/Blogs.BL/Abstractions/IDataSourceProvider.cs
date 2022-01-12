using System;

namespace Blogs.BL.Abstractions
{
    public interface IDataSourceProvider<DTOEntity> : ISyncStartable
    {
        event EventHandler<IDataSource<DTOEntity>> New;
    }
}
