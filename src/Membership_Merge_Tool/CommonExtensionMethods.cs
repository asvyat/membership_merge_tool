using System;
using System.Collections.Generic;
using System.Linq;

namespace Membership_Merge_Tool
{
    public static class ListExtensions
    {
        public static char[] ListSeparatorChars = new[] { ',', ';' };

        /// <summary>
        /// Split list string on common separator characters and return List object for all the non-empty items
        /// </summary>
        /// <param name="listString">For example: item1;item2,item3</param>
        public static List<string> SplitOnCommonSeparatorCharsAndRemoveEmptyEntries(string listString)
        {
            if (string.IsNullOrWhiteSpace(listString))
            {
                return new List<string>();
            }
            return listString.Split(ListSeparatorChars, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim())
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .ToList();
        }
    }
}
