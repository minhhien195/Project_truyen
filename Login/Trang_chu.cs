using Firebase.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static Login.Signup;

namespace Login
{
    public partial class Trang_chu : Form
    {
        bool isLogin = false;
        UserCredential user;
        FirebaseAuthClient client;
        IFirebaseClient ifclient;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        //Fields
        // private IconButton currentBtn;
        private Panel leftBorderBtn;
        // private Form currentChildForm;
        public Trang_chu(UserCredential userCredential, FirebaseAuthClient firebaseAuthClient, bool check)
        {
            isLogin = check;
            InitializeComponent();
            ifclient = new FireSharp.FirebaseClient(config);
            this.user = userCredential;
            this.client = firebaseAuthClient;
            
        }

        public Trang_chu()
        {
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

        private async void Trangchu_Load(object sender, EventArgs e)
        {
            try{
                if (isLogin)
                {
                    string ID = user.User.Info.Uid;
                    GraphicsPath grpath = new GraphicsPath();
                    grpath.AddEllipse(0, 0, pbAvt.Width, pbAvt.Height);
                    Region rg = new Region(grpath);
                    pbAvt.Region = rg;
                    string path_pic = "Nguoi_dung/" + ID + "/Anh_dai_dien";
                    FirebaseResponse response = await ifclient.GetAsync(path_pic);
                    string base64String = response.ResultAs<string>();
                    byte[] imageBytes = Convert.FromBase64String(base64String);

                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        Bitmap bitmap = new Bitmap(memoryStream);
                        pbAvt.Image = bitmap;
                        pbAvt.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch {
                MessageBox.Show("Lỗi!");
            }
            
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
            if (this.panelMenu.Width >= 250)
            {
                panelMenu.Width = 0;
                panelMenu.Visible = false;
                panelClose.Left -= 250;
                panelClose.Visible = true;
                lbdecu.Left -= 250;
                lbup.Left -= 250;
                btnLeft.Left -= 320;
                truyen1.Left -= 340;
                truyen2.Left -= 340;
                truyen3.Left -= 340;
                truyen4.Left -= 340;
                sangtac.Left -= 230;
                dangxem.Left -= 115;
                truyen5.BackColor = System.Drawing.Color.FromArgb(155, 227, 243);
                //panelheader.
            }
            else if (this.panelMenu.Width == 0)
            {
                panelMenu.Width = 250;
                panelMenu.Visible = true;
                panelClose.Left += 250;
                panelClose.Visible = false;
                lbdecu.Left += 250;
                lbup.Left += 250;
                btnLeft.Left += 320;
                truyen1.Left += 340;
                truyen2.Left += 340;
                truyen3.Left += 340;
                truyen4.Left += 340;
                sangtac.Left += 230;
                dangxem.Left += 115;
                truyen5.BackColor = System.Drawing.Color.FromArgb(220, 247, 253);
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
