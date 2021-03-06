﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using MailTools;

namespace PayrollTimeclock
{
    public partial class EmployeeTimesForm : Form
    {
        private Person _Employee;
        private Times _Times;
        private PayrollPeriod _Period;
        private bool _ReadOnly;

        public EmployeeTimesForm()
        {
            InitializeComponent();
        }

        public void Show(Person employee)
        {
            string chosenBareName;
            _Employee = employee;
            List<string> bareTimeNames = Times.BareNames(_Employee.FolderName);
            this.Text = "Employee Times For " + _Employee.FullName.GetValue;
            if (bareTimeNames.Count == 1)
            {
                chosenBareName = bareTimeNames[0];
            }
            else
            {
                using (ChooseTimeFileForm chooseFrm = new ChooseTimeFileForm())
                {
                    chosenBareName = chooseFrm.ChooseFile(bareTimeNames, _Employee.FolderName);
                    if (chosenBareName == null)
                        return;
                }
            }
            _ReadOnly = !chosenBareName.Equals(Times.StdBareName, StringComparison.InvariantCultureIgnoreCase);
            _Times = Times.Load(_Employee.FolderName, chosenBareName);
            _Period = PayrollStatic.Settings.CurrentPeriod;
            this.ShowDialog();
        }

        private void EmployeeTimesForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            ShowTimeCard();
            ShowMessages();
            CheckForUnreadMessages();
            EnableButtons(true);
        }

        private void EmployeeTimesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (_Times != null)
            //{
            //    _Times.SaveToFile();
            //}
        }

        private void EnableButtons(bool enabled)
        {
            btnClockNow.Enabled = enabled & !_ReadOnly;
            btnAddSpecific.Enabled = enabled & !_ReadOnly;
            btnAddAbsent.Enabled = enabled & !_ReadOnly;
            btnAddExtra.Enabled = enabled & !_ReadOnly;
            btnDeleteSpecific.Enabled = enabled & !_ReadOnly;
            btnCurrentPeriod.Enabled = enabled;
            btnLastPeriod.Enabled = enabled;
            btnReadMessage.Enabled = enabled & !_ReadOnly;
            btnSendMessage.Enabled = enabled & !_ReadOnly;
        }

        private void ShowTimeCard()
        {
            List<TimePair> timePairs;
            List<TimePair> absentPairs;
            List<TimePair> allPairs;
            double overtimeHours;
            _Times.Get(_Period, out timePairs, out overtimeHours, out absentPairs);
            allPairs = new List<TimePair>(timePairs);
            allPairs.AddRange(absentPairs);
            allPairs.Sort(delegate(TimePair t1, TimePair t2)
                { return t1.StartEvent.InOutDateTime.CompareTo(t2.StartEvent.InOutDateTime); }
                );
            lvwPairs.Items.Clear();
            double presentHours = 0;
            double absentHours = 0;
            double extraHours = 0;
            foreach (TimePair pair in allPairs)
            {
                string endTime = string.Empty;
                string hours = string.Empty;
                Color rowColor;
                string pairType = string.Empty;
                if (pair.IsOpen)
                    rowColor = Color.Pink;
                else
                {
                    endTime = FormatTimeOfDay(pair.EndEvent);
                    hours = pair.Length.TotalHours.ToString("N2");
                    rowColor = Color.White;
                    if (pair.IsAbsent)
                        pairType = " absent";
                    else if (pair.IsExtra)
                        pairType = " extra";
                    if (pair.IsMixed)
                    {
                        pairType = " mixed";
                        rowColor = Color.Red;
                    }
                }
                ListViewItem item = new ListViewItem(
                    new string[] {
                        pair.StartEvent.InOutDateTime.Date.ToString("MM/dd/yyyy"),
                        pair.StartEvent.InOutDateTime.DayOfWeek.ToString(),
                        FormatTimeOfDay(pair.StartEvent),
                        endTime,
                        hours + pairType
                    }
                    );
                item.BackColor = rowColor;
                lvwPairs.Items.Add(item);
                if (pair.IsAbsent)
                    absentHours += pair.Length.TotalHours;
                else
                    presentHours += pair.Length.TotalHours;
                if (pair.IsExtra)
                    extraHours += pair.Length.TotalHours;
            }
            lblPresentHours.Text = presentHours.ToString("N2") + " present hours";
            lblOvertimeHours.Text = overtimeHours.ToString("N2") + " overtime hours";
            lblAbsentHours.Text = "{" + absentHours.ToString("N2") + "} absent hours";
            lblExtraHours.Text = "[" + extraHours.ToString("N2") + "] extra hours";
            lblPeriodDates.Text = _Period.StartDate.ToString("MM/dd/yyyy") +
                " - " + _Period.EndDate.ToString("MM/dd/yyyy");
        }

        private string FormatTimeOfDay(ClockEvent clockEvent)
        {
            string result = clockEvent.InOutDateTime.ToString("hh:mmtt");
            if (clockEvent.Status == EventStatus.Overridden)
            {
                result = "<" + result + ">";
            }
            else if (clockEvent.Status == EventStatus.Absent)
            {
                result = "{" + result + "}";
            }
            else if (clockEvent.Status == EventStatus.Extra)
            {
                result = "[" + result + "]";
            }
            return result;
        }

        private void btnClockNow_Click(object sender, EventArgs e)
        {
            ClockEvent  newEvent = _Times.ClockInOut();
            _Times.SaveToFile(newEvent);
            ShowTimeCard();
            txtSpecificDateTime.Text = newEvent.InOutDateTime.ToString("MM/dd/yyyy hh:mmtt");
        }

        private void btnAddSpecific_Click(object sender, EventArgs e)
        {
            DateTime when;
            if (!TryParseDateTime(out when))
                return;
            ClockEvent newEvent = _Times.ClockInOut(when);
            _Times.SaveToFile(newEvent);
            ShowTimeCard();
        }

        private void btnAddAbsent_Click(object sender, EventArgs e)
        {
            DateTime when;
            if (!TryParseDateTime(out when))
                return;
            ClockEvent newEvent = _Times.ClockAbsent(when);
            _Times.SaveToFile(newEvent);
            ShowTimeCard();
        }

        private void btnAddExtra_Click(object sender, EventArgs e)
        {
            DateTime when;
            if (!TryParseDateTime(out when))
                return;
            ClockEvent newEvent = _Times.ClockExtra(when);
            _Times.SaveToFile(newEvent);
            ShowTimeCard();
        }

        private void btnDeleteSpecific_Click(object sender, EventArgs e)
        {
            DateTime when;
            ClockEvent newEvent;
            if (!TryParseDateTime(out when))
                return;
            if (!_Times.Delete(when, out newEvent))
            {
                MessageBox.Show("Could not find that date and time.");
                return;
            }
            _Times.SaveToFile(newEvent);
            ShowTimeCard();
        }

        private bool TryParseDateTime(out DateTime when)
        {
            if (!DateTime.TryParse(txtSpecificDateTime.Text, out when))
            {
                MessageBox.Show("Invalid date and time.");
                return false;
            }
            if (!_Period.ContainsDate(when))
            {
                MessageBox.Show("Date and time must be in the payroll period being displayed.");
                return false;
            }
            if (when.TimeOfDay.Ticks == 0)
            {
                MessageBox.Show("Date and time must include a time of day.");
                return false;
            }
            return true;
        }

        private void btnCurrentPeriod_Click(object sender, EventArgs e)
        {
            _Period = PayrollStatic.Settings.CurrentPeriod;
            ShowTimeCard();
            btnClockNow.Enabled = !_ReadOnly;
        }

        private void btnLastPeriod_Click(object sender, EventArgs e)
        {
            _Period = PayrollStatic.Settings.LastPeriod;
            ShowTimeCard();
            btnClockNow.Enabled = false;
        }

        private void ShowMessages()
        {
            lvwMessages.Items.Clear();
            string messagesFolder = PayrollStatic.EmployeesFolder + "\\" + _Employee.FolderName + "\\Messages";
            if (!Directory.Exists(messagesFolder))
                return;
            if (_Employee.UseIMAP4)
            {
                // Authentication errors throw an exception
                ShowProgress("Logging in to mail server...");
                try
                {
                    // Have to make new IMap4Repository every time we check,
                    // because IMAP server will close connection after a short period of inactivity.
                    IMap4Repository imapRep;
                    imapRep = new IMap4Repository(
                        _Employee.IMAP4Host.GetValue,
                        _Employee.IMAP4Port.GetValue,
                        _Employee.IMAP4SSL.GetValue,
                        _Employee.IMAP4Address.GetValue,
                        _Employee.IMAP4Password.GetValue);
                    FileMailBoxCache inboxCache = new PayrollMailBoxCache("inbox", messagesFolder);
                    MailBoxManager inboxManager = new MailBoxManager(imapRep, inboxCache);
                    inboxManager.ProgressEvent += ShowProgress;
                    inboxManager.Init();
                    inboxManager.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            ShowProgress("");
            List<Message> messages = new List<Message>();
            foreach (string fileName in Directory.GetFiles(messagesFolder))
            {
                bool isUnreadMessage = fileName.EndsWith(Message.UnreadExtension);
                bool isReadMessage = fileName.EndsWith(Message.ReadExtension);
                if (isReadMessage || isUnreadMessage)
                {
                    try
                    {
                        XmlDocument msgDoc = new XmlDocument();
                        msgDoc.Load(fileName);
                        Message message = new Message(msgDoc, fileName);
                        if (DateTime.Now.Subtract(message.SendDateTime).TotalDays < 30 || isUnreadMessage)
                        {
                            messages.Add(message);
                        }
                        else
                        {
                            File.Delete(fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading message file [" + fileName + "]: " + ex.Message);
                    }
                }
            }
            messages.Sort(
                delegate(Message msg1, Message msg2)
                {
                    return msg2.SendDateTime.CompareTo(msg1.SendDateTime);
                });
            foreach (Message message in messages)
            {
                ListViewItem msgItem = new ListViewItem(
                    new string[] {
                            message.SendDateTime.ToShortDateString(),
                            message.Subject,
                            message.Sender.ShortFormat
                        });
                msgItem.Tag = message;
                if (message.SourceFile.EndsWith(Message.UnreadExtension))
                {
                    msgItem.BackColor = Color.Pink;
                }
                lvwMessages.Items.Add(msgItem);
            }
        }

        private void CheckForUnreadMessages()
        {
            bool unreadFound = false;
            foreach (ListViewItem msgItem in lvwMessages.Items)
            {
                Message msg = (Message)msgItem.Tag;
                if (msg.IsUnread)
                    unreadFound = true;
            }
            if (unreadFound)
                MessageBox.Show("You have unread messages!", "Messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnReadMessage_Click(object sender, EventArgs e)
        {
            ReadSelectedMessage();
        }

        private void lvwMessages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ReadSelectedMessage();
        }

        private void ReadSelectedMessage()
        {
            if (lvwMessages.SelectedItems.Count <= 0)
            {
                MessageBox.Show("You must first click on a message to select it.");
                return;
            }
            ListViewItem selectedItem = lvwMessages.SelectedItems[0];
            Message msg = (Message)selectedItem.Tag;
            using (MessageForm frm = new MessageForm())
            {
                frm.Read(_Employee, msg);
            }
            EnableButtons(false);
            ShowMessages();
            EnableButtons(true);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            using (MessageForm frm = new MessageForm())
            {
                frm.Send(_Employee, new List<EmailAddress>(), string.Empty, string.Empty);
            }
            EnableButtons(false);
            ShowMessages();
            EnableButtons(true);
        }

        private void ShowProgress(string progressText)
        {
            lblProgess.Text = progressText;
            Application.DoEvents();
        }
    }
}
