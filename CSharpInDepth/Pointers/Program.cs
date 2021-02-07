using System;

namespace Pointers
{
    class Program
    {
        private static long topOfStack;
        private static readonly long stackSize = 1024 * 1024;

        unsafe static void Main(string[] args)
        {
            int someVariable = 5;
            topOfStack = (long)&someVariable;

            TestStruct test = new TestStruct();

            Console.WriteLine("Demo when we pass the parameter to the invoked function by value.");
            RecursiveFunction(test);

            Console.WriteLine(new string('-', 150));


            Console.WriteLine("Demo when we pass the parameter to the invoked function by reference.");
            RecursiveFunctionByReference(ref test);
        }

        unsafe private static void RecursiveFunction(TestStruct testStruct, int times = 0)
        {
            long usedStack;
            usedStack = topOfStack - (long)&usedStack;
            if (stackSize - usedStack < 0)
            {
                Console.WriteLine($"The function was invoked: {times} times before StackOverflow.");
                return;
            }

            RecursiveFunction(testStruct, ++times);
        }

        unsafe private static void RecursiveFunctionByReference(ref TestStruct testStruct, int times = 0)
        {
            long usedStack;
            usedStack = topOfStack - (long)&usedStack;
            if (stackSize - usedStack < 0)
            {
                Console.WriteLine($"The function was invoked: {times} times before StackOverflow.");
                return;
            }

            RecursiveFunctionByReference(ref testStruct, ++times);
        }
    }

    struct TestStruct
    {
        public double a, b, c, d, e, f, g, h;
    }
}
