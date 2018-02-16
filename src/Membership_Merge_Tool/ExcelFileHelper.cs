using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Membership_Merge_Tool.Models;
using System.Collections.Generic;
using System.Linq;

namespace Membership_Merge_Tool
{
    public class ExcelFileHelper
    {
        /// <summary>
        /// Merge or update input data into Excel File
        /// </summary>
        public static void MergeInputDataIntoExcelFile(string excelFileName, List<MembershipData> inputDataList)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(excelFileName, true))
            {
                var workSheets = document.WorkbookPart.Workbook.Descendants<Sheet>();
                var sharedStrings = document.WorkbookPart.SharedStringTablePart.SharedStringTable;

                var firstSheetId = workSheets.First().Id;
                var firstSheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(firstSheetId);
                var firstWorksheet = firstSheetPart.Worksheet;

                var rows = from row in firstWorksheet.Descendants<Row>()
                    where row.RowIndex > 1
                    select row;

                foreach (Row row in rows)
                {
                    // select non-empty values
                    //IEnumerable<String> textValues =
                    //    from cell in row.Descendants<Cell>()
                    //    where cell.CellValue != null
                    //    select
                    //        (cell.DataType != null
                    //        && cell.DataType.HasValue
                    //        && cell.DataType == CellValues.SharedString
                    //        ? sharedStrings.ChildElements[int.Parse(cell.CellValue.InnerText)].InnerText
                    //        : cell.CellValue.InnerText);

                    // select all values
                    //var textValues = row.Descendants<Cell>()
                    //    .Select(c => c.DataType != null
                    //    && c.DataType == CellValues.SharedString
                    //    ? sharedStrings.ChildElements[int.Parse(c.CellValue.InnerText)].InnerText
                    //    : c.CellValue.InnerText);

                    var textValues = from cell in row.Descendants<Cell>()
                        select
                            (cell.DataType != null
                            && cell.DataType.HasValue
                            && cell.DataType == CellValues.SharedString
                            ? sharedStrings.ChildElements[int.Parse(cell.CellValue.InnerText)].InnerText
                            : cell.CellValue.InnerText);



                }
            }

        }
    }
}
