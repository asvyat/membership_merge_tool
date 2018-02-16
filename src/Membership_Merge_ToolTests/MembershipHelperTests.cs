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
            var testOldRecord = new MembershipData { FirstName = "testFirstName", LastName = "testLastName", Email = "testEmail", Address = "oldAddress", UpdateDate = DateTime.Parse("02/15/18 10:00")};
            var testNewRecord = new MembershipData { FirstName = "testFirstName", LastName = "testLastName", Email = "testEmail", Address = "newAddress", UpdateDate = DateTime.Parse("02/15/18 11:00") };
            var testList = new List<MembershipData> { testOldRecord };

            MembershipHelper.AddOnlyLatestMembershipData(testList, testNewRecord);

            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual("newAddress", testList.FirstOrDefault().Address);
        }

        [TestMethod()]
        public void AddOnlyLatestMembershipData_OldRecordStays_Success_Test()
        {
            var testOldRecord = new MembershipData { FirstName = "testFirstName", LastName = "testLastName", Email = "testEmail", Address = "oldAddress", UpdateDate = DateTime.Parse("02/15/18 10:00") };
            var testNewRecord = new MembershipData { FirstName = "testFirstName", LastName = "testLastName", Email = "testEmail", Address = "newAddress", UpdateDate = DateTime.Parse("02/14/18 11:00") };
            var testList = new List<MembershipData> { testOldRecord };

            MembershipHelper.AddOnlyLatestMembershipData(testList, testNewRecord);

            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual("oldAddress", testList.FirstOrDefault().Address);
        }
    }
}