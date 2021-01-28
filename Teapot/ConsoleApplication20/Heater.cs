using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public class Heater
    {
        public double PowerPerHour { get; private set; }
        protected WaterBox WaterBox { get;set;}
        public Heater(double powerPerHour, WaterBox waterBox)
        {
            PowerPerHour = powerPerHour;
            WaterBox = waterBox;
        }

        public void Heat(TimeSpan timeSpan)
        {
            WaterBox.Heat(PowerPerHour * timeSpan.TotalHours);
        }


       
    }
}
