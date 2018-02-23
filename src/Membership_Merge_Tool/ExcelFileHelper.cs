using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Membership_Merge_Tool.Enumerations;
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
        public static int MergeInputDataIntoExcelFile(string excelFileName, List<MembershipDataRow> inputDataList)
        {
            int updatedRows = 0;
            var headerMappingOfColumnNames = new List<MembershipDataCellMapper>();

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
                        updatedRows = UpdateWorksheetRowFromMembershipData(sharedStrings, headerMappingOfColumnNames, row, inputDataList);
                    }
                    isHeader = false;                    
                }
                if (updatedRows > 0)
                {
                    // Save the worksheet.
                    firstSheetPart.Worksheet.Save();

                    // for recacluation of formula
                    document.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
                    document.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;
                }
            }
            return updatedRows;
        }

        private static int UpdateWorksheetRowFromMembershipData(SharedStringTable sharedStrings, List<MembershipDataCellMapper> columnMapperList, Row row, List<MembershipDataRow> inputDataList)
        {
            int updatedRows = 0;
            var cellValue = string.Empty;
            var collumnIndex = string.Empty;
            var rowIndex = row.RowIndex.ToString();
            try
            {
                var currentCellMapping = new List<MembershipDataCellMapper>();

                // First collecting current old data for each cell for update
                foreach (var cell in row.Descendants<Cell>())
                {
                    cellValue = collumnIndex = string.Empty;
                    GetCellValueAndColumn(sharedStrings, row, cell, out cellValue, out collumnIndex);

                    // If there's any mapping record for this specific cell column
                    var mapping = columnMapperList.Where(m => m.ExcelFileColumnIndex == collumnIndex).FirstOrDefault();
                    mapping = mapping == null
                        ? new MembershipDataCellMapper()
                        : mapping;
                    mapping.ExcelCellOldValue = cellValue;
                    currentCellMapping.Add(mapping);
                };

                // Next compare current values and update them if needed
                var newRow = new Row();
                //if (currentCellMapping.Any() && TryGetUpdatedRow(sharedStrings, row, currentCellMapping, inputDataList, out newRow))
                //{
                //    row = newRow;
                //    updatedRows++;
                //}
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
        /// Return true if able to return updated row in out parameter.
        /// Updated row returned if UpdateDate field of is greater then in the old Row.
        /// </summary>
        private static bool TryGetUpdatedRow(SharedStringTable sharedStrings, Row oldRow, List<MembershipDataCellMapper> currentCellMapping, List<MembershipDataRow> inputDataList, out Row newRow)
        {
            var oldKeyCell = currentCellMapping
                .Where(m => !string.IsNullOrWhiteSpace(m.ExcelFileColumnIndex)) // with File Index Column
                .Where(m => !string.IsNullOrWhiteSpace(m.ExcelFileColumnName)) // with File Column Name
                .Where(m => m.MembershipDataPropertyName == MembershipDataProperty.Email)
                .FirstOrDefault();
            newRow = new Row();

            // Exiting, If key cell is empty
            if (oldKeyCell == null || string.IsNullOrWhiteSpace(oldKeyCell.ExcelCellOldValue))
            {
                return false;
            }

            // First check if there is new data for specific email
            //var newData = inputDataList.Where(m => m.Email.Equals(oldKeyCell.ExcelCellOldValue));
            //if (!newData.Any())
            //{
            //    return false;
            //}

            // Finally updating any cells from Old Row that has any values in the mapping
            //foreach (var oldCell in oldRow.Descendants<Cell>())
            //{
            //    string oldCellValue;
            //    string collumnIndex;
            //    GetCellValueAndColumn(sharedStrings, oldRow, oldCell, out oldCellValue, out collumnIndex);

            //    var map = currentCellMapping.Where(m => m.ExcelFileColumnIndex == collumnIndex);
            //    if (map != null && map.Any())
            //    {
            //        var newCellValue = GetCellValueForMembershipProperty(map.FirstOrDefault().MembershipDataPropertyName, newData.FirstOrDefault(), oldCell.CellValue);
            //        oldCell.CellValue = newCellValue == null ? oldCell.CellValue : newCellValue;
            //    }

            //}
            return true;
        }

        /// <summary>
        /// Return new CellValue based on the MembershipData property name
        /// </summary>
        private static CellValue GetCellValueForMembershipProperty(string membershipDataPropertyName, MembershipDataRow newData, CellValue oldCellValue)
        {
            CellValue returnCell = null;
            var newPropertyValue = newData.GetType().GetProperty(membershipDataPropertyName).GetValue(newData, null);            
            if (newPropertyValue != null && !string.IsNullOrWhiteSpace(newPropertyValue.ToString()))
            {
                returnCell = new CellValue(newPropertyValue.ToString());

                // If old and new cell values are the same, return null so it will not be updated
                if (oldCellValue == returnCell)
                {
                    return null;
                }
            }
            return returnCell;
        }

        private static List<MembershipDataCellMapper> GetMembershipColumnMapper(SharedStringTable sharedStrings, Row row, MembershipDataRow membershipData)
        {
            var returnList = new List<MembershipDataCellMapper>();

            // Firstly get all the properties from MembershipData except Children List
            // using reflection to create a list of existing Doc properties and their values
            //foreach (var membershipProperty in membershipData.GetType().GetProperties()
            //    .Where(p => p.PropertyType != typeof(List<ChildData>)).ToList())
            //{                
            //    var descriptionAttribute = (DescriptionAttribute)membershipProperty.GetCustomAttributes(false).FirstOrDefault();

            //    var membershipColumnMapper = new MembershipDataCellMapper { MembershipDataPropertyName = membershipProperty.Name };
            //    if (descriptionAttribute != null && !string.IsNullOrWhiteSpace(descriptionAttribute.Description))
            //    {
            //        membershipColumnMapper.ExcelFileColumnName = descriptionAttribute.Description;
            //    }
            //    else // If no Description Attribute defined, will use Property name, meaning Property Name should Match Column Name in Excel
            //    {
            //        membershipColumnMapper.ExcelFileColumnName = membershipProperty.Name;
            //    }
            //    returnList.Add(membershipColumnMapper);
            //}

            // Secondly get all the properties from Child Data for 5 children
            // using reflection to create a list of existing Doc properties and their values
            // Make any test child data to get prop.names and descr.
            //var childData = new ChildData { ChildName = "NotRealChild", DateOfBirth = DateTime.Now};
            //for (int childIndex = 1; childIndex < 6; childIndex++)
            //{
            //    foreach (var childProperty in childData.GetType().GetProperties())
            //    {
            //        var descriptionAttribute = (DescriptionAttribute)childProperty.GetCustomAttributes(false).FirstOrDefault();
            //        var childDescription = descriptionAttribute.Description.Replace("#", childIndex.ToString());

            //        var membershipColumnMapper = new MembershipDataCellMapper
            //            { MembershipDataPropertyName = childProperty.Name, ExcelFileColumnName = childDescription };

            //        returnList.Add(membershipColumnMapper);
            //    }
            //}
           
            // Next match to a column name from the header row and gets Column index of the matched one
            foreach (Cell cell in row.Descendants<Cell>())
            {
                string cellValue;
                string collumnIndex;
                GetCellValueAndColumn(sharedStrings, row, cell, out cellValue, out collumnIndex);

                var toUpdate = returnList
                    .SingleOrDefault(i => i.ExcelFileColumnName.Equals(cellValue, StringComparison.InvariantCultureIgnoreCase));

                // In case of non existent column name, try to replace new line chars with space and try again
                if (toUpdate == null)
                {
                    cellValue = cellValue.Replace("\n", " ");
                    toUpdate = returnList
                    .SingleOrDefault(i => i.ExcelFileColumnName.Equals(cellValue, StringComparison.InvariantCultureIgnoreCase));
                }

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
