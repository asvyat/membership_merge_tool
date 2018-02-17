using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Membership_Merge_Tool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var t = inputDataList.FirstOrDefault();
            var desc = ValueHelper.GetDescription(() => t.FirstName);

            // For each MembershipData data property we need to map 
            // to the actual Header column name in Excel file
            // for example: MembershipData.FirstName => ExcelFileHeaderRow.First_Name
            var headerMappingOfColumnNames = new Dictionary<string, string>();

            using (SpreadsheetDocument document = SpreadsheetDocument.Open(excelFileName, true))
            {
                var workSheets = document.WorkbookPart.Workbook.Descendants<Sheet>();
                var sharedStrings = document.WorkbookPart.SharedStringTablePart.SharedStringTable;

                var firstSheetId = workSheets.First().Id;
                var firstSheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(firstSheetId);
                var firstWorksheet = firstSheetPart.Worksheet;
                var isHeader = true;

                //var rows = from row in firstWorksheet.Descendants<Row>()
                //    where row.RowIndex > 1
                //    select row;

                foreach (Row row in firstWorksheet.Descendants<Row>())
                {
                    if (isHeader)
                    {
                        headerMappingOfColumnNames = GetColumnMappingFromHeaderRow(sharedStrings, row);                        
                    }
                    else
                    {
                        UpdateWorksheetRowFromMembershipData(row, inputDataList);
                    }
                    isHeader = false;
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

                    //    var textValues = from cell in row.Descendants<Cell>()
                    //select
                    //    (cell.DataType != null
                    //    && cell.DataType.HasValue
                    //    && cell.DataType == CellValues.SharedString
                    //    ? sharedStrings.ChildElements[int.Parse(cell.CellValue.InnerText)].InnerText
                    //    : cell.CellValue.InnerText);
                }
            }   
        }

        private static void UpdateWorksheetRowFromMembershipData(Row row, List<MembershipData> inputDataList)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<string, string> GetColumnMappingFromHeaderRow(SharedStringTable sharedStrings, Row row)
        {

            var returnDictionary = new Dictionary<string, string>();

            foreach (Cell cell in row.Descendants<Cell>())
            {
                string cellVal;
                string col;
                GetCellValueAndColumn(sharedStrings, row, cell, out cellVal, out col);

                //if (cellVal.Equals(this.stateInformation.Schema.ColumnNameOfCLCID))
                //{
                //    clcidCol = col;
                //}
            }
            return returnDictionary;
        }

        public static void GetCellValueAndColumn(SharedStringTable sharedStrings, Row row, Cell cell, out string cellValue, out string collumn)
        {
            cellValue = string.Empty;
            if (cell.CellValue != null)
            {
                cellValue = (cell.DataType != null
                         && cell.DataType.HasValue
                         && cell.DataType == CellValues.SharedString)
                         ? sharedStrings.ChildElements[int.Parse(cell.CellValue.InnerText)].InnerText
                         : cell.CellValue.InnerText;
            }

            string celRef = cell.CellReference.Value;
            collumn = celRef.Substring(0, celRef.IndexOf(row.RowIndex.ToString()));
        }
    }
}
