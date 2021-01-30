using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prague_Parking_2._1
{
    public class Vehicle
    {
        public string RegPlate { get; set; }
        public int Size { get; set;  }
        
        public DateTime ArriveTime { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return RegPlate + " Parked at:" + ArriveTime;
        }
        public void Print()
        {
            if (RegPlate.Length >= 8)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(RegPlate.Substring(0, 8));
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("{0:8}", RegPlate);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static string ExitTimeCalculator(string CheckIn)
        {
            DateTime Checkin = DateTime.Parse(CheckIn);
            DateTime Checkout = DateTime.Now;
            TimeSpan Total = Checkout - Checkin;
            double days = ((Checkout.Day - Checkin.Day));
            if (days > 1)
            {
                days *= 24;
            }
            else
            {
                days = 0;
            }
            double minutes = Total.Minutes;
            double hours = Total.Hours;
            hours = hours + days;
            double TotalMinutes = (hours * 60) + minutes;
            if (TotalMinutes > 10)
            {
                TotalMinutes = ((TotalMinutes - 10) / 60);
            }
            else if (TotalMinutes < 10)
            {
                TotalMinutes = 0;
            }
            return TotalMinutes.ToString();
        }
        public static string CalculateParkedTime(string TotalPrice, int vehicleSize)
        {
            GlobalSettings settings = new GlobalSettings();
            double Total = double.Parse(TotalPrice);
            if (vehicleSize == settings.carSize)
            {
                var valueAsString = PriceList.PriceListLog[2].Substring(5, 3);
                int valueAsInt = int.Parse(valueAsString);
                TotalPrice = ((Total / 60) * valueAsInt).ToString();
            }
            if (vehicleSize == settings.mcSize)
            {
                var valueAsString = PriceList.PriceListLog[3].Substring(4, 3);
                int valueAsInt = int.Parse(valueAsString);
                TotalPrice = ((Total / 60) * valueAsInt).ToString();
            }
            if (vehicleSize == settings.bikeSize)
            {
                var valueAsString = PriceList.PriceListLog[4].Substring(5, 3);
                int valueAsInt = int.Parse(valueAsString);
                TotalPrice = ((Total / 60) * valueAsInt).ToString();
            }
            if (vehicleSize == settings.busSize)
            {
                var valueAsString = PriceList.PriceListLog[5].Substring(5, 3);
                int valueAsInt = int.Parse(valueAsString);
                TotalPrice = ((Total / 60) * valueAsInt).ToString();
            }
            return TotalPrice;
        }
    }
}
