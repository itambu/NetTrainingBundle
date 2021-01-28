using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Absractions
{
    public interface IProcessHandler
    {
        void Start();
        void Stop();
        void Cancel();
    }
}
