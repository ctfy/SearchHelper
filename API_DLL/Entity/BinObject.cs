using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API.Entity
{
    /// <summary>
    /// 数据表中无法存放的二进制对象实体的描述类
    /// </summary>
    public class BinObject : BaseEntity
    {
        /// <summary>
        /// 该实体的md5值
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// 该实体的作者
        /// </summary>
        public string Uid { get; set; }
    }
}
