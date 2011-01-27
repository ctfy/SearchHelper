using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ITJZ.SearchHelper.Client.Util
{
    public class ModelUtil
    {
        public static bool IsSuccess(ref XmlDocument doc)
        {
            return bool.Parse(doc.SelectSingleNode("//Success").InnerText);
        }

        public static string GetMessage(ref XmlDocument doc)
        {
            return doc.SelectSingleNode("//Message").InnerText;
        }

        public static XmlNodeList getList(ref XmlDocument doc, string xpath)
        {
            return doc.SelectNodes(xpath);
        }
    }
}
