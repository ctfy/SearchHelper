using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITJZ.SearchHelper.API.Exception;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;
using ITJZ.SearchHelper.API.Response;
using System.Data;

namespace ITJZ.SearchHelper.API.Operation
{
    /// <summary>
    /// 用户操作
    /// </summary>
    public class UserOperation : BaseOperation
    {
        #region 分类相关操作

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="uid">要获取uid对应用户的分类列表</param>
        public string getCategoryList(string uid)
        {
            needLogin();
            ArrayList list = new ArrayList();
            SqlDataReader reader = SqlHelper.ExecuteReader(WebConfig.DatabaseConnectionString, CommandType.Text,
                "SELECT * FROM [category] WHERE [uid]=@uid",
                new SqlParameter("@uid", uid));
            while (reader.Read())
            {
                list.Add(new { Guid = reader["guid"], Name = reader["name"] });
            }
            return object2xml(list);
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
            if (isExist)
            {
                bool success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                    "UPDATE [category] set [name]=@name where [guid]=@guid and [uid]=@uid",
                    new SqlParameter("@name", name),
                    new SqlParameter("@guid", guid),
                    new SqlParameter("@uid", CurrentUser.Guid)) > 0;
                return new BaseResponse(success, success ? "分类名修改成功" : "分类名修改失败").ToString();
            }
            else
            {
                bool success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                    "INSERT INTO [category]([guid],[uid],[name]) values(@guid,@uid,@name)",
                    new SqlParameter("@guid", guid),
                    new SqlParameter("@name", name),
                    new SqlParameter("@uid", CurrentUser.Guid)) > 0;
                return new BaseResponse(success, success ? "分类名修改成功" : "分类名修改失败").ToString();
            }
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
            return new BaseResponse(success, success ? "删除成功" : "删除失败").ToString();
        }

        #endregion

        #region 文章相关操作

        /// <summary>
        /// 获取文章索引列表
        /// </summary>
        /// <param name="user"></param>
        public string getArticleIndexList()
        {
            needLogin();
            ArrayList list = new ArrayList();
            SqlDataReader reader = SqlHelper.ExecuteReader(WebConfig.DatabaseConnectionString, CommandType.Text,
                "SELECT [guid] FROM [article] WHERE [uid]=@uid",
                new SqlParameter("@uid", CurrentUser.Guid));
            while (reader.Read())
            {
                list.Add(new { Guid = reader["guid"] });
            }
            return new ArticleIndexListResponse(list).ToString();
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
            if (isExist)
            {
                bool success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                   "INSERT INTO [BinObject](guid,uid) VALUES(@guid,@uid)",
                   new SqlParameter("@guid", article.Guid),
                   new SqlParameter("@uid", CurrentUser.Guid)) > 0;
                success &= saveBinObject(article.Guid, xmlText);
                return new BaseResponse(success, success ? "分类名修改成功" : "分类名修改失败").ToString();
            }
            else
            {
                bool success = SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                     "INSERT INTO [article]([guid],[uid]) VALUES(@guid,@uid)",
                     new SqlParameter("@guid", article.Guid),
                     new SqlParameter("@uid", CurrentUser.Guid)) > 0;
                success &= SqlHelper.ExecuteNonQuery(WebConfig.DatabaseConnectionString, CommandType.Text,
                   "INSERT INTO [BinObject]([guid],[uid]) VALUES(@guid,@uid)",
                   new SqlParameter("@guid", article.Guid),
                   new SqlParameter("@uid", CurrentUser.Guid)) > 0;
                success &= saveBinObject(article.Guid, xmlText);
                return new BaseResponse(success, success ? "保存成功" : "保存失败").ToString();
            }
            throw new BaseException();
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
            return new BaseResponse(success, success ? "删除成功" : "删除失败").ToString();
        }

        #endregion
    }
}
