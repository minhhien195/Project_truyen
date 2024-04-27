using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Login
{
    public partial class AlbumTruyen : Form
    {

        public AlbumTruyen()
        {
            InitializeComponent();
        }

        private void AlbumTruyen_Load(object sender, EventArgs e)
        {
            rtbTimtrangAlbum.SelectionAlignment = HorizontalAlignment.Center;

        }

        private void lbTenanh1_MouseHover(object sender, EventArgs e)
        {
            lbTenanh1.ForeColor = Color.Red;
        }

        private void lbTenanh1_MouseClick(object sender, MouseEventArgs e)
        {
            lbTenanh1.ForeColor = Color.Red;
        }

        private void lbTenanh1_MouseLeave(object sender, EventArgs e)
        {
            lbTenanh1.ForeColor = Color.Black;
        }

    }
}
