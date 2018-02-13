using Membership_Merge_Tool.Enumerations;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Membership_Merge_Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var configData = new Config();

                // Collect new input data from Update Files
                var inputData = ReadInputDataFromUpdateFiles(configData);

                // Merge new input data into Master file

                // Move input files into Completed folder

                // Report result back

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }            
        }

        private static string ReadInputDataFromUpdateFiles(Config configData)
        {
            Console.Write($"Reading Input Update Files from '{configData.FolderPath_Updates}' ... ");

            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var inputFolder = Path.Combine(currentDirectory, configData.FolderPath_Updates);
            var inputFiles = Directory.GetFiles(inputFolder, "*.csv");

            return string.Empty;
            //new List<VsReleaseData>();
            //using (var progress = new ProgressBar())
            //{
            //    vsReleasesFromCosmos = cosmosFileModifier.ReadTabDelimitedFileConvertToVsReleaseData(vsReleasesTSVFile);
            //    vsReleasesFromCosmos.Sort((x, y) => x.ChannelManifestId.CompareTo(y.ChannelManifestId));
            //}
            //Console.Write($"Done{Environment.NewLine}");
            //Console.WriteLine($"Found {vsReleasesFromCosmos.Count} records {Environment.NewLine}");

        }
    }
}
