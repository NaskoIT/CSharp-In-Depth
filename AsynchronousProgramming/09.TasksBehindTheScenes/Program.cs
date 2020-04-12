﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace TasksBehindTheScenes
{
    public class Program
    {
        public static async Task Main()
        {
            bool result = await RunAsync(() => true);
            Console.WriteLine(result);

            await DelayAsync(2000, () => Console.WriteLine("Delayed!"));

            await RunCancellation();

            string eventBasedResult = await EventBasedApiWrapper();
            Console.WriteLine(eventBasedResult);
        }

        public static async Task RunCancellation()
        {
            CancellationTokenSource cancellation = new CancellationTokenSource();
            int counter = 0;

            Task task = Task.Run(async () =>
            {
                while (true)
                {
                    if (counter == 5)
                    {
                        cancellation.Cancel();
                        break;
                    }

                    await Task.Delay(1000);

                    Console.WriteLine(DateTime.Now);

                    counter++;
                }
            });

            Task cancellationEnd = Task.Run(() => Console.WriteLine("End"));

            await Task.WhenAll(
                task,
                RunUntilCancellation(cancellation.Token, () => cancellationEnd));
        }

        public static Task<T> RunAsync<T>(Func<T> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    T result = function();
                    tcs.SetResult(result);
                }
                catch (Exception exc)
                {
                    tcs.SetException(exc);
                }
            });

            return tcs.Task;
        }

        public static Task DelayAsync(int millisecondsDelay, Action action)
        {
            if (millisecondsDelay < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(millisecondsDelay));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            var timer = new Timer(_ => tcs.SetResult(null), null, millisecondsDelay, Timeout.Infinite);

            return tcs.Task.ContinueWith(_ =>
            {
                timer.Dispose();
                action();
            });
        }

        public static Task RunUntilCancellation(
            CancellationToken cancellationToken,
            Func<Task> onCancel)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            cancellationToken.Register(
                async () =>
                {
                    await onCancel();
                    tcs.SetResult(true);
                });

            return tcs.Task;
        }

        public static Task<string> EventBasedApiWrapper()
        {
            var tcs = new TaskCompletionSource<string>();

            var obj = new EventBasedApi();

            obj.Done += arg =>
            {
                // This will notify the caller 
                // of the EventBasedApiWrapper that 
                // the task just completed.
                tcs.SetResult(arg);
            };

            // Start the event-based work.
            obj.Work();

            return tcs.Task;
        }
    }
}
