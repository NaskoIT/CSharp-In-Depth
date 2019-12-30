using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FasterExpressionCompilation
{
    class Program
    {
        private const int OperationsCount = 1000;

        static void Main(string[] args)
        {
            //return RedirectToAction("Index", new { id = 10, query = "Fast property getters using expression trees" }
            // Dictionary { ["id"] = 10, ["query"] = "Fast property getters using expression trees" }

            var queryParameters = new { id = 10, query = "Fast property getters using expression trees" };
            var queryParametersDictionary = new Dictionary<string, object>();
            var stopwatch = Stopwatch.StartNew();

            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                PropertyHelper
                   .GetProperties(queryParameters)
                   .ForEach(property => queryParametersDictionary[property.Name] = property.Value);
            }

            Console.WriteLine(queryParametersDictionary.Count);
            Console.WriteLine($"{stopwatch.Elapsed} - Normal Expression trees property getters");

            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                FastPropertyHelper
                   .GetProperties(queryParameters)
                   .ForEach(property => queryParametersDictionary[property.Name] = property.Value);
            }

            Console.WriteLine(queryParametersDictionary.Count);
            Console.WriteLine($"{stopwatch.Elapsed} - Fast Expression trees property getters");

        }
    }
}
