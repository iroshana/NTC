using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IExelReportService
    {
        void CreateExcelHeader(ExcelWorksheet worksheet, string text, int row, int col, string cell1, string cell2, bool vAlign, bool hAlign);
        void CreateExcelContents(ExcelWorksheet worksheet, string text, int row, int col, string cell1, string cell2, bool vAlign, bool leftAlign, bool rightAlign, bool isBold, string formula, string format);
    }
}
