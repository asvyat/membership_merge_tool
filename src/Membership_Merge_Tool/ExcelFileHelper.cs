using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Membership_Merge_Tool.Enumerations;
using Membership_Merge_Tool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Membership_Merge_Tool
{
    public class ExcelFileHelper
    {
        /// <summary>
        /// Add new MembershipData into Excel file as new rows after the header
        /// </summary>
        public static int AddInputDataIntoExcelFile(string excelFileName, List<MembershipData> inputDataList)
        {
            int addedRows = 0;

            using (SpreadsheetDocument document = SpreadsheetDocument.Open(excelFileName, true))
            {
                var workSheets = document.WorkbookPart.Workbook.Descendants<Sheet>();
                var sharedStrings = document.WorkbookPart.SharedStringTablePart.SharedStringTable;

                var firstSheetId = workSheets.First().Id;
                var firstSheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(firstSheetId);
                //var firstWorksheet = firstSheetPart.Worksheet;
                //var isHeader = true;
                //uint headerRowIndex = 0;

                //worksheet.Rows.EntireRow.Insert(DocumentFormat.OpenXml.Office.Excel.XlInsertShiftDirection.xlShiftDown, false);

                foreach (var membershipData in inputDataList.Where(m => m.ExistsInExcelFile == false))
                {
                    SheetData sheetData = firstSheetPart.Worksheet.GetFirstChild<SheetData>();
                    Row lastRow = sheetData.Elements<Row>().LastOrDefault();
                    var newRow = sheetData.InsertAfter(new Row() { RowIndex = (lastRow.RowIndex + 1) }, lastRow);

                    foreach (var membershipDataValues in membershipData.GetNoEmptyCsvNewValueMembershipDataValues())
                    {
                        var newCell = InsertCellInWorksheet(membershipDataValues.ExcelFileColumnIndex, newRow.RowIndex, firstSheetPart);
                        newCell.CellValue = new CellValue(membershipDataValues.CsvNewValue);
                        newCell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                        addedRows++;
                    }

                    if (addedRows > 0)
                    {
                        // Save the worksheet.
                        firstSheetPart.Worksheet.Save();
                        document.Save();
                    }
                }
                return addedRows;
            }
        }

        /// <summary>
        /// Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
        /// If the cell already exists, returns it.
        /// Code taken from: https://msdn.microsoft.com/en-us/library/office/cc861607.aspx
        /// </summary> 
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        /// <summary>
        /// Merge or update input data into Excel File
        /// </summary>
        public static int UpdateInputDataIntoExcelFile(string excelFileName, List<MembershipData> inputDataList)
        {
            int updatedRows = 0;

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
                        UpdateMembershipPropertiesWithColumnIndex(sharedStrings, row, inputDataList);
                    }
                    else
                    {
                        updatedRows = updatedRows + UpdateWorksheetRowFromMembershipData(sharedStrings, row, inputDataList);
                    }
                    isHeader = false;
                }
                if (updatedRows > 0)
                {
                    // Save the worksheet.
                    firstSheetPart.Worksheet.Save();
                    document.Save();
                }
            }
            return updatedRows;
        }

        private static int UpdateWorksheetRowFromMembershipData(SharedStringTable sharedStrings, Row row, List<MembershipData> inputDataList)
        {
            int updatedRows = 0;
            var cellValue = string.Empty;
            var collumnIndex = string.Empty;
            var rowIndex = row.RowIndex.ToString();
            try
            {
                var currentMembershipData = new MembershipData();
                currentMembershipData.CloneExcelColumnIndexInAllProperties(inputDataList.FirstOrDefault());

                // First collecting current old data for each cell for update
                // into currentMembershipData to verify new and old values next
                foreach (var cell in row.Descendants<Cell>())
                {
                    cellValue = collumnIndex = string.Empty;
                    GetCellValueAndColumn(sharedStrings, row, cell, out cellValue, out collumnIndex);

                    // Update Excel Values for each inputData record
                    currentMembershipData.UpdateExcelCellOldValueInAllProperties(collumnIndex, cellValue);
                };

                // Next copy all CSV New values from inputData list that match the same email
                inputDataList.ForEach(m =>
                    {
                        if (m.Email.CsvNewValue.Equals(currentMembershipData.Email.ExcelCellOldValue, StringComparison.InvariantCultureIgnoreCase))
                        {
                            currentMembershipData.CloneCsvNewValueInAllProperties(m);
                        }
                    });

                // If no new value in email, nothing to update
                if (string.IsNullOrEmpty(currentMembershipData.Email.CsvNewValue))
                {
                    return updatedRows;
                }

                // Update Exists property for the current record
                inputDataList.Where(m => m.Email.CsvNewValue.Equals(currentMembershipData.Email.CsvNewValue, StringComparison.InvariantCultureIgnoreCase))
                            .SingleOrDefault().ExistsInExcelFile = true;

                // Finally verify if we need to update that record
                if (currentMembershipData.ContainsAnyNotMatchingOldAndNewValues())
                {
                    if (TryUpdateRow(sharedStrings, row, currentMembershipData))
                    {
                        updatedRows++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nException detected during updating cell value '{cellValue}' " +
                    $"of column Index '{collumnIndex}' and row Index '{rowIndex}'. {ex}");
                throw;
            }
            return updatedRows;
        }

        /// <summary>
        /// Return true if able to return Excel row.
        /// </summary>
        private static bool TryUpdateRow(SharedStringTable sharedStrings, Row rowToBeUpdated, MembershipData membershipData)
        {
            var updated = false;

            // Finally updating any cells from Old Row that has any different values 
            foreach (var cell in rowToBeUpdated.Descendants<Cell>())
            {
                string oldCellValue;
                string collumnIndex;
                GetCellValueAndColumn(sharedStrings, rowToBeUpdated, cell, out oldCellValue, out collumnIndex);

                var newValueFromCsv = string.Empty;
                if (membershipData.TryGetCsvNewValueForMatchingColumnIndex(collumnIndex, out newValueFromCsv)
                    &&
                    !newValueFromCsv.Equals(oldCellValue, StringComparison.InvariantCultureIgnoreCase))
                {
                    var newCellValue = new CellValue(newValueFromCsv);
                    cell.CellValue = newCellValue;
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                    updated = true;
                }
            }
            return updated;
        }

        private static void UpdateMembershipPropertiesWithColumnIndex(SharedStringTable sharedStrings, Row row, List<MembershipData> membershipDataList)
        {
            // For each header cell match to a column name for each properties
            // in Membership data object and update Column Index values
            foreach (Cell cell in row.Descendants<Cell>())
            {
                string cellValue;
                string columnIndex;
                GetCellValueAndColumn(sharedStrings, row, cell, out cellValue, out columnIndex);

                foreach (var membershipData in membershipDataList)
                {
                    membershipData.UpdateExcelColumnIndexInAllProperties(cellValue, columnIndex);
                }
            }
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
