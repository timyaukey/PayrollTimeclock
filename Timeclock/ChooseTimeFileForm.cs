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
    public partial class ChooseTimeFileForm : Form
    {
        private string _ChosenName;
        private string _Folder;

        public ChooseTimeFileForm()
        {
            InitializeComponent();
            lblExplain.Text = lblExplain.Text.Replace("$$", Times.StdBareName);
        }

        public string ChooseFile(List<string> bareNames, string folder)
        {
            _Folder = folder;
            lstBareNames.Items.Clear();
            foreach(string bareName in bareNames)
            {
                lstBareNames.Items.Add(bareName);
            }
            _ChosenName = null;
            this.ShowDialog();
            return _ChosenName;
        }

        private void btnInspect_Click(object sender, EventArgs e)
        {
            if (lstBareNames.SelectedItem == null)
                return;
            _ChosenName = (string)lstBareNames.SelectedItem;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstBareNames.SelectedItem == null)
                return;
            string bareName = (string)lstBareNames.SelectedItem;
            if (bareName.Equals(Times.StdBareName, StringComparison.InvariantCultureIgnoreCase))
            {
                MessageBox.Show("You may not delete the main data file.");
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete the selected file?", "Confirm", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            System.IO.File.Delete(PayrollStatic.EmployeesFolder + "\\" +_Folder + "\\" + bareName);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
