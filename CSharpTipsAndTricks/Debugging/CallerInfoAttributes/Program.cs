using System;
using System.Runtime.CompilerServices;

namespace CallerInfoAttributes
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine(GetCallerMemberName());
            Console.WriteLine(GetCallerFilePath());
            Console.WriteLine(GetCallerLineNumber());
        }

        public static string GetCallerMemberName([CallerMemberName]string memberName = null)
        {
            return $"My caller member name is: {memberName}";
        }

        public static string GetCallerFilePath([CallerFilePath]string filePath = null)
        {
            return $"My caller file path is: {filePath}";
        }

        public static string GetCallerLineNumber([CallerLineNumber]int lineNumber = 0)
        {
            return $"My caller line number is: {lineNumber}";
        }
    }
}
