using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITJZ.SearchHelper.Client.Model;
using System.Collections;

namespace ITJZ.SearchHelper.Client
{
    public partial class ucMenu : UserControl
    {
        public ucMenu()
        {
            InitializeComponent();
        }

        private ITJZ.SearchHelper.Client.Model.Category[] mCategories;
        private void ucMenu_Load(object sender, EventArgs e)
        {
            updateItems(false);
        }

        public Category SelectedCategory { get; set; }

        //设置选中项
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectIndex = ((ListBox)sender).SelectedIndex;
            SelectedCategory = mCategories[selectIndex];
            if (null != SelectedCategoryChanged)
            {
                SelectedCategoryChanged(this,
                    new CategoryEventArgs()
                    {
                        SelectedCategory = this.SelectedCategory
                    }
                    );
            }
        }

        public event EventHandler<CategoryEventArgs> SelectedCategoryChanged;
        public class CategoryEventArgs : EventArgs
        {
            public Category SelectedCategory { get; set; }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategoryDialog frm = new FrmCategoryDialog();
            if (DialogResult.OK == frm.ShowDialog())
            {
                string name = frm.textBox1.Text.Trim();
                if (name.Length < 1 || name.Length > 10)
                {
                    MessageBox.Show("分类名应1-10个字符的字符串");
                    return;
                }

                Model.Category tCategory = new Model.Category()
                {
                    Guid = Guid.NewGuid().ToString(),
                    Name = name,
                    Uid = Model.User.getCurrentUser().Guid
                };
                Model.Category.save(tCategory);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = SelectedCategory.Name;
            FrmCategoryDialog frm = new FrmCategoryDialog();
            frm.Name = name;
            if (DialogResult.OK == frm.ShowDialog())
            {
                name = frm.textBox1.Text.Trim();
                if (name.Length < 1 || name.Length > 10)
                {
                    MessageBox.Show("分类名应1-10个字符的字符串");
                    return;
                }
                SelectedCategory.Name = name;
                Model.Category.save(SelectedCategory);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.Category.delete(SelectedCategory);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            updateItems(true);
        }

        private void updateItems(bool needUpdate)
        {
            try
            {
                mCategories = ITJZ.SearchHelper.Client.Model.Category.getMenu(needUpdate).ToArray();
                listBox1.Items.Clear();
                listBox1.Items.AddRange(mCategories);
                if (mCategories.Length > 0)
                {
                    SelectedCategory = mCategories[0];
                    listBox1.SelectedIndex = 0;
                }
            }
            catch 
            {
            }
        }
    }
}