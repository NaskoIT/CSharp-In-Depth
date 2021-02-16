using System.Collections.Generic;
using System.IO;

namespace MemoryLeaks
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "test.txt";

            for (int i = 0; i < 100000000; i++)
            {
                Runner runner = new Runner(fileName);
                Olympics.Runners.Add(runner);
            }
        }
    }

    public class Olympics
    {
        public static ICollection<Runner> Runners;
    }

    public class Runner
    {
        private FileStream fileStream;
        private string fileName;

        public Runner(string fileName)
        {
            this.fileName = fileName;
        }

        public void GetStats()
        {
            FileInfo fileInfo = new FileInfo(fileName);
            fileStream = fileInfo.OpenRead();
        }
    }
}
