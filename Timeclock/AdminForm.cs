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
    public partial class AdminForm : Form
    {
        private bool _PrivilegedMode;

        public AdminForm(bool privilegedMode)
        {
            _PrivilegedMode = privilegedMode;
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            if (!_PrivilegedMode)
                this.Text = "Pay Period Reports";
            txtDataFolder.Text = PayrollStatic.DataFolder;
            foreach (PayrollPeriod period in PayrollStatic.Settings.RecentPeriods)
            {
                cboEndDate.Items.Add(period);
            }
            chkShowHiddenEmployees.Checked = PayrollStatic.ShowHiddenEmployees;
            lblDataFolder.Visible = _PrivilegedMode;
            txtDataFolder.Visible = _PrivilegedMode;
            btnSelectDataFolder.Visible = _PrivilegedMode;
            chkShowHiddenEmployees.Visible = _PrivilegedMode;
        }

        private void btnTimecardsCurrent_Click(object sender, EventArgs e)
        {
            ShowTimecardReport(PayrollStatic.Settings.CurrentPeriod);
        }

        private void btnTimecardsLast_Click(object sender, EventArgs e)
        {
            ShowTimecardReport(PayrollStatic.Settings.LastPeriod);
        }

        private void btnTimecardsSelected_Click(object sender, EventArgs e)
        {
            PayrollPeriod period = (PayrollPeriod)cboEndDate.SelectedItem;
            if (period == null)
            {
                MessageBox.Show("Please select period end date.");
                return;
            }
            ShowTimecardReport(period);
        }

        private void ShowTimecardReport(PayrollPeriod period)
        {
            TimecardReportForm reportFrm = new TimecardReportForm();
            reportFrm.Show(period, _PrivilegedMode);
        }

        private void btnSelectDataFolder_Click(object sender, EventArgs e)
        {
            dlgSelectFolder.SelectedPath = txtDataFolder.Text;
            DialogResult result = dlgSelectFolder.ShowDialog();
            if (result != DialogResult.OK)
                return;
            Properties.Settings.Default.DataFilePath = dlgSelectFolder.SelectedPath;
            Properties.Settings.Default.Save();
            txtDataFolder.Text = Properties.Settings.Default.DataFilePath;
            MessageBox.Show("The new data folder will take effect the next time you start the software.");
        }

        private void chkShowHiddenEmployees_CheckedChanged(object sender, EventArgs e)
        {
            PayrollStatic.ShowHiddenEmployees = chkShowHiddenEmployees.Checked;
        }
    }
}
