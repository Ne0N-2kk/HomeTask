using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices
{
    public class Thermometer
    {
        int outsideTemperature;

        public Thermometer(int temperature)
        {
            outsideTemperature = temperature;
        }

        public int Measure()
        {
            return outsideTemperature;
        }
    }
}
