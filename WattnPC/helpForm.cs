using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WattnPC
{
    public partial class helpForm : Form
    {
        public helpForm()
        {
            InitializeComponent();
        }

        private void helpForm_Load(object sender, EventArgs e)
        {
            string html = Properties.Resources.info;
            webBrowser.DocumentText = html;
        }
    }
}
