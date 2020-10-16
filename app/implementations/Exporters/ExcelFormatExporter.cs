using ClosedXML.Excel;
using System.Collections.Generic;

namespace FileProcessor.Exporters
{
    public class ExcelFormatExporter : IFormatExporter
    {
        public void Export(string fileName, List<string[]> body, string[] headers)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Collections");

            for (int i = 1; i < headers.Length; i++)
            {
                ws.Cell(1, i).Value = headers[i - 1];
            }
            ws.Cell(2, 1).Value = body;

            wb.SaveAs($"{fileName}.xlsx");
        }

    }
}