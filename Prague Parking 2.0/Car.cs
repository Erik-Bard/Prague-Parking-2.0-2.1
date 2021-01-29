using System;
using System.Collections.Generic;
using System.Text;

namespace Prague_Parking_2._0
{
    public class Car : Vehicle
    {
        GlobalSettings set = new GlobalSettings();
        public Car()
        {
            set = GlobalSettings.ReadSettingsFromFile();
            base.Size = set.carSize;
        }

        public new void Print()
        {
            Console.Write("Car ");
            base.Print();
        }
        
    }
}
