using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailTools
{
    public interface IMailBoxCache
    {
        string MailBoxName { get; }
        bool UIDStateKnown { get; }
        UInt32 UIDValidity { get; set; }
        UInt32 HighestUIDRead { get; set; }
        UInt32 LowestUIDRead { get; set; }
        void LoadUIDState();
        void SaveUIDState();
        void ClearEverything();
        void PurgeExcept(List<UInt32> uids, UInt32 newestPurgeableUID);
        void SaveMessage(EmailInfo message);
    }
}
