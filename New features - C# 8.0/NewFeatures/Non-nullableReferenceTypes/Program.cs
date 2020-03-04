 using System;

namespace Non_nullableReferenceTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Compilation error
            //string test = null;

            string? test = null;

            //Compilation error beacuse we pass nullable reference type
            //Test(test);
            //Test(null)

            test = "test";
            Test(test);

            string? returnedValue = ReturnValue(true);
            string value = returnedValue!;
            Console.WriteLine(value);  
            

        }

        public static void Test(string test)
        {
        }

        public static int? GetNullableLength(string? input)
        {
            //Compilation error beacause trying to access property of nullable string
            //return input.Length;

            return input?.Length;
        }

        public static string? ReturnValue(bool returnNull)
        {
            return returnNull ? null : "Not null";
        }
    }
}
