using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinProxyManager
{
    public partial class APIFrm : Form
    {
        public APIFrm()
        {
            InitializeComponent();
        }

        public APIFrm(string msg)
        {
            InitializeComponent();
            this.txtMsg.Text = msg;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
