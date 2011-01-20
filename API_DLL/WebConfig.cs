using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.API_DLL
{
    public class WebConfig
    {
        static WebConfig()
        {
            DatabaseConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=""C:\Documents and Settings\a\My Documents\Visual Studio 2010\Projects\SearchHelper\API_DLL\Database1.mdf"";Integrated Security=True;Connect Timeout=30;User Instance=True";
            BinObjectSavePath = "c:/itjz-bin-object";
        }
        public static string DatabaseConnectionString { get; set; }
        public static string BinObjectSavePath { get; set; }
    }
}
