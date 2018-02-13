using System;

namespace Membership_Merge_Tool.Models
{
    public class ChildData
    {
        public string ChildName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Baptized { get; set; }
        public bool FirstCommunionReceived { get; set; }
    }
}
