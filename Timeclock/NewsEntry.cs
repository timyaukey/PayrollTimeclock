using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace PayrollTimeclock
{
    /// <summary>
    /// Read entries from a news feed. Currently only support ATOM format feeds.
    /// </summary>
    public class NewsEntry
    {
        public AtomData Title;
        public AtomData Content;
        public DateTime Updated;
        public string AuthorName;

        public NewsEntry(AtomData title, AtomData content, DateTime updated, string authorName)
        {
            Title = title;
            Content = content;
            Updated = updated;
            AuthorName = authorName;
        }

        public static void LoadURI(List<NewsEntry> news, string uri)
        {
            WebRequest request = WebRequest.Create(uri);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    XmlDocument responseDoc = new XmlDocument();
                    XmlNamespaceManager nsMgr = new XmlNamespaceManager(responseDoc.NameTable);
                    nsMgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");
                    responseDoc.Load(responseStream);
                    if (responseDoc.DocumentElement.Name != "feed")
                        throw new InvalidDataException("Document element must be <feed>");
                    XmlNodeList entries = responseDoc.DocumentElement.SelectNodes("atom:entry", nsMgr);
                    foreach (XmlElement entry in entries)
                    {
                        LoadAtomEntry(news, nsMgr, entry);
                    }
                }
            }
        }

        private static void LoadAtomEntry(List<NewsEntry> news, XmlNamespaceManager nsMgr, XmlElement entry)
        {
            AtomData title = GetAtomData((XmlElement)entry.SelectSingleNode("atom:title", nsMgr), nsMgr);
            AtomData content = GetAtomData((XmlElement)entry.SelectSingleNode("atom:content", nsMgr), nsMgr);
            string updatedText = entry.SelectSingleNode("atom:updated", nsMgr).InnerText;
            DateTime updated = DateTime.Parse(updatedText);
            string authorName = entry.SelectSingleNode("atom:author/atom:name", nsMgr).InnerText;
            news.Add(new NewsEntry(title, content, updated, authorName));
        }

        private static AtomData GetAtomData(XmlElement elm, XmlNamespaceManager nsMgr)
        {
            string dataTypeName = elm.GetAttribute("type");
            AtomDataType dataType;
            if (dataTypeName == "text")
                dataType = AtomDataType.Text;
            else if (dataTypeName == "html")
                dataType = AtomDataType.HTML;
            else
                throw new InvalidDataException("Atom data type must be [text] or [html]");
            string data = elm.InnerText;
            AtomData atomData = new AtomData(dataType, data);
            return atomData;
        }
    }

    public enum AtomDataType
    {
        Text = 1,
        HTML = 2
    }

    public class AtomData
    {
        public readonly AtomDataType DataType;
        public readonly string Data;

        public AtomData(AtomDataType dataType, string data)
        {
            DataType = dataType;
            Data = data;
        }

        public override string ToString()
        {
            return DataType + ":" + Data;
        }
    }
}
