using System;
using System.Security.Cryptography;

namespace RandomNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintPseudoRandomNumbers(4);
            Console.WriteLine(new string('=', 50));
            PrintCryptoRandomNumbers(4);
        }

        private static void PrintPseudoRandomNumbers(int numberOfRandomNumbers)
        {
            // In .NET Framework these two gets the same seed when created in the same millisecond
            var firstRandomNumbersGenerator = new Random(0);
            var secondRandomNumbersGenerator = new Random(0);

            Console.Write("firstRandomNumbersGenerator: ");
            for (var i = 0; i < numberOfRandomNumbers; i++)
            {
                Console.Write("{0} ", firstRandomNumbersGenerator.Next());
            }

            Console.WriteLine();

            Console.Write("secondRandomNumbersGenerator: ");
            for (var i = 0; i < numberOfRandomNumbers; i++)
            {
                Console.Write("{0} ", secondRandomNumbersGenerator.Next());
            }

            Console.WriteLine();
        }

        private static void PrintCryptoRandomNumbers(int numberOfRandomNumbers)
        {
            RandomNumberGenerator firstRandomNumberGenerator = RandomNumberGenerator.Create();
            RandomNumberGenerator secondRandomNumberGenerator = RandomNumberGenerator.Create();

            Console.WriteLine("First RNGCryptoServiceProvider:");
            for (int i = 0; i < numberOfRandomNumbers; i++)
            {
                var randomInt32Value = GenerateRandomInt32Value(firstRandomNumberGenerator);
                Console.Write("{0} ", randomInt32Value);
            }

            Console.WriteLine();

            Console.Write("Second RNGCryptoServiceProvider: ");
            for (int i = 0; i < numberOfRandomNumbers; i++)
            {
                var randomInt32Value = GenerateRandomInt32Value(secondRandomNumberGenerator);
                Console.Write("{0} ", randomInt32Value);
            }

            Console.WriteLine();

            static int GenerateRandomInt32Value(RandomNumberGenerator randomNumberGenerator)
            {
                var fourRandomBytes = new byte[4]; // 4 bytes = 32 bits = Int32
                randomNumberGenerator.GetBytes(fourRandomBytes);
                int randomInt32Value = BitConverter.ToInt32(fourRandomBytes, 0);
                return randomInt32Value;
            }
        }
    }
}
