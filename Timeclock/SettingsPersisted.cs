using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PayrollTimeclock
{
    public class SettingsPersisted
    {
        public DateTime FirstPeriodStart;
        public double DaysInPeriod;
        public string WebSiteLabel;
        public string WebSiteURL;
        public string AdminPasswordHash;
        public List<NewsSource> NewsSources;
        public string SMTPServer;
        public int SMTPPort;
        public string SMTPUserName;
        public string SMTPPassword;
        public string SMTPSenderAddress;
        public string IMAP4DefaultHost;
        public int IMAP4DefaultPort;
        public bool IMAP4DefaultSSL;
    }
}
