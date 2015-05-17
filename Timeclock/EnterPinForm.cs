using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PayrollTimeclock
{
    public partial class EnterPINForm : Form
    {
        private Person _Employee;
        private bool _PINMatches;

        public EnterPINForm()
        {
            InitializeComponent();
        }

        public bool Show(Person employee)
        {
            _Employee = employee;
            _PINMatches = false;
            lblFullName.Text = _Employee.FullName.GetValue;
            this.ShowDialog();
            return _PINMatches;
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            _PINMatches = (txtPIN.Text == _Employee.PIN.GetValue) ||
                (PayrollStatic.Settings.IsAdminPassword(txtPIN.Text));
            this.Close();
        }
    }
}
