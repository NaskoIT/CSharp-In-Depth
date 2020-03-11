using System;

namespace Yield
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var evenNumber in YieldNumbersGenerator.EvenNumbers(50, 60))
            {
                Console.WriteLine("Iterated number {0}", evenNumber);
            }
        }
    }
}
