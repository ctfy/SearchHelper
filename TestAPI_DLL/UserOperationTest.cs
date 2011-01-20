using ITJZ.SearchHelper.API.Operation;
using ITJZ.SearchHelper.API.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;
using System.Xml;

namespace TestProject1
{
    /// <summary>
    ///这是 UserOperationTest 的测试类，旨在
    ///包含所有 UserOperationTest 单元测试
    ///</summary>
    [TestClass()]
    public class UserOperationTest
    {
        User login()
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
            return currentUser;
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        ///deleteArticle 的测试
        ///</summary>
        [TestMethod()]
        public void deleteArticleTest()
        {
            UserOperation target = new UserOperation(); // TODO: 初始化为适当的值
            target.CurrentUser = login();
            string guid = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual = target.deleteArticle(guid);
            checkXmlResponse(actual);
        }

        /// <summary>
        ///deleteCategory 的测试
        ///</summary>
        [TestMethod()]
        public void deleteCategoryTest()
        {
            UserOperation target = new UserOperation(); // TODO: 初始化为适当的值
            target.CurrentUser = login();
            string guid = "category-grid-1"; // TODO: 初始化为适当的值
            string actual = target.deleteCategory(guid);
            checkXmlResponse(actual);
        }

        /// <summary>
        ///getArticleIndexList 的测试
        ///</summary>
        [TestMethod()]
        public void getArticleIndexListTest()
        {
            UserOperation target = new UserOperation(); // TODO: 初始化为适当的值
            target.CurrentUser = login();
            string actual;
            actual = target.getArticleIndexList();
            checkXmlResponse(actual);
        }

        /// <summary>
        ///getCategoryList 的测试
        ///</summary>
        [TestMethod()]
        public void getCategoryListTest()
        {
            UserOperation target = new UserOperation(); // TODO: 初始化为适当的值
            target.CurrentUser = login();
            string actual = target.getCategoryList(target.CurrentUser.Guid);
            checkXmlResponse(actual);
        }

        /// <summary>
        ///saveArticle 的测试
        ///</summary>
        [TestMethod()]
        public void saveArticleTest()
        {
            UserOperation target = new UserOperation(); // TODO: 初始化为适当的值
            target.CurrentUser = login();

            string actual = target.saveArticle(string.Format(@"<?xml version=""1.0"" encoding=""ISO-8859-1""?>
<article>
    <version>1</version>
    <guid>{0}</guid>
    <uid>{1}</uid>
    <categoryguid>{2}</categoryguid>
    <title>标题-{3}</title>
    <content>当前时间:{4} </content>
</article>            
            ", Guid.NewGuid().ToString(), target.CurrentUser.Guid, "category-guid-567", DateTime.Now, DateTime.Now));

            checkXmlResponse(actual);
        }

        /// <summary>
        ///saveCategory 的测试
        ///</summary>
        [TestMethod()]
        public void saveCategoryTest()
        {
            UserOperation target = new UserOperation(); // TODO: 初始化为适当的值

            target.CurrentUser = login();

            string guid = "category-guid-1"; // TODO: 初始化为适当的值
            string name = "c#"; // TODO: 初始化为适当的值
            string actual;
            actual = target.saveCategory(guid, name);

            checkXmlResponse(actual);
        }

        /// <summary>
        /// 检测输出结果是否合法（页面输出结果应当时一个符合定义的xml文档）
        /// </summary>
        /// <param name="xmlResponse"></param>
        public void checkXmlResponse(string xmlResponse)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlResponse);
            string success = doc.SelectSingleNode("/Response/Success").InnerText;
            string message = doc.SelectSingleNode("/Response/Message").InnerText;
            Assert.IsTrue((null != success) && (null != message));
        }
    }
}
