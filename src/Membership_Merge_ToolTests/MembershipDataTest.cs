using Membership_Merge_Tool;
using Membership_Merge_Tool.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Membership_Merge_ToolTests
{
    [TestClass]
    public class MembershipDataTest
    {
        [TestMethod]
        public void MembershipData_FromDataValuesWithQuotes_Success()
        {
            var testRecordRow = @"FirstName,LastName,""January 1, 1974"",Mama,Nadya,""February 2, 1974"",""123 Lapaivka Street"",Lapaivka,WA,USA,98100,425-123-3456,markiyan27test@hotmail.com,,,,Yes,,""Child 1"",""January 1, 2000"",Yes,No,,,No,No,,,No,No,,,No,No,,,No,No,5,""2018 - 02 - 10 11:19:13"",""2018 - 02 - 10 11:19:25""";
            var testData = ClassInstanceFactory.GetMembershipData(testRecordRow);

            Assert.AreEqual("FirstName", testData.FirstName);
            Assert.AreEqual("LastName", testData.LastName);
            Assert.AreEqual(DateTime.Parse("1/1/1974"), testData.DateOfBirth);
            Assert.AreEqual("markiyan27test@hotmail.com", testData.Email);
            Assert.AreEqual(1, testData.Children.Count);
            Assert.AreEqual("Child 1", testData.Children.FirstOrDefault().ChildName);
        }

        [TestMethod]
        public void MembershipData_EmptyValues_Success()
        {
            var testRecordRow = @",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            var testData = ClassInstanceFactory.GetMembershipData(testRecordRow);

            Assert.IsNull(testData);
        }
    }
}
