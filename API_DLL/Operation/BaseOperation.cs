using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API.Operation
{
    /// <summary>
    /// 操作的基类，包含基本的验证逻辑
    /// </summary>
    class BaseOperation
    {
        /// <summary>
        /// 当前正在操作的用户
        /// </summary>
        private ITJZ.SearchHelper.API.Entity.User CurrentUser{ get; set; }
    }
}
