using BlogExample.BL.LogicTaskContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Strategies.Factories
{
    public interface ILogicTaskStrategyFactory<TDataItem, TLogicTaskContext>
        where TLogicTaskContext : LogicTaskContext<TDataItem>
    {
        LogicTaskStrategyEventHandlerContainer<TLogicTaskContext, TDataItem> ActionContainer { get; }
        IGenericLogicTaskStrategy<TDataItem, TLogicTaskContext> CreateInstance(TLogicTaskContext taskContext);
    }
}
