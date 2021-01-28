using BlogExample.BL.LogicTaskContexts;
using BlogExample.BL.CSVParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Strategies
{
    public interface IGenericLogicTaskStrategy<TDataItem, TLogicTaskContext> : IDisposable
        where TLogicTaskContext : LogicTaskContext<TDataItem>
    {
         event EventHandler<TLogicTaskContext> TaskStarting;
         event EventHandler<TLogicTaskContext> TaskCancelled;
         event EventHandler<TLogicTaskContext> TaskCompleted;
         event EventHandler<TLogicTaskContext> TaskFaulted;

         EventHandler<TLogicTaskContext> DataItemHandler { get; }
         void Execute(TLogicTaskContext taskContext);
    }
}
