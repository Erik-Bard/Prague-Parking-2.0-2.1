using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Prague_Parking_2._0
{
    public class Control
    {
        public void Execute()
        {
            //only return errors and Exit program.
            try
            {
                //PriceList.Method();
                Menu.StartUpMenu();
                ExitProgram();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ExitProgram()
        {
            Console.WriteLine("Exiting program, please press any key...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
