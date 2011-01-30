using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.Client.Model
{
    [Serializable]
    public class Article
    {
        public string Guid { get; set; }
        public string Uid { get; set; }
        public string Title { get; set; }
        public string CategoryGuid { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public static void ShowArticle(string guid)
        {
        }

        public static void Save(Article article)
        {
        }

        internal static void Delete(Article mArticle)
        {
        }

        internal static Article[] getArticleList(Category CurrentCategory)
        {
            return DataServer.DataServer.getInstance().Search("", CurrentCategory.Guid).ToArray();
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
