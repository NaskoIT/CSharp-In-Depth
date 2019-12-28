using System;

namespace FuncAndActionDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Func example");
            Func<int, int, int> sumFunc = Sum;
            int sum = sumFunc(12, 18);
            Console.WriteLine(sum);

            Console.WriteLine();
            Console.WriteLine("Action example");
            Action<string> printName = PrintName;
            printName("Nasko");

            Console.WriteLine();
            Console.WriteLine("Pass action as argument to method");
            Action<Action<string>> printPersonInfo = PrintPersonInfo;
            printPersonInfo(PrintName);
            printPersonInfo(name => Console.WriteLine("My name is " + name));

            Console.WriteLine();
            Console.WriteLine("Store more than one method in Action");
            Action<double, double> calculatorDelegate = Multiply;
            calculatorDelegate += Divide;
            calculatorDelegate -= Multiply;
            calculatorDelegate(100, 20);

            Console.WriteLine();
            Console.WriteLine("Use annonymous function");
            calculatorDelegate += Multiply;
            calculatorDelegate += (double firstNumber, double secondNumber) => Console.WriteLine($"{firstNumber} + {secondNumber} = {firstNumber * secondNumber}");
            calculatorDelegate(20, 5);
        }

        private static int Sum(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        private static void PrintName(string name)
        {
            Console.WriteLine("I am " + name);
        }

        private static void PrintPersonInfo(Action<string> printName)
        {
            printName("Atanas");
            Console.WriteLine("I am 18 years old");
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
