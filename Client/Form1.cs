using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITJZ.SearchHelper.Client;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ITJZ.SearchHelper.Client.Model.User user = ITJZ.SearchHelper.Client.Model.User.getCurrentUser();
            if (null!=user)
            {
                toolStripDropDownButton1.Text = user.Nickname;
            }
        }

        private void ucMenu1_SelectedCategoryChanged(object sender, ITJZ.SearchHelper.Client.ucMenu.CategoryEventArgs e)
        {
            ucArticleList1.ShowCategoryList(e.SelectedCategory);
        }

        private void ucArticleList1_SelectArticleChanged(object sender, ITJZ.SearchHelper.Client.ucArticleList.ArticleEventArgs e)
        {
            ucArticle1.ShowArticle(e.SelectedArticle);
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            ITJZ.SearchHelper.Client.Model.User user = ITJZ.SearchHelper.Client.Model.User.getCurrentUser();
            if (null == user)
            {
                if (DialogResult.Yes == FrmLogin.getInstance().ShowDialog())
                {
                    user = ITJZ.SearchHelper.Client.Model.User.getCurrentUser();
                    toolStripDropDownButton1.Text = user.Nickname;
                }
            }
            else
            {
                MessageBox.Show(string.Format("你好:{0}", user.Nickname));
            }
        }
    }
}
