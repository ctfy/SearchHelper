using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ITJZ.SearchHelper.API.Exception;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace ITJZ.SearchHelper.API.Operation
{
    /// <summary>
    /// 操作的基类，包含基本的验证逻辑
    /// </summary>
    public class BaseOperation
    {
        /// <summary>
        /// 当前正在操作的用户
        /// </summary>
        public ITJZ.SearchHelper.API.Entity.User CurrentUser { get; set; }

        public SqlConnection DatabaseConnection { get; private set; }

        public void needLogin()
        {
            //验证是否登陆
            if (null == CurrentUser)
            {
                throw new PermissionTooLowException("你尚未登陆，无法添加或修改分类");
            }
        }

        public bool deleteBinObject(string guid)
        {
            throw new System.Exception("函数未实现");
        }

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

        public bool saveBinObject(string guid, byte[] b)
        {
            throw new System.Exception();
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
                return sb.ToString();
            }
        }
    }
}
