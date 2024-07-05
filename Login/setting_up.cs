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
using FontAwesome;

namespace Login
{
    public partial class setting_up : Form
    {

        UserCredential user;
        FirebaseAuthClient client;
        IFirebaseClient ifclient;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };


        private Trang_chu tc;

        public setting_up(UserCredential userCredential, FirebaseAuthClient firebaseAuthClient, Trang_chu trangchu)
        {
            InitializeComponent();
            ifclient = new FireSharp.FirebaseClient(config);
            this.user = userCredential;
            this.client = firebaseAuthClient;
            tc = trangchu;

        }

        private void btnout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void setting_up_Load(object sender, EventArgs e)
        {
            try
            {
                string ID = user.User.Info.Uid;
                GraphicsPath grpath = new GraphicsPath();
                grpath.AddEllipse(0, 0, pbAvt.Width, pbAvt.Height);
                Region rg = new Region(grpath);
                pbAvt.Region = rg;

                string path_name = "Nguoi_dung/" + ID + "/TK_dangnhap";
                FirebaseResponse res = await ifclient.GetAsync(path_name);
                lbname.Text = res.ResultAs<string>();
                /*lbname.TextAlign = ContentAlignment.MiddleCenter;*/
                lbname.Left = (this.Width - lbname.Width) / 2;

                string path_vaitro = "Nguoi_dung/" + ID + "/Vaitro";
                FirebaseResponse res2 = await ifclient.GetAsync(path_vaitro);
                try
                {
                    int vaitro = res2.ResultAs<int>();
                    if (vaitro == 1)
                    {
                        lbvaitro.Text = "Người dùng";
                        btnloi.Visible = true;
                        btnDuyet.Visible = false;
                    }
                    else if (vaitro == 0)
                    {
                        lbvaitro.Text = "Adminstration";
                        btnloi.Visible = false;
                        btnDuyet.Visible = true;
                        btnXLVP.Visible = true;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
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
            catch
            {
                MessageBox.Show("Lỗi!");
            }
        }

        private async void btnDX_Click(object sender, EventArgs e)
        {
            try
            {
                // Đăng xuất khỏi tài khoản Firebase
                client.SignOut();

                Login tb = new Login();
                this.Close();
                tc.Close();
                tb.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng xuất thất bại: " + ex.Message);
            }
        }

        private void btnXLVP_Click(object sender, EventArgs e)
        {
            Xuli_vipham vp = new Xuli_vipham();
            vp.Show();
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            tc.change_color();
            this.Close();
            tc.openChildForm(new Setting(user, client));
        }

        private void btnuser_Click(object sender, EventArgs e)
        {
            tc.change_color();
            this.Close();
            tc.openChildForm(new AccountInfo(user, client));
        }

        private void btnlsd_Click(object sender, EventArgs e)
        {
            tc.btnLichsudoc_Click(sender, e);
            this.Close();
        }

        private void btnalbum_Click(object sender, EventArgs e)
        {
            tc.change_color();
            this.Close();
            tc.openChildForm(new AlbumTruyen(user));
        }

        private void btndangt_Click(object sender, EventArgs e)
        {
            tc.change_color();
            this.Close();
            tc.openChildForm(new InsertNovel(user));
        }

        private void btntbao_Click(object sender, EventArgs e)
        {
            tc.btnThongbao_Click(sender, e);
            this.Close();
        }

        private void btnloi_Click(object sender, EventArgs e)
        {
            tc.btnHelpandError_Click(sender, e);
            this.Close();
        }

        private void btDuyet_Click(object sender, EventArgs e)
        {
            tc.change_color();
            this.Close();
            tc.openChildForm(new Duyet_Truyen(user));
        }
    }
}
