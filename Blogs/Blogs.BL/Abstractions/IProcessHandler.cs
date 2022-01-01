using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IProcessHandler<DTOEntity>
    {
        void StartProcess(Action<IBlogDataSource<DTOEntity>> pendingTask);
        void PendingTask(IBlogDataSource<DTOEntity> source);

        public event EventHandler<IBlogDataSource<DTOEntity>> TaskCompleted;
        public event EventHandler<IBlogDataSource<DTOEntity>> TaskFailed;
        public event EventHandler<IBlogDataSource<DTOEntity>> TaskInterrupted;
    }
}
