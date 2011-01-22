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
            this.ucArticle1 = new ITJZ.SearchHelper.Client.ucArticle();
            this.ucArticleList1 = new ITJZ.SearchHelper.Client.ucArticleList();
            this.ucMenu1 = new ITJZ.SearchHelper.Client.ucMenu();
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
            this.ucMenu1.SelectedCategory = null;
            this.ucMenu1.Size = new System.Drawing.Size(164, 462);
            this.ucMenu1.TabIndex = 0;
            this.ucMenu1.SelectedCategoryChanged += new System.EventHandler<ITJZ.SearchHelper.Client.ucMenu.CategoryEventArgs>(this.ucMenu1_SelectedCategoryChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 464);
            this.Controls.Add(this.ucArticle1);
            this.Controls.Add(this.ucArticleList1);
            this.Controls.Add(this.ucMenu1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ITJZ.SearchHelper.Client.ucMenu ucMenu1;
        private ITJZ.SearchHelper.Client.ucArticleList ucArticleList1;
        private ITJZ.SearchHelper.Client.ucArticle ucArticle1;


    }
}

