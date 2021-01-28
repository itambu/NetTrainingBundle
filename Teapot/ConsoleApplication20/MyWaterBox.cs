using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public class MyWaterBox : WaterBox
    {
        public MyWaterBox()
            : base(1.5)
        {

        }

        public override void Heat(double TotalPower)
        {
            base.Heat(TotalPower*0.95);
        }
    }
}
