using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITJZ.SearchHelper.API_DLL.Exception;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Xml.Linq;

namespace ITJZ.SearchHelper.API_DLL.Operation
{
    /// <summary>
    /// 用户操作
    /// </summary>
    public class UserOperation : BaseOperation
    {
        #region 用户权限和验证相关操作
        public string register(string email, string password, string nickname)
        {
            try
            {
                string sql = "INSERT INTO [User] set [guid]=@guid, [email]=@email, [password]=@password, [nickname]=@nickname";
                bool success = 1 == SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text, sql,
                    new SqlParameter("@guid", Guid.NewGuid().ToString()),
                     new SqlParameter("@email", email),
                    new SqlParameter("@password", password),
                    new SqlParameter("@nickname", nickname));
                return new SearchHelperResponse(success, "注册" + (success ? "成功" : "失败")).ToString();
            }
            catch
            {
                return new SearchHelperResponse(false, "因发生异常注册失败").ToString();
            }
        }

        public string login(string email, string password)
        {
            string sql = "SELECT * FROM [user] WHERE [email]=@email and [password]=@password";
            var reader = SqlHelper.ExecuteReader(WebConfig.DatabaseConnectionString,CommandType.Text,sql,
                new SqlParameter("@email", email),
                new SqlParameter("@password", password));
            if (reader.Read())
            {
                //设置当前登陆用户
                CurrentUser = new Entity.User()
                {
                    ID = (int)reader["ID"],
                    Guid = reader["Guid"].ToString(),
                    Nickname = reader["Nickname"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    CreateTime = (DateTime)reader["CreateTime"]
                };

                //返回的内容
                XElement xUser = new XElement("User");
                xUser.Add(new XElement("ID", reader["ID"]));
                xUser.Add(new XElement("Guid", reader["Guid"]));
                xUser.Add(new XElement("CreateTime", reader["CreateTime"]));
                xUser.Add(new XElement("Email", reader["email"]));
                xUser.Add(new XElement("Nickname", reader["Nickname"]));
                xUser.Add(new XElement("Password", reader["Password"]));
                xUser.Add(new XElement("SessionKey", System.Web.HttpContext.Current.Session.SessionID));
                return new SearchHelperResponse(true, "登陆成功", xUser).ToString();
            }
            else
            {
                return new SearchHelperResponse(false, "登陆失败").ToString();
            }
        }

        //public string login(string sessionKey)
        //{

        //}

        //public string changePassword(string oldPassword, string newPassword)
        //{

        //}

        #endregion

        #region 分类相关操作

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="uid">要获取uid对应用户的分类列表</param>
        public string getCategoryList(string uid)
        {
            needLogin();
            SqlDataReader reader = SqlHelper.ExecuteReader(WebConfig.DatabaseConnectionString, CommandType.Text,
                "SELECT * FROM [category] WHERE [uid]=@uid",
                new SqlParameter("@uid", uid));
            XElement items = new XElement ("Items");
            while (reader.Read())
            {
                items.Add(
                    new XElement("Item",
                        new XElement("Guid", reader["guid"]),
                        new XElement("Name", reader["name"]),
                        new XElement("Uid", reader["Uid"])));
            }
            return new SearchHelperResponse(true, "成功获取分类", items).ToString();
        }

        /// <summary>
        /// 添加或修改分类
        /// </summary>
        /// <param name="guid">要操作分类的guid</param>
        /// <param name="name">要使用的名字</param>
        public string saveCategory(string guid, string name)
        {
            //需要登陆
            needLogin();
            //该用户是否有该分类
            bool isExist = (int)SqlHelper.ExecuteScalar(WebConfig.DatabaseConnectionString, CommandType.Text,
                "SELECT count(1) from [category] where [uid]=@uid and [guid]=@guid",
                new SqlParameter("@uid", CurrentUser.Guid),
                new SqlParameter("@guid", guid)) > 0;

            bool success = false;
            if (isExist)
            {
                success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                   "UPDATE [category] set [name]=@name where [guid]=@guid and [uid]=@uid",
                   new SqlParameter("@name", name),
                   new SqlParameter("@guid", guid),
                   new SqlParameter("@uid", CurrentUser.Guid)) > 0;
            }
            else
            {
                success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                   "INSERT INTO [category]([guid],[uid],[name]) values(@guid,@uid,@name)",
                   new SqlParameter("@guid", guid),
                   new SqlParameter("@name", name),
                   new SqlParameter("@uid", CurrentUser.Guid)) > 0;
            }
            return new SearchHelperResponse(success, success ? "分类名修改成功" : "分类名修改失败").ToString();
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="guid">要删除的分类的guid</param>
        public string deleteCategory(string guid)
        {
            needLogin();
            bool success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                "DELETE FROM [category] WHERE [uid]=@uid and [guid]=@guid",
                new SqlParameter("@uid", CurrentUser.Guid),
                new SqlParameter("@guid", guid)) > 0;
            return new SearchHelperResponse(success, success ? "删除成功" : "删除失败").ToString();
        }

        #endregion

        #region 文章相关操作

        /// <summary>
        /// 获取文章索引列表
        /// </summary>
        /// <param name="user"></param>
        public string getArticleIndexList(string uid)
        {
            string fromUid = uid;

            needLogin();
            SqlDataReader reader = SqlHelper.ExecuteReader(WebConfig.DatabaseConnectionString, CommandType.Text,
                "SELECT [guid] FROM [article] WHERE [uid]=@uid",
                new SqlParameter("@uid", fromUid));
            XElement items = new XElement("Items");
            while (reader.Read())
            {
                items.Add(new XElement("Item",
                    new XElement("Guid", reader["guid"]),
                    new XElement("Uid", fromUid)
                    ));
            }
            return new SearchHelperResponse(true, "获取文章列表成功", items).ToString();
        }

        /// <summary>
        /// 添加或修改文章
        /// </summary>
        /// <param name="xmlText">描述文章的xml字符串</param>
        public string saveArticle(string xmlText)
        {
            //需要登陆
            needLogin();
            Entity.Article article = Entity.Article.initArticleFromXmlText(xmlText);
            //该用户是否有该文章
            bool isExist = (int)SqlHelper.ExecuteScalar(WebConfig.DatabaseConnectionString, CommandType.Text,
                "SELECT count(*) from [article] where [uid]=@uid and [guid]=@guid",
                new SqlParameter("@uid", CurrentUser.Guid),
                new SqlParameter("@guid", article.Guid)) > 0;
            bool success = false;
            if (isExist)
            {
                success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                  "INSERT INTO [BinObject](guid,uid) VALUES(@guid,@uid)",
                  new SqlParameter("@guid", article.Guid),
                  new SqlParameter("@uid", CurrentUser.Guid)) > 0;
                success &= saveBinObject(article.Guid, xmlText);
            }
            else
            {
                success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                    "INSERT INTO [article]([guid],[uid]) VALUES(@guid,@uid)",
                    new SqlParameter("@guid", article.Guid),
                    new SqlParameter("@uid", CurrentUser.Guid)) > 0;
                success &= SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                   "INSERT INTO [BinObject]([guid],[uid]) VALUES(@guid,@uid)",
                   new SqlParameter("@guid", article.Guid),
                   new SqlParameter("@uid", CurrentUser.Guid)) > 0;
                success &= saveBinObject(article.Guid, xmlText);
            }
            string message = string.Format("{0}文章{1}", isExist ? "修改" : "添加", success ? "成功" : "失败");
            return new SearchHelperResponse(success, message).ToString();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="guid">要删除文章的guid</param>
        public string deleteArticle(string guid)
        {
            needLogin();
            bool success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                "DELETE FROM [article] WHERE [uid]=@uid and guid=@guid",
                new SqlParameter("@uid", CurrentUser.Guid),
                new SqlParameter("@guid", guid)) > 0;
            return new SearchHelperResponse(success, success ? "删除成功" : "删除失败").ToString();
        }

        #endregion
    }
}
