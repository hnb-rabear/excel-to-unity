using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToUnity_DataConverter.Entities
{
    [System.Serializable]
    public class Settings
    {
        /// <summary>
        /// Input Excel file path
        /// </summary>
        public string inputDataFilePath = "";
        /// <summary>
        /// Output folder for excel json data
        /// </summary>
        public string outputDataFilePath = "";
        /// <summary>
        /// Output folder for Constants or ID data
        /// </summary>
        public string outputConstantsFilePath = "";
        /// <summary>
        /// Output folder for Localization
        /// </summary>
        public string outputLocalizationFilePath = "";
        /// <summary>
        /// All excel files path
        /// </summary>
        public List<ExcelPath> allFiles = new List<ExcelPath>();
		public List<GoogleSpreadSheetPath> googleSpreadSheets = new List<GoogleSpreadSheetPath>();
        /// <summary>
        /// Encrypted or not
        /// </summary>
        public bool encryption;
        /// <summary>
        /// String converted to Byte array (1,2,3,4,5,.....)
        /// </summary>
        public string encryptionKey;
        public string excludedSheets;
        public string _namespace;
        public string unminizedFields = "id; mode; type; group; level; rank";
        public bool seperateIDs;
        public bool seperateConstants;
        public bool seperateLocalizations;
        public bool mergeJsonsIntoSingleJson;
        public bool keepOnlyEnumAsIDs;
        public string languageCharactersMaps;

        public string[] GetExcludedSheets()
        {
            if (string.IsNullOrEmpty(excludedSheets))
                return null;
            var strs = excludedSheets.Split(';');
            for (int i = 0; i < strs.Length; i++)
                strs[i] = strs[i].Trim().ToLower();
            return strs;
        }

        public string[] GetUnminizedFields()
        {
            var strs = unminizedFields.Split(';');
            for (int i = 0; i < strs.Length; i++)
                strs[i] = strs[i].Trim().ToLower();
            return strs;
        }

		public string GetLocalizationFolder()
		{
			string path = outputLocalizationFilePath;
			string resourcesDirName = "Resources";

			// Find the index of the Resources directory
			int resourcesIndex = path.IndexOf(resourcesDirName, StringComparison.OrdinalIgnoreCase);
			if (resourcesIndex != -1)
			{
				// Add length of Resources to the index to get the part after it
				int startAfterResources = resourcesIndex + resourcesDirName.Length;

				// Get the path after Resources
				string pathAfterResources = path.Substring(startAfterResources).TrimStart(System.IO.Path.DirectorySeparatorChar);

				return pathAfterResources;
			}

			// Return empty or a default value if Resources not found
			return string.Empty;
		}
	}
}
