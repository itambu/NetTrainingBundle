using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.Infrastructure
{
    public interface ILocker
    {
        bool TryLockForStart();
        void ReleaseLockForStart();
    }
}
