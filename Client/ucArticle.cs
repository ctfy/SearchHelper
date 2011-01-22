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
    public partial class ucArticle : UserControl
    {
        private const int DEFAULT = 0;//默认正常显示
        private const int EDITING = 1;//编辑状态
        private int mStateCode;
        private bool mAllowEdit = false;
        private bool mChanged = false;
        private Model.Article mArticle;
        public ucArticle()
        {
            InitializeComponent();
        }

        private void ucArticle_Load(object sender, EventArgs e)
        {

        }

        public class ArticleState
        {
            public int currentArticleIndex;
            public static List<ArticleState> History { get; set; }
        }

        public void ShowArticle(Model.Article article)
        {
            mArticle = article;
            mStateCode = DEFAULT;

            txtTitle.ReadOnly = true;
            txtTitle.Text = article.Title;
            txtContent.ReadOnly = true;
            txtContent.Text = article.Content;

            if (null != Model.User.getCurrentUser())
            {
                mAllowEdit = article.Guid == Model.User.getCurrentUser().Guid;
            }
            bindButtonState();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (mChanged)
            {
                DialogResult result = MessageBox.Show("请问是否保存", "提示", MessageBoxButtons.YesNoCancel);
                if (DialogResult.Yes == result)
                {
                    save();
                }
            }
            txtTitle.ReadOnly = false;
            txtContent.ReadOnly = false;
            txtTitle.Text = "";
            txtContent.Text = "";
            mStateCode = EDITING;
            bindButtonState();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtTitle.ReadOnly = false;
            txtContent.ReadOnly = false;
            mStateCode = EDITING;
            bindButtonState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
            mStateCode = DEFAULT;
            mChanged = false;
            bindButtonState();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("真的要删除吗", "提示", MessageBoxButtons.YesNoCancel))
            {
                Model.Article.Delete(mArticle);
            }
            bindButtonState();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (mChanged)
            {
                if (DialogResult.Yes == MessageBox.Show("是否保存修改", "提示", MessageBoxButtons.YesNo))
                {
                    save();
                }
            }
            txtTitle.ReadOnly = true;
            txtTitle.Text = mArticle.Title;
            txtContent.ReadOnly = true;
            txtContent.Text = mArticle.Content;
            mStateCode = DEFAULT;
            bindButtonState();
        }

        private void save()
        {
            Model.Article tArticle = new Model.Article()
            {
                Guid = Guid.NewGuid().ToString(),
                Title = txtTitle.Text,
                Content = txtContent.Text,
                CategoryGuid = mArticle.CategoryGuid,
                CreateTime = DateTime.Now
            };
            Model.Article.Save(tArticle);
        }

        private void bindButtonState()
        {
            if (DEFAULT == mStateCode)
            {
                btnCreate.Visible = true;
                btnEdit.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnDelete.Visible = true;

                btnCreate.Enabled = true;
                var loginUser = Model.User.getCurrentUser();
                if (null != loginUser)
                {
                    btnEdit.Enabled = mArticle.Uid == loginUser.Guid;
                }
                btnSave.Enabled = mChanged;
                btnCancel.Enabled = false;
                btnDelete.Enabled = true;
            }
            else if (EDITING == mStateCode)
            {
                btnCreate.Visible = false;
                btnEdit.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                btnDelete.Visible = false;

                btnCreate.Enabled = true;
                var loginUser = Model.User.getCurrentUser();
                if (null != loginUser)
                {
                    btnEdit.Enabled = mArticle.Uid == loginUser.Guid;
                    btnDelete.Enabled = btnEdit.Enabled;
                }
                btnSave.Enabled = mChanged;
                btnCancel.Enabled = true;
            }
        }

        private void txtTitleAndContent_TextChanged(object sender, EventArgs e)
        {
            mChanged = true;
            bindButtonState();
        }
    }
}
