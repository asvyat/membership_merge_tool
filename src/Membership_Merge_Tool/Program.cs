using Membership_Merge_Tool.Enumerations;
using System;
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

            Console.WriteLine($"Config '{configData.GetValue(ConfigVariableName.FolderName_Updates)}'");
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var inputFolder = Path.Combine(currentDirectory, configData.FolderPath_Updates);
            var inputFiles = Directory.GetFiles(inputFolder, "*.csv");

            //using StreamReader as possible can get Out Of Memory Exception 
            long i = 0;
            using (var progress = new ProgressBar())
            {
                foreach (var inputFile in inputFiles)
                {
                    using (TextReader reader = new StreamReader(File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        var line = string.Empty;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var values = line.Split(',');
                            //returnList.Add();
                            //progress.Report((double)i / countOfLinesInputFile);
                            i++;
                        }
                    }
                }
                
            }
            Console.Write($"Done{Environment.NewLine}");
            
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
