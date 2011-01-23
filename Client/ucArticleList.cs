using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITJZ.SearchHelper.Client
{
    public partial class ucArticleList : UserControl
    {
        public ucArticleList()
        {
            InitializeComponent();
        }

        public Client.Model.Category CurrentCategory { get; set; }
        public Client.Model.Article[] CurrentArticles{ get; set; }
        public void ShowCategoryList(ITJZ.SearchHelper.Client.Model.Category category)
        {
            if (null == category)
            {
                throw new Exception("分类不能为空值");
            }
            CurrentCategory = category;
            showArticleList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((ListBox)sender).SelectedIndex;
            if (selectedIndex > -1)
            {
                ITJZ.SearchHelper.Client.Model.Article selectedArticle = CurrentArticles[selectedIndex];
                ArticleEventArgs args = new ArticleEventArgs() { SelectedArticle = selectedArticle };
                SelectArticleChanged(this, args);
            }
        }

        private void ucArticleList_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler<ArticleEventArgs> SelectArticleChanged;
        public class ArticleEventArgs : EventArgs
        {
            public Model.Article SelectedArticle { get; set; }
        }


        private void showArticleList()
        {
            listBox1.Items.Clear();
            CurrentArticles = Model.Article.getArticleList(CurrentCategory);
            listBox1.Items.AddRange(CurrentArticles);
        }
    }
}
