using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITJZ.SearchHelper.API.Operation;
using System.Threading;

namespace API_DLL
{
    class Program
    {
        static void Main(string[] args)
        {
            ITJZ.SearchHelper.API.Entity.User currentUser = new ITJZ.SearchHelper.API.Entity.User()
            {
                ID = 1,
                Guid = "user-guid-123",
                Nickname = "nickname",
                Password = "pwd123",
                Email = "wei@wei.com",
                CreateTime = DateTime.Now
            };
            UserOperation opt = new UserOperation()
            {
                CurrentUser = currentUser
            };
            testWriteCategory(opt, 10);
            //testWriteArticle(opt, 10);
        }

        static void testWriteCategory(UserOperation opt, int timeNumber)
        {
            for (int i = 0; i < timeNumber; i++)
            {
                opt.saveCategory("category-guid-1", "c#语言");
            }
        }

        static void testWriteArticle(UserOperation opt, int timeNumber)
        {
            Console.WriteLine("测试<UserOperation>.saveArticle(xmlText) {0} 次", timeNumber);
            for (int i = 0; i < timeNumber; i++)
            {
                opt.saveArticle(string.Format(@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<article>
    <version>1</version>
    <guid>{0}</guid>
    <uid>{1}</uid>
    <categoryguid>{2}</categoryguid>
    <title>标题-{3}</title>
    <content>当前时间:{4} </content>
</article>            
            ", Guid.NewGuid().ToString(), opt.CurrentUser.Guid, "category-guid-567", DateTime.Now, DateTime.Now));
                Thread.Sleep(10);
            }
        }
    }
}