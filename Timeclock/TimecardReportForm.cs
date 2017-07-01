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
        private bool _PrivilegedMode;

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

        public void Show(PayrollPeriod period, bool privilegedMode)
        {
            _Period = period;
            _PrivilegedMode = privilegedMode;
            btnExport.Visible = _PrivilegedMode;
            btnClipboardExport.Visible = _PrivilegedMode;
            AddDayColumns();
            ShowEmployees();
            ShowDialog();
        }

        private void AddDayColumns()
        {
            DateTime day = _Period.StartDate;
            for (; day <= _Period.EndDate; day = day.AddDays(1.0))
            {
                string dow = day.ToString("ddd");
                lvwTimecards.Columns.Add(dow + " " + day.ToShortDateString(), 120, HorizontalAlignment.Right);
            }
        }

        private void ShowEmployees()
        {
            double grandPresentHours = 0.0;
            double grandOvertimeHours = 0.0;
            double grandAbsentHours = 0.0;
            double grandExtraHours = 0.0;
            int daysInPeriod = (int)_Period.EndDate.Subtract(_Period.StartDate).TotalDays + 1;
            double[] totalPresentByDay = new double[daysInPeriod];
            double[] totalAbsentByDay = new double[daysInPeriod];
            double[] totalExtraByDay = new double[daysInPeriod];
            lblPeriod.Text = _Period.StartDate.ToString("MM/dd/yyyy") +
                " - " + _Period.EndDate.ToString("MM/dd/yyyy");
            lvwTimecards.Items.Clear();
            foreach (Person employee in PayrollStatic.People)
            {
                Times times = Times.Load(employee.FolderName, Times.StdBareName);
                List<TimePair> timePairs;
                List<TimePair> absentPairs;
                double presentHours = 0.0;
                double absentHours = 0.0;
                double extraHours = 0.0;
                double overtimeHours;
                bool hasOpenPairs = false;
                bool hasMixedPairs = false;
                double[] presentByDay = new double[daysInPeriod];
                double[] absentByDay = new double[daysInPeriod];
                double[] extraByDay = new double[daysInPeriod];
                times.Get(_Period, out timePairs, out overtimeHours, out absentPairs);
                foreach (TimePair pair in timePairs)
                {
                    if (pair.IsOpen)
                    {
                        hasOpenPairs = true;
                    }
                    else
                    {
                        presentHours += pair.Length.TotalHours;
                        int dayOffset = (int)pair.StartEvent.InOutDateTime.Subtract(_Period.StartDate).TotalDays;
                        presentByDay[dayOffset] += pair.Length.TotalHours;
                        totalPresentByDay[dayOffset] += pair.Length.TotalHours;
                        if (pair.IsExtra)
                        {
                            extraHours += pair.Length.TotalHours;
                            extraByDay[dayOffset] += pair.Length.TotalHours;
                            totalExtraByDay[dayOffset] += pair.Length.TotalHours;
                        }
                        if (pair.IsMixed)
                            hasMixedPairs = true;
                    }
                }
                foreach (TimePair pair in absentPairs)
                {
                    if (pair.IsOpen)
                    {
                        hasOpenPairs = true;
                    }
                    else
                    {
                        absentHours += pair.Length.TotalHours;
                        int dayOffset = (int)pair.StartEvent.InOutDateTime.Subtract(_Period.StartDate).TotalDays;
                        absentByDay[dayOffset] += pair.Length.TotalHours;
                        totalAbsentByDay[dayOffset] += pair.Length.TotalHours;
                    }
                }
                grandPresentHours += presentHours;
                grandOvertimeHours += overtimeHours;
                grandAbsentHours += absentHours;
                grandExtraHours += extraHours;
                ReportItem reportItem = new ReportItem();
                reportItem.Employee = employee;
                reportItem.Times = times;
                reportItem.TotalHours = presentHours;
                reportItem.OvertimeHours = overtimeHours;
                List<string> columnValues = new List<string>();
                columnValues.Add(employee.FullName.GetValue);
                columnValues.Add(employee.ExternalID.GetValue);
                columnValues.Add(presentHours.ToString("N2"));
                columnValues.Add((presentHours-overtimeHours).ToString("N2"));
                columnValues.Add(overtimeHours.ToString("N2"));
                columnValues.Add(string.Empty);
                columnValues.Add("{" + absentHours.ToString("N2") + "}");
                columnValues.Add("[" + extraHours.ToString("N2") + "]");
                for (int i = 0; i < daysInPeriod; i++)
                {
                    string dayValue = presentByDay[i].ToString("N2");
                    if (absentByDay[i] > 0.0)
                        dayValue += " {" + absentByDay[i].ToString("N2") + "}";
                    if (extraByDay[i] > 0.0)
                        dayValue += " [" + extraByDay[i].ToString("N2") + "]";
                    columnValues.Add(dayValue);
                }
                ListViewItem cardItem = new ListViewItem(columnValues.ToArray());
                cardItem.Checked = (reportItem.TotalHours > 0.0);
                cardItem.Tag = reportItem;
                if (hasOpenPairs || hasMixedPairs)
                    cardItem.BackColor = Color.Pink;
                lvwTimecards.Items.Add(cardItem);
            }
            List<string> totalsValues = new List<string>(new string[] { "Totals", "", "", "", "", "", "", "" });
            for (int i = 0; i < daysInPeriod; i++)
            {
                string dayValue = totalPresentByDay[i].ToString("N2");
                if (totalAbsentByDay[i] > 0.0)
                    dayValue += " {" + totalAbsentByDay[i].ToString("N2") + "}";
                if (totalExtraByDay[i] > 0.0)
                    dayValue += " [" + totalExtraByDay[i].ToString("N2") + "]";
                totalsValues.Add(dayValue);
            }
            ListViewItem totalsItem = new ListViewItem(totalsValues.ToArray());
            lvwTimecards.Items.Add(totalsItem);
            lblTotals.Text = "Present Total " + grandPresentHours.ToString("N2") +
                ", Regular Total " + (grandPresentHours - grandOvertimeHours).ToString("N2") +
                ", Overtime Total " + grandOvertimeHours.ToString("N2") +
                ", Absent Total {" + grandAbsentHours.ToString("N2") + "}" +
                ", Extra Total [" + grandExtraHours.ToString("N2") + "]";
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
                        if (reportItem != null)
                        {
                            ExportHoursValue(writer, reportItem.Employee, "Reg", reportItem.TotalHours - reportItem.OvertimeHours);
                            if (reportItem.OvertimeHours > 0.0)
                            {
                                ExportHoursValue(writer, reportItem.Employee, "OT", reportItem.OvertimeHours);
                            }
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

        private void btnClipboardExport_Click(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();
            foreach (ListViewItem item in lvwTimecards.Items)
            {
                if (item.Checked)
                {
                    ReportItem reportItem = (ReportItem)item.Tag;
                    if (reportItem != null)
                    {
                        decimal regularHours = (decimal)reportItem.TotalHours - (decimal)reportItem.OvertimeHours;
                        decimal overtimeHours = (decimal)reportItem.OvertimeHours;
                        decimal sickHours = 0.00M;
                        string line = reportItem.Employee.FullName.GetValue + "\t" + regularHours.ToString("F2") +
                            "\t" + overtimeHours.ToString("F2") +
                            "\t" + sickHours.ToString("F2");
                        output.AppendLine(line);
                    }
                }
            }
            System.Windows.Forms.Clipboard.Clear();
            System.Windows.Forms.Clipboard.SetText(output.ToString());
            MessageBox.Show("Payroll information saved to clipboard.");
        }
    }
}
