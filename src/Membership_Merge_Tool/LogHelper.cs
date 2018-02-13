using System;

namespace Membership_Merge_Tool
{
    public static class LogHelper
    {
        /// <summary>
        /// Write the specified string by prefixing current Data Time to console
        /// </summary>
        public static void WriteLine(string stringToWrite)
        {
            Console.WriteLine($"{DateTime.Now}: {stringToWrite}");
        }
    }
}
