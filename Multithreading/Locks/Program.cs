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
        private readonly object lockObject = new object();

        public void Generate()
        {
            lock (lockObject)
            {
                Console.Write(" {0} is Executing", Thread.CurrentThread.Name);
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(new Random().Next(5));
                    Console.Write(" {0},", i);
                }

                Console.WriteLine();
            }
        }
    }
}
