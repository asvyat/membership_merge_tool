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
        public static MembershipData GetMembershipDataRow(string csvString)
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
            return new MembershipData(values);
        }

        /// <summary>
        /// Add only latest Membership Data object into input list
        /// For example if there are several input files older and newer, will keep newer data
        /// </summary>
        public static void AddOnlyLatestMembershipData(List<MembershipData> inputList, MembershipData potentiallyNewerMembershipDataRow)
        {
            if (potentiallyNewerMembershipDataRow == null)
            {
                return;
            }
            // Get all the same rows with First and Last Names and email
            var existingRecordsInList = inputList
                .Where(i => i.FirstName.CsvNewValue.Equals(potentiallyNewerMembershipDataRow.FirstName.CsvNewValue, StringComparison.InvariantCultureIgnoreCase)
                && (i.LastName.CsvNewValue.Equals(potentiallyNewerMembershipDataRow.LastName.CsvNewValue, StringComparison.InvariantCultureIgnoreCase)
                && (i.Email.CsvNewValue.Equals(potentiallyNewerMembershipDataRow.Email.CsvNewValue, StringComparison.InvariantCultureIgnoreCase))));

            // If matching rows found continue checking on UpdateDate
            if (existingRecordsInList != null && existingRecordsInList.Any())
            {
                // If Update date matches on both rows, nothing to add
                var sameRecord = existingRecordsInList
                    .Where(i => i.UpdateDate.CsvNewValue.Equals(potentiallyNewerMembershipDataRow.UpdateDate.CsvNewValue, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();

                if (sameRecord != null)
                {
                    return;
                }

                var oldRecordInList = existingRecordsInList
                    .Where(i => (DateTime.Parse(i.UpdateDate.CsvNewValue) < DateTime.Parse(potentiallyNewerMembershipDataRow.UpdateDate.CsvNewValue)))
                    .FirstOrDefault();

                var newRecordInList = existingRecordsInList
                    .Where(i => (DateTime.Parse(i.UpdateDate.CsvNewValue) > DateTime.Parse(potentiallyNewerMembershipDataRow.UpdateDate.CsvNewValue)))
                    .FirstOrDefault();

                // If newer and older records exist, something is not right here
                if (oldRecordInList != null && newRecordInList != null)
                {
                    throw new InvalidOperationException("Found newer and older records in Membership List! " +
                        $"For First Name '{newRecordInList.FirstName.CsvNewValue}', "+
                        $"Last Name '{newRecordInList.LastName.CsvNewValue}', "+
                        $"Email '{newRecordInList.Email.CsvNewValue}'");
                }
                // Removes old record first
                if (oldRecordInList != null)
                {
                    inputList.Remove(oldRecordInList);
                }
                // If newer record exist in the original old list, exit, no need to add potentially newer record
                if (newRecordInList != null)
                {
                    return;
                }
            }
            inputList.Add(potentiallyNewerMembershipDataRow);
        }
    }
}
