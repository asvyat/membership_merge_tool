using System;
using System.IO;

namespace Membership_Merge_Tool
{
    public class FileHelper
    {
        /// <summary>
        /// Count number of lines in provided local file
        /// </summary>        
        public static long GetFileLineCount(string localFilePath)
        {
            //To report to the same line
            Console.Write($"Counting lines in a file {localFilePath} ... ");

            long linesCount = 0;
            FileInfo fi = new FileInfo(localFilePath);
            using (var progress = new ProgressBar())
            {
                using (TextReader reader = new StreamReader(File.Open(localFilePath, FileMode.Open, FileAccess.Read)))
                {
                    while (reader.ReadLine() != null)
                    {
                        linesCount++;
                        progress.Report((double)linesCount / (fi.Length / 180)); //it will not really correct percentage but still some progress to show
                    }
                }
            }
            Console.Write($"{linesCount}{Environment.NewLine}");
            return linesCount;
        }

        /// <summary>
        /// Create new output folder based on current date time stamp in a current folder
        /// </summary>
        public static string GetNewTempFolder()
        {
            //create output folder
            var outputFileFolder = Path.Combine(Environment.CurrentDirectory, DateTime.Now.ToString("yyyy-MM-dd__hh-mm-ss"));
            Directory.CreateDirectory(outputFileFolder);

            Console.WriteLine($"Temp folder created '{outputFileFolder}'");
            return outputFileFolder;
        }
    }
}
