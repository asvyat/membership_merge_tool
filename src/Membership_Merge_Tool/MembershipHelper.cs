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
        public static MembershipDataRow GetMembershipDataRow(string csvString)
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
        public static void AddOnlyLatestMembershipData(List<MembershipDataRow> inputList, MembershipDataRow potentiallyNewerMembershipDataRow)
        {
            if (potentiallyNewerMembershipDataRow == null)
            {
                return;
            }
            // Get all the same rows with First and Last Names and email
            var existingRecordsInList = inputList
                .Where(i => SameDataRows(i, potentiallyNewerMembershipDataRow, MembershipDataProperty.FirstName)
                && SameDataRows(i, potentiallyNewerMembershipDataRow, MembershipDataProperty.LastName)
                && SameDataRows(i, potentiallyNewerMembershipDataRow, MembershipDataProperty.Email));

            // If matching rows found continue checking on UpdateDate
            if (existingRecordsInList != null && existingRecordsInList.Any())
            {
                // If Update date matches on both rows, nothing to add
                var sameRecord = existingRecordsInList
                    .Where(i => SameDataRows(i, potentiallyNewerMembershipDataRow, MembershipDataProperty.UpdateDate))
                    .FirstOrDefault();

                if (sameRecord != null)
                {
                    return;
                }

                var oldRecord = existingRecordsInList
                    .Where(i => DatePropertyValueGreaterInFirstRow(i, potentiallyNewerMembershipDataRow, MembershipDataProperty.UpdateDate))
                    .FirstOrDefault();

                var newRecord = existingRecordsInList
                    .Where(i => DatePropertyValueGreaterInFirstRow(potentiallyNewerMembershipDataRow, i, MembershipDataProperty.UpdateDate))
                    .FirstOrDefault();

                // If newer and older records exist, something is not right here
                if (oldRecord != null && newRecord != null)
                {
                    throw new InvalidOperationException("Found newer and older records in Membership List! " +
                        $"For First Name '{newRecord.GetCsvNewValuesForDataProperty(MembershipDataProperty.FirstName).FirstOrDefault()}', "+
                        $"Last Name '{newRecord.GetCsvNewValuesForDataProperty(MembershipDataProperty.LastName).FirstOrDefault()}', "+
                        $"Email '{newRecord.GetCsvNewValuesForDataProperty(MembershipDataProperty.Email).FirstOrDefault()}'");
                }
                // Removes old record first
                if (oldRecord != null)
                {
                    inputList.Remove(oldRecord);
                }
                // If newer record exist in the list, exit
                if (newRecord != null)
                {
                    return;
                }
            }
            inputList.Add(potentiallyNewerMembershipDataRow);
        }

        private static bool DatePropertyValueGreaterInFirstRow(MembershipDataRow dataRow1, MembershipDataRow dataRow2, MembershipDataProperty property)
        {
            var dateString1 = DateTime.Parse(dataRow1.GetCsvNewValuesForDataProperty(property).FirstOrDefault());
            var dateString2 = DateTime.Parse(dataRow2.GetCsvNewValuesForDataProperty(property).FirstOrDefault());

            return dateString1 > dateString2;
        }

        private static bool SameDataRows(MembershipDataRow dataRow1, MembershipDataRow dataRow2, MembershipDataProperty property)
        {
            return dataRow1.GetCsvNewValuesForDataProperty(property)
                    .Any(f => f.Equals(
                        dataRow2.GetCsvNewValuesForDataProperty(property)
                                .FirstOrDefault(), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
