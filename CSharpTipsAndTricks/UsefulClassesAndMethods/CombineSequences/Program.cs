using System;
using System.Collections.Generic;
using System.Linq;

namespace CombineSequences
{
    class Program
    {
        private static readonly IEnumerable<string> FirstList = new List<string> { "apples", "bananas", "cherries", "durian" };

        private static readonly IEnumerable<string> SecondList = new List<string> { "durian", "eggplant", "apples" };

        public static void Main()
        {
            ConcatMethod();
            Console.WriteLine(new string('=', 50));
            UnionMethod();
            Console.WriteLine(new string('=', 50));
            IntersectMethod();
            Console.WriteLine(new string('=', 50));
            ExceptMethod();
            Console.WriteLine(new string('=', 50));
            ZipMethod();
            Console.WriteLine(new string('=', 50));
            AggregateMethod();
        }

        private static void ConcatMethod()
        {
            var newList = FirstList.Concat(SecondList);
            Console.WriteLine("Concat() result:");
            Console.WriteLine(string.Join(", ", newList));
        }

        private static void UnionMethod()
        {
            var newList = FirstList.Union(SecondList);
            Console.WriteLine("Union() result:");
            Console.WriteLine(string.Join(", ", newList));

            Console.WriteLine();

            var concatenatedListWithComparer = FirstList.Union(
                SecondList,
                new StringLengthEqualityComparer());
            Console.WriteLine("Union(with length comparer) result:");
            Console.WriteLine(string.Join(", ", concatenatedListWithComparer));
        }

        private static void IntersectMethod()
        {
            var newList = FirstList.Intersect(SecondList);
            Console.WriteLine("Intersect() result:");
            Console.WriteLine(string.Join(", ", newList));
        }

        private static void ExceptMethod()
        {
            var newList = FirstList.Except(SecondList);
            Console.WriteLine("first Except() second result:");
            Console.WriteLine(string.Join(", ", newList));

            Console.WriteLine();

            newList = SecondList.Except(FirstList);
            Console.WriteLine("second Except() first result:");
            Console.WriteLine(string.Join(", ", newList));
        }

        private static void ZipMethod()
        {
            var listOfNumbers = Enumerable.Range(1, FirstList.Count());
            IEnumerable<Tuple<int, string>> newList = listOfNumbers.Zip(FirstList, Tuple.Create);
            Console.WriteLine("Zip() result:");
            Console.WriteLine(string.Join(", ", newList));

            // Template magic:
            // public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
            //      this IEnumerable<TFirst> first,
            //      IEnumerable<TSecond> second,
            //      Func<TFirst, TSecond, TResult> resultSelector);
            // public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2);
        }

        private static void AggregateMethod()
        {
            var allString = FirstList.Aggregate("_", (seed, current) => seed + current);
            Console.WriteLine("Aggregate() result:");
            Console.WriteLine(allString);

            // If we don't specify seed value => the seed value becomes the first element of the collection

            // Console.WriteLine(FirstList.Aggregate(0, (seed, current) => seed++));
        }
    }
}
