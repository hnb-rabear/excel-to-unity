using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace ExcelToUnity_DataConverter
{
	public static class Config
    {
        private static Settings m_Settings;

        public static Settings Settings => m_Settings;

        public static void Init()
        {
            var savedSettingsFile = GetSaveFile();
			if (!File.Exists(savedSettingsFile))
			{
				m_Settings = new Settings();
                Save();
				return;
			}
			using (var sr = new StreamReader(savedSettingsFile))
			{
				string settingJson = sr.ReadToEnd();
				if (!string.IsNullOrEmpty(settingJson))
					m_Settings = JsonConvert.DeserializeObject<Settings>(settingJson);
				else
					m_Settings = new Settings();
			}
            for (int i = m_Settings.googleSheetsPaths.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(m_Settings.googleSheetsPaths[i].name)
                    || string.IsNullOrEmpty(m_Settings.googleSheetsPaths[i].id))
                    m_Settings.googleSheetsPaths.RemoveAt(i);

			}
        }

        public static void Save()
        {
			string content = JsonConvert.SerializeObject(m_Settings);
			Helper.WriteFile(GetSaveFile(), content);
		}

        public static void SaveSettingsToFile()
        {
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save Settings";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = "rad";
            saveFileDialog1.Filter = "Rad files (*.rad)|*.rad";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = GetSaveDirectory();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string settingsJson = JsonConvert.SerializeObject(m_Settings);
                Helper.WriteFile(saveFileDialog1.FileName, settingsJson);
            }
        }

        public static bool LoadSettingsFromFile()
        {
            bool success = false;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "Select a Rad file";
            openFileDialog.Filter = "Rad files (*.rad)|*.rad";
            openFileDialog.Title = "Open rad file";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.InitialDirectory = GetSaveDirectory();
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

		public static string GetSaveDirectory()
		{
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "ExcelToUnity_DataConverter");
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			return path;
		}

		public static string GetSaveFile()
		{
			var path = Path.Combine(GetSaveDirectory(), "save_temp.rad");
            if (!File.Exists(path))
                File.Create(path);
            return path;
		}
	}
}