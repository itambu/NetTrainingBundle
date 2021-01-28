using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.LogicTaskContexts.Factories
{
    public interface ILogicTaskContextFactory<TContext, TDataItem> where TContext  : LogicTaskContext<TDataItem>
    {
        TContext CreateInstance();
    }
}
