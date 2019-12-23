using System;
using System.Collections.Generic;
using System.Linq;

namespace CoolLibrary
{
    internal static class CoolClass
    {
        private static string Name { get; set; }

        private static double Value { get; set; }

        internal static string CoolMethod(int age, string name)
        {
            Console.WriteLine("Cool method was invoked");
            Console.WriteLine(age);
            Console.WriteLine(name);

            return name + " -> " + age;
        }

        internal static int Sum(int a, int b) => a + b;

        internal static double SumCollection(IEnumerable<double> collection) => collection.Sum();

        internal static void PrintHelloWord()
        {
            Console.WriteLine("Hello World");
        }
    }
}
