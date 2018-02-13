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
        public int Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SpouseEmail { get; set; }
        public string CellPhone { get; set; }
        public string SpouseCellPhone { get; set; }
        public bool IncludeInMailingList { get; set; }
        public int EnvelopeNumber { get; set; }
        
        public List<ChildData> Children { get; set; }        
    }
}
