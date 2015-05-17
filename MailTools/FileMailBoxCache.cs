using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace MailTools
{
    public abstract class FileMailBoxCache : IMailBoxCache
    {
        private string _MailBoxName;
        private string _LocalFolder;
        private bool _UIDStateKnown;
        private UInt32 _UIDValidity;
        private UInt32 _HighestUIDRead;
        private UInt32 _LowestUIDRead;
        private XmlSerializer _UIDStateSerializer;
        private string _UIDStateFile;
        public const string UIDFilePrefix = "UIDMSG_";

        public FileMailBoxCache(string mailBoxName, string localFolder)
        {
            _MailBoxName = mailBoxName;
            _LocalFolder = localFolder;
            _UIDStateKnown = false;
            _UIDStateSerializer = new XmlSerializer(typeof(UIDState));
            _UIDStateFile = Path.Combine(_LocalFolder, "UIDState.xml");
        }

        public string MailBoxName
        {
            get { return _MailBoxName; }
        }

        public string LocalFolder
        {
            get { return _LocalFolder; }
        }

        public bool UIDStateKnown
        {
            get { return _UIDStateKnown; }
        }

        public UInt32 UIDValidity
        {
            get { return _UIDValidity; }
            set { _UIDValidity = value; }
        }

        public UInt32 HighestUIDRead
        {
            get { return _HighestUIDRead; }
            set { _HighestUIDRead = value; }
        }

        public UInt32 LowestUIDRead
        {
            get { return _LowestUIDRead; }
            set { _LowestUIDRead = value; }
        }

        public void LoadUIDState()
        {
            _UIDStateKnown = false;
            if (File.Exists(_UIDStateFile))
            {
                using (TextReader stateReader = new StreamReader(_UIDStateFile))
                {
                    UIDState state = (UIDState)_UIDStateSerializer.Deserialize(stateReader);
                    _UIDValidity = state.UIDValidity;
                    _HighestUIDRead = state.HighestUIDRead;
                    _LowestUIDRead = state.LowestUIDRead;
                    _UIDStateKnown = true;
                }
            }
        }

        public void SaveUIDState()
        {
            using (TextWriter stateWriter = new StreamWriter(_UIDStateFile))
            {
                UIDState state = new UIDState();
                state.UIDValidity = _UIDValidity;
                state.HighestUIDRead = _HighestUIDRead;
                state.LowestUIDRead = _LowestUIDRead;
                _UIDStateSerializer.Serialize(stateWriter, state);
            }
        }

        public void ClearEverything()
        {
            _UIDStateKnown = false;
            _UIDValidity = 0;
            _HighestUIDRead = 0;
            _LowestUIDRead = 0;
            if (File.Exists(_UIDStateFile))
                File.Delete(_UIDStateFile);
            string[] files = Directory.GetFiles(_LocalFolder);
            foreach (string file in files)
            {
                if (Path.GetFileName(file).StartsWith(UIDFilePrefix))
                {
                    File.Delete(file);
                }
            }
        }

        public void PurgeExcept(List<UInt32> uids, UInt32 newestPurgeableUID)
        {
            string[] files = Directory.GetFiles(_LocalFolder);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                if (fileName.StartsWith(UIDFilePrefix))
                {
                    string uidText = Path.GetFileNameWithoutExtension(fileName).Substring(UIDFilePrefix.Length);
                    UInt32 fileUID = UInt32.Parse(uidText);
                    if (fileUID <= newestPurgeableUID && !uids.Contains(fileUID))
                    {
                        File.Delete(file);
                    }
                }
            }
        }

        public abstract void SaveMessage(EmailInfo message);
    }
}
