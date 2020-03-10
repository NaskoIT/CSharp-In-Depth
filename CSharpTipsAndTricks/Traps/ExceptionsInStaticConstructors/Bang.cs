using System;

namespace ExceptionsInStaticConstructors
{
    public sealed class Bang
    {
        static Bang()
        {
            Console.WriteLine("In static constructor");
            throw new Exception("Bang!");
        }

        public Bang()
        {
            Console.WriteLine("In instance Bang constructor.");
        }

        public static void StaticMethod()
        {
            Console.WriteLine("In StaticMethod()");
        }
    }
}
