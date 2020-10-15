using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace FileProcessor
{
    class Program
    {

        private static ILoggerFactory _loggerFactory;
        private static ILogger _logger;

        static void Main(string[] args)
        {
            RegisterLoggerFactory();

            // If a directory is not specified, or directory doesn't exists exit program.
            // i will extract this from config later
            if (args.Length != 1 || !Directory.Exists(args[0]))
            {
                Console.WriteLine("Usage: Watcher.exe (directory)");
                return;
            }

            _logger = _loggerFactory.CreateLogger<Program>();
            _logger.LogInformation("Example log message");

            Watcher watcher = new Watcher(_loggerFactory);
            //filter should be extracted from config
            watcher.Run(args[0], "*.dat");
        }

        private static void RegisterLoggerFactory()
        {
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
        }

    }
}
