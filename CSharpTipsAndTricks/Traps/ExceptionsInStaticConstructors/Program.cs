using System;

namespace ExceptionsInStaticConstructors
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 3; i++)
            {
                try
                {
                    // Notice the call of the static constructor
                    // Also notice that the static constructor is only called once
                    Bang.StaticMethod();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(
                        "Exception of type {0} (Inner: {1}).",
                        exception.GetType().Name,
                        exception.InnerException != null ? exception.InnerException.GetType().Name : "none");
                }
            }

            try
            {
                var bang = new Bang();
            }
            catch (Exception e)
            {
                Console.WriteLine("Calling instance constructor also failed!");
                Console.WriteLine($"Exception: {e}");
            }
        }
    }
}
