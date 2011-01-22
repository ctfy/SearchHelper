using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        }

        private void ucMenu1_SelectedCategoryChanged(object sender, ITJZ.SearchHelper.Client.ucMenu.CategoryEventArgs e)
        {
            ucArticleList1.ShowCategoryList(e.SelectedCategory);
        }

        private void ucArticleList1_SelectArticleChanged(object sender, ITJZ.SearchHelper.Client.ucArticleList.ArticleEventArgs e)
        {
            ucArticle1.ShowArticle(e.SelectedArticle);
        }
    }
}
