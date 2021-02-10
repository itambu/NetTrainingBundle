using BlogExample.BL.V2.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.V2.EventContainers
{
    public interface IEventContainer<TScope>
    {
        event Action<TScope> Precede;
        event Action Execute;
        event Action Follow;
        void Invoke();
    }
}
