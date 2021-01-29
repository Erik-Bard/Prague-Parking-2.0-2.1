using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Spectre.Console;

namespace Prague_Parking_2._0
{
    public class Menu
    {
        //public static string[] entities;
        ////public static int menuSelection;
        //public static int index;
        //public static int index1;
        //public static Vehicle CarCopy { get; set; }
        //public static int NumberOfSpots { get; set; }
        //public static string joined;
        //public static bool CompareFull = true;
        //public static string CarInput;
        //public static string mc;
        //public static string SF;
        //public static string oofer;
        //public static bool LoopMain = true;

        public static void StartUpMenu()
        {
            ParkingHouse parkingHouse = new ParkingHouse("../../../DATA/JsonDB.json");
            while(true)
            {
                Table table = new Table().Centered();
                Console.Clear();
                AnsiConsole.Render(new FigletText("PRAGUE PARKING!").LeftAligned().Color(Color.Aqua));

                //DateTime date = DateTime.Now;
                //int year = date.Year; int month = date.Month; int day = date.Day;
                //var calendar = new Calendar(year, month);
                //calendar.AddCalendarEvent(year, month, day);
                //calendar.HighlightStyle(Style.Parse("green"));
                ////calendar.Alignment = Justify.Right;
                //AnsiConsole.Render(calendar);

                //Console.WriteLine($"Today we have {parkingHouse.parkingSpots.Count} available parkingspaces.");
                table.AddColumn(new TableColumn(new Markup("[Bold blue]Menu[/]")));
                var selectInMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select in [green]Menu[/]?")
                    .PageSize(10)
                    .AddChoices(new[] {
                    "Park Vehicle", "Move Vehicle",
                    "Find Vehicle", "Retrieve & Exit withVehicle", "Print Garage",
                    "View our List of Prices", "Exit Program",
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
                    case "Print Garage":
                        parkingHouse.Print();
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
        //public static void PopulateListAtStart()
        //{
        //    try
        //    {
        //        Control control = new Control();
        //        control.DeserializeObject();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error in reading file.");
        //        Console.WriteLine(e.Message);
        //    }
        //}
    }
}
