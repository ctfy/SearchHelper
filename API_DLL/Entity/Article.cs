using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ITJZ.SearchHelper.API.Entity
{
    public class Article : BaseEntity
    {
        public string CategoryGuid { get; set; }
        public string Uid { get; set; }
        /// <summary>
        /// 从xml文本中构造出article对象
        /// </summary>
        /// <param name="xmlText"></param>
        /// <returns></returns>
        public static Article initArticleFromXmlText(string xmlText)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlText);
                return new Article()
                {
                    Guid = doc.SelectSingleNode("article/guid").InnerText,
                    Uid = doc.SelectSingleNode("article/uid").InnerText,
                    CategoryGuid = doc.SelectSingleNode("article/categoryguid").InnerText
                };
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
