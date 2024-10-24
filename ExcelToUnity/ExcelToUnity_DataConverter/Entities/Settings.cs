using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToUnity_DataConverter
{
	[Serializable]
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
		public List<GoogleSheetsPath> googleSheetsPaths = new List<GoogleSheetsPath>();
		/// <summary>
		/// Encrypted or not
		/// </summary>
		public bool encryption;
		/// <summary>
		/// String converted to Byte array (1,2,3,4,5,.....)
		/// </summary>
		public string encryptionKey = @"168, 220, 184, 133, 78, 149, 8, 249, 171, 138, 98, 170, 95, 15, 211, 200, 51, 242, 4, 193, 219, 181, 232, 99, 16, 240, 142, 128, 29, 163, 245, 24, 204, 73, 173, 32, 214, 76, 31, 99, 91, 239, 232, 53, 138, 195, 93, 195, 185, 210, 155, 184, 243, 216, 204, 42, 138, 101, 100, 241, 46, 145, 198, 66, 11, 17, 19, 86, 157, 27, 132, 201, 246, 112, 121, 7, 195, 148, 143, 125, 158, 29, 184, 67, 187, 100, 31, 129, 64, 130, 26, 67, 240, 128, 233, 129, 63, 169, 5, 211, 248, 200, 199, 96, 54, 128, 111, 147, 100, 6, 185, 0, 188, 143, 25, 103, 211, 18, 17, 249, 106, 54, 162, 188, 25, 34, 147, 3, 222, 61, 218, 49, 164, 165, 133, 12, 65, 92, 48, 40, 129, 76, 194, 229, 109, 76, 150, 203, 251, 62, 54, 251, 70, 224, 162, 167, 183, 78, 103, 28, 67, 183, 23, 80, 156, 97, 83, 164, 24, 183, 81, 56, 103, 77, 112, 248, 4, 168, 5, 72, 109, 18, 75, 219, 99, 181, 160, 76, 65, 16, 41, 175, 87, 195, 181, 19, 165, 172, 138, 172, 84, 40, 167, 97, 214, 90, 26, 124, 0, 166, 217, 97, 246, 117, 237, 99, 46, 15, 141, 69, 4, 245, 98, 73, 3, 8, 161, 98, 79, 161, 127, 19, 55, 158, 139, 247, 39, 59, 72, 161, 82, 158, 25, 65, 107, 173, 5, 255, 53, 28, 179, 182, 65, 162, 17";
		public string excludedSheets;
		public string _namespace;
		public string unminizedFields = @"id; mode; type; group; level; rank";
		public bool seperateIDs;
		public bool seperateConstants;
		public bool seperateLocalizations = true;
		public bool mergeJsonsIntoSingleJson;
		public bool keepOnlyEnumAsIDs;
		public string languageCharactersMaps = @"japan; korean; chinese";
		public string ggClientId;
		public string ggClientSecret;

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

		public void RemoveGoogleSheet(string googleSheetId)
		{
			for (int i = googleSheetsPaths.Count - 1; i >= 0; i--)
			{
				if (string.IsNullOrEmpty(googleSheetsPaths[i].id))
				{
					googleSheetsPaths.RemoveAt(i);
					continue;
				}
				if (googleSheetsPaths[i].id == googleSheetId)
				{
					googleSheetsPaths.RemoveAt(i);
					break;
				}
			}
		}

		public void AddGoogleSheet(string googleSheetId, string sheetName)
		{
			for (int i = 0; i < googleSheetsPaths.Count; i++)
			{
				if (googleSheetsPaths[i].id == googleSheetId)
				{
					googleSheetsPaths[i].AddSheet(sheetName);
					break;
				}
			}
			var temp = new GoogleSheetsPath
			{
				id = googleSheetId,
			};
			temp.AddSheet(sheetName);
			googleSheetsPaths.Add(temp);
		}

		public void RemoveGoogleSheet(string googleSheetId, string sheetName)
		{
			for (int i = 0; i < googleSheetsPaths.Count; i++)
			{
				if (googleSheetsPaths[i].id == googleSheetId)
				{
					googleSheetsPaths[i].RemoveSheet(sheetName);
					break;
				}
			}
		}

		public void SetGoogleSheet(string googleSheetId, string googleSheetName, List<GoogleSheetsPath.Sheet> sheets)
		{
			for (int i = 0; i < googleSheetsPaths.Count; i++)
			{
				if (googleSheetsPaths[i].id == googleSheetId)
				{
					googleSheetsPaths[i].name = googleSheetName;
					googleSheetsPaths[i].sheets = sheets;
					return;
				}
			}
			googleSheetsPaths.Add(new GoogleSheetsPath
			{
				id = googleSheetId,
				name = googleSheetName,
				sheets = sheets,
			});
			googleSheetsPaths.Sort();
		}
	}
	
	public class ID
	{
		public string Key { get; set; }
		public int Value { get; set; }

		public ID(string key, int value)
		{
			Key = key;
			Value = value;
		}
	}
	
	public class FieldValueType
	{
		public string name;
		public string type;

		public FieldValueType(string name)
		{
			this.name = name;
		}

		public FieldValueType(string name, string type)
		{
			this.name = name;
			this.type = type;
		}
	}
}