using ITJZ.SearchHelper.API.Operation;
using ITJZ.SearchHelper.API.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

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
            string actual;
            actual = target.deleteArticle(guid);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///deleteCategory 的测试
        ///</summary>
        [TestMethod()]
        public void deleteCategoryTest()
        {
            UserOperation target = new UserOperation(); // TODO: 初始化为适当的值
            target.CurrentUser = login();
            string guid = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.deleteCategory(guid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
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
            Assert.IsNotNull(actual);
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
            Assert.IsNotNull(actual);
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
            Assert.IsNotNull(actual);
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
            Assert.IsNotNull(actual);
        }
    }
}
