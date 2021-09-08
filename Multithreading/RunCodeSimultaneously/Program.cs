using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RunCodeSimultaneously
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            PrintNumbersInRange(0, 100);

            var tasks = new List<Task>();
            for (int i = 1; i <= n; i++)
            {
                var task = Task.Run(() => PrintNumbersInRange(100 * i, (100 * i) + 100));
                tasks.Add(task);
            }

            Console.WriteLine("Done!");
            Task.WaitAll(tasks.ToArray());
        }

        public static void PrintNumbersInRange(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }
        }
    }
}
