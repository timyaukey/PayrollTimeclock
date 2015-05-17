using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PayrollTimeclock
{
    public partial class EnterAdminPwdForm : Form
    {
        private bool _PasswordMatches;

        public EnterAdminPwdForm()
        {
            InitializeComponent();
        }

        public bool PasswordMatches()
        {
            _PasswordMatches = false;
            this.ShowDialog();
            return _PasswordMatches;
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            _PasswordMatches = PayrollStatic.Settings.IsAdminPassword(txtPassword.Text);
            this.Close();
        }

        private void btnNewHash_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Enter the password to compute the hash for.");
                return;
            }
            string newHash = Settings.ComputePasswordHash(txtPassword.Text);
            Clipboard.Clear();
            Clipboard.SetText(newHash);
            MessageBox.Show("Hash for password [" + txtPassword.Text + "] has been saved on your clipboard.");
            this.Close();
        }
    }
}
