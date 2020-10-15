using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using FileProcessor.Services;


namespace FileProcessor
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            // If a directory is not specified, or directory doesn't exists exit program.
            // i will extract this from config later
            if (args.Length != 1 || !Directory.Exists(args[0]))
            {
                Console.WriteLine("Usage: Watcher.exe (directory)");
                return;
            }

            RegisterServices();

            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<WatcherService>().Run(args[0], "*.dat");

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection()
            .AddLogging()
            // .AddSingleton<ICustomer, Customer>()
            .AddSingleton<WatcherService>();
            _serviceProvider = services.BuildServiceProvider(true);
        }


        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }

    }
}
