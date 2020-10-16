using System;
using FileProcessor.Entities;

namespace FileProcessor.Tasks
{
    public class ProcessFileTask
    {
        public ProcessFileTask(string fileToProcess, FileSchema fileSchema)
        {
            Console.WriteLine($"REACHED {fileToProcess} {fileSchema.Records.Count}");
        }

        public static void Execute(string fileToProcess, FileSchema fileSchema)
        {
            new ProcessFileTask(fileToProcess, fileSchema);
        }
    }
}