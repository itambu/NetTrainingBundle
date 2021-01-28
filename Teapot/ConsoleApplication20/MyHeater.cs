using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public class MyHeater : Heater
    {
        public MyHeater(WaterBox waterBox) : base(700, waterBox)
        {
        }
    }
}
