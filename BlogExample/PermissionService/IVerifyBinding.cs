using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistanceService
{
    public interface IVerifyBinding<T1, T2>
    {
        bool HasBinding(T1 t1, T2 t2);
    }
}
