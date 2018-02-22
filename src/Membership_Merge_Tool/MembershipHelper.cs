using Membership_Merge_Tool.Enumerations;
using Membership_Merge_Tool.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Membership_Merge_Tool
{
    public class MembershipHelper
    {
        /// <summary>
        /// Get Membership data object from CSV input string
        /// </summary>
        public static MembershipDataRow GetMembershipData(string csvString)
        {
            var values = ValueHelper.SplitCSV(csvString);

            // These required values should present to populate the rest
            var firstName = values[0];
            var lastName = values[1];
            var email = values[12];
            if (string.IsNullOrEmpty(firstName)
                && string.IsNullOrEmpty(lastName)
                && string.IsNullOrEmpty(email)
                || ValueHelper.IsCsvInputHeaderString(firstName, lastName))
            {
                return null;
            }
            return new MembershipDataRow(values);
        }

        /// <summary>
        /// Add only latest Membership Data object into input list
        /// For example if there are several input files older and newer, will keep newer data
        /// </summary>
        public static void AddOnlyLatestMembershipData(List<MembershipDataRow> inputList, MembershipDataRow membershipDataRow)
        {
            if (membershipDataRow == null)
            {
                return;
            }
            var existingRecordsInList = inputList
                .Where(i => i.GetCsvNewValuesForDataProperty(MembershipDataProperty.FirstName)
                    .Any(s => s
                        .Equals(membershipDataRow
                            .GetCsvNewValuesForDataProperty(MembershipDataProperty.FirstName)
                                .FirstOrDefault(), StringComparison.InvariantCultureIgnoreCase)));
            var i =0;
                    //.Where(c => c.GetCsvNewValue(MembershipDataProperty.FirstName)
                    //.Equals(membershipDataRow.FirstName, StringComparison.InvariantCultureIgnoreCase)
                    //&& i.LastName.Equals(membershipDataRow.LastName, StringComparison.InvariantCultureIgnoreCase)
                    //&& i.Email.Equals(membershipDataRow.Email, StringComparison.InvariantCultureIgnoreCase));

            //var existingRecordsInList = inputList.Where(i => i.MembershipDataCellList.Where(c => c.MembershipDataPropertyName == Enumerations.MembershipDataProperty.FirstName).Equals(membershipData.FirstName, StringComparison.InvariantCultureIgnoreCase)
            //        && i.LastName.Equals(membershipData.LastName, StringComparison.InvariantCultureIgnoreCase)
            //        && i.Email.Equals(membershipData.Email, StringComparison.InvariantCultureIgnoreCase));

            //if (existingRecordsInList != null && existingRecordsInList.Any())
            //{
            //    var sameRecord = existingRecordsInList
            //        .Where(i => i.UpdateDate == membershipData.UpdateDate).FirstOrDefault();

            //    if (sameRecord != null)
            //    {
            //        return;
            //    }

            //    var oldRecord = existingRecordsInList
            //        .Where(i => i.UpdateDate < membershipData.UpdateDate).FirstOrDefault();

            //    var newRecord = existingRecordsInList
            //        .Where(i => i.UpdateDate > membershipData.UpdateDate).FirstOrDefault();

            //    // If newer and older records exist, something is not right here
            //    if (oldRecord != null && newRecord != null)
            //    {
            //        throw new InvalidOperationException("Found newer and older records in Membership List! " +
            //            $"For First Name '{newRecord.FirstName}', Last Name '{newRecord.LastName}', Email '{newRecord.Email}'");
            //    }
            //    // Removes old record first
            //    if (oldRecord != null)
            //    {
            //        inputList.Remove(oldRecord);
            //    }
            //    // If newer record exist in the list, exit
            //    if (newRecord != null)
            //    {
            //        return;
            //    }
            //}
            //inputList.Add(membershipData);
        }
    }
}
