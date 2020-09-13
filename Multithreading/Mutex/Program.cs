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

        // Mutex stands for Mutual Exclusion that offers synchronization across multiple threads. 
        // The Mutex calss is derived from WaitHandle, you can do a WaitOne() to acquire the mutex lock and be the owner of the mutex that time. 
        // The mutex is released by invoking the ReleaseMutex() method as in the following;
        // Once you have successfully compiled this program, it shows when each new thread first enters into its application domain. 
        // Once it has finished its tasks then it is released and the second thread starts and so on.
    }
}
