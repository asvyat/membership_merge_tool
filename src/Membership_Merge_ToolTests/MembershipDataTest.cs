using Membership_Merge_Tool.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Membership_Merge_ToolTests
{
    [TestClass]
    public class MembershipDataTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testRecordRow = @"Tato,Sasha,""January 1, 1974"",Mama,Nadya,""February 2, 1974"",""123 Lapaivka Street"",Lapaivka,WA,USA,98100,425-123-3456,markiyan27test@hotmail.com,,,,Yes,,""Child 1"",""January 1, 2000"",Yes,No,,,No,No,,,No,No,,,No,No,,,No,No,5,""2018 - 02 - 10 11:19:13"",""2018 - 02 - 10 11:19:25""";

            var testData = new MembershipData(testRecordRow.Split(','));
        }

        //private string[] SplitFields(string csvValue)
        //{
        //    //if there aren't quotes, use the faster function
        //    if (!csvValue.Contains('\"') && !csvValue.Contains('\''))
        //    {
        //        return csvValue.Trim(',').Split(',');
        //    }
        //    else
        //    {
        //        //there are quotes, use this built in text parser
        //        using (var csvParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(new StringReader(csvValue.Trim(','))))
        //        {
        //            csvParser.Delimiters = new string[] { "," };
        //            csvParser.HasFieldsEnclosedInQuotes = true;
        //            return csvParser.ReadFields();
        //        }
        //    }
        }
    }
}
