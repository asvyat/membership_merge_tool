using Membership_Merge_Tool.Enumerations;
using Membership_Merge_Tool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                MergeInputDataIntoMasterExcelFile(configData.FilePath_MasterExcelFile, inputDataList);

                // Move input files into Completed folder
                MoveInputFilesIntoCompletedFolder(configData);
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

            Console.Write($"Moving Input Update Files into '{configData.FolderPath_Completed}' ... ");
            if (!Directory.Exists(configData.FolderPath_Completed))
            {
                Directory.CreateDirectory(configData.FolderPath_Completed);
            }

            foreach (var inputFilePath in inputFiles)
            {
                var fileName = Path.GetFileName(inputFilePath);
                File.Move(inputFilePath, Path.Combine(configData.FolderPath_Completed, fileName));
            }
            Console.Write($"Done{Environment.NewLine}");
        }

        private static void MergeInputDataIntoMasterExcelFile(string masterFile, List<MembershipData> inputDataRowList)
        {
            if (inputDataRowList.Any())
            {
                Console.Write($"Updating Excel Master File '{masterFile}' ... ");
                var updatedRows = ExcelFileHelper.UpdateInputDataIntoExcelFile(masterFile, inputDataRowList);
                Console.Write($"Done{Environment.NewLine}");
                Console.WriteLine($"Updated {updatedRows} rows");

                // Check if there are any non-updated new rows that needs to be added
                if (inputDataRowList.Any(p => p.ExistsInExcelFile == false))
                {
                    Console.Write($"Adding new records into Excel Master File '{masterFile}' ... ");
                    var addedRows = ExcelFileHelper.AddInputDataIntoExcelFile(masterFile, inputDataRowList);
                    Console.Write($"Done{Environment.NewLine}");
                    Console.WriteLine($"Added {addedRows} rows");
                }
            }            
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
                            var membershipData = MembershipHelper.GetMembershipDataRow(line);
                            MembershipHelper.AddOnlyLatestMembershipData(returnList, membershipData);
                        }
                    }
                }                
            }
            Console.Write($"Done{Environment.NewLine}");
            Console.WriteLine($"Found {returnList.Count} new records in CSV input files");

            return returnList;
        }
    }
}
