using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using Spectre.Console;

namespace Prague_Parking_2._1
{
    public class PriceList
    {
        public static List<string> PriceListLog = new List<string>();
        public static void priceList()
        {
            DeserializePriceList();
            foreach (var item in PriceListLog)
            {
                Console.WriteLine(item);
            }
            var selectInMenu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[Bold Aqua]Do you wish to change the prices[/]?")
                .PageSize(10)
                .AddChoices(new[] {
                "Yes", "No"}));
            switch (selectInMenu) 
            {
                case "Yes":
                    var subMenuSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Which [Bold Green]Vehicle[/] would you like to [Bold Yellow]change[/]?")
                    .PageSize(10)
                    .AddChoices(new[] {
                    "CAR", "MC","BIKE", "BUS"}));
                    switch(subMenuSelection)
                    {
                        case "CAR":
                        try
                        {
                            Console.WriteLine("What will be the new price/hour for CAR?");
                            int newPriceCar = int.Parse(Console.ReadLine());
                            string newLinePrice = $"Car: {newPriceCar} CZK/Hour";
                            PriceListLog[2] = newLinePrice;
                            SerializePriceList();
                        }
                        catch (Exception em)
                        {
                            Console.WriteLine("Error in editing file.");
                            Console.WriteLine(em.Message);
                        }
                        break;
                        case "MC":
                            try
                            {
                                Console.WriteLine("What will be the new price/hour for MC?");
                                int newPriceMc = int.Parse(Console.ReadLine());
                                string newLinePrice = $"MC: {newPriceMc} CZK/Hour";
                                PriceListLog[3] = newLinePrice;
                                SerializePriceList();
                            }
                            catch (Exception em)
                            {
                                Console.WriteLine("Error in editing file.");
                                Console.WriteLine(em.Message);
                            }
                        break;
                        case "BIKE":
                            try
                            {
                                Console.WriteLine("What will be the new price/hour for BIKE?");
                                int newPriceBike = int.Parse(Console.ReadLine());
                                string newLinePrice = $"Bike: {newPriceBike} CZK/Hour";
                                PriceListLog[4] = newLinePrice;
                                SerializePriceList();
                            }
                            catch (Exception em)
                            {
                                Console.WriteLine("Error in editing file.");
                                Console.WriteLine(em.Message);
                            }
                            break;
                        case "BUS":
                            try
                            {
                                Console.WriteLine("What will be the new price/hour for CAR?");
                                int newPriceBus = int.Parse(Console.ReadLine());
                                string newLinePrice = $"Bus: {newPriceBus} CZK/Hour";
                                PriceListLog[5] = newLinePrice;
                                SerializePriceList();
                            }
                            catch (Exception em)
                            {
                                Console.WriteLine("Error in editing file.");
                                Console.WriteLine(em.Message);
                            }
                            break;
                    }  
                break;
                case "No":
                    break;
            }

        
        }
        public static void DeserializePriceList()
        {
            string json = File.ReadAllText("../../../DATA/PriceList.txt");
            PriceListLog = JsonConvert.DeserializeObject<List<string>>(json);
        }
        public static void SerializePriceList()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            var JsonTextPrices = JsonConvert.SerializeObject(PriceListLog, Formatting.Indented);
            using (StreamWriter Writer = new StreamWriter("../../../DATA/PriceList.txt"))
            {
                Writer.Write(JsonTextPrices);
            }
        }
    }
}
