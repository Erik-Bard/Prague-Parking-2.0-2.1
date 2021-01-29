using System;
using System.Collections.Generic;
using System.Text;

namespace Prague_Parking_2._0
{
    public class MC : Vehicle
    {
        GlobalSettings set = new GlobalSettings();
        public MC()
        {
            set = GlobalSettings.ReadSettingsFromFile();
            base.Size = set.mcSize;
        }

        public new void Print()
        {
            Console.Write("MC  ");
            base.Print();
        }
    }
}
