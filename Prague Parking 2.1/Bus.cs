using System;
using System.Collections.Generic;
using System.Text;

namespace Prague_Parking_2._1
{
    class Bus : Vehicle
    {
        GlobalSettings set = new GlobalSettings();
        public Bus()
        {
            set = GlobalSettings.ReadSettingsFromFile();
            base.Size = set.busSize;
        }
    }
}
