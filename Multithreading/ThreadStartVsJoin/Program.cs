using System;
using System.Threading;

namespace ThreadStartVsJoin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start printing the numbers");

            var thread = new Thread(PrintNumbers);

            thread.Start();

            Console.WriteLine("Waiting for thread to finish work");

            thread.Join();

            Console.WriteLine("Work in the new thread is finshed!");
        }

        public static void PrintNumbers()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }
        }
    }
}
