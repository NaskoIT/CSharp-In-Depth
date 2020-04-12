using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gotchas
{
    public class Program
    {
        public static async Task Main()
        {
            // Remember: async void is fire and forget. It is really bad pracice to be used!!!
            RunAsyncVoid();

            // AsyncVoidLambda();

            // NestedTasks();

            // Uncomment the line below and see the differene
            // Console.ReadLine();
        }

        public static void RunAsyncVoid()
        {
            try
            {
                AsyncVoid();
            }
            catch
            {
                Console.WriteLine("Cannot be caught!");
            }
        }

        public static void AsyncVoidLambda()
        {
            try
            {
                var list = new List<int> { 1, 2, 3, 4, 5 };

                list.ForEach(async number =>
                {
                    await Task.Run(() => Console.WriteLine(number));

                    throw new InvalidOperationException("In a lambda!");
                });
            }
            catch
            {
                Console.WriteLine("Cannot be caught!");
            }
        }

        public static async void AsyncVoid()
        {
            await Task.Run(() => Console.WriteLine("Message"));

            throw new InvalidOperationException("Error");
        }

        public static async void NestedTasks()
        {
            await Task.Run(async () =>
            {
                Console.WriteLine("Before Delay");

                await Task.Delay(1000);

                Console.WriteLine("After Delay");
            });

            Console.WriteLine("After Task");
        }
    }
}
