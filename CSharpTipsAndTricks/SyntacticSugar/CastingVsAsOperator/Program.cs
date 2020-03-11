using System;

namespace CastingVsAsOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            object number = "Five";

            Console.WriteLine("The type of 'number' is: {0}", number.GetType());

            if (number is string)
            {
                Console.WriteLine("'number is string' is true");
            }

            //// This will cause an unhandled exception of type 'InvalidCastException' because the specified cast is not valid.
            // var numberAsInt = (int?)number;

            var numberAsInt = number as int?;
            //// This will cause an unhandled exception of type 'InvalidOperationException' because numberAsInt will be null
            // Console.WriteLine(numberAsInt.Value);

            //// When using the 'as' operator we should always consider the possible null value
            Console.WriteLine(numberAsInt.GetValueOrDefault());
        }
    }
}
