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
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Drawing.Design;

namespace Login
{

    public partial class Reading_History : Form
    {

        public class Lich_su_doc
        {
            public string ten_truyen { get; set; }
            public int Chuong_doccuoi { get; set; }
            public string TG_doccuoi { get; set; }
            public int tong_chuong { get; set; }
            public string Tacgia { get; set; }
            public string image { get; set; }

        }

        UserCredential user;
        private Trang_chu tc;


        public Reading_History(UserCredential user, Trang_chu trang_chu)
        {
            InitializeComponent();
            this.user = user;
            tc = trang_chu;
        }
        private void Reading_History_Load(object sender, EventArgs e)
        {
            Danhsach_lsd();
        }
        Dictionary<string, object> noidung_lsd = new Dictionary<string, object>();
        List<string> idtruyen = new List<string>();
        string uid;
        private async void Danhsach_lsd()
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
            /*uid = client.User.Uid;
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
                textBox.Text = "Lỗi! Không thể tải lịch sử đọc.";
                textBox.Font = font;
                textBox.ForeColor = Color.Red;
            }
*/
            uid = client.User.Uid;

            string path = "Nguoi_dung/" + uid + "/lichsudoc";

            FirebaseResponse res = await client1.GetAsync(path);
            if (res.Body == "null")
            {
                return;
            }
            else
            {
                JObject data = JObject.Parse(res.Body);
                if (data.Count == 0)
                {
                    TextBox textBox = new TextBox();
                    Font font = new Font("League Spartan SemiBold", 24.0f, FontStyle.Bold);
                    textBox.Text = "Lỗi! Không thể tải lịch sử truyện;";
                    textBox.Font = font;
                    textBox.ForeColor = Color.Red;
                    return;
                }
                foreach (JProperty property in data.Properties())
                {
                    idtruyen.Add(property.Name);
                    noidung_lsd.Add(property.Name, property.Value);
                }
                LSD();
            }
        }
        private async void LSD()
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
                        JObject outerDict = (JObject)noidung_lsd[idtruyen];
                        string datetime = outerDict["TG_doccuoi"].ToString();
                        DateTime startDateTime = DateTime.Parse(datetime);
                        DateTime currentDateTime = DateTime.Now;
                        
                        string text_clock = "";

                        TimeSpan elapsedTime = currentDateTime - startDateTime;
                        int elapsedSeconds = (int)elapsedTime.TotalSeconds;
                        int elapsedMinutes = (int)elapsedTime.TotalMinutes;
                        int elapsedHours = (int)elapsedTime.TotalHours;
                        int elapsedDays = (int)elapsedTime.TotalDays;
                        int elapsedMonth = elapsedDays / 30;
                        int elapsedYear = elapsedMonth / 12;
                        if (elapsedYear > 0)
                        {
                            text_clock = elapsedYear.ToString() + " năm trước";
                        }
                        else if (elapsedMonth > 0)
                        {
                            text_clock = elapsedMonth.ToString() + " tháng trước";
                        }
                        else if (elapsedDays > 0)
                        {
                            text_clock = elapsedDays.ToString() + " ngày trước";
                        }
                        else if (elapsedHours > 0)
                        {
                            text_clock = elapsedHours.ToString() + " giờ trước";
                        }
                        else if (elapsedMinutes > 0)
                        {
                            text_clock = elapsedMinutes.ToString() + " phút trước";
                        }
                        else
                        {
                            text_clock = elapsedSeconds.ToString() + " giây trước";
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
                        ten_truyen.Font = new Font("League Spartan", 18F, FontStyle.Bold);
                        ten_truyen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        ten_truyen.Visible = true;

                        IconButton tacgia = new IconButton();
                        tacgia.Text = snapshot.GetValue<string>("Tac_gia");
                        tacgia.AutoSize = true;
                        tacgia.FlatAppearance.BorderSize = 0;
                        tacgia.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        tacgia.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        tacgia.FlatStyle = FlatStyle.Flat;
                        tacgia.Font = new Font("League Spartan SemiBold", 14F, FontStyle.Bold);
                        tacgia.IconChar = IconChar.UserEdit;
                        tacgia.IconColor = Color.Black;
                        tacgia.IconSize = 32;
                        tacgia.IconFont = IconFont.Auto;
                        tacgia.ImageAlign = ContentAlignment.MiddleLeft;
                        tacgia.TextAlign = ContentAlignment.MiddleLeft;
                        tacgia.TextImageRelation = TextImageRelation.ImageBeforeText;
                        tacgia.Location = new Point(anh.Width + 25, 35);
                        tacgia.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        tacgia.Visible = true;

                        Label chuong = new Label();
                        chuong.Text =  outerDict["Chuong_doccuoi"].ToString() + "/" + snapshot.GetValue<int>("So_chuong").ToString() + " chương";
                        chuong.Font = new Font("League Spartan SemiBold", 14F, FontStyle.Bold);
                        chuong.AutoSize = true;
                        chuong.Location = new Point(anh.Width + 25, 75);
                        chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        chuong.Visible = true;

                        IconButton delete = new IconButton();
                        delete.Text = "";
                        delete.AutoSize = true;
                        delete.FlatAppearance.BorderSize = 0;
                        delete.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        delete.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        delete.FlatStyle = FlatStyle.Flat;
                        delete.Font = new Font("League Spartan", 14F, FontStyle.Regular);
                        delete.IconChar = IconChar.TrashAlt;
                        delete.IconColor = Color.Black;
                        delete.IconSize = 38;
                        delete.IconFont = IconFont.Auto;
                        delete.ImageAlign = ContentAlignment.MiddleCenter;
                        delete.TextAlign = ContentAlignment.MiddleCenter;
                        delete.TextImageRelation = TextImageRelation.Overlay;
                        delete.Location = new Point(900, 3);
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
                        clock.Font = new Font("League Spartan SemiBold", 14F, FontStyle.Bold);
                        clock.IconChar = IconChar.ClockFour;
                        clock.IconColor = Color.Black;
                        clock.IconSize = 32;
                        clock.IconFont = IconFont.Auto;
                        clock.ImageAlign = ContentAlignment.MiddleLeft;
                        clock.TextAlign = ContentAlignment.MiddleLeft;
                        clock.TextImageRelation = TextImageRelation.ImageBeforeText;
                        clock.Location = new Point(anh.Width + 25, 109);
                        clock.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        clock.Visible = true;

                        IconButton Doctiep = new IconButton();
                        Doctiep.AutoSize = true;
                        Doctiep.FlatAppearance.BorderSize = 0;
                        Doctiep.BackColor = Color.FromArgb(186, 31, 31);
                        Doctiep.ForeColor = Color.White;
                        Doctiep.FlatStyle = FlatStyle.Flat;
                        Doctiep.Font = new Font("League Spartan SemiBold", 14F, FontStyle.Bold);
                        Doctiep.Size = new Size(140, 60);
                        Doctiep.Location = new Point(900, 109);
                        Doctiep.Margin = new Padding(3, 3, 3, 3);
                        Doctiep.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        Doctiep.Visible = true;
                        Doctiep.Name = "btnDoctiep";
                        Doctiep.Text = "Đọc tiếp";
                        Doctiep.IconChar = IconChar.History;
                        Doctiep.IconSize = 32;
                        Doctiep.IconColor = Color.White;
                        Doctiep.TextImageRelation = TextImageRelation.ImageBeforeText;
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
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(Config);
            string path = "Nguoi_dung/" + uid + "/lichsudoc/" + truyen_id;
            FirebaseResponse res = await client.GetAsync(path);
            if (res.Body != "null")
            {
                Lich_su_doc LS = res.ResultAs<Lich_su_doc>();
                int chuong_dang_doc = LS.Chuong_doccuoi;
                tc.openChildForm(new Doc_Truyen(truyen_id, user, chuong_dang_doc, tc));                
                this.Close();
            }
        }

        private void lbLSD_Click(object sender, EventArgs e)
        {

        }

        private void ibtnThoigian_Click(object sender, EventArgs e)
        {

        }

        private void btnDoctiep1_Click(object sender, EventArgs e)
        {

        }
    }
}
