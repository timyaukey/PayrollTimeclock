using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailTools
{
    public class EmailInfo
    {
        public string Subject;
        public EmailAddress From;
        public List<EmailAddress> To;
        public List<EmailAddress> Cc;
        public DateTime ReceiveDate;
        public string MessageId;
        public UInt32 UID;
        public string BodyText;
    }
}
