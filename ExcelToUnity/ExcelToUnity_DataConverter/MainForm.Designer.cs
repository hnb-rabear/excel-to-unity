namespace ExcelToUnity_DataConverter
{
    partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.txtSettingOutputLocalizationFilePath = new System.Windows.Forms.TextBox();
			this.chkSeperateLocalization = new System.Windows.Forms.CheckBox();
			this.chkSeperateConstants = new System.Windows.Forms.CheckBox();
			this.txtUnminimizeFields = new System.Windows.Forms.TextBox();
			this.chkMergeJsonIntoSingleOne2 = new System.Windows.Forms.CheckBox();
			this.chkSeperateIDs = new System.Windows.Forms.CheckBox();
			this.txtSettingExcludedSheet = new System.Windows.Forms.TextBox();
			this.txtSettingOuputConstantsFilePath = new System.Windows.Forms.TextBox();
			this.txtSettingOutputDataFilePath = new System.Windows.Forms.TextBox();
			this.txtSettingEncryptionKey = new System.Windows.Forms.TextBox();
			this.txtLanguageMaps = new System.Windows.Forms.TextBox();
			this.chkKeepOnlyEnumAsIds = new System.Windows.Forms.CheckBox();
			this.chkSettingEnableEncryption = new System.Windows.Forms.CheckBox();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.statusStrip2 = new System.Windows.Forms.StatusStrip();
			this.txtVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.ToolStripStatusLblCopyRight = new System.Windows.Forms.ToolStripStatusLabel();
			this.tpChangeLog = new System.Windows.Forms.TabPage();
			this.weboxChangelog = new System.Windows.Forms.WebBrowser();
			this.linkDownloadExample = new System.Windows.Forms.LinkLabel();
			this.tpEncription = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtEncryptionInput = new System.Windows.Forms.TextBox();
			this.txtEncryptionOutput = new System.Windows.Forms.TextBox();
			this.BtnDecrypt = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.TxtGoogleClientSecret = new System.Windows.Forms.TextBox();
			this.TxtGoogleClientID = new System.Windows.Forms.TextBox();
			this.lblGoogleClientSecret = new System.Windows.Forms.Label();
			this.lblGoogleClientId = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.btnOpenFolderLocalization = new System.Windows.Forms.Button();
			this.btnSelectFolderLocalization = new System.Windows.Forms.Button();
			this.btnLoadDefaultSettings = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.btnOpenFolder2 = new System.Windows.Forms.Button();
			this.btnOpenFolder1 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.txtSettingNamespace = new System.Windows.Forms.TextBox();
			this.lblbExcluedSheets = new System.Windows.Forms.Label();
			this.lblEncryptionKey = new System.Windows.Forms.Label();
			this.btnSelectFolder2 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSelectFolder = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tpExportMultiExcels = new System.Windows.Forms.TabPage();
			this.panel3 = new System.Windows.Forms.Panel();
			this.BtnAddFile = new System.Windows.Forms.Button();
			this.DtgFilePaths = new System.Windows.Forms.DataGridView();
			this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.exportIds = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.exportConstants = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.status = new System.Windows.Forms.DataGridViewImageColumn();
			this.BtnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.BtnAllInOne = new System.Windows.Forms.Button();
			this.txtLog2 = new System.Windows.Forms.TextBox();
			this.tpExportExcel = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.BtnExportAll = new System.Windows.Forms.Button();
			this.BtnExportJson = new System.Windows.Forms.Button();
			this.BtnReloadGrid = new System.Windows.Forms.Button();
			this.btnExportLocalization = new System.Windows.Forms.Button();
			this.BtnExportConstants = new System.Windows.Forms.Button();
			this.BtnExportIds = new System.Windows.Forms.Button();
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.DtgIDs = new System.Windows.Forms.DataGridView();
			this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.txtInputXLSXFilePath = new System.Windows.Forms.TextBox();
			this.DtgSheets = new System.Windows.Forms.DataGridView();
			this.SheetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.tabMenu = new System.Windows.Forms.TabControl();
			this.tpGoogleSpreadSheets = new System.Windows.Forms.TabPage();
			this.TxtLogExportingGoogleSheets = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BtnExportGoogleSheets = new System.Windows.Forms.Button();
			this.BtnAddGoogleSheet = new System.Windows.Forms.Button();
			this.DtgGoogleSheets = new System.Windows.Forms.DataGridView();
			this.GoogleSheetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GoogleSheetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BtnEditGoogleSheet = new System.Windows.Forms.DataGridViewButtonColumn();
			this.BtnDeleteGoogleSheet = new System.Windows.Forms.DataGridViewButtonColumn();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.BtnSaveSettings = new System.Windows.Forms.Button();
			this.BtnLoadSettings = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
			this.statusStrip2.SuspendLayout();
			this.tpChangeLog.SuspendLayout();
			this.tpEncription.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.tpExportMultiExcels.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtgFilePaths)).BeginInit();
			this.tpExportExcel.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtgIDs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DtgSheets)).BeginInit();
			this.tabMenu.SuspendLayout();
			this.tpGoogleSpreadSheets.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtgGoogleSheets)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 10000;
			this.toolTip.InitialDelay = 100;
			this.toolTip.ReshowDelay = 100;
			this.toolTip.ShowAlways = true;
			// 
			// txtSettingOutputLocalizationFilePath
			// 
			this.txtSettingOutputLocalizationFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSettingOutputLocalizationFilePath.Location = new System.Drawing.Point(105, 57);
			this.txtSettingOutputLocalizationFilePath.Name = "txtSettingOutputLocalizationFilePath";
			this.txtSettingOutputLocalizationFilePath.Size = new System.Drawing.Size(464, 20);
			this.txtSettingOutputLocalizationFilePath.TabIndex = 43;
			this.toolTip.SetToolTip(this.txtSettingOutputLocalizationFilePath, "Folder where the localization text files will be exported");
			this.txtSettingOutputLocalizationFilePath.TextChanged += new System.EventHandler(this.txtSettingOutputLocalizationFilePath_TextChanged);
			// 
			// chkSeperateLocalization
			// 
			this.chkSeperateLocalization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSeperateLocalization.AutoSize = true;
			this.chkSeperateLocalization.Location = new System.Drawing.Point(105, 155);
			this.chkSeperateLocalization.Name = "chkSeperateLocalization";
			this.chkSeperateLocalization.Size = new System.Drawing.Size(161, 17);
			this.chkSeperateLocalization.TabIndex = 40;
			this.chkSeperateLocalization.Text = "Keep Localizations Separate";
			this.toolTip.SetToolTip(this.chkSeperateLocalization, "When this option is selected, the Localizations sheet with different name will be" +
        " exported individually");
			this.chkSeperateLocalization.UseVisualStyleBackColor = true;
			this.chkSeperateLocalization.CheckedChanged += new System.EventHandler(this.ChkSeparateLocalization_CheckedChanged);
			// 
			// chkSeperateConstants
			// 
			this.chkSeperateConstants.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSeperateConstants.AutoSize = true;
			this.chkSeperateConstants.Location = new System.Drawing.Point(105, 109);
			this.chkSeperateConstants.Name = "chkSeperateConstants";
			this.chkSeperateConstants.Size = new System.Drawing.Size(147, 17);
			this.chkSeperateConstants.TabIndex = 39;
			this.chkSeperateConstants.Text = "Keep Constants Separate";
			this.toolTip.SetToolTip(this.chkSeperateConstants, "When this option is selected, the Constants sheet in each Excel file will be expo" +
        "rted individually");
			this.chkSeperateConstants.UseVisualStyleBackColor = true;
			this.chkSeperateConstants.CheckedChanged += new System.EventHandler(this.ChkSeparateConstants_CheckedChanged);
			// 
			// txtUnminimizeFields
			// 
			this.txtUnminimizeFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUnminimizeFields.Location = new System.Drawing.Point(105, 204);
			this.txtUnminimizeFields.Name = "txtUnminimizeFields";
			this.txtUnminimizeFields.Size = new System.Drawing.Size(583, 20);
			this.txtUnminimizeFields.TabIndex = 34;
			this.txtUnminimizeFields.Text = "id; mode; type; group; level; rank";
			this.toolTip.SetToolTip(this.txtUnminimizeFields, "Type the name of the column in the json data sheet that you want to export even i" +
        "f it has no values.\r\nSeparate each field with a comma ‘;’");
			this.txtUnminimizeFields.TextChanged += new System.EventHandler(this.txtUnminimizeFields_TextChanged);
			this.txtUnminimizeFields.Leave += new System.EventHandler(this.txtUnminimizeFields_Leave);
			// 
			// chkMergeJsonIntoSingleOne2
			// 
			this.chkMergeJsonIntoSingleOne2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMergeJsonIntoSingleOne2.AutoSize = true;
			this.chkMergeJsonIntoSingleOne2.Location = new System.Drawing.Point(312, 109);
			this.chkMergeJsonIntoSingleOne2.Name = "chkMergeJsonIntoSingleOne2";
			this.chkMergeJsonIntoSingleOne2.Size = new System.Drawing.Size(129, 17);
			this.chkMergeJsonIntoSingleOne2.TabIndex = 32;
			this.chkMergeJsonIntoSingleOne2.Text = "One Json - One Excel";
			this.toolTip.SetToolTip(this.chkMergeJsonIntoSingleOne2, "Export a single JSON file for each Excel file");
			this.chkMergeJsonIntoSingleOne2.UseVisualStyleBackColor = true;
			this.chkMergeJsonIntoSingleOne2.CheckedChanged += new System.EventHandler(this.chkMergeJsonIntoSingleExcel2_CheckedChanged);
			// 
			// chkSeperateIDs
			// 
			this.chkSeperateIDs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSeperateIDs.AutoSize = true;
			this.chkSeperateIDs.Location = new System.Drawing.Point(105, 132);
			this.chkSeperateIDs.Name = "chkSeperateIDs";
			this.chkSeperateIDs.Size = new System.Drawing.Size(111, 17);
			this.chkSeperateIDs.TabIndex = 25;
			this.chkSeperateIDs.Text = "Keep ID Separate";
			this.toolTip.SetToolTip(this.chkSeperateIDs, "When this option is selected, the IDs sheet in each Excel file will be exported i" +
        "ndividually");
			this.chkSeperateIDs.UseVisualStyleBackColor = true;
			this.chkSeperateIDs.CheckedChanged += new System.EventHandler(this.ChkSeparateIDs_CheckedChanged);
			// 
			// txtSettingExcludedSheet
			// 
			this.txtSettingExcludedSheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSettingExcludedSheet.Location = new System.Drawing.Point(105, 230);
			this.txtSettingExcludedSheet.Name = "txtSettingExcludedSheet";
			this.txtSettingExcludedSheet.Size = new System.Drawing.Size(583, 20);
			this.txtSettingExcludedSheet.TabIndex = 21;
			this.txtSettingExcludedSheet.Text = "Sheet1; Sheet2; Sheet3;";
			this.toolTip.SetToolTip(this.txtSettingExcludedSheet, "Type the name of the sheet that you want to skip when exporting. The skipped shee" +
        "t will still be processed but not exported.\r\nSeparate each sheet with a comma ‘;" +
        "’");
			this.txtSettingExcludedSheet.TextChanged += new System.EventHandler(this.txtSettingExcludedSheet_TextChanged);
			this.txtSettingExcludedSheet.Leave += new System.EventHandler(this.txtSettingExcludedSheet_Leave);
			// 
			// txtSettingOuputConstantsFilePath
			// 
			this.txtSettingOuputConstantsFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSettingOuputConstantsFilePath.Location = new System.Drawing.Point(105, 31);
			this.txtSettingOuputConstantsFilePath.Name = "txtSettingOuputConstantsFilePath";
			this.txtSettingOuputConstantsFilePath.Size = new System.Drawing.Size(464, 20);
			this.txtSettingOuputConstantsFilePath.TabIndex = 14;
			this.toolTip.SetToolTip(this.txtSettingOuputConstantsFilePath, "Folder where the Constants and IDs classes will be exported");
			this.txtSettingOuputConstantsFilePath.TextChanged += new System.EventHandler(this.TxtOutputConstantsFilePath_TextChanged);
			// 
			// txtSettingOutputDataFilePath
			// 
			this.txtSettingOutputDataFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSettingOutputDataFilePath.Location = new System.Drawing.Point(105, 6);
			this.txtSettingOutputDataFilePath.Name = "txtSettingOutputDataFilePath";
			this.txtSettingOutputDataFilePath.Size = new System.Drawing.Size(464, 20);
			this.txtSettingOutputDataFilePath.TabIndex = 11;
			this.toolTip.SetToolTip(this.txtSettingOutputDataFilePath, "Folder where the json files will be exported");
			this.txtSettingOutputDataFilePath.TextChanged += new System.EventHandler(this.txtOutputDataFilePath_TextChanged);
			// 
			// txtSettingEncryptionKey
			// 
			this.txtSettingEncryptionKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSettingEncryptionKey.Location = new System.Drawing.Point(105, 308);
			this.txtSettingEncryptionKey.Name = "txtSettingEncryptionKey";
			this.txtSettingEncryptionKey.Size = new System.Drawing.Size(583, 20);
			this.txtSettingEncryptionKey.TabIndex = 17;
			this.txtSettingEncryptionKey.Text = resources.GetString("txtSettingEncryptionKey.Text");
			this.toolTip.SetToolTip(this.txtSettingEncryptionKey, "The key that encrypts the json data before exporting it");
			this.txtSettingEncryptionKey.TextChanged += new System.EventHandler(this.txtSettingEncryptionKey_TextChanged);
			this.txtSettingEncryptionKey.Leave += new System.EventHandler(this.txtSettingEncryptionKey_Leave);
			// 
			// txtLanguageMaps
			// 
			this.txtLanguageMaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLanguageMaps.Location = new System.Drawing.Point(105, 178);
			this.txtLanguageMaps.Name = "txtLanguageMaps";
			this.txtLanguageMaps.Size = new System.Drawing.Size(583, 20);
			this.txtLanguageMaps.TabIndex = 45;
			this.txtLanguageMaps.Tag = "";
			this.txtLanguageMaps.Text = "japan (jp); korea (ko)";
			this.toolTip.SetToolTip(this.txtLanguageMaps, "Type the name of the language you want to localize. The tool will extract all uni" +
        "que characters for each language and export them to a txt file.\r\nSeparate each l" +
        "anguage with a comma ‘;’");
			this.txtLanguageMaps.TextChanged += new System.EventHandler(this.txtLanguageMaps_TextChanged);
			this.txtLanguageMaps.Leave += new System.EventHandler(this.txtLanguageMaps_Leave);
			// 
			// chkKeepOnlyEnumAsIds
			// 
			this.chkKeepOnlyEnumAsIds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkKeepOnlyEnumAsIds.AutoSize = true;
			this.chkKeepOnlyEnumAsIds.Location = new System.Drawing.Point(312, 132);
			this.chkKeepOnlyEnumAsIds.Name = "chkKeepOnlyEnumAsIds";
			this.chkKeepOnlyEnumAsIds.Size = new System.Drawing.Size(110, 17);
			this.chkKeepOnlyEnumAsIds.TabIndex = 38;
			this.chkKeepOnlyEnumAsIds.Text = "Only Enum as IDs";
			this.toolTip.SetToolTip(this.chkKeepOnlyEnumAsIds, "In the IDs sheet, ID columns with the [enum] tag will only export data in enum fo" +
        "rm!");
			this.chkKeepOnlyEnumAsIds.UseVisualStyleBackColor = true;
			this.chkKeepOnlyEnumAsIds.CheckedChanged += new System.EventHandler(this.chkKeepOnlyEnumAsIds_CheckedChanged);
			// 
			// chkSettingEnableEncryption
			// 
			this.chkSettingEnableEncryption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSettingEnableEncryption.AutoSize = true;
			this.chkSettingEnableEncryption.Location = new System.Drawing.Point(312, 155);
			this.chkSettingEnableEncryption.Name = "chkSettingEnableEncryption";
			this.chkSettingEnableEncryption.Size = new System.Drawing.Size(87, 17);
			this.chkSettingEnableEncryption.TabIndex = 19;
			this.chkSettingEnableEncryption.Text = "Encrypt Json";
			this.toolTip.SetToolTip(this.chkSettingEnableEncryption, "Encrypts the json data before exporting it");
			this.chkSettingEnableEncryption.UseVisualStyleBackColor = true;
			this.chkSettingEnableEncryption.CheckedChanged += new System.EventHandler(this.chkSettingEnableEncryption_CheckedChanged);
			// 
			// statusStrip2
			// 
			this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtVersion,
            this.ToolStripStatusLblCopyRight});
			this.statusStrip2.Location = new System.Drawing.Point(0, 454);
			this.statusStrip2.Name = "statusStrip2";
			this.statusStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.statusStrip2.Size = new System.Drawing.Size(704, 22);
			this.statusStrip2.TabIndex = 9;
			// 
			// txtVersion
			// 
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.txtVersion.Size = new System.Drawing.Size(37, 17);
			this.txtVersion.Text = "v1.4.5";
			// 
			// ToolStripStatusLblCopyRight
			// 
			this.ToolStripStatusLblCopyRight.IsLink = true;
			this.ToolStripStatusLblCopyRight.Name = "ToolStripStatusLblCopyRight";
			this.ToolStripStatusLblCopyRight.Size = new System.Drawing.Size(121, 17);
			this.ToolStripStatusLblCopyRight.Text = "© (2018-2024) Rabear";
			this.ToolStripStatusLblCopyRight.Click += new System.EventHandler(this.toolStripStatusLabel3_Click);
			// 
			// tpChangeLog
			// 
			this.tpChangeLog.Controls.Add(this.weboxChangelog);
			this.tpChangeLog.Location = new System.Drawing.Point(4, 22);
			this.tpChangeLog.Name = "tpChangeLog";
			this.tpChangeLog.Padding = new System.Windows.Forms.Padding(3);
			this.tpChangeLog.Size = new System.Drawing.Size(696, 392);
			this.tpChangeLog.TabIndex = 6;
			this.tpChangeLog.Text = "Changes Log";
			// 
			// weboxChangelog
			// 
			this.weboxChangelog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.weboxChangelog.Location = new System.Drawing.Point(3, 3);
			this.weboxChangelog.MinimumSize = new System.Drawing.Size(20, 20);
			this.weboxChangelog.Name = "weboxChangelog";
			this.weboxChangelog.Size = new System.Drawing.Size(690, 386);
			this.weboxChangelog.TabIndex = 0;
			// 
			// linkDownloadExample
			// 
			this.linkDownloadExample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkDownloadExample.AutoSize = true;
			this.linkDownloadExample.Location = new System.Drawing.Point(594, 9);
			this.linkDownloadExample.Name = "linkDownloadExample";
			this.linkDownloadExample.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.linkDownloadExample.Size = new System.Drawing.Size(98, 13);
			this.linkDownloadExample.TabIndex = 26;
			this.linkDownloadExample.TabStop = true;
			this.linkDownloadExample.Text = "Download Example";
			this.linkDownloadExample.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// tpEncription
			// 
			this.tpEncription.Controls.Add(this.splitContainer1);
			this.tpEncription.Location = new System.Drawing.Point(4, 22);
			this.tpEncription.Name = "tpEncription";
			this.tpEncription.Padding = new System.Windows.Forms.Padding(3);
			this.tpEncription.Size = new System.Drawing.Size(696, 392);
			this.tpEncription.TabIndex = 4;
			this.tpEncription.Text = "Encryption Tool";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.txtEncryptionInput);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtEncryptionOutput);
			this.splitContainer1.Panel2.Controls.Add(this.BtnDecrypt);
			this.splitContainer1.Panel2.Controls.Add(this.button1);
			this.splitContainer1.Size = new System.Drawing.Size(690, 386);
			this.splitContainer1.SplitterDistance = 193;
			this.splitContainer1.TabIndex = 4;
			// 
			// txtEncryptionInput
			// 
			this.txtEncryptionInput.BackColor = System.Drawing.Color.White;
			this.txtEncryptionInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEncryptionInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEncryptionInput.Location = new System.Drawing.Point(0, 0);
			this.txtEncryptionInput.Margin = new System.Windows.Forms.Padding(0);
			this.txtEncryptionInput.Multiline = true;
			this.txtEncryptionInput.Name = "txtEncryptionInput";
			this.txtEncryptionInput.Size = new System.Drawing.Size(690, 193);
			this.txtEncryptionInput.TabIndex = 0;
			// 
			// txtEncryptionOutput
			// 
			this.txtEncryptionOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtEncryptionOutput.BackColor = System.Drawing.Color.White;
			this.txtEncryptionOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEncryptionOutput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.txtEncryptionOutput.Location = new System.Drawing.Point(0, 37);
			this.txtEncryptionOutput.Margin = new System.Windows.Forms.Padding(0);
			this.txtEncryptionOutput.Multiline = true;
			this.txtEncryptionOutput.Name = "txtEncryptionOutput";
			this.txtEncryptionOutput.ReadOnly = true;
			this.txtEncryptionOutput.Size = new System.Drawing.Size(690, 152);
			this.txtEncryptionOutput.TabIndex = 1;
			// 
			// BtnDecrypt
			// 
			this.BtnDecrypt.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.BtnDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnDecrypt.Location = new System.Drawing.Point(0, 3);
			this.BtnDecrypt.Name = "BtnDecrypt";
			this.BtnDecrypt.Size = new System.Drawing.Size(88, 28);
			this.BtnDecrypt.TabIndex = 2;
			this.BtnDecrypt.Text = "Decrypt";
			this.BtnDecrypt.UseVisualStyleBackColor = false;
			this.BtnDecrypt.Click += new System.EventHandler(this.BtnDecrypt_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(94, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 28);
			this.button1.TabIndex = 3;
			this.button1.Text = "Encrypt";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tpSettings
			// 
			this.tpSettings.Controls.Add(this.TxtGoogleClientSecret);
			this.tpSettings.Controls.Add(this.TxtGoogleClientID);
			this.tpSettings.Controls.Add(this.lblGoogleClientSecret);
			this.tpSettings.Controls.Add(this.lblGoogleClientId);
			this.tpSettings.Controls.Add(this.label11);
			this.tpSettings.Controls.Add(this.txtLanguageMaps);
			this.tpSettings.Controls.Add(this.label10);
			this.tpSettings.Controls.Add(this.txtSettingOutputLocalizationFilePath);
			this.tpSettings.Controls.Add(this.btnOpenFolderLocalization);
			this.tpSettings.Controls.Add(this.btnSelectFolderLocalization);
			this.tpSettings.Controls.Add(this.chkSeperateLocalization);
			this.tpSettings.Controls.Add(this.chkSeperateConstants);
			this.tpSettings.Controls.Add(this.chkKeepOnlyEnumAsIds);
			this.tpSettings.Controls.Add(this.btnLoadDefaultSettings);
			this.tpSettings.Controls.Add(this.label9);
			this.tpSettings.Controls.Add(this.txtUnminimizeFields);
			this.tpSettings.Controls.Add(this.chkMergeJsonIntoSingleOne2);
			this.tpSettings.Controls.Add(this.btnOpenFolder2);
			this.tpSettings.Controls.Add(this.btnOpenFolder1);
			this.tpSettings.Controls.Add(this.chkSeperateIDs);
			this.tpSettings.Controls.Add(this.label8);
			this.tpSettings.Controls.Add(this.txtSettingNamespace);
			this.tpSettings.Controls.Add(this.txtSettingExcludedSheet);
			this.tpSettings.Controls.Add(this.txtSettingEncryptionKey);
			this.tpSettings.Controls.Add(this.txtSettingOuputConstantsFilePath);
			this.tpSettings.Controls.Add(this.txtSettingOutputDataFilePath);
			this.tpSettings.Controls.Add(this.lblbExcluedSheets);
			this.tpSettings.Controls.Add(this.chkSettingEnableEncryption);
			this.tpSettings.Controls.Add(this.lblEncryptionKey);
			this.tpSettings.Controls.Add(this.btnSelectFolder2);
			this.tpSettings.Controls.Add(this.label3);
			this.tpSettings.Controls.Add(this.btnSelectFolder);
			this.tpSettings.Controls.Add(this.label2);
			this.tpSettings.Location = new System.Drawing.Point(4, 22);
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tpSettings.Size = new System.Drawing.Size(696, 392);
			this.tpSettings.TabIndex = 3;
			this.tpSettings.Text = "Settings";
			// 
			// TxtGoogleClientSecret
			// 
			this.TxtGoogleClientSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtGoogleClientSecret.Location = new System.Drawing.Point(105, 282);
			this.TxtGoogleClientSecret.Name = "TxtGoogleClientSecret";
			this.TxtGoogleClientSecret.Size = new System.Drawing.Size(583, 20);
			this.TxtGoogleClientSecret.TabIndex = 50;
			this.TxtGoogleClientSecret.Leave += new System.EventHandler(this.TxtGoogleClientSecret_Leave);
			// 
			// TxtGoogleClientID
			// 
			this.TxtGoogleClientID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtGoogleClientID.Location = new System.Drawing.Point(105, 256);
			this.TxtGoogleClientID.Name = "TxtGoogleClientID";
			this.TxtGoogleClientID.Size = new System.Drawing.Size(583, 20);
			this.TxtGoogleClientID.TabIndex = 49;
			this.TxtGoogleClientID.Leave += new System.EventHandler(this.TxtGoogleClientID_Leave);
			// 
			// lblGoogleClientSecret
			// 
			this.lblGoogleClientSecret.AutoSize = true;
			this.lblGoogleClientSecret.Location = new System.Drawing.Point(6, 285);
			this.lblGoogleClientSecret.Name = "lblGoogleClientSecret";
			this.lblGoogleClientSecret.Size = new System.Drawing.Size(86, 13);
			this.lblGoogleClientSecret.TabIndex = 48;
			this.lblGoogleClientSecret.Text = "GG Client Secret";
			// 
			// lblGoogleClientId
			// 
			this.lblGoogleClientId.AutoSize = true;
			this.lblGoogleClientId.Location = new System.Drawing.Point(6, 259);
			this.lblGoogleClientId.Name = "lblGoogleClientId";
			this.lblGoogleClientId.Size = new System.Drawing.Size(66, 13);
			this.lblGoogleClientId.TabIndex = 47;
			this.lblGoogleClientId.Text = "GG Client ID";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 181);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(83, 13);
			this.label11.TabIndex = 46;
			this.label11.Text = "Language maps";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 61);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(98, 13);
			this.label10.TabIndex = 44;
			this.label10.Text = "Localization Output";
			// 
			// btnOpenFolderLocalization
			// 
			this.btnOpenFolderLocalization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenFolderLocalization.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnOpenFolderLocalization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnOpenFolderLocalization.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOpenFolderLocalization.Location = new System.Drawing.Point(615, 57);
			this.btnOpenFolderLocalization.Margin = new System.Windows.Forms.Padding(0);
			this.btnOpenFolderLocalization.Name = "btnOpenFolderLocalization";
			this.btnOpenFolderLocalization.Size = new System.Drawing.Size(78, 20);
			this.btnOpenFolderLocalization.TabIndex = 42;
			this.btnOpenFolderLocalization.Text = "Open Explorer";
			this.btnOpenFolderLocalization.UseVisualStyleBackColor = false;
			this.btnOpenFolderLocalization.Click += new System.EventHandler(this.BtnOpenFolderLocalization_Click);
			// 
			// btnSelectFolderLocalization
			// 
			this.btnSelectFolderLocalization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectFolderLocalization.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnSelectFolderLocalization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnSelectFolderLocalization.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSelectFolderLocalization.Location = new System.Drawing.Point(572, 57);
			this.btnSelectFolderLocalization.Margin = new System.Windows.Forms.Padding(0);
			this.btnSelectFolderLocalization.Name = "btnSelectFolderLocalization";
			this.btnSelectFolderLocalization.Size = new System.Drawing.Size(43, 20);
			this.btnSelectFolderLocalization.TabIndex = 41;
			this.btnSelectFolderLocalization.Text = "Select";
			this.btnSelectFolderLocalization.UseVisualStyleBackColor = false;
			this.btnSelectFolderLocalization.Click += new System.EventHandler(this.BtnSelectFolderLocalization_Click);
			// 
			// btnLoadDefaultSettings
			// 
			this.btnLoadDefaultSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadDefaultSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnLoadDefaultSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnLoadDefaultSettings.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLoadDefaultSettings.Location = new System.Drawing.Point(596, 361);
			this.btnLoadDefaultSettings.Name = "btnLoadDefaultSettings";
			this.btnLoadDefaultSettings.Size = new System.Drawing.Size(97, 25);
			this.btnLoadDefaultSettings.TabIndex = 36;
			this.btnLoadDefaultSettings.Text = "Reset to Default";
			this.btnLoadDefaultSettings.UseVisualStyleBackColor = false;
			this.btnLoadDefaultSettings.Click += new System.EventHandler(this.BtnLoadDefaultSettings_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 207);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(90, 13);
			this.label9.TabIndex = 35;
			this.label9.Text = "Persistent column";
			// 
			// btnOpenFolder2
			// 
			this.btnOpenFolder2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenFolder2.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnOpenFolder2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnOpenFolder2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOpenFolder2.Location = new System.Drawing.Point(615, 31);
			this.btnOpenFolder2.Margin = new System.Windows.Forms.Padding(0);
			this.btnOpenFolder2.Name = "btnOpenFolder2";
			this.btnOpenFolder2.Size = new System.Drawing.Size(78, 20);
			this.btnOpenFolder2.TabIndex = 28;
			this.btnOpenFolder2.Text = "Open Explorer";
			this.btnOpenFolder2.UseVisualStyleBackColor = false;
			this.btnOpenFolder2.Click += new System.EventHandler(this.BtnOpenFolder2_Click);
			// 
			// btnOpenFolder1
			// 
			this.btnOpenFolder1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenFolder1.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnOpenFolder1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnOpenFolder1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOpenFolder1.Location = new System.Drawing.Point(615, 6);
			this.btnOpenFolder1.Margin = new System.Windows.Forms.Padding(0);
			this.btnOpenFolder1.Name = "btnOpenFolder1";
			this.btnOpenFolder1.Size = new System.Drawing.Size(78, 20);
			this.btnOpenFolder1.TabIndex = 27;
			this.btnOpenFolder1.Text = "Open Explorer";
			this.btnOpenFolder1.UseVisualStyleBackColor = false;
			this.btnOpenFolder1.Click += new System.EventHandler(this.BtnOpenFolder1_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 87);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 13);
			this.label8.TabIndex = 24;
			this.label8.Text = "Namespace";
			// 
			// txtSettingNamespace
			// 
			this.txtSettingNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSettingNamespace.Location = new System.Drawing.Point(105, 83);
			this.txtSettingNamespace.Name = "txtSettingNamespace";
			this.txtSettingNamespace.Size = new System.Drawing.Size(464, 20);
			this.txtSettingNamespace.TabIndex = 23;
			this.txtSettingNamespace.TextChanged += new System.EventHandler(this.txtSettingNamespace_TextChanged);
			this.txtSettingNamespace.Leave += new System.EventHandler(this.txtSettingNamespace_Leave);
			// 
			// lblbExcluedSheets
			// 
			this.lblbExcluedSheets.AutoSize = true;
			this.lblbExcluedSheets.Location = new System.Drawing.Point(6, 233);
			this.lblbExcluedSheets.Name = "lblbExcluedSheets";
			this.lblbExcluedSheets.Size = new System.Drawing.Size(87, 13);
			this.lblbExcluedSheets.TabIndex = 22;
			this.lblbExcluedSheets.Text = "Excluded Sheets";
			// 
			// lblEncryptionKey
			// 
			this.lblEncryptionKey.AutoSize = true;
			this.lblEncryptionKey.Location = new System.Drawing.Point(6, 311);
			this.lblEncryptionKey.Name = "lblEncryptionKey";
			this.lblEncryptionKey.Size = new System.Drawing.Size(78, 13);
			this.lblEncryptionKey.TabIndex = 18;
			this.lblEncryptionKey.Text = "Encryption Key\r\n";
			// 
			// btnSelectFolder2
			// 
			this.btnSelectFolder2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectFolder2.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnSelectFolder2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnSelectFolder2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSelectFolder2.Location = new System.Drawing.Point(572, 31);
			this.btnSelectFolder2.Margin = new System.Windows.Forms.Padding(0);
			this.btnSelectFolder2.Name = "btnSelectFolder2";
			this.btnSelectFolder2.Size = new System.Drawing.Size(43, 20);
			this.btnSelectFolder2.TabIndex = 16;
			this.btnSelectFolder2.Text = "Select";
			this.btnSelectFolder2.UseVisualStyleBackColor = false;
			this.btnSelectFolder2.Click += new System.EventHandler(this.BtnSelectOutputConstantsFile_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 35);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "Constant Output";
			// 
			// btnSelectFolder
			// 
			this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectFolder.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnSelectFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSelectFolder.Location = new System.Drawing.Point(572, 6);
			this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(0);
			this.btnSelectFolder.Name = "btnSelectFolder";
			this.btnSelectFolder.Size = new System.Drawing.Size(43, 20);
			this.btnSelectFolder.TabIndex = 13;
			this.btnSelectFolder.Text = "Select";
			this.btnSelectFolder.UseVisualStyleBackColor = false;
			this.btnSelectFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Json Data Output";
			// 
			// tpExportMultiExcels
			// 
			this.tpExportMultiExcels.Controls.Add(this.panel3);
			this.tpExportMultiExcels.Controls.Add(this.txtLog2);
			this.tpExportMultiExcels.Location = new System.Drawing.Point(4, 22);
			this.tpExportMultiExcels.Name = "tpExportMultiExcels";
			this.tpExportMultiExcels.Padding = new System.Windows.Forms.Padding(3);
			this.tpExportMultiExcels.Size = new System.Drawing.Size(696, 392);
			this.tpExportMultiExcels.TabIndex = 2;
			this.tpExportMultiExcels.Text = "Export Multi Excels (All In One)";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.BtnAddFile);
			this.panel3.Controls.Add(this.DtgFilePaths);
			this.panel3.Controls.Add(this.BtnAllInOne);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(690, 250);
			this.panel3.TabIndex = 26;
			// 
			// BtnAddFile
			// 
			this.BtnAddFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnAddFile.Location = new System.Drawing.Point(0, 3);
			this.BtnAddFile.Name = "BtnAddFile";
			this.BtnAddFile.Size = new System.Drawing.Size(72, 23);
			this.BtnAddFile.TabIndex = 1;
			this.BtnAddFile.Text = "Add File";
			this.BtnAddFile.UseVisualStyleBackColor = false;
			this.BtnAddFile.Click += new System.EventHandler(this.BtnAddFile_Click);
			// 
			// DtgFilePaths
			// 
			this.DtgFilePaths.AllowUserToAddRows = false;
			this.DtgFilePaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DtgFilePaths.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.DtgFilePaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DtgFilePaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.path,
            this.exportIds,
            this.exportConstants,
            this.status,
            this.BtnDelete});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DtgFilePaths.DefaultCellStyle = dataGridViewCellStyle3;
			this.DtgFilePaths.Location = new System.Drawing.Point(0, 29);
			this.DtgFilePaths.Margin = new System.Windows.Forms.Padding(0);
			this.DtgFilePaths.MultiSelect = false;
			this.DtgFilePaths.Name = "DtgFilePaths";
			this.DtgFilePaths.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DtgFilePaths.Size = new System.Drawing.Size(690, 221);
			this.DtgFilePaths.TabIndex = 0;
			this.DtgFilePaths.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgFilePaths_CellClick);
			this.DtgFilePaths.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgFilePaths_CellContentClick);
			this.DtgFilePaths.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgFilePaths_CellValueChanged);
			// 
			// path
			// 
			this.path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.path.DataPropertyName = "path";
			this.path.HeaderText = "File Path";
			this.path.Name = "path";
			this.path.ReadOnly = true;
			// 
			// exportIds
			// 
			this.exportIds.DataPropertyName = "exportIds";
			this.exportIds.HeaderText = "Export IDs";
			this.exportIds.Name = "exportIds";
			this.exportIds.Width = 60;
			// 
			// exportConstants
			// 
			this.exportConstants.DataPropertyName = "exportConstants";
			this.exportConstants.HeaderText = "Export Constants";
			this.exportConstants.Name = "exportConstants";
			// 
			// status
			// 
			this.status.HeaderText = "Status";
			this.status.Name = "status";
			this.status.ReadOnly = true;
			this.status.Width = 40;
			// 
			// BtnDelete
			// 
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.BtnDelete.DefaultCellStyle = dataGridViewCellStyle2;
			this.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnDelete.HeaderText = "Delete";
			this.BtnDelete.Name = "BtnDelete";
			this.BtnDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.BtnDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.BtnDelete.Text = "Delete";
			this.BtnDelete.ToolTipText = "Delete";
			this.BtnDelete.UseColumnTextForButtonValue = true;
			// 
			// BtnAllInOne
			// 
			this.BtnAllInOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnAllInOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnAllInOne.Location = new System.Drawing.Point(595, 3);
			this.BtnAllInOne.Name = "BtnAllInOne";
			this.BtnAllInOne.Size = new System.Drawing.Size(95, 23);
			this.BtnAllInOne.TabIndex = 3;
			this.BtnAllInOne.Text = "Export All";
			this.BtnAllInOne.UseVisualStyleBackColor = false;
			this.BtnAllInOne.Click += new System.EventHandler(this.BtnExportMultiExcels_Click);
			// 
			// txtLog2
			// 
			this.txtLog2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLog2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtLog2.Location = new System.Drawing.Point(3, 256);
			this.txtLog2.Multiline = true;
			this.txtLog2.Name = "txtLog2";
			this.txtLog2.ReadOnly = true;
			this.txtLog2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog2.Size = new System.Drawing.Size(690, 133);
			this.txtLog2.TabIndex = 24;
			// 
			// tpExportExcel
			// 
			this.tpExportExcel.Controls.Add(this.panel2);
			this.tpExportExcel.Controls.Add(this.txtLog);
			this.tpExportExcel.Location = new System.Drawing.Point(4, 22);
			this.tpExportExcel.Name = "tpExportExcel";
			this.tpExportExcel.Padding = new System.Windows.Forms.Padding(3);
			this.tpExportExcel.Size = new System.Drawing.Size(696, 392);
			this.tpExportExcel.TabIndex = 0;
			this.tpExportExcel.Text = "Export Excel";
			// 
			// panel2
			// 
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.Controls.Add(this.BtnExportAll);
			this.panel2.Controls.Add(this.BtnExportJson);
			this.panel2.Controls.Add(this.BtnReloadGrid);
			this.panel2.Controls.Add(this.btnExportLocalization);
			this.panel2.Controls.Add(this.BtnExportConstants);
			this.panel2.Controls.Add(this.BtnExportIds);
			this.panel2.Controls.Add(this.btnSelectFile);
			this.panel2.Controls.Add(this.DtgIDs);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.txtInputXLSXFilePath);
			this.panel2.Controls.Add(this.DtgSheets);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(690, 269);
			this.panel2.TabIndex = 27;
			// 
			// BtnExportAll
			// 
			this.BtnExportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExportAll.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportAll.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnExportAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportAll.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnExportAll.Location = new System.Drawing.Point(597, 37);
			this.BtnExportAll.Name = "BtnExportAll";
			this.BtnExportAll.Size = new System.Drawing.Size(90, 23);
			this.BtnExportAll.TabIndex = 21;
			this.BtnExportAll.Text = "Export All";
			this.BtnExportAll.UseVisualStyleBackColor = false;
			this.BtnExportAll.Click += new System.EventHandler(this.BtnExportAll_Click);
			// 
			// BtnExportJson
			// 
			this.BtnExportJson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExportJson.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportJson.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnExportJson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportJson.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnExportJson.Location = new System.Drawing.Point(597, 186);
			this.BtnExportJson.Name = "BtnExportJson";
			this.BtnExportJson.Size = new System.Drawing.Size(90, 23);
			this.BtnExportJson.TabIndex = 3;
			this.BtnExportJson.Text = "Export Json";
			this.BtnExportJson.UseVisualStyleBackColor = false;
			this.BtnExportJson.Click += new System.EventHandler(this.BtnExportJson_Click);
			// 
			// BtnReloadGrid
			// 
			this.BtnReloadGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnReloadGrid.BackColor = System.Drawing.SystemColors.Control;
			this.BtnReloadGrid.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnReloadGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnReloadGrid.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnReloadGrid.Location = new System.Drawing.Point(597, 215);
			this.BtnReloadGrid.Name = "BtnReloadGrid";
			this.BtnReloadGrid.Size = new System.Drawing.Size(90, 23);
			this.BtnReloadGrid.TabIndex = 14;
			this.BtnReloadGrid.Text = "Reload Sheets";
			this.BtnReloadGrid.UseVisualStyleBackColor = false;
			this.BtnReloadGrid.Click += new System.EventHandler(this.BtnReloadGrid_Click);
			// 
			// btnExportLocalization
			// 
			this.btnExportLocalization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExportLocalization.BackColor = System.Drawing.SystemColors.Control;
			this.btnExportLocalization.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnExportLocalization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExportLocalization.ForeColor = System.Drawing.SystemColors.WindowText;
			this.btnExportLocalization.Location = new System.Drawing.Point(597, 140);
			this.btnExportLocalization.Name = "btnExportLocalization";
			this.btnExportLocalization.Size = new System.Drawing.Size(90, 40);
			this.btnExportLocalization.TabIndex = 20;
			this.btnExportLocalization.Text = "Export Localization";
			this.btnExportLocalization.UseVisualStyleBackColor = false;
			this.btnExportLocalization.Click += new System.EventHandler(this.btnExportLocalization_Click);
			// 
			// BtnExportConstants
			// 
			this.BtnExportConstants.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExportConstants.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportConstants.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnExportConstants.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportConstants.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnExportConstants.Location = new System.Drawing.Point(597, 95);
			this.BtnExportConstants.Name = "BtnExportConstants";
			this.BtnExportConstants.Size = new System.Drawing.Size(90, 39);
			this.BtnExportConstants.TabIndex = 13;
			this.BtnExportConstants.Text = "Export Constants";
			this.BtnExportConstants.UseVisualStyleBackColor = false;
			this.BtnExportConstants.Click += new System.EventHandler(this.BtnExportConstants_Click);
			// 
			// BtnExportIds
			// 
			this.BtnExportIds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExportIds.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportIds.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnExportIds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportIds.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnExportIds.Location = new System.Drawing.Point(597, 66);
			this.BtnExportIds.Name = "BtnExportIds";
			this.BtnExportIds.Size = new System.Drawing.Size(90, 23);
			this.BtnExportIds.TabIndex = 12;
			this.BtnExportIds.Text = "Export IDs";
			this.BtnExportIds.UseVisualStyleBackColor = false;
			this.BtnExportIds.Click += new System.EventHandler(this.BtnExportIds_Click);
			// 
			// btnSelectFile
			// 
			this.btnSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectFile.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelectFile.Location = new System.Drawing.Point(597, 8);
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.Size = new System.Drawing.Size(90, 23);
			this.btnSelectFile.TabIndex = 0;
			this.btnSelectFile.Text = "Select File";
			this.btnSelectFile.UseVisualStyleBackColor = false;
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectInputFile_Click);
			// 
			// DtgIDs
			// 
			this.DtgIDs.AllowUserToAddRows = false;
			this.DtgIDs.AllowUserToDeleteRows = false;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.DtgIDs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.DtgIDs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DtgIDs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.DtgIDs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DtgIDs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Value});
			this.DtgIDs.Location = new System.Drawing.Point(259, 35);
			this.DtgIDs.Name = "DtgIDs";
			this.DtgIDs.ReadOnly = true;
			this.DtgIDs.Size = new System.Drawing.Size(332, 231);
			this.DtgIDs.TabIndex = 17;
			// 
			// ID
			// 
			this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ID.DataPropertyName = "Key";
			this.ID.HeaderText = "Key";
			this.ID.Name = "ID";
			this.ID.ReadOnly = true;
			this.ID.ToolTipText = "Key";
			// 
			// Value
			// 
			this.Value.DataPropertyName = "value";
			this.Value.HeaderText = "Value";
			this.Value.Name = "Value";
			this.Value.ReadOnly = true;
			this.Value.ToolTipText = "Value";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Excel File:";
			// 
			// txtInputXLSXFilePath
			// 
			this.txtInputXLSXFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtInputXLSXFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInputXLSXFilePath.Location = new System.Drawing.Point(61, 9);
			this.txtInputXLSXFilePath.Name = "txtInputXLSXFilePath";
			this.txtInputXLSXFilePath.Size = new System.Drawing.Size(530, 20);
			this.txtInputXLSXFilePath.TabIndex = 1;
			this.txtInputXLSXFilePath.TextChanged += new System.EventHandler(this.TxtInputFilePath_TextChanged);
			// 
			// DtgSheets
			// 
			this.DtgSheets.AllowUserToAddRows = false;
			this.DtgSheets.AllowUserToDeleteRows = false;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.DtgSheets.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
			this.DtgSheets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DtgSheets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.DtgSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DtgSheets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SheetName,
            this.Check});
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DtgSheets.DefaultCellStyle = dataGridViewCellStyle8;
			this.DtgSheets.Location = new System.Drawing.Point(0, 35);
			this.DtgSheets.Name = "DtgSheets";
			this.DtgSheets.Size = new System.Drawing.Size(253, 231);
			this.DtgSheets.TabIndex = 11;
			// 
			// SheetName
			// 
			this.SheetName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.SheetName.DataPropertyName = "SheetName";
			this.SheetName.HeaderText = "Sheet Name";
			this.SheetName.Name = "SheetName";
			this.SheetName.ReadOnly = true;
			// 
			// Check
			// 
			this.Check.DataPropertyName = "Check";
			this.Check.HeaderText = "Check";
			this.Check.Name = "Check";
			this.Check.Width = 50;
			// 
			// txtLog
			// 
			this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtLog.Location = new System.Drawing.Point(3, 275);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(690, 114);
			this.txtLog.TabIndex = 23;
			// 
			// tabMenu
			// 
			this.tabMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabMenu.Controls.Add(this.tpExportExcel);
			this.tabMenu.Controls.Add(this.tpExportMultiExcels);
			this.tabMenu.Controls.Add(this.tpSettings);
			this.tabMenu.Controls.Add(this.tpEncription);
			this.tabMenu.Controls.Add(this.tpChangeLog);
			this.tabMenu.Controls.Add(this.tpGoogleSpreadSheets);
			this.tabMenu.ItemSize = new System.Drawing.Size(124, 18);
			this.tabMenu.Location = new System.Drawing.Point(0, 33);
			this.tabMenu.Name = "tabMenu";
			this.tabMenu.SelectedIndex = 0;
			this.tabMenu.Size = new System.Drawing.Size(704, 418);
			this.tabMenu.TabIndex = 8;
			this.tabMenu.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tpGoogleSpreadSheets
			// 
			this.tpGoogleSpreadSheets.Controls.Add(this.TxtLogExportingGoogleSheets);
			this.tpGoogleSpreadSheets.Controls.Add(this.panel1);
			this.tpGoogleSpreadSheets.Location = new System.Drawing.Point(4, 22);
			this.tpGoogleSpreadSheets.Name = "tpGoogleSpreadSheets";
			this.tpGoogleSpreadSheets.Padding = new System.Windows.Forms.Padding(3);
			this.tpGoogleSpreadSheets.Size = new System.Drawing.Size(696, 392);
			this.tpGoogleSpreadSheets.TabIndex = 7;
			this.tpGoogleSpreadSheets.Text = "Google Spread Sheets";
			this.tpGoogleSpreadSheets.UseVisualStyleBackColor = true;
			// 
			// TxtLogExportingGoogleSheets
			// 
			this.TxtLogExportingGoogleSheets.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.TxtLogExportingGoogleSheets.Location = new System.Drawing.Point(3, 269);
			this.TxtLogExportingGoogleSheets.Multiline = true;
			this.TxtLogExportingGoogleSheets.Name = "TxtLogExportingGoogleSheets";
			this.TxtLogExportingGoogleSheets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtLogExportingGoogleSheets.Size = new System.Drawing.Size(690, 120);
			this.TxtLogExportingGoogleSheets.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.BtnExportGoogleSheets);
			this.panel1.Controls.Add(this.BtnAddGoogleSheet);
			this.panel1.Controls.Add(this.DtgGoogleSheets);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(690, 260);
			this.panel1.TabIndex = 1;
			// 
			// BtnExportGoogleSheets
			// 
			this.BtnExportGoogleSheets.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportGoogleSheets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportGoogleSheets.Location = new System.Drawing.Point(615, 3);
			this.BtnExportGoogleSheets.Name = "BtnExportGoogleSheets";
			this.BtnExportGoogleSheets.Size = new System.Drawing.Size(75, 23);
			this.BtnExportGoogleSheets.TabIndex = 2;
			this.BtnExportGoogleSheets.Text = "Export All";
			this.BtnExportGoogleSheets.UseVisualStyleBackColor = false;
			this.BtnExportGoogleSheets.Click += new System.EventHandler(this.BtnExportGoogleSheets_Click);
			// 
			// BtnAddGoogleSheet
			// 
			this.BtnAddGoogleSheet.BackColor = System.Drawing.SystemColors.Control;
			this.BtnAddGoogleSheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnAddGoogleSheet.Location = new System.Drawing.Point(0, 3);
			this.BtnAddGoogleSheet.Name = "BtnAddGoogleSheet";
			this.BtnAddGoogleSheet.Size = new System.Drawing.Size(107, 23);
			this.BtnAddGoogleSheet.TabIndex = 1;
			this.BtnAddGoogleSheet.Text = "Add Google Sheet";
			this.BtnAddGoogleSheet.UseVisualStyleBackColor = false;
			this.BtnAddGoogleSheet.Click += new System.EventHandler(this.BtnAddGoogleSheet_Click);
			// 
			// DtgGoogleSheets
			// 
			this.DtgGoogleSheets.AllowUserToAddRows = false;
			this.DtgGoogleSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DtgGoogleSheets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GoogleSheetName,
            this.GoogleSheetId,
            this.BtnEditGoogleSheet,
            this.BtnDeleteGoogleSheet});
			this.DtgGoogleSheets.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.DtgGoogleSheets.Location = new System.Drawing.Point(0, 32);
			this.DtgGoogleSheets.Name = "DtgGoogleSheets";
			this.DtgGoogleSheets.Size = new System.Drawing.Size(690, 228);
			this.DtgGoogleSheets.TabIndex = 0;
			this.DtgGoogleSheets.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgGoogleSheets_CellClick);
			// 
			// GoogleSheetName
			// 
			this.GoogleSheetName.DataPropertyName = "name";
			this.GoogleSheetName.HeaderText = "Name";
			this.GoogleSheetName.Name = "GoogleSheetName";
			this.GoogleSheetName.ReadOnly = true;
			// 
			// GoogleSheetId
			// 
			this.GoogleSheetId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.GoogleSheetId.DataPropertyName = "id";
			this.GoogleSheetId.HeaderText = "Id";
			this.GoogleSheetId.Name = "GoogleSheetId";
			this.GoogleSheetId.ReadOnly = true;
			// 
			// BtnEditGoogleSheet
			// 
			this.BtnEditGoogleSheet.HeaderText = "Edit";
			this.BtnEditGoogleSheet.Name = "BtnEditGoogleSheet";
			this.BtnEditGoogleSheet.ReadOnly = true;
			this.BtnEditGoogleSheet.Text = "Edit";
			this.BtnEditGoogleSheet.ToolTipText = "Edit";
			this.BtnEditGoogleSheet.UseColumnTextForButtonValue = true;
			this.BtnEditGoogleSheet.Width = 50;
			// 
			// BtnDeleteGoogleSheet
			// 
			this.BtnDeleteGoogleSheet.HeaderText = "Delete";
			this.BtnDeleteGoogleSheet.Name = "BtnDeleteGoogleSheet";
			this.BtnDeleteGoogleSheet.ReadOnly = true;
			this.BtnDeleteGoogleSheet.Text = "Delete";
			this.BtnDeleteGoogleSheet.ToolTipText = "Delete";
			this.BtnDeleteGoogleSheet.UseColumnTextForButtonValue = true;
			this.BtnDeleteGoogleSheet.Width = 50;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.BtnSaveSettings);
			this.flowLayoutPanel1.Controls.Add(this.BtnLoadSettings);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(393, 34);
			this.flowLayoutPanel1.TabIndex = 10;
			// 
			// BtnSaveSettings
			// 
			this.BtnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.BtnSaveSettings.Image = global::ExcelToUnity_DataConverter.Properties.Resources.floppy_disk__20x20_;
			this.BtnSaveSettings.Location = new System.Drawing.Point(2, 2);
			this.BtnSaveSettings.Margin = new System.Windows.Forms.Padding(0);
			this.BtnSaveSettings.Name = "BtnSaveSettings";
			this.BtnSaveSettings.Size = new System.Drawing.Size(30, 30);
			this.BtnSaveSettings.TabIndex = 0;
			this.BtnSaveSettings.UseVisualStyleBackColor = true;
			this.BtnSaveSettings.Click += new System.EventHandler(this.BtnSaveSettings_Click);
			// 
			// BtnLoadSettings
			// 
			this.BtnLoadSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.BtnLoadSettings.Image = global::ExcelToUnity_DataConverter.Properties.Resources.folder;
			this.BtnLoadSettings.Location = new System.Drawing.Point(32, 2);
			this.BtnLoadSettings.Margin = new System.Windows.Forms.Padding(0);
			this.BtnLoadSettings.Name = "BtnLoadSettings";
			this.BtnLoadSettings.Size = new System.Drawing.Size(30, 30);
			this.BtnLoadSettings.TabIndex = 1;
			this.BtnLoadSettings.UseVisualStyleBackColor = true;
			this.BtnLoadSettings.Click += new System.EventHandler(this.BtnLoadSettings_Click);
			// 
			// dataGridViewImageColumn1
			// 
			this.dataGridViewImageColumn1.HeaderText = "Status";
			this.dataGridViewImageColumn1.Image = global::ExcelToUnity_DataConverter.Properties.Resources.cancel;
			this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
			this.dataGridViewImageColumn1.ReadOnly = true;
			this.dataGridViewImageColumn1.Width = 40;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(704, 476);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.statusStrip2);
			this.Controls.Add(this.linkDownloadExample);
			this.Controls.Add(this.tabMenu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(720, 515);
			this.Name = "MainForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "SheetX - Sheets Exporter";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.statusStrip2.ResumeLayout(false);
			this.statusStrip2.PerformLayout();
			this.tpChangeLog.ResumeLayout(false);
			this.tpEncription.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.tpSettings.PerformLayout();
			this.tpExportMultiExcels.ResumeLayout(false);
			this.tpExportMultiExcels.PerformLayout();
			this.panel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DtgFilePaths)).EndInit();
			this.tpExportExcel.ResumeLayout(false);
			this.tpExportExcel.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtgIDs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DtgSheets)).EndInit();
			this.tabMenu.ResumeLayout(false);
			this.tpGoogleSpreadSheets.ResumeLayout(false);
			this.tpGoogleSpreadSheets.PerformLayout();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DtgGoogleSheets)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	    }
	    private System.Windows.Forms.Button BtnExportAll;
	    private System.Windows.Forms.DataGridViewTextBoxColumn ID;
	    private System.Windows.Forms.DataGridViewTextBoxColumn Value;

#endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLblCopyRight;
        private System.Windows.Forms.ToolStripStatusLabel txtVersion;
        private System.Windows.Forms.TabPage tpChangeLog;
        private System.Windows.Forms.TabPage tpEncription;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnDecrypt;
        private System.Windows.Forms.TextBox txtEncryptionOutput;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.CheckBox chkMergeJsonIntoSingleOne2;
        private System.Windows.Forms.Button btnOpenFolder2;
        private System.Windows.Forms.Button btnOpenFolder1;
        private System.Windows.Forms.CheckBox chkSeperateIDs;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSettingNamespace;
        private System.Windows.Forms.TextBox txtSettingExcludedSheet;
        private System.Windows.Forms.TextBox txtSettingEncryptionKey;
        private System.Windows.Forms.TextBox txtSettingOuputConstantsFilePath;
        private System.Windows.Forms.TextBox txtSettingOutputDataFilePath;
        private System.Windows.Forms.Label lblbExcluedSheets;
        private System.Windows.Forms.CheckBox chkSettingEnableEncryption;
        private System.Windows.Forms.Label lblEncryptionKey;
        private System.Windows.Forms.Button btnSelectFolder2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tpExportMultiExcels;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnAddFile;
        private System.Windows.Forms.DataGridView DtgFilePaths;
        private System.Windows.Forms.Button BtnAllInOne;
        private System.Windows.Forms.TextBox txtLog2;
        private System.Windows.Forms.TabPage tpExportExcel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.DataGridView DtgIDs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInputXLSXFilePath;
        private System.Windows.Forms.DataGridView DtgSheets;
        private System.Windows.Forms.Button BtnReloadGrid;
        private System.Windows.Forms.Button BtnExportJson;
        private System.Windows.Forms.Button BtnExportIds;
        private System.Windows.Forms.Button BtnExportConstants;
        private System.Windows.Forms.Button btnExportLocalization;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TabControl tabMenu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUnminimizeFields;
        private System.Windows.Forms.Button btnLoadDefaultSettings;
        private System.Windows.Forms.CheckBox chkKeepOnlyEnumAsIds;
        private System.Windows.Forms.TextBox txtEncryptionInput;
        private System.Windows.Forms.LinkLabel linkDownloadExample;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button BtnSaveSettings;
        private System.Windows.Forms.Button BtnLoadSettings;
        private System.Windows.Forms.CheckBox chkSeperateLocalization;
        private System.Windows.Forms.CheckBox chkSeperateConstants;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSettingOutputLocalizationFilePath;
        private System.Windows.Forms.Button btnOpenFolderLocalization;
        private System.Windows.Forms.Button btnSelectFolderLocalization;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtLanguageMaps;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.DataGridViewCheckBoxColumn exportIds;
        private System.Windows.Forms.DataGridViewCheckBoxColumn exportConstants;
        private System.Windows.Forms.DataGridViewImageColumn status;
        private System.Windows.Forms.DataGridViewButtonColumn BtnDelete;
		private System.Windows.Forms.WebBrowser weboxChangelog;
		private System.Windows.Forms.TabPage tpGoogleSpreadSheets;
		private System.Windows.Forms.TextBox TxtLogExportingGoogleSheets;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button BtnAddGoogleSheet;
		private System.Windows.Forms.DataGridView DtgGoogleSheets;
		private System.Windows.Forms.Button BtnExportGoogleSheets;
		private System.Windows.Forms.DataGridViewTextBoxColumn GoogleSheetName;
		private System.Windows.Forms.DataGridViewTextBoxColumn GoogleSheetId;
		private System.Windows.Forms.DataGridViewButtonColumn BtnEditGoogleSheet;
		private System.Windows.Forms.DataGridViewButtonColumn BtnDeleteGoogleSheet;
		private System.Windows.Forms.Label lblGoogleClientId;
		private System.Windows.Forms.TextBox TxtGoogleClientSecret;
		private System.Windows.Forms.TextBox TxtGoogleClientID;
		private System.Windows.Forms.Label lblGoogleClientSecret;
		private System.Windows.Forms.DataGridViewTextBoxColumn SheetName;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
	}
}

