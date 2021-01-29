using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Prague_Parking_2._0
{
    public class GlobalSettings
    {
        public string configFileName = "../../../DATA/ConfigSettings.json";
        public int ParkingSpotSize { get; set; }        // [DEFAULT]  4
        public int ParkingHouseSize { get; set; }           // [DEFAULT]  100
        public int bikeSize { get; set; }
        public int mcSize { get; set; }
        public int carSize { get; set; }
        public int busSize { get; set; }
        public static GlobalSettings ReadSettingsFromFile(string filePath = "../../../DATA/ConfigSettings.json")
        {
            string settingsJson = File.ReadAllText(filePath);
            GlobalSettings globalSettings = JsonConvert.DeserializeObject<GlobalSettings>(settingsJson);
            return globalSettings;
        }
    }
}
