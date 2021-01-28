using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public class WaterBox
    {
        const double MagicConst = 4200;

        public double Volume { get; private set; }
        public double MaxVolume { get; private set; }
        public double Tempature { get; private set; }

        public void Add(double volume, double tempature)
        {
            tempature += (Tempature * Volume + volume * tempature) / (Volume + volume);
            Volume += volume;
        }

        public void Clear()
        {
            Volume = 0;
        }

        public virtual void Heat(double TotalPower)
        {
            Tempature += TotalPower / (Volume * MagicConst);
        }

        public WaterBox(double maxVolume)
        {
            MaxVolume = maxVolume;
        }


    }
}
