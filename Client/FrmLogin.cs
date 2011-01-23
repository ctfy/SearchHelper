using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITJZ.SearchHelper.Client
{
    public partial class FrmLogin : Form
    {
        private static FrmLogin mFrmLogin;
        public static FrmLogin getInstance()
        {
            if (null==mFrmLogin)
            {
                mFrmLogin = new FrmLogin();
            }
            return mFrmLogin;
        }

        private FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool success = Model.User.login(txtEmail, txtPassword);
            if (success)
            {
                DialogResult = DialogResult.Yes;
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Model.User user = Model.User.getCurrentUser();
            if (null!=user)
            {
                txtEmail.Text = user.Email;
                txtPassword.Text = user.Password;
            }
        }
    }
}
