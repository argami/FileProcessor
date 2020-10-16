using System;
using System.IO;
using System.Security.Permissions;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Services
{
    public class WatcherService
    {

        private static ILogger _logger;
        private static ITasksService _taskService;

        public WatcherService(ILoggerFactory loggerFactory, ITasksService taskService)
        {
            _logger = loggerFactory.CreateLogger<WatcherService>();
            _taskService = taskService;
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

                // process the tasks.
                _taskService.processTasks();

                // Wait for the user to quit the program.
                _logger.LogInformation("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;

            }
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {

            _logger.LogInformation($"Enqueue {e.FullPath}");
            _taskService.Enqueue(e.FullPath);
        }
    }
}
