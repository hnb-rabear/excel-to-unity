using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelToUnity_DataConverter
{
    public partial class FrmSetupCredential : Form
    {
        public FrmSetupCredential()
        {
            InitializeComponent();
        }

        private void SetupCredential_Load(object sender, EventArgs e)
        {
            //txtCredential.Text = GGConfig.GetCredentialString();
        }

        private void btnImportCredential_Click(object sender, EventArgs e)
        {
            //if (GGConfig.ImportCredential())
            //    txtCredential.Text = GGConfig.GetCredentialString();
        }

        private void btnTestCredential_Click(object sender, EventArgs e)
        {
            //var sheets = GGConfig.DownloadSheet(txtSpreadSheetKey.Text);
            //if (sheets == null)
            //{
            //    MessageBox.Show("Could not get spread sheets!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else
            //{
            //    MessageBox.Show("Get user spread sheets successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
    }
}
