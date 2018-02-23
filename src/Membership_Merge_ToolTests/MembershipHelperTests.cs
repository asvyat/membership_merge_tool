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
            var testOldRecord = new MembershipDataRow();
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.FirstName, "testFirstName");
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.LastName, "testLastName");
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.Email, "testEmail");
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.Address, "oldAddress");
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.UpdateDate, DateTime.Parse("02/15/18 10:00").ToShortDateString());
            
            var testRecordWithNewerDate = new MembershipDataRow();
            testRecordWithNewerDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.FirstName, "testFirstName");
            testRecordWithNewerDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.LastName, "testLastName");
            testRecordWithNewerDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.Email, "testEmail");
            testRecordWithNewerDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.Address, "newAddress");
            testRecordWithNewerDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.UpdateDate, DateTime.Parse("02/15/18 11:00").ToShortDateString());
            
            var testList = new List<MembershipDataRow> { testOldRecord };
            MembershipHelper.AddOnlyLatestMembershipData(testList, testRecordWithNewerDate);

            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual("newAddress", testList.FirstOrDefault().GetCsvNewValuesForDataProperty(MembershipDataProperty.Address).FirstOrDefault());
        }

        [TestMethod()]
        public void AddOnlyLatestMembershipData_OldRecordStays_Success_Test()
        {
            var testOldRecord = new MembershipDataRow();
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.FirstName, "testFirstName");
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.LastName, "testLastName");
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.Email, "testEmail");
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.Address, "oldAddress");
            testOldRecord.UpdateDataItemWithCsvNewValue(MembershipDataProperty.UpdateDate, DateTime.Parse("02/15/18 10:00").ToShortDateString());

            var testRecordWithOlderDate = new MembershipDataRow();
            testRecordWithOlderDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.FirstName, "testFirstName");
            testRecordWithOlderDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.LastName, "testLastName");
            testRecordWithOlderDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.Email, "testEmail");
            testRecordWithOlderDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.Address, "newAddress");
            testRecordWithOlderDate.UpdateDataItemWithCsvNewValue(MembershipDataProperty.UpdateDate, DateTime.Parse("02/14/18 11:00").ToShortDateString());

            var testList = new List<MembershipDataRow> { testOldRecord };
            MembershipHelper.AddOnlyLatestMembershipData(testList, testRecordWithOlderDate);

            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual("oldAddress", testList.FirstOrDefault().GetCsvNewValuesForDataProperty(MembershipDataProperty.Address).FirstOrDefault());
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