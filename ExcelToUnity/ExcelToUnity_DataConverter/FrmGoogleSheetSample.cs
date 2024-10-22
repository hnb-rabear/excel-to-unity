using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Linq;

namespace ExcelToUnity_DataConverter
{
	public partial class FrmGoogleSheetSample : Form
	{
		static string CLIENT_ID = "871414866606-7b9687cp1ibjokihbbfl6nrjr94j14o8.apps.googleusercontent.com";
		static string CLIENT_SECRET = "zF_J3qHpzX5e8i2V-ZEvOdGV";

		public FrmGoogleSheetSample()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			
		}

		private void BtnImportCredential_Click(object sender, EventArgs e)
		{
			var frm = new FrmSetupCredential();
			frm.Show();
		}

		private void FrmGoogleSheetSample_Load(object sender, EventArgs e)
		{

		}

		private void DtgGoogleSheets_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e == null || e.RowIndex == DtgGoogleSheets.NewRowIndex || e.RowIndex < 0)
				return;

			var row = DtgGoogleSheets.Rows[e.RowIndex];

			if (e.ColumnIndex == DtgGoogleSheets.Columns["BtnDelete"].Index)
			{
				//Config.Settings.googleSheets.RemoveAt(e.RowIndex);

				DtgGoogleSheets.Rows.RemoveAt(e.RowIndex);
			}
		}

		// Method to retrieve data from DataGridView into a list
		public List<SpreadSheetConfig> GetDataFromDataGridView(DataGridView dataGridView)
		{
			List<SpreadSheetConfig> dataList = new List<SpreadSheetConfig>();

			// Iterate through each row in the DataGridView
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				// Only process rows that are not new rows (the empty row at the end of DataGridView)
				if (!row.IsNewRow)
				{
					var data = new SpreadSheetConfig()
					{
						path = row.Cells[0].Value?.ToString(),
						exportIds = row.Cells[1].Value != null && (bool)row.Cells[1].Value,
						exportConstants = row.Cells[2].Value != null && (bool)row.Cells[2].Value
					};

					dataList.Add(data);
				}
			}

			return dataList;
		}
	}
}
