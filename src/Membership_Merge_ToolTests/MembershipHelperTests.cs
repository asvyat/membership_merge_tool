using Membership_Merge_Tool.Enumerations;
using Membership_Merge_Tool.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Membership_Merge_Tool.Tests
{
    [TestClass()]
    public class MembershipHelperTests
    {
        [TestMethod()]
        public void AddOnlyLatestMembershipData_NewRecordUpdated_Success_Test()
        {
            var testOldRecord = new MembershipData();
            testOldRecord.FirstName.CsvNewValue = "testFirstName";
            testOldRecord.LastName.CsvNewValue = "testLastName";
            testOldRecord.Email.CsvNewValue = "testEmail";
            testOldRecord.Address.CsvNewValue = "oldAddress";
            testOldRecord.UpdateDate.CsvNewValue = DateTime.Parse("02/15/18 10:00").ToString();
            
            var testRecordWithNewerDate = new MembershipData();
            testRecordWithNewerDate.FirstName.CsvNewValue = "testFirstName";
            testRecordWithNewerDate.LastName.CsvNewValue = "testLastName";
            testRecordWithNewerDate.Email.CsvNewValue = "testEmail";
            testRecordWithNewerDate.Address.CsvNewValue = "newAddress";
            testRecordWithNewerDate.UpdateDate.CsvNewValue = DateTime.Parse("02/15/18 11:00").ToString();
            
            var testList = new List<MembershipData> { testOldRecord };
            MembershipHelper.AddOnlyLatestMembershipData(testList, testRecordWithNewerDate);

            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual("newAddress", testList.FirstOrDefault().Address.CsvNewValue);
        }

        [TestMethod()]
        public void AddOnlyLatestMembershipData_OldRecordStays_Success_Test()
        {
            var testOldRecord = new MembershipData();
            testOldRecord.FirstName.CsvNewValue = "testFirstName";
            testOldRecord.LastName.CsvNewValue = "testLastName";
            testOldRecord.Email.CsvNewValue = "testEmail";
            testOldRecord.Address.CsvNewValue = "oldAddress";
            testOldRecord.UpdateDate.CsvNewValue = DateTime.Parse("02/15/18 10:00").ToShortDateString();

            var testNewRecordWithOlderDate = new MembershipData();
            testNewRecordWithOlderDate.FirstName.CsvNewValue = "testFirstName";
            testNewRecordWithOlderDate.LastName.CsvNewValue = "testLastName";
            testNewRecordWithOlderDate.Email.CsvNewValue = "testEmail";
            testNewRecordWithOlderDate.Address.CsvNewValue = "newAddress";
            testNewRecordWithOlderDate.UpdateDate.CsvNewValue = DateTime.Parse("02/14/18 11:00").ToShortDateString();

            var testList = new List<MembershipData> { testOldRecord };
            MembershipHelper.AddOnlyLatestMembershipData(testList, testNewRecordWithOlderDate);

            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual("oldAddress", testList.FirstOrDefault().Address.CsvNewValue);
        }

        [TestMethod()]
        public void AddOnlyLatestMembershipData_SameRecordsOnlyOneStays_Success_Test()
        {
            //var testRecord1 = new MembershipData { FirstName = "testFirstName", LastName = "testLastName", Email = "testEmail", Address = "oldAddress", UpdateDate = DateTime.Parse("02/15/18 10:00") };
            //var testRecord2 = new MembershipData { FirstName = "testFirstName", LastName = "testLastName", Email = "testEmail", Address = "oldAddress", UpdateDate = DateTime.Parse("02/15/18 10:00") };
            //var testList = new List<MembershipData> { testRecord1 };

            //MembershipHelper.AddOnlyLatestMembershipData(testList, testRecord2);

            //Assert.AreEqual(1, testList.Count);
        }
    }
}