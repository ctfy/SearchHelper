using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ITJZ.SearchHelper.Client.Model
{
    [Serializable]
    public class User
    {
        public string Guid { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }

        public static bool login(TextBox tbEmail, TextBox tbPassword)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;
            string url = string.Format("{0}?method=login&email={1}&password={2}", AppConfig.BaseApiUrl, email, password);
            string result = Client.Util.Tools.DownloadString(url);
            var doc = Client.Util.Tools.buildXDoc(result);

            bool success = bool.Parse(doc.SelectSingleNode("//Success").InnerText);
            if (success)
            {
                mUser = new User()
                {
                    Guid = doc.SelectSingleNode("//User/ID").InnerText,
                    Nickname = doc.SelectSingleNode("//User/Nickname").InnerText,
                    Password = doc.SelectSingleNode("//User/Password").InnerText,
                    CreateTime = DateTime.Parse(doc.SelectSingleNode("//User/CreateTime").InnerText),
                    Email = doc.SelectSingleNode("//User/Email").InnerText
                };
                try
                {
                    using (FileStream fs = File.Open("User.bin", FileMode.Create))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(fs, mUser);
                    }
                }
                catch { }
                return true;
            }
            else
            {
                MessageBox.Show(doc.SelectSingleNode("//Message").InnerText);
                return false;
            }
        }

        private static User mUser;


        /// <summary>
        /// 返回登陆用户
        /// </summary>
        /// <returns>当前登陆用户,为登陆时返回null</returns>
        public static User getCurrentUser()
        {
            return getCurrentUser(false);
        }

        /// <summary>
        /// 返回登陆用户
        /// </summary>
        /// <param name="needLogin">如果该值为真，则在为登陆时弹出登陆窗口</param>
        /// <returns>当前登陆用户</returns>
        public static User getCurrentUser(bool needLogin)
        {
            if (null == mUser && File.Exists("User.bin"))
            {
                try
                {
                    using (FileStream fs = File.Open("User.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        mUser = (User)bin.Deserialize(fs);
                    }
                }
                catch (Exception)
                {
                    mUser = null;
                    File.Delete("User.bin");
                }
            }

            if (needLogin && null == mUser)
            {
                showLoginForm();
            }
            return mUser;
        }

        /// <summary>
        /// 显示登陆窗口
        /// </summary>
        public static void showLoginForm()
        {
            FrmLogin frm = FrmLogin.getInstance();
            frm.ShowDialog();
        }
    }
}
