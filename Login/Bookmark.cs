using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FontAwesome.Sharp;
using Google.Cloud.Firestore;

namespace Login
{
    public partial class Bookmark : Form
    {
        

        UserCredential user;
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public Bookmark(UserCredential user)
        {
            InitializeComponent();
            this.user = user;
            client = new FireSharp.FirebaseClient(config);
        }

        private async void Bookmark_Load(object sender, EventArgs e)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            List<string> result = new List<string>();
            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Bookmark");
            //MessageBox.Show("Nguoi_dung/" + user.User.Uid + "/Bookmark");
            //get data
            Dictionary<string, object> data = response.ResultAs<Dictionary<string, object>>();
            foreach (var item in data)
            {
                string idTruyen = item.Key;
                DocumentReference doc = db.Collection("Truyen").Document(idTruyen);
                DocumentSnapshot snapshot = await doc.GetSnapshotAsync();

                
                string imgBase64 = snapshot.GetValue<string>("Anh");
                FirebaseResponse res = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Bookmark/" + idTruyen + "/Chuong_Dang_Doc");
                string idChuongDangDoc = res.ResultAs<string>();
                Panel panel = new Panel();
                this.Controls.Add(panel);
                panel.AutoSize = true;
                panel.Dock = DockStyle.Top;

                Panel panelPicture = new Panel();
                panelPicture.AutoSize = true;
                panelPicture.Location = new Point (0, 0);
                panel.Controls.Add(panelPicture);
                panelPicture.Width = 130;
                panelPicture.Height = 177;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;

                byte[] imageBytes = Convert.FromBase64String(imgBase64);

                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(memoryStream);
                    pictureBox.Image = bitmap;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                panelPicture.Controls.Add(pictureBox);

                Label labelName = new Label();
                labelName.Font = new Font("League Spartan", 18, FontStyle.Regular);
                labelName.Location = new Point(panelPicture.Width + 9, 3);
                labelName.Text = snapshot.GetValue<string>("Ten");
                labelName.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                labelName.AutoSize = true;

                Label labelChuong = new Label();
                labelChuong.Font = new Font("League Spartan", 16, FontStyle.Regular);
                labelChuong.Location = new Point(panelPicture.Width + 9, labelName.Location.Y + labelName.Height + 33);
                labelChuong.Text = "Chương đánh dấu: " + int.Parse(idChuongDangDoc).ToString();
                labelChuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                labelChuong.AutoSize = true;

                IconButton iconButton = new IconButton();
                iconButton.Width = 215;
                iconButton.Height = 59;
                iconButton.IconChar = IconChar.TrashAlt;
                iconButton.Text = "Xóa Bookmark";
                iconButton.IconColor = Color.White;
                iconButton.IconSize = 48;
                iconButton.Font = new Font("League Spartan", 14, FontStyle.Regular);
                iconButton.ForeColor = Color.White;
                iconButton.BackColor = Color.FromArgb(191, 44, 36);
                iconButton.TextImageRelation = TextImageRelation.ImageBeforeText;
                iconButton.Location = new Point(panel.Width - iconButton.Width - 3, panel.Height - iconButton.Height - 3);
                iconButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                iconButton.FlatStyle = FlatStyle.Flat;
                iconButton.FlatAppearance.BorderSize = 0;

                panel.Controls.Add(iconButton);
                panel.Controls.Add(labelChuong);
                panel.Controls.Add(labelName);

                iconButton.Click += async (s, ev) =>
                {
                    panel.Visible = false;
                    await client.DeleteAsync("Nguoi_dung/" + user.User.Uid + "/Bookmark");
                };
            }
        }
    }
}
