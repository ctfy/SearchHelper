using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.Client.Model
{
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
            List<Article> list= new List<Article>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Article()
                {
                    Uid="user-guid-1",
                    CategoryGuid="category-guid-1",
                    Guid="article-guid-" +i,
                    Title="文章" + i,
                    Content="内容" +i,
                    CreateTime=DateTime.Now
                });
            }
            return list.ToArray();
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
