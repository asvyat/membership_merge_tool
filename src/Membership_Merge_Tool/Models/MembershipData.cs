using Membership_Merge_Tool.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Membership_Merge_Tool.Models
{
    public class MembershipData
    {
        public MembershipDataValues FirstName { get; set; } = new MembershipDataValues();
        public MembershipDataValues LastName { get; set; } = new MembershipDataValues();

        public MembershipDataValues DateOfBirth { get; set; } = new MembershipDataValues();
        public MembershipDataValues SpouseFirstName { get; set; } = new MembershipDataValues();
        public MembershipDataValues SpouseLastName { get; set; } = new MembershipDataValues();
        public MembershipDataValues SpouseDateOfBirth { get; set; } = new MembershipDataValues();
        public MembershipDataValues Address { get; set; } = new MembershipDataValues();
        public MembershipDataValues City { get; set; } = new MembershipDataValues();
        public MembershipDataValues State { get; set; } = new MembershipDataValues();
        public MembershipDataValues Zip { get; set; } = new MembershipDataValues();
        public MembershipDataValues Country { get; set; } = new MembershipDataValues();
        public MembershipDataValues Phone { get; set; } = new MembershipDataValues();
        public MembershipDataValues Email { get; set; } = new MembershipDataValues();
        public MembershipDataValues CellPhone { get; set; } = new MembershipDataValues();
        public MembershipDataValues SpouseEmail { get; set; } = new MembershipDataValues();
        public MembershipDataValues SpouseCellPhone { get; set; } = new MembershipDataValues();
        public MembershipDataValues Pager { get; set; } = new MembershipDataValues();
        public MembershipDataValues IncludeInMailingList { get; set; } = new MembershipDataValues();
        public MembershipDataValues EnvelopeNumber { get; set; } = new MembershipDataValues();
        public MembershipDataValues UpdateDate { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child1Name { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child1Dob { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child1Baptized { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child1FirstCommunionReceived { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child2Name { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child2Dob { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child2Baptized { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child2FirstCommunionReceived { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child3Name { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child3Dob { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child3Baptized { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child3FirstCommunionReceived { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child4Name { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child4Dob { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child4Baptized { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child4FirstCommunionReceived { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child5Name { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child5Dob { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child5Baptized { get; set; } = new MembershipDataValues();
        public MembershipDataValues Child5FirstCommunionReceived { get; set; } = new MembershipDataValues();

        public MembershipData()
        {
            InitializeMembershipData();            
        }
        
        public MembershipData(string[] values)
        {
            InitializeMembershipData();

            // Add all the fields
            FirstName.CsvNewValue = values[0];
            LastName.CsvNewValue = values[1];
            DateOfBirth.CsvNewValue = values[2];
            SpouseFirstName.CsvNewValue = values[3];
            SpouseLastName.CsvNewValue = values[4];
            SpouseDateOfBirth.CsvNewValue = values[5];
            Address.CsvNewValue = values[6];
            City.CsvNewValue = values[7];
            State.CsvNewValue = values[8];
            Country.CsvNewValue = values[9];
            Zip.CsvNewValue = values[10];
            Phone.CsvNewValue = values[11];
            Email.CsvNewValue = values[12];
            SpouseEmail.CsvNewValue = values[13];
            CellPhone.CsvNewValue = values[14];
            SpouseCellPhone.CsvNewValue = values[15];
            Pager.CsvNewValue = values[16];
            IncludeInMailingList.CsvNewValue = ValueHelper.ParseStringToBool(values[17]).ToString();
            EnvelopeNumber.CsvNewValue = values[18];

            Child1Name.CsvNewValue = values[19];
            Child1Dob.CsvNewValue = values[20];
            Child1Baptized.CsvNewValue = ValueHelper.ParseStringToBool(values[21]).ToString();
            Child1FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBool(values[22]).ToString();

            Child2Name.CsvNewValue = values[23];
            Child2Dob.CsvNewValue = values[24];
            Child2Baptized.CsvNewValue = ValueHelper.ParseStringToBool(values[25]).ToString();
            Child2FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBool(values[26]).ToString();

            Child3Name.CsvNewValue = values[27];
            Child3Dob.CsvNewValue = values[28];
            Child3Baptized.CsvNewValue = ValueHelper.ParseStringToBool(values[29]).ToString();
            Child3FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBool(values[30]).ToString();

            Child4Name.CsvNewValue = values[31];
            Child4Dob.CsvNewValue = values[32];
            Child4Baptized.CsvNewValue = ValueHelper.ParseStringToBool(values[33]).ToString();
            Child4FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBool(values[34]).ToString();

            Child5Name.CsvNewValue = values[35];
            Child5Dob.CsvNewValue = values[36];
            Child5Baptized.CsvNewValue = ValueHelper.ParseStringToBool(values[37]).ToString();
            Child5FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBool(values[38]).ToString();
            
            UpdateDate.CsvNewValue = ValueHelper.ParseStringToDateTime(values[40]).ToString();
        }

        /// <summary>
        /// Update Excel Column Index Value for each of the Properties in this Data object
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnIndex"></param>
        public void UpdateExcelColumnIndexForAllProperties(string columnName, string columnIndex)
        {
            foreach (var membershipProperty in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var membershipValue = (MembershipDataValues)membershipProperty.GetValue(this);

                if (membershipValue.ExcelFileColumnName.Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    membershipValue.ExcelFileColumnIndex = columnIndex;                    
                }
            }
        }

        private void InitializeMembershipData()
        {           
            // Initialize all the data cells with Excel column Names
            FirstName.ExcelFileColumnName = "First";
            LastName.ExcelFileColumnName = "Last";
            DateOfBirth.ExcelFileColumnName = "DOB";
            SpouseFirstName.ExcelFileColumnName = "Spouse First";
            SpouseLastName.ExcelFileColumnName = "Spouse Last";
            SpouseDateOfBirth.ExcelFileColumnName = "Spouse DOB";
            Address.ExcelFileColumnName = "Address";
            City.ExcelFileColumnName = "City";
            State.ExcelFileColumnName = "State";
            Zip.ExcelFileColumnName = "Zip";
            Country.ExcelFileColumnName = "Country";
            Phone.ExcelFileColumnName = "Phone";
            Email.ExcelFileColumnName = "Email";
            CellPhone.ExcelFileColumnName = "Cell";
            SpouseEmail.ExcelFileColumnName = "Spouse Email";
            SpouseCellPhone.ExcelFileColumnName = "Spouse Cell";
            Pager.ExcelFileColumnName = "Pager";
            IncludeInMailingList.ExcelFileColumnName = "Bulletin e-mail";
            EnvelopeNumber.ExcelFileColumnName = "Env";
            UpdateDate.ExcelFileColumnName = "Update Date";
            Child1Name.ExcelFileColumnName = "Child 1 Name";
            Child1Dob.ExcelFileColumnName = "Child 1 DOB";
            Child1Baptized.ExcelFileColumnName = "Child 1 Baptized";
            Child1FirstCommunionReceived.ExcelFileColumnName = "Child 1 First Communion";
            Child2Name.ExcelFileColumnName = "Child 2 Name";
            Child2Dob.ExcelFileColumnName = "Child 2 DOB";
            Child2Baptized.ExcelFileColumnName = "Child 2 Baptized";
            Child2FirstCommunionReceived.ExcelFileColumnName = "Child 2 First Communion";
            Child3Name.ExcelFileColumnName = "Child 3 Name";
            Child3Dob.ExcelFileColumnName = "Child 3 DOB";
            Child3Baptized.ExcelFileColumnName = "Child 3 Baptized";
            Child3FirstCommunionReceived.ExcelFileColumnName = "Child 3 First Communion";
            Child4Name.ExcelFileColumnName = "Child 4 Name";
            Child4Dob.ExcelFileColumnName = "Child 4 DOB";
            Child4Baptized.ExcelFileColumnName = "Child 4 Baptized";
            Child4FirstCommunionReceived.ExcelFileColumnName = "Child 4 First Communion";
            Child5Name.ExcelFileColumnName = "Child 5 Name";
            Child5Dob.ExcelFileColumnName = "Child 5 DOB";
            Child5Baptized.ExcelFileColumnName = "Child 5 Baptized";
            Child5FirstCommunionReceived.ExcelFileColumnName = "Child 5 First Communion";
        }
    }
}
