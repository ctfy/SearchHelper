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

        public string object2xml(object obj)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("");
            string fullName = obj.GetType().FullName;
            if (fullName == "System.Collections.ArrayList")
            {
                foreach (var item in (ArrayList)obj)
                {
                    Type t = Type.GetType(item.GetType().FullName);
                    foreach (PropertyInfo pi in t.GetProperties())
                    {
                    }

                }
            }
            return null;
        }

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
    }
}
