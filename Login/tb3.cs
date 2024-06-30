using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class tb3 : Form
    {
        public tb3()
        {
            InitializeComponent();
        }

        private void btnyes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnno_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
