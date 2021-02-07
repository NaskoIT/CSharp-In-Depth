using System;

namespace GarbageCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            // Large object heap
            var largeResource = new byte[85000];
            Console.WriteLine(GC.GetGeneration(largeResource));

            // Example why we should not call GC.Collect
            User user = new User();
            Console.WriteLine(GC.GetGeneration(user));
            GC.Collect();
            Console.WriteLine(GC.GetGeneration(user));
            GC.Collect();
            Console.WriteLine(GC.GetGeneration(user));
        }
    }

    public class User
    {
        public string Username { get; set; }
    }
}
