using System;
using System.Threading.Tasks;

namespace FromSyncToAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple example");
            SimpleExample();

            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Complex exmple");
            ComplexExample();
        }

        public static void SimpleExample()
        {
            var task = Task.Run(() => Console.WriteLine("First"));

            Console.WriteLine("Second");

            task.Wait();
        }

        public static void ComplexExample()
        {
            Task<string> task = Task.Run(() => Task
                    .Delay(2000)
                    .ContinueWith(t => "In a task"));

            Task.Delay(4000).Wait();
            Console.WriteLine("Outside of a task!");

            Task<Task> completion = Task
                .WhenAll(task)
                .ContinueWith(async t =>
                {
                    Console.WriteLine((await t)[0]);
                });

            completion.Wait();
        }
    }
}
