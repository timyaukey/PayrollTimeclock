using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ActiveUp.Net.Mail;

namespace MailTools
{
    public delegate void MailBoxProgressHandler(string progessText);

    public class MailBoxManager
    {
        private IMap4Repository _Rep;
        private IMailBoxCache _Cache;
        private Mailbox _MailBox;
        private Fetch _Fetch;
        private UInt32 _UIDValidity;

        public event MailBoxProgressHandler ProgressEvent;

        public MailBoxManager(IMap4Repository rep, IMailBoxCache cache)
        {
            _Rep = rep;
            _Cache = cache;
        }

        public void Init()
        {
            _MailBox = Client.SelectMailbox(_Cache.MailBoxName);
            _Fetch = _MailBox.Fetch;
            _UIDValidity = _MailBox.UidValidity;
        }

        public void Refresh()
        {
            _Cache.LoadUIDState();
            //TO DO: What is the failure mode of this code?
            // If the cache is still valid.
            if (_Cache.UIDStateKnown && _Cache.UIDValidity == _UIDValidity)
            {
                // Message UID's have not changed on the server, so the cache is valid.
                // Get any new messages. Note that IMAP will swap the order of the UID's
                // in the query if the first is greater than the second, so this query will
                // return the last UID already read if there are no new ones, so we
                // check for this case before fetching messages.
                string filter = "UID " + (_Cache.HighestUIDRead + 1) + ":*";
                List<UInt32> newUIDs = Search(filter);
                UInt32 highestNewUIDRead = 0;
                if (newUIDs.Count > 0)
                {
                    if (newUIDs.Count > 1 || newUIDs[0] != _Cache.HighestUIDRead)
                    {
                        SortUIDsOldToNew(newUIDs);
                        highestNewUIDRead = GetMessages(newUIDs);
                    }
                }
                // Check to make sure all the cached old messages still exist on server.
                filter = "UID " + _Cache.LowestUIDRead + ":" + _Cache.HighestUIDRead;
                List<UInt32> existingUIDs = Search(filter);
                _Cache.PurgeExcept(existingUIDs, _Cache.HighestUIDRead);
                if (highestNewUIDRead > 0)
                    _Cache.HighestUIDRead = highestNewUIDRead;
                _Cache.SaveUIDState();
            }
            else
            {
                // The cache is not valid, so start over.
                _Cache.ClearEverything();
                DateTime sinceDate = DateTime.Today.AddDays(-14.0d);
                string filter = "SINCE " + sinceDate.ToString("d-MMM-yyyy");
                List<UInt32> uids = Search(filter);
                if (uids.Count > 0)
                {
                    SortUIDsNewToOld(uids);
                    UInt32 highestUIDRead = GetMessages(uids);
                    _Cache.UIDValidity = _UIDValidity;
                    _Cache.HighestUIDRead = highestUIDRead;
                    _Cache.LowestUIDRead = uids[uids.Count - 1];
                    _Cache.SaveUIDState();
                }
            }
        }

        private List<UInt32> Search(string filter)
        {
            string searchResult = _MailBox.SourceClient.Command("uid search " + filter);
            searchResult = searchResult.Substring(0, searchResult.IndexOf("\r\n"));
            string[] uidsTemp = searchResult.Split(' ');
            List<UInt32> uids = new List<UInt32>();
            for (int uidsTempIdx = 2; uidsTempIdx < uidsTemp.Length; uidsTempIdx++)
            {
                uids.Add(UInt32.Parse(uidsTemp[uidsTempIdx]));
            }
            return uids;
        }

        private UInt32 GetMessages(List<UInt32> uids)
        {
            int msgCount = 0;
            UInt32 highestUIDRead = 0;
            for (int idx = 0; idx < uids.Count; idx++)
            {
                msgCount++;
                if (msgCount > 20)   //dbg
                    break;
                UInt32 uid = uids[idx];
                Header hdr = _Fetch.UidHeaderObject(uid);
                if (ProgressEvent != null)
                    ProgressEvent("Reading - " + hdr.Subject);
                string subject = hdr.Subject;
                string body;

                ActiveUp.Net.Mail.Message imapMsg = _Fetch.UidMessageObject(uid);
                body = imapMsg.BodyText.TextStripped;
                //body = _Fetch.UidText(uid);

                // Hack because extra text of the form ")\r\n* nn FETCH...." comes back the first time
                // a message is read from the server.
                int hackIndex = body.IndexOf(")\r\n*");
                if (hackIndex > 0)
                {
                    if (body.IndexOf("FETCH", hackIndex) > 0)
                        body = body.Substring(0, hackIndex);
                }
                while (body.EndsWith("\r\n"))
                {
                    body = body.Substring(0, body.Length - 2);
                }

                EmailInfo msg = new EmailInfo();
                msg.Subject = hdr.Subject;
                msg.UID = uid;
                Address from = hdr.From;
                msg.From = new EmailAddress(from);
                
                msg.To = new List<EmailAddress>();
                foreach (Address inputTo in hdr.To)
                {
                    EmailAddress outputTo = new EmailAddress(inputTo);
                    msg.To.Add(outputTo);
                }
                
                msg.Cc = new List<EmailAddress>();
                foreach (Address inputCc in hdr.Cc)
                {
                    EmailAddress outputCc = new EmailAddress(inputCc);
                    msg.Cc.Add(outputCc);
                }
                
                msg.ReceiveDate = hdr.Date;
                msg.MessageId = hdr.MessageId;
                msg.BodyText = body;
                _Cache.SaveMessage(msg);
                if (uid > highestUIDRead)
                    highestUIDRead = uid;
            }
            return highestUIDRead;
        }

        private void SortUIDsNewToOld(List<UInt32> uids)
        {
            uids.Sort(delegate(UInt32 uid1, UInt32 uid2) { return uid2.CompareTo(uid1); });
        }

        private void SortUIDsOldToNew(List<UInt32> uids)
        {
            uids.Sort(delegate(UInt32 uid1, UInt32 uid2) { return uid1.CompareTo(uid2); });
        }

        protected Imap4Client Client
        {
            get { return _Rep.Client; }
        }
    }
}
