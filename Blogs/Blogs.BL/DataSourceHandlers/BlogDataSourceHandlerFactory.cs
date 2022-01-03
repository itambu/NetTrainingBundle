using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blogs.BL.Abstractions;

namespace Blogs.BL.DataSourceHandlers
{
    public class BlogDataSourceHandlerFactory : IDataSourceHandlerFactory<BlogDataSourceDTO>
    {
        private readonly IConsistencyHandler _consistencyHandler;
        public IDataSourceHandler CreateInstance(
            IDataSource<BlogDataSourceDTO> source, 
            IDataItemHandler<BlogDataSourceDTO> handler,
            CancellationToken cancellationToken)
        {
            return new DataSourceHandler<BlogDataSourceDTO>(source, handler, cancellationToken, _consistencyHandler) ;
        }

        public BlogDataSourceHandlerFactory(IConsistencyHandler consistencyHandler )
        {
            _consistencyHandler = consistencyHandler;
        }
    }
}
