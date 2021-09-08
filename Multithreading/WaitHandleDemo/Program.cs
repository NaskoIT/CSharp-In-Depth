using System;
using System.Threading;

namespace WaitHandleDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ManualResetEvent[] events = new ManualResetEvent[10];

            for (int i = 0; i < events.Length; i++)
            {
                events[i] = new ManualResetEvent(false);
                Runner runner = new Runner(events[i], i);
                new Thread(runner.Run).Start();
            }

            int index = WaitHandle.WaitAny(events);
            Console.WriteLine($"**** The winner is: {index}");

            WaitHandle.WaitAll(events);
            Console.WriteLine("All finished!");
        }
    }
}
