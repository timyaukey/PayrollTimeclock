using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using MailTools;

namespace PayrollTimeclock
{
    public partial class MessageForm : Form
    {
        private Message _Msg;
        private Person _Employee;

        private enum FormMode
        {
            Read = 0,
            New = 1
        }
        private FormMode _FormMode;

        public MessageForm()
        {
            InitializeComponent();
        }

        private void MessageForm_Shown(object sender, EventArgs e)
        {
            txtBody.Focus();
            txtBody.SelectionLength = 0;
        }

        public void Read(Person employee, Message msg)
        {
            _Employee = employee;
            _Msg = msg;
            _FormMode = FormMode.Read;
            
            btnOkay.Text = "Mark Read";
            btnCancel.Text = "Mark Unread";
            txtSendDate.Text = _Msg.SendDateTime.ToShortDateString() + " " + _Msg.SendDateTime.ToShortTimeString();
            txtSubject.Text = _Msg.Subject;
            txtSender.Text = _Msg.Sender.ShortFormat;
            txtBody.Text = _Msg.Body;
            lstRecipients.Items.Clear();
            foreach (EmailAddress recipient in _Msg.Recipients)
            {
                lstRecipients.Items.Add(recipient);
            }

            btnSendToAll.Visible = false;
            btnReply.Visible = true;

            this.ShowDialog();
        }

        public void Send(Person employee, List<EmailAddress> recipients, string subject, string body)
        {
            _Employee = employee;
            _FormMode = FormMode.New;

            btnOkay.Text = "Send";

            txtSendDate.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            txtSendDate.ReadOnly = true;

            txtSender.Text = employee.FullName.GetValue;
            txtSender.ReadOnly = true;

            lstRecipients.Items.Clear();
            foreach (Person person in PayrollStatic.People)
            {
                lstRecipients.Items.Add(person.AddressObj);
            }

            txtSubject.Text = subject;
            txtBody.Text = body;
            foreach (EmailAddress recipient in recipients)
            {
                for (int index = 0; index < lstRecipients.Items.Count; index++)
                {
                    EmailAddress candidate = (EmailAddress)lstRecipients.Items[index];
                    if (recipient.IsSame(candidate))
                    {
                        lstRecipients.SetSelected(index, true);
                    }
                }
            }

            this.ShowDialog();
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            switch (_FormMode)
            {
                case FormMode.Read:
                    ChangeExtension(Message.ReadExtension);
                    this.Close();
                    break;
                case FormMode.New:
                    if (CreateMessage())
                        this.Close();
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (_FormMode)
            {
                case FormMode.Read:
                    ChangeExtension(Message.UnreadExtension);
                    this.Close();
                    break;
                case FormMode.New:
                    this.Close();
                    break;
            }
        }

        private void ChangeExtension(string newExt)
        {
            if (_Msg.SourceFile.EndsWith(newExt))
                return;
            string newName = _Msg.SourceFile.Substring(0, _Msg.SourceFile.LastIndexOf('.')) + newExt;
            if (_Msg.SourceFile != newName)
                System.IO.File.Move(_Msg.SourceFile, newName);
        }

        private bool CreateMessage()
        {
            DateTime sendDateTime = DateTime.Parse(txtSendDate.Text);
            string subject = txtSubject.Text.Trim();
            if (subject == string.Empty)
            {
                ShowValidationError("Subject is required.");
                return false;
            }
            if (lstRecipients.SelectedItems.Count == 0)
            {
                ShowValidationError("Select at least one recipient.");
                return false;
            }
            string body = txtBody.Text.Trim();
            if (body == string.Empty)
            {
                ShowValidationError("Message body is required.");
                return false;
            }
            List<EmailAddress> recipients = new List<EmailAddress>();
            foreach (EmailAddress recipient in lstRecipients.SelectedItems)
            {
                recipients.Add(recipient);
            }
            Message msg = new Message(sendDateTime, subject, _Employee.AddressObj, recipients, body);
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                msg.Send();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return true;
        }

        private void ShowValidationError(string errMsg)
        {
            MessageBox.Show(errMsg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSendToAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstRecipients.Items.Count; i++)
            {
                lstRecipients.SetSelected(i, true);
            }
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            List<EmailAddress> recipients = new List<EmailAddress>();
            bool foundSender = false;
            // Everyone who got the original gets the reply, except
            // the person who is replying.
            foreach (EmailAddress recipient in lstRecipients.Items)
            {
                if (recipient.IsSame(_Msg.Sender))
                    foundSender = true;
                if (!recipient.IsSame(_Employee.AddressObj))
                    recipients.Add(recipient);
            }
            // The sender of the original also gets it (of course),
            // but they may have included themselves in the initial
            // recipient list so we make sure we haven't already added them.
            if (!foundSender)
                recipients.Add(_Msg.Sender);
            using (MessageForm frm = new MessageForm())
            {
                frm.Send(_Employee, recipients, "Re:" + txtSubject.Text,
                    Environment.NewLine + Environment.NewLine + 
                    "-------- Sent By " + txtSender.Text + " On " + txtSendDate.Text + " --------" + 
                    Environment.NewLine + txtBody.Text);
            }
        }
    }
}
