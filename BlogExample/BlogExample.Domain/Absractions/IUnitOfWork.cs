using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Absractions
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        void Execute();
    }
}
