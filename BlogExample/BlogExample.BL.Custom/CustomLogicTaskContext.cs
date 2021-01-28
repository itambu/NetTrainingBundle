using BlogExample.BL.LogicTaskContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom
{
    public class CustomLogicTaskContext : LogicTaskContext<CSVDTO>
    {
        public Guid Session { get; set; }
    }
}
