using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public partial class Teapot
    {
        private WaterBox waterBox;
        private Heater heater;
        private PowerButton powerButton;

        public PowerButton PowerButton
        {
            get { return powerButton; }
            private set { powerButton = value; }
        }

        public Heater Heater
        {
            get { return heater; }
            private set { heater = value; }
        }

        public WaterBox WaterBox
        {
            get { return waterBox; }
            private set { waterBox = value; }
        }

        public Teapot(WaterBox waterBox, Heater heater, PowerButton powerButton)
        {
            this.WaterBox = waterBox;
            this.Heater = heater;
            this.PowerButton = powerButton;
        }



    }
}
