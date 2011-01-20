using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API.Entity
{
    /// <summary>
    /// 本项目中实体类的超类，包含实体中一定包含的属性
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 一个自动增长的id，用于数据库的自增id字段，在程序中并无实际作用
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 一个对象或一条数据库数据的独一无二的id号
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 该实例的创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
