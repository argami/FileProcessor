using System;
using System.IO;
using System.Security.Permissions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace FileProcessor
{
    public class Watcher
    {

        private static ILogger _logger;

        public Watcher(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Watcher>();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run(string directoryToWatch, string filter)
        {

            _logger.LogInformation("Watching");
            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = directoryToWatch;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = filter;

                // Add event handlers.
                watcher.Created += OnChanged;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }
    }
}