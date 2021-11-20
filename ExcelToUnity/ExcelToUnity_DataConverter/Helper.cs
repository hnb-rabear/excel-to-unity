using ChoETL;
using CsvHelper;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string cellStr = "";
            if (cell.CellType == CellType.Formula)
            {
                if (cell.CachedFormulaResultType == CellType.Numeric)
                    cellStr = cell.NumericCellValue.ToString();
                else if (cell.CachedFormulaResultType == CellType.String)
                    cellStr = cell.StringCellValue.ToString();
                else if (cell.CachedFormulaResultType == CellType.Boolean)
                    cellStr = cell.BooleanCellValue.ToString();
                else
                    cellStr = cell.ToString();
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
            using (FileStream file = new FileStream(pFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return new XSSFWorkbook((Stream)file);
            }
        }

        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = Newtonsoft.Json.Linq.JToken.Parse(strInput);
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
            else
            {
                return false;
            }
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
            ISheet sheet = pWorkBook.GetSheet(pSheetName);
            IRow rowData = sheet.GetRow(0);
            if (rowData.IsNull())
                return null;

            int lastCellNum = rowData.LastCellNum;
            var fieldsName = new string[lastCellNum];
            var fieldsValue = new string[lastCellNum];
            for (int col = 0; col < rowData.LastCellNum; col++)
            {
                ICell cell = rowData.GetCell(col);
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
                        ICell cell = rowData.GetCell(col);
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
                        decimal valNumber = 0;
                        bool valBool = false;
                        if (!filedValue.Contains(',') && decimal.TryParse(filedValue, out valNumber))
                        {
                            fieldValueType.type = "number";
                        }
                        else if (bool.TryParse(filedValue.ToLower(), out valBool))
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
                    for (int l = 0; l < values.Length; l++)
                    {
                        if (lenVal < values[l].Length)
                        {
                            lenVal = values[l].Length;
                            longestValue = values[l];
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
                            decimal valNumber = 0;
                            bool valBool = false;
                            if (!longestValue.Contains(',') && decimal.TryParse(longestValue, out valNumber))
                            {
                                fieldValueType.type = "array-number";
                            }
                            else if (bool.TryParse(longestValue.ToLower(), out valBool))
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

            string filePath = string.Format("{0}\\{1}", pFolderPath, pFileName);
            if (!File.Exists(filePath))
                using (File.Create(filePath)) { }

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(pContent);
                sw.Close();
            }
        }

        public static void WriteFile(string pFilePath, string pContent)
        {
            if (!File.Exists(pFilePath))
                using (File.Create(pFilePath)) { }

            using (StreamWriter sw = new StreamWriter(pFilePath))
            {
                sw.WriteLine(pContent);
                sw.Close();
            }
        }

        public static string ConvertCSVToJson<T>(string pFilePath)
        {
            using (TextReader fileReader = File.OpenText(pFilePath))
            {
                CultureInfo myCIintl = new CultureInfo("es-ES", false);
                var csvReader = new CsvReader(fileReader, myCIintl);
                var records = csvReader.GetRecords<T>().ToList();

                string jsonContent = JsonConvert.SerializeObject(records);
                return jsonContent;
            }
        }

        private static void ExportLocalizationSheet(IWorkbook pWorrkBook, string pSheetName, string pExportFolder, string pFileName, List<ID> pAdditionalIds = null)
        {
            var sheet = pWorrkBook.GetSheet(pSheetName);
            if (sheet.IsNull() || sheet.LastRowNum == 0)
            {
                MessageBox.Show(pSheetName + " is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idStrings = new List<string>();
            var textDict = new Dictionary<string, List<string>>();
            var fristRow = sheet.GetRow(0);
            int maxCellNum = fristRow.LastCellNum;

            string mergeCellValue = "";
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                var rowData = sheet.GetRow(row);
                if (rowData != null)
                {
                    for (int col = 0; col < maxCellNum; col++)
                    {
                        var celData = rowData.GetCell(col);
                        var fileldValue = celData == null ? "" : celData.ToString();
                        var fieldName = sheet.GetRow(0).GetCell(col).ToString();
                        if (celData != null && celData.IsMergedCell && !string.IsNullOrEmpty(fileldValue))
                            mergeCellValue = fileldValue;
                        if (celData != null && celData.IsMergedCell && string.IsNullOrEmpty(fileldValue))
                            fileldValue = mergeCellValue;
                        if (!string.IsNullOrEmpty(fieldName))
                        {
                            if (col == 0 && row > 0)
                            {
                                if (string.IsNullOrEmpty(fileldValue))
                                {
                                    //MessageBox.Show(string.Format("Sheet {0}: IdString can not be empty!", pSheetName), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                                idStrings.Add(fileldValue);
                            }
                            else if (col == 1 && row > 0)
                            {
                                if (!string.IsNullOrEmpty(fileldValue) && pAdditionalIds != null)
                                {
                                    bool existId = false;
                                    foreach (var id in pAdditionalIds)
                                        if (id.Key.Trim() == fileldValue.Trim())
                                        {
                                            fileldValue = id.Value.ToString();
                                            idStrings[idStrings.Count - 1] = string.Format("{0}_{1}", idStrings[idStrings.Count - 1], id.Value);
                                            existId = true;
                                            break;
                                        }

                                    if (!existId)
                                    {
                                        idStrings[idStrings.Count - 1] = string.Format("{0}_{1}", idStrings[idStrings.Count - 1], fileldValue);
                                    }
                                }
                            }
                            else if (col > 1 && row > 0)
                            {
                                if (!textDict.ContainsKey(fieldName))
                                    textDict.Add(fieldName, new List<string>());
                                textDict[fieldName].Add(fileldValue);
                            }
                        }
                        else
                        {
                            Console.Write(col);
                        }
                    }
                }
            }

            //Build id list
            var idBuilder = new StringBuilder();
            if (idStrings.Count > 0)
            {
                idBuilder.Append(string.Format("\tpublic const int "));
                for (int i = 0; i < idStrings.Count; i++)
                {
                    if (i < idStrings.Count - 1)
                        idBuilder.Append(string.Format("{0} = {1}, ", idStrings[i].ToUpper(), i));
                    else
                        idBuilder.Append(string.Format("{0} = {1};", idStrings[i].ToUpper(), i));
                }
            }
            var idBuilder2 = new StringBuilder();
            idBuilder2.Append("\tpublic enum ID { NONE = -1,");
            for (int i = 0; i < idStrings.Count; i++)
            {
                idBuilder2.Append(string.Format(" {0} = {1},", idStrings[i].ToUpper(), i));
            }
            idBuilder2.Append(" }");

            //Build idString dictionary
            var idStringDictBuilder = new StringBuilder();
            idStringDictBuilder.Append("\tpublic static readonly string[] idString = new string[] {");
            for (int i = 0; i < idStrings.Count; i++)
            {
                idStringDictBuilder.Append(string.Format(" \"{0}\",", idStrings[i]));
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
                    languagePackContent.Append(string.Format("\"{0}\"", text));

                    if (i < listText.Value.Count - 1)
                        languagePackContent.Append(", ");
                }
                languagePackContent.Append("\n\t};");

                if (listText.Key != textDict.Last().Key)
                    allLanguagePackBuilder.Append(languagePackContent.ToString()).AppendLine();
                else
                    allLanguagePackBuilder.Append(languagePackContent.ToString());
                allLanguagePackBuilder.AppendLine();
            }

            //Build language dictionary
            var languageDictBuilder = new StringBuilder();
            languageDictBuilder.Append("\tpublic static readonly Dictionary<string, string[]> language = new Dictionary<string, string[]>() { ");
            foreach (var listText in textDict)
            {
                languageDictBuilder.Append(string.Format(" {0} \"{1}\", {2} {3}", "{", listText.Key, listText.Key, "},"));
            }
            languageDictBuilder.Append(" };\n");
            languageDictBuilder.Append(string.Format("\tpublic static readonly string defaultLanguage = \"{0}\";", textDict.First().Key));

            //Write file
            string fileTemplateContent = File.ReadAllText(LOCALIZED_TEXT_TEMPLATE);
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_ENUM", idBuilder2.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_CONST", idBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_STRING", idStringDictBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_LIST", allLanguagePackBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY", languageDictBuilder.ToString());
            WriteFile(pExportFolder, pFileName, fileTemplateContent);

            MessageBox.Show("Export " + pSheetName + " successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SelectFolder(TextBox pTextBox)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    pTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        public static string[] SplitValueToArray(string pValue, bool pIncludeColon = true)
        {
            string[] result = null;
            if (pIncludeColon && pValue.Contains(":"))
                result = pValue.Split(':');
            else if (pValue.Contains("|"))
                result = pValue.Split('|');
            else if (pValue.Contains("\n") || pValue.Contains("\r\n"))
                result = pValue.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            else
                result = new string[] { pValue };
            return result;
        }

        public static IEnumerable<ID> SortIDsByLength(IEnumerable<ID> e)
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = from s in e
                         orderby s.Key.Length descending
                         select s;
            return sorted;
        }

        public static string ConvertFormulaCell(ICell pCell)
        {
            if (pCell.CellType == CellType.Formula)
            {
                if (pCell.CachedFormulaResultType == CellType.Numeric)
                    return pCell.NumericCellValue.ToString();
                else if (pCell.CachedFormulaResultType == CellType.String)
                    return pCell.StringCellValue.ToString();
                else if (pCell.CachedFormulaResultType == CellType.Boolean)
                    return pCell.BooleanCellValue.ToString();
            }
            return null;
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c == ' ')
                    sb.Append('_');
                else if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
