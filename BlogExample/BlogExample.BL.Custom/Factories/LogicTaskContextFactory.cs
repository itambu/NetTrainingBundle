using BlogExample.BL.LogicTaskContexts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.Factories
{
    public class CustomLogicTaskContextFactory : ILogicTaskContextFactory<CustomLogicTaskContext, CSVDTO>
    {
        public CustomLogicTaskContext CreateInstance()
        {
            return new CustomLogicTaskContext()
            {
                Session = Guid.NewGuid()
            };
        }
    }
}
