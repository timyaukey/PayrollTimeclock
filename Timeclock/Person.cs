using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

using MailTools;

namespace PayrollTimeclock
{
    public class Person
    {
        public readonly string FolderName;

        private XmlDocument _Doc;
        public PersonStringProperty FullName;
        public PersonStringProperty PIN;
        public PersonStringProperty ExternalID;
        public PersonStringProperty EmailAddress;
        public PersonStringProperty IMAP4Host;
        public PersonIntProperty IMAP4Port;
        public PersonBoolProperty IMAP4SSL;
        public PersonStringProperty IMAP4Address;
        public PersonStringProperty IMAP4Password;
        public EmailAddress AddressObj;

        public static Person Load(string folderName)
        {
            Person result = new Person(folderName);
            result.LoadFromFile();
            result.AddressObj = new EmailAddress(result.EmailAddress.GetValue, result.FullName.GetValue);
            return result;
        }

        private Person(string folderName)
        {
            FolderName = folderName;
        }

        private void LoadFromFile()
        {
            _Doc = new XmlDocument();
            _Doc.Load(PayrollStatic.EmployeesFolder + "\\" + FolderName + "\\Person.xml");
            FullName = new PersonStringProperty(_Doc, "FullName", string.Empty);
            PIN = new PersonStringProperty(_Doc, "PIN", string.Empty);
            ExternalID = new PersonStringProperty(_Doc, "ExternalID", string.Empty);
            EmailAddress = new PersonStringProperty(_Doc, "EmailAddress", string.Empty);
            IMAP4Host = new PersonStringProperty(_Doc, "IMAP4Host", PayrollStatic.Settings.IMAP4DefaultHost);
            IMAP4Port = new PersonIntProperty(_Doc, "IMAP4Port", PayrollStatic.Settings.IMAP4DefaultPort);
            IMAP4SSL = new PersonBoolProperty(_Doc, "IMAP4SSL", PayrollStatic.Settings.IMAP4DefaultSSL);
            IMAP4Address = new PersonStringProperty(_Doc, "IMAP4Address", EmailAddress.GetValue);
            IMAP4Password = new PersonStringProperty(_Doc, "IMAP4Password", string.Empty);
        }

        public bool UseIMAP4
        {
            get { return IMAP4Password.Exists; }
        }

        public override string ToString()
        {
            return FullName.GetValue;
        }

        public abstract class PersonProperty<T>
        {
            private XmlDocument _Doc;
            private string _PropertyName;
            private T _DefaultValue;
            private T _ActualValue;
            private bool _ValueExists;
            private bool _ValueLoaded;

            public PersonProperty(XmlDocument doc, string propertyName, T defaultValue)
            {
                _Doc = doc;
                _PropertyName = propertyName;
                _DefaultValue = defaultValue;
                _ValueLoaded = false;
            }

            public T GetValue
            {
                get
                {
                    EnsureValueLoaded();
                    return _ActualValue;
                }
            }

            public bool Exists
            {
                get
                {
                    EnsureValueLoaded();
                    return _ValueExists;
                }
            }

            private void EnsureValueLoaded()
            {
                if (!_ValueLoaded)
                {
                    XmlElement childElm = (XmlElement)_Doc.DocumentElement.SelectSingleNode(_PropertyName);
                    if (childElm == null)
                    {
                        _ActualValue = _DefaultValue;
                        _ValueExists = false;
                    }
                    else
                    {
                        _ActualValue = Convert(childElm.InnerText);
                        _ValueExists = true;
                    }
                    _ValueLoaded = true;
                }
            }

            protected abstract T Convert(string input);
        }

        public class PersonStringProperty : PersonProperty<string>
        {
            public PersonStringProperty(XmlDocument doc, string propertyName, string defaultValue)
                : base(doc, propertyName, defaultValue)
            {
            }

            protected override string Convert(string input)
            {
                return input;
            }
        }

        public class PersonIntProperty : PersonProperty<int>
        {
            public PersonIntProperty(XmlDocument doc, string propertyName, int defaultValue)
                : base(doc, propertyName, defaultValue)
            {
            }

            protected override int Convert(string input)
            {
                return int.Parse(input);
            }
        }

        public class PersonBoolProperty : PersonProperty<bool>
        {
            public PersonBoolProperty(XmlDocument doc, string propertyName, bool defaultValue)
                : base(doc, propertyName, defaultValue)
            {
            }

            protected override bool Convert(string input)
            {
                return bool.Parse(input);
            }
        }
    }
}
