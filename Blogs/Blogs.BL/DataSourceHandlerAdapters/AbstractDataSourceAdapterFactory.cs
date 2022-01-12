using Blogs.BL.Abstractions;
using System;

namespace Blogs.BL.DataSourceHandlerAdapters
{
    public class AbstractDataSourceAdapterFactory<DTOEntity, T> where T : IDataSourceHandlerAdapter<DTOEntity>
    {
        IDataSourceHandlerFactory<DTOEntity> _dataSourceHandlerFactory;
        EventHandler<IDataSource<DTOEntity>> _forCompleted;
        EventHandler<IDataSource<DTOEntity>> _forFailed;
        EventHandler<IDataSource<DTOEntity>> _forInterrupted;

        public AbstractDataSourceAdapterFactory(
            IDataSourceHandlerFactory<DTOEntity> dataSourceHandlerFactory,
            EventHandler<IDataSource<DTOEntity>> forCompleted,
            EventHandler<IDataSource<DTOEntity>> forFailed,
            EventHandler<IDataSource<DTOEntity>> forInterrupted
            )
        {
            _dataSourceHandlerFactory = dataSourceHandlerFactory;
            _forCompleted = forCompleted;
            _forFailed = forFailed;
            _forInterrupted = forInterrupted;
        }

        public T CreateInstance()
        {
            T temp = Create();
            temp.TaskCompleted += _forCompleted;
            temp.TaskFailed += _forFailed;
            temp.TaskInterrupted += _forInterrupted;
            return temp;
        }

        protected virtual T Create()
        {
            return (T)Activator.CreateInstance(typeof(T), _dataSourceHandlerFactory);
        }
    }
}
