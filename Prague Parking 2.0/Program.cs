using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Linq;

namespace Prague_Parking_2._0
{
    class Program
    {
        // #### ALL HAIL KING DENIS III !  #### //
        // Fått en del OOP information av Daniel
        static void Main(string[] args)
        {
            var control = new Control();
            control.Execute();
            PriceList.DeserializePriceList();

            Console.WriteLine("Press any key to continiue...");
            Console.ReadKey();
        }
    }

}
