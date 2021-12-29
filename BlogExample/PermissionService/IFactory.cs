using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistanceService
{
    public interface IFactory<T>
    {
        T CreateInstance();
    }
}
