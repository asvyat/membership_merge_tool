using System.ComponentModel;

namespace Membership_Merge_Tool.Enumerations
{
    /// <summary>
    /// Configuration variable names stored in App.config
    /// </summary>
    public enum ConfigVariableName
    {
        FolderName_Updates,
        FolderName_Completed,
        RootFolder,
        UpdateFileNamePattern,
        MasterExcelFileName
    }
    
    /// <summary>
    /// Membership data property names
    /// </summary>
    public enum MembershipDataProperty
    {
        FirstName,
        LastName,
        DateOfBirth,
        SpouseFirstName,
        SpouseLastName,
        SpouseDateOfBirth,
        Address,
        City,
        State,
        Zip,
        Country,
        Phone,
        Email,
        CellPhone,
        SpouseEmail,
        SpouseCellPhone,
        Pager,
        IncludeInMailingList,
        EnvelopeNumber,
        UpdateDate,
        Child1Name,
        Child1Dob,
        Child1Baptized,
        Child1FirstCommunionReceived,
        Child2Name,
        Child2Dob,
        Child2Baptized,
        Child2FirstCommunionReceived,
        Child3Name,
        Child3Dob,
        Child3Baptized,
        Child3FirstCommunionReceived,
        Child4Name,
        Child4Dob,
        Child4Baptized,
        Child4FirstCommunionReceived,
        Child5Name,
        Child5Dob,
        Child5Baptized,
        Child5FirstCommunionReceived
    }
}
