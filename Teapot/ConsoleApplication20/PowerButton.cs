using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication20
{
    public class PowerButton
    {
        protected Heater  Heater{ get; set;}
        public PowerButton(Heater heater)
        {
            Heater = heater;
        }

        public void PowerOn(TimeSpan timeSpan)
        {
            Heater.Heat(timeSpan);
        }


    }
}
