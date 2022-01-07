using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface ITaskEventable<DTOEntity>
    {
        public event EventHandler<IDataSource<DTOEntity>> TaskCompleted;
        public event EventHandler<IDataSource<DTOEntity>> TaskFailed;
        public event EventHandler<IDataSource<DTOEntity>> TaskInterrupted;
    }
}
