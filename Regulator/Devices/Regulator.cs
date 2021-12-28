using System;
using System.Timers;

namespace Devices

{

    public interface IHeatable
    {
        int Temperature { get; }
        void Engage();
        void Disengage();
        bool isEngaged { get; }
    }

    public class Regulator
    {
        IHeatable device;
        Timer timer;
        int minT, maxT, timeRemained;
        public Regulator(IHeatable device)
        {
            this.device = device;
            timer = new Timer(500);
            timer.Stop();
            timer.Elapsed += Control;
        }

        public void Regulate(int minTemperature, int maxTemperature, int timeInSeconds)
        {
            minT = minTemperature;
            maxT = maxTemperature;
            timeRemained = timeInSeconds;
            timer.Start();
        }

        void Control(object obj, ElapsedEventArgs e)
        {
            if (device.Temperature <= minT && !device.IsEngaged)
                device.Engage();
            else if(device.Temperature >= maxT && device.IsEngaged)
                device.Disengage();
            timeRemained = (int)((timeRemained * 1000 - timer.Interval) / 1000);
            if(timeRemained <= 0)
            {
                timer.Stop();
                device.Disengage();
                Console.WriteLine("Регулятор: время истекло");
            }
        }

    }
}
