using ChoETL;
using CsvHelper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ExcelToUnity_DataConverter
{
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

    public static class HelperExtension
    {
        public static string ToCellString(this ICell cell, string pDefault = "")
        {
            if (cell == null)
                return pDefault;
            string cellStr;
            if (cell.CellType == CellType.Formula)
            {
                switch (cell.CachedFormulaResultType)
                {
                    case CellType.Numeric:
                        cellStr = cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                        break;
                    
                    case CellType.String:
                        cellStr = cell.StringCellValue;
                        break;
                    
                    case CellType.Boolean:
                        cellStr = cell.BooleanCellValue.ToString();
                        break;
                    
                    default:
                        cellStr = cell.ToString();
                        break;
                }
            }
            else
                cellStr = cell.ToString();
            return cellStr;
        }
    }

    public static class Helper
    {
        private const string LOCALIZED_TEXT_TEMPLATE = "Resources\\LocalizedTextTemplate.txt";

        public static IWorkbook LoadWorkBook(string pFilePath)
        {
            using (var file = new FileStream(pFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return new XSSFWorkbook(file);
            }
        }

        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if (strInput.StartsWith("{") && strInput.EndsWith("}") || //For object
                strInput.StartsWith("[") && strInput.EndsWith("]")) //For array
            {
                try
                {
                    JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            return false;
        }

        //public static int GetLastCellNum(ISheet sheet)
        //{
        //    int lastCellNum = 0;
        //    int totalRows = sheet.LastRowNum; 
        //    if (totalRows > 4)
        //        totalRows = 4;
        //    for (int row = 0; row <= totalRows; row++)
        //    {
        //        IRow rowData = sheet.GetRow(row);
        //        if (lastCellNum < rowData.LastCellNum)
        //            lastCellNum = rowData.LastCellNum;
        //    }
        //    return lastCellNum;
        //}

        /// <summary>
        /// Return the the name and type of Field of a column
        /// </summary>
        public static List<FieldValueType> GetFieldValueTypes(IWorkbook pWorkBook, string pSheetName)
        {
            var sheet = pWorkBook.GetSheet(pSheetName);
            var rowData = sheet.GetRow(0);
            if (rowData.IsNull())
                return null;

            int lastCellNum = rowData.LastCellNum;
            var fieldsName = new string[lastCellNum];
            var fieldsValue = new string[lastCellNum];
            for (int col = 0; col < rowData.LastCellNum; col++)
            {
                var cell = rowData.GetCell(col);
                if (cell != null && !string.IsNullOrEmpty(cell.StringCellValue))
                    fieldsName[col] = cell.ToString().Replace(" ", "_");
                else
                    fieldsName[col] = "";
                fieldsValue[col] = "";
            }

            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                rowData = sheet.GetRow(row);
                if (rowData != null)
                {
                    //Find longest value, and use it to check value type
                    for (int col = 0; col < fieldsName.Length; col++)
                    {
                        var cell = rowData.GetCell(col);
                        if (cell != null)
                        {
                            string cellStr = cell.ToCellString();
                            if (cellStr.Length > fieldsValue[col].Length)
                                fieldsValue[col] = cellStr;
                        }
                    }
                }
            }

            var fieldValueTypes = new List<FieldValueType>();
            for (int i = 0; i < fieldsName.Length; i++)
            {
                string fieldName = fieldsName[i];
                string filedValue = fieldsValue[i].Trim();
                bool isArray = fieldName.Contains("[]");
                var fieldValueType = new FieldValueType(fieldName);

                //bool linked = fieldName.Contains("&"); // if this field value is linked to another sheet
                //if (linked)
                //{
                //    int ref1stIndex = fieldName.IndexOf("&");
                //    string linkedSheet = fieldName.Substring(fieldName.IndexOf("&") + 1);
                //    string linkedSheet_RefId = linkedSheet.Substring(linkedSheet.IndexOf("&") + 1);
                //    linkedSheet = linkedSheet.Replace("&" + linkedSheet_RefId, "");
                //    fieldName = fieldName.Substring(0, ref1stIndex);

                //    if (!string.IsNullOrEmpty(linkedSheet) && !string.IsNullOrEmpty(linkedSheet_RefId))
                //    {
                //        fieldValueType.linkedSheet = linkedSheet;
                //        fieldValueType.linkedSheet_RefId = linkedSheet_RefId;
                //    }
                //}

                if (!isArray)
                {
                    if (string.IsNullOrEmpty(filedValue))
                    {
                        fieldValueType.type = "string";
                    }
                    else
                    {
                        if (!filedValue.Contains(',') && decimal.TryParse(filedValue, out decimal _))
                        {
                            fieldValueType.type = "number";
                        }
                        else if (bool.TryParse(filedValue.ToLower(), out bool _))
                        {
                            fieldValueType.type = "bool";
                        }
                        else if (fieldName.Contains("{}"))
                        {
                            fieldValueType.type = "json";
                        }
                        else
                        {
                            fieldValueType.type = "string";
                        }
                    }
                    fieldValueTypes.Add(fieldValueType);
                }
                else
                {
                    string[] values = SplitValueToArray(filedValue, false);
                    int lenVal = 0;
                    string longestValue = "";
                    foreach (string val in values)
                    {
                        if (lenVal < val.Length)
                        {
                            lenVal = val.Length;
                            longestValue = val;
                        }
                    }
                    if (values.Length > 0)
                    {
                        if (string.IsNullOrEmpty(longestValue))
                        {
                            fieldValueType.type = "array-string";
                        }
                        else
                        {
                            if (!longestValue.Contains(',') && decimal.TryParse(longestValue, out decimal _))
                            {
                                fieldValueType.type = "array-number";
                            }
                            else if (bool.TryParse(longestValue.ToLower(), out bool _))
                            {
                                fieldValueType.type = "array-bool";
                            }
                            else
                            {
                                fieldValueType.type = "array-string";
                            }
                        }
                        fieldValueTypes.Add(fieldValueType);
                    }
                    else
                    {
                        fieldValueType.type = "array-string";
                        fieldValueTypes.Add(fieldValueType);
                    }
                }
            }

            return fieldValueTypes;
        }

        public static void WriteFile(string pFolderPath, string pFileName, string pContent)
        {
            if (!Directory.Exists(pFolderPath))
                Directory.CreateDirectory(pFolderPath);

            string filePath = $"{pFolderPath}\\{pFileName}";
            if (!System.IO.File.Exists(filePath))
                using (System.IO.File.Create(filePath)) { }

            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine(pContent);
                sw.Close();
            }
        }

        public static void WriteFile(string pFilePath, string pContent)
        {
			if (!File.Exists(pFilePath))
                using (File.Create(pFilePath)) { }

            using (var sw = new StreamWriter(pFilePath))
            {
                sw.WriteLine(pContent);
                sw.Close();
            }
        }

        public static string ConvertCSVToJson<T>(string pFilePath)
        {
            using (TextReader fileReader = System.IO.File.OpenText(pFilePath))
            {
                var cultureInfo = new CultureInfo("es-ES", false);
                var csvReader = new CsvReader(fileReader, cultureInfo);
                var records = csvReader.GetRecords<T>().ToList();

                string jsonContent = JsonConvert.SerializeObject(records);
                return jsonContent;
            }
        }

        private static void ExportLocalizationSheet(IWorkbook pWorkBook, string pSheetName, string pExportFolder, string pFileName, List<ID> pAdditionalIds = null)
        {
            var sheet = pWorkBook.GetSheet(pSheetName);
            if (sheet.IsNull() || sheet.LastRowNum == 0)
            {
                MessageBox.Show($@"{pSheetName} is empty!", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idStrings = new List<string>();
            var textDict = new Dictionary<string, List<string>>();
            var firstRow = sheet.GetRow(0);
            int maxCellNum = firstRow.LastCellNum;

            string mergeCellValue = "";
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                var rowData = sheet.GetRow(row);
                if (rowData == null)
                    continue;
                for (int col = 0; col < maxCellNum; col++)
                {
                    var celData = rowData.GetCell(col);
                    var filedValue = celData == null ? "" : celData.ToString();
                    var fieldName = sheet.GetRow(0).GetCell(col).ToString();
                    if (celData != null && celData.IsMergedCell && !string.IsNullOrEmpty(filedValue))
                        mergeCellValue = filedValue;
                    if (celData != null && celData.IsMergedCell && string.IsNullOrEmpty(filedValue))
                        filedValue = mergeCellValue;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        if (col == 0 && row > 0)
                        {
                            if (string.IsNullOrEmpty(filedValue))
                            {
                                //MessageBox.Show(string.Format("Sheet {0}: IdString can not be empty!", pSheetName), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            idStrings.Add(filedValue);
                        }
                        else if (col == 1 && row > 0)
                        {
                            if (string.IsNullOrEmpty(filedValue) || pAdditionalIds == null)
                                continue;
                            bool existId = false;
                            foreach (var id in pAdditionalIds)
                                if (id.Key.Trim() == filedValue.Trim())
                                {
                                    filedValue = id.Value.ToString();
                                    idStrings[idStrings.Count - 1] = $"{idStrings[idStrings.Count - 1]}_{id.Value}";
                                    existId = true;
                                    break;
                                }

                            if (!existId)
                                idStrings[idStrings.Count - 1] = $"{idStrings[idStrings.Count - 1]}_{filedValue}";
                        }
                        else if (col > 1 && row > 0)
                        {
                            if (!textDict.ContainsKey(fieldName))
                                textDict.Add(fieldName, new List<string>());
                            textDict[fieldName].Add(filedValue);
                        }
                    }
                    else
                    {
                        Console.Write(col);
                    }
                }
            }

            //Build id list
            var idBuilder = new StringBuilder();
            if (idStrings.Count > 0)
            {
                idBuilder.Append("\tpublic const int ");
                for (int i = 0; i < idStrings.Count; i++)
                {
                    if (i < idStrings.Count - 1)
                        idBuilder.Append($"{idStrings[i].ToUpper()} = {i}, ");
                    else
                        idBuilder.Append($"{idStrings[i].ToUpper()} = {i};");
                }
            }
            var idBuilder2 = new StringBuilder();
            idBuilder2.Append("\tpublic enum ID { NONE = -1,");
            for (int i = 0; i < idStrings.Count; i++)
            {
                idBuilder2.Append($" {idStrings[i].ToUpper()} = {i},");
            }
            idBuilder2.Append(" }");

            //Build idString dictionary
            var idStringDictBuilder = new StringBuilder();
            idStringDictBuilder.Append("\tpublic static readonly string[] idString = new string[] {");
            foreach (string id in idStrings)
            {
                idStringDictBuilder.Append($" \"{id}\",");
            }
            idStringDictBuilder.Append(" };");

            //Build language list
            var allLanguagePackBuilder = new StringBuilder();
            foreach (var listText in textDict)
            {
                var languagePackContent = new StringBuilder();
                languagePackContent.Append("\tpublic static readonly string[] " + listText.Key + " = new string[]");
                languagePackContent.Append("\n\t{\n");
                for (int i = 0; i < listText.Value.Count; i++)
                {
                    string text = listText.Value[i].Replace("\n", "\\n");

                    if (text.Contains("Active Skill"))
                    {
                        Console.WriteLine(text);
                    }

                    if (i > 0)
                        languagePackContent.Append("\n\t\t");
                    else
                        languagePackContent.Append("\t\t");
                    languagePackContent.Append($"\"{text}\"");

                    if (i < listText.Value.Count - 1)
                        languagePackContent.Append(", ");
                }
                languagePackContent.Append("\n\t};");

                if (listText.Key != textDict.Last().Key)
                    allLanguagePackBuilder.Append(languagePackContent).AppendLine();
                else
                    allLanguagePackBuilder.Append(languagePackContent);
                allLanguagePackBuilder.AppendLine();
            }

            //Build language dictionary
            var languageFilesBuilder = new StringBuilder();
            languageFilesBuilder.Append("\tpublic static readonly Dictionary<string, string[]> language = new Dictionary<string, string[]>() { ");
            foreach (var listText in textDict)
            {
                languageFilesBuilder.Append($" {"{"} \"{listText.Key}\", {listText.Key} {"},"}");
            }
            languageFilesBuilder.Append(" };\n");
            languageFilesBuilder.Append($"\tpublic static readonly string DefaultLanguage = \"{textDict.First().Key}\";");

            //Write file
            string fileTemplateContent = System.IO.File.ReadAllText(LOCALIZED_TEXT_TEMPLATE);
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_ENUM", idBuilder2.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_CONST", idBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_STRING", idStringDictBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_LIST", allLanguagePackBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY", languageFilesBuilder.ToString());
			fileTemplateContent = fileTemplateContent.Replace("LOCALIZATION_FOLDER", Config.Settings.GetLocalizationFolder());
			WriteFile(pExportFolder, pFileName, fileTemplateContent);

            MessageBox.Show($@"Export {pSheetName} successfully!", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SelectFolder(TextBox pTextBox)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    pTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        public static string[] SplitValueToArray(string pValue, bool pIncludeColon = true)
        {
            string[] splits = new[] { ":", "|", "\r\n", "\r", "\n" };
            if (!pIncludeColon)
                splits = new[] { "|", "\r\n", "\r", "\n" };
            string[] result = pValue.Split(splits, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
            return result;
        }

        public static IEnumerable<ID> SortIDsByLength(IEnumerable<ID> list)
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = from s in list
                orderby s.Key.Length descending
                select s;
            return sorted;
        }

        public static Dictionary<string, int> SortIDsByLength(Dictionary<string, int> dict)
        {
            var sortedDict = dict.OrderBy(x => x.Key.Length).ToDictionary(x => x.Key, x => x.Value);
            return sortedDict;
        }

        public static string ConvertFormulaCell(ICell pCell)
        {
            if (pCell.CellType == CellType.Formula)
            {
                if (pCell.CachedFormulaResultType == CellType.Numeric)
                    return pCell.NumericCellValue.ToString();
                if (pCell.CachedFormulaResultType == CellType.String)
                    return pCell.StringCellValue;
                if (pCell.CachedFormulaResultType == CellType.Boolean)
                    return pCell.BooleanCellValue.ToString();
            }
            return null;
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c == ' ')
                    sb.Append('_');
                else if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z' || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        
        public static string ToCapitalizeEachWord(string pString)
        {
            // Creates a TextInfo based on the "en-US" culture.
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(pString);
        }
        
        public static string RemoveLast(string text, string character)
        {
            if (text.Length < 1) return text;
            int index = text.LastIndexOf(character, StringComparison.Ordinal);
            return index >= 0 ? text.Remove(index, character.Length) : text;
        }

        public static string ConvertToNestedJson(List<JObject> original)
        {
            // Parse the original JSON into a JArray
            // var original = JArray.Parse(json);

            // Create a new JArray for the converted JSON
            var converted = new List<JObject>();

            // Iterate over all JObjects in the original JArray
            foreach (JObject obj in original)
            {
                // Create a new JObject for the converted JSON
                var newObj = new JObject();
                string root = "";

                // Iterate over all properties of the original JObject
                foreach (var property in obj.Properties())
                {
                    // Split the property name into parts
                    var parts = property.Name.Split('.');

                    // Create nested JObjects for each part except the last one
                    var current = newObj;
                    for (int i = 0; i < parts.Length - 1; i++)
                    {
                        if (current[parts[i]] == null)
                        {
                            current[parts[i]] = new JObject();
                        }
                        current = (JObject)current[parts[i]];
                    }

                    // Add the value to the last part
                    current[parts[parts.Length - 1]] = property.Value;
                    root = parts[0];
                }

                // Add the new JObject to the converted JArray
                converted.Add(newObj);
            }

            var combineJson = CombineJsonObjects(converted);

            return combineJson.ToString(Formatting.None);
        }

        public static JObject CombineJsonObjects(List<JObject> jsonArray)
        {
            var combined = new JObject();

            foreach (var obj in jsonArray)
            {
                foreach (var property in obj.Properties())
                {
                    // Check if the property value is a JObject
                    if (property.Value is JObject innerObj)
                    {
                        if (combined[property.Name] == null)
                        {
                            combined[property.Name] = new JObject();
                        }

                        foreach (var innerProperty in innerObj.Properties())
                        {
                            // Check if the inner property value is a JObject
                            if (innerProperty.Value is JObject innerInnerObj)
                            {
                                if (((JObject)combined[property.Name])[innerProperty.Name] == null)
                                {
                                    ((JObject)combined[property.Name])[innerProperty.Name] = new JObject();
                                }

                                foreach (var innerInnerProperty in innerInnerObj.Properties())
                                {
                                    ((JObject)((JObject)combined[property.Name])[innerProperty.Name])[innerInnerProperty.Name] = innerInnerProperty.Value;
                                }
                            }
                            else
                            {
                                // If the inner property value is not a JObject, just copy it
                                ((JObject)combined[property.Name])[innerProperty.Name] = innerProperty.Value;
                            }
                        }
                    }
                    else
                    {
                        // If the property value is not a JObject, just copy it
                        combined[property.Name] = property.Value;
                    }
                }
            }

            return combined;
        }
        
        public static Encryption CreateEncryption(string text)
        {
            string[] keysString = text.Trim().Replace(" ", "").Split(',');
            if (keysString.Length > 0)
            {
                bool validKey = true;
                byte[] keysByte = new byte[keysString.Length];
                for (int i = 0; i < keysString.Length; i++)
                {
                    if (byte.TryParse(keysString[i], out byte output))
                    {
                        keysByte[i] = output;
                    }
                    else
                    {
                        validKey = false;
                    }
                }
                if (validKey)
                    return new Encryption(keysByte);
            }
            return null;
        }

		private static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
		private static string CLIENT_ID = "871414866606-7b9687cp1ibjokihbbfl6nrjr94j14o8.apps.googleusercontent.com";
		private static string CLIENT_SECRET = "zF_J3qHpzX5e8i2V-ZEvOdGV";
		public static UserCredential AuthenticateGoogleStore()
        {
			UserCredential credential;

			var clientSecrets = new ClientSecrets();
			clientSecrets.ClientId = CLIENT_ID;
			clientSecrets.ClientSecret = CLIENT_SECRET;

			// The file token.json stores the user's access and refresh tokens, and is created
			// automatically when the authorization flow completes for the first time.
			credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
				clientSecrets,
				Scopes,
				"user",
				CancellationToken.None,
				new FileDataStore(Config.GetSaveDirectory(), true)).Result;

			Console.WriteLine("Credential file saved to: " + Config.GetSaveDirectory());
            return credential;
		}

		/// <summary>
		/// Helper method to convert column number to letter (e.g., 1 -> A, 2 -> B, ..., 26 -> Z, 27 -> AA)
		/// </summary>
		public static string GetColumnLetter(int columnNumber)
		{
			int dividend = columnNumber;
			string columnLetter = string.Empty;

			while (dividend > 0)
			{
				int modulo = (dividend - 1) % 26;
				columnLetter = (char)(65 + modulo) + columnLetter; // 65 is the ASCII value for 'A'
				dividend = (dividend - modulo) / 26;
			}

			return columnLetter;
		}
	}
}