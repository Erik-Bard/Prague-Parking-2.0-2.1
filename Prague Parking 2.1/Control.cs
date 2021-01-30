using System;

namespace Prague_Parking_2._1
{
    public class Control
    {
        public void Execute()
        {
            //only return errors and Exit program.
            try
            {
                PriceList.DeserializePriceList();
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
