using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Membership_Merge_Tool.Models
{
    public class MembershipData
    {
        //const string MembershipDataDescriptionCategory = "ExcelFileColumnName";

        //[Description("First"), Category(MembershipDataDescriptionCategory)]
        [Description("First")]
        public string FirstName { get; set; }

        //[Description("Last"), Category(MembershipDataDescriptionCategory)]
        public string LastName { get; set; }

        //[Description("DOB"), Category(MembershipDataDescriptionCategory)]
        public DateTime? DateOfBirth { get; set; }

        //[Description("Spouse First"), Category(MembershipDataDescriptionCategory)]
        public string SpouseFirstName { get; set; }

        public string SpouseLastName { get; set; }

        public DateTime? SpouseDateOfBirth { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int? Zip { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string CellPhone { get; set; }

        public string SpouseEmail { get; set; }

        public string SpouseCellPhone { get; set; }

        public string Pager { get; set; }   // TODO : add into a web sit form as well

        public bool IncludeInMailingList { get; set; }

        public int EnvelopeNumber { get; set; }

        public List<ChildData> Children { get; set; }

        public DateTime? UpdateDate { get; set; }

        public MembershipData() {}

        public MembershipData(string[] values)
        {
            // Add all the fields
            FirstName   = values[0];
            LastName    = values[1];
            DateOfBirth = ValueHelper.ParseStringToDateTime(values[2]);
            SpouseFirstName = values[3];
            SpouseLastName  = values[4];
            SpouseDateOfBirth = ValueHelper.ParseStringToDateTime(values[5]);            
            Address     = values[6];
            City        = values[7];
            State       = values[8];
            Country     = values[9];

            int inputZip;
            if (int.TryParse(values[10], out inputZip))
            {
                Zip = inputZip;
            }

            Phone       = values[11];
            Email       = values[12];
            SpouseEmail = values[13];
            CellPhone   = values[14];
            SpouseCellPhone = values[15];
            IncludeInMailingList = ValueHelper.ParseStringToBool(values[16]);

            int inputEnvelopeNumber;
            if (int.TryParse(values[17], out inputEnvelopeNumber))
            {
                EnvelopeNumber = inputEnvelopeNumber;
            }

            Children = ParseChildrenData(values, 18);
            UpdateDate = ValueHelper.ParseStringToDateTime(values[40]);
        }
        
        /// <summary>
        /// Parse last elements of the values from starting Index position
        /// </summary>
        /// <returns>List of parsed Child data objects</returns>
        public List<ChildData> ParseChildrenData(string[] values, int startingIndex)
        {
            var returnChildrenList = new List<ChildData>();
            var isNewChildStartColumn = true;
            var skipColumnCount = 0;

            var maxNumberOfChildren = 5;
            var maxColumnIndexWithChildrenData = startingIndex - 1 +
                maxNumberOfChildren * 4;

            for (int i = 0; i < values.Length; i++)
            {
                // If found first child data, skip next 3 columns
                // to read next child data set
                if (!isNewChildStartColumn)
                {
                    if (skipColumnCount == 3)
                    {
                        isNewChildStartColumn = true;
                    }
                    else
                    {
                        skipColumnCount++;
                    }
                }

                // If first column with Child data found
                if (i >= startingIndex && isNewChildStartColumn 
                    && i <= maxColumnIndexWithChildrenData)
                {
                    var childName = values[i];
                    if (!string.IsNullOrEmpty(childName))
                    {
                        // Child data are set in 4 columns set
                        var childData = new ChildData
                        {
                            ChildName = childName,
                            Baptized = ValueHelper.ParseStringToBool(values[i + 2]),
                            FirstCommunionReceived = ValueHelper.ParseStringToBool(values[i + 3]),
                        };

                        childData.DateOfBirth = ValueHelper.ParseStringToDateTime(values[i + 1]);
                        returnChildrenList.Add(childData);
                    }
                    
                    isNewChildStartColumn = false;
                    skipColumnCount = 0;
                }
            }
            return returnChildrenList;
        }
    }
}
