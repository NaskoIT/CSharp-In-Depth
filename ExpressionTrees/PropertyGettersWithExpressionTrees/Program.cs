using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PropertyGettersWithExpressionTrees
{
    class Program
    {
        private const int OperationsCount = 100000;

        static void Main(string[] args)
        {
            //return RedirectToAction("Index", new { id = 10, query = "Fast property getters using expression trees" }
            // Dictionary { ["id"] = 10, ["query"] = "Fast property getters using expression trees" }

            var queryParameters = new { id = 10, query = "Fast property getters using expression trees" };
            var queryParametersDictionary = new Dictionary<string, object>();
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                queryParameters
                    .GetType()
                    .GetProperties()
                    .Select(property => new
                    {
                        property.Name,
                        Value = property.GetValue(queryParameters)
                    })
                    .ToList()
                    .ForEach(property => queryParametersDictionary[property.Name] = property.Value);
            }

            Console.WriteLine(queryParametersDictionary.Count);
            Console.WriteLine($"{stopwatch.Elapsed} - Reflection property getters");

            //If it is precached it is even faster beacuse the only slow method is compilation of the lambda function
            PropertyHelper.GetProperties(queryParameters);
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                PropertyHelper
                   .GetProperties(queryParameters)
                   .ForEach(property => queryParametersDictionary[property.Name] = property.Value);
            }

            Console.WriteLine(queryParametersDictionary.Count);
            Console.WriteLine($"{stopwatch.Elapsed} - Expression trees property getters");
        }
    }
}
