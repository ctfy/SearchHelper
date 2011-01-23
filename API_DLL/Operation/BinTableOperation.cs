using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITJZ.SearchHelper.API_DLL.Exception;
using System.IO;

namespace ITJZ.SearchHelper.API_DLL.Operation
{
    /// <summary>
    /// 2进制表的操作类
    /// </summary>
    public class BinTableOperation
    {
        /// <summary>
        /// 根据md5获取二进制对象
        /// </summary>
        /// <param name="md5">对象的md5值</param>
        /// <returns></returns>
        public byte[] getBinOjbect(string md5)
        {
            return File.ReadAllBytes(WebConfig.BinObjectSavePath + "/" + md5);
        }
    }
}
