using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.Style;
using NTC.InterfaceServices;

namespace NTC.Services
{
    public class ExelReportService : IExelReportService
    {
        public void CreateExcelHeader(ExcelWorksheet worksheet, string text, int row, int col, string cell1, string cell2, bool vAlign, bool hAlign)
        {
            string cellId = cell1 + ":" + cell2;
            worksheet.Cells[cellId].Value = text;
            worksheet.Cells[cellId].Merge = true;
            if (hAlign)
            {
                worksheet.Cells[cellId].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
            if (vAlign)
            {
                worksheet.Cells[cellId].Style.Border.Diagonal.Style = ExcelBorderStyle.Double;
            }
            worksheet.Cells[cellId].Style.Font.Bold = true;
            worksheet.Cells[cellId].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[cellId].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[cellId].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[cellId].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }

        public void CreateExcelContents(ExcelWorksheet worksheet, string text, int row, int col, string cell1, string cell2, bool vAlign, bool leftAlign, bool rightAlign, bool isBold, string formula, string format)
        {
            string cellId = cell1 + ":" + cell2;
            worksheet.Cells[cellId].Value = text;
            worksheet.Cells[cellId].Merge = true;

            if (vAlign)
            {
                worksheet.Cells[cellId].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
            if (leftAlign)
            {
                worksheet.Cells[cellId].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            }
            if (rightAlign)
            {
                worksheet.Cells[cellId].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }

            if (formula != "")
            {
                worksheet.Cells[cellId].Formula = formula;
            }

            if (format != "")
            {
                worksheet.Cells[cellId].Style.Numberformat.Format = "##0,00";//format;
            }

            if (isBold)
            {
                worksheet.Cells[cellId].Style.Font.Bold = true;
            }
            //worksheet.Cells[cellId].Style.Border.BorderAround = 
            worksheet.Cells[cellId].Style.Border.Top.Style = ExcelBorderStyle.None;
            worksheet.Cells[cellId].Style.Border.Bottom.Style = ExcelBorderStyle.None;
            worksheet.Cells[cellId].Style.Border.Left.Style = ExcelBorderStyle.None;
            worksheet.Cells[cellId].Style.Border.Right.Style = ExcelBorderStyle.None;



            char firstCell = cell1[0];
            char secondCell = cell2[0];

            int index1 = char.ToUpper(firstCell) - 64;
            int index2 = char.ToUpper(secondCell) - 64;

            ExcelRange columnCells = worksheet.Cells[worksheet.Dimension.Start.Row, index1, worksheet.Dimension.End.Row, index2];
            // Check what is the longest string and set the length
            int maxLength = columnCells.Max(cell => cell.Value.ToString().Count(c => char.IsLetterOrDigit(c)));

            worksheet.Column(index2).Width = maxLength - 5; // 2 is just an extra buffer for all that is not letter/digits.
        }
    }
}
