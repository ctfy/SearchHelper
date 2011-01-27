using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.Client.Util
{
    public class ApiUrlBuilder
    {
        private String _url;
        public ApiUrlBuilder()
        {
            _url = AppConfig.BaseApiUrl + "?";
        }

        public void AddParam(string key, string value)
        {
            _url += key + "=" + value + "&";
        }

        public override string ToString()
        {
            return _url;
        }
    }
}
