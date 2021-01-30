using System;
using System.Collections.Generic;
using System.Text;

namespace Prague_Parking_2._1
{
    class Bike : Vehicle
    {
        GlobalSettings set = new GlobalSettings();
        public Bike()
        {
            set = GlobalSettings.ReadSettingsFromFile();
            base.Size = set.bikeSize;
        }
    }
}
