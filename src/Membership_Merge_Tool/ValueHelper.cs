using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Membership_Merge_Tool
{
    public class ValueHelper
    {
        public static bool ParseStringToBool(string input)
        {
            return string.IsNullOrWhiteSpace(input) || input.ToLower() == "yes" ? true :
                input.ToLower() == "no" ? false : bool.Parse(input);
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
                // TODO
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
                if (0 == curr.Length)
                {
                    list.Add("");
                }
                list.Add(curr.TrimStart(','));
            }
            return list.ToArray();
        }
    }
}
