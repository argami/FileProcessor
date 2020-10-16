using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FileProcessor.Services;
using System.Xml.Serialization;
using FileProcessor.Entities;
using Microsoft.Extensions.Configuration;


namespace FileProcessor
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static IAppSettings _config;
        private static ILogger _logger;

        private static FileSchema _fileSchema;


        private static void LoadConfiguration()
        {
            try
            {
                var config = new ConfigurationBuilder()
                                    .AddJsonFile(@"./appsettings.json", false, true)
                                    .Build();
                _config = new AppSettings();

                config.Bind(_config);

                Directory.CreateDirectory(_config.OutputDir);
                Directory.CreateDirectory(_config.WatchDir);
                Directory.CreateDirectory(_config.SchemasDir);
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error Loading Configuration {e.Message}");
            }
        }

        private static FileSchema DeserializeObject(string filename)
        {
            Console.WriteLine("Reading with Stream");
            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(typeof(FileSchema));

            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                FileSchema i = (FileSchema)serializer.Deserialize(reader);
                return i;
            }

        }

        private static void LoadSchemas()
        {
            // This is prepared to load multiple schemas
            _fileSchema = DeserializeObject(@"./schemas/CONTABIL.xml");

        }

        static void Main(string[] args)
        {

            RegisterServices();

            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<WatcherService>().Run(_config.WatchDir, _fileSchema.Filter);

            DisposeServices();
        }

        private static void RegisterServices()
        {
            // required 
            LoadConfiguration();
            LoadSchemas();

            var services = new ServiceCollection()
                                    .AddLogging(builder => builder.AddConsole())
                                    .AddSingleton<ITasksService, TasksService>()
                                    .AddSingleton(_config)
                                    .AddSingleton<WatcherService>()
                                    .AddSingleton(_fileSchema);
            _serviceProvider = services.BuildServiceProvider(true);

            _logger = _serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
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
