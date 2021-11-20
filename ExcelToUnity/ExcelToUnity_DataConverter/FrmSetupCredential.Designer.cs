namespace ExcelToUnity_DataConverter
{
    partial class FrmSetupCredential
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
            this.btnImportCredential = new System.Windows.Forms.Button();
            this.txtCredential = new System.Windows.Forms.TextBox();
            this.btnTestCredential = new System.Windows.Forms.Button();
            this.txtSpreadSheetKey = new System.Windows.Forms.TextBox();
            this.txtSheetName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnImportCredential
            // 
            this.btnImportCredential.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnImportCredential.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportCredential.Location = new System.Drawing.Point(85, 188);
            this.btnImportCredential.Name = "btnImportCredential";
            this.btnImportCredential.Size = new System.Drawing.Size(104, 23);
            this.btnImportCredential.TabIndex = 1;
            this.btnImportCredential.Text = "Import Credential";
            this.btnImportCredential.UseVisualStyleBackColor = false;
            this.btnImportCredential.Click += new System.EventHandler(this.btnImportCredential_Click);
            // 
            // txtCredential
            // 
            this.txtCredential.Location = new System.Drawing.Point(12, 12);
            this.txtCredential.Multiline = true;
            this.txtCredential.Name = "txtCredential";
            this.txtCredential.ReadOnly = true;
            this.txtCredential.Size = new System.Drawing.Size(360, 119);
            this.txtCredential.TabIndex = 2;
            // 
            // btnTestCredential
            // 
            this.btnTestCredential.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnTestCredential.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestCredential.Location = new System.Drawing.Point(195, 188);
            this.btnTestCredential.Name = "btnTestCredential";
            this.btnTestCredential.Size = new System.Drawing.Size(104, 23);
            this.btnTestCredential.TabIndex = 3;
            this.btnTestCredential.Text = "Test";
            this.btnTestCredential.UseVisualStyleBackColor = false;
            this.btnTestCredential.Click += new System.EventHandler(this.btnTestCredential_Click);
            // 
            // txtSpreadSheetKey
            // 
            this.txtSpreadSheetKey.Location = new System.Drawing.Point(112, 137);
            this.txtSpreadSheetKey.Name = "txtSpreadSheetKey";
            this.txtSpreadSheetKey.Size = new System.Drawing.Size(260, 20);
            this.txtSpreadSheetKey.TabIndex = 4;
            // 
            // txtSheetName
            // 
            this.txtSheetName.Location = new System.Drawing.Point(112, 163);
            this.txtSheetName.Name = "txtSheetName";
            this.txtSheetName.Size = new System.Drawing.Size(260, 20);
            this.txtSheetName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Spread Sheet Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Sheet Name";
            // 
            // FrmSetupCredential
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 223);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSheetName);
            this.Controls.Add(this.txtSpreadSheetKey);
            this.Controls.Add(this.btnTestCredential);
            this.Controls.Add(this.txtCredential);
            this.Controls.Add(this.btnImportCredential);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSetupCredential";
            this.Text = "Setup Credential";
            this.Load += new System.EventHandler(this.SetupCredential_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnImportCredential;
        private System.Windows.Forms.TextBox txtCredential;
        private System.Windows.Forms.Button btnTestCredential;
        private System.Windows.Forms.TextBox txtSpreadSheetKey;
        private System.Windows.Forms.TextBox txtSheetName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}