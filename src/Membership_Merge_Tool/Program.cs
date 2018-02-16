using Membership_Merge_Tool.Enumerations;
using Membership_Merge_Tool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                var inputDataList = ReadInputDataFromUpdateFiles(configData);

                // Merge new input data into Master file
                MergeInputDataIntoMasterExcelFile(inputDataList, configData);

                // Move input files into Completed folder
                MoveInputFilesIntoCompletedFolder(configData);

                // Report result back

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }            
        }

        private static void MoveInputFilesIntoCompletedFolder(Config configData)
        {
            var inputFiles = Directory.GetFiles(configData.FolderPath_Updates, configData.ConfigEntries[ConfigVariableName.UpdateFileNamePattern]);
            if (!inputFiles.Any())
            {
                return;
            }

            Console.WriteLine($"Moving Input Update Files into '{configData.FolderPath_Completed}'.");
            if (!Directory.Exists(configData.FolderPath_Completed))
            {
                Directory.CreateDirectory(configData.FolderPath_Completed);
            }

            foreach (var inputFilePath in inputFiles)
            {
                var fileName = Path.GetFileName(inputFilePath);
                File.Move(inputFilePath, Path.Combine(configData.FolderPath_Completed, fileName));
            }
            Console.WriteLine($"Done{Environment.NewLine}");
        }

        private static void MergeInputDataIntoMasterExcelFile(List<MembershipData> inputDataList, Config configData)
        {
            // Open Excel file

            // Update from input list
        }

        private static List<MembershipData> ReadInputDataFromUpdateFiles(Config configData)
        {
            var returnList = new List<MembershipData>();
            Console.Write($"Reading Input Update Files from '{configData.FolderPath_Updates}' ... ");                        
            var inputFiles = Directory.GetFiles(configData.FolderPath_Updates, configData.ConfigEntries[ConfigVariableName.UpdateFileNamePattern]);

            using (var progress = new ProgressBar())
            {
                foreach (var inputFile in inputFiles)
                {
                    using (TextReader reader = new StreamReader(File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        var line = string.Empty;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var membershipData = MembershipHelper.GetMembershipData(line);
                            MembershipHelper.AddOnlyLatestMembershipData(returnList, membershipData);
                        }
                    }
                }                
            }

            Console.Write($"Done{Environment.NewLine}");
            Console.WriteLine($"Found {returnList.Count} new records");

            return returnList;
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
