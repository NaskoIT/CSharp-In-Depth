using System;

namespace BasicDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintNameDelegate printNameDelegate = new PrintNameDelegate(PrintName);
            printNameDelegate("Atanas");

            Console.WriteLine("Result when pass the delegate as parameter to method.");
            PrintPersonInfo(printNameDelegate);

            Console.WriteLine("Store more than one method in delegate");
            CalculatorDelegate calculatorDelegate = Sum;
            calculatorDelegate += Multiply;
            calculatorDelegate += Divide;
            calculatorDelegate -= Multiply;
            calculatorDelegate(100, 20);

            Console.WriteLine();
            Console.WriteLine("Use annonymous function");
            calculatorDelegate += (double firstNumber, double secondNumber) => Console.WriteLine($"{firstNumber} * {secondNumber} = {firstNumber * secondNumber}");
            calculatorDelegate(20, 5);
        }

        private static void PrintName(string name)
        {
            Console.WriteLine("I am " + name);
        }

        private static void PrintPersonInfo(PrintNameDelegate printNameDelegate)
        {
            printNameDelegate("Atanas");
            Console.WriteLine("I am 18 years old");
        }

        private static void Sum(double firstNumber, double secondNumber)
        {
            Console.WriteLine($"{firstNumber} + {secondNumber} = {firstNumber + secondNumber}");
        }

        private static void Multiply(double firstNumber, double secondNumber)
        {
            Console.WriteLine($"{firstNumber} * {secondNumber} = {firstNumber * secondNumber}");
        }

        private static void Divide(double firstNumber, double secondNumber)
        {
            Console.WriteLine($"{firstNumber} / {secondNumber} = {firstNumber / secondNumber}");
        }
    }
}
