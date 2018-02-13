using System;
using System.Collections.Generic;

namespace Membership_Merge_Tool.Models
{
    public class MembershipData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseLastName { get; set; }
        public DateTime? SpouseDateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SpouseEmail { get; set; }
        public string CellPhone { get; set; }
        public string SpouseCellPhone { get; set; }
        public bool IncludeInMailingList { get; set; }
        public int EnvelopeNumber { get; set; }
        
        public List<ChildData> Children { get; set; }

        public MembershipData(string[] values)
        {
            FirstName   = values[0];
            LastName    = values[1];

            DateTime inputDateOfBirth;
            if (DateTime.TryParse(values[2], out inputDateOfBirth))
            {
                DateOfBirth = inputDateOfBirth;
            }

            SpouseFirstName = values[3];
            SpouseLastName  = values[4];

            DateTime inputSpouseDateOfBirth;
            if (DateTime.TryParse(values[5], out inputSpouseDateOfBirth))
            {
                SpouseDateOfBirth = inputSpouseDateOfBirth;
            }

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
            IncludeInMailingList = string.IsNullOrWhiteSpace(values[16]) ? true : bool.Parse(values[16]);

            int inputEnvelopeNumber;
            if (int.TryParse(values[17], out inputEnvelopeNumber))
            {
                EnvelopeNumber = inputEnvelopeNumber;
            }

            Children = ParseChildrenData(values, 18);
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

            for (int i = 0; i < values.Length; i++)
            {
                if (i >= startingIndex && isNewChildStartColumn && !string.IsNullOrWhiteSpace(values[i]))
                {
                    // Child data are set in 4 columns set
                    var childData = new ChildData {
                        ChildName = values[i],
                        Baptized = bool.Parse(values[i+2]),
                        FirstCommunionReceived= bool.Parse(values[i + 3]),
                        };

                    DateTime inputChildDateOfBirth;
                    if (DateTime.TryParse(values[i+1], out inputChildDateOfBirth))
                    {
                        childData.DateOfBirth = inputChildDateOfBirth;
                    }
                    returnChildrenList.Add(childData);
                    isNewChildStartColumn = false;
                    skipColumnCount = 0;
                }

                // If found first child data, skip next 3 columns
                // to read next child data
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
            }

            return returnChildrenList;
        }
    }
}
