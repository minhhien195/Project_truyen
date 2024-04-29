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
    public partial class Bang_Xep_Hang : Form
    {
        public Bang_Xep_Hang()
        {
            InitializeComponent();
        }

        private void btnTrend_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childFormPanel.Controls.Add(childForm);
            childFormPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void iconButtonTrend_Click(object sender, EventArgs e)
        {
            iconButtonTrend.BackColor = Color.FromArgb(191, 44, 36);
            iconButtonTrend.ForeColor = Color.White;
            iconButtonTrend.IconColor = Color.White;

            iconButtonRead.BackColor = Color.White;
            iconButtonRead.ForeColor = Color.Black;
            iconButtonRead.IconColor = Color.Black;

            iconButtonRecom.BackColor = Color.White;
            iconButtonRecom.ForeColor = Color.Black;
            iconButtonRecom.IconColor = Color.Black;

            activeForm.Close();
            panelList1.Visible = true;
        }

        private void iconButtonRead_Click(object sender, EventArgs e)
        {
            panelList1.Visible = false;
            iconButtonRead.BackColor = Color.FromArgb(191, 44, 36);
            iconButtonRead.ForeColor = Color.White;
            iconButtonRead.IconColor = Color.White;

            iconButtonTrend.BackColor = Color.White;
            iconButtonTrend.ForeColor = Color.Black;
            iconButtonTrend.IconColor = Color.Black;

            iconButtonRecom.BackColor = Color.White;
            iconButtonRecom.ForeColor = Color.Black;
            iconButtonRecom.IconColor = Color.Black;

            openChildForm(new BXH_Doc_Nhieu());
        }

        private void iconButtonRecom_Click(object sender, EventArgs e)
        {
            panelList1.Visible = false;
            iconButtonRecom.BackColor = Color.FromArgb(191, 44, 36);
            iconButtonRecom.ForeColor = Color.White;
            iconButtonRecom.IconColor = Color.White;

            iconButtonTrend.BackColor = Color.White;
            iconButtonTrend.ForeColor = Color.Black;
            iconButtonTrend.IconColor = Color.Black;

            iconButtonRead.BackColor = Color.White;
            iconButtonRead.ForeColor = Color.Black;
            iconButtonRead.IconColor = Color.Black;

            openChildForm(new BXH_De_Cu());
        }

        private void iconButton3_MouseEnter(object sender, EventArgs e)
        {
            iconButton3.BackColor = Color.Transparent;
        }

        private void iconButton3_MouseHover(object sender, EventArgs e)
        {
            iconButton3.BackColor = Color.Transparent;
        }
    }
}
