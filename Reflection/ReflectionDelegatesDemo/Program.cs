using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ReflectionDelegatesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeController homeController = Activator.CreateInstance<HomeController>();
            Type homeConrollerType = homeController.GetType();

            PropertyInfo dataProperty = homeConrollerType.GetProperties().FirstOrDefault(p => p.IsDefined(typeof(DataAttribute), true));
            MethodInfo getter = dataProperty.GetMethod;

            Stopwatch stopwatch = Stopwatch.StartNew();
            IDictionary<string, object> data = null;

            //It is a bit slow
            for (int i = 0; i < 1000000; i++)
            {
                data = (IDictionary<string, object>)getter.Invoke(homeController, Array.Empty<object>());
            }

            Console.WriteLine("Using normal reflection: " + stopwatch.Elapsed);

            Func<object, IDictionary<string, object>> dataDelegate = PropertyHelper.MakeFastPropertyGetter<IDictionary<string, object>>(dataProperty);

            IDictionary<string, object> dataDictionary = null;
            stopwatch.Restart();
            //It is up to ten times faster using delagate, so run the demo and see the difference
            for (int i = 0; i < 1000000; i++)
            {
                dataDictionary = dataDelegate(homeController);
            }

            Console.WriteLine("Using reflection with delegates and do not know types of delegate's arguments compile time: " + stopwatch.Elapsed);

            stopwatch.Restart();
            var getMethodDelegate = (Func<HomeController, IDictionary<string, object>>)getter.CreateDelegate(typeof(Func<HomeController, IDictionary<string, object>>));

            //It is even faster but in this way we have to know the types of delegate's arguments compile time
            for (int i = 0; i < 1000000; i++)
            {
                dataDictionary = getMethodDelegate(homeController);
            }

            Console.WriteLine("Using reflection with delegates and know the types of delegate's arguments compile time: " + stopwatch.Elapsed);
            Console.WriteLine(string.Join(Environment.NewLine, dataDictionary.Select(x => $"{x.Key} -> {x.Value}")));
        }
    }
}
