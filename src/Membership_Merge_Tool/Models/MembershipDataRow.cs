using Membership_Merge_Tool.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership_Merge_Tool.Models
{
    public class MembershipDataRow
    {
        private string[] values;

        public List<MembershipDataCellMapper> MembershipDataCellList { get; set; }

        public MembershipDataRow()
        {
            MembershipDataCellList = new List<MembershipDataCellMapper>();

            // Initialize all the data cells
            AddEmptyDataItem(MembershipDataProperty.FirstName, "First");
            AddEmptyDataItem(MembershipDataProperty.LastName, "Last");
            AddEmptyDataItem(MembershipDataProperty.DateOfBirth, "DOB");
            AddEmptyDataItem(MembershipDataProperty.SpouseFirstName, "Spouse First");
            AddEmptyDataItem(MembershipDataProperty.SpouseLastName, "Spouse Last");
            AddEmptyDataItem(MembershipDataProperty.SpouseDateOfBirth, "Spouse DOB");

        }

        public MembershipDataRow(string[] values)
        {
            // Add all the fields
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.FirstName, values[0]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.LastName, values[1]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.DateOfBirth, values[2]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.SpouseFirstName, values[3]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.SpouseLastName, values[4]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.SpouseDateOfBirth, values[5]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Address, values[6]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.City, values[7]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.State, values[8]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Country, values[9]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Zip, values[10]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Phone, values[11]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Email, values[12]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.SpouseEmail, values[13]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.CellPhone, values[14]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.SpouseCellPhone, values[15]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.IncludeInMailingList, ValueHelper.ParseStringToBool(values[16]).ToString());
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.EnvelopeNumber, values[17]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child1Name, values[18]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child1Dob, values[19]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child1Baptized, ValueHelper.ParseStringToBool(values[20]).ToString());
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child1FirstCommunionReceived, ValueHelper.ParseStringToBool(values[21]).ToString());

            UpdateDataItemWithCsvNewValue(MembershipDataProperty.UpdateDate, ValueHelper.ParseStringToDateTime(values[40]).ToString());

        }

        public IEnumerable<string> GetCsvNewValuesForDataProperty(MembershipDataProperty property)
        {
            return MembershipDataCellList.Where(c => c.MembershipDataPropertyName == property)
                .Select(c => c.CsvNewValue).ToList();
        }

        public void UpdateDataItemWithCsvNewValue(MembershipDataProperty property, string csvNewValue)
        {
            MembershipDataCellList.SingleOrDefault(p => p.MembershipDataPropertyName == property).CsvNewValue = csvNewValue;
        }

        private void AddEmptyDataItem(MembershipDataProperty property, string excelFileColumn)
        {
            MembershipDataCellList.Add(new MembershipDataCellMapper
            {
                MembershipDataPropertyName = property,
                ExcelFileColumnName = excelFileColumn
            });
        }
    }
}
