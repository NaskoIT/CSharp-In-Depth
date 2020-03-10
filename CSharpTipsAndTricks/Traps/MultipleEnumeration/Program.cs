using System;
using System.Linq;

namespace MultipleEnumeration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==== Multiple enumerations:");
            LinqQueryMultipleEnumeration();

            Console.WriteLine("==== Single enumeration (calling .ToList()):");
            LinqQuerySingleEnumeration();
        }

        public static void LinqQueryMultipleEnumeration()
        {
            var results = Enumerable.Range(1, 3).Select(x => new { Number = x, Time = DateTime.Now.ToString("O") });

            Console.WriteLine("First enumeration:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Second enumeration:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        public static void LinqQuerySingleEnumeration()
        {
            var results = Enumerable.Range(1, 3).Select(x => new { Number = x, Time = DateTime.Now.ToString("O") }).ToList();

            Console.WriteLine("First enumeration:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Second enumeration:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
