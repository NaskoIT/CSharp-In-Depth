using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace StateMachine
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Prints all state machines in the console.
            Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(d => d.Name.Contains("d_"))
                .ToList()
                .ForEach(Console.WriteLine);

            await Compiled.MainCompiled(args);
        }

        public static async Task SomeAsyncMethod(int id)
        {
            Console.WriteLine("Async without await " + id);
        }

        public static async Task SomeAsyncMethodWithAwait(int id)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Async with await " + id);
            });
        }

        public static async Task StateMachineCompiled()
        {
            int input = 5;

            await Task.Run(() =>
            {
                Console.WriteLine(input);
            });

            bool result = await Task.Run(() => true);

            Console.WriteLine(result);
        }

        public static async Task HardcoreStateMachineCompiled()
        {
            int input = 5;
            string secondInput = Console.ReadLine();

            Task<bool> result = await Task
                .Run(() =>
                {
                    Console.WriteLine(input);
                })
                .ContinueWith(async task =>
                {
                    await Task
                        .Delay(3000)
                        .ContinueWith(_ =>
                        {
                            Console.WriteLine(secondInput);
                        });
                })
                .ContinueWith(t =>
                {
                    return Task.Run(() => true);
                });

            Console.WriteLine(await result);
        }
    }
}
