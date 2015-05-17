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
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            txtDataFolder.Text = PayrollStatic.DataFolder;
            foreach (PayrollPeriod period in PayrollStatic.Settings.RecentPeriods)
            {
                cboEndDate.Items.Add(period);
            }
            chkShowHiddenEmployees.Checked = PayrollStatic.ShowHiddenEmployees;
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
            reportFrm.Show(period);
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
