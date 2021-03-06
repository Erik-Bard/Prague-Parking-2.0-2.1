﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using Spectre.Console;

namespace Prague_Parking_2._0
{
    public class ParkingHouse
    {
        public List<ParkingSpot> parkingSpots;
        public GlobalSettings settings;
        private int _allAvailableSpace;
        public ParkingHouse(string fileName = "../../../DATA/JsonDB.json") : this()
        {
            ReadFromFile(fileName);
            ConstructParkingHouse();
        }
        public int AllAvailableSpace
        {
            get { return _allAvailableSpace = settings.ParkingHouseSize; }
            set { _allAvailableSpace = settings.ParkingHouseSize; }
        }
        public ParkingHouse()
        {
            settings = GlobalSettings.ReadSettingsFromFile();
            parkingSpots = new List<ParkingSpot>(AllAvailableSpace);
        }

        public ParkingSpot FindAvailableSpot(Vehicle vehicle)
        {
            foreach (var parkingSpot in parkingSpots)
            {
                if (parkingSpot.CheckAvailableSpace(vehicle)) 
                {
                    return parkingSpot;  
                }
            }
            return null;
        }
        public void ReadFromFile(string fileName)
        {
            string json = File.ReadAllText(fileName);
            parkingSpots = JsonConvert.DeserializeObject<List<ParkingSpot>>(json);
            foreach (var item in parkingSpots)
            {
                item.TotalSpace = settings.ParkingSpotSize;
            }
            WriteToFile();
        }
        public void WriteToFile()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            string parkingHouseString = JsonConvert.SerializeObject(parkingSpots, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("../../../DATA/JsonDB.json"))
            {
                writer.Write(parkingHouseString);
            }
        }
        public ParkingSpot ReturnParkingSpot(Vehicle vehicle)
        {
            foreach (var item in parkingSpots)
            {
                if (vehicle.Size == 2 && item.TotalSpace >= vehicle.Size)
                {
                    int indexu = parkingSpots.IndexOf(item);
                    Console.WriteLine($"Parking At: {indexu}");
                    return item;
                }
                if (vehicle.Size == 2 && item.TotalSpace! >= vehicle.Size)
                {
                    int indexu = parkingSpots.IndexOf(item);
                    Console.WriteLine($"Parking here: {indexu}");
                    return item;
                }
                if (vehicle.Size == 4 && item.TotalSpace == 4)
                {
                    Console.WriteLine("Empty spot to park your car in.");
                    Console.ReadKey();
                    return item;
                }
            }
            return null;
        }
        // Take vehicle and return first empty / available spot
        public void ParkVehicle()
        {
            Console.WriteLine("Park a Car or Mc?");
            string VehicleChoise = Console.ReadLine();
            VehicleChoise.ToLower();
            Console.WriteLine("Enter registration plate: ");
            string input = Console.ReadLine();
            switch (VehicleChoise)
            {
                case "car":
                    Car car = new Car();
                    car.RegPlate = input;
                    car.ArriveTime = DateTime.Now;

                    ParkingSpot spot;
                    spot = FindAvailableSpot(car);
                    if (spot != null)
                    {
                        spot.AddVehicle(car);
                        WriteToFile();
                        Console.WriteLine("Your car is now parked.");
                        Console.ReadKey();
                    }
                    else
                        Console.WriteLine("No empty spots available.");
                        Console.ReadKey();
                    break;

                case "mc":
                    MC newMC = new MC();
                    newMC.RegPlate = input;
                    newMC.ArriveTime = DateTime.Now;

                    ParkingSpot Spot;
                    Spot = FindAvailableSpot(newMC);
                    Spot.AddVehicle(newMC);
                    WriteToFile();
                    Console.WriteLine("Done");
                    Console.ReadKey();
                    break;
            }
        }
        public void MoveVehicle()
        {
            Vehicle vehicle = new Vehicle();
            vehicle = RemoveAdd();
            ParkReturnedVehicle(vehicle);
            Console.ReadKey();
        }
        public int? SearchForVehicle(string regNumber)
        {
            foreach (var item in parkingSpots)
            {
                foreach (Vehicle v in item.vehicles)
                {
                    if (v.RegPlate.Equals(regNumber))
                    {
                        int indexlmao = parkingSpots.IndexOf(item);
                        return indexlmao;
                    }
                }
            }
            return -1;
        }
        public void RemoveVehicle()
        {
            Console.WriteLine("Which vehicle do you wish us to retrieve and exit with?");
            string VehicleExit = Console.ReadLine();
            int indexPog;
            foreach (var item in parkingSpots)
            {
                foreach (Vehicle v in item.vehicles)
                {
                    if (v.RegPlate.Equals(VehicleExit) && v.Size == 4)
                    {
                        indexPog = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {indexPog}");
                        for (int i = 0; i < item.vehicles.Count; i++)
                        {
                            Console.WriteLine(item.vehicles[i]);
                        }
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        item.vehicles.Remove(v);
                        Console.WriteLine($"To Pay: {TotalPrice} CZK at checkout time: {DateTime.Now}");
                        break;
                    }
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == 2)
                    {
                        indexPog = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {indexPog}");
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        item.vehicles.Remove(v);
                        Console.WriteLine($"To Pay: {TotalPrice} CZK at checkout time: {DateTime.Now}");
                        break;
                    }
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == 2 && item != null)
                    {
                        indexPog = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {indexPog}");
                        // Divide and select the specific vehicle on that spot //
                        for (int i = 0; i < item.vehicles.Count; i++)
                        {
                            Console.WriteLine($"{item.vehicles[i]}, {i}");
                        }
                        Console.WriteLine("Which vehicle would you like to remove?");
                        int IndexChoise = int.Parse(Console.ReadLine());
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        item.vehicles.RemoveAt(IndexChoise);
                        Console.WriteLine($"To Pay: {TotalPrice} CZK at checkout time: {DateTime.Now}");
                        break;
                    }
                }
            }
            Control ctrl = new Control();
            WriteToFile();
            Console.ReadKey();

        }
        public Vehicle RemoveAdd()
        {
            Console.WriteLine("Please insert the regnumber of the vehicle you wish to move!");
            string VehicleExit = Console.ReadLine();
            int indexPog;
            foreach (var item in parkingSpots)
            {
                foreach (Vehicle v in item.vehicles)
                {
                    if (v.RegPlate.Equals(VehicleExit) && v.Size == 4)
                    {
                        indexPog = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {indexPog}");
                        for (int i = 0; i < item.vehicles.Count; i++)
                        {
                            Console.WriteLine(item.vehicles[i]);
                        }
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        Car CarCopy = new Car();
                        CarCopy.RegPlate = v.RegPlate;
                        CarCopy.Size = v.Size;
                        CarCopy.ArriveTime = v.ArriveTime;
                        Car VehicleReturn = CarCopy;
                        item.vehicles.Remove(v);
                        return VehicleReturn;
                    }
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == 2)
                    {
                        indexPog = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {indexPog}");
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        MC MCCopy = new MC();
                        MCCopy.RegPlate = v.RegPlate;
                        MCCopy.Size = v.Size;
                        MCCopy.ArriveTime = v.ArriveTime;
                        MC VehicleReturn = MCCopy;
                        item.vehicles.Remove(v);
                        return VehicleReturn;
                    }
                }
            }
            WriteToFile();
            Console.ReadKey();
            return null;
        }
        public void ParkReturnedVehicle(Vehicle VehicleReturn)
        {
            Car newCar = new Car();
            MC newMC = new MC();
            if (VehicleReturn.Size == newCar.Size)
            {
                newCar.RegPlate = VehicleReturn.RegPlate;
                newCar.Size = VehicleReturn.Size;
                newCar.ArriveTime = VehicleReturn.ArriveTime;
                ParkingSpot newSpot;
                newSpot = FindAvailableSpot(newCar);
                if (newSpot != null)
                {
                    newSpot.AddVehicle(newCar);
                    WriteToFile();
                    Console.WriteLine("Your car is now parked.");
                    Console.ReadKey();
                }
                else
                    Console.WriteLine("No empty spots available.");

            }
            if (VehicleReturn.Size == newMC.Size)
            {
                newMC.RegPlate = VehicleReturn.RegPlate;
                newMC.Size = VehicleReturn.Size;
                newMC.ArriveTime = VehicleReturn.ArriveTime;
                ParkingSpot Spot;
                Spot = FindAvailableSpot(newMC);
                Spot.AddVehicle(newMC);
                WriteToFile();
                Console.WriteLine("Done");
            }
        }
        public void ConstructParkingHouse()
        {
            int NumberOfPlots = settings.ParkingHouseSize;
            if (parkingSpots.Count <= NumberOfPlots)
            {
                for (int i = parkingSpots.Count; i < NumberOfPlots; i++)
                {
                    ParkingSpot parkingspot = new ParkingSpot();
                    int plot = 1 + i;
                    parkingSpots.Add(new ParkingSpot { ParkingSpotNumber = plot });
                }
            }
            else if (parkingSpots.Count >= NumberOfPlots)
            {
                for (int j = parkingSpots.Count; j > NumberOfPlots; j--)
                {
                    if (parkingSpots[j].AvailableSpace == settings.ParkingSpotSize)
                    {
                        parkingSpots.RemoveAt(j);
                    }
                    else if (parkingSpots[j].AvailableSpace != settings.ParkingSpotSize)
                    {
                        // DO NOTHING //
                        NumberOfPlots += 1;
                    }
                }
            }
        }
        public void PrintWithRegNum()
        {
            string fullInfo;
            var title = new Rule("Here is a list of parking spots with Vehicles and Regnumbers");
            AnsiConsole.Render(title);
            for (int i = 0; i < parkingSpots.Count; i++)
            {
                var table = new Table();
                table.Centered().Expand();
                int length = i + 9;
                for (int j = i; j < length; j++)
                {
                    fullInfo = "";
                    if (j >= parkingSpots.Count)
                    {
                        break;
                    }
                    if (parkingSpots[j].AvailableSpace < 4)
                    {
                        for (int k = 0; k < parkingSpots[j].vehicles.Count; k++)
                        {
                            fullInfo = fullInfo + parkingSpots[j].vehicles[k].Size + "," + parkingSpots[j].vehicles[k].RegPlate + " ";
                        }
                    }
                    if (fullInfo == "") { fullInfo = "Empty Lot"; }
                    table.AddColumn($"Contents: {fullInfo}");
                }
                i += 8;
                AnsiConsole.Render(table);
            }
        }
        public void Print()
        {
            var MapTitle = new Rule("Map over the Parkinghouse");
            AnsiConsole.Render(MapTitle);
            var ColorCode = new Rule("Green = Empty, Yellow = Half filled, Red = Full");
            AnsiConsole.Render(ColorCode);
            for (int i = 0; i < parkingSpots.Count; i++)
            {
                Table Table = new Table().Centered();
                string colour;
                var Result = "";
                int length = i + 25;      // [NOTE] SET X amount of spots/row 
                for (int j = i; j < length; j++)
                {
                    if (j >= parkingSpots.Count)
                    {
                        break;
                    }
                    if (parkingSpots[j].AvailableSpace == settings.ParkingSpotSize)
                    {
                        colour = "green";
                    }
                    else if (parkingSpots[j].AvailableSpace == settings.ParkingSpotSize - settings.mcSize && parkingSpots[j].AvailableSpace == settings.mcSize)
                    {
                        colour = "yellow";
                    }
                    else if (parkingSpots[j].AvailableSpace == 0 && parkingSpots[j].TotalSpace == settings.carSize)
                    {
                        colour = "red";
                    }
                    else if (parkingSpots[j].AvailableSpace == 0 && parkingSpots[j].AvailableSpace == settings.carSize)
                    {
                        colour = "red";
                    }
                    else 
                    { 
                        colour = "Orange"; 
                    }
                    Result += ($"P{j + 1}.[{colour}]X[/] ");
                }
                Table.AddColumn(new TableColumn(Result));
                i += 24;
                AnsiConsole.Render(Table);
            }
        }
    }
}
