using System;

namespace Prague_Parking_2._1
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
