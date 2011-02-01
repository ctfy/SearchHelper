using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Community.CsharpSqlite.SQLiteClient;
using System.Threading;

namespace ITJZ.SearchHelper.Client.DataServer
{
    public class DataServer
    {
        private static DataServer _DataServer;
        private DataServer()
        {
            Thread t = new Thread(new ThreadStart(ClientAndServer.Start));
            t.Start();

        }
        public static DataServer getInstance()
        {
            if (null==_DataServer)
            {
                _DataServer = new DataServer();
            }
            return _DataServer;
        }

        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public List<Model.Category> getCategoryList(string uid, bool needUpdate)
        {
            List<Model.Category> forReturnList = new List<Model.Category>();
            string binName = "Category.bin";
            if (File.Exists(binName) && !needUpdate)
            {
                using (FileStream fs = File.Open(binName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    forReturnList = (List<Model.Category>)bin.Deserialize(fs);
                }
            }
            else
            {
                string apiUrl = string.Format("{0}?method=getCategoryList&uid={1}", AppConfig.BaseApiUrl, uid);
                string result = Util.Tools.DownloadString(apiUrl);
                var doc = Util.Tools.buildXDoc(result);

                bool success = bool.Parse(doc.SelectSingleNode("//Success").InnerText);
                string message = doc.SelectSingleNode("//Message").InnerText;

                if (success)
                {
                    XmlNodeList nodeList = doc.SelectNodes("//Items/Item");
                    foreach (XmlNode item in nodeList)
                    {
                        Model.Category tempCategory = new Model.Category();
                        tempCategory.Guid = item["Guid"].InnerText;
                        tempCategory.Name = item["Name"].InnerText;
                        tempCategory.Uid = item["Uid"].InnerText;
                        forReturnList.Add(tempCategory);
                    }
                    using (FileStream fs = File.Open(binName, FileMode.Create))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(fs, forReturnList);
                    }
                }
            }
            return forReturnList;
        }

        public List<Model.Article> Search(string keyword, params string[] categoryGuid)
        {
            List<Model.Article> list = new List<Model.Article>();
            using (SqliteConnection conn = new SqliteConnection (AppConfig.DatabaseString))
            {
                string sql = "select * from article where title like '%" + keyword + "%' and categoryGuid='" + categoryGuid + "'";
                using (SqliteCommand cmd=new SqliteCommand (sql, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        list.Add(new Model.Article()
                        {
                            Guid = (String)reader["Guid"],
                            Uid = (String)reader["Uid"],
                            Title = (String)reader["Title"],
                            Content = (String)reader["Content"],
                            CategoryGuid = (String)reader["CategoryGuid"],
                            CreateTime = (DateTime)reader["CreateTime"],
                        });
                    }

                }
            }
            return list;
        }
    }


    class ClientAndServer
    {
        public void updateCategory()
        {
            Model.User user = Model.User.getCurrentUser();
            if (null == user)
            {
                return;
            }

            List<Model.Category> list = new List<Model.Category>();
            #region 从网络下载最新的分类
            string apiUrl = string.Format("{0}?method=getCategoryList&uid={1}", AppConfig.BaseApiUrl, user.Guid);
            string result = Util.Tools.DownloadString(apiUrl);
            var doc = Util.Tools.buildXDoc(result);

            bool success = bool.Parse(doc.SelectSingleNode("//Success").InnerText);
            if (success)
            {
                XmlNodeList nodeList = doc.SelectNodes("//Items/Item");
                foreach (XmlNode item in nodeList)
                {
                    Model.Category tempCategory = new Model.Category();
                    tempCategory.Guid = item.SelectSingleNode("//Guid").InnerText;
                    tempCategory.Name = item.SelectSingleNode("//Name").InnerText;
                    tempCategory.Uid = item.SelectSingleNode("//Uid").InnerText;
                    list.Add(tempCategory);
                }
            }
            #endregion

            string sql = "INSERT INTO [category]([guid], [name], [uid]) VALUES(@guid, @name, @uid)";
            using (SqliteConnection conn = new SqliteConnection(AppConfig.DatabaseString))
            {
                using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.Add("@guid", SqlDbType.NChar);
                    cmd.Parameters.Add("@name", SqlDbType.NChar);
                    cmd.Parameters.Add("@uid", SqlDbType.NChar);
                    foreach (var item in list)
                    {
                        cmd.Parameters["@guid"].Value = item.Guid;
                        cmd.Parameters["@name"].Value = item.Name;
                        cmd.Parameters["@uid"].Value = item.Uid;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void updateArticle()
        {
            Model.User user = Model.User.getCurrentUser();
            if (null == user)
            {
                return;
            }

            List<Model.Article> list = new List<Model.Article>();
            #region 从网络下载最新
            string apiUrl = string.Format("{0}?method=getArticleIndexList&uid={1}", AppConfig.BaseApiUrl, user.Guid);
            string result = Util.Tools.DownloadString(apiUrl);
            var doc = Util.Tools.buildXDoc(result);

            bool success = bool.Parse(doc.SelectSingleNode("//Success").InnerText);
            if (success)
            {
                XmlNodeList nodeList = doc.SelectNodes("//Items/Item");
                foreach (XmlNode item in nodeList)
                {
                    Model.Article temp = new Model.Article();
                    temp.Guid = item.SelectSingleNode("//Guid").InnerText;
                    temp.Uid = item.SelectSingleNode("//Uid").InnerText;
                    list.Add(temp);
                }
            }
            #endregion

            string sql = "INSERT INTO [article]([guid], [uid]) VALUES(@guid, @uid)";
            using (SqliteConnection conn = new SqliteConnection(AppConfig.DatabaseString))
            {
                using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.Add("@guid", SqlDbType.NChar);
                    cmd.Parameters.Add("@uid", SqlDbType.NChar);
                    foreach (var item in list)
                    {
                        cmd.Parameters["@guid"].Value = item.Guid;
                        cmd.Parameters["@uid"].Value = item.Uid;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void updateBinObject()
        {
            List<string> md5list = new List<string>();
            string sql = "select [guid] from [article] where len(content)<2";
            using (SqliteConnection conn = new SqliteConnection(AppConfig.DatabaseString))
            {
                using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        md5list.Add((string)reader["md5"]);
                    }
                }
            }
            foreach (string p_md5 in md5list)
            {
                string apiUrl = string.Format("{0}?class=BinTableOperation&method=getBinOjbect&md5={1}", AppConfig.BaseApiUrl, p_md5);
                string content = Util.Tools.DownloadString(apiUrl);
            }
        }

        public static void Start()
        {
            ClientAndServer cas = new ClientAndServer();

            while (true)
            {
                cas.updateArticle();
                Thread.Sleep(3000);
            }
        }
    }
}
