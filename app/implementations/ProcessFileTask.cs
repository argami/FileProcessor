using System;
using FileProcessor.Entities;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using FileProcessor.Formatters;

namespace FileProcessor.Tasks
{
    public class ProcessFileTask
    {
        public static void Execute(string fileToProcess, FileSchema fileSchema, ILogger logger, IAppSettings config)
        {
            (new ProcessFileTask()).execute(fileToProcess, fileSchema, logger, config);
        }

        public void execute(string fileToProcess, FileSchema fileSchema, ILogger logger, IAppSettings config)
        {
            if (!File.Exists(fileToProcess))
            {
                logger.LogError($"Error Processing file: {fileToProcess} Not Found");
            }

            var directoryTo = Path.Join(config.OutputDir, DateTime.Now.ToString("yyyyMMddHHmmss"));

            Directory.CreateDirectory(directoryTo);

            var moveTo = Path.Join(directoryTo, Path.GetFileName(fileToProcess));
            File.Move(fileToProcess, moveTo);

            Dictionary<string, List<string[]>> results = new Dictionary<string, List<string[]>>();
            foreach (string line in File.ReadLines(moveTo))
            {
                try
                {
                    var lineParsed = fileSchema.ParseLine(line);
                    if (!results.ContainsKey(lineParsed.Key))
                    {
                        results.TryAdd(lineParsed.Key, new List<string[]>());
                    }

                    var value = results.GetValueOrDefault(lineParsed.Key);
                    value.Add(lineParsed.Value);

                }
                catch (System.Exception e)
                {
                    logger.LogError($"Error Processing {line}: Exception: {e.Message}");
                }
            }

            var exporter = new ExcelFormatExporter();
            foreach (var result in results)
            {
                var headers = fileSchema.GetHeadersByKey(result.Key);

                exporter.Export(Path.Join(directoryTo, $"Results_{result.Key}"), result.Value, headers.ToArray());
            }


        }



    }
}