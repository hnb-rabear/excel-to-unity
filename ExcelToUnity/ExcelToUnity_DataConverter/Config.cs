using ExcelToUnity_DataConverter.Entities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace ExcelToUnity_DataConverter
{
	public static class Config
    {
        private const string FILE_PATH_SETTINGS = "Resources\\Configs.txt";
        private static Settings m_Settings;

        public static Settings Settings => m_Settings;

        public static void Init()
        {
            string configFilePath = FILE_PATH_SETTINGS;
            if (!File.Exists(configFilePath))
            {
                File.Create(configFilePath);
                m_Settings = new Settings();
            }
            else
            {
                using (StreamReader sr = new StreamReader(configFilePath))
                {
                    string settingJson = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(settingJson))
                        m_Settings = JsonConvert.DeserializeObject<Settings>(settingJson);
                    else
                        m_Settings = new Settings();
                }
            }
        }

        public static void Save()
        {
			string settingsJson = JsonConvert.SerializeObject(m_Settings);
			Helper.WriteFile(FILE_PATH_SETTINGS, settingsJson);
		}

        public static void SaveSettingsToFile()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "ExcelToUnity_DataConverter");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Export Config File";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = "rad";
            saveFileDialog1.Filter = "Rad files (*.rad)|*.rad";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "ExcelToUnity_DataConverter");

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string settingsJson = JsonConvert.SerializeObject(m_Settings);
                Helper.WriteFile(saveFileDialog1.FileName, settingsJson);
            }
        }

        public static bool LoadSettingsFromFile()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "ExcelToUnity_DataConverter");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            bool success = false;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "Select a Rad file";
            openFileDialog.Filter = "Rad files (*.rad)|*.rad";
            openFileDialog.Title = "Open rad file";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "ExcelToUnity_DataConverter");
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string settingsJson = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(settingsJson))
                    {
                        m_Settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
                        Save();
                        success = true;
                    }
                }
            }
            return success;
        }

        public static void ClearSettings()
        {
            m_Settings = new Settings();
            Save();
        }
    }
}
