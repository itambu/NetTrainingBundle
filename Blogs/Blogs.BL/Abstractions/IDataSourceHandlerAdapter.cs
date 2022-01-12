using System;

namespace Blogs.BL.Abstractions
{
    public interface IDataSourceHandlerAdapter<DTOEntity> : IDataSourceProceedable<DTOEntity>
    {
        public event EventHandler<IDataSource<DTOEntity>> TaskCompleted;
        public event EventHandler<IDataSource<DTOEntity>> TaskFailed;
        public event EventHandler<IDataSource<DTOEntity>> TaskInterrupted;
    }
}
