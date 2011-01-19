using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API.Response
{
    public class BaseResponse
    {
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public BaseResponse(bool success, string message)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
