using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class Program
    {
        public static async Task Main()
        {
            await WaitForTask();
            await TaskContinuation();
            await TaskExceptionsAndStatus();
            await MultipleTasksAtTheSameTime();
            await AtLeastOneTaskToFinish();
            await CompletedTaskAndFromResult();
            await DownloadContentAndSaveItToFile();

            AsyncLambda();
        }

        public static async Task WaitForTask()
        {
            //var firstTask = Task.Run(() =>
            //{
            //    Console.WriteLine("First task");
            //});

            //var secondTask = Task.Run(() => "Second task");

            //Console.WriteLine("Sync write!");

            //firstTask.Wait();

            //var result = secondTask.Result;

            //Console.WriteLine(result);

            Task firstTask = Task.Run(() =>
            {
                Console.WriteLine("First task");
            });

            Task<string> secondTask = Task.Run(() => "Second task");

            Console.WriteLine("Sync write!");

            await firstTask;

            string result = await secondTask;

            Console.WriteLine(result);
        }

        public static async Task TaskContinuation()
        {
            //var task = Task
            //    .Run(() => "Result")
            //    .ContinueWith(previousTask =>
            //    {
            //        Console.WriteLine(previousTask.Result);
            //    })
            //    .ContinueWith(previousTask => Task.Delay(2000).Wait())
            //    .ContinueWith(previousTask =>
            //    {
            //        Console.WriteLine("After delay!");
            //    });

            //task.Wait();

            string result = await Task.Run(() => "Result");

            Console.WriteLine(result);

            await Task.Delay(2000);

            Console.WriteLine("After delay!");
        }

        public static async Task TaskExceptionsAndStatus()
        {
            try
            {
                await Task.Run(() => throw new InvalidOperationException("Some exception"));
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("Done");

            //var task = Task
            //    .Run(() => throw new InvalidOperationException("Some exception"))
            //    .ContinueWith(previousTask =>
            //    {
            //        if (previousTask.IsFaulted)
            //        {
            //            Console.WriteLine(previousTask.Exception.Message);
            //        }
            //    })
            //    .ContinueWith(previousTask =>
            //    {
            //        if (previousTask.IsCompletedSuccessfully)
            //        {
            //            Console.WriteLine("Done");
            //        }
            //    });

            //task.Wait();
        }

        public static async Task MultipleTasksAtTheSameTime()
        {
            Task firstTask = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("First");
            });

            Task secondTask = Task.Run(async () =>
            {
                await Task.Delay(1000);
                Console.WriteLine("Second");
            });

            Task thirdTask = Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine("Third");
            });

            await Task.WhenAll(firstTask, secondTask, thirdTask);

            //var firstTask = Task
            //    .Run(() => Task.Delay(3000).Wait())
            //    .ContinueWith(_ => Console.WriteLine("First"));

            //var secondTask = Task
            //    .Run(() => Task.Delay(1000).Wait())
            //    .ContinueWith(_ => Console.WriteLine("Second"));

            //var thirdTask = Task
            //    .Run(() => Task.Delay(2000).Wait())
            //    .ContinueWith(_ => Console.WriteLine("Third"));

            //Task.WaitAll(firstTask, secondTask, thirdTask);
        }

        public static async Task AtLeastOneTaskToFinish()
        {
            Console.WriteLine("You have 5 seconds to solve this: 111 * 111");

            Task inputTask = Task.Run(() =>
            {
                while (true)
                {
                    var input = Console.ReadLine();

                    if (input == "12321")
                    {
                        Console.WriteLine("Correct!");
                        break;
                    }

                    Console.WriteLine("Wrong answer!");
                }
            });

            Task timerTask = Task.Run(async () =>
            {
                for (var i = 5; i > 0; i--)
                {
                    Console.WriteLine(i);

                    await Task.Delay(1000);
                }
            });

            await Task.WhenAny(inputTask, timerTask);

            //Console.WriteLine("You have 5 seconds to solve this: 111 * 111");

            //var inputTask = Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        var input = Console.ReadLine();

            //        if (input == "12321")
            //        {
            //            Console.WriteLine("Correct!");
            //            break;
            //        }

            //        Console.WriteLine("Wrong answer!");
            //    }
            //});

            //var timerTask = Task.Run(() =>
            //{
            //    for (var i = 5; i > 0; i--)
            //    {
            //        Console.WriteLine(i);

            //        Task.Delay(1000).Wait();
            //    }
            //});

            //Task.WaitAny(inputTask, timerTask);
        }

        public static async Task CompletedTaskAndFromResult()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "end")
                {
                    break;
                }

                Task task = input switch
                {
                    "delay" => Task
                        .Delay(2000)
                        .ContinueWith(_ => Console.WriteLine("Delayed")),

                    "print" => Task
                        .Run(() => Console.WriteLine("Printed!")),

                    "throw" => Task
                        .FromException(new InvalidOperationException("Error"))
                        .ContinueWith(prev => Console.WriteLine(prev.Exception.Message)),

                    "42" => Task
                        .FromResult(42)
                        .ContinueWith(prev => Console.WriteLine(prev.Result)),

                    _ => Task
                        .CompletedTask
                        .ContinueWith(_ => Console.WriteLine("Invalid input!"))
                };

                await task;
            }

            //while (true)
            //{
            //    var input = Console.ReadLine();

            //    if (input == "end")
            //    {
            //        break;
            //    }

            //    var task = input switch
            //    {
            //        "delay" => Task
            //            .Delay(2000)
            //            .ContinueWith(_ => Console.WriteLine("Delayed")),

            //        "print" => Task
            //            .Run(() => Console.WriteLine("Printed!")),

            //        "throw" => Task
            //            .FromException(new InvalidOperationException("Error"))
            //            .ContinueWith(prev => Console.WriteLine(prev.Exception.Message)),

            //        "42" => Task
            //            .FromResult(42)
            //            .ContinueWith(prev => Console.WriteLine(prev.Result)),

            //        _ => Task
            //            .CompletedTask
            //            .ContinueWith(_ => Console.WriteLine("Invalid input!"))
            //    };

            //    task.Wait();
            //}
        }

        public static async Task DownloadContentAndSaveItToFile()
        {
            using var httpClient = new HttpClient();

            Task<string> googleTask = httpClient.GetStringAsync("https://google.com");
            Task<string> microsoftTask = httpClient.GetStringAsync("https://www.microsoft.com/bg-bg");
            Task<string> myGitHubTask = httpClient.GetStringAsync("https://github.com/NaskoVasilev");

            List<Task<string>> tasks = new List<Task<string>>
            {
                googleTask, microsoftTask, myGitHubTask,
            };

            (string google, string microsoft, string myGitHub) = await Task.WhenAll(tasks);

            List<Task> writeFileTasks = new List<Task>
            {
                File.WriteAllTextAsync("google.txt", google),
                File.WriteAllTextAsync("microsoft.txt", microsoft),
                File.WriteAllTextAsync("myGitHub.txt", myGitHub)
            };

            await Task.WhenAll(writeFileTasks);

            string content = $"{google}{microsoft}{myGitHub}";

            await File.AppendAllTextAsync("downloads.txt", content);

            //using var httpClient = new HttpClient();

            //Task<string> googleTask = httpClient.GetStringAsync("https://google.com");

            //googleTask
            //    .ContinueWith(prevTask =>
            //    {
            //        File.WriteAllTextAsync("google.txt", prevTask.Result).Wait();
            //    });

            //Task<string> microsoftTask = httpClient.GetStringAsync("https://www.microsoft.com/bg-bg");

            //microsoftTask
            //    .ContinueWith(prevTask =>
            //    {
            //        File.WriteAllTextAsync("microsoft.txt", prevTask.Result).Wait();
            //    });

            //Task<string> myGitHubTask = httpClient.GetStringAsync("https://github.com/NaskoVasilev");

            //myGitHubTask
            //    .ContinueWith(prevTask =>
            //    {
            //        File.WriteAllTextAsync("myGitHub.txt", prevTask.Result).Wait();
            //    });

            //var tasks = new[]
            //{
            //    googleTask,
            //    microsoftTask,
            //    myGitHubTask
            //};

            //Task.WhenAll(tasks)
            //    .ContinueWith(prevTask =>
            //    {
            //        string content = $"{prevTask.Result[0]}{prevTask.Result[1]}{prevTask.Result[2]}";

            //        File.AppendAllTextAsync("downloads.txt", content).Wait();
            //    })
            //    .Wait();
        }

        public static void AsyncLambda()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            list.ForEach(async number =>
            {
                await Task.Run(() => Console.WriteLine(number));
            });
        }
    }
}
