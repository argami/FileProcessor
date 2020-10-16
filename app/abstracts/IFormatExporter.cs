using System.Collections.Generic;

namespace FileProcessor.Exporters
{
    public interface IFormatExporter
    {
        void Export(string fileName, List<string[]> body, string[] headers);

    }
}