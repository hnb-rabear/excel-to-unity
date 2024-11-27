using ChoETL;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Markdig;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelToUnity_DataConverter
{
	public partial class MainForm : Form
	{
		public const string VERSION = "1.5.3";

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
				var list = new List<string>();
				list.Add($"\"id\":{id}");
				if (value != 0) list.Add($"\"value\":{value}");
				if (increase != 0) list.Add($"\"increase\":{increase}");
				if (unlock != 0) list.Add($"\"unlock\":{unlock}");
				if (max != 0) list.Add($"\"max\":{max}");
				if (!string.IsNullOrEmpty(valueString) && valueString != value.ToString()) list.Add($"\"valueString\":\"{valueString}\"");
				if (id == -1 && !string.IsNullOrEmpty(idString)) list.Add($"\"idString\":\"{idString}\"");
				if (values != null) list.Add($"\"values\":{JsonConvert.SerializeObject(values)}");
				if (increases != null) list.Add($"\"increases\":{JsonConvert.SerializeObject(increases)}");
				if (maxes != null) list.Add($"\"maxes\":{JsonConvert.SerializeObject(maxes)}");
				if (unlocks != null) list.Add($"\"unlocks\":{JsonConvert.SerializeObject(unlocks)}");
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
				return string.Compare(name, other.name, StringComparison.Ordinal);
			}
		}

#endregion

		//=================================

#region Constants

		public const string APPLICATION_NAME = "Google Sheet to Unity - Data Converter";
		private const string CONSTANTS_CS_TEMPLATE = "Resources\\Templates\\ConstantsTemplate.txt";
		private const string IDS_CS_TEMPLATE = "Resources\\Templates\\IDsTemplate.txt";
		private const string LOCALIZATION_MANAGER_TEMPLATE = "Resources\\Templates\\LocalizationsManagerTemplate.txt";
		private const string LOCALIZATION_TEMPLATE = "Resources\\Templates\\LocalizationTemplate.txt";
		private const string LOCALIZATION_TEMPLATE_V2 = "Resources\\Templates\\LocalizationTemplateV2.txt";
		private const string LOCALIZATION_TEXT_TEMPLATE = "Resources\\Templates\\LocalizationTextTemplate.txt";
		private const string SETTINGS_CS_TEMPLATE = "Resources\\Templates\\SettingsTemplate.txt";
		private const string IDS_SHEET = "IDs";
		private const string CONSTANTS_SHEET = "Constants";
		private const string SETTINGS_SHEET = "Settings";
		private const string LOCALIZATION_SHEET = "Localization";

#endregion

		//==================================

#region Members

		private List<Sheet> m_sheets = new List<Sheet>();
		private Dictionary<string, int> m_allIds = new Dictionary<string, int>();
		private Dictionary<string, int> m_allIDsSorted; //List sorted by length will be used for linked data, for IDs which have prefix that is exactly same with another ID
		private Dictionary<string, StringBuilder> m_idsBuilderDict = new Dictionary<string, StringBuilder>();
		private Dictionary<string, StringBuilder> m_constantsBuilderDict = new Dictionary<string, StringBuilder>();
		private Dictionary<string, LocalizationBuilder> m_localizationsDict = new Dictionary<string, LocalizationBuilder>();
		private BindingList<ExcelPath> m_bindingExcelPaths = new BindingList<ExcelPath>();
		private IWorkbook m_workBook;
		private Encryption m_encryption;
		private List<string> m_localizedSheetsExported;
		private List<string> m_localizedLanguages;

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

		private string ConvertSheetToJson(IWorkbook pWorkBook, string pSheetName, string pFileName, bool pEncrypt, bool pWriteFile)
		{
			var fieldValueTypes = Helper.GetFieldValueTypes(pWorkBook, pSheetName);
			if (fieldValueTypes == null)
				return "{}";
			return ConvertSheetToJson(pWorkBook, pSheetName, pFileName, fieldValueTypes, pEncrypt, pWriteFile);
		}

		private static void SetupConfigFolders(TextBox pTextBox, ref string pFolderPath)
		{
			if (string.IsNullOrEmpty(pTextBox.Text.Trim()))
				return;

			if (pTextBox.Text.Trim() == pFolderPath)
				return;

			pFolderPath = pTextBox.Text.Trim();

			Config.Save();
		}

#region Load and create IDs

		private bool BuildContentOfFileIDs(IWorkbook pWorkBook, string pSheetName)
		{
			var sheet = pWorkBook.GetSheet(pSheetName);

			if (sheet.IsNull() || sheet.LastRowNum == 0)
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty!");
				return false;
			}

			var idsBuilders = new List<StringBuilder>();
			var idsEnumBuilders = new List<StringBuilder>();
			var idsEnumBuilderNames = new List<string>();
			var idsEnumBuilderIndexes = new List<int>();
			for (int row = 0; row <= sheet.LastRowNum; row++)
			{
				var rowData = sheet.GetRow(row);
				if (rowData != null)
				{
					for (int col = 0; col <= rowData.LastCellNum; col += 3)
					{
						var cellKey = rowData.GetCell(col);
						if (cellKey != null)
						{
							int index = col / 3;
							var sb = index < idsBuilders.Count ? idsBuilders[index] : new StringBuilder();
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
								var cellValue = rowData.GetCell(col + 1);
								if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
								{
									MessageBox.Show($@"Sheet {sheet.SheetName}: Key {key} doesn't have value!", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									continue;
								}

								string valueStr = cellValue.ToString().Trim();
								int.TryParse(valueStr, out int value);
								sb.Append("\tpublic const int ");
								sb.Append(key);
								sb.Append(" = ");
								sb.Append(value);
								sb.Append(";");

								//Comment
								var cellComment = rowData.GetCell(col + 2);
								if (cellComment != null && !string.IsNullOrEmpty(cellComment.ToString().Trim()))
								{
									string cellCommentFormula = Helper.ConvertFormulaCell(cellComment);
									if (cellCommentFormula != null)
										sb.Append(" /*").Append(cellCommentFormula).Append("*/");
									else
										sb.Append(" /*").Append(cellComment).Append("*/");
								}

								if (m_allIds.TryGetValue(key, out int val))
								{
									if (val != value)
										MessageBox.Show($@"ID {key} is duplicated in sheet {pSheetName}", @"Duplicated ID!", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
								else
									m_allIds.AddOrUpdate(key, value);
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
									.Append(cellKey);
							}
							sb.Append(Environment.NewLine);
						}
					}
				}
			}

			m_allIds = m_allIds.OrderBy(m => m.Key).ToDictionary(x => x.Key, x => x.Value);

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
					var enumIndex = str.IndexOf("[enum]", StringComparison.Ordinal);
					str = str.Remove(0, enumIndex + 6).Replace(",", ", ");

					string enumName = idsEnumBuilderNames[i].Replace(" ", "_");

					var enumBuilder = new StringBuilder();
					enumBuilder.Append("\tpublic enum ")
						.Append(enumName)
						.Append(" { ")
						.Append(str)
						.Append(" }\n");
					if (Config.Settings.onlyEnumAsIDs)
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

			if (m_idsBuilderDict.ContainsKey(pSheetName))
			{
				m_idsBuilderDict[pSheetName].AppendLine();
				m_idsBuilderDict[pSheetName].Append(builder);
			}
			else
				m_idsBuilderDict.Add(pSheetName, builder);

			return true;
		}

		private void LoadSheetIDsValues(IWorkbook pWorkBook, string pSheetName)
		{
			var sheet = pWorkBook.GetSheet(pSheetName);

			if (sheet.IsNull() || sheet.LastRowNum == 0)
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty!");
				return;
			}

			for (int row = 0; row <= sheet.LastRowNum; row++)
			{
				var rowData = sheet.GetRow(row);
				if (rowData == null)
					continue;
				for (int col = 0; col <= rowData.LastCellNum; col += 3)
				{
					var cellIdName = rowData.GetCell(col);
					if (cellIdName == null)
						continue;
					string key = cellIdName.ToString().Trim();
					if (row <= 0 || string.IsNullOrEmpty(key))
						continue;
					var cellIdValue = rowData.GetCell(col + 1);
					if (cellIdValue == null)
						continue;
					int value = int.Parse(cellIdValue.ToString().Trim());

					if (m_allIds.ContainsKey(key))
						MessageBox.Show($@"ID {key} is duplicated in sheet {pSheetName}", @"Duplicated ID!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					m_allIds.AddOrUpdate(key, value);
				}
			}

			m_allIds = m_allIds.OrderBy(m => m.Key).ToDictionary(x => x.Key, x => x.Value);
		}

		private void CreateFileIDs(string exportFileName, string content)
		{
			string fileContent = File.ReadAllText(IDS_CS_TEMPLATE);
			fileContent = fileContent.Replace("_IDS_CLASS_NAME_", exportFileName);
			fileContent = fileContent.Replace("public const int _FIELDS_ = 0;", content);
			fileContent = AddNamespace(fileContent);

			Helper.WriteFile(Config.Settings.constantsOutputFolder, $"{exportFileName}.cs", fileContent);
			Log(LogType.Message, $"Exported {exportFileName}.cs!");
		}

		private bool CheckExistedId(string pKey)
		{
			foreach (var id in m_allIds)
				if (id.Key == pKey.Trim())
					return true;
			return false;
		}

		private int GetReferenceId(string pKey, out bool pFound)
		{
			if (m_allIDsSorted == null || m_allIDsSorted.Count == 0)
			{
				m_allIDsSorted = Helper.SortIDsByLength(m_allIds);
			}

			if (!string.IsNullOrEmpty(pKey))
			{
				if (int.TryParse(pKey, out int value))
				{
					pFound = true;
					return value;
				}

				if (m_allIDsSorted.TryGetValue(pKey, out int id))
				{
					pFound = true;
					return id;
				}
			}

			pFound = false;
			return 0;
		}

#endregion

#region Load and create Constants

		private void LoadSheetConstantsData(IWorkbook pWorkbook, string pSheetName)
		{
			var sheet = pWorkbook.GetSheet(pSheetName);
			if (sheet.IsNull() || sheet.LastRowNum == 0)
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty!");
				return;
			}

			var constants = new List<ConstantBuilder>();
			for (int row = 0; row <= sheet.LastRowNum; row++)
			{
				var rowData = sheet.GetRow(row);
				if (rowData != null)
				{
					string name = null;
					string value = null;
					string valueType = null;
					string comment = null;
					var cell = rowData.GetCell(0); //Name
					if (cell != null)
						name = cell.ToString().Trim();

					cell = rowData.GetCell(1); //Type
					if (cell != null)
						valueType = cell.ToString().Trim();

					cell = rowData.GetCell(2); //Value
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
			BuildContentOfFileConstants(constants, pSheetName);
		}

		private void BuildContentOfFileConstants(List<ConstantBuilder> constants, string constantsSheet)
		{
			var constantsSB = new StringBuilder("");
			for (int i = 0; i < constants.Count; i++)
			{
				string name = constants[i].name;
				string value = constants[i].value;
				string valueType = constants[i].valueType;
				string comment = constants[i].comment;
				string fieldStr = "";

				//Try to find references in ids list
				if (valueType == "int" && !int.TryParse(value, out int _))
				{
					int outValue = GetReferenceId(value, out bool found);
					if (found)
						value = outValue.ToString();
				}
				if (valueType == "int-array")
				{
					string[] strValues = Helper.SplitValueToArray(value);
					for (int j = 0; j < strValues.Length; j++)
					{
						//Try to find references in ids list
						if (int.TryParse(strValues[j].Trim(), out int _))
							continue;

						int refVal = GetReferenceId(strValues[j], out bool found);
						if (found)
						{
							value = value.Replace(strValues[j], refVal.ToString());
							strValues[j] = refVal.ToString();
						}
					}
				}

				switch (valueType)
				{
					case "int":
						fieldStr = $"\tpublic const int {name} = {value.Trim()};";
						break;
					case "float":
						fieldStr = $"\tpublic const float {name} = {value.Trim()}f;";
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
						fieldStr = $"\tpublic static readonly float[] {name} = new float[{floatValues.Length}] {"{"} {floatArrayStr} {"}"};";
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
						fieldStr = $"\tpublic static readonly int[] {name} = new int[{intValues.Length}] {"{"} {intArrayStr} {"}"};";
						break;
					case "vector2":
						string[] vector2Values = Helper.SplitValueToArray(value);
						fieldStr = $"\tpublic static readonly Vector2 {name} = new Vector2({vector2Values[0].Trim()}f, {vector2Values[1].Trim()}f);";
						break;
					case "vector3":
						string[] vector3Values = Helper.SplitValueToArray(value);
						fieldStr = $"\tpublic static readonly Vector3 {name} = new Vector3({vector3Values[0].Trim()}f, {vector3Values[1].Trim()}f, {vector3Values[2].Trim()}f);";
						break;
					case "string":
						fieldStr = $"\tpublic const string {name} = \"{value.Trim()}\";";
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
						fieldStr = $"\tpublic static readonly string[] {name} = new string[{values.Length}] {"{"} {arrayStr} {"}"};";
					}
						break;
				}

				if (fieldStr != "")
				{
					if (!string.IsNullOrEmpty(comment))
						fieldStr += $" /*{comment}*/";
					constantsSB.Append(fieldStr).AppendLine();
				}
			}

			if (m_constantsBuilderDict.ContainsKey(constantsSheet))
				m_constantsBuilderDict[constantsSheet].AppendLine();
			else
				m_constantsBuilderDict.Add(constantsSheet, new StringBuilder());
			m_constantsBuilderDict[constantsSheet].Append(constantsSB);
		}

		private void CreateFileConstants(string pContent, string pExportFileName)
		{
			string fileContent = File.ReadAllText(CONSTANTS_CS_TEMPLATE);
			fileContent = fileContent.Replace("_CONST_CLASS_NAME_", pExportFileName);
			fileContent = fileContent.Replace("public const int _FIELDS_ = 0;", pContent);
			fileContent = AddNamespace(fileContent);

			Helper.WriteFile(Config.Settings.constantsOutputFolder, pExportFileName + ".cs", fileContent);
			Log(LogType.Message, $"Exported {pExportFileName}.cs!");
		}

#endregion

		private string ConvertSheetToJson(IWorkbook pWorkBook, string pSheetName, string pOutputFile, List<FieldValueType> pFieldValueTypes, bool pEncrypt, bool pAutoWriteFile)
		{
			var sheet = pWorkBook.GetSheet(pSheetName);
			if (sheet.IsNull() || sheet.LastRowNum == 0)
			{
				Log(LogType.Warning, $"Sheet {sheet.SheetName} is empty!");
				return null;
			}

			var persistentFields = Config.Settings.GetPersistentFields();
			int lastCellNum = 0;
			string[] fields = null;
			string[] mergeValues = null;
			bool[] validCols = null;
			var rowContents = new List<RowContent>();

			for (int row = 0; row <= sheet.LastRowNum; row++)
			{
				var rowData = sheet.GetRow(row);
				if (rowData.IsNull())
					continue;

				if (row == 0)
				{
					lastCellNum = rowData.LastCellNum;
					fields = new string[lastCellNum];
					mergeValues = new string[lastCellNum];
					validCols = new bool[lastCellNum];

					//Find valid columns
					for (int col = 0; col < lastCellNum; col++)
					{
						var cell = rowData.GetCell(col);
						if (cell != null
							&& cell.CellType == CellType.String
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
						var cell = rowData.GetCell(col);
						if (fields != null)
						{
							string fieldName = fields[col];
							string fieldValue = cell.ToCellString().Trim();

							if (cell != null && cell.IsMergedCell && !string.IsNullOrEmpty(fieldValue))
								mergeValues[col] = fieldValue;
							if (cell != null && cell.IsMergedCell && string.IsNullOrEmpty(fieldValue))
								fieldValue = mergeValues[col];

							fieldName = fieldName.Replace(" ", "_");
							rowContent.fieldNames.Add(fieldName);
							rowContent.fieldValues.Add(fieldValue);
						}
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
				bool rowIsEmpty = true; //Because Loading sheet sometime includes the empty rows, I don't know why it happen
				var nestedObjects = new List<JObject>();

				for (int j = 0; j < rowContent.fieldNames.Count; j++)
				{
					bool valid = validCols[j];
					if (!valid)
						continue;
					string fieldName = rowContent.fieldNames[j];
					string fieldValue = rowContent.fieldValues[j];
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
						var att = new Att();
						att.id = GetReferenceId(fieldValue, out bool found);
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
										att.unlock = GetReferenceId(fieldValue, out found);
								}
								else
								{
									string[] inValues = Helper.SplitValueToArray(fieldValue, false);
									float[] outValues = new float[inValues.Length];
									for (int t = 0; t < inValues.Length; t++)
									{
										if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
											outValues[t] = GetReferenceId(inValues[t].Trim(), out found);
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
										att.increase = GetReferenceId(fieldValue, out found);
								}
								else
								{
									string[] inValues = Helper.SplitValueToArray(fieldValue, false);
									float[] outValues = new float[inValues.Length];
									for (int t = 0; t < inValues.Length; t++)
									{
										if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
											outValues[t] = GetReferenceId(inValues[t].Trim(), out found);
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
										att.value = GetReferenceId(fieldValue, out found);
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
											outValues[t] = GetReferenceId(inValues[t].Trim(), out found);
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
										att.max = GetReferenceId(fieldValue, out found);
								}
								else
								{
									string[] inValues = Helper.SplitValueToArray(fieldValue, false);
									float[] outValues = new float[inValues.Length];
									for (int t = 0; t < inValues.Length; t++)
									{
										if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
											outValues[t] = GetReferenceId(inValues[t].Trim(), out found);
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
						bool IsPersistentField()
						{
							bool importantField = false;
							foreach (var value in persistentFields)
							{
								if (fieldName.StartsWith(value))
								{
									return true;
								}
							}
							return false;
						}
						
						//Ignore empty field or field have value which equal 0
						if ((string.IsNullOrEmpty(fieldValue) || fieldValue == "0") && !IsPersistentField())
							continue;

						bool nestedFiled = fieldName.Contains(".");
						foreach (var field in pFieldValueTypes)
						{
							//Find referenced Id in string and convert it to number
							if (field.name == fieldName)
							{
								string fieldType = field.type;
								bool referencedId = false;
								if (fieldType == "string") //Find and replace string value with referenced ID
								{
									if (CheckExistedId(fieldValue))
									{
										fieldType = "number";
										referencedId = true;
									}
									else if (int.TryParse(fieldValue, out int _))
									{
										fieldType = "number";
										referencedId = true;
									}
								}
								if (fieldType == "array-string") //Find and replace string value with referenced ID
								{
									string[] arrayValue = Helper.SplitValueToArray(fieldValue, false);
									foreach (string val in arrayValue)
									{
										if (CheckExistedId(val.Trim()))
										{
											fieldType = "array-number";
											referencedId = true;
											break;
										}
									}
								}

								var jsonObject = new JObject();
								switch (fieldType)
								{
									case "number":
										if (referencedId)
										{
											int intValue = GetReferenceId(fieldValue, out bool _);
											if (!nestedFiled)
												fieldContentStr += $"\"{fieldName}\":{intValue},";
											jsonObject[fieldName] = intValue;
										}
										else
										{
											if (!nestedFiled)
												fieldContentStr += $"\"{fieldName}\":{fieldValue},";
											jsonObject[fieldName] = fieldValue;
										}
										break;

									case "string":
										fieldValue = fieldValue.Replace("\n", "\\n");
										fieldValue = fieldValue.Replace("\"", "\\\"");
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":\"{fieldValue}\",";
										else
											jsonObject[fieldName] = fieldValue;
										break;

									case "bool":
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{fieldValue.ToLower()},";
										else
											jsonObject[fieldName] = fieldValue;
										break;

									case "array-number":
									{
										fieldName = fieldName.Replace("[]", "");
										var arrayValue = Helper.SplitValueToArray(fieldValue, false);
										var arrayStr = "[";
										for (int k = 0; k < arrayValue.Length; k++)
										{
											string val = arrayValue[k].Trim();
											if (referencedId)
												val = GetReferenceId(val, out bool _).ToString();
											if (k == 0) arrayStr += val;
											else arrayStr += "," + val;
										}
										arrayStr += "]";
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{arrayStr},";
										else
										{
											int[] array = JsonConvert.DeserializeObject<int[]>(arrayStr);
											jsonObject[fieldName] = JArray.FromObject(array);
										}
									}
										break;

									case "array-string":
									{
										fieldName = fieldName.Replace("[]", "");
										var arrayValue = Helper.SplitValueToArray(fieldValue, false);
										var arrayStr = "[";
										for (int k = 0; k < arrayValue.Length; k++)
										{
											if (k == 0) arrayStr += $"\"{arrayValue[k].Trim()}\"";
											else arrayStr += $",\"{arrayValue[k].Trim()}\"";
										}
										arrayStr += "]";
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{arrayStr},";
										else
										{
											string[] array = JsonConvert.DeserializeObject<string[]>(arrayStr);
											jsonObject[fieldName] = JArray.FromObject(array);
										}
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
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{arrayStr},";
										else
										{
											bool[] array = JsonConvert.DeserializeObject<bool[]>(arrayStr);
											jsonObject[fieldName] = JArray.FromObject(array);
										}
									}
										break;

									case "json":
									{
										fieldName = fieldName.Replace("{}", "");

										//Search Id in field value
										if (m_allIDsSorted == null || m_allIDsSorted.Count == 0)
										{
											m_allIDsSorted = Helper.SortIDsByLength(m_allIds);
										}
										foreach (var id in m_allIDsSorted)
										{
											if (fieldValue.Contains(id.Key))
												fieldValue = fieldValue.Replace(id.Key, id.Value.ToString());
										}
										if (!Helper.IsValidJson(fieldValue))
										{
											MessageBox.Show($@"Invalid Json string at Sheet: {pSheetName} Field: {fieldName} Row: {i + 1}",
												@"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
											Log(LogType.Error, $"Invalid data, Sheet: {pSheetName}, Field: {fieldName}, Row: {i + 1}");
										}
										var tempObj = JsonConvert.DeserializeObject(fieldValue);
										var tempJsonStr = JsonConvert.SerializeObject(tempObj);
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{tempJsonStr},";
										else
										{
											jsonObject[fieldName] = JObject.Parse(tempJsonStr);
										}
									}
										break;
								}

								// Nested Object
								if (nestedFiled)
									nestedObjects.Add(jsonObject);
							}
						}
					}
				}
				if (nestedObjects.Count > 0)
				{
					var nestedObjectsJson = Helper.ConvertToNestedJson(nestedObjects);
					fieldContentStr += $"{nestedObjectsJson.Substring(1, nestedObjectsJson.Length - 2)}";
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
				if (nestedObjects.Count == 0)
					fieldContentStr = Helper.RemoveLast(fieldContentStr, ",");


				if (!rowIsEmpty)
					content += $"{"{"}{fieldContentStr}{"},"}";
			}
			content = Helper.RemoveLast(content, ",");
			content += "]";

			if (content == "[]")
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty!");
				return null;
			}
			string finalContent = content;
			if (pEncrypt && m_encryption != null)
				finalContent = m_encryption.EncryptValue(content);

			if (pAutoWriteFile)
			{
				Helper.WriteFile(Config.Settings.jsonOutputFolder, $"{pOutputFile}.txt", finalContent);
				if (pEncrypt && m_encryption != null)
					Log(LogType.Message, $"Exported encrypted Json data to {pOutputFile}.txt.");
				else
					Log(LogType.Message, $"Exported Json data to {pOutputFile}.txt.");
			}
			return finalContent;
		}

		private string ExportSettingsSheetToScriptableObject(IWorkbook workbook, string sheetName)
		{
			string nameSpace = Config.Settings.@namespace;

			var sheet = workbook.GetSheet(sheetName);
			var fieldsDict = new Dictionary<string, string>();
			var fields = new List<string>();

			for (int row = 0; row <= sheet.LastRowNum; row++)
			{
				var rowData = sheet.GetRow(row);
				if (rowData != null)
				{
					string fieldStr = "";
					string name = null;
					string value = null;
					string valueType = null;
					var cell = rowData.GetCell(0);
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
							fieldStr = $"\"{name}\":{value}";
							break;
						case "bool":
							fieldStr = $"\"{name}\":{value.ToLower()}";
							break;
						case "float-array":
						case "int-array":
						case "float-list":
						case "int-list":
							string[] parts = Helper.SplitValueToArray(value);
							fieldStr = $"\"{name}\":[{string.Join(",", parts)}]";
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
							fieldStr = $"\"{name}\":\"{value}\"";
							break;
						case "string-array":
						case "string-list":
							string[] partsString = Helper.SplitValueToArray(value);
							string[] jsonFromatString = new string[partsString.Length];
							for (int ii = 0; ii < partsString.Length; ii++)
							{
								jsonFromatString[ii] = $"\"{partsString[ii]}\"";
							}
							fieldStr = $"\"{name}\":[{string.Join(",", jsonFromatString)}]";
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

				string str = $"public {fieldType} {field.Key};";
				builder.Append(str);
				builder.Append(Environment.NewLine);
			}
			string csFileTemplate = File.ReadAllText(SETTINGS_CS_TEMPLATE);
			string className = sheetName.Replace(" ", "_");
			csFileTemplate = csFileTemplate.Replace("_SETTINGS_CLASS_NAME_", className);
			csFileTemplate = csFileTemplate.Replace("//#REPLACE_FIELDS", "\t\t" + builder);
			if (!string.IsNullOrEmpty(nameSpace))
			{
				csFileTemplate = $"namespace {nameSpace}\n{"{"}\n{csFileTemplate}\n{"}"}";
			}

			Helper.WriteFile(Config.Settings.constantsOutputFolder, className + ".cs", csFileTemplate);
			Log(LogType.Message, $"Exported {className}.cs");

			return content;
		}

		private void RefreshDtgExcelFiles()
		{
			//for (int i = Config.Settings.allFiles.Count - 1; i >= 0; i--)
			//{
			//	//Check if file exist
			//	if (!File.Exists(Config.Settings.allFiles[i].path))
			//		Config.Settings.allFiles.RemoveAt(i);
			//}
			if (Config.Settings.allFiles != null)
				Config.Settings.allFiles.Sort();
			
			m_bindingExcelPaths.ResetBindings();
			DtgFilePaths.Refresh();
			DtgFilePaths.AutoResizeColumns();

			CheckFilesExist();
		}

		private void InitExcels()
		{
			DtgFilePaths.AutoGenerateColumns = false;
			m_bindingExcelPaths = new BindingList<ExcelPath>(Config.Settings.allFiles);
			DtgFilePaths.DataSource = m_bindingExcelPaths;

			CheckFilesExist();
		}

		private void CheckFilesExist()
		{
			foreach (DataGridViewRow row in DtgFilePaths.Rows)
				ValidatePathRow(row);
		}

		private void CreateEncryption()
		{
			m_encryption = Helper.CreateEncryption(txtSettingEncryptionKey.Text);
			if (m_encryption == null)
			{
				MessageBox.Show(@"Encryption key is invalid", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Log(LogType.Error, "Encryption key is invalid");
			}
		}

		private static void ExcludeSheets(ref List<Sheet> pSheets)
		{
			var excludedSheets = Config.Settings.GetExcludedSheets();
			if (excludedSheets == null)
				return;
			for (int i = pSheets.Count - 1; i >= 0; i--)
			{
				if (excludedSheets.Contains(pSheets[i].SheetName.ToLower()))
					pSheets.RemoveAt(i);
			}
		}

		private void LoadSheetLocalizationData(IWorkbook pWorkBook, string pSheetName)
		{
			var sheet = pWorkBook.GetSheet(pSheetName);
			if (sheet.IsNull() || sheet.LastRowNum == 0)
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty!");
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
								break;
							idStrings.Add(fieldValue);
						}
						//relativeId
						else if (col == 1 && row > 0)
						{
							if (string.IsNullOrEmpty(fieldValue) || m_allIds == null)
								continue;
							bool existId = false;
							foreach (var id in m_allIds)
								if (id.Key.Trim() == fieldValue.Trim())
								{
									fieldValue = id.Value.ToString();
									idStrings[idStrings.Count - 1] = $"{idStrings[idStrings.Count - 1]}_{id.Value}";
									existId = true;
									break;
								}

							if (!existId)
								idStrings[idStrings.Count - 1] = $"{idStrings[idStrings.Count - 1]}_{fieldValue}";
						}
						//languages
						else if (col > 1 && row > 0)
						{
							if (!textDict.ContainsKey(fieldName))
								textDict.Add(fieldName, new List<string>());
							textDict[fieldName].Add(fieldValue);
						}
					}
				}
			}

			if (m_localizationsDict.ContainsKey(pSheetName))
			{
				var builder = m_localizationsDict[pSheetName];
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
				m_localizationsDict[pSheetName] = new LocalizationBuilder()
				{
					idsString = idStrings,
					languageTextDict = textDict,
				};
			}
			else
				m_localizationsDict.Add(pSheetName, new LocalizationBuilder()
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

			//Build id integer array
			var idBuilder = new StringBuilder();
			if (pIdsString.Count > 0)
			{
				idBuilder.Append("\tpublic const int");
				idBuilder.Append("\n\t\t");
				for (int i = 0; i < pIdsString.Count; i++)
				{
					if (i > 0 && i % 100 == 0)
						idBuilder.Append("\n\t\t");

					if (i < pIdsString.Count - 1)
						idBuilder.Append($"{pIdsString[i].RemoveSpecialCharacters()} = {i}, ");
					else
						idBuilder.Append($"{pIdsString[i].RemoveSpecialCharacters()} = {i};");
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
					idBuilder2.Append($"{pIdsString[i].RemoveSpecialCharacters()},");
				}
				else
				{
					if (i == 0)
						idBuilder2.Append($"{pIdsString[i].RemoveSpecialCharacters()} = {i},");
					else
						idBuilder2.Append($" {pIdsString[i].RemoveSpecialCharacters()},");
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
					idStringDictBuilder.Append($"\"{pIdsString[i]}\",");
				}
				else if (i == 0)
					idStringDictBuilder.Append($"\"{pIdsString[i]}\",");
				else
					idStringDictBuilder.Append($" \"{pIdsString[i]}\",");
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
					languagePackContent.Append($"\"{text}\"");

					if (i < listText.Value.Count - 1)
						languagePackContent.Append(", ");
				}
				languagePackContent.Append("\n\t};");

				if (listText.Key != pLanguageTextDict.Last().Key)
					allLanguagePackBuilder.Append(languagePackContent).AppendLine();
				else
					allLanguagePackBuilder.Append(languagePackContent);
				allLanguagePackBuilder.AppendLine();
			}

			//Build language dictionary
			var languageFilesBuilder = new StringBuilder();
			languageFilesBuilder.Append("\tpublic static readonly Dictionary<string, string[]> language = new Dictionary<string, string[]>() { ");
			foreach (var listText in pLanguageTextDict)
			{
				languageFilesBuilder.Append($" {"{"} \"{listText.Key}\", {listText.Key} {"},"}");
			}
			languageFilesBuilder.Append(" };\n");
			languageFilesBuilder.Append($"\tpublic static readonly string DefaultLanguage = \"{pLanguageTextDict.First().Key}\";");

			//Write file
			string fileTemplateContent = File.ReadAllText(LOCALIZATION_TEMPLATE);
			fileTemplateContent = fileTemplateContent.Replace("LOCALIZATION_CLASS_NAME", pFileName);
			fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_ENUM", idBuilder2.ToString());
			fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_CONST", idBuilder.ToString());
			fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY_KEY_STRING", idStringDictBuilder.ToString());
			fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_LIST", allLanguagePackBuilder.ToString());
			fileTemplateContent = fileTemplateContent.Replace("//LOCALIZED_DICTIONARY", languageFilesBuilder.ToString());
			fileTemplateContent = fileTemplateContent.Replace("LOCALIZATION_FOLDER", Config.Settings.GetLocalizationFolder(out bool isAddressable));
			fileTemplateContent = fileTemplateContent.Replace("IS_ADDRESSABLE", isAddressable.ToString().ToLower());
			Helper.WriteFile(Config.Settings.constantsOutputFolder, pFileName + ".cs", fileTemplateContent);
			Log(LogType.Message, $"Exported {pFileName}.cs!");
		}

		private Dictionary<string, string> m_langCharSets;

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
				idBuilder.Append("\tpublic const int");
				idBuilder.Append("\n\t\t");
				for (int i = 0; i < pIdsString.Count; i++)
				{
					if (i > 0 && i % 100 == 0)
						idBuilder.Append("\n\t\t");

					if (i < pIdsString.Count - 1)
						idBuilder.Append($"{pIdsString[i].RemoveSpecialCharacters()} = {i}, ");
					else
						idBuilder.Append($"{pIdsString[i].RemoveSpecialCharacters()} = {i};");
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
					idBuilder2.Append($"{pIdsString[i].RemoveSpecialCharacters()},");
				}
				else
				{
					if (i == 0)
						idBuilder2.Append($"{pIdsString[i].RemoveSpecialCharacters()} = {i},");
					else
						idBuilder2.Append($" {pIdsString[i].RemoveSpecialCharacters()},");
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
					idStringDictBuilder.Append($"\"{pIdsString[i]}\",");
				}
				else if (i == 0)
					idStringDictBuilder.Append($"\"{pIdsString[i]}\",");
				else
					idStringDictBuilder.Append($" \"{pIdsString[i]}\",");
			}
			idStringDictBuilder.Append("\n\t};");

			//Build language json data
			foreach (var listText in pLanguageTextDict)
			{
				string json = JsonConvert.SerializeObject(listText.Value);
				Helper.WriteFile(Config.Settings.localizationOutputFolder, $"{pFileName}_{listText.Key}.txt", json);
				Log(LogType.Message, $"Exported Localization content to {pFileName}_{listText.Key}.txt!");

				if (Config.Settings.langCharSets != null && Config.Settings.langCharSets.Contains(listText.Key))
				{
					if (m_langCharSets.ContainsKey(listText.Key))
						m_langCharSets[listText.Key] += json;
					else
						m_langCharSets[listText.Key] = json;
				}
			}

			//Build language dictionary
			var languagesDictBuilder = new StringBuilder();
			languagesDictBuilder.Append("\tpublic static readonly Dictionary<string, string> LanguageFiles = new Dictionary<string, string>() { ");
			foreach (var textsList in pLanguageTextDict)
			{
				languagesDictBuilder.Append($" {"{"} \"{textsList.Key}\", {$"\"{pFileName}_{textsList.Key}\""} {"},"}");

				if (!m_localizedLanguages.Contains(textsList.Key))
					m_localizedLanguages.Add(textsList.Key);
			}
			languagesDictBuilder.Append(" };\n");
			languagesDictBuilder.Append($"\tpublic static readonly string DefaultLanguage = \"{pLanguageTextDict.First().Key}\";");

			//Write file localization constants
			string fileContent = File.ReadAllText(LOCALIZATION_TEMPLATE_V2);
			fileContent = fileContent.Replace("LOCALIZATION_CLASS_NAME", pFileName);
			fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY_KEY_ENUM", idBuilder2.ToString());
			fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY_KEY_CONST", idBuilder.ToString());
			fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY_KEY_STRING", idStringDictBuilder.ToString());
			fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY", languagesDictBuilder.ToString());
			fileContent = fileContent.Replace("LOCALIZATION_FOLDER", Config.Settings.GetLocalizationFolder(out bool isAddressable));
			fileContent = fileContent.Replace("IS_ADDRESSABLE", isAddressable.ToString().ToLower());
			fileContent = AddNamespace(fileContent);
			Helper.WriteFile(Config.Settings.constantsOutputFolder, $"{pFileName}.cs", fileContent);
			Log(LogType.Message, $"Exported {pFileName}.cs!");
			
			//Write file localized text component
			fileContent = File.ReadAllText(LOCALIZATION_TEXT_TEMPLATE);
			fileContent = fileContent.Replace("LOCALIZATION_CLASS_NAME", pFileName);
			fileContent = AddNamespace(fileContent);
			Helper.WriteFile(Config.Settings.constantsOutputFolder, $"{pFileName}Text.cs", fileContent);
			Log(LogType.Message, $"Exported {pFileName}Text.cs!");
		}

		/// <summary>
		/// Incase we have more thane one localizations file, we need a middle manager to initialize them
		/// </summary>
		private void CreateLocalizationsManagerFile()
		{
			if (m_localizedSheetsExported.Count > 0)
			{
				//Build language dictionary
				var languagesDictBuilder = new StringBuilder();
				var systemLanguages = new StringBuilder();
				languagesDictBuilder.Append("\tpublic static readonly List<string> languages = new List<string>() { ");
				foreach (var lang in m_localizedLanguages)
				{
					languagesDictBuilder.Append($"\"{lang}\", ");

					string langLower = lang.ToLower();
					if (langLower.Contains("english") || langLower == "en")
						systemLanguages.Append($"\t\t\tSystemLanguage.English => \"{lang}\",").AppendLine();
					else if (langLower.Contains("vietnam") || langLower == "vn")
						systemLanguages.Append($"\t\t\tSystemLanguage.Vietnamese => \"{lang}\",").AppendLine();
					else if (langLower.Contains("spanish") || langLower == "es")
						systemLanguages.Append($"\t\t\tSystemLanguage.Spanish => \"{lang}\",").AppendLine();
					else if (langLower.Contains("portugal") || langLower.Contains("portuguese") || langLower == "pt")
						systemLanguages.Append($"\t\t\tSystemLanguage.Portuguese => \"{lang}\",").AppendLine();
					else if (langLower.Contains("russia") || langLower == "ru")
						systemLanguages.Append($"\t\t\tSystemLanguage.Russian => \"{lang}\",").AppendLine();
					else if (langLower.Contains("germany") || langLower.Contains("german") || langLower == "de")
						systemLanguages.Append($"\t\t\tSystemLanguage.German => \"{lang}\",").AppendLine();
					else if (langLower.Contains("indonesia") || langLower == "id")
						systemLanguages.Append($"\t\t\tSystemLanguage.Indonesian => \"{lang}\",").AppendLine();
					else if (langLower.Contains("thai") || langLower == "th")
						systemLanguages.Append($"\t\t\tSystemLanguage.Thai => \"{lang}\",").AppendLine();
					else if (langLower.Contains("korea") || langLower == "kr")
						systemLanguages.Append($"\t\t\tSystemLanguage.Korean => \"{lang}\",").AppendLine();
					else if (langLower.Contains("japan") || langLower == "jp")
						systemLanguages.Append($"\t\t\tSystemLanguage.Japanese => \"{lang}\",").AppendLine();
					else if (langLower.Contains("french") || langLower == "fr")
						systemLanguages.Append($"\t\t\tSystemLanguage.French => \"{lang}\",").AppendLine();
					else if (langLower.Contains("italian") || langLower == "it")
						systemLanguages.Append($"\t\t\tSystemLanguage.Italian => \"{lang}\",").AppendLine();
					else if (langLower.Contains("chinese") || langLower == "cn")
						systemLanguages.Append($"\t\t\tSystemLanguage.ChineseSimplified => \"{lang}\",").AppendLine();
					else if (langLower.Contains("arabic") || langLower == "ar")
						systemLanguages.Append($"\t\t\tSystemLanguage.Arabic => \"{lang}\",").AppendLine();
				}
				systemLanguages.Append($"\t\t\t_ => \"{m_localizedLanguages[0]}\",").AppendLine();
				languagesDictBuilder.Append("};\n");
				languagesDictBuilder.Append($"\tpublic static readonly string DefaultLanguage = \"{m_localizedLanguages.First()}\";");

				//Build initialization code
				var initLines = new StringBuilder();
				var initAsynLines = new StringBuilder();
				var setFolder = new StringBuilder();
				var useAddressable = new StringBuilder();

				for (int i = 0; i < m_localizedSheetsExported.Count; i++)
				{
					initLines.Append($"\t\t{m_localizedSheetsExported[i]}.Init();");
					if (i < m_localizedSheetsExported.Count - 1)
						initLines.Append(Environment.NewLine);

					initAsynLines.Append($"\t\tyield return {m_localizedSheetsExported[i]}.InitAsync();");
					if (i < m_localizedSheetsExported.Count - 1)
						initAsynLines.Append(Environment.NewLine);

					setFolder.Append($"\t\t{m_localizedSheetsExported[i]}.Folder = pFolder;");
					if (i < m_localizedSheetsExported.Count - 1)
						setFolder.Append(Environment.NewLine);

					useAddressable.Append($"\t\t{m_localizedSheetsExported[i]}.Addressable = pValue;");
					if (i < m_localizedSheetsExported.Count - 1)
						useAddressable.Append(Environment.NewLine);
				}

				string fileContent = File.ReadAllText(LOCALIZATION_MANAGER_TEMPLATE);
				fileContent = fileContent.Replace("//LOCALIZATION_INIT_ASYNC", initAsynLines.ToString());
				fileContent = fileContent.Replace("//LOCALIZATION_INIT", initLines.ToString());
				fileContent = fileContent.Replace("//LOCALIZED_DICTIONARY", languagesDictBuilder.ToString());
				fileContent = fileContent.Replace("//LOCALIZATION_SET_FOLDER", setFolder.ToString());
				fileContent = fileContent.Replace("//LOCALIZATION_USE_ADDRESSABLE", useAddressable.ToString());
				fileContent = fileContent.Replace("//LOCALIZATION_SYSTEM_LANGUAGE", systemLanguages.ToString());
				fileContent = fileContent.Replace("LOCALIZATION_FOLDER", Config.Settings.GetLocalizationFolder(out bool isAddressable));
				fileContent = fileContent.Replace("IS_ADDRESSABLE", isAddressable.ToString().ToLower());
				fileContent = AddNamespace(fileContent);
				Helper.WriteFile(Config.Settings.constantsOutputFolder, "LocalizationsManager.cs", fileContent);
				Log(LogType.Message, $"Exported LocalizationsManager.cs!");
			}
		}

		private string AddNamespace(string fileContent)
		{
			if (!string.IsNullOrEmpty(Config.Settings.@namespace))
			{
				fileContent = fileContent.Replace(Environment.NewLine, $"NEW_LINE");
				fileContent = fileContent.Replace("\n", $"NEW_LINE");
				fileContent = fileContent.Replace("NEW_LINE", $"{Environment.NewLine}\t");
				fileContent = $"namespace {Config.Settings.@namespace}\n{"{"}\n\t{fileContent}\n{"}"}";
			}
			return fileContent;
		}

#endregion

		//====================================================

#region Events

		private void MainForm_Load(object sender, EventArgs e)
		{
			Config.Init();
			LoadSettings();
			InitExcel();
			InitExcels();

			// btnOpenGoogleSheet.Visible = false;
			tabMenu.TabPages.RemoveByKey("tabPage2");

			// Set up the delays for the ToolTip.
			toolTip.ShowAlways = true;

			const string changelogPath = "changelog.md";
			using (var reader = new StreamReader(changelogPath))
			{
				string content = reader.ReadToEnd();
				string htmlContent = Markdown.ToHtml(content);
				weboxChangelog.DocumentText = htmlContent;
			}

			string helpPath = "Resources\\help.md";
			using (var reader = new StreamReader(helpPath))
			{
				string content = reader.ReadToEnd();
				string htmlContent = Markdown.ToHtml(content);
				weboxHelp.DocumentText = htmlContent;
			}

			txtVersion.Text = VERSION;

			//Validate user
			TabPage tpEncryption = tabMenu.TabPages[3];
			TabPage tpChangeLog = tabMenu.TabPages[5];
			TabPage tpExportMultiExcels = tabMenu.TabPages[1];
			TabPage tpExportMultiGoogleSheets = tabMenu.TabPages[6];
			switch (Config.User.user)
			{
				case UserType.None:
					tabMenu.TabPages.Remove(tpExportMultiExcels);
					tabMenu.TabPages.Remove(tpEncryption);
					tabMenu.TabPages.Remove(tpChangeLog);
					tabMenu.TabPages.Remove(tpExportMultiGoogleSheets);
					txtSettingEncryptionKey.Visible = false;
					lblEncryptionKey.Visible = false;
					chkSettingEnableEncryption.Visible = false;
					chkSettingEnableEncryption.Checked = false;
					TxtGoogleClientID.Visible = false;
					TxtGoogleClientSecret.Visible = false;
					lblGoogleClientId.Visible = false;
					lblGoogleClientSecret.Visible = false;
					txtSettingExcludedSheet.Visible = false;
					lblbExcluedSheets.Visible = false;
					break;
				case UserType.Pro:
				case UserType.Free:
					tabMenu.TabPages.Remove(tpEncryption);
					tabMenu.TabPages.Remove(tpChangeLog);
					txtSettingEncryptionKey.Visible = false;
					lblEncryptionKey.Visible = false;
					chkSettingEnableEncryption.Visible = false;
					chkSettingEnableEncryption.Checked = false;
					break;
			}
		}

		private void btnOutputFolder_Click(object sender, EventArgs e)
		{
			Helper.SelectFolder(txtSettingOutputDataFilePath);
		}

		private void BtnSelectOutputConstantsFile_Click(object sender, EventArgs e)
		{
			Helper.SelectFolder(txtSettingOuputConstantsFilePath);
		}

		private void TxtOutputConstantsFilePath_TextChanged(object sender, EventArgs e)
		{
			SetupConfigFolders(txtSettingOuputConstantsFilePath, ref Config.Settings.constantsOutputFolder);
		}

		private void txtOutputDataFilePath_TextChanged(object sender, EventArgs e)
		{
			SetupConfigFolders(txtSettingOutputDataFilePath, ref Config.Settings.jsonOutputFolder);
		}

		private void txtSettingOutputLocalizationFilePath_TextChanged(object sender, EventArgs e)
		{
			SetupConfigFolders(txtSettingOutputLocalizationFilePath, ref Config.Settings.localizationOutputFolder);
		}

		private static bool IsJsonSheet(string pName)
		{
			return !pName.EndsWith(IDS_SHEET)
				&& !pName.EndsWith(CONSTANTS_SHEET)
				&& !pName.Contains(SETTINGS_SHEET)
				&& !pName.StartsWith(LOCALIZATION_SHEET);
		}

		private Dictionary<string, string> GenerateLangCharSets(Dictionary<string, string> pCharacterMaps)
		{
			var output = new Dictionary<string, string>();
			foreach (var map in pCharacterMaps)
			{
				string combinedStr = "";
				var unique = new HashSet<char>(map.Value);
				foreach (char c in unique)
					combinedStr += c;
				combinedStr = string.Concat(combinedStr.OrderBy(c => c));
				output.Add(map.Key, combinedStr);
			}
			return output;
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (sender is TabControl tabControl)
			{
				var tab = tabControl.SelectedTab;
				if (tab.Name == "tpExportMultiExcels")
				{
					RefreshDtgExcelFiles();
				}
			}
		}

		private void BtnAddFile_Click(object sender, EventArgs e)
		{
			var fileDialog = new OpenFileDialog();
			fileDialog.Filter = @"Excel Files|*.xls;*.xlsx;*.xlsm";
			fileDialog.FilterIndex = 2;
			fileDialog.RestoreDirectory = true;
			fileDialog.Multiselect = true;

			var result = fileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				foreach (string file in fileDialog.FileNames)
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
						Config.Settings.allFiles.Add(new ExcelPath(file));
				}

				RefreshDtgExcelFiles();
				Config.Save();
			}
		}

		private void BtnExportMultiExcels_Click(object sender, EventArgs e)
		{
			var bindingList = (BindingList<ExcelPath>)DtgFilePaths.DataSource;
			Config.Settings.allFiles = bindingList.ToList();
			Config.Save();
			
			m_idsBuilderDict = new Dictionary<string, StringBuilder>();
			m_constantsBuilderDict = new Dictionary<string, StringBuilder>();
			m_localizationsDict = new Dictionary<string, LocalizationBuilder>();
			txtLog2.Text = "";
			m_allIDsSorted = null;
			m_allIds = new Dictionary<string, int>();
			m_localizedSheetsExported = new List<string>();
			m_localizedLanguages = new List<string>();
			m_langCharSets = new Dictionary<string, string>();

			//Process IDs sheets first
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
					if (sheets[i].SheetName.EndsWith(IDS_SHEET))
						LoadSheetIDsValues(workBook, sheets[i].SheetName);
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
					if (sheets[i].SheetName.EndsWith(IDS_SHEET))
						if (BuildContentOfFileIDs(workBook, sheets[i].SheetName) && exportIDs && Config.Settings.separateIDs)
							CreateFileIDs(sheets[i].SheetName, m_idsBuilderDict[sheets[i].SheetName].ToString());
				}

				//Load and write json file
				var allJsons = new Dictionary<string, string>();
				for (int i = 0; i < sheets.Count; i++)
				{
					if (IsJsonSheet(sheets[i].SheetName))
					{
						string fileName = sheets[i].SheetName.Trim().Replace(" ", "_");
						string json = ConvertSheetToJson(workBook, sheets[i].SheetName, fileName, Config.Settings.encryptJson, !Config.Settings.combineJson);

						if (Config.Settings.combineJson)
						{
							if (allJsons.ContainsKey(fileName))
							{
								Log(LogType.Error, $"Could not create single Json file {fileName}, because key {fileName} is already exists!");
								continue;
							}
							allJsons.Add(fileName, json);
						}
					}
				}

				if (Config.Settings.combineJson)
				{
					//Build json file for all jsons content
					string mergedJson = JsonConvert.SerializeObject(allJsons);
					string mergedFileName = Path.GetFileNameWithoutExtension(file.path).Trim().Replace(" ", "_");
					Helper.WriteFile(Config.Settings.jsonOutputFolder, $"{mergedFileName}.txt", mergedJson);

					if (Config.Settings.encryptJson)
						Log(LogType.Message, $"Exported encrypted Json data to {mergedFileName}.txt.");
					else
						Log(LogType.Message, $"Exported Json data to {mergedFileName}.txt.");
				}

				//Load and write constants
				for (int i = 0; i < sheets.Count; i++)
				{
					if (sheets[i].SheetName.EndsWith(CONSTANTS_SHEET))
					{
						LoadSheetConstantsData(workBook, sheets[i].SheetName);

						if (m_constantsBuilderDict.ContainsKey(sheets[i].SheetName) && Config.Settings.separateConstants)
							CreateFileConstants(m_constantsBuilderDict[sheets[i].SheetName].ToString(), sheets[i].SheetName);
					}
				}

				//Load and write localizations
				for (int i = 0; i < sheets.Count; i++)
				{
					if (sheets[i].SheetName.StartsWith(LOCALIZATION_SHEET))
					{
						LoadSheetLocalizationData(workBook, sheets[i].SheetName);

						if (m_localizationsDict.ContainsKey(sheets[i].SheetName) && Config.Settings.separateLocalizations)
						{
							var builder = m_localizationsDict[sheets[i].SheetName];
							CreateLocalizationFileV2(builder.idsString, builder.languageTextDict, sheets[i].SheetName);
							m_localizedSheetsExported.Add(sheets[i].SheetName);
						}
					}
				}
			}

			//Create file contain all IDs
			if (!Config.Settings.separateIDs)
			{
				var builder = new StringBuilder();
				int count = 0;
				int length = m_idsBuilderDict.Count;
				foreach (var b in m_idsBuilderDict)
				{
					builder.Append(b.Value);
					if (count < length - 1)
						builder.AppendLine();
					count++;
				}
				CreateFileIDs("IDs", builder.ToString());
			}

			//Create file contain all Constants
			if (!Config.Settings.separateConstants)
			{
				var builder = new StringBuilder();
				int count = 0;
				int length = m_constantsBuilderDict.Count;
				foreach (var b in m_constantsBuilderDict)
				{
					builder.Append(b.Value);
					if (count < length - 1)
						builder.AppendLine();
					count++;
				}
				CreateFileConstants(builder.ToString(), "Constants");
			}

			//Create file contain all Localizations
			if (!Config.Settings.separateLocalizations)
			{
				var localizationBuilder = new LocalizationBuilder();
				foreach (var b in m_localizationsDict)
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
				CreateLocalizationFileV2(localizationBuilder.idsString, localizationBuilder.languageTextDict, "Localization");
				m_localizedSheetsExported.Add("Localization");
			}

			//Create language character sets
			if (m_langCharSets != null && m_langCharSets.Count > 0)
			{
				var maps = GenerateLangCharSets(m_langCharSets);
				foreach (var map in maps)
				{
					Helper.WriteFile(Config.Settings.localizationOutputFolder, $"characters_set_{map.Key}.txt", map.Value);
					Log(LogType.Message, $"Exported characters_set_{map.Key}.txt!");
				}
			}

			//Create localization manager file
			CreateLocalizationsManagerFile();
			
			Log(LogType.Message, "Done!");
		}

		private void DtgFilePaths_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e == null || e.RowIndex == DtgFilePaths.NewRowIndex || e.RowIndex < 0)
				return;

			var row = DtgFilePaths.Rows[e.RowIndex];

			if (e.ColumnIndex == DtgFilePaths.Columns["BtnDelete"].Index)
			{
				Config.Settings.allFiles.RemoveAt(e.RowIndex);

				RefreshDtgExcelFiles();
			}
		}

		private void DtgFilePaths_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e == null || e.RowIndex == DtgFilePaths.NewRowIndex || e.RowIndex < 0)
				return;

			if (e.ColumnIndex == DtgFilePaths.Columns["path"].Index)
			{
				var row = DtgFilePaths.Rows[e.RowIndex];
				ValidatePathRow(row);
			}
		}

		private void ValidatePathRow(DataGridViewRow row)
		{
			// Get the column indices from the column names
			int pathColumnIndex = DtgFilePaths.Columns["path"].Index;
			int statusColumnIndex = DtgFilePaths.Columns["status"].Index;

			// Get the path cell value
			var value = row.Cells[pathColumnIndex].Value;
			if (value == null)
				return;
			var path1 = value.ToString();

			bool fileExists = File.Exists(path1);
			var statusCell = row.Cells[statusColumnIndex];
			if (fileExists)
			{
				var image = Properties.Resources.ResourceManager.GetObject("check") as Image;
				statusCell.Value = image;
			}
			else
			{
				var image = Properties.Resources.ResourceManager.GetObject("cancel") as Image;
				statusCell.Value = image;
			}
		}

		private void chkSettingEnableEncryption_CheckedChanged(object sender, EventArgs e)
		{
			Config.Settings.encryptJson = chkSettingEnableEncryption.Checked;

			if (chkSettingEnableEncryption.Checked)
				CreateEncryption();

			Config.Save();
		}

		private void chkMergeJsonIntoSingleExcel2_CheckedChanged(object sender, EventArgs e)
		{
			Config.Settings.combineJson = chkMergeJsonIntoSingleOne2.Checked;
			Config.Save();
		}

		private void txtSettingEncryptionKey_TextChanged(object sender, EventArgs e) { }

		private void txtSettingEncryptionKey_Leave(object sender, EventArgs e)
		{
			CreateEncryption();
		}

		private void txtSettingExcludedSheet_TextChanged(object sender, EventArgs e) { }

		private void txtSettingExcludedSheet_Leave(object sender, EventArgs e)
		{
			if (Config.Settings.excludedSheets != txtSettingExcludedSheet.Text)
			{
				Config.Settings.excludedSheets = txtSettingExcludedSheet.Text.Trim();
				Config.Save();
			}
		}

		private void txtUnminimizeFields_TextChanged(object sender, EventArgs e) { }

		private void txtUnminimizeFields_Leave(object sender, EventArgs e)
		{
			if (Config.Settings.persistentFields != txtUnminimizeFields.Text)
			{
				Config.Settings.persistentFields = txtUnminimizeFields.Text.Trim();
				Config.Save();
			}
		}

		private void txtSettingNamespace_TextChanged(object sender, EventArgs e) { }

		private void txtSettingNamespace_Leave(object sender, EventArgs e)
		{
			if (Config.Settings.@namespace != txtSettingNamespace.Text)
			{
				Config.Settings.@namespace = txtSettingNamespace.Text.Trim();
				Config.Save();
			}
		}

		private void txtLanguageMaps_TextChanged(object sender, EventArgs e) { }

		private void txtLanguageMaps_Leave(object sender, EventArgs e)
		{
			if (Config.Settings.langCharSets != txtLanguageMaps.Text)
			{
				Config.Settings.langCharSets = txtLanguageMaps.Text.Trim();
				Config.Save();
			}
		}

		private void ChkSeparateIDs_CheckedChanged(object sender, EventArgs e)
		{
			Config.Settings.separateIDs = chkSeperateIDs.Checked;
			Config.Save();
		}

		private void ChkSeparateConstants_CheckedChanged(object sender, EventArgs e)
		{
			Config.Settings.separateConstants = chkSeperateConstants.Checked;
			Config.Save();
		}

		private void ChkSeparateLocalization_CheckedChanged(object sender, EventArgs e)
		{
			Config.Settings.separateLocalizations = chkSeperateLocalization.Checked;
			Config.Save();
		}

		private void Log(LogType pLogType, string pLog)
		{
			var sb = new StringBuilder();
			if (pLogType == LogType.Error)
				sb.Append("[ERROR] ").Append(pLog);
			else if (pLogType == LogType.Warning)
				sb.Append("[WARNING] ").Append(pLog);
			else
				sb.Append("[+] ").Append(pLog);

			if (tabMenu.SelectedTab.Name == "tpExportExcel")
				txtLog.Text += sb.AppendLine();
			else if (tabMenu.SelectedTab.Name == "tpExportMultiExcels")
				txtLog2.Text += sb.AppendLine();
			else if (tabMenu.SelectedTab.Name == "tpGoogleSpreadSheets")
				TxtLogExportingGoogleSheets.Text += sb.AppendLine();
		}

		#endregion

		//====================================================

		#region Single Excel

		private void InitExcel()
		{
			m_idsBuilderDict = new Dictionary<string, StringBuilder>();
			DtgIDs.DataSource = null;
			DtgIDs.Rows.Clear();

			m_sheets = new List<Sheet>();
			if (!string.IsNullOrEmpty(txtInputXLSXFilePath.Text.Trim()))
			{
				m_workBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);
				if (m_workBook != null)
				{
					for (int i = 0; i < m_workBook.NumberOfSheets; i++)
					{
						string sheetName = m_workBook.GetSheetName(i);
						m_sheets.Add(new Sheet(sheetName));
					}
					ExcludeSheets(ref m_sheets);
				}
			}
			DtgSheets.DataSource = new BindingList<Sheet>(m_sheets);
		}

		private void btnSelectInputFile_Click(object sender, EventArgs e)
		{
			var result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				txtInputXLSXFilePath.Text = fileName;
			}
		}

		private void BtnReloadGrid_Click(object sender, EventArgs e)
		{
			InitExcel();
		}

		[Obsolete]
		private void ExportSettings()
		{
			if (m_workBook == null)
				m_workBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);

			for (int i = 0; i < m_sheets.Count; i++)
			{
				if (m_sheets[i].SheetName.Contains(SETTINGS_SHEET) && m_sheets[i].Check)
				{
					string content = ExportSettingsSheetToScriptableObject(m_workBook, m_sheets[i].SheetName);
					if (!string.IsNullOrEmpty(content))
					{
						Helper.WriteFile(Config.Settings.jsonOutputFolder, $"{m_sheets[i].SheetName}.txt", content);
						Log(LogType.Message, $"Exported {m_sheets[i].SheetName}.txt");
					}
				}
			}
		}

		/// <summary>
		/// Export single excel file
		/// </summary>
		private void BtnExportIds_Click(object sender, EventArgs e)
		{
			if (m_workBook == null)
				m_workBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);
			if (m_workBook == null)
				return;

			m_idsBuilderDict = new Dictionary<string, StringBuilder>();
			m_allIds = new Dictionary<string, int>();

			foreach (var m in m_sheets)
			{
				if (m.SheetName.EndsWith(IDS_SHEET) && m.Check)
				{
					//Load All IDs
					BuildContentOfFileIDs(m_workBook, m.SheetName);

					//Create IDs Files
					if (Config.Settings.separateConstants)
					{
						var content = m_idsBuilderDict[m.SheetName].ToString();
						CreateFileIDs(m.SheetName, content);
					}
				}
			}

			if (!Config.Settings.separateConstants)
			{
				var iDsBuilder = new StringBuilder();
				foreach (var builder in m_idsBuilderDict)
				{
					var content = builder.Value.ToString();
					iDsBuilder.Append(content);
					iDsBuilder.AppendLine();
				}
				CreateFileIDs("IDs", iDsBuilder.ToString());

			}

			RefreshDtgIDs();
		}

		private void BtnExportConstants_Click(object sender, EventArgs e)
		{
			if (m_workBook == null)
				m_workBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);
			if (m_workBook == null)
				return;

			m_constantsBuilderDict = new Dictionary<string, StringBuilder>();

			for (int i = 0; i < m_sheets.Count; i++)
			{
				if (m_sheets[i].SheetName.EndsWith(CONSTANTS_SHEET) && m_sheets[i].Check)
				{
					LoadSheetConstantsData(m_workBook, m_sheets[i].SheetName);

					if (m_constantsBuilderDict.ContainsKey(m_sheets[i].SheetName) && Config.Settings.separateConstants)
					{
						CreateFileConstants(m_constantsBuilderDict[m_sheets[i].SheetName].ToString(), m_sheets[i].SheetName);
					}
				}
			}

			if (!Config.Settings.separateConstants)
			{
				var builder = new StringBuilder();
				foreach (var b in m_constantsBuilderDict)
				{
					builder.Append(b.Value);
					builder.AppendLine();
				}
				CreateFileConstants(builder.ToString(), "Constants");
			}
		}

		private void btnExportLocalization_Click(object sender, EventArgs e)
		{
			if (m_workBook == null)
				m_workBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);
			if (m_workBook == null)
				return;

			if (m_allIds == null || m_allIds.Count == 0)
			{
				m_allIds = new Dictionary<string, int>();
				foreach (var sheet in m_sheets)
					if (sheet.SheetName.EndsWith(IDS_SHEET))
						LoadSheetIDsValues(m_workBook, sheet.SheetName);
			}

			m_localizationsDict = new Dictionary<string, LocalizationBuilder>();
			m_localizedSheetsExported = new List<string>();
			m_localizedLanguages = new List<string>();
			m_langCharSets = new Dictionary<string, string>();

			for (int i = 0; i < m_sheets.Count; i++)
			{
				if (m_sheets[i].Check && m_sheets[i].SheetName.StartsWith(LOCALIZATION_SHEET))
				{
					LoadSheetLocalizationData(m_workBook, m_sheets[i].SheetName);

					if (m_localizationsDict.ContainsKey(m_sheets[i].SheetName) && Config.Settings.separateLocalizations)
					{
						var builder = m_localizationsDict[m_sheets[i].SheetName];
						//CreateLocalizationFile(builder.idsString, builder.languageTextDict, mSheets[i].SheetName);
						CreateLocalizationFileV2(builder.idsString, builder.languageTextDict, m_sheets[i].SheetName);
						m_localizedSheetsExported.Add(m_sheets[i].SheetName);
					}
				}
			}

			if (!Config.Settings.separateLocalizations)
			{
				var builder = new LocalizationBuilder();
				foreach (var b in m_localizationsDict)
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
				m_localizedSheetsExported.Add("Localization");
			}

			//Create language character sets
			if (m_langCharSets != null && m_langCharSets.Count > 0)
			{
				var maps = GenerateLangCharSets(m_langCharSets);
				foreach (var map in maps)
				{
					Helper.WriteFile(Config.Settings.localizationOutputFolder, $"characters_set_{map.Key}.txt", map.Value);
					Log(LogType.Message, $"Exported characters_set_{map.Key}.txt!");
				}
			}

			//Create localization manager file
			CreateLocalizationsManagerFile();
		}

		/// <summary>
		/// Export single excel file
		/// </summary>
		private void BtnExportJson_Click(object sender, EventArgs e)
		{
			if (m_workBook == null)
				m_workBook = Helper.LoadWorkBook(txtInputXLSXFilePath.Text);
			if (m_workBook == null)
				return;

			if (m_allIds == null || m_allIds.Count == 0)
			{
				m_allIds = new Dictionary<string, int>();
				foreach (var sheet in m_sheets)
					if (sheet.SheetName.EndsWith(IDS_SHEET))
						LoadSheetIDsValues(m_workBook, sheet.SheetName);
			}
			
			var allSheets = new List<string>();
			bool writeJsonFileForSingleSheet = !Config.Settings.combineJson;
			var allJsons = new Dictionary<string, string>();
			foreach (var sheet in m_sheets)
			{
				if (sheet.Check && IsJsonSheet(sheet.SheetName))
				{
					string fileName = sheet.SheetName.Trim().Replace(" ", "_");
					string json = ConvertSheetToJson(m_workBook, sheet.SheetName, fileName, Config.Settings.encryptJson, writeJsonFileForSingleSheet);

					//Merge all json into a single file
					if (Config.Settings.combineJson)
					{
						if (allJsons.ContainsKey(fileName))
						{
							Log(LogType.Error, $"Could not create single json file {fileName}, because file {fileName} is already exists!");
							continue;
						}
						allJsons.Add(fileName, json);
					}

					allSheets.Add(sheet.SheetName);
				}
			}
			if (Config.Settings.combineJson)
			{
				//Build json file for all jsons content
				string mergedJson = JsonConvert.SerializeObject(allJsons);
				string mergedFileName = Path.GetFileNameWithoutExtension(Config.Settings.inputDataFilePath).Trim().Replace(" ", "_");
				Helper.WriteFile(Config.Settings.jsonOutputFolder, $"{mergedFileName}.txt", mergedJson);

				if (Config.Settings.encryptJson)
					Log(LogType.Message, $"Exported encrypted Json data to {mergedFileName}.txt.");
				else
					Log(LogType.Message, $"Exported Json data to {mergedFileName}.txt.");
			}
		}

		private void BtnExportAll_Click(object sender, EventArgs e)
		{
			txtLog.Text = "";
			BtnExportIds_Click(null, null);
			BtnExportConstants_Click(null, null);
			btnExportLocalization_Click(null, null);
			BtnExportJson_Click(null, null);
		}

		private void RefreshDtgIDs()
		{
			var ids = new List<ID>();
			foreach (var pair in m_allIds)
				ids.Add(new ID(pair.Key, pair.Value));
			DtgIDs.DataSource = new BindingList<ID>(ids);
		}

		private void TxtInputFilePath_TextChanged(object sender, EventArgs e)
		{
			if (File.Exists(txtInputXLSXFilePath.Text.Trim()))
			{
				SetupConfigFolders(txtInputXLSXFilePath, ref Config.Settings.inputDataFilePath);
				InitExcel();
			}
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
		}

#endregion

		private void BtnDecrypt_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtEncryptionInput.Text))
				return;
			CreateEncryption();
			string text = txtEncryptionInput.Text;
			try
			{
				string decryptedCode = m_encryption.DecryptValue(text);
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
			try
			{
				if (m_encryption == null)
					CreateEncryption();
				string encryptedCode = m_encryption.EncryptValue(text);
				txtEncryptionOutput.Text = encryptedCode;
			}
			catch (Exception ex)
			{
				txtEncryptionOutput.Text = ex.ToString();
			}
		}

		private void BtnOpenFolder1_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtSettingOutputDataFilePath.Text))
				return;

			if (Directory.Exists(txtSettingOutputDataFilePath.Text))
				Process.Start(txtSettingOutputDataFilePath.Text);
			else
				MessageBox.Show(@"Folder not exists!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void BtnOpenFolder2_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtSettingOuputConstantsFilePath.Text))
				return;

			if (Directory.Exists(txtSettingOuputConstantsFilePath.Text))
				Process.Start(txtSettingOuputConstantsFilePath.Text);
			else
				MessageBox.Show(@"Folder not exists!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void BtnOpenFolderLocalization_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtSettingOutputLocalizationFilePath.Text))
				return;

			if (Directory.Exists(txtSettingOutputLocalizationFilePath.Text))
				Process.Start(txtSettingOutputLocalizationFilePath.Text);
			else
				MessageBox.Show(@"Folder not exists!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void BtnSelectFolderLocalization_Click(object sender, EventArgs e)
		{
			Helper.SelectFolder(txtSettingOutputLocalizationFilePath);
		}

		private void LoadSettings()
		{
			var settings = Config.Settings;
			if (!string.IsNullOrEmpty(settings.inputDataFilePath))
			{
				if (!File.Exists(settings.inputDataFilePath))
				{
					txtInputXLSXFilePath.Text = "";
					settings.inputDataFilePath = "";
				}
				else
					txtInputXLSXFilePath.Text = settings.inputDataFilePath;
			}
			else
				txtInputXLSXFilePath.Text = "";
			txtSettingOutputDataFilePath.Text = settings.jsonOutputFolder;
			txtSettingOutputLocalizationFilePath.Text = settings.localizationOutputFolder;
			txtSettingOuputConstantsFilePath.Text = settings.constantsOutputFolder;
			chkSettingEnableEncryption.Checked = settings.encryptJson;
			if (!string.IsNullOrEmpty(settings.encryptionKey))
				txtSettingEncryptionKey.Text = settings.encryptionKey;
			txtSettingExcludedSheet.Text = settings.excludedSheets;
			txtSettingNamespace.Text = settings.@namespace;
			chkMergeJsonIntoSingleOne2.Checked = settings.combineJson;
			txtUnminimizeFields.Text = settings.persistentFields;
			txtLanguageMaps.Text = settings.langCharSets;
			chkSeperateConstants.Checked = settings.separateConstants;
			chkSeperateIDs.Checked = settings.separateIDs;
			chkSeperateLocalization.Checked = settings.separateLocalizations;
			chkKeepOnlyEnumAsIds.Checked = settings.onlyEnumAsIDs;
			TxtGoogleClientID.Text = settings.googleClientId;
			TxtGoogleClientSecret.Text = settings.googleClientSecret;

			if (settings.encryptJson)
				CreateEncryption();

			RefreshDtgGoogleSheetsPaths();
		}

		private void BtnLoadDefaultSettings_Click(object sender, EventArgs e)
		{
			Config.ClearSettings();
			var settings = Config.Settings;

			txtSettingOutputDataFilePath.Text = settings.jsonOutputFolder;
			txtSettingOuputConstantsFilePath.Text = settings.constantsOutputFolder;
			txtSettingNamespace.Text = settings.@namespace;
			chkSeperateIDs.Checked = settings.separateIDs;
			chkSeperateConstants.Checked = settings.separateConstants;
			chkSeperateLocalization.Checked = settings.separateLocalizations;
			chkMergeJsonIntoSingleOne2.Checked = settings.combineJson;
			chkKeepOnlyEnumAsIds.Checked = settings.onlyEnumAsIDs;
			txtSettingEncryptionKey.Text = settings.encryptionKey;
			txtSettingExcludedSheet.Text = settings.excludedSheets;
			txtUnminimizeFields.Text = settings.persistentFields;
			txtLanguageMaps.Text = settings.langCharSets;

			RefreshDtgGoogleSheetsPaths();
		}

		private void chkKeepOnlyEnumAsIds_CheckedChanged(object sender, EventArgs e)
		{
			if (Config.Settings.onlyEnumAsIDs == chkKeepOnlyEnumAsIds.Checked)
				return;
			Config.Settings.onlyEnumAsIDs = chkKeepOnlyEnumAsIds.Checked;
			Config.Save();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/hnb-rabear/excel-to-unity/blob/main/Example.xlsx");
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
				InitExcels();
			}
		}

		private void DtgFilePaths_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

		#region Google Sheets

		private void BtnAddGoogleSheet_Click(object sender, EventArgs e)
		{
			var form = new FrmGoogleSheetSample();
			form.ShowDialog();

			if (!string.IsNullOrEmpty(form.googleSheetId) && !string.IsNullOrEmpty(form.googleSheetName))
			{
				Config.Settings.SetGoogleSheet(form.googleSheetId, form.googleSheetName, form.sheets);
				Config.Save();

				RefreshDtgGoogleSheetsPaths();
			}
		}

		private BindingList<GoogleSheetsPath> m_bindingGoogleSheetsPaths;

		private void DtgGoogleSheets_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e == null || e.RowIndex == DtgGoogleSheets.NewRowIndex || e.RowIndex < 0)
				return;

			var row = DtgGoogleSheets.Rows[e.RowIndex];
			int idColumnIdx = DtgGoogleSheets.Columns["GoogleSheetId"].Index;
			int nameColumnIdx = DtgGoogleSheets.Columns["GoogleSheetName"].Index;

			if (e.ColumnIndex == DtgGoogleSheets.Columns["BtnDeleteGoogleSheet"].Index)
			{
				string googleSheetId = row.Cells[idColumnIdx].Value?.ToString();

				Config.Settings.RemoveGoogleSheet(googleSheetId);
				Config.Save();

			}
			else if (e.ColumnIndex == DtgGoogleSheets.Columns["BtnEditGoogleSheet"].Index)
			{
				var form = new FrmGoogleSheetSample();
				form.googleSheetId = row.Cells[idColumnIdx].Value?.ToString();
				form.googleSheetName = row.Cells[nameColumnIdx].Value?.ToString();
				form.ShowDialog();

				if (!string.IsNullOrEmpty(form.googleSheetId) && !string.IsNullOrEmpty(form.googleSheetName))
				{
					Config.Settings.SetGoogleSheet(form.googleSheetId, form.googleSheetName, form.sheets);
					Config.Save();

					RefreshDtgGoogleSheetsPaths();
				}
			}
		}

		private void RefreshDtgGoogleSheetsPaths()
		{
			m_bindingGoogleSheetsPaths = new BindingList<GoogleSheetsPath>(Config.Settings.googleSheetsPaths);
			DtgGoogleSheets.DataSource = m_bindingGoogleSheetsPaths;
		}

		public List<GoogleSheetsPath> GetGoogleSheetsPathsFromGridView()
		{
			var dataList = new List<GoogleSheetsPath>();

			// Get column indices by their names
			int nameColumnIdx = DtgGoogleSheets.Columns["GoogleSheetName"].Index;
			int keyColumnIdx = DtgGoogleSheets.Columns["GoogleSheetId"].Index;

			// Iterate through each row in the DataGridView
			var list = DtgGoogleSheets.Rows;
			for (int i = 0; i < list.Count; i++)
			{
				var row = list[i];
				// Only process rows that are not new rows (the empty row at the end of DataGridView)
				if (!row.IsNewRow)
				{
					var data = new GoogleSheetsPath()
					{
						name = row.Cells[nameColumnIdx].Value?.ToString(),
						id = row.Cells[keyColumnIdx].Value?.ToString(),
					};

					dataList.Add(data);
				}
			}

			return dataList;
		}

		private void BtnExportGoogleSheets_Click(object sender, EventArgs e)
		{
			// Create Google Sheets API service.
			var service = new SheetsService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = Helper.AuthenticateGoogleStore(),
				ApplicationName = APPLICATION_NAME,
			});

			m_idsBuilderDict = new Dictionary<string, StringBuilder>();
			m_constantsBuilderDict = new Dictionary<string, StringBuilder>();
			m_localizationsDict = new Dictionary<string, LocalizationBuilder>();
			
			TxtLogExportingGoogleSheets.Text = "";
			m_allIDsSorted = null;
			m_allIds = new Dictionary<string, int>();

			m_localizedSheetsExported = new List<string>();
			m_localizedLanguages = new List<string>();
			m_langCharSets = new Dictionary<string, string>();

			var settings = Config.Settings;
			var googleSheetsPaths = Config.Settings.googleSheetsPaths;

			//Load and write Ids first
			foreach (var googleSheets in googleSheetsPaths)
			{
				// Get the sheet metadata to determine its dimensions
				var sheetMetadata = service.Spreadsheets.Get(googleSheets.id).Execute();
				foreach (var sheet in googleSheets.sheets)
				{
					if (!sheet.selected || !sheet.name.EndsWith(IDS_SHEET))
						continue;

					var sheetInfo = sheetMetadata.Sheets.FirstOrDefault(s => s.Properties.Title == sheet.name);
					if (sheetInfo == null)
						continue;

					var columnCount = sheetInfo.Properties.GridProperties.ColumnCount;

					// Construct the range dynamically based on row and column counts
					var range = $"{sheet.name}!A1:{Helper.GetColumnLetter(columnCount.Value)}";

					// Create a request to get the sheet data
					var request = service.Spreadsheets.Values.Get(googleSheets.id, range);
					var response = request.Execute();
					var values = response.Values;

					// Build contents of file IDs and export to file if seperateIDs = true
					if (BuildContentOfFileIDs(sheet.name, values) && Config.Settings.separateIDs)
						CreateFileIDs(sheet.name, m_idsBuilderDict[sheet.name].ToString());
				}
			}

			// Read and write other data type
			foreach (var googleSheets in googleSheetsPaths)
			{
				var sheets = new List<GoogleSheetsPath.Sheet>();
				var excludedSheets = settings.GetExcludedSheets();
				foreach (var sheet in googleSheets.sheets)
				{
					if (sheet.selected && (excludedSheets == null || !excludedSheets.Contains(sheet.name)))
						sheets.Add(sheet);
				}

				// Get the sheet metadata to determine its dimensions
				var ggSheetsMetadata = service.Spreadsheets.Get(googleSheets.id).Execute();
				var allJsons = new Dictionary<string, string>();
				foreach (var sheet in sheets)
				{
					var sheetInfo = ggSheetsMetadata.Sheets.FirstOrDefault(s => s.Properties.Title == sheet.name);
					if (sheetInfo == null)
						continue;

					var columnCount = sheetInfo.Properties.GridProperties.ColumnCount;

					// Construct the range dynamically based on row and column counts
					var range = $"{sheet.name}!A1:{Helper.GetColumnLetter(columnCount.Value)}";
					if (sheet.name.EndsWith(CONSTANTS_SHEET))
						range = $"{sheet.name}!A1:D";

					// Create a request to get the sheet data
					var request = service.Spreadsheets.Values.Get(googleSheets.id, range);
					var response = request.Execute();
					var values = response.Values;
					
					//Load and write json file
					if (IsJsonSheet(sheet.name))
					{
						string fileName = sheet.name.Trim().Replace(" ", "_");
						string json = ConvertSheetToJson(values, sheet.name, fileName, Config.Settings.encryptJson, !Config.Settings.combineJson);
						if (Config.Settings.combineJson)
						{
							if (allJsons.ContainsKey(fileName))
							{
								Log(LogType.Error, $"Can not merge sheet {fileName}, because key {fileName} is already exists!");
								continue;
							}
							allJsons.Add(fileName, json);
						}
					}

					if (Config.Settings.combineJson)
					{
						//Build json file for all jsons content
						string mergedJson = JsonConvert.SerializeObject(allJsons);
						string mergedFileName = ggSheetsMetadata.Properties.Title;
						Helper.WriteFile(Config.Settings.jsonOutputFolder, $"{mergedFileName}.txt", mergedJson);

						if (Config.Settings.encryptJson)
							Log(LogType.Message, $"Exported encrypted Json data to {mergedFileName}.txt.");
						else
							Log(LogType.Message, $"Exported Json data to {mergedFileName}.txt.");
					}
					
					//Load and write constants
					if (sheet.name.EndsWith(CONSTANTS_SHEET))
					{
						LoadSheetConstantsData(sheet.name, values);
						
						if (m_constantsBuilderDict.ContainsKey(sheet.name) && Config.Settings.separateConstants)
							CreateFileConstants(m_constantsBuilderDict[sheet.name].ToString(), sheet.name);
					}
					
					//Load and write localizations
					if (sheet.name.StartsWith(LOCALIZATION_SHEET))
					{
						LoadSheetLocalizationData(values, sheet.name);
						
						if (m_localizationsDict.ContainsKey(sheet.name) && Config.Settings.separateLocalizations)
						{
							var builder = m_localizationsDict[sheet.name];
							CreateLocalizationFileV2(builder.idsString, builder.languageTextDict, sheet.name);
							m_localizedSheetsExported.Add(sheet.name);
						}
					}
				}
			}

			//Create file contain all IDs
			if (!Config.Settings.separateIDs)
			{
				var builder = new StringBuilder();
				int count = 0;
				int length = m_idsBuilderDict.Count;
				foreach (var b in m_idsBuilderDict)
				{
					builder.Append(b.Value);
					if (count < length - 1)
						builder.AppendLine();
					count++;
				}
				CreateFileIDs("IDs", builder.ToString());
			}
			
			//Create file contain all Constants
			if (!Config.Settings.separateConstants)
			{
				var builder = new StringBuilder();
				int count = 0;
				int length = m_constantsBuilderDict.Count;
				foreach (var b in m_constantsBuilderDict)
				{
					builder.Append(b.Value);
					if (count < length - 1)
						builder.AppendLine();
					count++;
				}
				CreateFileConstants(builder.ToString(), "Constants");
			}
			
			//Create file contain all Localizations
			if (!Config.Settings.separateLocalizations)
			{
				var localizationBuilder = new LocalizationBuilder();
				foreach (var b in m_localizationsDict)
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
				CreateLocalizationFileV2(localizationBuilder.idsString, localizationBuilder.languageTextDict, "Localization");
				m_localizedSheetsExported.Add("Localization");
			}
			
			//Create characters sets
			if (m_langCharSets != null && m_langCharSets.Count > 0)
			{
				var maps = GenerateLangCharSets(m_langCharSets);
				foreach (var map in maps)
				{
					Helper.WriteFile(Config.Settings.localizationOutputFolder, $"characters_set_{map.Key}.txt", map.Value);
					Log(LogType.Message, $"Exported characters_set_{map.Key}.txt");
				}
			}
			
			//Create localization manager file
			CreateLocalizationsManagerFile();
			
			Log(LogType.Message, "Done!");
		}

		private Dictionary<string, int> LoadSheetIDsValue(string sheetName, IList<IList<object>> values)
		{
			if (values == null || values.Count <= 0)
			{
				Console.WriteLine($"No data found in sheet {sheetName}.");
				return null;
			}

			var result = new Dictionary<string, int>();
			for (int row = 0; row < values.Count; row++)
			{
				var rowValues = values[row];
				for (int col = 0; col < rowValues.Count; col += 3)
				{
					var cellIdName = rowValues[col].ToString();
					if (string.IsNullOrEmpty(cellIdName))
						continue;
					if (row <= 0)
						continue;

					var cellIdValue = rowValues[col + 1].ToString();
					if (string.IsNullOrEmpty(cellIdValue))
						continue;
					string key = cellIdName;
					int value = int.Parse(cellIdValue.Trim());

					result[key] = value;
				}
			}

			return result;
		}

		private void LoadSheetConstantsData(string sheetName, IList<IList<object>> rowsData)
		{
			if (rowsData == null || rowsData.Count <= 0)
			{
				Console.WriteLine($"No data found in sheet {sheetName}.");
				return;
			}

			var constants = new List<ConstantBuilder>();
			for (int row = 0; row < rowsData.Count; row++)
			{
				var newConst = new ConstantBuilder();
				var rowValues = rowsData[row];

				if (rowValues.Count < 1)
					continue;
				newConst.name = rowValues[0].ToString().Trim();
				if (rowValues.Count < 2)
					continue;
				newConst.valueType = rowValues[1].ToString().Trim();
				if (rowValues.Count < 3)
					continue;
				newConst.value = rowValues[2].ToString().Trim();
				if (rowValues.Count >= 4)
					newConst.comment = rowValues[3].ToString().Trim();

				if (string.IsNullOrEmpty(newConst.name)
				    || string.IsNullOrEmpty(newConst.valueType)
				    || string.IsNullOrEmpty(newConst.value))
					continue;

				constants.Add(newConst);
			}
			constants.Sort();
			BuildContentOfFileConstants(constants, sheetName);
		}

		private bool BuildContentOfFileIDs(string pSheetName, IList<IList<object>> rowsData)
		{
			if (rowsData == null || rowsData.Count <= 0)
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty");
				return false;
			}

			var idsBuilders = new List<StringBuilder>();
			var idsEnumBuilders = new List<StringBuilder>();
			var idsEnumBuilderNames = new List<string>();
			var idsEnumBuilderIndexes = new List<int>();
			for (int row = 0; row < rowsData.Count; row++)
			{
				var rowData = rowsData[row];
				for (int col = 0; col < rowData.Count; col += 3)
				{
					var key = rowData[col].ToString().Trim();
					if (string.IsNullOrEmpty(key))
						continue;
					int index = col / 3;
					var sb = index < idsBuilders.Count ? idsBuilders[index] : new StringBuilder();
					if (!idsBuilders.Contains(sb))
					{
						idsBuilders.Add(sb);
					}
					//Values row
					if (row > 0)
					{
						if (string.IsNullOrEmpty(key))
							continue;

						//Value
						var valueStr = rowData[col + 1].ToString().Trim();
						if (string.IsNullOrEmpty(valueStr))
						{
							MessageBox.Show($@"Sheet {pSheetName}: Key {key} doesn't have value!", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							continue;
						}
						
						int.TryParse(valueStr, out int value);
						sb.Append("\tpublic const int ");
						sb.Append(key);
						sb.Append(" = ");
						sb.Append(value);
						sb.Append(";");

						//Comment
						if (col + 2 < rowData.Count)
						{
							var cellComment = rowData[col + 2].ToString();
							if (!string.IsNullOrEmpty(cellComment.Trim()))
								sb.Append(" /*").Append(cellComment).Append("*/");
						}

						if (m_allIds.ContainsKey(key))
							MessageBox.Show($@"ID {key} is duplicated in sheet {pSheetName}", @"Duplicated ID!", MessageBoxButtons.OK, MessageBoxIcon.Error);
						m_allIds.AddOrUpdate(key, value);
					}
					//Header row
					else
					{
						if (key.Contains("[enum]"))
						{
							idsEnumBuilders.Add(sb);
							idsEnumBuilderNames.Add(key.Replace("[enum]", ""));
							idsEnumBuilderIndexes.Add(index);
						}

						sb.Append("\t#region ")
							.Append(key);
					}
					sb.Append(Environment.NewLine);
				}
			}

			m_allIds = m_allIds.OrderBy(m => m.Key).ToDictionary(x => x.Key, x => x.Value);

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
					var enumIndex = str.IndexOf("[enum]", StringComparison.Ordinal);
					str = str.Remove(0, enumIndex + 6).Replace(",", ", ");

					string enumName = idsEnumBuilderNames[i].Replace(" ", "_");

					var enumBuilder = new StringBuilder();
					enumBuilder.Append("\tpublic enum ")
						.Append(enumName)
						.Append(" { ")
						.Append(str)
						.Append(" }\n");
					if (Config.Settings.onlyEnumAsIDs)
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

			if (m_idsBuilderDict.ContainsKey(pSheetName))
			{
				m_idsBuilderDict[pSheetName].AppendLine();
				m_idsBuilderDict[pSheetName].Append(builder);
			}
			else
				m_idsBuilderDict.Add(pSheetName, builder);

			return true;
		}

		private string ConvertSheetToJson(IList<IList<object>> pValues, string pSheetName, string pFileName, bool pEncrypt, bool pWriteFile)
		{
			var fieldValueTypes = Helper.GetFieldValueTypes(pValues);
			if (fieldValueTypes == null)
				return "{}";
			return ConvertSheetToJson(pValues, pSheetName, pFileName, fieldValueTypes, pEncrypt, pWriteFile);
		}
		
		private string ConvertSheetToJson(IList<IList<object>> pValues, string pSheetName, string pOutputFile, List<FieldValueType> pFieldValueTypes, bool pEncrypt, bool pAutoWriteFile)
		{
			var unminifiedFields = Config.Settings.GetPersistentFields();

			if (pValues == null || pValues.Count == 0)
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty!");
				return null;
			}

			int lastCellNum = 0;
			string[] fields = null;
			bool[] validCols = null;
			var rowContents = new List<RowContent>();

			for (int row = 0; row < pValues.Count; row++)
			{
				var rowValues = pValues[row];
				if (rowValues == null || rowValues.Count == 0)
					continue;

				if (row == 0)
				{
					lastCellNum = rowValues.Count;
					fields = new string[lastCellNum];
					validCols = new bool[lastCellNum];

					for (int col = 0; col < lastCellNum; col++)
					{
						var cellValue = rowValues[col].ToString().Trim();
						if (!string.IsNullOrEmpty(cellValue) && !cellValue.Contains("[x]"))
						{
							validCols[col] = true;
							fields[col] = cellValue.Trim();
						}
						else
						{
							validCols[col] = false;
							fields[col] = "";
						}
					}
				}
				else
				{
					var rowContent = new RowContent();
					for (int col = 0; col < lastCellNum; col++)
					{
						var cellValue = "";
						if (col < rowValues.Count)
							cellValue = rowValues[col].ToString().Trim();
						if (fields != null)
						{
							string fieldName = fields[col];
							string fieldValue = cellValue;
							fieldName = fieldName.Replace(" ", "_");
							rowContent.fieldNames.Add(fieldName);
							rowContent.fieldValues.Add(fieldValue);
						}
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
				bool rowIsEmpty = true; //Because Loading sheet sometime includes the empty rows, I don't know why it happen
				var nestedObjects = new List<JObject>();

				for (int j = 0; j < rowContent.fieldNames.Count; j++)
				{
					bool valid = validCols[j];
					if (!valid)
						continue;
					string fieldName = rowContent.fieldNames[j];
					string fieldValue = rowContent.fieldValues[j];
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
						var att = new Att();
						att.id = GetReferenceId(fieldValue, out bool found);
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
										att.unlock = GetReferenceId(fieldValue, out found);
								}
								else
								{
									string[] inValues = Helper.SplitValueToArray(fieldValue, false);
									float[] outValues = new float[inValues.Length];
									for (int t = 0; t < inValues.Length; t++)
									{
										if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
											outValues[t] = GetReferenceId(inValues[t].Trim(), out found);
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
										att.increase = GetReferenceId(fieldValue, out found);
								}
								else
								{
									string[] inValues = Helper.SplitValueToArray(fieldValue, false);
									float[] outValues = new float[inValues.Length];
									for (int t = 0; t < inValues.Length; t++)
									{
										if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
											outValues[t] = GetReferenceId(inValues[t].Trim(), out found);
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
										att.value = GetReferenceId(fieldValue, out found);
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
											outValues[t] = GetReferenceId(inValues[t].Trim(), out found);
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
										att.max = GetReferenceId(fieldValue, out found);
								}
								else
								{
									string[] inValues = Helper.SplitValueToArray(fieldValue, false);
									float[] outValues = new float[inValues.Length];
									for (int t = 0; t < inValues.Length; t++)
									{
										if (!float.TryParse(inValues[t].Trim(), out outValues[t]))
											outValues[t] = GetReferenceId(inValues[t].Trim(), out found);
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
						bool importantField = unminifiedFields.Contains(fieldName.Replace("[]", "").ToLower());

						//Ignore empty field or field have value which equal 0
						if (string.IsNullOrEmpty(fieldValue) || fieldValue == "0" && !importantField)
							continue;

						bool nestedFiled = fieldName.Contains(".");
						foreach (var field in pFieldValueTypes)
						{
							//Find referenced Id in string and convert it to number
							if (field.name == fieldName)
							{
								string fieldType = field.type;
								bool referencedId = false;
								if (fieldType == "string") //Find and replace string value with referenced ID
								{
									if (CheckExistedId(fieldValue))
									{
										fieldType = "number";
										referencedId = true;
									}
									else if (int.TryParse(fieldValue, out int _))
									{
										fieldType = "number";
										referencedId = true;
									}
								}
								if (fieldType == "array-string") //Find and replace string value with referenced ID
								{
									string[] arrayValue = Helper.SplitValueToArray(fieldValue, false);
									foreach (string val in arrayValue)
									{
										if (CheckExistedId(val.Trim()))
										{
											fieldType = "array-number";
											referencedId = true;
											break;
										}
									}
								}

								var jsonObject = new JObject();
								switch (fieldType)
								{
									case "number":
										if (referencedId)
										{
											int intValue = GetReferenceId(fieldValue, out bool _);
											if (!nestedFiled)
												fieldContentStr += $"\"{fieldName}\":{intValue},";
											jsonObject[fieldName] = intValue;
										}
										else
										{
											if (!nestedFiled)
												fieldContentStr += $"\"{fieldName}\":{fieldValue},";
											jsonObject[fieldName] = fieldValue;
										}
										break;

									case "string":
										fieldValue = fieldValue.Replace("\n", "\\n");
										fieldValue = fieldValue.Replace("\"", "\\\"");
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":\"{fieldValue}\",";
										else
											jsonObject[fieldName] = fieldValue;
										break;

									case "bool":
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{fieldValue.ToLower()},";
										else
											jsonObject[fieldName] = fieldValue;
										break;

									case "array-number":
									{
										fieldName = fieldName.Replace("[]", "");
										var arrayValue = Helper.SplitValueToArray(fieldValue, false);
										var arrayStr = "[";
										for (int k = 0; k < arrayValue.Length; k++)
										{
											string val = arrayValue[k].Trim();
											if (referencedId)
												val = GetReferenceId(val, out bool _).ToString();
											if (k == 0) arrayStr += val;
											else arrayStr += "," + val;
										}
										arrayStr += "]";
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{arrayStr},";
										else
										{
											int[] array = JsonConvert.DeserializeObject<int[]>(arrayStr);
											jsonObject[fieldName] = JArray.FromObject(array);
										}
									}
										break;

									case "array-string":
									{
										fieldName = fieldName.Replace("[]", "");
										var arrayValue = Helper.SplitValueToArray(fieldValue, false);
										var arrayStr = "[";
										for (int k = 0; k < arrayValue.Length; k++)
										{
											if (k == 0) arrayStr += $"\"{arrayValue[k].Trim()}\"";
											else arrayStr += $",\"{arrayValue[k].Trim()}\"";
										}
										arrayStr += "]";
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{arrayStr},";
										else
										{
											string[] array = JsonConvert.DeserializeObject<string[]>(arrayStr);
											jsonObject[fieldName] = JArray.FromObject(array);
										}
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
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{arrayStr},";
										else
										{
											bool[] array = JsonConvert.DeserializeObject<bool[]>(arrayStr);
											jsonObject[fieldName] = JArray.FromObject(array);
										}
									}
										break;

									case "json":
									{
										fieldName = fieldName.Replace("{}", "");

										//Search Id in field value
										if (m_allIDsSorted == null || m_allIDsSorted.Count == 0)
										{
											m_allIDsSorted = Helper.SortIDsByLength(m_allIds);
										}
										foreach (var id in m_allIDsSorted)
										{
											if (fieldValue.Contains(id.Key))
												fieldValue = fieldValue.Replace(id.Key, id.Value.ToString());
										}
										if (!Helper.IsValidJson(fieldValue))
										{
											MessageBox.Show($@"Invalid Json string at Sheet: {pSheetName} Field: {fieldName} Row: {i + 1}",
												@"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
											Log(LogType.Error, $"Invalid Json string at Sheet: {pSheetName} Field: {fieldName} Row: {i + 1}");
										}
										var tempObj = JsonConvert.DeserializeObject(fieldValue);
										var tempJsonStr = JsonConvert.SerializeObject(tempObj);
										if (!nestedFiled)
											fieldContentStr += $"\"{fieldName}\":{tempJsonStr},";
										else
										{
											jsonObject[fieldName] = JObject.Parse(tempJsonStr);
										}
									}
										break;
								}

								// Nested Object
								if (nestedFiled)
									nestedObjects.Add(jsonObject);
							}
						}
					}
				}
				if (nestedObjects.Count > 0)
				{
					var nestedObjectsJson = Helper.ConvertToNestedJson(nestedObjects);
					fieldContentStr += $"{nestedObjectsJson.Substring(1, nestedObjectsJson.Length - 2)}";
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
				if (nestedObjects.Count == 0)
					fieldContentStr = Helper.RemoveLast(fieldContentStr, ",");


				if (!rowIsEmpty)
					content += $"{"{"}{fieldContentStr}{"},"}";
			}
			content = Helper.RemoveLast(content, ",");
			content += "]";

			if (content == "[]")
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty!");
				return null;
			}
			string finalContent = content;
			if (pEncrypt && m_encryption != null)
				finalContent = m_encryption.EncryptValue(content);

			if (pAutoWriteFile)
			{
				Helper.WriteFile(Config.Settings.jsonOutputFolder, $"{pOutputFile}.txt", finalContent);
				if (pEncrypt && m_encryption != null)
					Log(LogType.Message, $"Exported encrypted Json data to {pOutputFile}.txt.");
				else
					Log(LogType.Message, $"Exported Json data to {pOutputFile}.txt.");
			}
			return finalContent;
		}

		private void LoadSheetLocalizationData(IList<IList<object>> rowsData, string pSheetName)
		{
			if (rowsData == null || rowsData.Count == 0)
			{
				Log(LogType.Warning, $"Sheet {pSheetName} is empty!");
				return;
			}

			var idStrings = new List<string>();
			var textDict = new Dictionary<string, List<string>>();
			var firstRow = rowsData[0];
			int maxCellNum = firstRow.Count;

			for (int row = 0; row < rowsData.Count; row++)
			{
				var rowData = rowsData[row];
				if (rowData == null || rowData.Count == 0)
					continue;
				for (int col = 0; col < maxCellNum; col++)
				{
					var fieldValue = rowData[col].ToString().Trim();
					var fieldName = rowsData[0][col].ToString();
					if (!string.IsNullOrEmpty(fieldName))
					{
						//idString
						if (col == 0 && row > 0)
						{
							if (string.IsNullOrEmpty(fieldValue))
								break;
							idStrings.Add(fieldValue);
						}
						//relativeId
						else if (col == 1 && row > 0)
						{
							if (string.IsNullOrEmpty(fieldValue) || m_allIds == null)
								continue;
							bool existId = false;
							foreach (var id in m_allIds)
								if (id.Key.Trim() == fieldValue.Trim())
								{
									fieldValue = id.Value.ToString();
									idStrings[idStrings.Count - 1] = $"{idStrings[idStrings.Count - 1]}_{id.Value}";
									existId = true;
									break;
								}

							if (!existId)
								idStrings[idStrings.Count - 1] = $"{idStrings[idStrings.Count - 1]}_{fieldValue}";
						}
						//languages
						else if (col > 1 && row > 0)
						{
							if (!textDict.ContainsKey(fieldName))
								textDict.Add(fieldName, new List<string>());
							textDict[fieldName].Add(fieldValue);
						}
					}
				}
			}

			if (m_localizationsDict.ContainsKey(pSheetName))
			{
				var builder = m_localizationsDict[pSheetName];
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
				m_localizationsDict[pSheetName] = new LocalizationBuilder()
				{
					idsString = idStrings,
					languageTextDict = textDict,
				};
			}
			else
				m_localizationsDict.Add(pSheetName, new LocalizationBuilder()
				{
					idsString = idStrings,
					languageTextDict = textDict,
				});
		}

		private void TxtGoogleClientID_Leave(object sender, EventArgs e)
		{
			bool changed = Config.Settings.googleClientId != TxtGoogleClientID.Text;
			if (changed)
			{
				Config.Settings.googleClientId = TxtGoogleClientID.Text;
				Config.Save();
			}
		}

		private void TxtGoogleClientSecret_Leave(object sender, EventArgs e)
		{
			bool changed = Config.Settings.googleClientSecret != TxtGoogleClientSecret.Text;
			if (changed)
			{
				Config.Settings.googleClientSecret = TxtGoogleClientSecret.Text;
				Config.Save();
			}
		}

		#endregion

		private void toolStripStatusLabel3_Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/hnb-rabear/excel-to-unity");
		}

		private void LinkGetProVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}
	}
}