using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using MailTools;

namespace PayrollTimeclock
{
    public class Message
    {
        private DateTime _SendDateTime = DateTime.MinValue;
        private string _Subject = "(no subject)";
        private EmailAddress _Sender;
        private List<EmailAddress> _Recipients;
        private string _Body = string.Empty;
        private string _SourceFile;

        public static string ReadExtension = ".read";
        public static string UnreadExtension = ".unread";

        public Message(XmlDocument doc, string sourceFile)
        {
            _SendDateTime = DateTime.Parse(doc.DocumentElement.SelectSingleNode("senddatetime").InnerText);
            _Subject = doc.DocumentElement.SelectSingleNode("subject").InnerText;
            _Sender = new EmailAddress(doc.DocumentElement.SelectSingleNode("sender").InnerText);
            _Body = doc.DocumentElement.SelectSingleNode("body").InnerText;
            _Recipients = new List<EmailAddress>();
            foreach (XmlNode recipNode in doc.DocumentElement.SelectNodes("recipient"))
            {
                _Recipients.Add(new EmailAddress(recipNode.InnerText));
            }
            _SourceFile = sourceFile;
        }

        public Message(DateTime sendDateTime, string subject, EmailAddress sender,
            IEnumerable<EmailAddress> recipients, string body)
        {
            _SendDateTime = sendDateTime;
            _Subject = subject;
            _Sender = sender;
            _Recipients = new List<EmailAddress>();
            foreach(EmailAddress recipient in recipients)
            {
                _Recipients.Add(recipient);
            }
            _Body = body;
        }

        public void Send()
        {
            try
            {
                SmtpClient smtp = new SmtpClient(PayrollStatic.Settings.SMTPServer, PayrollStatic.Settings.SMTPPort);
                ICredentialsByHost credentials = new NetworkCredential(PayrollStatic.Settings.SMTPUserName,
                    PayrollStatic.Settings.SMTPPassword);
                smtp.Credentials = credentials;
                bool isSentToSelf = false;
                using (MailMessage msg = CreateMailMessage(""))
                {
                    foreach (EmailAddress recipient in _Recipients)
                    {
                        if (recipient.IsSame(_Sender))
                            isSentToSelf = true;
                        msg.To.Add( new MailAddress(recipient.Address, recipient.Name));
                    }
                    smtp.Send(msg);
                }
                if (!isSentToSelf)
                {
                    using (MailMessage msg = CreateMailMessage("Sent:"))
                    {
                        msg.To.Add(new MailAddress(_Sender.Address, _Sender.Name));
                        smtp.Send(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception sending email: " + ex.Message);
            }
        }

        private MailMessage CreateMailMessage(string subjectPrefix)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(_Sender.Address, _Sender.Name);
            msg.Subject = subjectPrefix + Subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = Body;
            msg.BodyEncoding = Encoding.UTF8;
            return msg;
        }

        public void Save(string filePath)
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<message>");
            xml.AppendLine("  <subject>" + EncodeXmlText(_Subject) + "</subject>");
            xml.AppendLine("  <sender>" + EncodeXmlText(_Sender.PackedFormat) + "</sender>");
            foreach (EmailAddress recipient in _Recipients)
            {
                xml.AppendLine("  <recipient>" + EncodeXmlText(recipient.PackedFormat) + "</recipient>");
            }
            xml.AppendLine("  <senddatetime>" + _SendDateTime.ToShortDateString() + " " +
                _SendDateTime.ToShortTimeString() + "</senddatetime>");
            xml.AppendLine("  <body>" + EncodeXmlText(_Body) + "</body>");
            xml.AppendLine("</message>");

            using (TextWriter writer = new StreamWriter(filePath))
            {
                writer.Write(xml);
            }
        }

        private string EncodeXmlText(string input)
        {
            return input.Replace("&","&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public DateTime SendDateTime
        {
            get { return _SendDateTime; }
        }

        public string Subject
        {
            get { return _Subject; }
        }

        public EmailAddress Sender
        {
            get { return _Sender; }
        }

        public List<EmailAddress> Recipients
        {
            get { return _Recipients; }
        }

        public string Body
        {
            get { return _Body; }
        }

        public string SourceFile
        {
            get { return _SourceFile; }
        }

        public bool IsUnread
        {
            get { return _SourceFile.EndsWith(Message.UnreadExtension); }
        }
    }
}
