using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.Client
{
    class AppConfig
    {
        private static string mBaseApiUrl = "http://localhost:1618/api.aspx";
        public static string BaseApiUrl
        {
            get
            {
                return mBaseApiUrl;
            }
        }
    }
}
