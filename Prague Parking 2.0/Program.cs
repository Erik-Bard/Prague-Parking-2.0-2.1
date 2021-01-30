﻿using System;
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
        // Extra Tack till Denis, Daniel och Marcus // 
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
