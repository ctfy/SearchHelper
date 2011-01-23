namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            ITJZ.SearchHelper.Client.Model.Category category1 = new ITJZ.SearchHelper.Client.Model.Category();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ucArticle1 = new ITJZ.SearchHelper.Client.ucArticle();
            this.ucArticleList1 = new ITJZ.SearchHelper.Client.ucArticleList();
            this.ucMenu1 = new ITJZ.SearchHelper.Client.ucMenu();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucArticle1
            // 
            this.ucArticle1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucArticle1.Location = new System.Drawing.Point(332, 1);
            this.ucArticle1.Name = "ucArticle1";
            this.ucArticle1.Size = new System.Drawing.Size(567, 462);
            this.ucArticle1.TabIndex = 2;
            // 
            // ucArticleList1
            // 
            this.ucArticleList1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ucArticleList1.CurrentArticles = null;
            this.ucArticleList1.CurrentCategory = null;
            this.ucArticleList1.Location = new System.Drawing.Point(171, 1);
            this.ucArticleList1.Name = "ucArticleList1";
            this.ucArticleList1.Size = new System.Drawing.Size(150, 451);
            this.ucArticleList1.TabIndex = 1;
            this.ucArticleList1.SelectArticleChanged += new System.EventHandler<ITJZ.SearchHelper.Client.ucArticleList.ArticleEventArgs>(this.ucArticleList1_SelectArticleChanged);
            // 
            // ucMenu1
            // 
            this.ucMenu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ucMenu1.Location = new System.Drawing.Point(1, 1);
            this.ucMenu1.Name = "ucMenu1";
            category1.Guid = "category-guid-0";
            category1.Name = "分类0";
            category1.Uid = "user-guid-1";
            this.ucMenu1.SelectedCategory = category1;
            this.ucMenu1.Size = new System.Drawing.Size(164, 462);
            this.ucMenu1.TabIndex = 0;
            this.ucMenu1.SelectedCategoryChanged += new System.EventHandler<ITJZ.SearchHelper.Client.ucMenu.CategoryEventArgs>(this.ucMenu1_SelectedCategoryChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 442);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(907, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(42, 20);
            this.toolStripDropDownButton1.Text = "登陆";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 464);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ucArticle1);
            this.Controls.Add(this.ucArticleList1);
            this.Controls.Add(this.ucMenu1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ITJZ.SearchHelper.Client.ucMenu ucMenu1;
        private ITJZ.SearchHelper.Client.ucArticleList ucArticleList1;
        private ITJZ.SearchHelper.Client.ucArticle ucArticle1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;


    }
}

