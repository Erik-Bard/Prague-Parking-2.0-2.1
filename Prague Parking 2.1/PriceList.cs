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
                            string newLinePriceCar;
                            Console.WriteLine("What will be the new price/hour for CAR?");
                            int newPriceCar;
                            bool ValidNumber = int.TryParse(Console.ReadLine(), out newPriceCar);
                            if(ValidNumber)
                            {
                                newLinePriceCar = $"Car: {newPriceCar} CZK/Hour";
                                PriceListLog[2] = newLinePriceCar;
                                SerializePriceList();
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input.");
                            }
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
                                string newLinePriceMc;
                                Console.WriteLine("What will be the new price/hour for MC?");
                                int newPriceMc;
                                bool ValidNumber = int.TryParse(Console.ReadLine(), out newPriceMc);
                                if (ValidNumber)
                                {
                                    newLinePriceMc = $"MC: {newPriceMc} CZK/Hour";
                                    PriceListLog[3] = newLinePriceMc;
                                    SerializePriceList();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input.");
                                }
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
                                string newLinePriceBike;
                                Console.WriteLine("What will be the new price/hour for BIKE?");
                                int newPriceBike;
                                bool ValidNumber = int.TryParse(Console.ReadLine(), out newPriceBike);
                                if (ValidNumber)
                                {
                                    newLinePriceBike = $"Bike: {newPriceBike} CZK/Hour";
                                    PriceListLog[4] = newLinePriceBike;
                                    SerializePriceList();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                }
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
                                string newLinePriceBus;
                                Console.WriteLine("What will be the new price/hour for CAR?");
                                int newPriceBus;
                                bool ValidNumber = int.TryParse(Console.ReadLine(), out newPriceBus);
                                if (ValidNumber)
                                {
                                    newLinePriceBus = $"Bus: {newPriceBus} CZK/Hour";
                                    PriceListLog[5] = newLinePriceBus;
                                    SerializePriceList();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                }
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
