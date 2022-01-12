using Blogs.BL.Abstractions;
using Blogs.BL.Abstractions.Factories;
using System.Threading;

namespace Blogs.BL.DataSourceHandlers
{
    public class BlogDataSourceHandlerFactory : IDataSourceHandlerFactory<BlogDataSourceDTO>
    {
        private IConsistencyHandler _consistencyHandler;
        private IDataItemHandlerFactory<BlogDataSourceDTO> _dataItemHandlerFactory;
        private CancellationToken _cancelToken;

        public IDataSourceHandler CreateInstance(IDataSource<BlogDataSourceDTO> source)
        {
            return new DataSourceHandler<BlogDataSourceDTO>(
                source,
                _dataItemHandlerFactory.CreateInstance(),
                _cancelToken,
                _consistencyHandler);
        }

        public BlogDataSourceHandlerFactory(
            IDataItemHandlerFactory<BlogDataSourceDTO> _dataItemHandlerFactory,
            IConsistencyHandler consistencyHandler,
            CancellationToken cancelToken
            )
        {
            this._dataItemHandlerFactory = _dataItemHandlerFactory;
            _consistencyHandler = consistencyHandler;
            _cancelToken = cancelToken;
        }
    }
}
