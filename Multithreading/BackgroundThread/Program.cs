using System;
using System.Threading;

namespace BackgroundThread
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Change IsBackground to false and observe the difference
            Thread thread = new Thread(PrintMessage)
            {
                Name = "Thread1",
                IsBackground = true,
                // IsBackground = false,
            };

            thread.Start();
            Console.WriteLine("Main thread Running");
        }

        public static void PrintMessage()
        {
            Console.WriteLine("Thread {0} started", Thread.CurrentThread.Name);
            Thread.Sleep(2000);
            Console.WriteLine("Thread {0} completed", Thread.CurrentThread.Name);
        }
    }
}
