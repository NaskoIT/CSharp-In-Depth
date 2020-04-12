using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PerformanceDemo
{
    public class Program
    {
        private const int NumberOfCpus = 12;

        private const string ImagesDirectory = @"../../../Images";
        private const string CpuSyncDirectory = ImagesDirectory + @"\CpuSync\";
        private const string CpuAsyncDirectory = ImagesDirectory + @"\CpuAsync\";
        private const string CpuAsyncBlockingDirectory = ImagesDirectory + @"\CpuAsyncBlocking\";
        private const string CpuAsyncMultipleDirectory = ImagesDirectory + @"\CpuAsyncMultiple\";

        public static async Task Main()
        {
            //Summary benchmark = BenchmarkRunner.Run<Program>();

            //Console.WriteLine(benchmark);

            var program = new Program();

            Stopwatch stopWatch = Stopwatch.StartNew();

            program.ResizeCpuSynchronously();

            Console.WriteLine($"CPU Sync - {stopWatch.Elapsed}");

            stopWatch = Stopwatch.StartNew();

            await program.ResizeCpuAsynchronously();

            Console.WriteLine($"CPU Async - {stopWatch.Elapsed}");

            stopWatch = Stopwatch.StartNew();

            await program.ResizeCpuAsynchronouslyMultipleTasks();

            Console.WriteLine($"CPU Async Multiple Tasks - {stopWatch.Elapsed}");

            stopWatch = Stopwatch.StartNew();

            program.ResizeCpuAsynchronouslyBlocking();

            Console.WriteLine($"CPU Asynchronously blocking - {stopWatch.Elapsed}");
        }

        [Benchmark]
        public void ResizeCpuSynchronously()
        {
            string dir = CpuSyncDirectory;

            Directory.CreateDirectory(dir);

            IEnumerable<string> files = ReadFiles().Take(NumberOfCpus);

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                ProcessImage(file, dir + $"{fileInfo.Name}");
            }

            Directory.Delete(dir, true);
        }

        [Benchmark]
        public async Task ResizeCpuAsynchronously()
        {
            string dir = CpuAsyncDirectory;

            Directory.CreateDirectory(dir);

            IEnumerable<string> files = ReadFiles().Take(NumberOfCpus);

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                await Task.Run(() =>
                {
                    ProcessImage(file, dir + $"{fileInfo.Name}");
                });
            }

            Directory.Delete(dir, true);
        }

        [Benchmark]
        public void ResizeCpuAsynchronouslyBlocking()
        {
            string dir = CpuAsyncBlockingDirectory;

            Directory.CreateDirectory(dir);

            IEnumerable<string> files = ReadFiles();

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                Task.Run(() =>
                {
                    ProcessImage(file, dir + $"{fileInfo.Name}");
                })
                .Wait();
            }

            Directory.Delete(dir, true);
        }

        [Benchmark]
        public async Task ResizeCpuAsynchronouslyMultipleTasks()
        {
            string dir = CpuAsyncMultipleDirectory;

            Directory.CreateDirectory(dir);

            IEnumerable<string> files = ReadFiles().Take(NumberOfCpus);

            List<Task> tasks = new List<Task>();

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                tasks.Add(Task.Run(() =>
                {
                    ProcessImage(file, dir + $"{fileInfo.Name}");
                }));
            }

            await Task.WhenAll(tasks);

            Directory.Delete(dir, true);
        }

        private static IEnumerable<string> ReadFiles() => 
            Directory.GetFiles(ImagesDirectory);

        private static void ProcessImage(string inputPath, string outputPath)
        {
            using Image image = Image.Load(inputPath);

            image.Mutate(x => x.Resize(2000, 2000));

            image.Save(outputPath);
        }
    }
}
