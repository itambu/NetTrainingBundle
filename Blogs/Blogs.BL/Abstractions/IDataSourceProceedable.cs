using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IDataSourceProceedable<DTOEntity>
    {
        void PendingTask(object sender, IDataSource<DTOEntity> source);
    }
}
