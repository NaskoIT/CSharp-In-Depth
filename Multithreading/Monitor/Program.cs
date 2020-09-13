using System;
using System.Threading;

namespace RaceCondition
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NumberGenerator numberGenerator = new NumberGenerator();
            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(numberGenerator.Generate))
                {
                    Name = string.Format("Working Thread: {0}", i)
                };
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }
        }
    }

    public class NumberGenerator
    {
        private readonly object obj = new object();

        public void Generate()
        {
            Monitor.Enter(obj);

            try
            {
                Console.Write(" {0} is Executing", Thread.CurrentThread.Name);
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(new Random().Next(5));
                    Console.Write(" {0},", i);
                }

                Console.WriteLine();
            }
            catch { }
            finally
            {
                Monitor.Exit(obj);
            }
        }
    }

    // The lock statement is resolved by the compiler to the use of the Monitor class. 
    // The Monitor class is almost similar to a lock but its advantage is better control than the lock statement. 
    // You are able to instruct the lock's enter and exit explicitly, as shown in the code below. 

    // In fact, if you observe the IL code of the any application that uses the lock statement, you will find the Monitor class reference there as in the following;
}
