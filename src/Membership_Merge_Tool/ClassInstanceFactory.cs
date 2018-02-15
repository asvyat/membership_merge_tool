using Membership_Merge_Tool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership_Merge_Tool
{
    public class ClassInstanceFactory
    {
        public static MembershipData GetMembershipData(string csvString)
        {
            var values = ValueHelper.SplitCSV(csvString);

            // These required values should present to populate the rest
            var firstName = values[0];
            var lastName = values[1];
            var email = values[12];
            if (string.IsNullOrEmpty(firstName)
                && string.IsNullOrEmpty(lastName)
                && string.IsNullOrEmpty(email)
                || ValueHelper.IsHeaderString(firstName, lastName))
            {
                return null;
            }
            return new MembershipData(values);
        }       
    }
}
