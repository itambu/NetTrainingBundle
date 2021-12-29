using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IDataSourceHandlerFactory<DTOEntity>
    {
        IDataSourceHandler CreateInstance(
            IBlogDataSource<DTOEntity> source, 
            IDataItemHandler<DTOEntity> handler, 
            CancellationToken cancellationToken);
    }
}
