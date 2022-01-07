using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions.Factories
{
    public interface IDataItemHandlerFactory<DTOEntity>
    {
        public IDataItemHandler<DTOEntity> CreateInstance();
    }
}
