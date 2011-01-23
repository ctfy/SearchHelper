using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ITJZ.SearchHelper.API_DLL.Exception;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace ITJZ.SearchHelper.API_DLL.Operation
{
    /// <summary>
    /// 操作的基类，包含基本的验证逻辑
    /// </summary>
    public class BaseOperation
    {
        public static ITJZ.SearchHelper.API_DLL.Entity.User mCurrentUser;

        /// <summary>
        /// 当前正在操作的用户
        /// </summary>
        public static ITJZ.SearchHelper.API_DLL.Entity.User CurrentUser
        {
            get
            {
                return mCurrentUser;
            }
            set
            {
                mCurrentUser = value;
            }
        }

        /// <summary>
        /// 数据库的连接
        /// </summary>
        public SqlConnection DatabaseConnection { get; private set; }

        /// <summary>
        /// 登录验证
        /// </summary>
        public void needLogin()
        {
            //验证是否登陆
            if (null == CurrentUser)
            {
                throw new PermissionTooLowException("你尚未登陆，无法添加或修改分类");
            }
        }

        /// <summary>
        /// 删除某个2进制对象
        /// </summary>
        /// <param name="guid">在数据库中存放这个2进制对象的guid</param>
        /// <returns></returns>
        public bool deleteBinObject(string guid)
        {
            throw new System.Exception("函数未实现");
        }

        /// <summary>
        /// 保存一个2进制对象
        /// </summary>
        /// <param name="guid">保存该对象时用的guid</param>
        /// <param name="str">字符串类型的2进制对象</param>
        /// <returns></returns>
        public bool saveBinObject(string guid, string str)
        {
            try
            {
                File.WriteAllText(WebConfig.BinObjectSavePath + "\\" + guid, str);
            }
            catch (System.Exception)
            {
                throw;
            }
            return true;
        }

        /// <summary>
        /// 保存一个2进制对象
        /// </summary>
        /// <param name="guid">保存该对象时用的guid</param>
        /// <param name="b">2进制内容</param>
        /// <returns></returns>
        public bool saveBinObject(string guid, byte[] b)
        {
            try
            {
                File.WriteAllBytes(WebConfig.BinObjectSavePath + "\\" + guid, b);
            }
            catch (System.Exception)
            {
                throw;
            }
            return true;
        }


        /// <summary>
        /// 创建一个页面输出结果
        /// </summary>
        public class SearchHelperResponse
        {
            private XDocument mXDocument;
            private XElement mXRoot;

            /// <summary>
            /// 一个页面输出结果类型的构造函数
            /// </summary>
            /// <param name="success">是否执行成功</param>
            /// <param name="message">执行结果提示的消息</param>
            public SearchHelperResponse(bool success, string message,params XElement[] toAddElements)
            {
                //初始化一个xdocument对象，并设置xml头部
                mXDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", "no"));

                //创建根元素并添加到xml文档
                mXRoot = new XElement("Response");
                mXDocument.Add(mXRoot);

                //添加必须参数
                mXRoot.Add(new XElement("Success", success));
                mXRoot.Add(new XElement("Message", message));

                //添加主体输出内容
                foreach (var item in toAddElements)
                {
                    mXRoot.Add(item);
                }
            }

            /// <summary>
            /// 向输出结果添加主体
            /// </summary>
            /// <param name="xelement">一个xElement元素</param>
            public void AddXElement(XElement xelement)
            {
                mXRoot.Add(xelement);
            }

            /// <summary>
            /// 输出格式化后的xml字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                XmlWriter xmlWriter = XmlWriter.Create(sb);
                mXDocument.WriteTo(xmlWriter);
                xmlWriter.Flush();
                return sb.ToString().Replace("utf-16", "utf-8");
            }
        }
    }
}
