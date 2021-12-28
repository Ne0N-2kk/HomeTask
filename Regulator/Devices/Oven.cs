using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Devices
{
    public class Oven
    {
        public int Temperature { get; private set; }
        Thermometer thermometer;
        int power; // мощность: увеличение температуры печи (град./с)
        Timer heatTimer;
        Timer coolingTimer;
        public bool IsEngaged;

        public Oven(Thermometer thermometr, int power)
        {
            this.thermometer = thermometr;
            Temperature = thermometr.Measure();
            this.power = power;
            IsEngaged = false;
            heatTimer = new Timer(1000);
            heatTimer.Stop();
            heatTimer.Elapsed += EncreaseTemperature;
            coolingTimer = new Timer(1000);
            coolingTimer.Stop();
            coolingTimer.Elapsed += DecreaseTemperature;
        }

        public void Engage()
        {
            IsEngaged = true;
            coolingTimer.Stop();
            heatTimer.Start();
        }

        public void Disengage()
        {
            IsEngaged = false;
            heatTimer.Stop();
            coolingTimer.Start();
        }

        void EncreaseTemperature(object obj, ElapsedEventArgs e)
        {
            Temperature += power;
            PrintInfo("Нагрев");
        }

        void DecreaseTemperature(object obj, ElapsedEventArgs e)
        {
            var t = thermometer.Measure();
            Temperature = t + (int)((Temperature - t) * 0.95);
            if (Temperature == t)
            {
                coolingTimer.Stop();
                PrintInfo("Печь остыла до температуры окружающей среды");
            }
            else    
                PrintInfo("Остывание");
        }

        void PrintInfo(string message)
        {
            Console.Write(message);
            Console.WriteLine($": температура печи {Temperature}°");
        }
    }
}
