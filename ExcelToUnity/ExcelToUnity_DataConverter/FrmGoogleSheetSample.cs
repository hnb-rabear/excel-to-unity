using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExcelToUnity_DataConverter
{
    public partial class FrmGoogleSheetSample : Form
    {
        public FrmGoogleSheetSample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string spreadsheetId = "1nJKuYnBrLiGJTo1VX3dqVGrPcRQmr6WsQIK2aRwRgqc";
            //var values = GGConfig.DownloadSheet(spreadsheetId, "Class Data");
            //if (values != null && values.Count > 0)
            //{
            //    List<TestGG> items = new List<TestGG>();
            //    foreach (var value in values)
            //    {
            //        foreach (var v2 in value.Values)
            //            Console.WriteLine(JsonConvert.ToString(v2.ToString()));
            //    }
            //    dataGridView1.DataSource = null;
            //    dataGridView1.Rows.Clear();
            //    dataGridView1.DataSource = items;
            //}
            //else
            //{
            //    Console.WriteLine("No data found.");
            //}
            //Console.Read();
        }

        private void BtnImportCredential_Click(object sender, EventArgs e)
        {
            var frm = new FrmSetupCredential();
            frm.Show();
        }
    }

    public class TestGG
    {
        public string name { get; set; }
        public string gender { get; set; }
    }
}
