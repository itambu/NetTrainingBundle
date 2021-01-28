using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.LogicTaskHandlers
{
    public interface ILogicTaskHandler : IProducerConsumerCollection<Task>
    {
        TaskFactory TaskFactory { get; }
        bool WaitAll(int timeOut = 0);
    }
}
