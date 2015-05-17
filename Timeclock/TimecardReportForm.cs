using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PayrollTimeclock
{
    public partial class TimecardReportForm : Form
    {
        private PayrollPeriod _Period;

        private class ReportItem
        {
            public Person Employee;
            public Times Times;
            public double TotalHours;
            public double OvertimeHours;
        }

        public TimecardReportForm()
        {
            InitializeComponent();
        }

        public void Show(PayrollPeriod period)
        {
            _Period = period;
            ShowEmployees();
            ShowDialog();
        }

        private void ShowEmployees()
        {
            double grandTotalHours = 0.0;
            double grandOvertimeHours = 0.0;
            lblPeriod.Text = _Period.StartDate.ToString("MM/dd/yyyy") +
                " - " + _Period.EndDate.ToString("MM/dd/yyyy");
            lvwTimecards.Items.Clear();
            foreach (Person employee in PayrollStatic.People)
            {
                Times times = Times.Load(employee.FolderName);
                List<TimePair> timePairs;
                double totalHours = 0.0;
                double overtimeHours;
                bool hasOpenPairs = false;
                times.Get(_Period, out timePairs, out overtimeHours);
                foreach (TimePair pair in timePairs)
                {
                    if (pair.IsOpen)
                    {
                        hasOpenPairs = true;
                    }
                    else
                    {
                        totalHours += pair.Length.TotalHours;
                    }
                }
                grandTotalHours += totalHours;
                grandOvertimeHours += overtimeHours;
                ReportItem reportItem = new ReportItem();
                reportItem.Employee = employee;
                reportItem.Times = times;
                reportItem.TotalHours = totalHours;
                reportItem.OvertimeHours = overtimeHours;
                ListViewItem cardItem = new ListViewItem( new string[] {
                    employee.FullName.GetValue,
                    employee.ExternalID.GetValue,
                    totalHours.ToString("N2"),
                    (totalHours-overtimeHours).ToString("N2"),
                    overtimeHours.ToString("N2")
                });
                cardItem.Checked = (reportItem.TotalHours > 0.0);
                cardItem.Tag = reportItem;
                if (hasOpenPairs)
                    cardItem.BackColor = Color.Pink;
                lvwTimecards.Items.Add(cardItem);
            }
            lblTotals.Text = "Grand Total " + grandTotalHours.ToString("N2") +
                ", Regular Total " + (grandTotalHours - grandOvertimeHours).ToString("N2") +
                ", Overtime Total " + grandOvertimeHours.ToString("N2");
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (TextWriter writer = new StreamWriter(PayrollStatic.PeriodExportFile))
            {
                foreach (ListViewItem item in lvwTimecards.Items)
                {
                    if (item.Checked)
                    {
                        ReportItem reportItem = (ReportItem)item.Tag;
                        ExportHoursValue(writer, reportItem.Employee, "Reg", reportItem.TotalHours - reportItem.OvertimeHours);
                        if (reportItem.OvertimeHours > 0.0)
                        {
                            ExportHoursValue(writer, reportItem.Employee, "OT", reportItem.OvertimeHours);
                        }
                    }
                }
            }
            MessageBox.Show("Created period export file " + PayrollStatic.PeriodExportFile);
        }

        private void ExportHoursValue(TextWriter writer, Person employee, string code, double hours)
        {
            writer.WriteLine("{0},E,{1},{2:N2}", employee.ExternalID.GetValue, code, hours);
        }
    }
}
