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
			this.BtnReadGGSheet = new System.Windows.Forms.Button();
			this.DtgGoogleSheets = new System.Windows.Forms.DataGridView();
			this.BtnImportCredential = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SheetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ExportIDs = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ExportConstants = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.BtnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.DtgGoogleSheets)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnReadGGSheet
			// 
			this.BtnReadGGSheet.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.BtnReadGGSheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnReadGGSheet.Location = new System.Drawing.Point(700, 43);
			this.BtnReadGGSheet.Name = "BtnReadGGSheet";
			this.BtnReadGGSheet.Size = new System.Drawing.Size(88, 23);
			this.BtnReadGGSheet.TabIndex = 0;
			this.BtnReadGGSheet.Text = "BtnTest";
			this.BtnReadGGSheet.UseVisualStyleBackColor = false;
			this.BtnReadGGSheet.Click += new System.EventHandler(this.button1_Click);
			// 
			// DtgGoogleSheets
			// 
			this.DtgGoogleSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DtgGoogleSheets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SheetName,
            this.ExportIDs,
            this.ExportConstants,
            this.BtnDelete});
			this.DtgGoogleSheets.Location = new System.Drawing.Point(12, 72);
			this.DtgGoogleSheets.Name = "DtgGoogleSheets";
			this.DtgGoogleSheets.Size = new System.Drawing.Size(776, 353);
			this.DtgGoogleSheets.TabIndex = 1;
			this.DtgGoogleSheets.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgGoogleSheets_CellClick);
			// 
			// BtnImportCredential
			// 
			this.BtnImportCredential.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.BtnImportCredential.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnImportCredential.Location = new System.Drawing.Point(700, 14);
			this.BtnImportCredential.Name = "BtnImportCredential";
			this.BtnImportCredential.Size = new System.Drawing.Size(88, 23);
			this.BtnImportCredential.TabIndex = 6;
			this.BtnImportCredential.Text = "Setup";
			this.BtnImportCredential.UseVisualStyleBackColor = false;
			this.BtnImportCredential.Click += new System.EventHandler(this.BtnImportCredential_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 428);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.statusStrip1.Size = new System.Drawing.Size(800, 22);
			this.statusStrip1.TabIndex = 9;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(74, 17);
			this.toolStripStatusLabel1.Text = "Status: None";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(111, 12);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(583, 20);
			this.textBox1.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Spread Sheet Key";
			// 
			// SheetName
			// 
			this.SheetName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.SheetName.DataPropertyName = "path";
			this.SheetName.HeaderText = "Sheet Name";
			this.SheetName.Name = "SheetName";
			// 
			// ExportIDs
			// 
			this.ExportIDs.DataPropertyName = "exportIds";
			this.ExportIDs.HeaderText = "Export IDs";
			this.ExportIDs.Name = "ExportIDs";
			this.ExportIDs.Width = 70;
			// 
			// ExportConstants
			// 
			this.ExportConstants.DataPropertyName = "exportConstants";
			this.ExportConstants.HeaderText = "Export Constants";
			this.ExportConstants.Name = "ExportConstants";
			this.ExportConstants.Width = 70;
			// 
			// BtnDelete
			// 
			this.BtnDelete.HeaderText = "Delete";
			this.BtnDelete.Name = "BtnDelete";
			this.BtnDelete.Text = "Delete";
			this.BtnDelete.ToolTipText = "Delete";
			this.BtnDelete.UseColumnTextForButtonValue = true;
			this.BtnDelete.Width = 70;
			// 
			// FrmGoogleSheetSample
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.BtnImportCredential);
			this.Controls.Add(this.DtgGoogleSheets);
			this.Controls.Add(this.BtnReadGGSheet);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmGoogleSheetSample";
			this.Text = "Google Spread Sheets";
			this.Load += new System.EventHandler(this.FrmGoogleSheetSample_Load);
			((System.ComponentModel.ISupportInitialize)(this.DtgGoogleSheets)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnReadGGSheet;
        private System.Windows.Forms.DataGridView DtgGoogleSheets;
        private System.Windows.Forms.Button BtnImportCredential;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn SheetName;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ExportIDs;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ExportConstants;
		private System.Windows.Forms.DataGridViewButtonColumn BtnDelete;
	}
}