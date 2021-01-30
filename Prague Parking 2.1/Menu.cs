using System;
using Spectre.Console;

namespace Prague_Parking_2._1
{
    public class Menu
    {
        public static void StartUpMenu()
        {
            ParkingHouse parkingHouse = new ParkingHouse("../../../DATA/JsonDB.json");
            ParkingSpot.InstallSettings();
            while (true)
            {
                Table table = new Table().Centered();
                Console.Clear();
                AnsiConsole.Render(new FigletText("PRAGUE PARKING!").LeftAligned().Color(Color.Aqua));
                parkingHouse.DisplayEmptySpots();
                table.AddColumn(new TableColumn(new Markup("[Bold blue]Menu[/]")));
                var selectInMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select in [green]Menu[/]?")
                    .PageSize(10)
                    .AddChoices(new[] {
                    "Park Vehicle", "Move Vehicle",
                    "Find Vehicle", "Retrieve & Exit withVehicle", "See Overview of Garage",
                    "View entire Garage", "View our List of Prices", "Exit Program",
                    }));
                switch (selectInMenu)
                {
                    case "Park Vehicle":
                        parkingHouse.ParkVehicle();
                        break;
                    case "Move Vehicle":
                        parkingHouse.MoveVehicle();
                        break;
                    case "Find Vehicle":
                        Console.WriteLine("Search for registration plate...");
                        string sF = Console.ReadLine();
                        parkingHouse.SearchForVehicle(sF);
                        break;
                    case "Retrieve & Exit withVehicle":
                        parkingHouse.RemoveVehicle();
                        break;
                    case "See Overview of Garage":
                        parkingHouse.Print();
                        Console.ReadKey();
                        break;
                    case "View entire Garage":
                        parkingHouse.PrintWithRegNum();
                        Console.ReadKey();
                        break;
                    case "View our List of Prices":
                        PriceList.priceList();
                        break;
                    case "Exit Program":
                        return;
                    default:
                        break;
                }
            }
        }
    }
}
