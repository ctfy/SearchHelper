using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Xml;

namespace ITJZ.SearchHelper.Client.Util
{
    public class Tools
    {
        public static string DownloadString(string url)
        {
            WebClient wc = new WebClient();
            byte[] bs = wc.DownloadData(url);
            return System.Text.Encoding.GetEncoding("utf-8").GetString(bs);
        }

        public static XmlDocument buildXDoc(string xmlText)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);
            return doc;
        }
    }
}
