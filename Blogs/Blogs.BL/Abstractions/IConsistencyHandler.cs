using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Abstractions
{
    public interface IConsistencyHandler
    {
        bool IsConsisted { get; }
        void Commit(Guid session);
        void Rollback(Guid session);
        void RollbackAll();
    }
}
