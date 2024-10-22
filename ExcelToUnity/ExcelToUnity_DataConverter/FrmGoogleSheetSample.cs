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

				var list = GetDataFromDataGridView();
			}
		}

		public List<GoogleSpreadSheetPath.SpreadSheet> GetDataFromDataGridView()
		{
			var dataList = new List<GoogleSpreadSheetPath.SpreadSheet>();

			// Get column indices by their names
			int pathColumnIndex = DtgGoogleSheets.Columns["SheetName"].Index;
			int selectedColumnIndex = DtgGoogleSheets.Columns["Selected"].Index;

			// Iterate through each row in the DataGridView
			var list = DtgGoogleSheets.Rows;
			for (int i = 0; i < list.Count; i++)
			{
				DataGridViewRow row = list[i];
				// Only process rows that are not new rows (the empty row at the end of DataGridView)
				if (!row.IsNewRow)
				{
					var data = new GoogleSpreadSheetPath.SpreadSheet()
					{
						name = row.Cells[pathColumnIndex].Value?.ToString(),
						selected = row.Cells[selectedColumnIndex].Value != null && (bool)row.Cells[selectedColumnIndex].Value,
					};

					dataList.Add(data);
				}
			}

			return dataList;
		}
	}
}