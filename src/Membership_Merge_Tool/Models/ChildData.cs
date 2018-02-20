using System;
using System.ComponentModel;

namespace Membership_Merge_Tool.Models
{
    /// <summary>
    /// Child Data Class to store data extracted from CSV input files
    /// And later to update into Excel Master File
    /// Value in the Description Attribute should match the desired column name in Excel file
    /// # will be replaced with child index number, i.e. Child 1 Name
    /// </summary>
    public class ChildData
    {
        [Description("Child # Name")]
        public string ChildName { get; set; }

        [Description("Child # DOB")]
        public DateTime? DateOfBirth { get; set; }

        [Description("Child # Baptized")]
        public bool Baptized { get; set; }

        [Description("Child # First Communion")]
        public bool FirstCommunionReceived { get; set; }
    }
}
