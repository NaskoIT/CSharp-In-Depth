using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cancellation
{
    public class Program
    {
        public static async Task Main()
        {
            // var cancellation = new CancellationTokenSource(2000);
            CancellationTokenSource cancellation = new CancellationTokenSource();

            Task printTask = Task.Run(async () =>
            {
                while (true)
                {
                    if (cancellation.IsCancellationRequested)
                    {
                        Console.WriteLine("Enough!");

                        // cancellation.Token.ThrowIfCancellationRequested();

                        break;
                    }

                    Console.WriteLine(DateTime.Now);

                    await Task.Delay(1000);
                }
            }, cancellation.Token);

            Task readTask = Task.Run(() =>
            {
                while (true)
                {
                    string input = Console.ReadLine();

                    if (input == "end")
                    {
                        cancellation.Cancel();
                        break;
                    }
                }
            });

            await Task.WhenAll(printTask, readTask);
        }
    }
}
