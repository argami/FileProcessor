using System;

namespace FileProcessor
{
    public interface IAppSettings
    {
        string OutputDir { get; set; }

        string SchemasDir { get; set; }


        string WatchDir { get; set; }

    }
}