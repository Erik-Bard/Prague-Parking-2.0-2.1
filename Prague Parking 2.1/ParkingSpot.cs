using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Prague_Parking_2._1
{
    public class ParkingSpot
    {
        public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();
        public int ParkingSpotNumber { get; set; }
        public static GlobalSettings globalSet;
        public ParkingSpot(int parkingSpotNumber)
        {
            TotalSpace = globalSet.ParkingSpotSize;
            ParkingSpotNumber = parkingSpotNumber;
            AvailableSpace = globalSet.ParkingSpotSize;
        }
        public ParkingSpot()
        {

        }
        public int TotalSpace { get; set; }         // Size of EMPTY spot
        public int AvailableSpace { get; set; }    // Available space
        public static void InstallSettings()
        {
            globalSet = GlobalSettings.ReadSettingsFromFile();
        }
        public bool AddVehicle(Vehicle vehicle)
        {
            if (CheckAvailableSpace(vehicle))
            {
                vehicles.Add(vehicle);
                AvailableSpace -= vehicle.Size;
                return true;
            }
            return false;
        }
        public void RemoveVehicle(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
            AvailableSpace += vehicle.Size;
        }
        public bool CheckAvailableSpace(Vehicle vehicle)
        {
            if (vehicle.Size <= AvailableSpace)
            {
                return true;
            }
            else
                return false;
        }
        public void Print()
        {
            Console.Write($"At:{ParkingSpotNumber}");
            foreach (var vehicle in vehicles) 
            {
                Console.Write(", ");
                vehicle.Print();
            }
        }
    }
}
