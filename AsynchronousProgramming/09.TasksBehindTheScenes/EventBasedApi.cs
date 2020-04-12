﻿using System;

namespace TasksBehindTheScenes
{
    public class EventBasedApi
    {
        public event Action<string> Done = s => { };

        public void Work()
        {
            Console.WriteLine("Working...");

            DateTime end = DateTime.Now.AddSeconds(5);

            while (true)
            {
                if (DateTime.Now > end)
                {
                    break;
                }
            }

            Console.WriteLine("Done...");

            this.Done("Data");
        }
    }
}
