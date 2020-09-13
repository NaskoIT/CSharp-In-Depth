using System;
using System.Threading;

namespace MutexDemo
{
    public class Program
    {
        private static readonly Mutex mutex = new Mutex();

        public static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)
            {
                Thread t = new Thread(new ThreadStart(MutexDemoFunction))
                {
                    Name = string.Format("Thread {0} :", i + 1)
                };

                t.Start();
            }
        }

        private static void MutexDemoFunction()
        {
            try
            {
                mutex.WaitOne();   // Wait until it is safe to enter.  
                Console.WriteLine("{0} has entered in the Domain", Thread.CurrentThread.Name);
                Thread.Sleep(1000);    // Wait until it is safe to enter.  
                Console.WriteLine("{0} is leaving the Domain\r\n", Thread.CurrentThread.Name);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
