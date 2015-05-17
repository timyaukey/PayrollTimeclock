using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Configuration;
using System.Xml.Serialization;

namespace PayrollTimeclock
{
    public class Settings
    {
        private DateTime _FirstPeriodStart;
        private PayrollPeriod _CurrentPeriod;
        private PayrollPeriod _LastPeriod;
        private List<PayrollPeriod> _RecentPeriods;
        private double _DaysInPeriod;
        private string _WebSiteLabel;
        private string _WebSiteURL;
        private string _AdminPasswordHash;
        private List<NewsSource> _NewsSources;
        private string _SMTPServer;
        private int _SMTPPort;
        private string _SMTPUserName;
        private string _SMTPPassword;
        private string _SMTPSenderAddress;
        private string _IMAP4DefaultHost;
        private int _IMAP4DefaultPort;
        private bool _IMAP4DefaultSSL;

        public string LoadFromConfigFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SettingsPersisted));
            SettingsPersisted persisted;
            /*
            persisted = new SettingsPersisted();
            persisted.FirstPeriodStart = new DateTime(2010, 4, 1);
            persisted.DaysInPeriod = 14;
            persisted.AdminPasswordHash = "x";
            persisted.NewsSources = new List<NewsSource>();
            persisted.NewsSources.Add(new NewsSource());
            persisted.SMTPPassword = "x";
            persisted.SMTPPort = 1;
            persisted.SMTPSenderAddress = "e";
            persisted.SMTPServer = "s";
            persisted.SMTPUserName = "u";
            persisted.WebSiteLabel = "l";
            persisted.WebSiteURL = "ur";
            using (TextWriter writer = new StreamWriter(PayrollStatic.PayrollConfigFile))
            {
                serializer.Serialize(writer, persisted);
            }
            */
            
            if (!File.Exists(PayrollStatic.PayrollConfigFile))
            {
                return "Could not find configuration file " + PayrollStatic.PayrollConfigFile;
            }
            using (TextReader reader = new StreamReader(PayrollStatic.PayrollConfigFile))
            {
                persisted = (SettingsPersisted)serializer.Deserialize(reader);
            }

            _DaysInPeriod = persisted.DaysInPeriod;
            _FirstPeriodStart = persisted.FirstPeriodStart;

            _WebSiteLabel = persisted.WebSiteLabel;
            if (string.IsNullOrEmpty(_WebSiteLabel))
            {
                return "Missing StartFormWebSiteLabel setting in configuration file";
            }

            _WebSiteURL = persisted.WebSiteURL;
            if (!System.Uri.IsWellFormedUriString(_WebSiteURL, UriKind.Absolute))
            {
                return "Invalid StartFormWebSiteURL setting in configuration file";
            }

            _AdminPasswordHash = persisted.AdminPasswordHash;
            if (string.IsNullOrEmpty(_AdminPasswordHash))
            {
                return "Missing AdminPasswordHash setting in configuration file";
            }

            _NewsSources = persisted.NewsSources;

            _SMTPServer = persisted.SMTPServer;
            if (string.IsNullOrEmpty(_SMTPServer))
            {
                return "Missing SMTPServer setting in configuration file";
            }

            _SMTPPort = persisted.SMTPPort;

            _SMTPUserName = persisted.SMTPUserName;
            if (string.IsNullOrEmpty(_SMTPUserName))
            {
                return "Missing SMTPUserName setting in configuration file";
            }

            _SMTPPassword = persisted.SMTPPassword;
            if (string.IsNullOrEmpty(_SMTPPassword))
            {
                return "Missing SMTPPassword setting in configuration file";
            }

            _SMTPSenderAddress = persisted.SMTPSenderAddress;
            if (string.IsNullOrEmpty(_SMTPSenderAddress))
            {
                return "Missing SMTPSenderAddress setting in configuration file";
            }

            _IMAP4DefaultHost = persisted.IMAP4DefaultHost;
            _IMAP4DefaultPort = persisted.IMAP4DefaultPort;
            _IMAP4DefaultSSL = persisted.IMAP4DefaultSSL;

            ConfigureForDate();
            
            return null;
        }

        public void ConfigureForDate()
        {
            double fullPeriodsInPast = Math.Floor(DateTime.Today.Date.Subtract(
                _FirstPeriodStart).TotalDays / _DaysInPeriod);
            DateTime startDate = _FirstPeriodStart.AddDays(fullPeriodsInPast * _DaysInPeriod);
            _CurrentPeriod = new PayrollPeriod(startDate);
            DateTime nextDate = startDate.AddDays(-_DaysInPeriod);
            _LastPeriod = new PayrollPeriod(nextDate);
            if (!IsPeriodStart(_CurrentPeriod.StartDate))
                throw new InvalidOperationException("Invalid current period start");
            if (!IsPeriodStart(_LastPeriod.StartDate))
                throw new InvalidOperationException("Invalid last period start");
            _RecentPeriods = new List<PayrollPeriod>();
            _RecentPeriods.Add(_CurrentPeriod);
            _RecentPeriods.Add(_LastPeriod);
            DateTime cutoffDate = DateTime.Now.Date.AddDays(365.0 * -2.0);
            for (; ; )
            {
                nextDate = nextDate.AddDays(-PayrollStatic.Settings.DaysInPeriod);
                if (nextDate.CompareTo(cutoffDate)<0)
                    break;
                PayrollPeriod period = new PayrollPeriod(nextDate);
                _RecentPeriods.Add(period);
            }
        }

        public bool IsAdminPassword(string password)
        {
            return ComputePasswordHash(password) == _AdminPasswordHash;
        }

        public static string ComputePasswordHash(string password)
        {
            byte[] passwordBytes = Encoding.Default.GetBytes(password);
            HashAlgorithm hashProvider = new MD5CryptoServiceProvider();
            string hash = Convert.ToBase64String(hashProvider.ComputeHash(passwordBytes));
            return hash;
        }

        public double DaysInPeriod
        {
            get { return _DaysInPeriod; }
        }

        public PayrollPeriod CurrentPeriod
        {
            get { return _CurrentPeriod; }
        }

        public PayrollPeriod LastPeriod
        {
            get { return _LastPeriod; }
        }

        public IEnumerable<PayrollPeriod> RecentPeriods
        {
            get { return _RecentPeriods; }
        }

        public bool IsPeriodStart(DateTime start)
        {
            return Math.IEEERemainder(start.Subtract(_FirstPeriodStart).TotalDays, _DaysInPeriod) == 0.0;
        }

        public string WebSiteLabel
        {
            get { return _WebSiteLabel; }
        }

        public string WebSiteURL
        {
            get { return _WebSiteURL; }
        }

        public List<NewsSource> NewsSources
        {
            get { return _NewsSources; }
        }

        public string SMTPServer
        {
            get { return _SMTPServer; }
        }

        public int SMTPPort
        {
            get { return _SMTPPort; }
        }

        public string SMTPUserName
        {
            get { return _SMTPUserName; }
        }

        public string SMTPPassword
        {
            get { return _SMTPPassword; }
        }

        public string SMTPSenderAddress
        {
            get { return _SMTPSenderAddress; }
        }

        public string IMAP4DefaultHost
        {
            get { return _IMAP4DefaultHost; }
        }

        public int IMAP4DefaultPort
        {
            get { return _IMAP4DefaultPort; }
        }

        public bool IMAP4DefaultSSL
        {
            get { return _IMAP4DefaultSSL; }
        }
    }
}
