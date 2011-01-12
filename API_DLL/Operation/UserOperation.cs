using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITJZ.SearchHelper.API.Exception;

namespace ITJZ.SearchHelper.API.Operation
{
    /// <summary>
    /// 用户操作
    /// </summary>
    public class UserOperation
    {
        #region 分类相关操作

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="uid">要获取uid对应用户的分类列表</param>
        public void getCategoryList(string uid)
        {
            throw new BaseException();
        }

        /// <summary>
        /// 添加或修改分类
        /// </summary>
        /// <param name="guid">要操作分类的guid</param>
        /// <param name="name">要使用的名字</param>
        public void saveCategory(string guid, string name)
        {

        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="guid">要删除的分类的guid</param>
        public void deleteCategory(string guid)
        {

        }

        #endregion

        #region 文章相关操作

        /// <summary>
        /// 获取文章索引列表
        /// </summary>
        /// <param name="user"></param>
        public void geyArticleIndexList()
        {
            throw new BaseException();
        }

        /// <summary>
        /// 添加或修改文章
        /// </summary>
        /// <param name="xmlText">描述文章的xml字符串</param>
        public void saveArticle(string xmlText)
        {
            throw new BaseException();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="guid">要删除文章的guid</param>
        public void deleteArticle(string guid)
        {
            throw new BaseException();
        }

        #endregion
    }
}
