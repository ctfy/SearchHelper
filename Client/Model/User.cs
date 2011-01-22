using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITJZ.SearchHelper.Client.Model
{
    public class User
    {
        public string Guid { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }

        public bool login(TextBox tbEmail, TextBox tbPassword)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;

            mUser = new User();
            return true;
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
            if (needLogin)
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
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
        }
    }
}
