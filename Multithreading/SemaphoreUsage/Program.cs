using System;
using System.Threading;

namespace SemaphoreUsage
{
    public class Program
    {
        private static readonly Semaphore semaphore = new Semaphore(2, 4);

        public static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                new Thread(Start).Start(i);
            }
        }

        private static void Start(object id)
        {
            Console.WriteLine(id + "-->>Wants to Get Enter");
            try
            {
                semaphore.WaitOne();
                Console.WriteLine(" Success: " + id + " is in!");
                Thread.Sleep(2000);
                Console.WriteLine(id + "<<-- is Evacuating");
            }
            finally
            {
                semaphore.Release();
            }
        }

        // A semaphore is very similar to a Mutex but a semaphore can be used by multiple threads at once while a Mutex can't. 
        // With a Semaphore, you can define a count of how many threads are allowed to access the resources shielded by a semaphore simultaneously.
    }
}
