using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace GetPropertyPerformanceTest
{
    class Program
    {
        private const int ObjectsCount = 100000;

        private static readonly Dictionary<Type, object> typeToValueDictionary = new Dictionary<Type, object>
    {
        { typeof(string), "***********" },
        { typeof(DateTime), DateTime.MinValue },
        { typeof(DateTime?), DateTime.MinValue },
        { typeof(int), 0 },
        { typeof(int?), 0 }
    };

        public static void Main(String[] args)
        {
            // Interesting: if the ObjectsCount = 1000000, the slow method is faster
            // But if the ObjectsCount = 100000, fast method is very very faster

            Console.WriteLine("Slow method performance test!");
            ApplyCensorshipSlow();

            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Fast method performance test!");
            ApplyCensorshipFast();
        }

        private static void ApplyCensorshipFast()
        {
            var users = GenerateUsers().ToList();

            var properties = users.First().GetType().GetProperties().ToList();

            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var item in users)
            {
                CensorPropertiesFast(item, properties);
            }

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        private static void ApplyCensorshipSlow()
        {
            var users = GenerateUsers().ToList();

            var propertyNames = users.First().GetType().GetProperties().Select(p => p.Name).ToList();

            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var item in users)
            {
                CensorPropertiesSlow(item, propertyNames);
            }

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        private static IEnumerable<User> GenerateUsers()
        {
            for (int i = 0; i < ObjectsCount; i++)
            {
                yield return new User
                {
                    Username = "Test " + i,
                    Email = "Email " + i.ToString(),
                    Age = i,
                };
            }
        }

        private static void CensorPropertiesFast(object censorObject, List<PropertyInfo> properties)
        {
            properties.ForEach(property => property.SetValue(censorObject, typeToValueDictionary[property.PropertyType]));
        }

        private static void CensorPropertiesSlow(object censorObject, List<string> properties)
        {
            properties.ForEach(propertyName => censorObject.GetType().GetProperty(propertyName)
                   .SetValue(
                       censorObject,
                       typeToValueDictionary[censorObject.GetType().GetProperty(propertyName).PropertyType]));
        }

        public class User
        {
            public string Username { get; set; }

            public string Email { get; set; }

            public int Age { get; set; }
        }
    }
}
