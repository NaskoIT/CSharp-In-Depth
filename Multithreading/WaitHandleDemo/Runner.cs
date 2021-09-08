using System;
using System.Threading;

namespace WaitHandleDemo
{
    public class Runner
    {
        private static readonly object obj = new object();
        private static readonly Random random = new Random();
        private readonly ManualResetEvent resetEvent;
        private readonly int id;

        public Runner(ManualResetEvent resetEvent, int id)
        {
            this.resetEvent = resetEvent;
            this.id = id;
        }

        public void Run()
        {
            for (int i = 0; i < 10; i++)
            {
                int sleepTime;
                lock (obj)
                {
                    sleepTime = random.Next(2000);
                }

                Thread.Sleep(sleepTime);
                Console.WriteLine($"Runner {id} at stage {i}");
            }

            resetEvent.Set();
        }
    }
}
