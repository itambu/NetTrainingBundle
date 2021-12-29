using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IDataSourceFactory<DTOEntity>
    {
        IBlogDataSource<DTOEntity> CreateInstance(string fileName);
    }
}
