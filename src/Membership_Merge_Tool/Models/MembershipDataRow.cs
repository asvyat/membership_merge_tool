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
            InitializeMembershipDataCellList();            
        }
        
        public MembershipDataRow(string[] values)
        {
            InitializeMembershipDataCellList();

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

            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child2Name, values[22]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child2Dob, values[23]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child2Baptized, ValueHelper.ParseStringToBool(values[24]).ToString());
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child2FirstCommunionReceived, ValueHelper.ParseStringToBool(values[25]).ToString());

            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child3Name, values[26]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child3Dob, values[27]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child3Baptized, ValueHelper.ParseStringToBool(values[28]).ToString());
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child3FirstCommunionReceived, ValueHelper.ParseStringToBool(values[29]).ToString());

            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child4Name, values[30]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child4Dob, values[31]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child4Baptized, ValueHelper.ParseStringToBool(values[32]).ToString());
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child4FirstCommunionReceived, ValueHelper.ParseStringToBool(values[33]).ToString());

            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child5Name, values[34]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child5Dob, values[35]);
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child5Baptized, ValueHelper.ParseStringToBool(values[36]).ToString());
            UpdateDataItemWithCsvNewValue(MembershipDataProperty.Child5FirstCommunionReceived, ValueHelper.ParseStringToBool(values[37]).ToString());
            
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

        private void InitializeMembershipDataCellList()
        {
            MembershipDataCellList = new List<MembershipDataCellMapper>();

            // Initialize all the data cells with Excel column Names
            AddEmptyDataItem(MembershipDataProperty.FirstName, "First");
            AddEmptyDataItem(MembershipDataProperty.LastName, "Last");
            AddEmptyDataItem(MembershipDataProperty.DateOfBirth, "DOB");
            AddEmptyDataItem(MembershipDataProperty.SpouseFirstName, "Spouse First");
            AddEmptyDataItem(MembershipDataProperty.SpouseLastName, "Spouse Last");
            AddEmptyDataItem(MembershipDataProperty.SpouseDateOfBirth, "Spouse DOB");
            AddEmptyDataItem(MembershipDataProperty.Address, "Address");
            AddEmptyDataItem(MembershipDataProperty.City, "City");
            AddEmptyDataItem(MembershipDataProperty.State, "State");
            AddEmptyDataItem(MembershipDataProperty.Country, "Country");
            AddEmptyDataItem(MembershipDataProperty.Zip, "Zip");
            AddEmptyDataItem(MembershipDataProperty.Phone, "Phone");
            AddEmptyDataItem(MembershipDataProperty.Email, "Email");
            AddEmptyDataItem(MembershipDataProperty.SpouseEmail, "Spouse Email");
            AddEmptyDataItem(MembershipDataProperty.CellPhone, "Cell");
            AddEmptyDataItem(MembershipDataProperty.SpouseCellPhone, "Spouse Cell");
            AddEmptyDataItem(MembershipDataProperty.Pager, "Pager");
            AddEmptyDataItem(MembershipDataProperty.IncludeInMailingList, "Bulletin e-mail");
            AddEmptyDataItem(MembershipDataProperty.EnvelopeNumber, "Env");
            AddEmptyDataItem(MembershipDataProperty.Child1Name, "Child 1 Name");
            AddEmptyDataItem(MembershipDataProperty.Child1Dob, "Child 1 DOB");
            AddEmptyDataItem(MembershipDataProperty.Child1Baptized, "Child 1 Baptized");
            AddEmptyDataItem(MembershipDataProperty.Child1FirstCommunionReceived, "Child 1 First Communion");
            AddEmptyDataItem(MembershipDataProperty.Child2Name, "Child 2 Name");
            AddEmptyDataItem(MembershipDataProperty.Child2Dob, "Child 2 DOB");
            AddEmptyDataItem(MembershipDataProperty.Child2Baptized, "Child 2 Baptized");
            AddEmptyDataItem(MembershipDataProperty.Child2FirstCommunionReceived, "Child 2 First Communion");
            AddEmptyDataItem(MembershipDataProperty.Child3Name, "Child 3 Name");
            AddEmptyDataItem(MembershipDataProperty.Child3Dob, "Child 3 DOB");
            AddEmptyDataItem(MembershipDataProperty.Child3Baptized, "Child 3 Baptized");
            AddEmptyDataItem(MembershipDataProperty.Child3FirstCommunionReceived, "Child 3 First Communion");
            AddEmptyDataItem(MembershipDataProperty.Child4Name, "Child 4 Name");
            AddEmptyDataItem(MembershipDataProperty.Child4Dob, "Child 4 DOB");
            AddEmptyDataItem(MembershipDataProperty.Child4Baptized, "Child 4 Baptized");
            AddEmptyDataItem(MembershipDataProperty.Child4FirstCommunionReceived, "Child 4 First Communion");
            AddEmptyDataItem(MembershipDataProperty.Child5Name, "Child 5 Name");
            AddEmptyDataItem(MembershipDataProperty.Child5Dob, "Child 5 DOB");
            AddEmptyDataItem(MembershipDataProperty.Child5Baptized, "Child 5 Baptized");
            AddEmptyDataItem(MembershipDataProperty.Child5FirstCommunionReceived, "Child 5 First Communion");
            AddEmptyDataItem(MembershipDataProperty.UpdateDate, "Update Date");
        }
    }
}
