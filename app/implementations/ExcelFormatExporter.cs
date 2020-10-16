using ClosedXML.Excel;
using System.Collections.Generic;

namespace FileProcessor.Formatters
{
    public class ExcelFormatExporter : IFormatExporter
    {
        public void Export(string fileName, List<string[]> body, string[] headers)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Collections");

            ws.Cell(1, 1).Value = headers;
            ws.Cell(2, 1).Value = body;

            wb.SaveAs($"{fileName}.xlsx");
        }

    }
}