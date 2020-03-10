using System;

namespace StringReferences
{
    class Program
    {
        static void Main(string[] args)
        {
            var string1 = "some value";
            var string2 = "some value";
            Console.WriteLine("ReferenceEquals(string1, string2) = {0}", ReferenceEquals(string1, string2)); // true
            Console.Write("Please enter \"some value\": ");
            var stringFromConsole = Console.ReadLine();
            Console.WriteLine("ReferenceEquals(string1, stringFromConsole) = {0}", ReferenceEquals(string1, stringFromConsole)); // false
            var stringInternFromConsole = string.Intern(stringFromConsole);
            Console.WriteLine("ReferenceEquals(string1, stringInternFromConsole) = {0}", ReferenceEquals(string1, stringInternFromConsole)); //true
        }
    }
}
