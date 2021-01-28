using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    class Program
    {
        static void Main(string[] args)
        {
            WaterBox wb = new MyWaterBox();
            Heater ht = new MyHeater(wb);
            PowerButton pb = new PowerButton(ht);

            Teapot t = new Teapot(waterBox: wb, heater: ht, powerButton: pb);

            t.PowerButton.PowerOn(new TimeSpan(0, 3, 0));


            //MyWaterBox wb1 = wb as MyWaterBox;
            //if (wb1 != null)
            //{
            //    wb1.Add(
            //}
        }
    }
}
