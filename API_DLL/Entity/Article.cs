using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ITJZ.SearchHelper.API.Entity
{
    /// <summary>
    /// 文章实体
    /// </summary>
    public class Article : BaseEntity
    {
        /// <summary>
        /// 此篇文章的分类对应的Guid
        /// </summary>
        public string CategoryGuid { get; set; }

        /// <summary>
        /// 这篇文章的作者的uid
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 从xml文本中构造出article对象
        /// </summary>
        /// <param name="xmlText">一个包含文章实例的xml字符串</param>
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
