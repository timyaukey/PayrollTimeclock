using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MailTools;

namespace PayrollTimeclock
{
    public class PayrollMailBoxCache : FileMailBoxCache
    {
        public PayrollMailBoxCache(string mailBoxName, string localFolder)
            : base(mailBoxName, localFolder)
        {
        }

        public override void SaveMessage(EmailInfo message)
        {
            List<EmailAddress> recipientNames = new List<EmailAddress>();
            foreach (var toAddress in message.To)
            {
                recipientNames.Add(toAddress);
            }
            foreach (var ccAddress in message.Cc)
            {
                recipientNames.Add(ccAddress);
            }
            Message payrollMessage = new Message(message.ReceiveDate,
                message.Subject, message.From,
                recipientNames, message.BodyText);
            string pathStart = Path.Combine(LocalFolder, UIDFilePrefix + message.UID);
            string filePath = pathStart + Message.ReadExtension;
            if (!File.Exists(filePath))
            {
                filePath = pathStart + Message.UnreadExtension;
                if (!File.Exists(filePath))
                    payrollMessage.Save(filePath);
            }
        }
    }
}
