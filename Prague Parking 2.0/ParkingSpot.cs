using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Prague_Parking_2._0
{
    public class ParkingSpot
    {
        public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();
        public int ParkingSpotNumber { get; set; }
        public GlobalSettings globalSet;
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
        // Claes parking
        public void RemoveVehicle(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
            AvailableSpace += vehicle.Size;
        }
        // Claes Remove
        public bool CheckAvailableSpace(Vehicle vehicle)
        {
            if (vehicle.Size <= AvailableSpace)
            {
                return true;
            }
            else
                return false;
        }
        // Claes Availability-Check
        public void Print()
        {
            foreach(var vehicle in vehicles) 
            {
                Console.Write("{0:3}: ", ParkingSpotNumber);
                vehicle.Print();
            }
            
        }
        // Claes print method
    }
}
