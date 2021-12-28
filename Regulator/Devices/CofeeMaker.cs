using System;
using System.Timers;

namespace Devices
{
    public class CofeeMaker : IHeatable
    {
        public int Temperature { get; private set; }
        Thermometer thermometer;
        readonly int power = 10; // мощность: увеличение температуры кофеварки (град./с)
        Timer heatTimer;
        Timer coolingTimer;
        public bool IsEngaged;

        public bool IsCofeeReady;

        public CofeeMaker(Thermometer thermometr)
        {
            this.thermometer = thermometr;
            Temperature = thermometr.Measure();
            IsEngaged = false;
            heatTimer = new Timer(1000);
            heatTimer.Stop();
            heatTimer.Elapsed += Heat;
            coolingTimer = new Timer(1000);
            coolingTimer.Stop();
            coolingTimer.Elapsed += CoolOut;
            IsCofeeReady = false;

        }

        public void Engage()
        {
            IsCofeeReady = false;
            SwitchOn();
        }

        public void SwitchOn()
        {
            IsEngaged = true;
            coolingTimer.Stop();
            heatTimer.Start();
        }

        public void Disengage()
        {

            if (IsCofeeReady)
            {
                IsEngaged = false;
                heatTimer.Stop();
                coolingTimer.Start();
            }
        }

        void Heat(object obj, ElapsedEventArgs e)
        {

            Temperature += power;
            if (Temperature >= 100)
            {
                IsCofeeReady = true;
                Disengage();
                PrintInfo("Кофе готов: ");
            }
            else
                PrintInfo("Нагрев");
        }

        void CoolOut(object obj, ElapsedEventArgs e)
        {
            var t = thermometer.Measure();
            Temperature = t + (int)((Temperature - t) * 0.75);
            if (Temperature == t)
            {
                coolingTimer.Stop();
                PrintInfo("Кофеварка остыла до температуры окружающей среды");
            }
            else
                PrintInfo("Остывание");
        }

        void PrintInfo(string message)
        {
            Console.Write(message);
            Console.WriteLine($": температура кофеварки {Temperature}°");
        }
    }
}