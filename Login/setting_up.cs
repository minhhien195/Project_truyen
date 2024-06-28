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


        Trang_chu tc;

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
                    }
                    else if (vaitro == 0)
                    {
                        lbvaitro.Text = "Adminstration";
                        btnloi.Visible = false;
                        IconButton btDuyet = new IconButton()
                        {
                            Name = "btnDuyet",
                            Font = new Font("League Spartan", 16f, FontStyle.Regular),
                            AutoSize = false,
                            FlatAppearance = {
                                BorderSize = 0,
                            },
                            FlatStyle = FlatStyle.Flat,
                            Location = new Point(-10, 390),
                            Size = new Size(210, 40),
                            Margin = new Padding(3, 3, 3, 3),
                            Text = " Duyệt truyện",
                            IconChar = IconChar.ArrowAltCircleUp,
                            IconColor = Color.Black,
                            IconSize = 26,
                            IconFont = IconFont.Auto,
                            TextAlign = ContentAlignment.MiddleLeft,
                            TextImageRelation = TextImageRelation.ImageBeforeText,
                        };
                        this.Controls.Add(btDuyet);
                    }
                }
                catch(Exception ex) {

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

        private void btnKick_Click(object sender, EventArgs e)
        {
            tb3 tb = new tb3();
            tb.ShowDialog();
        }
    }
}
