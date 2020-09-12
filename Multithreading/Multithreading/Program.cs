using System;
using System.Threading;

namespace Multithreading
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread thread = new Thread(PrintMessage);
            thread.Start();
            Console.WriteLine("Message from the maim thread");
        }

        public static void PrintMessage()
        {
            Console.WriteLine("This method will be executed in different thread");
        }
    }
}
