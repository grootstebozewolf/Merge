using System;
using System.Windows.Forms;

namespace UpdateUI
{
    public partial class ConnectDB : Form
    {
        public ConnectDB()
        {
            InitializeComponent();
        }

        public string ConnectionString
        {
            get { return txtConnectionString.Text; }
            set { txtConnectionString.Text = value; }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
