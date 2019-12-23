using System;
using System.Collections.Generic;
using System.Reflection;

namespace DynamicReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.Load(new AssemblyName("CoolLibrary"));
            Type type = assembly.GetType("CoolLibrary.CoolClass");
            MethodInfo method = type.GetMethod("CoolMethod", BindingFlags.NonPublic | BindingFlags.Static);
            string result = (string)method.Invoke(null, new object[] { 18, "Atanas Vasilev" });
            Console.WriteLine(result);

            Console.WriteLine();
            Console.WriteLine("Invocation using dynamic");

            dynamic coolClass = new ExposedObject(type);

            //Call methods
            string dynamicResult = coolClass.CoolMethod(18, "Atanas Vasilev");
            int integerSum = coolClass.Sum(12, 15);
            double collectionSum = coolClass.SumCollection(new List<double>() { 12.5, 12.3 });
            coolClass.PrintHelloWord();

            //Set and get properties
            coolClass.Name = "Dynamic reflection";
            coolClass.Value = 10.5;
            string name = coolClass.Name;
            double value = coolClass.Value;

            Console.WriteLine(name);
            Console.WriteLine(value);
            Console.WriteLine(dynamicResult);
            Console.WriteLine("Integer sum " +  integerSum);
            Console.WriteLine("Collection sum " +  collectionSum);
        }
    }
}
