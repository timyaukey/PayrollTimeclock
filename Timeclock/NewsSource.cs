using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PayrollTimeclock
{
    public class NewsSource
    {
        [XmlAttributeAttribute("Address")]
        public string SourceAddress;
    }
}
