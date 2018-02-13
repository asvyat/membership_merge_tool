using Membership_Merge_Tool.Enumerations;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Membership_Merge_Tool
{
    public class Config
    {
        public Dictionary<ConfigVariableName, string> ConfigEntries { get; set; }

        public Config()
        {
            // for each enum item, will get corresponding item from App.config
            ConfigEntries = new Dictionary<ConfigVariableName, string>();

            foreach (ConfigVariableName configName in Enum.GetValues(typeof(ConfigVariableName)))
            {
                var configValue = ConfigurationManager.AppSettings[configName.ToString()];
                if (string.IsNullOrWhiteSpace(configValue))
                {
                    throw new ArgumentNullException($"Unable to find any value for '{configName}' config entry in App.config file!");
                }
                ConfigEntries.Add(configName, configValue);
            }
        }

        /// <summary>
        /// Get a value of a config setting
        /// </summary>
        public string GetValue(ConfigVariableName configVariableName)
        {
            return ConfigEntries[configVariableName];
        }

        public string GetFolderPath(ConfigVariableName configVariableName)
        {
            return $"{GetValue(ConfigVariableName.RootFolder)}\\{configVariableName}";
        }
    }
}
