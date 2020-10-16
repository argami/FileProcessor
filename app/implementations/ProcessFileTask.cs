using System;
using FileProcessor.Entities;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Tasks
{
    public class ProcessFileTask
    {
        public static void Execute(string fileToProcess, FileSchema fileSchema, ILogger logger)
        {
            (new ProcessFileTask()).execute(fileToProcess, fileSchema, logger);
        }

        public void execute(string fileToProcess, FileSchema fileSchema, ILogger logger)
        {
            Console.WriteLine($"REACHED {fileToProcess} {fileSchema.Records.Count}");

            Dictionary<string, List<string[]>> results = new Dictionary<string, List<string[]>>();
            foreach (string line in File.ReadLines(fileToProcess))
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
        }


    }
}