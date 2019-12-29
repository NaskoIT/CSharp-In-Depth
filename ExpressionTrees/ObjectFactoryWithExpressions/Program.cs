using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ObjectFactoryWithExpressions
{
    class Program
    {
        private const int InsrancesCount = 1000000;

        static void Main(string[] args)
        {
            List<Cat> cats = new List<Cat>();
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < InsrancesCount; i++)
            {
                Cat cat = Activator.CreateInstance<Cat>();
                cats.Add(cat);
            }

            Console.WriteLine(stopwatch.Elapsed + " - Using Activator - constructor with no parameters");
            
            //The first compilaton of the expression tree is slow, so it is a good idea to invoke the method on application start
            ObjectFactory.CreateInstance<Cat>();
            cats = new List<Cat>();
            stopwatch.Restart();

            for (int i = 0; i < InsrancesCount; i++)
            {
                Cat cat = ObjectFactory.CreateInstance<Cat>();
                cats.Add(cat);
            }

            Console.WriteLine(stopwatch.Elapsed + " - Using Expression Trees - constructor with no parameters");

            cats = new List<Cat>();
            Type catType = typeof(Cat);
            stopwatch.Restart();

            for (int i = 0; i < InsrancesCount; i++)
            {
                Cat cat = (Cat)Activator.CreateInstance(catType, "My cool cat", 20);
                cats.Add(cat);
            }

            Console.WriteLine(stopwatch.Elapsed + " - Using Activator - constructor with 2 parameters");

            cats = new List<Cat>();
            ObjectFactory.CreateInstance(catType, "My cool cat", 20);
            stopwatch.Restart();

            for (int i = 0; i < InsrancesCount; i++)
            {
                Cat cat = (Cat)ObjectFactory.CreateInstance(catType, "My cool cat", 20);
                cats.Add(cat);
            }

            Console.WriteLine(stopwatch.Elapsed + " - Using Expression Trees - constructor with 2 parameters");
        }
    }
}
