using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PayrollTimeclock
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            this.Text = "Payroll Timeclock - " + Application.ProductVersion;

            string exeToData = "\\Data";
            if (Environment.CommandLine.Contains("/visualstudio"))
                exeToData = "\\..\\..\\Data";
            PayrollStatic.DataFolder = Path.GetDirectoryName(Application.ExecutablePath) + exeToData;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.DataFilePath))
            {
                PayrollStatic.DataFolder = Properties.Settings.Default.DataFilePath;
            }

            PayrollStatic.Settings = new Settings();
            string loadErrorMsg = PayrollStatic.Settings.LoadFromConfigFile();
            if (loadErrorMsg != null)
            {
                MessageBox.Show(loadErrorMsg);
                this.Close();
                return;
            }
            lnkWebSite.Text = PayrollStatic.Settings.WebSiteLabel;

            PayrollStatic.LoadPeople();
            LoadEmployeeList();

            timerBlogRefresh.Interval = 1000 * 60 * 10;     // every 10 minutes
            timerBlogRefresh.Enabled = true;
            LoadNews();
        }

        private void LoadEmployeeList()
        {
            lstEmployees.Items.Clear();
            lstEmployees.Refresh();
            foreach (Person person in PayrollStatic.People)
            {
                lstEmployees.Items.Add(person);
            }
        }

        private void btnClockInOut_Click(object sender, EventArgs e)
        {
            ClockInOut();
        }

        private void lstEmployees_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClockInOut();
        }

        private void ClockInOut()
        {
            PayrollStatic.Settings.ConfigureForDate();
            if (lstEmployees.SelectedItem == null)
            {
                MessageBox.Show("Please select an employee first.");
                return;
            }
            Person employee = (Person)lstEmployees.SelectedItem;
            using (EnterPINForm pinForm = new EnterPINForm())
            {
                if (pinForm.Show(employee))
                {
                    EmployeeTimesForm times = new EmployeeTimesForm();
                    times.Show(employee);
                }
                else
                {
                    MessageBox.Show("Employee PIN does not match.");
                }
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            PayrollStatic.Settings.ConfigureForDate();
            EnterAdminPwdForm passwordFrm = new EnterAdminPwdForm();
            if (!passwordFrm.PasswordMatches())
            {
                MessageBox.Show("Invalid administrator password.");
                return;
            }
            AdminForm adminFrm = new AdminForm(true);
            adminFrm.ShowDialog();
            PayrollStatic.LoadPeople();
            LoadEmployeeList();
        }

        private void btnPayPeriodSummary_Click(object sender, EventArgs e)
        {
            PayrollStatic.Settings.ConfigureForDate();
            AdminForm adminFrm = new AdminForm(false);
            adminFrm.ShowDialog();
        }

        private void lnkTheBook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(PayrollStatic.Settings.WebSiteURL);
        }

        private void timerBlogRefresh_Tick(object sender, EventArgs e)
        {
            webBrowser.Refresh(WebBrowserRefreshOption.Normal);
            LoadNews();
        }

        private void LoadNews()
        {
            List<NewsEntry> news = new List<NewsEntry>();
            foreach (NewsSource source in PayrollStatic.Settings.NewsSources)
            {
                NewsEntry.LoadURI(news, source.SourceAddress);
            }
            news.Sort(delegate(NewsEntry e1, NewsEntry e2)
                {
                    return e2.Updated.CompareTo(e1.Updated);
                });
            StringBuilder html = new StringBuilder();
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            BuildNewsStyles(html);
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            foreach (NewsEntry entry in news)
            {
                BuildNewsEntry(html, entry);
            }
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            webBrowser.Navigate("about:blank");
            webBrowser.Document.OpenNew(false);
            webBrowser.Document.Write(html.ToString());
            webBrowser.Refresh();

        }

        private static void BuildNewsStyles(StringBuilder html)
        {
            html.AppendLine("<style>");
            html.AppendLine("body,p { font-family:sans-serif; }");
            html.AppendLine("p { margin-top:0px; margin-bottom:0px; padding-top:0px; padding-bottom:0px; }");
            html.AppendLine(".TITLE { font-size: 14; font-weight: bold; margin-bottom: 0.3em; }");
            html.AppendLine(".CONTENT { font-size: 12px; margin-bottom:0.5em; }");
            html.AppendLine(".POSTEDBY { background-color: #E0E0E0; padding:2px; font-size:10px; margin-bottom:1.0em;");
            html.AppendLine("</style>");
        }

        private static void BuildNewsEntry(StringBuilder html, NewsEntry entry)
        {
            html.AppendLine("<div>");
            html.AppendLine("<h1 class='TITLE'>" + entry.Title.Data + "</h1>");
            html.AppendLine("<p class='CONTENT'>" + entry.Content.Data + "</p>");
            html.AppendLine("<p class='POSTEDBY'>Posted " + entry.Updated.ToShortDateString() + " " + entry.Updated.ToShortTimeString() + "</p>");
            html.AppendLine("</div>");
        }
    }
}
