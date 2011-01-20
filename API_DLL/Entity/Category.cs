using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API.Entity
{
    /// <summary>
    /// 文章的分类
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// 分类拥有者的用户id
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 分类的名字
        /// </summary>
        public string Name { get; set; }
    }
}
