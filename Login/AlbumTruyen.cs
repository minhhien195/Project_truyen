using FontAwesome.Sharp;
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
    public partial class AlbumTruyen : Form
    {
        //Fields
       // private IconButton currentBtn;
        private Panel leftBorderBtn;
       // private Form currentChildForm;
        public AlbumTruyen()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            customDesign();
            /*          //Form
                        this.Text = string.Empty;
                        this.ControlBox = false;
                        this.DoubleBuffered = true;
                        this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;*/
        }
        private void customDesign()
        {
            panelTheodoiSubmenu.Visible = false;
            panelTieudeSubmenu.Visible = false;
            panelHEALTruyenSubmenu.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelTheodoiSubmenu.Visible)
            {
                panelTheodoiSubmenu.Visible = false;
            }
            if (panelTieudeSubmenu.Visible)
            {
                panelTieudeSubmenu.Visible = false;
            }
            if (panelHEALTruyenSubmenu.Visible)
            {
                panelHEALTruyenSubmenu.Visible = false;
            }    
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                //hideSubMenu();
                subMenu.Visible = true;
            }    
            else
                subMenu.Visible = false;
        }
        private void btnbars_Click(object sender, EventArgs e)
        {
           // panelClose.Visible = true;
            CollapseMenu();
        }
        private void ibtnXmark_Click(object sender, EventArgs e)
        {
            //panelClose.Visible = false;
            CollapseMenu();
        }
        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 300 )
            {
                panelMenu.Width = 0;
                panelMenu.Visible = false;
                panelClose.Left -= 350;
                panelClose.Visible = true;
                //panelheader.
            }    
            else if (this.panelMenu.Width == 0)
            {
                panelMenu.Width = 378;
                panelMenu.Visible = true;
                panelClose.Left += 350;
                panelClose.Visible = false;
            }    
        }


        private void ibtnTheodoi_Click(object sender, EventArgs e)
        {
            showSubMenu(panelTheodoiSubmenu);
        }

        private void btnAlbum_Click(object sender, EventArgs e)
        {

        }

        private void btnBookmark_Click(object sender, EventArgs e)
        {

        }

        private void btnLichsudoc_Click(object sender, EventArgs e)
        {

        }

        private void ibtnTieude_Click(object sender, EventArgs e)
        {
            showSubMenu(panelTieudeSubmenu);
        }

        private void btnadvancedsearch_Click(object sender, EventArgs e)
        {

        }
        private void btnBXH_Click(object sender, EventArgs e)
        {

        }

        private void ibtnHEALTruyen_Click(object sender, EventArgs e)
        {
            showSubMenu(panelHEALTruyenSubmenu);
        }

        private void btnHelpandError_Click(object sender, EventArgs e)
        {

        }

        private void btnChat_Click(object sender, EventArgs e)
        {

        }

        private void btnThongbao_Click(object sender, EventArgs e)
        {

        }

        private void AlbumTruyen_Load(object sender, EventArgs e)
        {

        }



        /*private Form activeForm = null;
private void openChildform(Form childForm)
{
   if (activeForm != null)
   {
       activeForm.Close();
   }
   activeForm = childForm;
   childForm.TopLevel = false;
   childForm.FormBorderStyle = FormBorderStyle.None;
   childForm.Dock = DockStyle.Fill;
   panelChildForm.Controls.Add(childForm);
   panelChildForm.Tag = childForm;
   childForm.BringToFront();
   childForm.Show();
}*/
    }
}
