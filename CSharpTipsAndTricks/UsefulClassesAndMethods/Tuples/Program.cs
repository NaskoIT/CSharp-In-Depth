using System;
using System.Collections.Generic;

namespace Tuples
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = GetCountAndAverage(new List<int> { 1, 2, 3, 4 });
            Console.WriteLine("Count: {0}", result.Item1);
            Console.WriteLine("Average: {0}", result.Item2);

            var anotherResult = GetCountAndAverage(new List<int> { 4, 3, 2, 1 });
            Console.WriteLine(anotherResult.Equals(result));
            Console.WriteLine(anotherResult == result); // reference comparison

            //// result.Item1 = 1; // Exception: Property or indexer 'System.Tuple<int,decimal>.Item1' cannot be assigned to -- it is read only

            // C# 7.0
            Console.WriteLine("================ C# 7.0 ================");
            var resultNew = GetCountAndAverageNewTuples(new List<int> { 1, 2, 3, 4 });
            Console.WriteLine("Count: {0}", resultNew.Count); // .Item1 still works
            Console.WriteLine("Average: {0}", resultNew.Average);

            var anotherResultNew = GetCountAndAverageNewTuples(new List<int> { 4, 3, 2, 1 });
            Console.WriteLine(resultNew.Equals(anotherResultNew));
            Console.WriteLine(resultNew == anotherResultNew);

            resultNew.Count = 0; // This is OK
        }

        public static Tuple<int, decimal> GetCountAndAverage(IEnumerable<int> numbers)
        {
            var count = 0;
            long sum = 0;
            foreach (var number in numbers)
            {
                count++;
                sum += number;
            }

            var average = (decimal)sum / count;
            return new Tuple<int, decimal>(count, average);
        }

        public static (int Count, decimal Average) GetCountAndAverageNewTuples(IEnumerable<int> numbers)
        {
            var count = 0;
            long sum = 0;
            foreach (var number in numbers)
            {
                count++;
                sum += number;
            }

            var average = (decimal)sum / count;
            return (count, average);
        }
    }
}
