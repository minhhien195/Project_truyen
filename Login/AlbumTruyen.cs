
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
using AlbumTruyen;
using thongbao;
using FireSharp.Config;
using FireSharp.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Auth.Providers;
using Firebase.Auth;
using System.IO;
using FireSharp.Response;
using System.Security.Cryptography;
using Google.Cloud.Firestore;

namespace Login
{
    public partial class AlbumTruyen : Form
    {
        UserCredential user;

        public AlbumTruyen(UserCredential user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void AlbumTruyen_Load(object sender, EventArgs e)
        {

        }
        Dictionary<string, Dictionary<string, object>> albumtruyen = new Dictionary<string, Dictionary<string, object>>();
        List<Dictionary<string, object>> noidung_album = new List<Dictionary<string, object>>();
        List<string> idtruyen = new List<string>();
        private List<IconButton> toggleButtons;
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
            CRUD_album album = new CRUD_album();
            Task<Dictionary<string, Dictionary<string, object>>> album1 = album.Lay_album(uid);

            var result = await album1;
            // Kiểm tra xem tác vụ có thành công không
            if (album1.Status == TaskStatus.RanToCompletion)
            {
                // Truy xuất danh sách
                albumtruyen = result;
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
            foreach (var dictionary in albumtruyen)
            {
                idtruyen.Add(dictionary.Key);
                // Truy xuất giá trị từ điển thông qua khóa
                Dictionary<string, object> innerDictionary = dictionary.Value;
                //xem xem có bao nhiêu truyện được lưu trong album
                noidung_album.Add(innerDictionary);
            }
        }
        private async void Album()
        {
            int sl_truyen = noidung_album.Count;
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
                album_name.Text = "ALBUM TRUYỆN";
                album_name.Font = new Font("League Spartan", 14F, FontStyle.Bold);
                album_name.AutoSize = true;
                album_name.Location = new Point(9, 19);
                album_name.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                album_name.Visible = true;

                Button truycap = new Button();
                truycap.AutoSize = true;
                truycap.FlatAppearance.BorderSize = 0;
                truycap.BackColor = Color.Salmon;
                truycap.FlatAppearance.MouseOverBackColor = Color.White;
                truycap.FlatAppearance.MouseDownBackColor = Color.White;
                truycap.FlatStyle = FlatStyle.Flat;
                truycap.Font = new Font("League Spartan SemiBold", 12F, FontStyle.Bold);
                truycap.ForeColor = Color.Red;
                truycap.Location = new Point(1204, 16);
                truycap.Margin = new Padding(3, 3, 3, 3);
                truycap.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                truycap.Visible = true;
                truycap.Name = "btnAccessAlbum";
                truycap.Click += btnAccessAlbum_Click;

                TextBox linkshare = new TextBox();
                linkshare.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                linkshare.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                linkshare.Name = "tbLinkShare";
                linkshare.Location = new Point(1357, 24);
                linkshare.Margin = new Padding(3, 3, 3, 3);
                linkshare.Visible = true;

                Label duong_ke = new Label();
                duong_ke.FlatStyle = FlatStyle.Flat;
                duong_ke.BorderStyle = BorderStyle.Fixed3D;
                duong_ke.Location = new Point(10, 60);
                duong_ke.Size = new Size(1804, 1);
                duong_ke.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                duong_ke.Visible = true;

                result.Controls.Add(album_name);
                result.Controls.Add(linkshare);
                result.Controls.Add(truycap);
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

                        IconButton chuong = new IconButton();
                        chuong.Text = "  " + snapshot.GetValue<int>("So_chuong").ToString() + " chương";
                        chuong.AutoSize = true;
                        chuong.FlatAppearance.BorderSize = 0;
                        chuong.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        chuong.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        chuong.FlatStyle = FlatStyle.Flat;
                        chuong.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                        chuong.IconChar = IconChar.LayerGroup;
                        chuong.IconColor = Color.Black;
                        chuong.IconSize = 32;
                        chuong.IconFont = IconFont.Auto;
                        chuong.ImageAlign = ContentAlignment.MiddleLeft;
                        chuong.TextAlign = ContentAlignment.MiddleLeft;
                        chuong.TextImageRelation = TextImageRelation.ImageBeforeText;
                        chuong.Location = new Point(anh.Width + 25, 54);
                        chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        chuong.Visible = true;

                        IconButton chuong_dangdoc = new IconButton();
                        chuong_dangdoc.Text = "  " + noidung_album[i]["Chuong_dangdoc"].ToString() + " chương";
                        chuong_dangdoc.AutoSize = true;
                        chuong_dangdoc.FlatAppearance.BorderSize = 0;
                        chuong_dangdoc.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        chuong_dangdoc.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        chuong_dangdoc.FlatStyle = FlatStyle.Flat;
                        chuong_dangdoc.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                        chuong_dangdoc.IconChar = IconChar.Bookmark;
                        chuong_dangdoc.IconColor = Color.Black;
                        chuong_dangdoc.IconSize = 32;
                        chuong_dangdoc.IconFont = IconFont.Auto;
                        chuong_dangdoc.ImageAlign = ContentAlignment.MiddleLeft;
                        chuong_dangdoc.TextAlign = ContentAlignment.MiddleLeft;
                        chuong_dangdoc.TextImageRelation = TextImageRelation.ImageBeforeText;
                        chuong_dangdoc.Location = new Point(anh.Width + 25, 98);
                        chuong_dangdoc.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        chuong_dangdoc.Visible = true;

                        TableLayoutPanel trang_thai = new TableLayoutPanel();
                        trang_thai.AutoSize = true;
                        trang_thai.Location = new Point(anh.Width + 25, 156);
                        trang_thai.Width = 340;
                        trang_thai.Height = 61;
                        trang_thai.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        trang_thai.Visible = true;
                        trang_thai.RowCount = 1;
                        trang_thai.ColumnCount = 3;
                        trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                        trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                        trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                        Button tac_gia = new Button();
                        tac_gia.AutoSize = true;
                        tac_gia.FlatAppearance.BorderSize = 4;
                        tac_gia.FlatAppearance.MouseOverBackColor = Color.White;
                        tac_gia.FlatAppearance.MouseDownBackColor = Color.White;
                        tac_gia.FlatStyle = FlatStyle.Flat;
                        tac_gia.Font = new Font("League Spartan SemiBold", 9F, FontStyle.Bold);
                        tac_gia.ForeColor = Color.Red;
                        tac_gia.Margin = new Padding(3, 3, 6, 3);
                        tac_gia.Text = snapshot.GetValue<string>("Tac_gia");
                        tac_gia.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        tac_gia.Visible = true;


                        Button trang_thai_truyen = new Button();
                        trang_thai_truyen.AutoSize = true;
                        trang_thai_truyen.FlatAppearance.BorderSize = 4;
                        trang_thai_truyen.FlatAppearance.MouseOverBackColor = Color.White;
                        trang_thai_truyen.FlatAppearance.MouseDownBackColor = Color.White;
                        trang_thai_truyen.FlatStyle = FlatStyle.Flat;
                        trang_thai_truyen.Font = new Font("League Spartan SemiBold", 9F, FontStyle.Bold);
                        trang_thai_truyen.ForeColor = Color.FromArgb(0, 110, 0);
                        trang_thai_truyen.Margin = new Padding(3, 3, 6, 3);
                        if (snapshot.GetValue<int>("Trang_thai") == 0)
                            trang_thai_truyen.Text = "Dừng cập nhật";
                        else if (snapshot.GetValue<int>("Trang_thai") == 1)
                            trang_thai_truyen.Text = "Đang tiến hành";
                        else
                            trang_thai_truyen.Text = "Hoàn thành";
                        trang_thai_truyen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        trang_thai_truyen.Visible = true;

                        Button the_loai = new Button();
                        the_loai.AutoSize = true;
                        the_loai.FlatAppearance.BorderSize = 4;
                        the_loai.FlatAppearance.MouseOverBackColor = Color.White;
                        the_loai.FlatAppearance.MouseDownBackColor = Color.White;
                        the_loai.FlatStyle = FlatStyle.Flat;
                        the_loai.Font = new Font("League Spartan SemiBold", 9F, FontStyle.Bold);
                        the_loai.ForeColor = Color.Blue;
                        the_loai.Margin = new Padding(3, 3, 10, 3);
                        the_loai.Text = snapshot.GetValue<List<string>>("The_loai")[0];
                        the_loai.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        the_loai.Visible = true;

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
                        delete.Location = new Point(1757, 14);
                        delete.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        delete.Click += deleteButton_Click;
                        delete.Visible = true;

                        IconButton thong_bao_tat = new IconButton();
                        thong_bao_tat.Text = "";
                        thong_bao_tat.AutoSize = true;
                        thong_bao_tat.FlatAppearance.BorderSize = 0;
                        thong_bao_tat.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        thong_bao_tat.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        thong_bao_tat.FlatStyle = FlatStyle.Flat;
                        thong_bao_tat.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                        thong_bao_tat.IconChar = IconChar.Bell;
                        thong_bao_tat.IconColor = Color.Black;
                        thong_bao_tat.IconSize = 32;
                        thong_bao_tat.IconFont = IconFont.Auto;
                        thong_bao_tat.ImageAlign = ContentAlignment.MiddleCenter;
                        thong_bao_tat.TextAlign = ContentAlignment.MiddleCenter;
                        thong_bao_tat.TextImageRelation = TextImageRelation.Overlay;
                        thong_bao_tat.Location = new Point(1757, 70);
                        thong_bao_tat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        thong_bao_tat.Click += Notification_turnoff_Click;
                        thong_bao_tat.Visible = true;
                        thong_bao_tat.Tag = i;
                        toggleButtons.Add(thong_bao_tat);

                        IconButton thong_bao_bat = new IconButton();
                        thong_bao_bat.Text = "";
                        thong_bao_bat.AutoSize = true;
                        thong_bao_bat.FlatAppearance.BorderSize = 0;
                        thong_bao_bat.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        thong_bao_bat.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        thong_bao_bat.FlatStyle = FlatStyle.Flat;
                        thong_bao_bat.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                        thong_bao_bat.IconChar = IconChar.BellSlash;
                        thong_bao_bat.IconColor = Color.Black;
                        thong_bao_bat.IconSize = 32;
                        thong_bao_bat.IconFont = IconFont.Auto;
                        thong_bao_bat.ImageAlign = ContentAlignment.MiddleCenter;
                        thong_bao_bat.TextAlign = ContentAlignment.MiddleCenter;
                        thong_bao_bat.TextImageRelation = TextImageRelation.Overlay;
                        thong_bao_bat.Location = new Point(1757, 70);
                        thong_bao_bat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        thong_bao_bat.Click += Notification_turnon_Click;
                        thong_bao_bat.Visible = false;
                        thong_bao_bat.Tag = i + 1;
                        toggleButtons.Add(thong_bao_bat);

                        Label space1 = new Label();
                        space1.AutoSize = true;
                        space1.Font = new Font("League Spartan", 20F, FontStyle.Regular);
                        space1.Text = " ";

                        trang_thai.Controls.Add(tac_gia);
                        trang_thai.Controls.Add(trang_thai_truyen);
                        trang_thai.Controls.Add(the_loai);

                        panel1.Controls.Add(panel2);
                        panel1.Controls.Add(ten_truyen);
                        panel1.Controls.Add(chuong);
                        panel1.Controls.Add(chuong_dangdoc);
                        panel1.Controls.Add(trang_thai);
                        panel1.Controls.Add(space1);
                        panel1.Controls.Add(delete);
                        panel1.Controls.Add(thong_bao_tat);
                        panel1.Controls.Add(thong_bao_bat);
                        panel.BringToFront();
                    }
                    i++;
                }
            }
            else
            {
                panelAlbumTruyen.Visible = false;
                ibtnChiaseAlbum.Visible = false;
            }
            
        }

        private void Thong_bao_tat_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void deleteButton_Click(object sender, EventArgs e)
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
            CRUD_album album = new CRUD_album();
            await album.xoa_album(uid, idtruyen);
        }
        private async void Notification_turnon_Click(object sender, EventArgs e)
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
            var uid = client.User.Uid;
            IconButton iconButton = (IconButton)sender;
            Panel panel = iconButton.Parent as Panel;
            // Lấy thông tin truyện từ panel
            string idtruyen = panel.Tag as string;
            int position = (int)iconButton.Tag;
            string buttonName = iconButton.Name;
            if (albumtruyen.ContainsKey(idtruyen))
            {
                Dictionary<string, object> thong_tin_truyen = albumtruyen[idtruyen];
                Them_Lay_thongbao thongbao = new Them_Lay_thongbao();
                await thongbao.Tat_Bat_thongbao_album(uid, idtruyen, thong_tin_truyen["Chuong_dangdoc"].ToString(),
                    thong_tin_truyen["Tentruyen"].ToString(), thong_tin_truyen["Tacgia"].ToString(), thong_tin_truyen["image"].ToString(),
                    thong_tin_truyen["The_loai"].ToString(), Convert.ToInt32(thong_tin_truyen["Trang_thai"].ToString()),
                    Convert.ToInt32(thong_tin_truyen["tong_chuong"].ToString()), true);
                foreach (IconButton button in toggleButtons)
                {
                    if ((int)button.Tag == position - 1)
                    {
                        button.Visible = true;
                        iconButton.Visible = false;
                        break;
                    }
                }
            }

        }
        private async void Notification_turnoff_Click(object sender, EventArgs e)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client1 = new FireSharp.FirebaseClient(Config);
            IconButton iconButton = (IconButton)sender;
            Panel panel = iconButton.Parent as Panel;
            // Lấy thông tin truyện từ panel
            string idtruyen = panel.Tag as string;
            int position = (int)iconButton.Tag;
            string buttonName = iconButton.Name;
            if (albumtruyen.ContainsKey(idtruyen))
            {
                Dictionary<string, object> thong_tin_truyen = albumtruyen[idtruyen];
                Them_Lay_thongbao thongbao = new Them_Lay_thongbao();
                await thongbao.Tat_Bat_thongbao_album(uid, idtruyen, thong_tin_truyen["Chuong_dangdoc"].ToString(),
                    thong_tin_truyen["Tentruyen"].ToString(), thong_tin_truyen["Tacgia"].ToString(), thong_tin_truyen["image"].ToString(),
                    thong_tin_truyen["The_loai"].ToString(), Convert.ToInt32(thong_tin_truyen["Trang_thai"].ToString()),
                    Convert.ToInt32(thong_tin_truyen["tong_chuong"].ToString()), false);
                foreach (IconButton button in toggleButtons)
                {
                    if ((int)button.Tag == position + 1)
                    {
                        button.Visible = true;
                        iconButton.Visible = false;
                        break;
                    }
                }
            }
        }

        private void lbTenanh1_Click(object sender, EventArgs e)
        {

        }

        private async void ibtnChiaseAlbum_Click(object sender, EventArgs e)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(Config);
            FirebaseResponse response = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Album");
            Dictionary<string, object> res = response.ResultAs<Dictionary<string, object>>();
            if (res != null)
            {
                string shareId = Guid.NewGuid().ToString();

                string shareLink = $"Nguoi_dung/{user.User.Uid}/Album";
                string key = "12345678912345678912345678912345";

                string encrypt_Share_Link = Encrypt(shareLink, key);

                //SetResponse r = await client.SetAsync("Nguoi_dung/" + user.User.Uid + "/Album/LinkShare", shareLink);
                Clipboard.SetText(encrypt_Share_Link);
                MessageBox.Show("Link đã lưu vào clipboard");
            }

        }

        static string Encrypt(string plainText, string key)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB; // Chế độ mã hóa ECB
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        static string Decrypt(string encryptedText, string key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB; // Chế độ mã hóa ECB
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        private async void btnAccessAlbum_Click(object sender, EventArgs e)
        {
            string key = "12345678912345678912345678912345";
            string sharelink = Decrypt(tbLinkShare.Text, key);
            //config firestore
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            //config realtimedb
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse res = await client.GetAsync(sharelink);
            Dictionary<string, object> result = res.ResultAs<Dictionary<string, object>>();

            if (result != null)
            {
                int i = 0;
                foreach (var item in result.Keys)
                {
                    string idTruyen = item;
                    Google.Cloud.Firestore.DocumentReference doc = db.Collection("Truyen").Document(idTruyen);
                    DocumentSnapshot snapshot = await doc.GetSnapshotAsync();

                    string imgBase64 = snapshot.GetValue<string>("Anh");
                    string tenTruyen = snapshot.GetValue<string>("Ten");
                    int tongChuong = snapshot.GetValue<int>("So_chuong");
                    string tacGia = snapshot.GetValue<string>("Tac_gia");
                    List<string> type = snapshot.GetValue<List<string>>("The_loai");
                    string theLoai = type[0];
                    int status = snapshot.GetValue<int>("Trang_thai");
                    string trangThai = "";
                    if (status == 0)
                    {
                        trangThai = "Dừng cập nhật";   
                    } else if (status == 1)
                    {
                        trangThai = "Đang ra";
                    } else if (status == 2)
                    {
                        trangThai = "Hoàn thành";
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

                    IconButton chuong = new IconButton();
                    chuong.Text = "  " + tongChuong + " chương";
                    chuong.AutoSize = true;
                    chuong.FlatAppearance.BorderSize = 0;
                    chuong.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                    chuong.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                    chuong.FlatStyle = FlatStyle.Flat;
                    chuong.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                    chuong.IconChar = IconChar.LayerGroup;
                    chuong.IconColor = Color.Black;
                    chuong.IconSize = 32;
                    chuong.IconFont = IconFont.Auto;
                    chuong.ImageAlign = ContentAlignment.MiddleLeft;
                    chuong.TextAlign = ContentAlignment.MiddleLeft;
                    chuong.TextImageRelation = TextImageRelation.ImageBeforeText;
                    chuong.Location = new Point(anh.Width + 25, 54);
                    chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    chuong.Visible = true;

                    IconButton chuong_dangdoc = new IconButton();
                    chuong_dangdoc.Text = "  " + noidung_album[i]["Chuong_dangdoc"].ToString() + " chương";
                    chuong_dangdoc.AutoSize = true;
                    chuong_dangdoc.FlatAppearance.BorderSize = 0;
                    chuong_dangdoc.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                    chuong_dangdoc.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                    chuong_dangdoc.FlatStyle = FlatStyle.Flat;
                    chuong_dangdoc.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                    chuong_dangdoc.IconChar = IconChar.Bookmark;
                    chuong_dangdoc.IconColor = Color.Black;
                    chuong_dangdoc.IconSize = 32;
                    chuong_dangdoc.IconFont = IconFont.Auto;
                    chuong_dangdoc.ImageAlign = ContentAlignment.MiddleLeft;
                    chuong_dangdoc.TextAlign = ContentAlignment.MiddleLeft;
                    chuong_dangdoc.TextImageRelation = TextImageRelation.ImageBeforeText;
                    chuong_dangdoc.Location = new Point(anh.Width + 25, 98);
                    chuong_dangdoc.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    chuong_dangdoc.Visible = true;

                    TableLayoutPanel trang_thai = new TableLayoutPanel();
                    trang_thai.AutoSize = true;
                    trang_thai.Location = new Point(anh.Width + 25, 156);
                    trang_thai.Width = 340;
                    trang_thai.Height = 61;
                    trang_thai.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    trang_thai.Visible = true;
                    trang_thai.RowCount = 1;
                    trang_thai.ColumnCount = 3;
                    trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                    Button tac_gia = new Button();
                    tac_gia.AutoSize = true;
                    tac_gia.FlatAppearance.BorderSize = 4;
                    tac_gia.FlatAppearance.MouseOverBackColor = Color.White;
                    tac_gia.FlatAppearance.MouseDownBackColor = Color.White;
                    tac_gia.FlatStyle = FlatStyle.Flat;
                    tac_gia.Font = new Font("League Spartan SemiBold", 9F, FontStyle.Bold);
                    tac_gia.ForeColor = Color.Red;
                    tac_gia.Margin = new Padding(3, 3, 6, 3);
                    tac_gia.Text = tacGia;
                    tac_gia.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    tac_gia.Visible = true;


                    Button trang_thai_truyen = new Button();
                    trang_thai_truyen.AutoSize = true;
                    trang_thai_truyen.FlatAppearance.BorderSize = 4;
                    trang_thai_truyen.FlatAppearance.MouseOverBackColor = Color.White;
                    trang_thai_truyen.FlatAppearance.MouseDownBackColor = Color.White;
                    trang_thai_truyen.FlatStyle = FlatStyle.Flat;
                    trang_thai_truyen.Font = new Font("League Spartan SemiBold", 9F, FontStyle.Bold);
                    trang_thai_truyen.ForeColor = Color.FromArgb(0, 110, 0);
                    trang_thai_truyen.Margin = new Padding(3, 3, 6, 3);
                    trang_thai_truyen.Text = trangThai;
                    trang_thai_truyen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    trang_thai_truyen.Visible = true;

                    Button the_loai = new Button();
                    the_loai.AutoSize = true;
                    the_loai.FlatAppearance.BorderSize = 4;
                    the_loai.FlatAppearance.MouseOverBackColor = Color.White;
                    the_loai.FlatAppearance.MouseDownBackColor = Color.White;
                    the_loai.FlatStyle = FlatStyle.Flat;
                    the_loai.Font = new Font("League Spartan SemiBold", 9F, FontStyle.Bold);
                    the_loai.ForeColor = Color.Blue;
                    the_loai.Margin = new Padding(3, 3, 10, 3);
                    the_loai.Text = theLoai;
                    the_loai.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    the_loai.Visible = true;

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
                    delete.Location = new Point(1757, 14);
                    delete.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    delete.Click += deleteButton_Click;
                    delete.Visible = true;

                    IconButton thong_bao_tat = new IconButton();
                    thong_bao_tat.Text = "";
                    thong_bao_tat.AutoSize = true;
                    thong_bao_tat.FlatAppearance.BorderSize = 0;
                    thong_bao_tat.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                    thong_bao_tat.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                    thong_bao_tat.FlatStyle = FlatStyle.Flat;
                    thong_bao_tat.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                    thong_bao_tat.IconChar = IconChar.Bell;
                    thong_bao_tat.IconColor = Color.Black;
                    thong_bao_tat.IconSize = 32;
                    thong_bao_tat.IconFont = IconFont.Auto;
                    thong_bao_tat.ImageAlign = ContentAlignment.MiddleCenter;
                    thong_bao_tat.TextAlign = ContentAlignment.MiddleCenter;
                    thong_bao_tat.TextImageRelation = TextImageRelation.Overlay;
                    thong_bao_tat.Location = new Point(1757, 70);
                    thong_bao_tat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    thong_bao_tat.Click += Notification_turnoff_Click;
                    thong_bao_tat.Visible = true;
                    thong_bao_tat.Tag = i;
                    toggleButtons.Add(thong_bao_tat);

                    IconButton thong_bao_bat = new IconButton();
                    thong_bao_bat.Text = "";
                    thong_bao_bat.AutoSize = true;
                    thong_bao_bat.FlatAppearance.BorderSize = 0;
                    thong_bao_bat.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                    thong_bao_bat.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                    thong_bao_bat.FlatStyle = FlatStyle.Flat;
                    thong_bao_bat.Font = new Font("League Spartan", 9F, FontStyle.Regular);
                    thong_bao_bat.IconChar = IconChar.BellSlash;
                    thong_bao_bat.IconColor = Color.Black;
                    thong_bao_bat.IconSize = 32;
                    thong_bao_bat.IconFont = IconFont.Auto;
                    thong_bao_bat.ImageAlign = ContentAlignment.MiddleCenter;
                    thong_bao_bat.TextAlign = ContentAlignment.MiddleCenter;
                    thong_bao_bat.TextImageRelation = TextImageRelation.Overlay;
                    thong_bao_bat.Location = new Point(1757, 70);
                    thong_bao_bat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    thong_bao_bat.Click += Notification_turnon_Click;
                    thong_bao_bat.Visible = false;
                    thong_bao_bat.Tag = i + 1;
                    toggleButtons.Add(thong_bao_bat);

                    Label space1 = new Label();
                    space1.AutoSize = true;
                    space1.Font = new Font("League Spartan", 20F, FontStyle.Regular);
                    space1.Text = " ";

                    trang_thai.Controls.Add(tac_gia);
                    trang_thai.Controls.Add(trang_thai_truyen);
                    trang_thai.Controls.Add(the_loai);

                    panel1.Controls.Add(panel2);
                    panel1.Controls.Add(ten_truyen);
                    panel1.Controls.Add(chuong);
                    panel1.Controls.Add(chuong_dangdoc);
                    panel1.Controls.Add(trang_thai);
                    panel1.Controls.Add(space1);
                    panel1.Controls.Add(delete);
                    panel1.Controls.Add(thong_bao_tat);
                    panel1.Controls.Add(thong_bao_bat);
                    panel.BringToFront();

                    i++;

                    //MessageBox.Show(flowLayoutPanel.Location.Y.ToString());
                }
            }
        }

    }
}
