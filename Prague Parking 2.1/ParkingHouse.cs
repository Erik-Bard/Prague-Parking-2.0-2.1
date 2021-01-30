using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using Spectre.Console;
using System.Linq;

namespace Prague_Parking_2._1
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
        public void ParkVehicle()
        {
            Console.WriteLine("Park a Car, MC, Bike or Bus");
            string VehicleChoise = Console.ReadLine();
            VehicleChoise.ToLower();
            Console.WriteLine("Enter registration plate: ");
            string input = Console.ReadLine();
            input.ToUpper();
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

                case "bike":
                    Bike bike = new Bike();
                    bike.RegPlate = input;
                    bike.ArriveTime = DateTime.Now;

                    ParkingSpot spot1;
                    spot1 = FindAvailableSpot(bike);
                    if (spot1 != null)
                    {
                        spot1.AddVehicle(bike);
                        WriteToFile();
                        Console.WriteLine("Your bike is now parked.");
                        Console.ReadKey();
                    }
                    else
                        Console.WriteLine("No empty spots available.");
                    Console.ReadKey();
                    break;

                case "bus":
                    Bus bus = new Bus();
                    bus.RegPlate = input;
                    bus.ArriveTime = DateTime.Now;

                    int placeToPark;
                    placeToPark = SpotAsInt();
                    if(placeToPark == 0 || placeToPark == -1)
                    {
                        Console.WriteLine("No empty spots available.");
                    }
                    else
                    {
                        for (int i = placeToPark; i < placeToPark + 4; i++)
                        {
                            parkingSpots[i].vehicles.Add(bus);
                            parkingSpots[i].AvailableSpace = 0;
                        }
                        WriteToFile();
                    }
                    Console.ReadKey();
                    break;
            }
        }
        public int SpotAsInt()
        {
            int BusSpot;
            int SpotsInRow;
            int parkingLots = parkingSpots.Count > 50 ? 50 : parkingSpots.Count;
            for (int i = 0; i < parkingLots - 4; i++)
            {
                SpotsInRow = 0;
                for (int s = i; s < i + 4; s++)
                {
                    if (parkingSpots[s].AvailableSpace == 4)
                    {
                        SpotsInRow += 1;
                    }
                    if (parkingSpots[s].AvailableSpace == 0)
                    {
                        SpotsInRow = 0;
                    }
                }
                if (SpotsInRow == 4)
                {
                    BusSpot = i;
                    Console.WriteLine($"Parking here: {BusSpot+1}");
                    return BusSpot;
                }
            }
            return -1;
        }
        public void MoveVehicle()
        {
            Vehicle vehicle = new Vehicle();
            vehicle = RemoveAdd();
            ParkReturnedVehicle(vehicle);
            Console.ReadKey();
        }
        // Prague Parking 2.1 version of Search vehicle uses Lambda Expressions
        public int? SearchForVehicle(string regNumber)
        {
            var FoundValue = parkingSpots.Find(x => x.vehicles.Find(y => y.RegPlate == regNumber) != null);
            int index = parkingSpots.IndexOf(FoundValue);
            Console.WriteLine($"Your vehicle is parked at {index+1}");
            Console.ReadKey();
            return index;
        }
        // DisplayEmptySpots use Linq to show the amount of empty spaces
        public void DisplayEmptySpots()
        {
            var spots = from stuff in parkingSpots where stuff.AvailableSpace == settings.ParkingSpotSize select new { stuff };
            int counter = 0;
            foreach (var spot in spots)
            {
                counter++;
            }
            Console.WriteLine($"Today we have {counter} empty lots!");
        }
        public void RemoveVehicle()
        {
            Console.WriteLine("Which vehicle do you wish us to retrieve and exit with?");
            string VehicleExit = Console.ReadLine();
            VehicleExit.ToUpper();
            int index;
            foreach (var item in parkingSpots)
            {
                foreach (Vehicle v in item.vehicles)
                {
                    if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.carSize)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
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
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.mcSize)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        item.vehicles.Remove(v);
                        Console.WriteLine($"To Pay: {TotalPrice} CZK at checkout time: {DateTime.Now}");
                        break;
                    }
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.mcSize && item != null)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
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
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.bikeSize)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        item.vehicles.Remove(v);
                        Console.WriteLine($"To Pay: {TotalPrice} CZK at checkout time: {DateTime.Now}");
                        break;
                    }
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.busSize)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace = settings.ParkingSpotSize;
                        item.vehicles.Remove(v);
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
            int index;
            foreach (var item in parkingSpots)
            {
                foreach (Vehicle v in item.vehicles)
                {
                    if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.carSize)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
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
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.mcSize)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
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
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.bikeSize)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        Bike bikeCopy = new Bike();
                        bikeCopy.RegPlate = v.RegPlate;
                        bikeCopy.Size = v.Size;
                        bikeCopy.ArriveTime = v.ArriveTime;
                        Bike VehicleReturn = bikeCopy;
                        item.vehicles.Remove(v);
                        return VehicleReturn;
                    }
                    else if (v.RegPlate.Equals(VehicleExit) && v.Size == settings.busSize)
                    {
                        index = parkingSpots.IndexOf(item);
                        Console.WriteLine($"Found at: {index}");
                        string checkin = v.ArriveTime.ToString();
                        string TotalTime = Vehicle.ExitTimeCalculator(checkin).ToString();
                        string TotalPrice = Vehicle.CalculateParkedTime(TotalTime, v.Size);
                        item.AvailableSpace += v.Size;
                        Bus busCopy = new Bus();
                        busCopy.RegPlate = v.RegPlate;
                        busCopy.Size = v.Size;
                        busCopy.ArriveTime = v.ArriveTime;
                        Bus VehicleReturn = busCopy;
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
            Bike newBike = new Bike();
            Bus newBus = new Bus();
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
            else if(VehicleReturn.Size == newMC.Size)
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
            else if (VehicleReturn.Size == newBike.Size)
            {
                newBike.RegPlate = VehicleReturn.RegPlate;
                newBike.Size = VehicleReturn.Size;
                newBike.ArriveTime = VehicleReturn.ArriveTime;
                ParkingSpot Spot;
                Spot = FindAvailableSpot(newBike);
                Spot.AddVehicle(newBike);
                WriteToFile();
                Console.WriteLine("Done");
            }
            else if (VehicleReturn.Size == newBus.Size)
            {
                newBus.RegPlate = VehicleReturn.RegPlate;
                newBus.Size = VehicleReturn.Size;
                newBus.ArriveTime = VehicleReturn.ArriveTime;
                ParkingSpot Spot;
                Spot = FindAvailableSpot(newBus);
                Spot.AddVehicle(newBus);
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
                            fullInfo = fullInfo + parkingSpots[j].vehicles[k].Size
                                + "," + parkingSpots[j].vehicles[k].RegPlate + " ";
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
