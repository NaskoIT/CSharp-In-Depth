using OptionalParameters.Library;
using System;

namespace OptionalParameters.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var bankAccount = new BankAccount("John Doe");
            Console.WriteLine(bankAccount);
            Console.ReadLine();
        }
    }
}
