using Microsoft.VisualStudio.TestTools.UnitTesting;
using Membership_Merge_Tool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership_Merge_Tool.Models.Tests
{
    [TestClass()]
    public class MembershipDataTests
    {
        [TestMethod()]
        public void ContainsNotMatchingOldAndNewValues_WithoutExcelOldValues_Success_Test()
        {
            var testRecord = new MembershipData();
            testRecord.FirstName.CsvNewValue = "testFirstName";
            testRecord.LastName.CsvNewValue = "testLastName";
            testRecord.Email.CsvNewValue = "testEmail";

            Assert.IsTrue(testRecord.ContainsAnyNotMatchingOldAndNewValues());
        }

        [TestMethod()]
        public void ContainsNotMatchingOldAndNewValues_WithExcelOldValues_NotTheSame_Success_Test()
        {
            var testRecord = new MembershipData();
            testRecord.FirstName.CsvNewValue = "testFirstName";
            testRecord.LastName.CsvNewValue = "testLastName";
            testRecord.Email.CsvNewValue = "testEmail";
            testRecord.FirstName.ExcelCellOldValue = "oldFirstName";

            Assert.IsTrue(testRecord.ContainsAnyNotMatchingOldAndNewValues());
        }

        [TestMethod()]
        public void ContainsNotMatchingOldAndNewValues_WithExcelOldValues_TheSame_Success_Test()
        {
            var testRecord = new MembershipData();
            testRecord.FirstName.CsvNewValue = "testFirstName";
            testRecord.FirstName.ExcelCellOldValue = "testFirstName";
            testRecord.Email.CsvNewValue = "testEmail";            
            testRecord.Email.ExcelCellOldValue = "testEmail";

            Assert.IsFalse(testRecord.ContainsAnyNotMatchingOldAndNewValues());
        }

        [TestMethod]
        public void MembershipData_FromDataValuesWithQuotes_Success()
        {
            var testRecordRow = @"FirstName,LastName,""January 1, 1974"",Mama,Nadya,""February 2, 1974"",""123 Lapaivka Street"",Lapaivka,WA,USA,98100,425-123-3456,testEmail@hotmail.com,,,,,Yes,""Child 1"",""January 1, 2000"",Yes,No,,,No,No,,,No,No,,,No,No,,,No,No,5,""2018-02-10 11:19:13"",""2018-02-10 11:19:25""";
            var testDataRow = MembershipHelper.GetMembershipDataRow(testRecordRow);

            Assert.AreEqual("FirstName", testDataRow.FirstName.CsvNewValue);
            Assert.AreEqual("LastName", testDataRow.LastName.CsvNewValue);
            Assert.AreEqual(DateTime.Parse("1/1/1974"), DateTime.Parse(testDataRow.DateOfBirth.CsvNewValue));
            Assert.AreEqual("testEmail@hotmail.com", testDataRow.Email.CsvNewValue);
            Assert.AreEqual("Child 1", testDataRow.Child1Name.CsvNewValue);
        }

        [TestMethod]
        public void MembershipData_EmptyValues_Success()
        {
            var testRecordRow = @",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            var testData = MembershipHelper.GetMembershipDataRow(testRecordRow);

            Assert.IsNull(testData);
        }        
    }
}