using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Membership_Merge_Tool
{
    public class ValueHelper
    {
        /// <summary>
        /// Get Class Property Description attribute value
        /// For example:
        /// Foo foo = new Foo();
        /// Console.WriteLine(GetDescription(() => foo.property1 )
        /// </summary>
        public static string GetDescription<T>(Expression<Func<T>> expr)
        {
            var mexpr = expr.Body as MemberExpression;
            if (mexpr == null) return null;
            if (mexpr.Member == null) return null;
            object[] attrs = mexpr.Member.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs == null || attrs.Length == 0) return null;
            DescriptionAttribute desc = attrs[0] as DescriptionAttribute;
            if (desc == null) return null;
            return desc.Description;
        }


        public static bool IsHeaderString(string firstName, string lastName)
        {
            return firstName.Equals("first_name", StringComparison.InvariantCultureIgnoreCase)
                && lastName.Equals("last_name", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool ParseStringToBool(string input)
        {
            var returnBool = true;
            try
            {
                returnBool = string.IsNullOrWhiteSpace(input) || input.ToLower() == "yes" ? true :
                input.ToLower() == "no" ? false : bool.Parse(input);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Unable to parse to bool this input '{input}'", ex);
            }
            return returnBool;
        }

        public static DateTime? ParseStringToDateTime(string input)
        {
            DateTime? returnDateTime = null;
            //returnDateTime = 
            if (!string.IsNullOrWhiteSpace(input))
            {
                DateTime inputChildDateOfBirth;
                if (DateTime.TryParse(input, out inputChildDateOfBirth))
                {
                    returnDateTime = inputChildDateOfBirth;
                };

                // Try a differeny method
                // TODO: add in case needed
                if (returnDateTime == null)
                {

                }
            } 
            return returnDateTime;
        }

        /// <summary>
        /// Split string values on coma with or without inner quotes
        /// This method found: https://stackoverflow.com/questions/3776458/split-a-comma-separated-string-with-both-quoted-and-unquoted-strings
        /// </summary>
        /// <param name="input">Input string, i.e. "1,2,""more, text"", text"</param>
        /// <returns>Array of splitted values</returns>
        public static string[] SplitCSV(string input)
        {
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;

                if (curr.Contains("\""))
                {
                    curr = curr.Replace("\"", string.Empty);
                }

                if (0 == curr.Length)
                {
                    list.Add("");
                }
                list.Add(curr.TrimStart(',').Trim());
            }
            return list.ToArray();
        }
    }
}
