using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITJZ.SearchHelper.API_DLL.Operation;
using System.Reflection;

namespace ITJZ.SearchHelper.API
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BaseOperation.CurrentUser = new API_DLL.Entity.User()
                {
                    ID = 1,
                    Guid = "user-guid-1",
                    Nickname = "aaa",
                    Password = "aaa",
                    Email = "aaa@aaa.com",
                    CreateTime = DateTime.Now
                };


                Response.AppendHeader("Content-Type", "text/xml");
                string tMethod = getMustRequestString("method");

                UserOperation c = new UserOperation();
                MethodInfo methodInfo = c.GetType().GetMethod(tMethod);

                var ps = methodInfo.GetParameters();
                object[] o = new object[ps.Length];
                int i = 0;
                foreach (var item in ps)
                {
                    o[i++] = getMustRequestString(item.Name);
                }
                string resHtml = (string)methodInfo.Invoke(c, o);
                Response.Write(resHtml);
            }
            catch (System.Exception ex)
            {
                Response.Write(
                    new ITJZ.SearchHelper.API_DLL.Operation.BaseOperation.SearchHelperResponse(false, ex.Message).ToString());
            }
        }

        private string getMustRequestString(string key, params string[] defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new System.Exception("获取请求参数的key为空");
            }

            string v = Request.QueryString[key];

            if (string.IsNullOrEmpty(v))
            {
                if (defaultValue.Length > 0)
                {
                    return defaultValue[0] + "";//返回默认值，并防止返回null
                }
                else
                {
                    throw new System.Exception("缺少参数:" + key);
                }
            }
            else
            {
                return v;
            }


        }
    }
}