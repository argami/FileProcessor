using System;

namespace FileProcessor
{
    public class AppSettings : IAppSettings
    {
        public string ProcessedFilesDir { get; set; }

        public string OutputDir { get; set; }

        public string SchemasDir { get; set; }


        public string WatchDir { get; set; }

    }
}