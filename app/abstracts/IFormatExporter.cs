using System.Collections.Generic;

namespace FileProcessor.Formatters
{
    public interface IFormatExporter
    {
        void Export(string fileName, List<string[]> body, string[] headers);

    }
}