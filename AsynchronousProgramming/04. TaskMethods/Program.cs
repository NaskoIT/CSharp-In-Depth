using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskMethods
{
    public class Program
    {
        public static void Main()
        {
            WaitForTask();
            // TaskContinuation();
            // TaskExceptionsAndStatus();
            // MultipleTasksAtTheSameTime();
            // AtLeastOneTaskToFinish();
            // CompletedTaskAndFromResult();
            // DownloadContentAndSaveItToFile();
        }

        public static void WaitForTask()
        {
            Task firstTask = Task.Run(() =>
            {
                Console.WriteLine("First task");
            });

            Task<string> secondTask = Task.Run(() => "Second task");

            Console.WriteLine("Sync write!");

            firstTask.Wait();

            string result = secondTask.Result;

            Console.WriteLine(result);
        }

        public static void TaskContinuation()
        {
            Task task = Task
                .Run(() => "Result")
                .ContinueWith(previousTask =>
                {
                    Console.WriteLine(previousTask.Result);
                })
                .ContinueWith(previousTask => Task.Delay(2000).Wait())
                .ContinueWith(previousTask =>
                {
                    Console.WriteLine("After delay!");
                });

            task.Wait();
        }

        public static void TaskExceptionsAndStatus()
        {
            Task task = Task
                .Run(() => throw new InvalidOperationException("Some exception"))
                .ContinueWith(previousTask =>
                {
                    if (previousTask.IsFaulted)
                    {
                        Console.WriteLine(previousTask.Exception.Message);
                    }
                })
                .ContinueWith(previousTask =>
                {
                    if (previousTask.IsCompletedSuccessfully)
                    {
                        Console.WriteLine("Done");
                    }
                });

            task.Wait();
        }

        public static void MultipleTasksAtTheSameTime()
        {
            var firstTask = Task
                .Run(() => Task.Delay(3000).Wait())
                .ContinueWith(_ => Console.WriteLine("First"));

            var secondTask = Task
                .Run(() => Task.Delay(1000).Wait())
                .ContinueWith(_ => Console.WriteLine("Second"));

            var thirdTask = Task
                .Run(() => Task.Delay(2000).Wait())
                .ContinueWith(_ => Console.WriteLine("Third"));

            Task.WaitAll(firstTask, secondTask, thirdTask);
        }

        public static void AtLeastOneTaskToFinish()
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

            Task timerTask = Task.Run(() =>
            {
                for (var i = 5; i > 0; i--)
                {
                    Console.WriteLine(i);

                    Task.Delay(1000).Wait();
                }
            });

            Task.WaitAny(inputTask, timerTask);
        }

        public static void CompletedTaskAndFromResult()
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

                task.Wait();
            }
        }

        public static void DownloadContentAndSaveItToFile()
        {
            using var httpClient = new HttpClient();

            Task<string> googleTask = httpClient.GetStringAsync("https://google.com");

            googleTask
                .ContinueWith(prevTask =>
                {
                    File.WriteAllTextAsync("google.txt", prevTask.Result).Wait();
                });

            Task<string> microsoftTask = httpClient.GetStringAsync("https://www.microsoft.com/bg-bg");

            microsoftTask
                .ContinueWith(prevTask =>
                {
                    File.WriteAllTextAsync("microsoft.txt", prevTask.Result).Wait();
                });

            Task<string> myGitHubTask = httpClient.GetStringAsync("https://github.com/NaskoVasilev");

            myGitHubTask
                .ContinueWith(prevTask =>
                {
                    File.WriteAllTextAsync("myGitHub.txt", prevTask.Result).Wait();
                });

            var tasks = new[]
            {
                googleTask,
                microsoftTask,
                myGitHubTask
            };

            Task.WhenAll(tasks)
                .ContinueWith(prevTask =>
                {
                    string content = $"{prevTask.Result[0]}{prevTask.Result[1]}{prevTask.Result[2]}";

                    File.AppendAllTextAsync("downloads.txt", content).Wait();
                })
                .Wait();
        }
    }
}
