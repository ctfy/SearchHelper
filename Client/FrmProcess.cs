using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ITJZ.SearchHelper.Client
{
    public partial class FrmProcess : Form
    {
        private FrmProcess()
        {
            InitializeComponent();
        }

        private static FrmProcess mFrmProcess;
        public static FrmProcess getInstance()
        {
            if (null==mFrmProcess)
            {
                mFrmProcess = new FrmProcess();
            }
            return mFrmProcess;
        }

        private void FrmProcess_Load(object sender, EventArgs e)
        {

        }
    
        public delegate void ExecuteMethod();

        public void Show2(ExecuteMethod method)
        {
            base.Show();
            var result = BeginInvoke(method);
            EndInvoke(result);
        }
    }
}
