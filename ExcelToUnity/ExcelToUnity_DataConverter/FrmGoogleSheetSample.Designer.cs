namespace ExcelToUnity_DataConverter
{
    partial class FrmGoogleSheetSample
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.DtgGoogleSheets = new System.Windows.Forms.DataGridView();
			this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.TxtGoogleSheetId = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.BtnDownload = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.TxtGoogleSheetName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.DtgGoogleSheets)).BeginInit();
			this.SuspendLayout();
			// 
			// DtgGoogleSheets
			// 
			this.DtgGoogleSheets.AllowUserToAddRows = false;
			this.DtgGoogleSheets.AllowUserToDeleteRows = false;
			this.DtgGoogleSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DtgGoogleSheets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.selected});
			this.DtgGoogleSheets.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.DtgGoogleSheets.Location = new System.Drawing.Point(0, 77);
			this.DtgGoogleSheets.Name = "DtgGoogleSheets";
			this.DtgGoogleSheets.Size = new System.Drawing.Size(584, 284);
			this.DtgGoogleSheets.TabIndex = 1;
			this.DtgGoogleSheets.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgGoogleSheets_CellEndEdit);
			// 
			// name
			// 
			this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.name.DataPropertyName = "name";
			this.name.HeaderText = "Name";
			this.name.Name = "name";
			// 
			// selected
			// 
			this.selected.DataPropertyName = "selected";
			this.selected.HeaderText = "Selected";
			this.selected.Name = "selected";
			this.selected.Width = 70;
			// 
			// TxtGoogleSheetId
			// 
			this.TxtGoogleSheetId.Location = new System.Drawing.Point(105, 12);
			this.TxtGoogleSheetId.Name = "TxtGoogleSheetId";
			this.TxtGoogleSheetId.Size = new System.Drawing.Size(373, 20);
			this.TxtGoogleSheetId.TabIndex = 10;
			this.TxtGoogleSheetId.TextChanged += new System.EventHandler(this.TxtGoogleSheetId_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "GG Sheet Id:";
			// 
			// BtnDownload
			// 
			this.BtnDownload.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.BtnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.BtnDownload.Location = new System.Drawing.Point(484, 12);
			this.BtnDownload.Name = "BtnDownload";
			this.BtnDownload.Size = new System.Drawing.Size(88, 46);
			this.BtnDownload.TabIndex = 12;
			this.BtnDownload.Text = "Download";
			this.BtnDownload.UseVisualStyleBackColor = false;
			this.BtnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.Location = new System.Drawing.Point(12, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "List Spread Sheets:";
			// 
			// TxtGoogleSheetName
			// 
			this.TxtGoogleSheetName.Location = new System.Drawing.Point(105, 38);
			this.TxtGoogleSheetName.Name = "TxtGoogleSheetName";
			this.TxtGoogleSheetName.ReadOnly = true;
			this.TxtGoogleSheetName.Size = new System.Drawing.Size(373, 20);
			this.TxtGoogleSheetName.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.Location = new System.Drawing.Point(12, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "GG Sheet Name:";
			// 
			// FrmGoogleSheetSample
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 361);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.TxtGoogleSheetName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.BtnDownload);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TxtGoogleSheetId);
			this.Controls.Add(this.DtgGoogleSheets);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmGoogleSheetSample";
			this.Text = "Google Spread Sheets";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGoogleSheetSample_FormClosing);
			this.Load += new System.EventHandler(this.FrmGoogleSheetSample_Load);
			((System.ComponentModel.ISupportInitialize)(this.DtgGoogleSheets)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DtgGoogleSheets;
		private System.Windows.Forms.TextBox TxtGoogleSheetId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnDownload;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TxtGoogleSheetName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridViewTextBoxColumn name;
		private System.Windows.Forms.DataGridViewCheckBoxColumn selected;
	}
}