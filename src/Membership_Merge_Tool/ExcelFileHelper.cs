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
            var headerMappingOfColumnNames = new List<MembershipColumnMapper>();

            using (SpreadsheetDocument document = SpreadsheetDocument.Open(excelFileName, true))
            {
                var workSheets = document.WorkbookPart.Workbook.Descendants<Sheet>();
                var sharedStrings = document.WorkbookPart.SharedStringTablePart.SharedStringTable;

                var firstSheetId = workSheets.First().Id;
                var firstSheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(firstSheetId);
                var firstWorksheet = firstSheetPart.Worksheet;
                var isHeader = true;
                               
                foreach (Row row in firstWorksheet.Descendants<Row>())
                {
                    if (isHeader)
                    {
                        headerMappingOfColumnNames = GetMembershipColumnMapper(sharedStrings, row, inputDataList.FirstOrDefault());                        
                    }
                    else
                    {
                        UpdateWorksheetRowFromMembershipData(headerMappingOfColumnNames, row, inputDataList);
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

        private static void UpdateWorksheetRowFromMembershipData(List<MembershipColumnMapper>headerMappingOfColumnNames, Row row, List<MembershipData> inputDataList)
        {
            throw new NotImplementedException();
        }

        private static List<MembershipColumnMapper> GetMembershipColumnMapper(SharedStringTable sharedStrings, Row row, MembershipData membershipData)
        {
            var returnList = new List<MembershipColumnMapper>();

            // First get all the properties from MembershipData
            //using reflection to create a list of existing Doc properties and their values
            var allMembershipProperties = membershipData.GetType().GetProperties().ToList();
            foreach (var membershipProperty in allMembershipProperties)
            {
                //var desc = ValueHelper.GetDescription(() => membershipProperty);
                var descriptionAttribute = (DescriptionAttribute)membershipProperty.GetCustomAttributes(false).FirstOrDefault();

                var membershipColumnMapper = new MembershipColumnMapper { MembershipDataPropertyName = membershipProperty.Name };
                if (descriptionAttribute != null && !string.IsNullOrWhiteSpace(descriptionAttribute.Description))
                {
                    membershipColumnMapper.ExcelFileColumnName = descriptionAttribute.Description;
                }
                returnList.Add(membershipColumnMapper);
            }

            // Next will match to a column name from the header row
            foreach (Cell cell in row.Descendants<Cell>())
            {
                string columnName;
                string collumnIndex;
                GetCellValueAndColumn(sharedStrings, row, cell, out columnName, out collumnIndex);

                var toUpdate = returnList
                    .SingleOrDefault(i => i.ExcelFileColumnName.Equals(columnName, StringComparison.InvariantCultureIgnoreCase));
                               
                if (toUpdate != null)
                {
                    toUpdate.ExcelFileColumnIndex = collumnIndex;
                }
            }
            return returnList;
        }

        public static void GetCellValueAndColumn(SharedStringTable sharedStrings, Row row, Cell cell, out string cellValue, out string collumnIndex)
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
            collumnIndex = celRef.Substring(0, celRef.IndexOf(row.RowIndex.ToString()));
        }
    }
}
