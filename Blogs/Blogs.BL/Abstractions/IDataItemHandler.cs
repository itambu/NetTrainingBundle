using System;

namespace Blogs.BL.Abstractions
{
    public interface IDataItemHandler<DTOEntity> : IDisposable
    {
        void SaveItem(DTOEntity dtoEntity);
    }
}
