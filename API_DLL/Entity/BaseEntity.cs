using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API.Entity
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public string Guid { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
