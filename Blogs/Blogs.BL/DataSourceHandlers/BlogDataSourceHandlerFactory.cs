using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blogs.BL.Abstractions;
using Blogs.BL.Abstractions.Factories;

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
                _consistencyHandler) ;
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
