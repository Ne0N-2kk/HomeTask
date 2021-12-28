using System;
using Devices;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var thermometer = new Thermometer(22);
            var oven = new Oven(thermometer, 5);

            var regulator = new Regulator((IHeatable)oven);
            //regulator.Regulate(180, 200, 120);

            var cofeeMaker = new CofeeMaker(thermometer);
            cofeeMaker.SwitchOn();

            Console.ReadKey();
        }
    }
}
