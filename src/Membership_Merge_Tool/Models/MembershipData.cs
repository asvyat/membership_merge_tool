using System;
using System.Reflection;

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
            IncludeInMailingList.CsvNewValue = ValueHelper.ParseStringToBoolString(values[17]);

            Child1Name.CsvNewValue = values[18];
            Child1Dob.CsvNewValue = values[19];
            Child1Baptized.CsvNewValue = ValueHelper.ParseStringToBoolString(values[20], Child1Name.CsvNewValue);
            Child1FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBoolString(values[21], Child1Name.CsvNewValue);

            Child2Name.CsvNewValue = values[22];
            Child2Dob.CsvNewValue = values[23];
            Child2Baptized.CsvNewValue = ValueHelper.ParseStringToBoolString(values[24], Child2Name.CsvNewValue);
            Child2FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBoolString(values[25], Child2Name.CsvNewValue);

            Child3Name.CsvNewValue = values[26];
            Child3Dob.CsvNewValue = values[27];
            Child3Baptized.CsvNewValue = ValueHelper.ParseStringToBoolString(values[28], Child3Name.CsvNewValue);
            Child3FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBoolString(values[29], Child3Name.CsvNewValue);

            Child4Name.CsvNewValue = values[30];
            Child4Dob.CsvNewValue = values[31];
            Child4Baptized.CsvNewValue = ValueHelper.ParseStringToBoolString(values[32], Child4Name.CsvNewValue);
            Child4FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBoolString(values[33], Child4Name.CsvNewValue);

            Child5Name.CsvNewValue = values[34];
            Child5Dob.CsvNewValue = values[35];
            Child5Baptized.CsvNewValue = ValueHelper.ParseStringToBoolString(values[36], Child5Name.CsvNewValue);
            Child5FirstCommunionReceived.CsvNewValue = ValueHelper.ParseStringToBoolString(values[37], Child5Name.CsvNewValue);
            
            UpdateDate.CsvNewValue = ValueHelper.ParseStringToDateTime(values[39]).ToString();
        }

        /// <summary>
        /// To clone Excel Column Index values from another Membership data for each of the Properties in this Data object
        /// </summary>
        public void CloneExcelColumnIndexInAllProperties(MembershipData anotherMembershipData)
        {
            foreach (var anotherMembershipProperty in anotherMembershipData.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var anotherMembershipValue = (MembershipDataValues)anotherMembershipProperty.GetValue(anotherMembershipData);

                // verify and match Column name for each data member
                foreach (var membershipProperty in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var membershipValue = (MembershipDataValues)membershipProperty.GetValue(this);
                    if (anotherMembershipValue.ExcelFileColumnName.Equals(membershipValue.ExcelFileColumnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        membershipValue.ExcelFileColumnIndex = anotherMembershipValue.ExcelFileColumnIndex;
                    }
                }
            }
        }

        /// <summary>
        /// To clone Csv New Value from another Membership data for each of the Properties in this Data object
        /// </summary>
        public void CloneCsvNewValueInAllProperties(MembershipData anotherMembershipData)
        {
            foreach (var anotherMembershipProperty in anotherMembershipData.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var anotherMembershipValue = (MembershipDataValues)anotherMembershipProperty.GetValue(anotherMembershipData);

                // verify and match Column name for each data member
                foreach (var membershipProperty in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var membershipValue = (MembershipDataValues)membershipProperty.GetValue(this);
                    if (anotherMembershipValue.ExcelFileColumnName.Equals(membershipValue.ExcelFileColumnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        membershipValue.CsvNewValue = anotherMembershipValue.CsvNewValue;
                    }
                }
            }
        }

        /// <summary>
        /// Update Excel Column Index Value for each of the Properties in this Data object
        /// </summary>
        public void UpdateExcelColumnIndexInAllProperties(string columnName, string columnIndex)
        {
            foreach (var membershipProperty in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var membershipValue = (MembershipDataValues)membershipProperty.GetValue(this);

                if (membershipValue.ExcelFileColumnName.Equals(columnName, StringComparison.InvariantCultureIgnoreCase)
                    ||
                    membershipValue.ExcelFileColumnName.Equals(columnName.Replace("\n", " "), StringComparison.InvariantCultureIgnoreCase))
                {
                    membershipValue.ExcelFileColumnIndex = columnIndex;                    
                }
            }
        }

        /// <summary>
        /// Update Excel Cell Old Values for each of Properties in this Data object
        /// Each data property should match the same columnIndex
        /// </summary>
        public void UpdateExcelCellOldValueInAllProperties(string columnIndex, string cellValue)
        {
            foreach (var membershipProperty in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var membershipValue = (MembershipDataValues)membershipProperty.GetValue(this);

                if (membershipValue.ExcelFileColumnIndex.Equals(columnIndex, StringComparison.InvariantCultureIgnoreCase))
                {
                    membershipValue.ExcelCellOldValue = cellValue;
                }
            }
        }

        /// <summary>
        /// Verify if there are any Not Matching old Excel and new Csv values
        /// And if yes, return true
        /// </summary>
        public bool ContainsAnyNotMatchingOldAndNewValues()
        {
            var anyNotMatchingFound = false;
            foreach (var membershipProperty in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var membershipValue = (MembershipDataValues)membershipProperty.GetValue(this);
                if (!membershipValue.ExcelCellOldValue.Equals(membershipValue.CsvNewValue, StringComparison.InvariantCultureIgnoreCase))
                {
                    anyNotMatchingFound = true;
                }
            }            
            return anyNotMatchingFound;
        }

        /// <summary>
        /// Return CSV New Value for desired matching Column Index
        /// </summary>
        public bool TryGetCsvNewValueForMatchingColumnIndex(string desiredColumnIndex, out string csvNewValue)
        {
            var valueFoundForColumnIndex = false;
            csvNewValue = string.Empty;
            foreach (var membershipProperty in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var membershipValue = (MembershipDataValues)membershipProperty.GetValue(this);
                if (membershipValue.ExcelFileColumnIndex.Equals(desiredColumnIndex, StringComparison.InvariantCultureIgnoreCase))
                {
                    csvNewValue = membershipValue.CsvNewValue;
                    valueFoundForColumnIndex = true;
                }
            }
            return valueFoundForColumnIndex;
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
