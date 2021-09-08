using System;
using System.Threading;

namespace ExceptionHandling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                new Thread(DoWork).Start();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DoWork()
        {
            throw new ArgumentException();
        }
    }
}
