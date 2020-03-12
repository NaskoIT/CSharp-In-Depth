using System;
using System.IO;

namespace FileSystemWatcher
{
    class Program
    {
        public static void Main()
        {
            var fileSystemWatcher = new System.IO.FileSystemWatcher
            {
                // Filter = "*.cs",
                IncludeSubdirectories = true,
                Path = Environment.CurrentDirectory,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.DirectoryName,
                EnableRaisingEvents = true
            };

            /* Notify filters:
                FileName, DirectoryName, Attributes, Size
                LastWrite, LastAccess, CreationTime, Security
             */

            // Events may be raised more than once.
            // Read more: http://stackoverflow.com/questions/1764809/filesystemwatcher-changed-event-is-raised-twice
            fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
            fileSystemWatcher.Created += FileSystemWatcherOnCreated;
            fileSystemWatcher.Deleted += FileSystemWatcherOnDeleted;
            fileSystemWatcher.Error += FileSystemWatcherOnError;
            fileSystemWatcher.Renamed += FileSystemWatcherOnRenamed;
            Console.WriteLine($"Started watching {Environment.CurrentDirectory}");
            Console.WriteLine("Press [enter] to stop watching...");
            Console.ReadLine();
        }

        private static void FileSystemWatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Console.WriteLine($"{fileSystemEventArgs.ChangeType} {fileSystemEventArgs.Name}");
        }

        private static void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Console.WriteLine($"{fileSystemEventArgs.ChangeType} {fileSystemEventArgs.Name}");
        }

        private static void FileSystemWatcherOnDeleted(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Console.WriteLine($"{fileSystemEventArgs.ChangeType} {fileSystemEventArgs.Name}");
        }

        private static void FileSystemWatcherOnError(object sender, ErrorEventArgs errorEventArgs)
        {
            Console.WriteLine($"Error: {errorEventArgs.GetException()}");
        }

        private static void FileSystemWatcherOnRenamed(object sender, RenamedEventArgs renamedEventArgs)
        {
            Console.WriteLine($"{renamedEventArgs.ChangeType} {renamedEventArgs.OldName} to {renamedEventArgs.Name}");
        }
    }
}
