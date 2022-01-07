using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IProcessHandler<DTOEntity> : ITaskEventable<DTOEntity>
    {
        void StartProcess(Action<IDataSource<DTOEntity>> pendingTask);
        void PendingTask(IDataSource<DTOEntity> source);
    }
}
