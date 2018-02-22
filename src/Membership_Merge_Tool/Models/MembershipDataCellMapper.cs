using System;
using Membership_Merge_Tool.Enumerations;

namespace Membership_Merge_Tool.Models
{
    /// <summary>
    /// For each MembershipData data property we need to map 
    /// to the actual Header column name in Excel file
    /// for example: MembershipData.FirstName map to ExcelFileHeaderRow.First_Name and Column Index
    /// This class will hold this mapping for each property of the MembershipData class
    /// </summary>
    public class MembershipDataCellMapper
    {
        public MembershipDataProperty MembershipDataPropertyName { get; set; }

        public string ExcelFileColumnName { get; set; } = string.Empty;
        public string ExcelFileColumnIndex { get; set; } = string.Empty;
        public string ExcelCellOldValue { get; set; } = string.Empty;
        public string CsvNewValue { get; set; } = string.Empty;        
    }
}
