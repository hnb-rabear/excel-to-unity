//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Services;
//using Google.Apis.Sheets.v4;
//using Google.Apis.Sheets.v4.Data;
//using Google.Apis.Util.Store;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using static Google.Apis.Sheets.v4.SpreadsheetsResource;

namespace ExcelToUnity_DataConverter
{
    public static class GGConfig
    {
        //private const string FILE_PATH_GG_CREDENTIAL = "Resources\\credential.json";
        //private static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        //private static string ApplicationName = "Data Converter";

        //public static UserCredential GetUserCredential()
        //{
        //    UserCredential credential;

        //    using (var stream = new FileStream(FILE_PATH_GG_CREDENTIAL, FileMode.Open, FileAccess.Read))
        //    {
        //        // The file token.json stores the user's access and refresh tokens, and is created
        //        // automatically when the authorization flow completes for the first time.
        //        string credPath = "token.json";
        //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets,
        //            Scopes,
        //            "user",
        //            CancellationToken.None,
        //            new FileDataStore(credPath, true)).Result;
        //    }

        //    return credential;
        //}

        //public static IList<ValueRange> DownloadSheet(string spreadSheetKey, params string[] sheetsName)
        //{
        //    try
        //    {
        //        var service = new SheetsService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = GetUserCredential(),
        //            ApplicationName = ApplicationName,
        //        });

        //        GetRequest request1 = service.Spreadsheets.Get(spreadSheetKey);
        //        Spreadsheet response1 = request1.Execute();
        //        IList<Sheet> sheets = response1.Sheets;
        //        List<string> selectedSheets = new List<string>();

        //        if ((sheets == null) || (sheets.Count <= 0))
        //            return null;

        //        if (sheetsName != null && sheetsName.Length > 0)
        //        {
        //            foreach (var name in sheetsName)
        //            {
        //                for (int i = sheets.Count - 1; i >= 0; i--)
        //                {
        //                    if (sheets[i].Properties.Title.ToLower() == name.Trim().ToLower())
        //                        selectedSheets.Add(sheets[i].Properties.Title);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = sheets.Count - 1; i >= 0; i--)
        //                selectedSheets.Add(sheets[i].Properties.Title);
        //        }


        //        ValuesResource.BatchGetRequest request2 = service.Spreadsheets.Values.BatchGet(spreadSheetKey);
        //        request2.Ranges = selectedSheets;
        //        BatchGetValuesResponse response2 = request2.Execute();
        //        IList<ValueRange> results = response2.ValueRanges;
        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //}

        //public static string GetCredentialString()
        //{
        //    if (!File.Exists(FILE_PATH_GG_CREDENTIAL))
        //        return "";

        //    using (StreamReader sr = new StreamReader(FILE_PATH_GG_CREDENTIAL))
        //        return sr.ReadToEnd();
        //}

        //public static bool ImportCredential()
        //{
        //    bool success = false;
        //    var openFileDialog = new OpenFileDialog();
        //    openFileDialog.Title = "Import Credential";
        //    openFileDialog.FileName = "Select a text file";
        //    openFileDialog.Filter = "Text files (*.Json)|*.Json";
        //    openFileDialog.Title = "Open text file";
        //    openFileDialog.DefaultExt = "Json";

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        var filePath = openFileDialog.FileName;
        //        using (StreamReader sr = new StreamReader(filePath))
        //        {
        //            string content = sr.ReadToEnd();
        //            if (!string.IsNullOrEmpty(content))
        //            {
        //                success = true;
        //                Helper.WriteFile(FILE_PATH_GG_CREDENTIAL, content);
        //            }
        //        }
        //    }
        //    return success;
        //}
    }
}
