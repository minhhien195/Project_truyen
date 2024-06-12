using AlbumTruyen;
using Firebase.Auth.Providers;
using Firebase.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FontAwesome.Sharp;
using Google.Cloud.Firestore;
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
using Readinghistory;
using Google.Protobuf;

namespace Login
{
    public partial class Reading_History : Form
    {
        public Reading_History()
        {
            InitializeComponent();
        }
        private void Reading_History_Load(object sender, EventArgs e)
        {
            Danhsach_album();
            Khoi_tao_album();
            Album();
        }
        Dictionary<string, Dictionary<string, object>> lsdtruyen = new Dictionary<string, Dictionary<string, object>>();
        List<Dictionary<string, object>> noidung_lsd = new List<Dictionary<string, object>>();
        List<string> idtruyen = new List<string>();
        string uid;
        private async void Danhsach_album()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
                AuthDomain = "healtruyen.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };
            var client = new FirebaseAuthClient(config);
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client1 = new FireSharp.FirebaseClient(Config);
            uid = client.User.Uid;
            CRUD_lsd lsd = new CRUD_lsd();
            Task<Dictionary<string, Dictionary<string, object>>> lsd1 = lsd.Lay_lichsudoc(uid);

            var result = await lsd1;
            // Kiểm tra xem tác vụ có thành công không
            if (lsd1.Status == TaskStatus.RanToCompletion)
            {
                // Truy xuất danh sách
                lsdtruyen = result;
            }
            else
            {
                TextBox textBox = new TextBox();
                Font font = new Font("League Spartan SemiBold", 24.0f, FontStyle.Bold);
                textBox.Text = "Lỗi! Không thể tải Album truyện.";
                textBox.Font = font;
                textBox.ForeColor = Color.Red;
            }

        }
        private void Khoi_tao_album()
        {
            // Truy xuất từ điển trong mỗi phần tử
            foreach (var dictionary in lsdtruyen)
            {
                idtruyen.Add(dictionary.Key);
                // Truy xuất giá trị từ điển thông qua khóa
                Dictionary<string, object> innerDictionary = dictionary.Value;
                //xem xem có bao nhiêu truyện được lưu trong album
                noidung_lsd.Add(innerDictionary);
            }
        }
        private async void Album()
        {
            int sl_truyen = noidung_lsd.Count;
            if (sl_truyen != 0)
            {
                Panel result = new Panel();
                result.Dock = DockStyle.Top;
                result.AutoSize = true;
                result.Size = new Size(1898, 893);
                result.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                result.Visible = true;
                result.Margin = new Padding(0, 0, 0, 50);
                result.Height = Convert.ToInt32(this.Height / 14);
                this.Controls.Add(result);

                Label space = new Label();
                space.AutoSize = true;
                space.Font = new Font("League Spartan", 23F, FontStyle.Regular);
                space.Text = " ";

                Label album_name = new Label();
                album_name.Text = "LỊCH SỬ ĐỌC";
                album_name.Font = new Font("League Spartan", 14F, FontStyle.Bold);
                album_name.AutoSize = true;
                album_name.Location = new Point(9, 19);
                album_name.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                album_name.Visible = true;

                Label duong_ke = new Label();
                duong_ke.FlatStyle = FlatStyle.Flat;
                duong_ke.BorderStyle = BorderStyle.Fixed3D;
                duong_ke.Location = new Point(10, 60);
                duong_ke.Size = new Size(1804, 1);
                duong_ke.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                duong_ke.Visible = true;

                result.Controls.Add(album_name);
                result.Controls.Add(duong_ke);
                result.Controls.Add(space);

                int i = 0;
                foreach (var idtruyen in idtruyen)
                {
                    FirestoreDb db = FirestoreDb.Create("healtruyen");
                    DocumentReference docReference = db.Collection("Truyen").Document(idtruyen);
                    DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
                    if (snapshot.Exists)
                    {
                        string datetime = noidung_lsd[i]["TG_doccuoi"].ToString();
                        DateTime startDateTime = DateTime.Parse(datetime);
                        DateTime currentDateTime = DateTime.Now;
                        TimeSpan elapsedTime = currentDateTime - startDateTime;
                        int elapsedMinutes = (int)elapsedTime.TotalMinutes;
                        int elapsedHours = (int)elapsedTime.TotalHours;
                        int elapsedDays = (int)elapsedTime.TotalDays;

                        int elapsedMonth = elapsedDays / 30;

                        string text_clock = "";

                        if(elapsedMonth > 0)
                        {
                            text_clock = elapsedDays.ToString() + "tháng trước";
                        }
                        else if (elapsedDays != 0)
                        {
                            text_clock = elapsedDays.ToString() + "ngày trước";
                        }
                        else if (elapsedHours != 0)
                        {
                            text_clock = elapsedHours.ToString() + "giờ trước";
                        }

                        Panel panel = new Panel();
                        panel.Dock = DockStyle.Top;
                        panel.AutoScroll = true;
                        panel.AutoSize = true;
                        panel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        panel.Visible = true;
                        this.Controls.Add(panel);

                        Panel panel1 = new Panel();
                        panel1.Dock = DockStyle.Top;
                        panel1.AutoSize = true;
                        panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        panel1.Visible = true;
                        panel1.Tag = idtruyen;

                        panel.Controls.Add(panel1);

                        Panel panel2 = new Panel();
                        panel2.Location = new Point(0, 0);
                        panel2.Width = 149;
                        panel2.Height = 183;
                        panel2.Padding = new Padding(10, 0, 0, 0);
                        panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        panel2.Visible = true;

                        // Chuyển chuỗi Base64 thành mảng byte
                        byte[] imageBytes = Convert.FromBase64String(snapshot.GetValue<string>("Anh"));
                        // Tạo MemoryStream từ mảng byte
                        Image image;
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            // Đọc hình ảnh từ MemoryStream
                            image = Image.FromStream(ms);

                        }
                        PictureBox anh = new PictureBox();
                        anh.Location = new Point(0, 0);
                        anh.Dock = DockStyle.Fill;
                        anh.SizeMode = PictureBoxSizeMode.StretchImage;
                        anh.Image = image;
                        anh.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        anh.Visible = true;

                        panel2.Controls.Add(anh);

                        Label ten_truyen = new Label();
                        ten_truyen.Text = snapshot.GetValue<string>("Ten");
                        ten_truyen.AutoSize = true;
                        ten_truyen.Location = new Point(anh.Width + 25, 3);
                        ten_truyen.Font = new Font("League Spartan", 12F, FontStyle.Bold);
                        ten_truyen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        ten_truyen.Visible = true;

                        IconButton tacgia = new IconButton();
                        tacgia.Text = snapshot.GetValue<string>("Tac_gia");
                        tacgia.AutoSize = true;
                        tacgia.FlatAppearance.BorderSize = 0;
                        tacgia.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        tacgia.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        tacgia.FlatStyle = FlatStyle.Flat;
                        tacgia.Font = new Font("League Spartan SemiBold", 10F, FontStyle.Bold);
                        tacgia.IconChar = IconChar.UserEdit;
                        tacgia.IconColor = Color.Black;
                        tacgia.IconSize = 32;
                        tacgia.IconFont = IconFont.Auto;
                        tacgia.ImageAlign = ContentAlignment.MiddleLeft;
                        tacgia.TextAlign = ContentAlignment.MiddleLeft;
                        tacgia.TextImageRelation = TextImageRelation.ImageBeforeText;
                        tacgia.Location = new Point(anh.Width + 25, 54);
                        tacgia.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        tacgia.Visible = true;

                        Label chuong = new Label();
                        chuong.Text = snapshot.GetValue<int>("So_chuong").ToString() + "/" + noidung_lsd[i]["Chuong_doccuoi"].ToString();
                        chuong.Font = new Font("League Spartan SemiBold", 10F, FontStyle.Bold);
                        chuong.AutoSize = true;
                        chuong.Location = new Point(9, 19);
                        chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        chuong.Visible = true;

                        IconButton delete = new IconButton();
                        delete.Text = "";
                        delete.AutoSize = true;
                        delete.FlatAppearance.BorderSize = 0;
                        delete.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        delete.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        delete.FlatStyle = FlatStyle.Flat;
                        delete.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                        delete.IconChar = IconChar.TrashAlt;
                        delete.IconColor = Color.Black;
                        delete.IconSize = 38;
                        delete.IconFont = IconFont.Auto;
                        delete.ImageAlign = ContentAlignment.MiddleCenter;
                        delete.TextAlign = ContentAlignment.MiddleCenter;
                        delete.TextImageRelation = TextImageRelation.Overlay;
                        delete.Location = new Point(1753, 14);
                        delete.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        delete.Click += DeleteButton_Click;
                        delete.Visible = true;

                        IconButton clock = new IconButton();
                        clock.Text = text_clock;
                        clock.AutoSize = true;
                        clock.FlatAppearance.BorderSize = 0;
                        clock.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        clock.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        clock.FlatStyle = FlatStyle.Flat;
                        clock.Font = new Font("League Spartan SemiBold", 10F, FontStyle.Bold);
                        clock.IconChar = IconChar.ClockFour;
                        clock.IconColor = Color.Black;
                        clock.IconSize = 32;
                        clock.IconFont = IconFont.Auto;
                        clock.ImageAlign = ContentAlignment.MiddleLeft;
                        clock.TextAlign = ContentAlignment.MiddleLeft;
                        clock.TextImageRelation = TextImageRelation.ImageBeforeText;
                        clock.Location = new Point(1635, 55);
                        clock.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        clock.Visible = true;

                        Button Doctiep = new Button();
                        Doctiep.AutoSize = true;
                        Doctiep.FlatAppearance.BorderSize = 0;
                        Doctiep.BackColor = Color.FromArgb(186, 31, 31);
                        Doctiep.ForeColor = Color.White;
                        Doctiep.FlatStyle = FlatStyle.Flat;
                        Doctiep.Font = new Font("League Spartan SemiBold", 10F, FontStyle.Bold);
                        Doctiep.ForeColor = Color.Red;
                        Doctiep.Location = new Point(1667, 125);
                        Doctiep.Margin = new Padding(3, 3, 3, 3);
                        Doctiep.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        Doctiep.Visible = true;
                        Doctiep.Name = "btnDoctiep";
                        Doctiep.Click += btnDoctiep_Click;

                        Label space1 = new Label();
                        space1.AutoSize = true;
                        space1.Font = new Font("League Spartan", 20F, FontStyle.Regular);
                        space1.Text = " ";


                        panel1.Controls.Add(panel2);
                        panel1.Controls.Add(ten_truyen);
                        panel1.Controls.Add(chuong);
                        panel1.Controls.Add(tacgia);
                        panel1.Controls.Add(space1);
                        panel1.Controls.Add(delete);
                        panel1.Controls.Add(clock);
                        panel1.Controls.Add(Doctiep);
                        panel.BringToFront();
                    }
                    i++;
                }
            }
            else
            {
                panelLsdTruyen.Visible = false;
            }

        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
                AuthDomain = "healtruyen.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };
            var client = new FirebaseAuthClient(config);
            var uid = client.User.Uid;
            IconButton deleteButton = (IconButton)sender; // Ép kiểu sender thành Button
            System.Windows.Forms.Panel panel = deleteButton.Parent as System.Windows.Forms.Panel;
            if (panel != null)
            {
                // Lấy thông tin truyện từ panel
                string idtruyen = panel.Tag as string;

                if (!string.IsNullOrEmpty(idtruyen))
                {
                    // Thực hiện xóa truyện dựa trên truyenId
                    Xoa_truyen_theo_idtruyen(idtruyen, uid);

                    // Xóa panel khỏi giao diện người dùng
                    panel.Parent.Controls.Remove(panel);
                }
            }
        }
        private async void Xoa_truyen_theo_idtruyen(string idtruyen, string uid)
        {
            CRUD_lsd lsd = new CRUD_lsd();
            await lsd.xoa_lichsudoc(uid, idtruyen);
        }

        private async void btnDoctiep_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            System.Windows.Forms.Panel panel = button.Parent as System.Windows.Forms.Panel;
            // Lấy thông tin truyện từ panel
            string truyen_id = panel.Tag as string;
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            DocumentReference docReference = db.Collection("Truyen").Document(truyen_id);
            DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                string ten_truyen = snapshot.GetValue<string>("Ten");
                string chuong_dang_doc = snapshot.GetValue<int>("So_chuong").ToString();
                //Doctruyen doctruyen = new Doctruyen(ten_truyen, truyen_id, chuong_dang_doc);
                //doctruyen.Show();
                this.Hide();
            }
        }

        private void lbLSD_Click(object sender, EventArgs e)
        {

        }

        private void ibtnThoigian_Click(object sender, EventArgs e)
        {

        }
    }
}
