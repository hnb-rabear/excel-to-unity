using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace ExcelToUnity_DataConverter
{
	// 1: eiB2d2B0JTI4JilpdX5gYmVfZnh3aUR+ZGFpeSkyOD4jLmhqY39jYk90bHNvQmtkbWVtXGZoaWcwKyEneA==
	// 2: eiB2d2B0JTI7JilpdX5gYmVfZnh3aUR+ZGFpeSkyOD4jLmhqY39jYk90bHNvQmtkbWVtXGZoaWcwKyEneA==
	// 3: eiB2d2B0JTI6JilpdX5gYmVfZnh3aUR+ZGFpeSkyOD4jLmhqY39jYk90bHNvQmtkbWVtXGZoaWcwKyEneA==

	public static class Config
    {
		public const bool AAA = true;

        private static Settings m_Settings;
        public static Settings Settings => m_Settings;

		private static User m_User;
		public static User User => m_User;

		private static Encryption m_Encryption;

		public static void Init()
        {
			m_Encryption = new Encryption(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
			var savedSettingsFilePath = GetSaveFile();
			if (!File.Exists(savedSettingsFilePath))
			{
				m_Settings = new Settings();
                Save();
				return;
			}
			using (var sr = new StreamReader(savedSettingsFilePath))
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
			string userFilePath = GetLicenseFile();
			using (var sr = new StreamReader(userFilePath))
			{
				string json = sr.ReadToEnd();
				if (AAA)
					json = m_Encryption.Decrypt(json);
				if (!string.IsNullOrEmpty(json))
				{
					try
					{
						m_User = JsonConvert.DeserializeObject<User>(json);
					}
					catch 
					{
						m_User = new User();
					}
				}
				else
					m_User = new User();
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
            saveFileDialog1.DefaultExt = "e2u";
            saveFileDialog1.Filter = "e2u files (*.e2u)|*.e2u";
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
            openFileDialog.FileName = "Select a e2u file";
            openFileDialog.Filter = "e2u files (*.e2u)|*.e2u";
            openFileDialog.Title = "Open e2u file";
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
			var path = Path.Combine(GetSaveDirectory(), "save_temp.e2u");
			if (!File.Exists(path))
				using (File.Create(path)) { }
			return path;
		}

		public static string GetLicenseFile()
		{
			string fileName = AAA ? m_Encryption.Encrypt("license") : "license";
			var path = Path.Combine("Resources", $"{fileName}.e2u");
			if (!File.Exists(path))
				using (File.Create(path)) { }
			return path;
		}

		public static void SaveLicense()
		{
			string content = JsonConvert.SerializeObject(m_User);
			if (AAA)
				content = m_Encryption.Encrypt(content);
			Helper.WriteFile(GetLicenseFile(), content);
		}
	}
}