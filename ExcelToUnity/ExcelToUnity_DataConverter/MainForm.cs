using ChoETL;
using ExcelToUnity_DataConverter.Entities;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelToUnity_DataConverter
{
    public partial class MainForm : Form
    {
        #region Internal Class

        public class Sheet
        {
            public Sheet(string name)
            {
                SheetName = name;
                Check = true;
            }

            public string SheetName { get; set; }
            public bool Check { get; set; }
        }

        [Serializable]
        public class Att
        {
            //Main setup
            public int id;
            public float value;
            public float increase;
            public float unlock;
            public float max;

            //Optional setup
            public string idString; //Sometime id is not integer because of error in Excel Data, this field use to point out which data is error
            public float[] values; //Sometime value is defined by multi values which are used in diffirent cases 
            public float[] increases; //Sometime increases is define by multi values which are used for diffirent purposes eg. level and rarity
            public float[] maxes; //Sometime maxes is defined by multi values which are used for diffirent purposes eg. max-level, max-rarity
            public float[] unlocks;
            public string valueString; //Sometime value is not number because of error in Excel Data or you want it like that

            public string GetJsonString()
            {
                List<string> list = new List<string>();
                list.Add((string.Format("\"id\":{0}", id)));
                if (value != 0) list.Add(string.Format("\"value\":{0}", value));
                if (increase != 0) list.Add(string.Format("\"increase\":{0}", increase));
                if (unlock != 0) list.Add(string.Format("\"unlock\":{0}", unlock));
                if (max != 0) list.Add(string.Format("\"max\":{0}", max));
                if (!string.IsNullOrEmpty(valueString) && valueString != value.ToString()) list.Add(string.Format("\"valueString\":\"{0}\"", valueString));
                if (id == -1 && !string.IsNullOrEmpty(idString)) list.Add(string.Format("\"idString\":\"{0}\"", idString));
                if (values != null) list.Add(string.Format("\"values\":{0}", JsonConvert.SerializeObject(values)));
                if (increases != null) list.Add(string.Format("\"increases\":{0}", JsonConvert.SerializeObject(increases)));
                if (maxes != null) list.Add(string.Format("\"maxes\":{0}", JsonConvert.SerializeObject(maxes)));
                if (unlocks != null) list.Add(string.Format("\"unlocks\":{0}", JsonConvert.SerializeObject(unlocks)));
                return "{" + string.Join(",", list.ToArray()) + "}";
            }
        }

        public class ConstantBuilder : IComparable<ConstantBuilder>
        {
            public string name;
            public string value;
            public string valueType;
            public string comment;

            public int CompareTo(ConstantBuilder other)
            {
                return name.CompareTo(other.name);
            }
        }

        #endregion

        //=================================

        #region Constants

        private const string IDS_CS_TEMPLATE = "Resources\\IDsTemplate.txt";
        private const string CONSTANTS_CS_TEMPLATE = "Resources\\ConstantsTemplate.txt";
        private const string SETTINGS_CS_TEMPATE = "Resources\\SettingsTemplate.txt";
        private const string LOCALIZATION_TEMPLATE = "Resources\\LOCALIZATION_TEMPLATE.txt";
        private const string LOCALIZATION_TEMPLATE_V2 = "Resources\\LOCALIZATION_TEMPLATE_V2.txt";
        private const string LOCALIZATION_TEXT_TEMPLATE = "Resources\\LOCALIZATION_TEXT_TEMPLATE.txt";
        private const string LOCALIZATION_MANAGER_TEMPLATE = "Resources\\LOCALIZATION_MANAGER_TEMPLATE.txt";
        private const string IDS_SHEET = "IDs";
        private const string CONSTANTS_SHEET = "Constants";
        private const string SETTINGS_SHEET = "Settings";
        private const string LOCALIZATION_SHEET = "Localization";
        private const string TEMP = "Temp";

        #endregion

        //==================================

        #region Members

        private List<Sheet> m_Sheets = new List<Sheet>();
        private List<ID> m_AllIds = new List<ID>();
        private List<ID> m_AllIDsSorted; //List sorted by length will be used for linked data, for IDs whih have prefix that is exaclty same with another ID
        private Dictionary<string, StringBuilder> m_IDsBuilderDict = new Dictionary<string, StringBuilder>();
        private Dictionary<string, StringBuilder> m_ConstantsBuilderDict = new Dictionary<string, StringBuilder>();
        private Dictionary<string, LocalizationBuilder> m_LocalizationsDict = new Dictionary<string, LocalizationBuilder>();
        private BindingList<FileEntity> m_ExcelFilesBind = new BindingList<FileEntity>();
        private IWorkbook m_WorkBook;
        private Encryption m_Encryption;
        private List<string> m_LocalizedSheetsExported;
        private List<string> m_LocalizedLanguages;

        ////--------- Setup for single files
        /// <summary>
        /// No Merge IDs Sheets
        /// No Merge Constants Sheets
        /// No Merge Localizations Sheets
        /// </summary>
        private bool m_SeperateConstants1 = false;
        /// <summary>
        /// Encrypt json data
        /// </summary>
        private bool m_EncryptData1 = false;
        private bool m_MergeJsonsIntoSingleJson1 = false; // Merge all json data to an json array object

        #endregion

        //=========================================

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        //=============================================================

        #region Private

        private void ClearCaches()
        {
            m_IDsBuilderDict = new Dictionary<string, StringBuilder>();
            m_ConstantsBuilderDict = new Dictionary<string, StringBuilder>();
            m_LocalizationsDict = new Dictionary<string, LocalizationBuilder>();
            ClearLog();
        }

        private string ConvertSheetToJson(IWorkbook pWorkBook, string pSheetName, string pFileName, bool pEncrypt, bool pWriteFile)
        {
            var fieldValueTypes = Helper.GetFieldValueTypes(pWorkBook, pSheetName);
            if (fieldValueTypes == null)
                return "{}";
            return ConvertSheetToJson(pWorkBook, pSheetName, pFileName, fieldValueTypes, pEncrypt, pWriteFile);
        }

        private void SetupConfigFolders(TextBox pTextBox, ref string pFolderPath)
        {
            if (string.IsNullOrEmpty(pTextBox.Text.Trim()))
                return;

            if (pTextBox.Text.Trim() == pFolderPath)
                return;

            pFolderPath = pTextBox.Text.Trim();

            Config.Save();
        }

        private void LoadWorkBook()
        {
            m_IDsBuilderDict = new Dictionary<string, StringBuilder>();
            DtgIDs.DataSource = null;
            DtgIDs.Rows.Clear();

            DtgSheets.DataSource = null;
            DtgSheets.Rows.Clear();
            DtgSheets.Refresh();
            m_Sheets = new List<Sheet>();

            if (!string.IsNullOrEmpty(txtInputXLSXFilePath.Text.Trim()))
            {
                m_WorkBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);
                if (m_WorkBook != null)
                {
                    for (int i = 0; i < m_WorkBook.NumberOfSheets; i++)
                    {
                        string sheetName = m_WorkBook.GetSheetName(i);
                        m_Sheets.Add(new Sheet(sheetName));
                    }
                    ExcludeSheets(ref m_Sheets);
                    DtgSheets.DataSource = m_Sheets;
                    DtgSheets.AutoResizeColumns();
                }
            }
        }

        private void LoadFileSettings()
        {
            Config.Init();
            LoadSettings();
            Active(true);
        }

        private void Active(bool pValue)
        {
            BtnAllInOne.Visible = pValue;
            BtnAddFile.Visible = pValue;
            if (!pValue)
                tabChangeLog.TabPages.RemoveByKey("tabPage4");
        }

        private bool LoadIDSheet(IWorkbook pWorkBook, string pIdsSheet)
        {
            ISheet sheet = pWorkBook.GetSheet(pIdsSheet);

            if (sheet.IsNull() || sheet.LastRowNum == 0)
            {
                Log(LogType.Warning, string.Format("Sheet {0} is empty", pIdsSheet));
                return false;
            }

            var ids = new List<ID>();
            var idsBuilders = new List<StringBuilder>();
            var idsEnumBuilders = new List<StringBuilder>();
            var idsEnumBuilderNames = new List<string>();
            var idsEnumBuilderIndexes = new List<int>();
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                IRow rowData = sheet.GetRow(row);
                if (rowData != null)
                {
                    for (int col = 0; col <= rowData.LastCellNum; col += 3)
                    {
                        ICell cellKey = rowData.GetCell(col);
                        if (cellKey != null)
                        {
                            bool ignore = false;
                            int index = col / 3;
                            StringBuilder sb = (index < idsBuilders.Count) ? idsBuilders[index] : new StringBuilder();
                            if (!idsBuilders.Contains(sb))
                            {
                                idsBuilders.Add(sb);
                            }
                            //Values row
                            if (row > 0)
                            {
                                string key = cellKey.ToString().Trim();
                                if (string.IsNullOrEmpty(key))
                                    continue;

                                //Value
                                ICell cellValue = rowData.GetCell(col + 1);
                                if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
                                {
                                    MessageBox.Show(string.Format("Sheet {0}: Key {1} does't have value!", sheet.SheetName, key), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue;
                                }

                                string valueStr = cellValue.ToString().Trim();
                                int.TryParse(valueStr, out int value);
                                ids.Add(new ID(key, value));
                                sb.Append("\tpublic const int ");
                                sb.Append(key);
                                sb.Append(" = ");
                                sb.Append(value);
                                sb.Append(";");

                                //Comment
                                ICell cellComment = rowData.GetCell(col + 2);
                                if (cellComment != null && !string.IsNullOrEmpty(cellComment.ToString().Trim()))
                                {
                                    string cellCommentFormula = Helper.ConvertFormulaCell(cellComment);
                                    if (cellCommentFormula != null)
                                        sb.Append(" /*").Append(cellCommentFormula).Append("*/");
                                    else
                                        sb.Append(" /*").Append(cellComment).Append("*/");
                                }

                                //Add to global keys
                                bool hadKey = false;
                                foreach (var k in m_AllIds)
                                    if (k.Key == cellKey.ToString().Trim() && k.Value == value)
                                    {
                                        hadKey = true;
                                        break;
                                    }
                                    else if (k.Key == cellKey.ToString().Trim() && k.Value != value)
                                    {
                                        MessageBox.Show(string.Format("Keys Conflicted!\nSHEET:{0}\nKEY:{1}", pIdsSheet, k.Key), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Log(LogType.Error, string.Format("Keys Conflicted!\nSHEET:{0}\nKEY:{1}", pIdsSheet, k.Key));
                                        break;
                                    }
                                if (!hadKey)
                                    m_AllIds.Add(new ID(key, value));
                            }
                            //Header row
                            else
                            {
                                if (cellKey.ToString().Contains("[enum]"))
                                {
                                    idsEnumBuilders.Add(sb);
                                    idsEnumBuilderNames.Add(cellKey.ToString().Replace("[enum]", ""));
                                    idsEnumBuilderIndexes.Add(index);
                                }

                                sb.Append("\t#region ")
                                    .Append(cellKey.ToString());
                            }
                            if (!ignore)
                                sb.Append(Environment.NewLine);
                        }
                    }
                }
            }

            //Build Ids Enum
            if (idsEnumBuilders.Count > 0)
            {
                for (int i = 0; i < idsEnumBuilders.Count; i++)
                {
                    var str = idsEnumBuilders[i].ToString()
                        .Replace("\r\n\tpublic const int ", "")
                        .Replace("\r\n", "")
                        .Replace(";", ",").Trim();
                    str = str.Remove(str.Length - 1);
                    var enumIndex = str.IndexOf("[enum]");
                    str = str.Remove(0, enumIndex + 6).Replace(",", ", ");

                    string enumName = idsEnumBuilderNames[i].Replace(" ", "_");

                    var enumBuilder = new StringBuilder();
                    enumBuilder.Append("\tpublic enum ")
                        .Append(enumName)
                        .Append(" { ")
                        .Append(str)
                        .Append(" }\n");
                    if (Config.Settings.keepOnlyEnumAsIDs)
                    {
                        var tempSb = new StringBuilder();
                        tempSb.Append("\t#region ")
                            .Append(enumName)
                            .Append(Environment.NewLine)
                            .Append(enumBuilder);
                        idsBuilders[idsEnumBuilderIndexes[i]] = tempSb;
                    }
                    else
                        idsBuilders[idsEnumBuilderIndexes[i]].Append(enumBuilder);
                }
            }

            //Add end region and add to final dictionary
            var builder = new StringBuilder();
            for (int i = 0; i < idsBuilders.Count; i++)
            {
                string str = idsBuilders[i].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    builder.Append(str);
                    builder.Append("\t#endregion");
                    if (i < idsBuilders.Count - 1)
                        builder.Append(Environment.NewLine);
                }
            }

            if (m_IDsBuilderDict.ContainsKey(pIdsSheet))
                m_IDsBuilderDict[pIdsSheet].AppendLine();
            else
                m_IDsBuilderDict.Add(pIdsSheet, new StringBuilder());
            m_IDsBuilderDict[pIdsSheet].Append(builder);

            DtgIDs.DataSource = null;
            DtgIDs.Rows.Clear();
            DtgIDs.Refresh();
            m_AllIds = m_AllIds.OrderBy(m => m.Key).ToList();
            DtgIDs.DataSource = m_AllIds;
            DtgIDs.AutoResizeColumns();

            return true;
        }

        private bool LoadIDSheetOnlyValue(IWorkbook pWorkBook, string pIdsSheet)
        {
            ISheet sheet = pWorkBook.GetSheet(pIdsSheet);

            if (sheet.IsNull() || sheet.LastRowNum == 0)
            {
                Log(LogType.Warning, string.Format("Sheet {0} is empty!", pIdsSheet));
                return false;
            }

            var ids = new List<ID>();
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                IRow rowData = sheet.GetRow(row);
                if (rowData != null)
                {
                    for (int col = 0; col <= rowData.LastCellNum; col += 3)
                    {
                        ICell cellIdName = rowData.GetCell(col);
                        if (cellIdName != null)
                        {
                            int index = col / 3;
                            if (row > 0)
                            {
                                try
                                {
                                    ICell cellIdValue = rowData.GetCell(col + 1);
                                    if (cellIdValue == null)
                                        continue;
                                    string key = cellIdName.ToString().Trim();
                                    int value = int.Parse(cellIdValue.ToString().Trim());
                                    ids.Add(new ID(key, value));

                                    //Add to global keys
                                    bool hadKey = false;
                                    foreach (var k in m_AllIds)
                                        if (k.Key == cellIdName.ToString().Trim() && k.Value == value)
                                        {
                                            hadKey = true;
                                            break;
                                        }
                                    if (!hadKey)
                                        m_AllIds.Add(new ID(key, value));
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }

            DtgIDs.DataSource = null;
            DtgIDs.Rows.Clear();
            DtgIDs.Refresh();
            m_AllIds = m_AllIds.OrderBy(m => m.Key).ToList();
            DtgIDs.DataSource = m_AllIds;
            DtgIDs.AutoResizeColumns();

            return true;
        }

        private void CreateIDsFile(string pExportFileName, string pContent)
        {
            StringBuilder sb = new StringBuilder();
            string fileContent = File.ReadAllText(IDS_CS_TEMPLATE);
            fileContent = fileContent.Replace("_IDS_CLASS_NAME_", pExportFileName);
            fileContent = fileContent.Replace("public const int _FIELDS_ = 0;", pContent/*mIDsBuilderDict.ToString()*/);
            fileContent = AddNamespace(fileContent);

            Helper.WriteFile(Config.Settings.outputConstantsFilePath, pExportFileName + ".cs", fileContent);
        }

        private void LoadConstantsSheet(IWorkbook workbook, string pContantsSheet)
        {
            ISheet sheet = workbook.GetSheet(pContantsSheet);

            if (sheet.IsNull() || sheet.LastRowNum == 0)
            {
                Log(LogType.Warning, string.Format("Sheet {0} is empty!", pContantsSheet));
                return;
            }

            List<ConstantBuilder> constants = new List<ConstantBuilder>();
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                IRow rowData = sheet.GetRow(row);
                if (rowData != null)
                {
                    string name = null;
                    string value = null;
                    string valueType = null;
                    string comment = null;
                    ICell cell = rowData.GetCell(0);//Name
                    if (cell != null)
                        name = cell.ToString().Trim();

                    cell = rowData.GetCell(1);//Type
                    if (cell != null)
                        valueType = cell.ToString().Trim();

                    cell = rowData.GetCell(2);//Value
                    if (cell != null)
                    {
                        string formulaCellValue = Helper.ConvertFormulaCell(cell);
                        if (formulaCellValue != null)
                            value = formulaCellValue;
                        else
                            value = cell.ToString().Trim();
                    }

                    cell = rowData.GetCell(3); //Comment 
                    if (cell != null)
                    {
                        string formulaCellValue = Helper.ConvertFormulaCell(cell);
                        if (formulaCellValue != null)
                            comment = formulaCellValue;
                        else
                            comment = cell.ToString().Trim();
                    }

                    if (name == null || value == null || valueType == null)
                        continue;

                    constants.Add(new ConstantBuilder()
                    {
                        name = name,
                        value = value,
                        valueType = valueType.ToLower(),
                        comment = comment,
                    });
                }
            }
            constants.Sort();

            var constantsSB = new StringBuilder("");
            for (int i = 0; i < constants.Count; i++)
            {
                string name = constants[i].name;
                string value = constants[i].value;
                string valueType = constants[i].valueType;
                string comment = constants[i].comment;
                string fieldStr = "";

                //Try to find references in ids list
                int intValue = 0;
                if (valueType == "int" && !int.TryParse(value, out intValue))
                {
                    int outValue = GetReferenceID(value, out bool found);
                    if (found)
                        value = outValue.ToString();
                }
                if (valueType == "int-array")
                {
                    string[] strValues = Helper.SplitValueToArray(value);
                    for (int j = 0; j < strValues.Length; j++)
                    {
                        intValue = 0;
                        //Try to find references in ids list
                        if (!int.TryParse(strValues[j].Trim(), out intValue))
                        {
                            GetReferenceID(null, out bool found);

                            for (int k = 0; k < m_AllIDsSorted.Count; k++)
                                if (strValues[j].Contains(m_AllIDsSorted[k].Key))
                                {
                                    strValues[j] = strValues[j].Replace(m_AllIDsSorted[k].Key, m_AllIDsSorted[k].Value.ToString());
                                    value = value.Replace(m_AllIDsSorted[k].Key, m_AllIDsSorted[k].Value.ToString());
                                    break;
                                }
                        }
                    }
                }

                switch (valueType)
                {
                    case "int":
                        fieldStr = string.Format("\tpublic const int {0} = {1};", name, value.Trim().ToString());
                        break;
                    case "float":
                        fieldStr = string.Format("\tpublic const float {0} = {1}f;", name, value.Trim().ToString());
                        break;
                    case "float-array":
                        string floatArrayStr = "";
                        string[] floatValues = Helper.SplitValueToArray(value);
                        for (int j = 0; j < floatValues.Length; j++)
                        {
                            if (j == floatValues.Length - 1)
                                floatArrayStr += floatValues[j] + "f";
                            else
                                floatArrayStr += floatValues[j] + "f, ";
                        }
                        fieldStr = string.Format("\tpublic static readonly float[] {0} = new float[{1}] {2} {3} {4};", name, floatValues.Length, "{", floatArrayStr, "}");
                        break;
                    case "int-array":
                        string intArrayStr = "";
                        string[] intValues = Helper.SplitValueToArray(value);
                        for (int j = 0; j < intValues.Length; j++)
                        {
                            if (j == intValues.Length - 1)
                                intArrayStr += intValues[j].Trim();
                            else
                                intArrayStr += intValues[j].Trim() + ", ";
                        }
                        fieldStr = string.Format("\tpublic static readonly int[] {0} = new int[{1}] {2} {3} {4};", name, intValues.Length, "{", intArrayStr, "}");
                        break;
                    case "vector2":
                        string[] vector2Values = Helper.SplitValueToArray(value);
                        fieldStr = string.Format("\tpublic static readonly Vector2 {0} = new Vector2({1}f, {2}f);", name, vector2Values[0].Trim(), vector2Values[1].Trim());
                        break;
                    case "vector3":
                        string[] vector3Values = Helper.SplitValueToArray(value);
                        fieldStr = string.Format("\tpublic static readonly Vector3 {0} = new Vector3({1}f, {2}f, {3}f);", name, vector3Values[0].Trim(), vector3Values[1].Trim(), vector3Values[2].Trim());
                        break;
                    case "string":
                        fieldStr = string.Format("\tpublic const string {0} = \"{1}\";", name, value.Trim().ToString());
                        break;
                    case "string-array":
                        {
                            string arrayStr = "";
                            string[] values = Helper.SplitValueToArray(value);
                            for (int j = 0; j < values.Length; j++)
                            {
                                if (j == values.Length - 1)
                                    arrayStr += "\"" + values[j].Trim() + "\"";
                                else
                                    arrayStr += "\"" + values[j].Trim() + "\", ";
                            }
                            fieldStr = string.Format("\tpublic static readonly string[] {0} = new string[{1}] {2} {3} {4};", name, values.Length, "{", arrayStr, "}");
                        }
                        break;
                }

                if (fieldStr != "")
                {
                    if (!string.IsNullOrEmpty(comment))
                        fieldStr += string.Format(" /*{0}*/", comment);
                    constantsSB.Append(fieldStr).AppendLine();
                }
            }

            if (m_ConstantsBuilderDict.ContainsKey(pContantsSheet))
                m_ConstantsBuilderDict[pContantsSheet].AppendLine();
            else
                m_ConstantsBuilderDict.Add(pContantsSheet, new StringBuilder());
            m_ConstantsBuilderDict[pContantsSheet].Append(constantsSB);
        }

        private void CreateConstantsFile(string content, string pExportFileName)
        {
            string fileContent = File.ReadAllText(CONSTANTS_CS_TEMPLATE);
            fileContent = fileContent.Replace("_CONST_CLASS_NAME_", pExportFileName);
            fileContent = fileContent.Replace("public const int _FIELDS_ = 0;", content);
            fileContent = AddNamespace(fileContent);

            Helper.WriteFile(Config.Settings.outputConstantsFilePath, pExportFileName + ".cs", fileContent);
            Log(LogType.Message, "Export " + pExportFileName + ".cs successfully!");
        }

        //private const string BUSINESS_SHEET = "Businesses";
        ///// <summary>
        ///// OLD VERSION
        ///// </summary>
        //private string ConvertToJsonBusinessSheet(IWorkbook workbook)
        //{
        //    ISheet sheet = workbook.GetSheet(BUSINESS_SHEET);
        //    int maxCell = 0;
        //    List<string> fields = new List<string>();
        //    List<BusinessEntity> businesses = new List<BusinessEntity>();

        //    for (int row = 0; row <= sheet.LastRowNum; row++)
        //    {
        //        IRow rowData = sheet.GetRow(row);
        //        if (rowData.IsNull())
        //            continue;
        //        if (row == 0)
        //        {
        //            for (int col = 0; col < rowData.LastCellNum; col++)
        //            {
        //                ICell cell = rowData.GetCell(col);
        //                if (cell != null)
        //                {
        //                    maxCell++;
        //                    fields.Add(cell.ToString());
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var business = new BusinessEntity();
        //            for (int col = 0; col < maxCell; col++)
        //            {
        //                ICell cell = rowData.GetCell(col);
        //                string cellValue = cell.ToString();
        //                switch (fields[col].ToString())
        //                {
        //                    case "id":
        //                        business.id = int.Parse(cellValue);
        //                        break;

        //                    case "name":
        //                        business.name = cell.ToString();
        //                        break;

        //                    case "worldId":
        //                        business.worldId = GetReferenceID(cellValue);
        //                        break;

        //                    case "basePrice":
        //                        business.basePrice = cellValue;
        //                        break;

        //                    case "priceModifier":
        //                        float.TryParse(cellValue, out business.priceModifier);
        //                        break;

        //                    case "profitSpeed":
        //                        float.TryParse(cellValue, out business.profitSpeed);
        //                        break;

        //                    case "profit":
        //                        business.profit = cellValue;
        //                        break;

        //                    case "state":
        //                        business.state = GetReferenceID(cellValue);
        //                        break;
        //                }
        //            }
        //            businesses.Add(business);
        //        }
        //    }

        //    string content = JsonConvert.SerializeObject(businesses);
        //    return content;
        //}

        //public string BUSINESS_GOAL_SHEET = "BusinessGoals";
        ///// <summary>
        ///// OLD VERSION
        ///// </summary>
        //private string ConvertToJsonBusinessGoalSheet(IWorkbook workbook)
        //{
        //    ISheet sheet = workbook.GetSheet(BUSINESS_GOAL_SHEET);
        //    int maxCell = 0;
        //    List<string> fields = new List<string>();
        //    List<BusinessGoalEntity> businessGoals = new List<BusinessGoalEntity>();

        //    for (int row = 0; row <= sheet.LastRowNum; row++)
        //    {
        //        IRow rowData = sheet.GetRow(row);
        //        if (row == 0)
        //        {
        //            for (int col = 0; col < rowData.LastCellNum; col++)
        //            {
        //                ICell cell = rowData.GetCell(col);
        //                if (cell != null)
        //                {
        //                    maxCell++;
        //                    fields.Add(cell.ToString());
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var bg = new BusinessGoalEntity();
        //            for (int col = 0; col < maxCell; col++)
        //            {
        //                ICell cell = rowData.GetCell(col);
        //                string value = cell != null ? cell.ToString() : "";
        //                switch (fields[col])
        //                {
        //                    case "id":
        //                        int.TryParse(value, out bg.id);
        //                        break;

        //                    case "businessId":
        //                        bg.businessId = GetReferenceID(value);
        //                        break;

        //                    case "required":
        //                        int.TryParse(value, out bg.required);
        //                        break;

        //                    case "name":
        //                        bg.name = value;
        //                        break;

        //                    case "rewardType":
        //                        bg.rewardType = GetReferenceID(value);
        //                        break;

        //                    case "rewardAmount":
        //                        int.TryParse(value, out bg.rewardAmount);
        //                        break;

        //                    case "rewardId":
        //                        bg.rewardId = GetReferenceID(value);
        //                        break;

        //                    case "businessGetReward":
        //                        bg.businessGetReward = GetReferenceID(value);
        //                        break;

        //                    case "worldId":
        //                        bg.worldId = GetReferenceID(value);
        //                        break;
        //                }
        //            }
        //            businessGoals.Add(bg);
        //        }
        //    }

        //    string content = JsonConvert.SerializeObject(businessGoals);
        //    return content;
        //}

        private int GetReferenceID(string pKey, out bool pFound)
        {
            if (m_AllIDsSorted == null || m_AllIDsSorted.Count == 0)
            {
                m_AllIDsSorted = new List<ID>();
                foreach (var id in Helper.SortIDsByLength(m_AllIds))
                    m_AllIDsSorted.Add(id);
            }

            if (!string.IsNullOrEmpty(pKey))
            {
                int value = 0;

                if (int.TryParse(pKey, out value))
                {
                    pFound = true;
                    return value;
                }

                foreach (var id in m_AllIDsSorted)
                    if (id.Key == pKey.Trim())
                    {
                        pFound = true;
                        return id.Value;
                    }
            }

            pFound = false;
            return 0;
        }

        private bool CheckExistID(string pKey)
        {
            foreach (var id in m_AllIds)
                if (id.Key == pKey.Trim())
                    return true;
            return false;
        }

        private void CreateJsonFromCSV(string pDataPath)
        {
            using (var fs = new FileStream(pDataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs))
                {

                }
            }
        }

        private string ConvertSheetToJson(IWorkbook workBook, string sheetName, string ouputFile, List<FieldValueType> fieldValueTypes, bool pEncrypt, bool pAutoWriteFile)
        {
            var unminizedFields = Config.Settings.GetUnminizedFields();

            ISheet sheet = workBook.GetSheet(sheetName);
            if (sheet.IsNull() || sheet.LastRowNum == 0)
            {
                Log(LogType.Warning, string.Format("Sheet {0} is empty!", sheet.SheetName));
                return null;
            }

            int lastCellNum = 0;
            string[] fields = null;
            string[] mergeValues = null;
            bool[] validCols = null;
            var rowContents = new List<RowContent>();

            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                IRow rowData = sheet.GetRow(row);
                if (rowData.IsNull())
                    continue;

                if (row == 0)
                {
                    lastCellNum = rowData.LastCellNum;
                    fields = new string[lastCellNum];
                    mergeValues = new string[lastCellNum];
                    validCols = new bool[lastCellNum];

                    for (int col = 0; col < lastCellNum; col++)
                    {
                        ICell cell = rowData.GetCell(col);
                        if (cell != null
                            && !string.IsNullOrEmpty(cell.StringCellValue)
                            && !cell.StringCellValue.Contains("[x]"))
                        {
                            validCols[col] = true;
                            fields[col] = cell.ToString().Trim();
                        }
                        else
                        {
                            validCols[col] = false;
                            fields[col] = "";
                        }
                        mergeValues[col] = "";
                    }
                }
                else
                {
                    var rowContent = new RowContent();
                    for (int col = 0; col < lastCellNum; col++)
                    {
                        ICell cell = rowData.GetCell(col);
                        string fieldName = fields[col];
                        string fieldValue = cell.ToCellString().Trim();

                        if (cell != null && cell.IsMergedCell && !string.IsNullOrEmpty(fieldValue))
                            mergeValues[col] = fieldValue;
                        if (cell != null && cell.IsMergedCell && string.IsNullOrEmpty(fieldValue))
                            fieldValue = mergeValues[col];

                        fieldName = fieldName.Replace(" ", "_");
                        rowContent.fieldNames.Add(fieldName);
                        rowContent.fieldValues.Add(fieldValue);
                        rowContent.fieldCells.Add(cell);
                    }
                    rowContents.Add(rowContent);
                }
            }

            string content = "[";
            for (int i = 0; i < rowContents.Count; i++)
            {
                var rowContent = rowContents[i];

                var attributes = new List<Att>();
                string fieldContentStr = "";
                bool rowIsEmpty = true; //Because Loading sheet sometime inculdes the empty rows, I don't know why it happen
                var inValidJson = new List<int>();

                for (int j = 0; j < rowContent.fieldNames.Count; j++)
                {
                    bool valid = validCols[j];
                    if (!valid)
                        continue;
                    string fieldName = rowContent.fieldNames[j];
                    string fieldValue = rowContent.fieldValues[j];
                    if (rowContent.fieldCells[j] != null && rowContent.fieldCells[j].CellType == CellType.Formula)
                    {
                        var cell = rowContent.fieldCells[j];
                        if (cell.CachedFormulaResultType == CellType.Numeric)
                            fieldValue = cell.NumericCellValue.ToString();
                        else if (cell.CachedFormulaResultType == CellType.String)
                            fieldValue = cell.StringCellValue.ToString();
                        else if (cell.CachedFormulaResultType == CellType.Boolean)
                            fieldValue = cell.BooleanCellValue.ToString();
                    }
                    bool isAttribute = fieldName.ToLower().Contains("attribute") && fieldName.Length <= 11;

                    //some weird situation, data has attribute field, therefore Converter will confuse it with attrbiute from Attribute System
                    //To fix this problem I have to add one more condition, the next field must be value
                    if (isAttribute)
                    {
                        if (j + 1 >= rowContent.fieldNames.Count)
                            isAttribute = false;
                        string nextFieldName = rowContent.fieldNames[j + 1];
                        if (!nextFieldName.ToLower().Contains("value") || nextFieldName.Length > 9)
                            isAttribute = false;
                    }

                    if (!string.IsNullOrEmpty(fieldValue))
                        rowIsEmpty = false;

                    //Attributes System includes fields: attribute, value/value[], increase/increase[], max/max[], unlock/unlock[]
                    //All these fields must lie on last of data sheet
                    if (isAttribute)
                    {
                        Att att = new Att();
                        att.id = GetReferenceID(fieldValue, out bool found);
                        att.idString = fieldValue;
                        while (j < rowContent.fieldNames.Count - 1)
                        {
                            fieldValue = rowContent.fieldValues[j + 1].Trim();
                            fieldName = rowContent.fieldNames[j + 1].Trim();
                            if (fieldName.ToLower().Contains("unlock"))
                            {
                                bool isArray = fieldName.Contains("[]");
                                j++;
                                if (!isArray)
                                {
                                    if (!float.TryParse(fieldValue, out att.unlock))
                                        att.unlock = GetReferenceID(fieldValue, out found);
                                }
                                else
                                {
                                    string[] inValues = Helper.SplitValueToArray(fieldValue, false);
                                    float[] outValues = new float[inValues.Length];
                                    for (int t = 0; t < inValues.Length; t++)
                                    {
                                        if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
                                            outValues[t] = GetReferenceID(inValues[t].Trim(), out found);
                                    }
                                    att.unlocks = outValues;
                                }
                            }
                            else if (fieldName.ToLower().Contains("increase"))
                            {
                                bool isArray = fieldName.Contains("[]");
                                j++;
                                if (!isArray)
                                {
                                    if (!float.TryParse(fieldValue, out att.increase))
                                        att.increase = GetReferenceID(fieldValue, out found);
                                }
                                else
                                {
                                    string[] inValues = Helper.SplitValueToArray(fieldValue, false);
                                    float[] outValues = new float[inValues.Length];
                                    for (int t = 0; t < inValues.Length; t++)
                                    {
                                        if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
                                            outValues[t] = GetReferenceID(inValues[t].Trim(), out found);
                                    }
                                    att.increases = outValues;
                                }
                            }
                            else if (fieldName.ToLower().Contains("value"))
                            {
                                bool isArray = fieldName.Contains("[]"); //If attribute value is array
                                j++;
                                if (!isArray)
                                {
                                    if (!float.TryParse(fieldValue, out att.value))
                                        att.value = GetReferenceID(fieldValue, out found);
                                    if (!found)
                                        att.valueString = fieldValue;
                                }
                                else
                                {
                                    string[] inValues = Helper.SplitValueToArray(fieldValue, false);
                                    float[] outValues = new float[inValues.Length];
                                    for (int t = 0; t < inValues.Length; t++)
                                    {
                                        if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
                                            outValues[t] = GetReferenceID(inValues[t].Trim(), out found);
                                    }
                                    if (outValues.Length == 1 && outValues[0] == 0)
                                        outValues = null;
                                    att.values = outValues;
                                }
                            }
                            else if (fieldName.ToLower().Contains("max"))
                            {
                                bool isArray = fieldName.Contains("[]");
                                j++;
                                if (!isArray)
                                {
                                    if (!float.TryParse(fieldValue, out att.max))
                                        att.max = GetReferenceID(fieldValue, out found);
                                }
                                else
                                {
                                    string[] inValues = Helper.SplitValueToArray(fieldValue, false);
                                    float[] outValues = new float[inValues.Length];
                                    for (int t = 0; t < inValues.Length; t++)
                                    {
                                        if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
                                            outValues[t] = GetReferenceID(inValues[t].Trim(), out found);
                                    }
                                    att.maxes = outValues;
                                }
                            }
                            else
                                break;
                        }
                        if (att.idString != "ATT_NULL" && !string.IsNullOrEmpty(att.idString))
                        {
                            attributes.Add(att);
                        }
                    }
                    else
                    {
                        bool importantField = unminizedFields.Contains(fieldName.Replace("[]", "").ToLower());

                        //Ignore empty field or field have value which equal 0
                        if (string.IsNullOrEmpty(fieldValue) || (fieldValue == "0" && !importantField))
                            continue;

                        for (int f = 0; f < fieldValueTypes.Count; f++)
                        {
                            //Find refrenced Id in string and convert it to number
                            if (fieldValueTypes[f].name == fieldName)
                            {
                                string fieldType = fieldValueTypes[f].type;
                                bool referecencedId = false;
                                if (fieldType == "string") //Find and replace string value with referenced ID
                                {
                                    int tryParse = -1;
                                    if (CheckExistID(fieldValue))
                                    {
                                        fieldType = "number";
                                        referecencedId = true;
                                    }
                                    else if (int.TryParse(fieldValue, out tryParse))
                                    {
                                        fieldType = "number";
                                        referecencedId = true;
                                    }
                                }
                                if (fieldType == "array-string") //Find and replace string value with referenced ID
                                {
                                    string[] arrayValue = Helper.SplitValueToArray(fieldValue, false);
                                    for (int k = 0; k < arrayValue.Length; k++)
                                    {
                                        if (CheckExistID(arrayValue[k].Trim()))
                                        {
                                            fieldType = "array-number";
                                            referecencedId = true;
                                            break;
                                        }
                                    }
                                }

                                switch (fieldType)
                                {
                                    case "number":
                                        if (referecencedId)
                                            fieldContentStr += string.Format("\"{0}\":{1},", fieldName, GetReferenceID(fieldValue, out bool found));
                                        else
                                            fieldContentStr += string.Format("\"{0}\":{1},", fieldName, fieldValue);
                                        break;

                                    case "string":
                                        fieldValue = fieldValue.Replace("\n", "\\n");
                                        fieldValue = fieldValue.Replace("\"", "\\\"");
                                        fieldContentStr += string.Format("\"{0}\":\"{1}\",", fieldName, fieldValue);
                                        break;

                                    case "bool":
                                        fieldContentStr += string.Format("\"{0}\":{1},", fieldName, fieldValue.ToLower());
                                        break;

                                    case "array-number":
                                        {
                                            fieldName = fieldName.Replace("[]", "");
                                            var arrayValue = Helper.SplitValueToArray(fieldValue, false);
                                            var arrayStr = "[";
                                            for (int k = 0; k < arrayValue.Length; k++)
                                            {
                                                string val = arrayValue[k].Trim();
                                                if (referecencedId)
                                                    val = GetReferenceID(val, out bool found).ToString();
                                                if (k == 0) arrayStr += val;
                                                else arrayStr += "," + val;
                                            }
                                            arrayStr += "]";
                                            fieldContentStr += string.Format("\"{0}\":{1},", fieldName, arrayStr);
                                        }
                                        break;

                                    case "array-string":
                                        {
                                            fieldName = fieldName.Replace("[]", "");
                                            var arrayValue = Helper.SplitValueToArray(fieldValue, false);
                                            var arrayStr = "[";
                                            for (int k = 0; k < arrayValue.Length; k++)
                                            {
                                                if (k == 0) arrayStr += string.Format("\"{0}\"", arrayValue[k].Trim());
                                                else arrayStr += string.Format(",\"{0}\"", arrayValue[k].Trim());
                                            }
                                            arrayStr += "]";
                                            fieldContentStr += string.Format("\"{0}\":{1},", fieldName, arrayStr);
                                        }
                                        break;

                                    case "array-bool":
                                        {
                                            fieldName = fieldName.Replace("[]", "");
                                            var arrayValue = Helper.SplitValueToArray(fieldValue, false);
                                            var arrayStr = "[";
                                            for (int k = 0; k < arrayValue.Length; k++)
                                            {
                                                if (k == 0) arrayStr += arrayValue[k].Trim().ToLower();
                                                else arrayStr += "," + arrayValue[k].Trim().ToLower();
                                            }
                                            arrayStr += "]";
                                            fieldContentStr += string.Format("\"{0}\":{1},", fieldName, arrayStr);
                                        }
                                        break;

                                    case "json":
                                        {
                                            fieldName = fieldName.Replace("{}", "");

                                            //Search Id in field value
                                            if (m_AllIDsSorted == null || m_AllIDsSorted.Count == 0)
                                            {
                                                m_AllIDsSorted = new List<ID>();
                                                foreach (var id in Helper.SortIDsByLength(m_AllIds))
                                                    m_AllIDsSorted.Add(id);
                                            }
                                            foreach (var id in m_AllIDsSorted)
                                            {
                                                if (fieldValue.Contains(id.Key))
                                                    fieldValue = fieldValue.Replace(id.Key, id.Value.ToString());
                                            }

                                            var tempObj = JsonConvert.DeserializeObject(fieldValue);
                                            var tempJsonStr = JsonConvert.SerializeObject(tempObj);

                                            fieldContentStr += string.Format("\"{0}\":{1},", fieldName, tempJsonStr);

                                            if (!Helper.IsValidJson(fieldValue))
                                            {
                                                inValidJson.Add(i);
                                                MessageBox.Show(string.Format("Invalid Json string at Sheet: {0} Field: {1} Row: {2}", sheetName, fieldName, i + 1),
                                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                Log(LogType.Error, string.Format("Invalid Json string at Sheet: {0} Field: {1} Row: {2}", sheetName, fieldName, i + 1));
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                if (attributes.Count > 0)
                {
                    fieldContentStr += "\"Attributes\":[";
                    for (int a = 0; a < attributes.Count; a++)
                    {
                        fieldContentStr += attributes[a].GetJsonString();
                        if (a < attributes.Count - 1)
                            fieldContentStr += ",";
                    }
                    fieldContentStr += "],";
                }
                fieldContentStr = RemoveLast(fieldContentStr, ",");

                if (!rowIsEmpty)
                    content += string.Format("{0}{1}{2}", "{", fieldContentStr, "},");
            }
            content = RemoveLast(content, ",");
            content += "]";

            //if (toList)
            //    content = string.Format("{0}\"list\":{1}{2}", "{", content, "}");

            if (content == "[]")
            {
                Log(LogType.Warning, string.Format("Sheet {0} is empty!", sheetName));
                return null;
            }
            string finalContent = content;
            if (pEncrypt && m_Encryption != null)
                finalContent = m_Encryption.EncryptValue(content);

            if (pAutoWriteFile)
            {
                Helper.WriteFile(Config.Settings.outputDataFilePath, ouputFile + ".txt", finalContent);
                if (pEncrypt && m_Encryption != null)
                    Log(LogType.Message, string.Format("Exported Sheet {0} as encrypted JSON data.", sheetName));
                else
                    Log(LogType.Message, string.Format("Exported Sheet {0} as JSON data.", sheetName));
            }
            return finalContent;
        }

        private string ExportSettingsSheetToScripableObject(IWorkbook workbook, string sheetName)
        {
            string nameSpace = Config.Settings._namespace;

            ISheet sheet = workbook.GetSheet(sheetName);
            var fieldsDict = new Dictionary<string, string>();
            var fields = new List<string>();

            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                IRow rowData = sheet.GetRow(row);
                if (rowData != null)
                {
                    string fieldStr = "";
                    string name = null;
                    string value = null;
                    string valueType = null;
                    ICell cell = rowData.GetCell(0);
                    if (cell != null)
                        name = cell.ToString().Trim();
                    cell = rowData.GetCell(1);
                    if (cell != null)
                        valueType = cell.ToString().Trim();
                    cell = rowData.GetCell(2);
                    if (cell != null)
                        value = cell.ToString().Trim();

                    if (name == null || value == null || valueType == null)
                        continue;

                    switch (valueType.ToLower())
                    {
                        case "int":
                        case "float":
                            fieldStr = string.Format("\"{0}\":{1}", name, value);
                            break;
                        case "bool":
                            fieldStr = string.Format("\"{0}\":{1}", name, value.ToLower());
                            break;
                        case "float-array":
                        case "int-array":
                        case "float-list":
                        case "int-list":
                            string[] parts = Helper.SplitValueToArray(value);
                            fieldStr = string.Format("\"{0}\":[{1}]", name, string.Join(",", parts));
                            break;
                        case "vector2":
                            string[] partsVector2 = Helper.SplitValueToArray(value);
                            fieldStr = "\"" + name + "\":" + "{\"x\":" + partsVector2[0] + ",\"y\":" + partsVector2[1] + "}";
                            break;
                        case "vector3":
                            string[] partsVector3 = Helper.SplitValueToArray(value);
                            fieldStr = "\"" + name + "\":" + "{\"x\":" + partsVector3[0] + ",\"y\":" + partsVector3[1] + ",\"z\":" + partsVector3[2] + "}";
                            break;
                        case "string":
                            fieldStr = string.Format("\"{0}\":\"{1}\"", name, value);
                            break;
                        case "string-array":
                        case "string-list":
                            string[] partsString = Helper.SplitValueToArray(value);
                            string[] jsonFromatString = new string[partsString.Length];
                            for (int ii = 0; ii < partsString.Length; ii++)
                            {
                                jsonFromatString[ii] = string.Format("\"{0}\"", partsString[ii]);
                            }
                            fieldStr = string.Format("\"{0}\":[{1}]", name, string.Join(",", jsonFromatString));
                            break;
                    }
                    if (!string.IsNullOrEmpty(fieldStr))
                    {
                        fields.Add(fieldStr);
                        fieldsDict.Add(name, valueType);
                    }
                }
            }
            string content = "{" + string.Join(",", fields) + "}";

            //Generate cs file
            var builder = new StringBuilder();
            foreach (var field in fieldsDict)
            {
                string fieldType = field.Value.ToLower();
                switch (field.Value.ToLower())
                {
                    case "int":
                    case "float":
                    case "bool":
                    case "string":
                        break;
                    case "float-array":
                        fieldType = "float[]";
                        break;
                    case "int-array":
                        fieldType = "int[]";
                        break;
                    case "float-list":
                        fieldType = "List<float>";
                        break;
                    case "int-list":
                        fieldType = "List<int>";
                        break;
                    case "vector2":
                        fieldType = "Vector2";
                        break;
                    case "vector3":
                        fieldType = "Vector3";
                        break;
                    case "string-array":
                        fieldType = "string[]";
                        break;
                    case "string-list":
                        fieldType = "List<string>";
                        break;
                }

                string str = string.Format("public {0} {1};", fieldType, field.Key);
                builder.Append(str);
                builder.Append(Environment.NewLine);
            }
            string csFileTemplate = System.IO.File.ReadAllText(SETTINGS_CS_TEMPATE);
            string className = sheetName.Replace(" ", "_");
            csFileTemplate = csFileTemplate.Replace("_SETTINGS_CLASS_NAME_", className);
            csFileTemplate = csFileTemplate.Replace("//#REPLACE_FIELDS", "\t\t" + builder.ToString());
            if (!string.IsNullOrEmpty(nameSpace))
            {
                csFileTemplate = string.Format("namespace {0}\n{1}\n{2}\n{3}", nameSpace, "{", csFileTemplate, "}");
            }

            Helper.WriteFile(Config.Settings.outputConstantsFilePath, className + ".cs", csFileTemplate);
            Log(LogType.Message, string.Format("Exported {0}.cs", className));

            return content;
        }

        public string RemoveLast(string text, string character)
        {
            if (text.Length < 1) return text;
            int index = text.ToString().LastIndexOf(character);
            return index >= 0 ? text.Remove(index, character.Length) : text;
        }

        public string ToCapitalizeEachWord(string pString)
        {
            // Creates a TextInfo based on the "en-US" culture.
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(pString);
        }

        private void RefreshDtgExcelFiles()
        {
            for (int i = Config.Settings.allFiles.Count - 1; i >= 0; i--)
            {
                //Check if file exist
                if (!System.IO.File.Exists(Config.Settings.allFiles[i].path))
                    Config.Settings.allFiles.RemoveAt(i);
            }
            if (Config.Settings.allFiles != null)
                Config.Settings.allFiles.Sort();
            m_ExcelFilesBind.ResetBindings();
            DtgFilePaths.Refresh();
            DtgFilePaths.AutoResizeColumns();
        }

        //private void RefreshDtgFiles()
        //{
        //    DtgFilePaths.DataSource = null;
        //    DtgFilePaths.Rows.Clear();
        //    DtgFilePaths.Refresh();
        //    for (int i = Config.Settings.allFiles.Count - 1; i >= 0; i--)
        //    {
        //        //Check if file exist
        //        if (!File.Exists(Config.Settings.allFiles[i].path))
        //            Config.Settings.allFiles.RemoveAt(i);
        //    }
        //    DtgFilePaths.DataSource = Config.Settings.allFiles;
        //    DtgFilePaths.AutoResizeColumns();
        //}

        /// <summary>
        /// Must be called after init data files list
        /// </summary>
        private void InitializeDtgFiles()
        {
            //DtgFilePaths.Columns.Add("path", "Path");
            //DtgFilePaths.Columns["path"].DataPropertyName = "path";

            //DtgFilePaths.Columns.Add(new DataGridViewCheckBoxColumn()
            //{
            //    Name = "exportJsonList",
            //    HeaderText = "Export Json List",
            //    ValueType = typeof(bool),
            //    DataPropertyName = "exportJsonList",
            //});

            //DtgFilePaths.Columns.Add(new DataGridViewCheckBoxColumn()
            //{
            //    Name = "exportIds",
            //    HeaderText = "Export Ids",
            //    ValueType = typeof(bool),
            //    DataPropertyName = "exportIds",
            //});

            //DtgFilePaths.Columns.Add(new DataGridViewCheckBoxColumn()
            //{
            //    Name = "exportConstants",
            //    HeaderText = "Export Constants",
            //    ValueType = typeof(bool),
            //    DataPropertyName = "exportConstants",
            //});

            //DtgFilePaths.Columns.Add(new DataGridViewButtonColumn()
            //{
            //    Name = "BtnDelete",
            //    HeaderText = "Delete",
            //    Text = "Delete",
            //    UseColumnTextForButtonValue = true,
            //});

            DtgFilePaths.AutoGenerateColumns = false;
            m_ExcelFilesBind = new BindingList<FileEntity>(Config.Settings.allFiles);
            DtgFilePaths.DataSource = m_ExcelFilesBind;
        }

        private void CreateEncryption()
        {
            string[] keysString = txtSettingEncryptionKey.Text.Trim().Replace(" ", "").Split(',');
            if (keysString.Length > 0)
            {
                bool validKey = true;
                byte[] keysByte = new byte[keysString.Length];
                for (int i = 0; i < keysString.Length; i++)
                {
                    byte output = 0;
                    if (byte.TryParse(keysString[i], out output))
                    {
                        keysByte[i] = output;
                    }
                    else
                    {
                        validKey = false;
                    }
                }
                if (validKey)
                {
                    Config.Settings.encryptionKey = txtSettingEncryptionKey.Text.Trim();
                    m_Encryption = new Encryption(keysByte);
                }
                else
                {
                    m_Encryption = null;

                    MessageBox.Show("Encryption key is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log(LogType.Error, "Encryption key is invalid");
                }
            }
            else
            {
                MessageBox.Show("Encryption key is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(LogType.Error, "Encryption key is invalid");
            }
        }

        private void ExcludeSheets(ref List<Sheet> pSheets)
        {
            if (Config.Settings.excludedSheets == null)
                return;

            var excludedSheets = Config.Settings.GetExcludedSheets();
            for (int i = pSheets.Count - 1; i >= 0; i--)
            {
                if (excludedSheets.Contains(pSheets[i].SheetName.ToLower()))
                    pSheets.RemoveAt(i);
            }
        }

        public void LoadLocalizationSheet(IWorkbook pWorrkBook, string pSheetName)
        {
            var sheet = pWorrkBook.GetSheet(pSheetName);
            if (sheet.IsNull() || sheet.LastRowNum == 0)
            {
                Log(LogType.Warning, pSheetName + " is empty!");
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
                        var cell = rowData.GetCell(col);
                        var fieldValue = cell.ToCellString();
                        var fieldName = sheet.GetRow(0).GetCell(col).ToString();
                        if (cell != null && cell.IsMergedCell && !string.IsNullOrEmpty(fieldValue))
                            mergeCellValue = fieldValue;
                        if (cell != null && cell.IsMergedCell && string.IsNullOrEmpty(fieldValue))
                            fieldValue = mergeCellValue;
                        if (!string.IsNullOrEmpty(fieldName))
                        {
                            //idString
                            if (col == 0 && row > 0)
                            {
                                if (string.IsNullOrEmpty(fieldValue))
                                {
                                    //MessageBox.Show(string.Format("Sheet {0}: IdString can not be empty!", pSheetName), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                                idStrings.Add(fieldValue);
                            }
                            //relativeId
                            else if (col == 1 && row > 0)
                            {
                                if (!string.IsNullOrEmpty(fieldValue) && m_AllIds != null)
                                {
                                    bool existId = false;
                                    foreach (var id in m_AllIds)
                                        if (id.Key.Trim() == fieldValue.Trim())
                                        {
                                            fieldValue = id.Value.ToString();
                                            idStrings[idStrings.Count - 1] = string.Format("{0}_{1}", idStrings[idStrings.Count - 1], id.Value);
                                            existId = true;
                                            break;
                                        }

                                    if (!existId)
                                    {
                                        idStrings[idStrings.Count - 1] = string.Format("{0}_{1}", idStrings[idStrings.Count - 1], fieldValue);
                                    }
                                }
                            }
                            //languages
                            else if (col > 1 && row > 0)
                            {
                                if (!textDict.ContainsKey(fieldName))
                                    textDict.Add(fieldName, new List<string>());
                                textDict[fieldName].Add(fieldValue);
                            }
                        }
                        else
                        {
                            Console.Write(col);
                        }
                    }
                }
            }

            if (m_LocalizationsDict.ContainsKey(pSheetName))
            {
                var builder = m_LocalizationsDict[pSheetName];
                idStrings.AddRange(builder.idsString);
                foreach (var b in builder.languageTextDict)
                {
                    var language = b.Key;
                    var texts = b.Value;
                    if (textDict.ContainsKey(language))
                        textDict[language].AddRange(texts);
                    else
                        textDict.Add(b.Key, b.Value);
                }
                m_LocalizationsDict[pSheetName] = new LocalizationBuilder()
                {
                    idsString = idStrings,
                    languageTextDict = textDict,
                };
            }
            else
                m_LocalizationsDict.Add(pSheetName, new LocalizationBuilder()
                {
                    idsString = idStrings,
                    languageTextDict = textDict,
                });
        }

        public class LocalizationBuilder
        {
            public List<string> idsString = new List<string>();
            public Dictionary<string, List<string>> languageTextDict = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// All Languages are saved in single cs file
        /// </summary>
        private void CreateLocalizationFile(List<string> pIdsString, Dictionary<string, List<string>> pLanguageTextDict, string pFileName)
        {
            if (pLanguageTextDict.Count == 0 || pLanguageTextDict.Count == 0)
            {
                return;
            }

            //Build id interger array
            var idBuilder = new StringBuilder();
            if (pIdsString.Count > 0)
            {
                idBuilder.Append(string.Format("\tpublic const int"));
                idBuilder.Append("\n\t\t");
                for (int i = 0; i < pIdsString.Count; i++)
                {
                    if (i > 0 && i % 100 == 0)
                        idBuilder.Append("\n\t\t");

                    if (i < pIdsString.Count - 1)
                        idBuilder.Append(string.Format("{0} = {1}, ", pIdsString[i].RemoveSpecialCharacters(), i));
                    else
                        idBuilder.Append(string.Format("{0} = {1};", pIdsString[i].RemoveSpecialCharacters(), i));
                }
            }

            //Build id enum array
            var idBuilder2 = new StringBuilder();
            idBuilder2.Append("\tpublic enum ID \n\t{\n\t\tNONE = -1,");
            idBuilder2.Append("\n\t\t");
            for (int i = 0; i < pIdsString.Count; i++)
            {
                if (i > 0 && i % 100 == 0)
                {
                    idBuilder2.Append("\n\t\t");
                    idBuilder2.Append(string.Format("{0},", pIdsString[i].RemoveSpecialCharacters(), i));
                }
                else
                {
                    if (i == 0)
                        idBuilder2.Append(string.Format("{0} = {1},", pIdsString[i].RemoveSpecialCharacters(), i));
                    else
                        idBuilder2.Append(string.Format(" {0},", pIdsString[i].RemoveSpecialCharacters(), i));
                }
            }
            idBuilder2.Append("\n\t}");

            //Build id string array
            var idStringDictBuilder = new StringBuilder();
            idStringDictBuilder.Append("\tpublic static readonly string[] idString = new string[]\n\t{\n\t\t");
            for (int i = 0; i < pIdsString.Count; i++)
            {
                if (i > 0 && i % 100 == 0)
                {
                    idStringDictBuilder.Append("\n\t\t");
                    idStringDictBuilder.Append(string.Format("\"{0}\",", pIdsString[i]));
                }
                else if (i == 0)
                    idStringDictBuilder.Append(string.Format("\"{0}\",", pIdsString[i]));
                else
                    idStringDictBuilder.Append(string.Format(" \"{0}\",", pIdsString[i]));
            }
            idStringDictBuilder.Append("\n\t};");

            //Build language list
            var allLanguagePackBuilder = new StringBuilder();
            foreach (var listText in pLanguageTextDict)
            {
                var languagePackContent = new StringBuilder();
                languagePackContent.Append("\tpublic static readonly string[] " + listText.Key + " = new string[]");
                languagePackContent.Append("\n\t{\n");
                for (int i = 0; i < listText.Value.Count; i++)
                {
                    string text = listText.Value[i].Replace("\n", "\\n");
                    if (i > 0)
                        languagePackContent.Append("\n\t\t");
                    else
                        languagePackContent.Append("\t\t");
                    languagePackContent.Append(string.Format("\"{0}\"", text));

                    if (i < listText.Value.Count - 1)
                        languagePackContent.Append(", ");
                }
                languagePackContent.Append("\n\t};");

                if (listText.Key != pLanguageTextDict.Last().Key)
                    allLanguagePackBuilder.Append(languagePackContent.ToString()).AppendLine();
                else
                    allLanguagePackBuilder.Append(languagePackContent.ToString());
                allLanguagePackBuilder.AppendLine();
            }

            //Build language dictionary
            var languageDictBuilder = new StringBuilder();
            languageDictBuilder.Append("\tpublic static readonly Dictionary<string, string[]> language = new Dictionary<string, string[]>() { ");
            foreach (var listText in pLanguageTextDict)
            {
                languageDictBuilder.Append(string.Format(" {0} \"{1}\", {2} {3}", "{", listText.Key, listText.Key, "},"));
            }
            languageDictBuilder.Append(" };\n");
            languageDictBuilder.Append(string.Format("\tpublic static readonly string defaultLanguage = \"{0}\";", pLanguageTextDict.First().Key));

            //Write file
            string fileTemplateContent = File.ReadAllText(LOCALIZATION_TEMPLATE);
            fileTemplateContent = fileTemplateContent.Replace("LOCALIZATION_CLASS_NAME", pFileName);
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_ENUM", idBuilder2.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_CONST", idBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_STRING", idStringDictBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_LIST", allLanguagePackBuilder.ToString());
            fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY", languageDictBuilder.ToString());
            Helper.WriteFile(Config.Settings.outputConstantsFilePath, pFileName + ".cs", fileTemplateContent);
            Log(LogType.Message, "Export " + pFileName + ".cs successfully!");
        }

        /// <summary>
        /// Each language have a json file
        /// </summary>
        private void CreateLocalizationFileV2(List<string> pIdsString, Dictionary<string, List<string>> pLanguageTextDict, string pFileName)
        {
            if (pLanguageTextDict.Count == 0 || pLanguageTextDict.Count == 0)
            {
                return;
            }

            //Build id interger array
            var idBuilder = new StringBuilder();
            if (pIdsString.Count > 0)
            {
                idBuilder.Append(string.Format("\tpublic const int"));
                idBuilder.Append("\n\t\t");
                for (int i = 0; i < pIdsString.Count; i++)
                {
                    if (i > 0 && i % 100 == 0)
                        idBuilder.Append("\n\t\t");

                    if (i < pIdsString.Count - 1)
                        idBuilder.Append(string.Format("{0} = {1}, ", pIdsString[i].RemoveSpecialCharacters(), i));
                    else
                        idBuilder.Append(string.Format("{0} = {1};", pIdsString[i].RemoveSpecialCharacters(), i));
                }
            }

            //Build id enum array
            var idBuilder2 = new StringBuilder();
            idBuilder2.Append("\tpublic enum ID \n\t{\n\t\tNONE = -1,");
            idBuilder2.Append("\n\t\t");
            for (int i = 0; i < pIdsString.Count; i++)
            {
                if (i > 0 && i % 100 == 0)
                {
                    idBuilder2.Append("\n\t\t");
                    idBuilder2.Append(string.Format("{0},", pIdsString[i].RemoveSpecialCharacters(), i));
                }
                else
                {
                    if (i == 0)
                        idBuilder2.Append(string.Format("{0} = {1},", pIdsString[i].RemoveSpecialCharacters(), i));
                    else
                        idBuilder2.Append(string.Format(" {0},", pIdsString[i].RemoveSpecialCharacters(), i));
                }
            }
            idBuilder2.Append("\n\t}");

            //Build id string array
            var idStringDictBuilder = new StringBuilder();
            idStringDictBuilder.Append("\tpublic static readonly string[] idString = new string[]\n\t{\n\t\t");
            for (int i = 0; i < pIdsString.Count; i++)
            {
                if (i > 0 && i % 100 == 0)
                {
                    idStringDictBuilder.Append("\n\t\t");
                    idStringDictBuilder.Append(string.Format("\"{0}\",", pIdsString[i]));
                }
                else if (i == 0)
                    idStringDictBuilder.Append(string.Format("\"{0}\",", pIdsString[i]));
                else
                    idStringDictBuilder.Append(string.Format(" \"{0}\",", pIdsString[i]));
            }
            idStringDictBuilder.Append("\n\t};");

            //Build language json data
            foreach (var listText in pLanguageTextDict)
            {
                string json = JsonConvert.SerializeObject(listText.Value);
                Helper.WriteFile(Config.Settings.outputLocalizationFilePath, pFileName + "_" + listText.Key + ".txt", json);
            }

            //Build language dictionary
            var languagesDictBuilder = new StringBuilder();
            languagesDictBuilder.Append("\tpublic static readonly Dictionary<string, string> languageDict = new Dictionary<string, string>() { ");
            foreach (var textsList in pLanguageTextDict)
            {
                languagesDictBuilder.Append(string.Format(" {0} \"{1}\", {2} {3}", "{", textsList.Key, string.Format("\"{0}_{1}\"", pFileName, textsList.Key), "},"));

                if (!m_LocalizedLanguages.Contains(textsList.Key))
                    m_LocalizedLanguages.Add(textsList.Key);
            }
            languagesDictBuilder.Append(" };\n");
            languagesDictBuilder.Append(string.Format("\tpublic static readonly string defaultLanguage = \"{0}\";", pLanguageTextDict.First().Key));

            //Write file localization constants
            string fileContent = File.ReadAllText(LOCALIZATION_TEMPLATE_V2);
            fileContent = fileContent.Replace("LOCALIZATION_CLASS_NAME", pFileName);
            fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY_KEY_ENUM", idBuilder2.ToString());
            fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY_KEY_CONST", idBuilder.ToString());
            fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY_KEY_STRING", idStringDictBuilder.ToString());
            fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY", languagesDictBuilder.ToString());
            fileContent = AddNamespace(fileContent);
            Helper.WriteFile(Config.Settings.outputConstantsFilePath, pFileName + ".cs", fileContent);

            //Write file localized text component
            fileContent = File.ReadAllText(LOCALIZATION_TEXT_TEMPLATE);
            fileContent = fileContent.Replace("LOCALIZATION_CLASS_NAME", pFileName);
            fileContent = AddNamespace(fileContent);
            Helper.WriteFile(Config.Settings.outputConstantsFilePath, pFileName + "Text.cs", fileContent);

            Log(LogType.Message, "Export " + pFileName + ".cs successfully!");
        }

        /// <summary>
        /// Incase we have more thane one localizations file, we need a middle manager to initialize them
        /// </summary>
        private void CreateLocalizationsManagerFile()
        {
            if (m_LocalizedSheetsExported.Count > 1)
            {
                //Build language dictionary
                var languagesDictBuilder = new StringBuilder();
                languagesDictBuilder.Append("\tpublic static readonly List<string> languages = new List<string>() { ");
                foreach (var lang in m_LocalizedLanguages)
                {
                    languagesDictBuilder.Append($"\"{lang}\", ");
                }
                languagesDictBuilder.Append("};\n");
                languagesDictBuilder.Append(string.Format("\tpublic static readonly string defaultLanguage = \"{0}\";", m_LocalizedLanguages.First()));

                //Build initialization code
                var initLines = new StringBuilder();
                var initAsynLines = new StringBuilder();
                var setFolder = new StringBuilder();
                for (int i = 0; i < m_LocalizedSheetsExported.Count; i++)
                {
                    initLines.Append($"\t\t{m_LocalizedSheetsExported[i]}.Init();");
                    if (i < m_LocalizedSheetsExported.Count - 1)
                        initLines.Append(Environment.NewLine);

                    initAsynLines.Append($"\t\tyield return CoroutineUtil.StartCoroutine({m_LocalizedSheetsExported[i]}.InitAsync());");
                    if (i < m_LocalizedSheetsExported.Count - 1)
                        initAsynLines.Append(Environment.NewLine);

                    setFolder.Append($"\t\t{m_LocalizedSheetsExported[i]}.Folder = pFolder;");
                    if (i < m_LocalizedSheetsExported.Count - 1)
                        setFolder.Append(Environment.NewLine);
                }

                string fileContent = File.ReadAllText(LOCALIZATION_MANAGER_TEMPLATE);
                fileContent = fileContent.Replace("//LOCALIZATION_INIT_ASYN", initAsynLines.ToString());
                fileContent = fileContent.Replace("//LOCALIZATION_INIT", initLines.ToString());
                fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY", languagesDictBuilder.ToString());
                fileContent = fileContent.Replace("//LOCALIZATION_SET_FOLDER", setFolder.ToString());
                fileContent = AddNamespace(fileContent);
                Helper.WriteFile(Config.Settings.outputConstantsFilePath, "LocalizationsManager.cs", fileContent);
            }
        }

        private string AddNamespace(string fileContent)
        {
            if (!string.IsNullOrEmpty(Config.Settings._namespace))
            {
                fileContent = fileContent.Replace(Environment.NewLine, $"NEW_LINE");
                fileContent = fileContent.Replace("\n", $"NEW_LINE");
                fileContent = fileContent.Replace("NEW_LINE", $"{Environment.NewLine}\t");
                fileContent = string.Format("namespace {0}\n{1}\n\t{2}\n{3}", Config.Settings._namespace, "{", fileContent, "}");
            }
            return fileContent;
        }

        #endregion

        //====================================================

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadFileSettings();
            LoadWorkBook();
            InitializeDtgFiles();

            chkSeperateConstants1.Checked = m_SeperateConstants1;
            chkEncrypt.Checked = m_EncryptData1;
            chkMergeJsonIntoSingleOne.Checked = m_MergeJsonsIntoSingleJson1;
            txtMegedJsonCustomName.ReadOnly = !m_MergeJsonsIntoSingleJson1;
            BtnTest.Visible = false;
            btnOpenGoogleSheet.Visible = false;
            tabChangeLog.TabPages.RemoveByKey("tabPage2");

            // Set up the delays for the ToolTip.
            toolTip.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip.SetToolTip(txtSettingOutputDataFilePath, "Địa chỉ thư mục lưu file JSON data (.txt)");
            toolTip.SetToolTip(txtSettingOuputConstantsFilePath, "Địa chỉ thư mục lưu file Constants và IDs (.cs)");
            toolTip.SetToolTip(chkSettingEnableEncryption, "Mã hóa các file JSON data");
            toolTip.SetToolTip(chkSeperateIDs, "Hợp nhất các file Constants, IDs và Localization vào 1 file ứng với mỗi loại");
            toolTip.SetToolTip(txtSettingExcludedSheet, "Danh sách các sheet sẽ bị bỏ qua khi đọc data từ Excel, tên sheet cách nhau bởi dấu \";\"");
            toolTip.SetToolTip(btnOpenFolder1, "Đến folder đích");
            toolTip.SetToolTip(btnOpenFolder2, "Đến folder đích");
            toolTip.SetToolTip(btnSelectFolder, "Chọn folder đích");
            toolTip.SetToolTip(btnSelectFolder2, "Chọn folder đích");
            toolTip.SetToolTip(txtUnminimizeFields, "Danh sách các cột (field) cần giữ nguyên giá trị. Giá trị trong các cột này sẽ không được thu gọn");
            toolTip.SetToolTip(chkKeepOnlyEnumAsIds, "Trong sheet IDs, những cột ID có gắn tag [enum] sẽ chỉ export ra data dạng enum!");
        }

        private void btnSelectInputFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                txtInputXLSXFilePath.Text = fileName;
            }

            //Load and show all sheets
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            Helper.SelectFolder(txtSettingOutputDataFilePath);
        }

        private void btnSelectCSVFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog.FileName;
                txtInputCSVFilePath.Text = file;
            }
        }

        /// <summary>
        /// Old version
        /// </summary>
        private void btnSelectOutputJsonFiles_Click(object sender, EventArgs e)
        {
            Helper.SelectFolder(txtOutputJsonFilePath);
        }

        private void btnSelectOuputConstantsFile_Click(object sender, EventArgs e)
        {
            Helper.SelectFolder(txtSettingOuputConstantsFilePath);
        }

        private void txtOuputConstantsFilePath_TextChanged(object sender, EventArgs e)
        {
            SetupConfigFolders(txtSettingOuputConstantsFilePath, ref Config.Settings.outputConstantsFilePath);
        }

        private void txtInputFilePath_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(txtInputXLSXFilePath.Text.Trim()))
            {
                SetupConfigFolders(txtInputXLSXFilePath, ref Config.Settings.inputDataFilePath);
                LoadWorkBook();
                txtMegedJsonCustomName.Text = Path.GetFileNameWithoutExtension(Config.Settings.inputDataFilePath).Trim().Replace(" ", "_");
            }
            else
                txtMegedJsonCustomName.Text = "Merged Json custom name";
        }

        private void txtOutputDataFilePath_TextChanged(object sender, EventArgs e)
        {
            SetupConfigFolders(txtSettingOutputDataFilePath, ref Config.Settings.outputDataFilePath);
        }

        private void txtInputCSVFilePath_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(txtInputXLSXFilePath.Text.Trim()))
                SetupConfigFolders(txtInputCSVFilePath, ref Config.Settings.inputCSVFilePath);
        }

        private void txtOuputJsonFilePath_TextChanged(object sender, EventArgs e)
        {
            SetupConfigFolders(txtOutputJsonFilePath, ref Config.Settings.outputDataFilePath);
		}

		private void txtSettingOutputLocalizationFilePath_TextChanged(object sender, EventArgs e)
		{
			SetupConfigFolders(txtSettingOutputLocalizationFilePath, ref Config.Settings.outputLocalizationFilePath);
		}

		/// <summary>
		/// Export single excel file
		/// </summary>
		private void BtnExportJson_Click(object sender, EventArgs e)
        {
            var allSheets = new List<string>();
            if (m_WorkBook == null)
                m_WorkBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);

            for (int i = 0; i < m_Sheets.Count; i++)
            {
                if (m_Sheets[i].SheetName.Contains(IDS_SHEET))
                {
                    //Load All IDs
                    LoadIDSheet(m_WorkBook, m_Sheets[i].SheetName);

                    //Create IDs Files
                    if (m_Sheets[i].Check && m_SeperateConstants1)
                    {
                        var builder = m_IDsBuilderDict[m_Sheets[i].SheetName];
                        CreateIDsFile(m_Sheets[i].SheetName, builder.ToString());
                    }
                }
            }

            if (!m_SeperateConstants1)
            {
                //Export all IDs in one file
                StringBuilder iDsBuilder = new StringBuilder();
                foreach (var b in m_IDsBuilderDict)
                {
                    iDsBuilder.Append(b.Value.ToString());
                    iDsBuilder.AppendLine();
                }
                CreateIDsFile("IDs", iDsBuilder.ToString());
                Log(LogType.Message, "Export IDs successfully!");
            }

            bool writeJsonFileForSingleSheet = !m_MergeJsonsIntoSingleJson1;
            Dictionary<string, string> allJsons = new Dictionary<string, string>();
            for (int i = 0; i < m_Sheets.Count; i++)
            {
                if (m_Sheets[i].Check && IsJsonSheet(m_Sheets[i].SheetName))
                {
                    string fileName = m_Sheets[i].SheetName.Trim().Replace(" ", "_");
                    string json = ConvertSheetToJson(m_WorkBook, m_Sheets[i].SheetName, fileName, m_EncryptData1, writeJsonFileForSingleSheet);

                    //Merge all json into a single file
                    if (m_MergeJsonsIntoSingleJson1)
                    {
                        if (allJsons.ContainsKey(fileName))
                        {
                            Log(LogType.Error, $"Can not merge sheet {fileName}, because key {fileName} is already exists!");
                            continue;
                        }
                        allJsons.Add(fileName, json);
                    }

                    allSheets.Add(m_Sheets[i].SheetName);
                }
            }
            if (m_MergeJsonsIntoSingleJson1)
            {
                //Build json file for all jsons content
                string mergedJson = JsonConvert.SerializeObject(allJsons);
                Helper.WriteFile(Config.Settings.outputDataFilePath, txtMegedJsonCustomName.Text + ".txt", mergedJson);

                if (m_EncryptData1)
                    Log(LogType.Message, string.Format("Exported Sheet {0} as encrypted JSON data.", txtMegedJsonCustomName.Text));
                else
                    Log(LogType.Message, string.Format("Exported Sheet {0} as JSON data.", txtMegedJsonCustomName.Text));
            }

            if (m_Sheets.Count > 0)
                Log(LogType.Message, "Export Json Data done!\n" + string.Join(", ", allSheets.ToArray()));
        }

        private bool IsJsonSheet(string pName)
        {
            return !pName.Contains(IDS_SHEET)
                    && !pName.Contains(CONSTANTS_SHEET)
                    && !pName.Contains(SETTINGS_SHEET)
                    && !pName.Contains(LOCALIZATION_SHEET);
        }

        /// <summary>
        /// Export single excel file
        /// </summary>
        private void BtnExportIds_Click(object sender, EventArgs e)
        {
            if (m_WorkBook == null)
                m_WorkBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);

            ClearCaches();

            foreach (var m in m_Sheets)
            {
                if (m.SheetName.Contains(IDS_SHEET) && m.Check)
                {
                    //Load All IDs
                    LoadIDSheet(m_WorkBook, m.SheetName);

                    //Create IDs Files
                    if (m_SeperateConstants1)
                    {
                        var content = m_IDsBuilderDict[m.SheetName].ToString();
                        CreateIDsFile(m.SheetName, content);
                        Log(LogType.Message, "Export " + m.SheetName + " successfully!");
                    }
                }
            }

            if (!m_SeperateConstants1)
            {
                StringBuilder iDsBuilder = new StringBuilder();
                foreach (var builder in m_IDsBuilderDict)
                {
                    iDsBuilder.Append(builder.ToString());
                    iDsBuilder.AppendLine();
                }
                CreateIDsFile("IDs", iDsBuilder.ToString());
                Log(LogType.Message, "Export IDs successfully!");
            }
        }

        private void BtnExportConstants_Click(object sender, EventArgs e)
        {
            if (m_WorkBook == null)
                m_WorkBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);

            ClearCaches();

            for (int i = 0; i < m_Sheets.Count; i++)
            {
                if (m_Sheets[i].SheetName.Contains(CONSTANTS_SHEET) && m_Sheets[i].Check)
                {
                    LoadConstantsSheet(m_WorkBook, m_Sheets[i].SheetName);

                    if (m_ConstantsBuilderDict.ContainsKey(m_Sheets[i].SheetName) && m_SeperateConstants1)
                    {
                        CreateConstantsFile(m_ConstantsBuilderDict[m_Sheets[i].SheetName].ToString(), m_Sheets[i].SheetName);
                    }
                }
            }

            if (!m_SeperateConstants1)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var b in m_ConstantsBuilderDict)
                {
                    builder.Append(b.Value.ToString());
                    builder.AppendLine();
                }
                CreateConstantsFile(builder.ToString(), "Constants");
            }
        }

        private void BtnReloadGrid_Click(object sender, EventArgs e)
        {
            LoadWorkBook();
        }

        private void BtnExportSettings_Click(object sender, EventArgs e)
        {
            if (m_WorkBook == null)
                m_WorkBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);

            for (int i = 0; i < m_Sheets.Count; i++)
            {
                if (m_Sheets[i].SheetName.Contains(SETTINGS_SHEET) && m_Sheets[i].Check)
                {
                    string content = ExportSettingsSheetToScripableObject(m_WorkBook, m_Sheets[i].SheetName);
                    if (!string.IsNullOrEmpty(content))
                    {
                        Helper.WriteFile(Config.Settings.outputDataFilePath, m_Sheets[i].SheetName + ".txt", content);
                        Log(LogType.Message, "Export " + m_Sheets[i].SheetName + " successfully!");
                    }
                }
            }
        }

        private void btnExportLocalization_Click(object sender, EventArgs e)
        {
            if (m_WorkBook == null)
                m_WorkBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);

            m_LocalizedSheetsExported = new List<string>();
            m_LocalizedLanguages = new List<string>();

            ClearCaches();

            for (int i = 0; i < m_Sheets.Count; i++)
            {
                if (m_Sheets[i].Check && m_Sheets[i].SheetName.Contains(LOCALIZATION_SHEET))
                {
                    LoadLocalizationSheet(m_WorkBook, m_Sheets[i].SheetName);

                    if (m_LocalizationsDict.ContainsKey(m_Sheets[i].SheetName) && m_SeperateConstants1)
                    {
                        var builder = m_LocalizationsDict[m_Sheets[i].SheetName];
                        //CreateLocalizationFile(builder.idsString, builder.languageTextDict, mSheets[i].SheetName);
                        CreateLocalizationFileV2(builder.idsString, builder.languageTextDict, m_Sheets[i].SheetName);
                        m_LocalizedSheetsExported.Add(m_Sheets[i].SheetName);
                    }
                }
            }

            if (!m_SeperateConstants1)
            {
                var builder = new LocalizationBuilder();
                foreach (var b in m_LocalizationsDict)
                {
                    builder.idsString.AddRange(b.Value.idsString);
                    foreach (var t in b.Value.languageTextDict)
                    {
                        var language = t.Key;
                        var texts = t.Value;
                        if (!builder.languageTextDict.ContainsKey(language))
                            builder.languageTextDict.Add(language, new List<string>());
                        builder.languageTextDict[language].AddRange(texts);
                    }
                }
                //CreateLocalizationFile(builder.idsString, builder.languageTextDict, "Localization");
                CreateLocalizationFileV2(builder.idsString, builder.languageTextDict, "Localization");
                m_LocalizedSheetsExported.Add("Localization");
            }

            CreateLocalizationsManagerFile();
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            m_Sheets = (List<Sheet>)DtgSheets.DataSource;

            foreach (var i in m_Sheets)
                System.Diagnostics.Debug.WriteLine(i.SheetName + " " + i.Check);
        }

        private void chkSeperateConstants1_CheckedChanged(object sender, EventArgs e)
        {
            m_SeperateConstants1 = chkSeperateConstants1.Checked;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabControl = sender as TabControl;
            var tab = tabControl.SelectedTab;
            var tabIndex = tabControl.SelectedIndex;
            if (tab.Name == "tabPage3")
            {
                ClearCaches();
                RefreshDtgExcelFiles();
                //RefreshDtgFiles();
            }
        }

        private void BtnAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Workbook (*.xlsx,*.xls,*.csv)|*.xlsx,*.xls,*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            List<FileInfo> fList = new List<FileInfo>();

            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    bool existed = false;
                    foreach (var f in Config.Settings.allFiles)
                    {
                        if (f.path == file)
                        {
                            existed = true;
                        }
                    }
                    if (!existed)
                        Config.Settings.allFiles.Add(new FileEntity(file));
                }

                RefreshDtgExcelFiles();
                //RefreshDtgFiles();
                Config.Save();
            }
        }

        private void BtnAllInOne_Click(object sender, EventArgs e)
        {
            var bindingList = (BindingList<FileEntity>)DtgFilePaths.DataSource;
            Config.Settings.allFiles = bindingList.ToList();
            Config.Save();
            ClearCaches();

            m_AllIDsSorted = null;
            m_AllIds = new List<ID>();
            m_LocalizedSheetsExported = new List<string>();
            m_LocalizedLanguages = new List<string>();

            //Process IDs sheet first
            foreach (var file in Config.Settings.allFiles)
            {
                var workBook = Helper.LoadWorkBook(file.path);
                if (workBook == null)
                    continue;

                var sheets = new List<Sheet>();
                for (int i = 0; i < workBook.NumberOfSheets; i++)
                {
                    string sheetName = workBook.GetSheetName(i);
                    sheets.Add(new Sheet(sheetName));
                }
                ExcludeSheets(ref sheets);

                //Load and write Ids
                for (int i = 0; i < sheets.Count; i++)
                {
                    if (sheets[i].SheetName.Contains(IDS_SHEET))
                        LoadIDSheetOnlyValue(workBook, sheets[i].SheetName);
                }
            }

            //Then process other type of sheets
            foreach (var file in Config.Settings.allFiles)
            {
                var workBook = Helper.LoadWorkBook(file.path);
                if (workBook == null)
                    continue;

                var sheets = new List<Sheet>();
                var exportIDs = file.exportIds;
                for (int i = 0; i < workBook.NumberOfSheets; i++)
                {
                    string sheetName = workBook.GetSheetName(i);
                    sheets.Add(new Sheet(sheetName));
                }
                ExcludeSheets(ref sheets);

                //Load and write Ids
                for (int i = 0; i < sheets.Count; i++)
                {
                    if (sheets[i].SheetName.Contains(IDS_SHEET))
                        if (LoadIDSheet(workBook, sheets[i].SheetName) && exportIDs && Config.Settings.seperateIDs)
                            CreateIDsFile(sheets[i].SheetName, m_IDsBuilderDict[sheets[i].SheetName].ToString());
                }

                //Load and write json file
                Dictionary<string, string> allJsons = new Dictionary<string, string>();
                for (int i = 0; i < sheets.Count; i++)
                {
                    if (IsJsonSheet(sheets[i].SheetName))
                    {
                        string fileName = sheets[i].SheetName.Trim().Replace(" ", "_");
                        string json = ConvertSheetToJson(workBook, sheets[i].SheetName, fileName, Config.Settings.encryption, !Config.Settings.mergeJsonsIntoSingleJson);

                        if (Config.Settings.mergeJsonsIntoSingleJson)
                        {
                            if (allJsons.ContainsKey(fileName))
                            {
                                Log(LogType.Error, $"Can not merge sheet {fileName}, because key {fileName} is already exists!");
                                continue;
                            }
                            allJsons.Add(fileName, json);
                        }
                    }
                }

                if (Config.Settings.mergeJsonsIntoSingleJson)
                {
                    //Build json file for all jsons content
                    string mergedJson = JsonConvert.SerializeObject(allJsons);
                    string mergedFileName = Path.GetFileNameWithoutExtension(file.path).Trim().Replace(" ", "_");
                    Helper.WriteFile(Config.Settings.outputDataFilePath, mergedFileName + ".txt", mergedJson);

                    if (m_EncryptData1)
                        Log(LogType.Message, string.Format("Exported Sheet {0} as encrypted JSON data.", mergedFileName));
                    else
                        Log(LogType.Message, string.Format("Exported Sheet {0} as JSON data.", mergedFileName));
                }

                //Load and write constants
                for (int i = 0; i < sheets.Count; i++)
                {
                    if (sheets[i].SheetName.Contains(CONSTANTS_SHEET))
                    {
                        LoadConstantsSheet(workBook, sheets[i].SheetName);

                        if (m_ConstantsBuilderDict.ContainsKey(sheets[i].SheetName) && Config.Settings.seperateConstants)
                            CreateConstantsFile(m_ConstantsBuilderDict[sheets[i].SheetName].ToString(), sheets[i].SheetName);
                    }
                }

                //Load and write localization
                for (int i = 0; i < sheets.Count; i++)
                {
                    if (sheets[i].SheetName.Contains(LOCALIZATION_SHEET))
                    {
                        LoadLocalizationSheet(workBook, sheets[i].SheetName);

                        if (m_LocalizationsDict.ContainsKey(sheets[i].SheetName) && Config.Settings.seperateLocalizations)
                        {
                            var builder = m_LocalizationsDict[sheets[i].SheetName];
                            //CreateLocalizationFile(builder.idsString, builder.languageTextDict, sheets[i].SheetName);
                            CreateLocalizationFileV2(builder.idsString, builder.languageTextDict, sheets[i].SheetName);
                            m_LocalizedSheetsExported.Add(sheets[i].SheetName);
                        }
                    }
                }
            }

            if (!Config.Settings.seperateIDs)
            {
                //Create IDs comprehensively
                var builder = new StringBuilder();
                int count = 0;
                int length = m_IDsBuilderDict.Count;
                foreach (var b in m_IDsBuilderDict)
                {
                    builder.Append(b.Value.ToString());
                    if (count < length - 1)
                        builder.AppendLine();
                    count++;
                }
                CreateIDsFile("IDs", builder.ToString());
                Log(LogType.Message, "Export IDs successfully!");
            }

            if (!Config.Settings.seperateConstants)
            {
                //Create Constants comprehensively
                var builder = new StringBuilder();
                int count = 0;
                int length = m_ConstantsBuilderDict.Count;
                foreach (var b in m_ConstantsBuilderDict)
                {
                    builder.Append(b.Value.ToString());
                    if (count < length - 1)
                        builder.AppendLine();
                    count++;
                }
                CreateConstantsFile(builder.ToString(), "Constants");
            }

            if (!Config.Settings.seperateLocalizations)
            {
                //Create Localization comprehensively
                var localizationBuilder = new LocalizationBuilder();
                foreach (var b in m_LocalizationsDict)
                {
                    localizationBuilder.idsString.AddRange(b.Value.idsString);
                    foreach (var t in b.Value.languageTextDict)
                    {
                        var language = t.Key;
                        var texts = t.Value;
                        if (!localizationBuilder.languageTextDict.ContainsKey(language))
                            localizationBuilder.languageTextDict.Add(language, new List<string>());
                        localizationBuilder.languageTextDict[language].AddRange(texts);
                    }
                }
                //CreateLocalizationFile(localizationBuilder.idsString, localizationBuilder.languageTextDict, "Localization");
                CreateLocalizationFileV2(localizationBuilder.idsString, localizationBuilder.languageTextDict, "Localization");
                m_LocalizedSheetsExported.Add("Localization");
            }

            //Write localization manager file
            CreateLocalizationsManagerFile();
        }

        private void DtgFilePaths_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || e.RowIndex == DtgFilePaths.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == DtgFilePaths.Columns["BtnDelete"].Index)
            {
                //System.Diagnostics.Debug.WriteLine(e.RowIndex);
                //DtgFilePaths.Rows.RemoveAt(e.RowIndex);
                Config.Settings.allFiles.RemoveAt(e.RowIndex);

                RefreshDtgExcelFiles();
                //RefreshDtgFiles();
            }

            //Config.Settings.allFiles = (List<FileEntity>)DtgFilePaths.DataSource;
            //string settingJson = JsonConvert.SerializeObject(mSetting);
            //WriteFile(TOOL_CONFIG_FILE, settingJson);
        }

        private void DtgFilePaths_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.RowIndex);
        }

        private void chkSettingEnableEncryption_CheckedChanged(object sender, EventArgs e)
        {
            Config.Settings.encryption = chkSettingEnableEncryption.Checked;

            if (chkSettingEnableEncryption.Checked)
                CreateEncryption();

            Config.Save();
        }

        private void chkMergeJsonIntoSingleExcel2_CheckedChanged(object sender, EventArgs e)
        {
            Config.Settings.mergeJsonsIntoSingleJson = chkMergeJsonIntoSingleOne2.Checked;
            Config.Save();
        }

        private void txtSettingEncryptionKey_Leave(object sender, EventArgs e)
        {
            CreateEncryption();
        }

        private void txtSettingExcludedSheet_Leave(object sender, EventArgs e)
        {
            if (Config.Settings.excludedSheets != txtSettingExcludedSheet.Text)
            {
                Config.Settings.excludedSheets = txtSettingExcludedSheet.Text.Trim();
                Config.Save();
            }
        }

        private void txtUnminimizeFields_Leave(object sender, EventArgs e)
        {
            if (Config.Settings.unminizedFields != txtUnminimizeFields.Text)
            {
                Config.Settings.unminizedFields = txtUnminimizeFields.Text.Trim();
                Config.Save();
            }
        }

        private void txtSettingNamespace_TextChanged(object sender, EventArgs e)
        {
            Config.Settings._namespace = txtSettingNamespace.Text.Trim();
        }

        private void txtSettingNamespace_Leave(object sender, EventArgs e)
        {
            Config.Save();
        }

        private void chkSeperateIDs_CheckedChanged(object sender, EventArgs e)
        {
            Config.Settings.seperateIDs = chkSeperateIDs.Checked;
            Config.Save();
        }

        private void chkSeperateConstants_CheckedChanged(object sender, EventArgs e)
        {
            Config.Settings.seperateConstants = chkSeperateConstants.Checked;
            Config.Save();
        }

        private void chkSeperateLocalization_CheckedChanged(object sender, EventArgs e)
        {
            Config.Settings.seperateLocalizations = chkSeperateLocalization.Checked;
            Config.Save();
        }

        private void chkEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            m_EncryptData1 = chkEncrypt.Checked;
        }

        private void ClearLog()
        {
            txtLog.Text = "";
            txtLog2.Text = "";
        }

        private void Log(LogType pLogType, string pLog)
        {
            StringBuilder sb = new StringBuilder();
            if (pLogType == LogType.Error)
            {
                sb.Append("[ERROR] ").Append(pLog);
            }
            else if (pLogType == LogType.Warning)
            {
                sb.Append("[WARNING] ").Append(pLog);
            }
            else
                sb.Append("[+] ").Append(pLog);
            txtLog.Text += sb.AppendLine();
            txtLog2.Text = txtLog.Text;
        }

        private IEnumerable<ID> SortIDsByLength()
        {
            var sorted = from s in m_AllIds
                         orderby s.Key.Length ascending
                         select s;
            return sorted;
        }

        #endregion

        //====================================================

        #region Internal Class

        public enum LogType
        {
            Message,
            Error,
            Warning,
        }

        /// <summary>
        /// Define field name and value type
        /// </summary>
        private class RowContent
        {
            public List<string> fieldNames = new List<string>();
            public List<string> fieldValues = new List<string>();
            public List<ICell> fieldCells = new List<ICell>();
        }

        #endregion

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEncryptionInput.Text))
                return;

            string text = txtEncryptionInput.Text;
            string decryptedCode = "";
            try
            {
                decryptedCode = m_Encryption.DecryptValue(text);
                txtEncryptionOutput.Text = decryptedCode;
            }
            catch (Exception ex)
            {
                txtEncryptionOutput.Text = ex.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEncryptionInput.Text))
                return;

            string text = txtEncryptionInput.Text;
            string encryptedCode = "";
            try
            {
                if (m_Encryption == null)
                    CreateEncryption();
                encryptedCode = m_Encryption.EncryptValue(text);
                txtEncryptionOutput.Text = encryptedCode;
            }
            catch (Exception ex)
            {
                txtEncryptionOutput.Text = ex.ToString();
            }
        }

        private void btnOpenFolder1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSettingOutputDataFilePath.Text))
                return;

            if (Directory.Exists(txtSettingOutputDataFilePath.Text))
                Process.Start(txtSettingOutputDataFilePath.Text);
            else
                MessageBox.Show("Folder not exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnOpenFolder2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSettingOuputConstantsFilePath.Text))
                return;

            if (Directory.Exists(txtSettingOuputConstantsFilePath.Text))
                Process.Start(txtSettingOuputConstantsFilePath.Text);
            else
                MessageBox.Show("Folder not exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void btnOpenFolderLocalization_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtSettingOutputLocalizationFilePath.Text))
				return;

			if (Directory.Exists(txtSettingOutputLocalizationFilePath.Text))
				Process.Start(txtSettingOutputLocalizationFilePath.Text);
			else
				MessageBox.Show("Folder not exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void btnSelectFolderLocalization_Click(object sender, EventArgs e)
		{
			Helper.SelectFolder(txtSettingOutputLocalizationFilePath);
		}

		private void LoadSettings()
        {
            //--This is demo setup (not used anymore)
            if (!string.IsNullOrEmpty(Config.Settings.inputCSVFilePath))
            {
                if (!System.IO.File.Exists(Config.Settings.inputCSVFilePath))
                {
                    txtInputCSVFilePath.Text = "";
                    Config.Settings.inputCSVFilePath = "";
                }
                else
                    txtInputCSVFilePath.Text = Config.Settings.inputCSVFilePath;
            }
            else
                txtInputCSVFilePath.Text = "";
            txtOutputJsonFilePath.Text = Config.Settings.outputDataFilePath;

            //--
            if (!string.IsNullOrEmpty(Config.Settings.inputDataFilePath))
            {
                if (!File.Exists(Config.Settings.inputDataFilePath))
                {
                    txtInputXLSXFilePath.Text = "";
                    Config.Settings.inputDataFilePath = "";
                }
                else
                    txtInputXLSXFilePath.Text = Config.Settings.inputDataFilePath;
            }
            else
                txtInputXLSXFilePath.Text = "";
            txtSettingOutputDataFilePath.Text = Config.Settings.outputDataFilePath;
            txtSettingOutputLocalizationFilePath.Text = Config.Settings.outputLocalizationFilePath;
            txtSettingOuputConstantsFilePath.Text = Config.Settings.outputConstantsFilePath;
            chkSettingEnableEncryption.Checked = Config.Settings.encryption;
            if (!string.IsNullOrEmpty(Config.Settings.encryptionKey))
                txtSettingEncryptionKey.Text = Config.Settings.encryptionKey;
            txtSettingExcludedSheet.Text = Config.Settings.excludedSheets;
            txtSettingNamespace.Text = Config.Settings._namespace;
            chkMergeJsonIntoSingleOne2.Checked = Config.Settings.mergeJsonsIntoSingleJson;
            txtUnminimizeFields.Text = Config.Settings.unminizedFields;
            chkSeperateConstants.Checked = Config.Settings.seperateConstants;
            chkSeperateIDs.Checked = Config.Settings.seperateIDs;
            chkSeperateLocalization.Checked = Config.Settings.seperateLocalizations;
            chkKeepOnlyEnumAsIds.Checked = Config.Settings.keepOnlyEnumAsIDs;

            if (Config.Settings.encryption)
                CreateEncryption();

            if (string.IsNullOrEmpty(txtSettingEncryptionKey.Text.Trim()))
                txtSettingEncryptionKey.Text = "168, 220, 184, 133, 78, 149, 8, 249, 171, 138, 98, 170, 95, 15, 211, 200, 51, 242, 4, 193, 219, 181, 232, 99, 16, 240, 142, 128, 29, 163, 245, 24, 204, 73, 173, 32, 214, 76, 31, 99, 91, 239, 232, 53, 138, 195, 93, 195, 185, 210, 155, 184, 243, 216, 204, 42, 138, 101, 100, 241, 46, 145, 198, 66, 11, 17, 19, 86, 157, 27, 132, 201, 246, 112, 121, 7, 195, 148, 143, 125, 158, 29, 184, 67, 187, 100, 31, 129, 64, 130, 26, 67, 240, 128, 233, 129, 63, 169, 5, 211, 248, 200, 199, 96, 54, 128, 111, 147, 100, 6, 185, 0, 188, 143, 25, 103, 211, 18, 17, 249, 106, 54, 162, 188, 25, 34, 147, 3, 222, 61, 218, 49, 164, 165, 133, 12, 65, 92, 48, 40, 129, 76, 194, 229, 109, 76, 150, 203, 251, 62, 54, 251, 70, 224, 162, 167, 183, 78, 103, 28, 67, 183, 23, 80, 156, 97, 83, 164, 24, 183, 81, 56, 103, 77, 112, 248, 4, 168, 5, 72, 109, 18, 75, 219, 99, 181, 160, 76, 65, 16, 41, 175, 87, 195, 181, 19, 165, 172, 138, 172, 84, 40, 167, 97, 214, 90, 26, 124, 0, 166, 217, 97, 246, 117, 237, 99, 46, 15, 141, 69, 4, 245, 98, 73, 3, 8, 161, 98, 79, 161, 127, 19, 55, 158, 139, 247, 39, 59, 72, 161, 82, 158, 25, 65, 107, 173, 5, 255, 53, 28, 179, 182, 65, 162, 17";
        }

        private void chkMergeJsonInSingleExcel_CheckedChanged(object sender, EventArgs e)
        {
            m_MergeJsonsIntoSingleJson1 = chkMergeJsonIntoSingleOne.Checked;
            txtMegedJsonCustomName.ReadOnly = !m_MergeJsonsIntoSingleJson1;
        }

        private void txtMegedJsonCustomName_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnOpenGoogleSheet_Click(object sender, EventArgs e)
        {
            var form = new FrmGoogleSheetSample();
            form.Show();
        }

        private void btnLoadDefaultSettings_Click(object sender, EventArgs e)
        {
            Config.ClearSettings();
            txtSettingOutputDataFilePath.Text = "";
            txtSettingOuputConstantsFilePath.Text = "";
            txtSettingNamespace.Text = "";
            chkSeperateIDs.Checked = false;
            chkSeperateConstants.Checked = false;
            chkSeperateLocalization.Checked = false;
            chkEncrypt.Checked = false;
            chkMergeJsonIntoSingleOne.Checked = false;
            chkMergeJsonIntoSingleOne2.Checked = false;
            chkKeepOnlyEnumAsIds.Checked = false;
            txtSettingEncryptionKey.Text = "168, 220, 184, 133, 78, 149, 8, 249, 171, 138, 98, 170, 95, 15, 211, 200, 51, 242, 4, 193, 219, 181, 232, 99, 16, 240, 142, 128, 29, 163, 245, 24, 204, 73, 173, 32, 214, 76, 31, 99, 91, 239, 232, 53, 138, 195, 93, 195, 185, 210, 155, 184, 243, 216, 204, 42, 138, 101, 100, 241, 46, 145, 198, 66, 11, 17, 19, 86, 157, 27, 132, 201, 246, 112, 121, 7, 195, 148, 143, 125, 158, 29, 184, 67, 187, 100, 31, 129, 64, 130, 26, 67, 240, 128, 233, 129, 63, 169, 5, 211, 248, 200, 199, 96, 54, 128, 111, 147, 100, 6, 185, 0, 188, 143, 25, 103, 211, 18, 17, 249, 106, 54, 162, 188, 25, 34, 147, 3, 222, 61, 218, 49, 164, 165, 133, 12, 65, 92, 48, 40, 129, 76, 194, 229, 109, 76, 150, 203, 251, 62, 54, 251, 70, 224, 162, 167, 183, 78, 103, 28, 67, 183, 23, 80, 156, 97, 83, 164, 24, 183, 81, 56, 103, 77, 112, 248, 4, 168, 5, 72, 109, 18, 75, 219, 99, 181, 160, 76, 65, 16, 41, 175, 87, 195, 181, 19, 165, 172, 138, 172, 84, 40, 167, 97, 214, 90, 26, 124, 0, 166, 217, 97, 246, 117, 237, 99, 46, 15, 141, 69, 4, 245, 98, 73, 3, 8, 161, 98, 79, 161, 127, 19, 55, 158, 139, 247, 39, 59, 72, 161, 82, 158, 25, 65, 107, 173, 5, 255, 53, 28, 179, 182, 65, 162, 17";
            txtSettingExcludedSheet.Text = "";
            txtUnminimizeFields.Text = "id; mode; type; group; level; rank";
        }

        private void chkKeepOnlyEnumAsIds_CheckedChanged(object sender, EventArgs e)
        {
            if (Config.Settings.keepOnlyEnumAsIDs != chkKeepOnlyEnumAsIds.Checked)
            {
                Config.Settings.keepOnlyEnumAsIDs = chkKeepOnlyEnumAsIds.Checked;
                Config.Save();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/nbhung100914/ExcelToUnity/blob/main/ExcelToUnity/Document/DataConverterExample.xlsx");
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            Config.SaveSettingsToFile();
        }

        private void BtnLoadSettings_Click(object sender, EventArgs e)
        {
            if (Config.LoadSettingsFromFile())
            {
                LoadSettings();
                InitializeDtgFiles();
            }
        }
    }
}