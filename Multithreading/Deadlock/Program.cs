using System;
using System.Threading;

namespace Deadlock
{
    public class Program
    {
        private static readonly object obj1 = new object();
        private static readonly object obj2 = new object();

        public static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(DeadLock1));
            Thread t2 = new Thread(new ThreadStart(DeadLock2));
            t1.Start();
            t2.Start();
        }

        public static void DeadLock1()
        {
            lock (obj1)
            {
                Console.WriteLine("Thread 1 got locked");
                Thread.Sleep(500);
                lock (obj2)
                {
                    Console.WriteLine("Thread 2 got locked");
                }
            }
        }

        public static void DeadLock2()
        {
            lock (obj2)
            {
                Console.WriteLine("Thread 2 got locked");
                Thread.Sleep(500);
                lock (obj1)
                {
                    Console.WriteLine("Thread 1 got locked");
                }
            }
        }

        // Having too much locking in an application can get your application into trouble.In a deadlock, 
        // at least two threads wait for each other to release a lock. As both threads wait for each other, 
        // a deadlock situation occurs and threads wait endlessly and the program stops responding.

        // Here, both the methods changed the state of objects obj1 and obj2 by locking them. 
        // The method DeadLock1() first locks obj1 and next for obj2 similarly method DeadLock2() 
        // first locks obj2 and then obj1.So lock for obj1 is released, 
        // next a thread switch occurs and the second method starts and gets the lock for obj2.
        // The second thread now waits for the lock of obj1. Both of threads now wait and don't release each other. 
        // This is typically a deadlock situation.
    }
}
