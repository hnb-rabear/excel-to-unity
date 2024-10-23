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
using System.ComponentModel;
using ChoETL;
using NPOI.SS.Formula.Functions;

namespace ExcelToUnity_DataConverter
{
	public partial class FrmGoogleSheetSample : Form
	{
		private BindingList<GoogleSheetsPath.Sheet> m_bindingSheets;
		public List<GoogleSheetsPath.Sheet> sheets;
		public string googleSheetId;
		public string googleSheetName;

		public FrmGoogleSheetSample()
		{
			InitializeComponent();
		}

		private void FrmGoogleSheetSample_Load(object sender, EventArgs e)
		{
			TxtGoogleSheetId.Text = googleSheetId;
			TxtGoogleSheetId.ReadOnly = !string.IsNullOrEmpty(googleSheetId);
			TxtGoogleSheetName.Text = googleSheetName;
			TxtGoogleSheetName.ReadOnly = true;
			
			if (!string.IsNullOrEmpty(googleSheetId))
				Authenticate();
		}

		public List<GoogleSheetsPath.Sheet> GetGoogleSheetsFromGridView()
		{
			var dataList = new List<GoogleSheetsPath.Sheet>();

			// Get column indices by their names
			int pathColumnIdx = DtgGoogleSheets.Columns["name"].Index;
			int selectedColumnIdx = DtgGoogleSheets.Columns["selected"].Index;

			// Iterate through each row in the DataGridView
			var list = DtgGoogleSheets.Rows;
			for (int i = 0; i < list.Count; i++)
			{
				DataGridViewRow row = list[i];
				// Only process rows that are not new rows (the empty row at the end of DataGridView)
				if (!row.IsNewRow)
				{
					var data = new GoogleSheetsPath.Sheet()
					{
						name = row.Cells[pathColumnIdx].Value?.ToString(),
						selected = row.Cells[selectedColumnIdx].Value != null && (bool)row.Cells[selectedColumnIdx].Value,
					};

					dataList.Add(data);
				}
			}

			return dataList;
		}

		private void BtnDownload_Click(object sender, EventArgs e)
		
		
		
		{
			string key = TxtGoogleSheetId.Text;
			if (string.IsNullOrEmpty(key))
			{
				Console.WriteLine("Key can not be empty");
				return;
			}

			Authenticate();
		}

		private void Authenticate()
		{
			// Create Google Sheets API service.
			var service = new SheetsService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = Helper.AuthenticateGoogleStore(),
				ApplicationName = MainForm.APPLICATION_NAME,
			});

			googleSheetId = TxtGoogleSheetId.Text;

			// Fetch metadata for the entire spreadsheet.
			Spreadsheet spreadsheet;
			try
			{
				spreadsheet = service.Spreadsheets.Get(googleSheetId).Execute();
			}
			catch
			{
				return;
			}
			googleSheetName = spreadsheet.Properties.Title;
			TxtGoogleSheetName.Text = spreadsheet.Properties.Title;
			sheets = new List<GoogleSheetsPath.Sheet>();
			foreach (var sheet in spreadsheet.Sheets)
			{
				var sheetName = sheet.Properties.Title;
				sheets.Add(new GoogleSheetsPath.Sheet()
				{
					name = sheetName,
					selected = true,
				});
			}

			// Sync with current save
			var savedSheets = Config.Settings.googleSheetsPaths.Find(x => x.id == googleSheetId);
			if (savedSheets != null && savedSheets.sheets != null)
			{
                foreach (var sheet in sheets)
                {
					var existedSheet = savedSheets.sheets.Find(x => x.name == sheet.name);
					if (existedSheet != null)
						sheet.selected = existedSheet.selected;
				}
            }

			m_bindingSheets = new BindingList<GoogleSheetsPath.Sheet>(sheets);
			DtgGoogleSheets.DataSource = m_bindingSheets;
		}

		private void TxtGoogleSheetId_TextChanged(object sender, EventArgs e)
		{
			BtnDownload.Enabled = !string.IsNullOrEmpty(TxtGoogleSheetId.Text);
		}

		private void DtgGoogleSheets_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e == null || e.RowIndex == DtgGoogleSheets.NewRowIndex || e.RowIndex < 0)
				return;

			var row = DtgGoogleSheets.Rows[e.RowIndex];

			if (e.ColumnIndex == DtgGoogleSheets.Columns["selected"].Index)
			{
				int selectedColumnIdx = DtgGoogleSheets.Columns["selected"].Index;
				bool selected = row.Cells[selectedColumnIdx].Value != null && (bool)row.Cells[selectedColumnIdx].Value;
				sheets[e.RowIndex].selected = selected;
			}
		}

		private void FrmGoogleSheetSample_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DtgGoogleSheets.IsCurrentCellInEditMode)
			{
				DtgGoogleSheets.EndEdit();

				// Save currennt cell before form closing
				int selectedColumnIdx = DtgGoogleSheets.Columns["selected"].Index;
				int columnIndex = DtgGoogleSheets.CurrentCell.ColumnIndex;
				if (columnIndex == selectedColumnIdx)
				{
					int rowIndex = DtgGoogleSheets.CurrentCell.RowIndex;
					bool selected = DtgGoogleSheets.CurrentRow.Cells[selectedColumnIdx].Value != null && (bool)DtgGoogleSheets.CurrentRow.Cells[selectedColumnIdx].Value;
					sheets[rowIndex].selected = selected;
				}
			}
		}
	}
}