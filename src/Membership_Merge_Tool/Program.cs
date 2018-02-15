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

        private static List<MembershipData> ReadInputDataFromUpdateFiles(Config configData)
        {
            var returnList = new List<MembershipData>();
            Console.Write($"Reading Input Update Files from '{configData.FolderPath_Updates}' ... ");

            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var inputFolder = Path.Combine(currentDirectory, configData.FolderPath_Updates);
            var inputFiles = Directory.GetFiles(inputFolder, "*.csv");

            // Use StreamReader as possible can get Out Of Memory Exception 
            // during large file reading
            //long i = 0;
            using (var progress = new ProgressBar())
            {
                foreach (var inputFile in inputFiles)
                {
                    using (TextReader reader = new StreamReader(File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        var line = string.Empty;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var membershipData = ClassInstanceFactory.GetMembershipData(line);

                            // Add only items to a list that are new or 
                            // list contains the same Key elements with lower UpdateDate
                            if (returnList.Count > 0
                                && returnList
                                .Any(i => i.FirstName.Equals(membershipData.FirstName, StringComparison.InvariantCultureIgnoreCase)
                                    && i.LastName.Equals(membershipData.LastName, StringComparison.InvariantCultureIgnoreCase)
                                    && i.Email.Equals(membershipData.Email, StringComparison.InvariantCultureIgnoreCase)
                                    && i.UpdateDate < membershipData.UpdateDate))
                            {
                                returnList.Add(membershipData);
                            }                           
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
