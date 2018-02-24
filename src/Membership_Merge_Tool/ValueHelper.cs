using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Membership_Merge_Tool
{
    public class ValueHelper
    {        
        /// <summary>
        /// If this is a Header string from CSV Input file
        /// </summary>
        public static bool IsCsvInputHeaderString(string firstName, string lastName)
        {
            return firstName.Equals("first_name", StringComparison.InvariantCultureIgnoreCase)
                && lastName.Equals("last_name", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Parse input string to Bool value and return string value of it
        /// </summary>
        /// <param name="input"></param>
        /// <param name="childName">Optional child name, to verify if it's empty and return empty string</param>
        public static string ParseStringToBoolString(string input, string childName = null)
        {
            var returnBoolString = string.Empty;
            try
            {
                // if no child name provided or provided with non-empty value
                if (childName == null || !string.IsNullOrWhiteSpace(childName))
                {
                    returnBoolString = string.IsNullOrWhiteSpace(input) || input.ToLower() == "yes" ? true.ToString() :
                    input.ToLower() == "no" ? false.ToString() : bool.Parse(input).ToString();
                }                
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Unable to parse to bool this input '{input}'", ex);
            }
            return returnBoolString;
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
