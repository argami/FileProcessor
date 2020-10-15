using System;
using System.IO;

namespace FileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // If a directory is not specified, or directory doesn't exists exit program.
            if (args.Length != 1 || !Directory.Exists(args[0]))
            {
                Console.WriteLine("Usage: Watcher.exe (directory)");
                return;
            }

            Watcher.Run(args[0], "*.dat");
        }

    }
}
