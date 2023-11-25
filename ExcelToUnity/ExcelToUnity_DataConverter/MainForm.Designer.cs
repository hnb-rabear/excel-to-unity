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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
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
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.txtChangesLog = new System.Windows.Forms.TextBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.BtnDecrypt = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.txtEncryptionOutput = new System.Windows.Forms.TextBox();
			this.txtEncryptionInput = new System.Windows.Forms.TextBox();
			this.tabSetUp = new System.Windows.Forms.TabPage();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.btnOpenFolderLocalization = new System.Windows.Forms.Button();
			this.btnSelectFolderLocalization = new System.Windows.Forms.Button();
			this.btnLoadDefaultSettings = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.btnOpenGoogleSheet = new System.Windows.Forms.Button();
			this.btnOpenFolder2 = new System.Windows.Forms.Button();
			this.btnOpenFolder1 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.txtSettingNamespace = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.btnSelectFolder2 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSelectFolder = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.panel3 = new System.Windows.Forms.Panel();
			this.BtnAddFile = new System.Windows.Forms.Button();
			this.DtgFilePaths = new System.Windows.Forms.DataGridView();
			this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.exportIds = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.exportConstants = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.BtnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.BtnAllInOne = new System.Windows.Forms.Button();
			this.txtLog2 = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btnConvertCSVToJson = new System.Windows.Forms.Button();
			this.txtInputCSVFilePath = new System.Windows.Forms.TextBox();
			this.txtOutputJsonFilePath = new System.Windows.Forms.TextBox();
			this.btnSelectFolder3 = new System.Windows.Forms.Button();
			this.btnSelectCSVFile = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.DtgIDs = new System.Windows.Forms.DataGridView();
			this.txtMegedJsonCustomName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkMergeJsonIntoSingleOne = new System.Windows.Forms.CheckBox();
			this.txtInputXLSXFilePath = new System.Windows.Forms.TextBox();
			this.DtgSheets = new System.Windows.Forms.DataGridView();
			this.chkEncrypt = new System.Windows.Forms.CheckBox();
			this.chkSeperateConstants1 = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BtnReloadGrid = new System.Windows.Forms.Button();
			this.BtnExportJson = new System.Windows.Forms.Button();
			this.BtnExportIds = new System.Windows.Forms.Button();
			this.BtnExportConstants = new System.Windows.Forms.Button();
			this.BtnExportSettings = new System.Windows.Forms.Button();
			this.btnExportLocalization = new System.Windows.Forms.Button();
			this.BtnTest = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.tabChangeLog = new System.Windows.Forms.TabControl();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.BtnSaveSettings = new System.Windows.Forms.Button();
			this.BtnLoadSettings = new System.Windows.Forms.Button();
			this.statusStrip2.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabSetUp.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtgFilePaths)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtgIDs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DtgSheets)).BeginInit();
			this.panel1.SuspendLayout();
			this.tabChangeLog.SuspendLayout();
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
			this.chkSeperateLocalization.AutoSize = true;
			this.chkSeperateLocalization.Location = new System.Drawing.Point(531, 85);
			this.chkSeperateLocalization.Name = "chkSeperateLocalization";
			this.chkSeperateLocalization.Size = new System.Drawing.Size(133, 17);
			this.chkSeperateLocalization.TabIndex = 40;
			this.chkSeperateLocalization.Text = "Separate Localizations";
			this.toolTip.SetToolTip(this.chkSeperateLocalization, "When this option is selected, the Localizations sheet with different name will be" +
        " exported individually");
			this.chkSeperateLocalization.UseVisualStyleBackColor = true;
			this.chkSeperateLocalization.CheckedChanged += new System.EventHandler(this.chkSeperateLocalization_CheckedChanged);
			// 
			// chkSeperateConstants
			// 
			this.chkSeperateConstants.AutoSize = true;
			this.chkSeperateConstants.Location = new System.Drawing.Point(406, 85);
			this.chkSeperateConstants.Name = "chkSeperateConstants";
			this.chkSeperateConstants.Size = new System.Drawing.Size(119, 17);
			this.chkSeperateConstants.TabIndex = 39;
			this.chkSeperateConstants.Text = "Separate Constants";
			this.toolTip.SetToolTip(this.chkSeperateConstants, "When this option is selected, the Constants sheet in each Excel file will be expo" +
        "rted individually");
			this.chkSeperateConstants.UseVisualStyleBackColor = true;
			this.chkSeperateConstants.CheckedChanged += new System.EventHandler(this.chkSeperateConstants_CheckedChanged);
			// 
			// txtUnminimizeFields
			// 
			this.txtUnminimizeFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUnminimizeFields.Location = new System.Drawing.Point(105, 319);
			this.txtUnminimizeFields.Multiline = true;
			this.txtUnminimizeFields.Name = "txtUnminimizeFields";
			this.txtUnminimizeFields.Size = new System.Drawing.Size(588, 36);
			this.txtUnminimizeFields.TabIndex = 34;
			this.txtUnminimizeFields.Text = "id; mode; type; group; level; rank";
			this.toolTip.SetToolTip(this.txtUnminimizeFields, "Type the name of the column in the json data sheet that you want to export even i" +
        "f it has no values.\r\nSeparate each field with a comma ‘;’");
			this.txtUnminimizeFields.TextChanged += new System.EventHandler(this.txtUnminimizeFields_TextChanged);
			this.txtUnminimizeFields.Leave += new System.EventHandler(this.txtUnminimizeFields_Leave);
			// 
			// chkMergeJsonIntoSingleOne2
			// 
			this.chkMergeJsonIntoSingleOne2.AutoSize = true;
			this.chkMergeJsonIntoSingleOne2.Location = new System.Drawing.Point(531, 109);
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
			this.chkSeperateIDs.AutoSize = true;
			this.chkSeperateIDs.Location = new System.Drawing.Point(312, 85);
			this.chkSeperateIDs.Name = "chkSeperateIDs";
			this.chkSeperateIDs.Size = new System.Drawing.Size(88, 17);
			this.chkSeperateIDs.TabIndex = 25;
			this.chkSeperateIDs.Text = "Separate IDs";
			this.toolTip.SetToolTip(this.chkSeperateIDs, "When this option is selected, the IDs sheet in each Excel file will be exported i" +
        "ndividually");
			this.chkSeperateIDs.UseVisualStyleBackColor = true;
			this.chkSeperateIDs.CheckedChanged += new System.EventHandler(this.chkSeperateIDs_CheckedChanged);
			// 
			// txtSettingExcludedSheet
			// 
			this.txtSettingExcludedSheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSettingExcludedSheet.Location = new System.Drawing.Point(105, 273);
			this.txtSettingExcludedSheet.Multiline = true;
			this.txtSettingExcludedSheet.Name = "txtSettingExcludedSheet";
			this.txtSettingExcludedSheet.Size = new System.Drawing.Size(588, 40);
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
			this.txtSettingOuputConstantsFilePath.TextChanged += new System.EventHandler(this.txtOuputConstantsFilePath_TextChanged);
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
			this.txtSettingEncryptionKey.Location = new System.Drawing.Point(105, 132);
			this.txtSettingEncryptionKey.Multiline = true;
			this.txtSettingEncryptionKey.Name = "txtSettingEncryptionKey";
			this.txtSettingEncryptionKey.Size = new System.Drawing.Size(588, 109);
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
			this.txtLanguageMaps.Location = new System.Drawing.Point(105, 247);
			this.txtLanguageMaps.Name = "txtLanguageMaps";
			this.txtLanguageMaps.Size = new System.Drawing.Size(588, 20);
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
			this.chkKeepOnlyEnumAsIds.AutoSize = true;
			this.chkKeepOnlyEnumAsIds.Location = new System.Drawing.Point(405, 109);
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
			this.chkSettingEnableEncryption.AutoSize = true;
			this.chkSettingEnableEncryption.Location = new System.Drawing.Point(312, 109);
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
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel3});
			this.statusStrip2.Location = new System.Drawing.Point(0, 456);
			this.statusStrip2.Name = "statusStrip2";
			this.statusStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.statusStrip2.Size = new System.Drawing.Size(704, 22);
			this.statusStrip2.TabIndex = 9;
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(37, 17);
			this.toolStripStatusLabel4.Text = "v1.4.5";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(245, 17);
			this.toolStripStatusLabel3.Text = "© (2018) RadBear, nbhung71711@gmail.com";
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.txtChangesLog);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(696, 394);
			this.tabPage6.TabIndex = 6;
			this.tabPage6.Text = "Changes Log";
			// 
			// txtChangesLog
			// 
			this.txtChangesLog.AllowDrop = true;
			this.txtChangesLog.BackColor = System.Drawing.SystemColors.HighlightText;
			this.txtChangesLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtChangesLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtChangesLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.txtChangesLog.Location = new System.Drawing.Point(3, 3);
			this.txtChangesLog.Multiline = true;
			this.txtChangesLog.Name = "txtChangesLog";
			this.txtChangesLog.ReadOnly = true;
			this.txtChangesLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtChangesLog.Size = new System.Drawing.Size(690, 388);
			this.txtChangesLog.TabIndex = 0;
			this.txtChangesLog.Text = resources.GetString("txtChangesLog.Text");
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.richTextBox1);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(696, 394);
			this.tabPage5.TabIndex = 5;
			this.tabPage5.Text = "Help";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(690, 388);
			this.richTextBox1.TabIndex = 23;
			this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
			// 
			// linkLabel1
			// 
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(585, 9);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.linkLabel1.Size = new System.Drawing.Size(107, 13);
			this.linkLabel1.TabIndex = 26;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Download Document";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.BtnDecrypt);
			this.tabPage4.Controls.Add(this.button1);
			this.tabPage4.Controls.Add(this.txtEncryptionOutput);
			this.tabPage4.Controls.Add(this.txtEncryptionInput);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(696, 394);
			this.tabPage4.TabIndex = 4;
			this.tabPage4.Text = "Encrypt & Decrypt Text";
			// 
			// BtnDecrypt
			// 
			this.BtnDecrypt.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.BtnDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnDecrypt.Location = new System.Drawing.Point(313, 141);
			this.BtnDecrypt.Name = "BtnDecrypt";
			this.BtnDecrypt.Size = new System.Drawing.Size(70, 28);
			this.BtnDecrypt.TabIndex = 2;
			this.BtnDecrypt.Text = "Decrypt";
			this.BtnDecrypt.UseVisualStyleBackColor = false;
			this.BtnDecrypt.Click += new System.EventHandler(this.BtnDecrypt_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(313, 107);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(70, 28);
			this.button1.TabIndex = 3;
			this.button1.Text = "Encrypt";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtEncryptionOutput
			// 
			this.txtEncryptionOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtEncryptionOutput.BackColor = System.Drawing.Color.White;
			this.txtEncryptionOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEncryptionOutput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.txtEncryptionOutput.Location = new System.Drawing.Point(386, 3);
			this.txtEncryptionOutput.Margin = new System.Windows.Forms.Padding(0);
			this.txtEncryptionOutput.Multiline = true;
			this.txtEncryptionOutput.Name = "txtEncryptionOutput";
			this.txtEncryptionOutput.ReadOnly = true;
			this.txtEncryptionOutput.Size = new System.Drawing.Size(307, 388);
			this.txtEncryptionOutput.TabIndex = 1;
			// 
			// txtEncryptionInput
			// 
			this.txtEncryptionInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.txtEncryptionInput.BackColor = System.Drawing.Color.White;
			this.txtEncryptionInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEncryptionInput.Location = new System.Drawing.Point(3, 3);
			this.txtEncryptionInput.Margin = new System.Windows.Forms.Padding(0);
			this.txtEncryptionInput.Multiline = true;
			this.txtEncryptionInput.Name = "txtEncryptionInput";
			this.txtEncryptionInput.Size = new System.Drawing.Size(307, 388);
			this.txtEncryptionInput.TabIndex = 0;
			// 
			// tabSetUp
			// 
			this.tabSetUp.Controls.Add(this.label11);
			this.tabSetUp.Controls.Add(this.txtLanguageMaps);
			this.tabSetUp.Controls.Add(this.label10);
			this.tabSetUp.Controls.Add(this.txtSettingOutputLocalizationFilePath);
			this.tabSetUp.Controls.Add(this.btnOpenFolderLocalization);
			this.tabSetUp.Controls.Add(this.btnSelectFolderLocalization);
			this.tabSetUp.Controls.Add(this.chkSeperateLocalization);
			this.tabSetUp.Controls.Add(this.chkSeperateConstants);
			this.tabSetUp.Controls.Add(this.chkKeepOnlyEnumAsIds);
			this.tabSetUp.Controls.Add(this.btnLoadDefaultSettings);
			this.tabSetUp.Controls.Add(this.label9);
			this.tabSetUp.Controls.Add(this.txtUnminimizeFields);
			this.tabSetUp.Controls.Add(this.btnOpenGoogleSheet);
			this.tabSetUp.Controls.Add(this.chkMergeJsonIntoSingleOne2);
			this.tabSetUp.Controls.Add(this.btnOpenFolder2);
			this.tabSetUp.Controls.Add(this.btnOpenFolder1);
			this.tabSetUp.Controls.Add(this.chkSeperateIDs);
			this.tabSetUp.Controls.Add(this.label8);
			this.tabSetUp.Controls.Add(this.txtSettingNamespace);
			this.tabSetUp.Controls.Add(this.txtSettingExcludedSheet);
			this.tabSetUp.Controls.Add(this.txtSettingEncryptionKey);
			this.tabSetUp.Controls.Add(this.txtSettingOuputConstantsFilePath);
			this.tabSetUp.Controls.Add(this.txtSettingOutputDataFilePath);
			this.tabSetUp.Controls.Add(this.label7);
			this.tabSetUp.Controls.Add(this.chkSettingEnableEncryption);
			this.tabSetUp.Controls.Add(this.label6);
			this.tabSetUp.Controls.Add(this.btnSelectFolder2);
			this.tabSetUp.Controls.Add(this.label3);
			this.tabSetUp.Controls.Add(this.btnSelectFolder);
			this.tabSetUp.Controls.Add(this.label2);
			this.tabSetUp.Location = new System.Drawing.Point(4, 22);
			this.tabSetUp.Name = "tabSetUp";
			this.tabSetUp.Padding = new System.Windows.Forms.Padding(3);
			this.tabSetUp.Size = new System.Drawing.Size(696, 394);
			this.tabSetUp.TabIndex = 3;
			this.tabSetUp.Text = "Settings";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 250);
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
			this.btnOpenFolderLocalization.Click += new System.EventHandler(this.btnOpenFolderLocalization_Click);
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
			this.btnSelectFolderLocalization.Click += new System.EventHandler(this.btnSelectFolderLocalization_Click);
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
			this.btnLoadDefaultSettings.Text = "Default Settings";
			this.btnLoadDefaultSettings.UseVisualStyleBackColor = false;
			this.btnLoadDefaultSettings.Click += new System.EventHandler(this.btnLoadDefaultSettings_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 322);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(93, 13);
			this.label9.TabIndex = 35;
			this.label9.Text = "Unminimized fields";
			// 
			// btnOpenGoogleSheet
			// 
			this.btnOpenGoogleSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenGoogleSheet.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnOpenGoogleSheet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnOpenGoogleSheet.Enabled = false;
			this.btnOpenGoogleSheet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOpenGoogleSheet.Location = new System.Drawing.Point(493, 361);
			this.btnOpenGoogleSheet.Name = "btnOpenGoogleSheet";
			this.btnOpenGoogleSheet.Size = new System.Drawing.Size(97, 25);
			this.btnOpenGoogleSheet.TabIndex = 33;
			this.btnOpenGoogleSheet.Text = "Test Google Sheet";
			this.btnOpenGoogleSheet.UseVisualStyleBackColor = false;
			this.btnOpenGoogleSheet.Click += new System.EventHandler(this.btnOpenGoogleSheet_Click);
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
			this.btnOpenFolder2.Click += new System.EventHandler(this.btnOpenFolder2_Click);
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
			this.btnOpenFolder1.Click += new System.EventHandler(this.btnOpenFolder1_Click);
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
			this.txtSettingNamespace.Size = new System.Drawing.Size(201, 20);
			this.txtSettingNamespace.TabIndex = 23;
			this.txtSettingNamespace.TextChanged += new System.EventHandler(this.txtSettingNamespace_TextChanged);
			this.txtSettingNamespace.Leave += new System.EventHandler(this.txtSettingNamespace_Leave);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 276);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(87, 13);
			this.label7.TabIndex = 22;
			this.label7.Text = "Excluded Sheets";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 135);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Encryption Key\r\n";
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
			this.btnSelectFolder2.Click += new System.EventHandler(this.btnSelectOuputConstantsFile_Click);
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
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.panel3);
			this.tabPage3.Controls.Add(this.txtLog2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(696, 394);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Convert Multi Files (All In One)";
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.BtnAddFile);
			this.panel3.Controls.Add(this.DtgFilePaths);
			this.panel3.Controls.Add(this.BtnAllInOne);
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(696, 250);
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
			this.DtgFilePaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DtgFilePaths.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
			this.DtgFilePaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DtgFilePaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.path,
            this.exportIds,
            this.exportConstants,
            this.BtnDelete});
			dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DtgFilePaths.DefaultCellStyle = dataGridViewCellStyle27;
			this.DtgFilePaths.Location = new System.Drawing.Point(0, 29);
			this.DtgFilePaths.Margin = new System.Windows.Forms.Padding(0);
			this.DtgFilePaths.MultiSelect = false;
			this.DtgFilePaths.Name = "DtgFilePaths";
			this.DtgFilePaths.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DtgFilePaths.Size = new System.Drawing.Size(696, 221);
			this.DtgFilePaths.TabIndex = 0;
			this.DtgFilePaths.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgFilePaths_CellClick);
			this.DtgFilePaths.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgFilePaths_CellValueChanged);
			// 
			// path
			// 
			this.path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.path.DataPropertyName = "path";
			this.path.HeaderText = "File Path";
			this.path.Name = "path";
			// 
			// exportIds
			// 
			this.exportIds.DataPropertyName = "exportIds";
			this.exportIds.HeaderText = "Export IDs";
			this.exportIds.Name = "exportIds";
			// 
			// exportConstants
			// 
			this.exportConstants.DataPropertyName = "exportConstants";
			this.exportConstants.HeaderText = "Export Constants";
			this.exportConstants.Name = "exportConstants";
			// 
			// BtnDelete
			// 
			dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.BtnDelete.DefaultCellStyle = dataGridViewCellStyle26;
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
			this.BtnAllInOne.Location = new System.Drawing.Point(601, 3);
			this.BtnAllInOne.Name = "BtnAllInOne";
			this.BtnAllInOne.Size = new System.Drawing.Size(95, 23);
			this.BtnAllInOne.TabIndex = 3;
			this.BtnAllInOne.Text = "Export All";
			this.BtnAllInOne.UseVisualStyleBackColor = false;
			this.BtnAllInOne.Click += new System.EventHandler(this.BtnAllInOne_Click);
			// 
			// txtLog2
			// 
			this.txtLog2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLog2.Location = new System.Drawing.Point(0, 253);
			this.txtLog2.Multiline = true;
			this.txtLog2.Name = "txtLog2";
			this.txtLog2.ReadOnly = true;
			this.txtLog2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog2.Size = new System.Drawing.Size(696, 141);
			this.txtLog2.TabIndex = 24;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btnConvertCSVToJson);
			this.tabPage2.Controls.Add(this.txtInputCSVFilePath);
			this.tabPage2.Controls.Add(this.txtOutputJsonFilePath);
			this.tabPage2.Controls.Add(this.btnSelectFolder3);
			this.tabPage2.Controls.Add(this.btnSelectCSVFile);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(696, 394);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Convert CSV To Json (Test)";
			// 
			// btnConvertCSVToJson
			// 
			this.btnConvertCSVToJson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConvertCSVToJson.Location = new System.Drawing.Point(471, 64);
			this.btnConvertCSVToJson.Name = "btnConvertCSVToJson";
			this.btnConvertCSVToJson.Size = new System.Drawing.Size(75, 23);
			this.btnConvertCSVToJson.TabIndex = 14;
			this.btnConvertCSVToJson.Text = "Convert";
			this.btnConvertCSVToJson.UseVisualStyleBackColor = false;
			// 
			// txtInputCSVFilePath
			// 
			this.txtInputCSVFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInputCSVFilePath.Location = new System.Drawing.Point(61, 10);
			this.txtInputCSVFilePath.Name = "txtInputCSVFilePath";
			this.txtInputCSVFilePath.ReadOnly = true;
			this.txtInputCSVFilePath.Size = new System.Drawing.Size(404, 20);
			this.txtInputCSVFilePath.TabIndex = 9;
			// 
			// txtOutputJsonFilePath
			// 
			this.txtOutputJsonFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOutputJsonFilePath.Location = new System.Drawing.Point(61, 36);
			this.txtOutputJsonFilePath.Name = "txtOutputJsonFilePath";
			this.txtOutputJsonFilePath.ReadOnly = true;
			this.txtOutputJsonFilePath.Size = new System.Drawing.Size(404, 20);
			this.txtOutputJsonFilePath.TabIndex = 11;
			// 
			// btnSelectFolder3
			// 
			this.btnSelectFolder3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelectFolder3.Location = new System.Drawing.Point(471, 35);
			this.btnSelectFolder3.Name = "btnSelectFolder3";
			this.btnSelectFolder3.Size = new System.Drawing.Size(75, 23);
			this.btnSelectFolder3.TabIndex = 13;
			this.btnSelectFolder3.Text = "Open";
			this.btnSelectFolder3.UseVisualStyleBackColor = false;
			// 
			// btnSelectCSVFile
			// 
			this.btnSelectCSVFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelectCSVFile.Location = new System.Drawing.Point(471, 9);
			this.btnSelectCSVFile.Name = "btnSelectCSVFile";
			this.btnSelectCSVFile.Size = new System.Drawing.Size(75, 23);
			this.btnSelectCSVFile.TabIndex = 8;
			this.btnSelectCSVFile.Text = "Select File";
			this.btnSelectCSVFile.UseVisualStyleBackColor = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Export To";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "CSV File";
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panel2);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Controls.Add(this.txtLog);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(696, 394);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Convert Single File";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.Controls.Add(this.btnSelectFile);
			this.panel2.Controls.Add(this.DtgIDs);
			this.panel2.Controls.Add(this.txtMegedJsonCustomName);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.chkMergeJsonIntoSingleOne);
			this.panel2.Controls.Add(this.txtInputXLSXFilePath);
			this.panel2.Controls.Add(this.DtgSheets);
			this.panel2.Controls.Add(this.chkEncrypt);
			this.panel2.Controls.Add(this.chkSeperateConstants1);
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(594, 277);
			this.panel2.TabIndex = 27;
			// 
			// btnSelectFile
			// 
			this.btnSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectFile.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelectFile.Location = new System.Drawing.Point(501, 8);
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
			dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.DtgIDs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle28;
			this.DtgIDs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DtgIDs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle29;
			this.DtgIDs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DtgIDs.Location = new System.Drawing.Point(259, 60);
			this.DtgIDs.Name = "DtgIDs";
			this.DtgIDs.ReadOnly = true;
			this.DtgIDs.Size = new System.Drawing.Size(332, 214);
			this.DtgIDs.TabIndex = 17;
			// 
			// txtMegedJsonCustomName
			// 
			this.txtMegedJsonCustomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMegedJsonCustomName.Location = new System.Drawing.Point(423, 35);
			this.txtMegedJsonCustomName.Name = "txtMegedJsonCustomName";
			this.txtMegedJsonCustomName.Size = new System.Drawing.Size(168, 20);
			this.txtMegedJsonCustomName.TabIndex = 25;
			this.txtMegedJsonCustomName.Text = "Custom Json File Name";
			this.txtMegedJsonCustomName.TextChanged += new System.EventHandler(this.txtMegedJsonCustomName_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Data File";
			// 
			// chkMergeJsonIntoSingleOne
			// 
			this.chkMergeJsonIntoSingleOne.AutoSize = true;
			this.chkMergeJsonIntoSingleOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chkMergeJsonIntoSingleOne.Location = new System.Drawing.Point(259, 37);
			this.chkMergeJsonIntoSingleOne.Name = "chkMergeJsonIntoSingleOne";
			this.chkMergeJsonIntoSingleOne.Size = new System.Drawing.Size(158, 17);
			this.chkMergeJsonIntoSingleOne.TabIndex = 24;
			this.chkMergeJsonIntoSingleOne.Text = "Merge Jsons into single Json";
			this.chkMergeJsonIntoSingleOne.UseVisualStyleBackColor = true;
			this.chkMergeJsonIntoSingleOne.CheckedChanged += new System.EventHandler(this.chkMergeJsonInSingleExcel_CheckedChanged);
			// 
			// txtInputXLSXFilePath
			// 
			this.txtInputXLSXFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtInputXLSXFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInputXLSXFilePath.Location = new System.Drawing.Point(66, 9);
			this.txtInputXLSXFilePath.Name = "txtInputXLSXFilePath";
			this.txtInputXLSXFilePath.Size = new System.Drawing.Size(429, 20);
			this.txtInputXLSXFilePath.TabIndex = 1;
			this.txtInputXLSXFilePath.TextChanged += new System.EventHandler(this.txtInputFilePath_TextChanged);
			// 
			// DtgSheets
			// 
			this.DtgSheets.AllowUserToAddRows = false;
			this.DtgSheets.AllowUserToDeleteRows = false;
			dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.DtgSheets.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle30;
			this.DtgSheets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DtgSheets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
			this.DtgSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
			dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DtgSheets.DefaultCellStyle = dataGridViewCellStyle32;
			this.DtgSheets.Location = new System.Drawing.Point(0, 60);
			this.DtgSheets.Name = "DtgSheets";
			this.DtgSheets.Size = new System.Drawing.Size(253, 214);
			this.DtgSheets.TabIndex = 11;
			// 
			// chkEncrypt
			// 
			this.chkEncrypt.AutoSize = true;
			this.chkEncrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chkEncrypt.Location = new System.Drawing.Point(125, 37);
			this.chkEncrypt.Name = "chkEncrypt";
			this.chkEncrypt.Size = new System.Drawing.Size(85, 17);
			this.chkEncrypt.TabIndex = 22;
			this.chkEncrypt.Text = "Encrypt Data";
			this.chkEncrypt.UseVisualStyleBackColor = true;
			this.chkEncrypt.CheckedChanged += new System.EventHandler(this.chkEncrypt_CheckedChanged);
			// 
			// chkSeperateConstants1
			// 
			this.chkSeperateConstants1.AutoSize = true;
			this.chkSeperateConstants1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chkSeperateConstants1.Location = new System.Drawing.Point(3, 37);
			this.chkSeperateConstants1.Name = "chkSeperateConstants1";
			this.chkSeperateConstants1.Size = new System.Drawing.Size(116, 17);
			this.chkSeperateConstants1.TabIndex = 21;
			this.chkSeperateConstants1.Text = "Seperate Constants";
			this.chkSeperateConstants1.UseVisualStyleBackColor = true;
			this.chkSeperateConstants1.CheckedChanged += new System.EventHandler(this.chkSeperateConstants1_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.BtnReloadGrid);
			this.panel1.Controls.Add(this.BtnExportJson);
			this.panel1.Controls.Add(this.BtnExportIds);
			this.panel1.Controls.Add(this.BtnExportConstants);
			this.panel1.Controls.Add(this.BtnExportSettings);
			this.panel1.Controls.Add(this.btnExportLocalization);
			this.panel1.Controls.Add(this.BtnTest);
			this.panel1.Location = new System.Drawing.Point(594, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(102, 277);
			this.panel1.TabIndex = 26;
			// 
			// BtnReloadGrid
			// 
			this.BtnReloadGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnReloadGrid.BackColor = System.Drawing.SystemColors.Control;
			this.BtnReloadGrid.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnReloadGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnReloadGrid.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnReloadGrid.Location = new System.Drawing.Point(7, 8);
			this.BtnReloadGrid.Name = "BtnReloadGrid";
			this.BtnReloadGrid.Size = new System.Drawing.Size(90, 23);
			this.BtnReloadGrid.TabIndex = 14;
			this.BtnReloadGrid.Text = "Reload Sheets";
			this.BtnReloadGrid.UseVisualStyleBackColor = false;
			this.BtnReloadGrid.Click += new System.EventHandler(this.BtnReloadGrid_Click);
			// 
			// BtnExportJson
			// 
			this.BtnExportJson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExportJson.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportJson.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnExportJson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportJson.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnExportJson.Location = new System.Drawing.Point(7, 37);
			this.BtnExportJson.Name = "BtnExportJson";
			this.BtnExportJson.Size = new System.Drawing.Size(90, 23);
			this.BtnExportJson.TabIndex = 3;
			this.BtnExportJson.Text = "Export Json";
			this.BtnExportJson.UseVisualStyleBackColor = false;
			this.BtnExportJson.Click += new System.EventHandler(this.BtnExportJson_Click);
			// 
			// BtnExportIds
			// 
			this.BtnExportIds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExportIds.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportIds.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnExportIds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportIds.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnExportIds.Location = new System.Drawing.Point(7, 66);
			this.BtnExportIds.Name = "BtnExportIds";
			this.BtnExportIds.Size = new System.Drawing.Size(90, 23);
			this.BtnExportIds.TabIndex = 12;
			this.BtnExportIds.Text = "Export IDs";
			this.BtnExportIds.UseVisualStyleBackColor = false;
			this.BtnExportIds.Click += new System.EventHandler(this.BtnExportIds_Click);
			// 
			// BtnExportConstants
			// 
			this.BtnExportConstants.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExportConstants.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportConstants.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnExportConstants.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportConstants.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnExportConstants.Location = new System.Drawing.Point(7, 95);
			this.BtnExportConstants.Name = "BtnExportConstants";
			this.BtnExportConstants.Size = new System.Drawing.Size(90, 39);
			this.BtnExportConstants.TabIndex = 13;
			this.BtnExportConstants.Text = "Export Constants";
			this.BtnExportConstants.UseVisualStyleBackColor = false;
			this.BtnExportConstants.Click += new System.EventHandler(this.BtnExportConstants_Click);
			// 
			// BtnExportSettings
			// 
			this.BtnExportSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExportSettings.BackColor = System.Drawing.SystemColors.Control;
			this.BtnExportSettings.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnExportSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportSettings.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnExportSettings.Location = new System.Drawing.Point(7, 140);
			this.BtnExportSettings.Name = "BtnExportSettings";
			this.BtnExportSettings.Size = new System.Drawing.Size(90, 37);
			this.BtnExportSettings.TabIndex = 15;
			this.BtnExportSettings.Text = "Export Settings";
			this.BtnExportSettings.UseVisualStyleBackColor = false;
			// 
			// btnExportLocalization
			// 
			this.btnExportLocalization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExportLocalization.BackColor = System.Drawing.SystemColors.Control;
			this.btnExportLocalization.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnExportLocalization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExportLocalization.ForeColor = System.Drawing.SystemColors.WindowText;
			this.btnExportLocalization.Location = new System.Drawing.Point(7, 183);
			this.btnExportLocalization.Name = "btnExportLocalization";
			this.btnExportLocalization.Size = new System.Drawing.Size(90, 40);
			this.btnExportLocalization.TabIndex = 20;
			this.btnExportLocalization.Text = "Export Localization";
			this.btnExportLocalization.UseVisualStyleBackColor = false;
			this.btnExportLocalization.Click += new System.EventHandler(this.btnExportLocalization_Click);
			// 
			// BtnTest
			// 
			this.BtnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnTest.BackColor = System.Drawing.SystemColors.Control;
			this.BtnTest.Cursor = System.Windows.Forms.Cursors.Default;
			this.BtnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTest.ForeColor = System.Drawing.SystemColors.WindowText;
			this.BtnTest.Location = new System.Drawing.Point(7, 229);
			this.BtnTest.Name = "BtnTest";
			this.BtnTest.Size = new System.Drawing.Size(90, 23);
			this.BtnTest.TabIndex = 16;
			this.BtnTest.Text = "TEST";
			this.BtnTest.UseVisualStyleBackColor = false;
			this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
			// 
			// txtLog
			// 
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLog.Location = new System.Drawing.Point(0, 280);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(696, 114);
			this.txtLog.TabIndex = 23;
			// 
			// tabChangeLog
			// 
			this.tabChangeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabChangeLog.Controls.Add(this.tabPage1);
			this.tabChangeLog.Controls.Add(this.tabPage2);
			this.tabChangeLog.Controls.Add(this.tabPage3);
			this.tabChangeLog.Controls.Add(this.tabSetUp);
			this.tabChangeLog.Controls.Add(this.tabPage4);
			this.tabChangeLog.Controls.Add(this.tabPage5);
			this.tabChangeLog.Controls.Add(this.tabPage6);
			this.tabChangeLog.ItemSize = new System.Drawing.Size(124, 18);
			this.tabChangeLog.Location = new System.Drawing.Point(0, 33);
			this.tabChangeLog.Name = "tabChangeLog";
			this.tabChangeLog.SelectedIndex = 0;
			this.tabChangeLog.Size = new System.Drawing.Size(704, 420);
			this.tabChangeLog.TabIndex = 8;
			this.tabChangeLog.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.BtnSaveSettings);
			this.flowLayoutPanel1.Controls.Add(this.BtnLoadSettings);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(582, 34);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(704, 478);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.statusStrip2);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.tabChangeLog);
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(720, 500);
			this.Name = "MainForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "Excel To Unity - Data Converter";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.statusStrip2.ResumeLayout(false);
			this.statusStrip2.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.tabPage5.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.tabSetUp.ResumeLayout(false);
			this.tabSetUp.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.panel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DtgFilePaths)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtgIDs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DtgSheets)).EndInit();
			this.panel1.ResumeLayout(false);
			this.tabChangeLog.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox txtChangesLog;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnDecrypt;
        private System.Windows.Forms.TextBox txtEncryptionOutput;
        private System.Windows.Forms.TabPage tabSetUp;
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSettingEnableEncryption;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSelectFolder2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnAddFile;
        private System.Windows.Forms.DataGridView DtgFilePaths;
        private System.Windows.Forms.Button BtnAllInOne;
        private System.Windows.Forms.TextBox txtLog2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnConvertCSVToJson;
        private System.Windows.Forms.TextBox txtInputCSVFilePath;
        private System.Windows.Forms.TextBox txtOutputJsonFilePath;
        private System.Windows.Forms.Button btnSelectFolder3;
        private System.Windows.Forms.Button btnSelectCSVFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.DataGridView DtgIDs;
        private System.Windows.Forms.TextBox txtMegedJsonCustomName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkMergeJsonIntoSingleOne;
        private System.Windows.Forms.TextBox txtInputXLSXFilePath;
        private System.Windows.Forms.DataGridView DtgSheets;
        private System.Windows.Forms.CheckBox chkEncrypt;
        private System.Windows.Forms.CheckBox chkSeperateConstants1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnReloadGrid;
        private System.Windows.Forms.Button BtnExportJson;
        private System.Windows.Forms.Button BtnExportIds;
        private System.Windows.Forms.Button BtnExportConstants;
        private System.Windows.Forms.Button BtnExportSettings;
        private System.Windows.Forms.Button btnExportLocalization;
        private System.Windows.Forms.Button BtnTest;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TabControl tabChangeLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.DataGridViewCheckBoxColumn exportIds;
        private System.Windows.Forms.DataGridViewCheckBoxColumn exportConstants;
        private System.Windows.Forms.DataGridViewButtonColumn BtnDelete;
        private System.Windows.Forms.Button btnOpenGoogleSheet;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUnminimizeFields;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnLoadDefaultSettings;
        private System.Windows.Forms.CheckBox chkKeepOnlyEnumAsIds;
        private System.Windows.Forms.TextBox txtEncryptionInput;
        private System.Windows.Forms.LinkLabel linkLabel1;
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
	}
}

