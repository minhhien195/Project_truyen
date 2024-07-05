
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Controls.Primitives;

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
            Album();
            label1.Width = panelAlbumTruyen.Width;
        }
        Dictionary<string, object> noidung_album = new Dictionary<string, object>();
        List<string> idtruyen = new List<string>();
        private JObject data;
        string uid;
        private async void Album()
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

            string path = "Nguoi_dung/" + uid + "/Album";

            FirebaseResponse res = await client1.GetAsync(path);
            /*            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(res.Body);
            */
            if (res.Body == "null")
            {
                return;
            }
            else
            {
                data = JObject.Parse(res.Body);
                if (data.Count == 0)
                {
                    TextBox textBox = new TextBox();
                    Font font = new Font("League Spartan SemiBold", 24.0f, FontStyle.Bold);
                    textBox.Text = "Lỗi! Không thể tải Album truyện.";
                    textBox.Font = font;
                    textBox.ForeColor = Color.Red;
                    return;
                }
                foreach (JProperty property in data.Properties())
                {
                    idtruyen.Add(property.Name);
                    noidung_album.Add(property.Name, property.Value);
                }

                int sl_truyen = noidung_album.Count;
                if (sl_truyen != 0)
                {
                    int i = 0;
                    foreach (var id in idtruyen)
                    {
                        FirestoreDb db = FirestoreDb.Create("healtruyen");
                        DocumentReference docReference = db.Collection("Truyen").Document(id);
                        DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
                        if (snapshot.Exists)
                        {
                            Panel panel = new Panel();
                            panel.Dock = DockStyle.Top;
                            panel.AutoScroll = true;
                            panel.AutoSize = true;
                            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            panel.Visible = true;
                            panelAlbumTruyen.Controls.Add(panel);

                            Panel panel1 = new Panel();
                            panel1.Dock = DockStyle.Top;
                            panel1.AutoSize = true;
                            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            panel1.Visible = true;
                            panel1.Tag = id;

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
                            ten_truyen.Text = UpdateSummaryText(snapshot.GetValue<string>("Ten"), ten_truyen.Font, this.Width - 300);
                            ten_truyen.AutoSize = true;
                            ten_truyen.Location = new Point(anh.Width + 25, 3);
                            ten_truyen.Font = new Font("League Spartan", 20F, FontStyle.Bold);
                            ten_truyen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            ten_truyen.Visible = true;

                            IconButton chuong = new IconButton();
                            chuong.Text = "  " + snapshot.GetValue<int>("So_chuong").ToString() + " chương";
                            chuong.AutoSize = true;
                            chuong.FlatAppearance.BorderSize = 0;
                            chuong.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                            chuong.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                            chuong.FlatStyle = FlatStyle.Flat;
                            chuong.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                            chuong.IconChar = IconChar.LayerGroup;
                            chuong.IconColor = Color.Black;
                            chuong.IconSize = 32;
                            chuong.IconFont = IconFont.Auto;
                            chuong.ImageAlign = ContentAlignment.MiddleLeft;
                            chuong.TextAlign = ContentAlignment.MiddleLeft;
                            chuong.TextImageRelation = TextImageRelation.ImageBeforeText;
                            chuong.Location = new Point(anh.Width + 25, 45);
                            chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            chuong.Visible = true;

                            IconButton chuong_dangdoc = new IconButton();
                            JObject outerDict = (JObject)noidung_album[id];
                            chuong_dangdoc.Text = "  " + outerDict["Chuong_dang_doc"].ToString() + " chương";
                            chuong_dangdoc.AutoSize = true;
                            chuong_dangdoc.FlatAppearance.BorderSize = 0;
                            chuong_dangdoc.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                            chuong_dangdoc.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                            chuong_dangdoc.FlatStyle = FlatStyle.Flat;
                            chuong_dangdoc.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                            chuong_dangdoc.IconChar = IconChar.Bookmark;
                            chuong_dangdoc.IconColor = Color.Black;
                            chuong_dangdoc.IconSize = 32;
                            chuong_dangdoc.IconFont = IconFont.Auto;
                            chuong_dangdoc.ImageAlign = ContentAlignment.MiddleLeft;
                            chuong_dangdoc.TextAlign = ContentAlignment.MiddleLeft;
                            chuong_dangdoc.TextImageRelation = TextImageRelation.ImageBeforeText;
                            chuong_dangdoc.Location = new Point(anh.Width + 25, 89);
                            chuong_dangdoc.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            chuong_dangdoc.Visible = true;

                            TableLayoutPanel trang_thai = new TableLayoutPanel();
                            trang_thai.AutoSize = true;
                            trang_thai.Location = new Point(anh.Width + 25, 140);
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
                                trang_thai_truyen.Text = "Tạm dừng";
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
                            delete.IconSize = 32;
                            delete.IconFont = IconFont.Auto;
                            delete.ImageAlign = ContentAlignment.MiddleCenter;
                            delete.TextAlign = ContentAlignment.MiddleCenter;
                            delete.TextImageRelation = TextImageRelation.Overlay;
                            delete.Location = new Point(this.Width - 150, 3);
                            delete.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            delete.Click += deleteButton_Click;
                            delete.MouseEnter += mouse_enter;
                            delete.MouseLeave += mouse_leave;
                            delete.Visible = true;

                            IconButton thong_bao_tat = new IconButton();
                            thong_bao_tat.Text = "";
                            thong_bao_tat.AutoSize = true;
                            thong_bao_tat.FlatAppearance.BorderSize = 0;
                            thong_bao_tat.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                            thong_bao_tat.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                            thong_bao_tat.FlatStyle = FlatStyle.Flat;
                            thong_bao_tat.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                            thong_bao_tat.IconChar = IconChar.BellSlash;
                            thong_bao_tat.IconColor = Color.Black;
                            thong_bao_tat.IconSize = 32;
                            thong_bao_tat.IconFont = IconFont.Auto;
                            thong_bao_tat.ImageAlign = ContentAlignment.MiddleCenter;
                            thong_bao_tat.TextAlign = ContentAlignment.MiddleCenter;
                            thong_bao_tat.TextImageRelation = TextImageRelation.Overlay;
                            thong_bao_tat.Location = new Point(this.Width - 200, 3);
                            thong_bao_tat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            thong_bao_tat.Click += Notification_turnoff_Click;
                            thong_bao_tat.MouseEnter += mouse_enter;
                            thong_bao_tat.MouseLeave += mouse_leave;
                            thong_bao_tat.Visible = true;
                            thong_bao_tat.Tag = i;


                            IconButton thong_bao_bat = new IconButton();
                            thong_bao_bat.Text = "";
                            thong_bao_bat.AutoSize = true;
                            thong_bao_bat.FlatAppearance.BorderSize = 0;
                            thong_bao_bat.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                            thong_bao_bat.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                            thong_bao_bat.FlatStyle = FlatStyle.Flat;
                            thong_bao_bat.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                            thong_bao_bat.IconChar = IconChar.Bell;
                            thong_bao_bat.IconColor = Color.Black;
                            thong_bao_bat.IconSize = 32;
                            thong_bao_bat.IconFont = IconFont.Auto;
                            thong_bao_bat.ImageAlign = ContentAlignment.MiddleCenter;
                            thong_bao_bat.TextAlign = ContentAlignment.MiddleCenter;
                            thong_bao_bat.TextImageRelation = TextImageRelation.Overlay;
                            thong_bao_bat.Location = new Point(this.Width - 200, 3);
                            thong_bao_bat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            thong_bao_bat.Click += Notification_turnon_Click;
                            thong_bao_bat.MouseEnter += mouse_enter;
                            thong_bao_bat.MouseLeave += mouse_leave;
                            thong_bao_bat.Visible = false;
                            thong_bao_bat.Tag = i + 1;

                            if ((bool)outerDict["Thong_bao"])
                            {
                                thong_bao_bat.Visible = true;
                                thong_bao_tat.Visible = false;
                            }

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
                        i += 2;
                    }
                }
                else
                {
                    panelAlbumTruyen.Visible = false;
                }
            }
        }

        private string UpdateSummaryText(string ten, Font label, int width)
        {
            return GetSummaryText(ten, label, width);
        }

        private string GetSummaryText(string originalText, Font font, int maxWidth)
        {
            // Tạo đối tượng đồ họa ảo để đo kích thước văn bản
            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Thiết lập các thông số vẽ văn bản
                StringFormat format = StringFormat.GenericDefault;

                // Kiểm tra kích thước văn bản
                SizeF textSize = g.MeasureString(originalText, font);
                if (textSize.Width <= maxWidth)
                {
                    // Trả về toàn bộ văn bản nếu nó vừa với Label
                    return originalText;
                }
                else
                {
                    // Tìm độ dài tối đa của tên
                    int maxSummaryLength = originalText.Length;
                    for (int i = originalText.Length - 1; i >= 0; i--)
                    {
                        string summary = originalText.Substring(0, i) + "...";
                        SizeF summarySize = g.MeasureString(summary, font);
                        if (summarySize.Width <= maxWidth)
                        {
                            maxSummaryLength = i;
                            break;
                        }
                    }

                    // Trả về tên
                    return originalText.Substring(0, maxSummaryLength) + "...";
                }
            }
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

            if (data.ContainsKey(idtruyen))
            {
                Dictionary<string, object> thong_tin_truyen = ((JObject)data[idtruyen]).ToObject<Dictionary<string, object>>();
                Them_Lay_thongbao thongbao = new Them_Lay_thongbao();
                await thongbao.Tat_Bat_thongbao_album(uid, idtruyen, thong_tin_truyen["Chuong_dang_doc"].ToString(), false);
            }

            IconButton btn = panel.Controls.OfType<IconButton>().FirstOrDefault(b => b.Tag != null && Convert.ToInt32(b.Tag) == (position-1));
            {
                btn.Visible = true;
                iconButton.Visible = false;
            }

            string path = "Nguoi_dung/Thong_bao_album/" + idtruyen;
            Dictionary<string, string> users_data = new Dictionary<string, string>();
            users_data.Add(user.User.Uid, "");
            var snshot = await client1.GetAsync(path);

            if (snshot != null)
            {
                // Nút tồn tại, thực hiện cập nhật
                await client1.UpdateAsync(path, users_data);
            }
            else
            {
                // Nút không tồn tại, thực hiện tạo mới
                await client1.SetAsync(path, users_data);
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

            if (data.ContainsKey(idtruyen))
            {
                Dictionary<string, object> thong_tin_truyen = ((JObject)data[idtruyen]).ToObject<Dictionary<string, object>>();
                Them_Lay_thongbao thongbao = new Them_Lay_thongbao();
                await thongbao.Tat_Bat_thongbao_album(uid, idtruyen, thong_tin_truyen["Chuong_dang_doc"].ToString(), true);
            }
            IconButton btn = panel.Controls.OfType<IconButton>().FirstOrDefault(b => b.Tag != null && b.Tag.ToString() == (position+1).ToString());
            btn.Visible = true;
            iconButton.Visible = false;

            string path = "Nguoi_dung/Thong_bao_album/" + idtruyen;
            var snshot = await client1.DeleteAsync(path + "/" + user.User.Uid);
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

            if (sharelink == $"Nguoi_dung/{user.User.Uid}/Album")
            {
                MessageBox.Show("Lỗi, đây là Album của bạn.");
                return;
            }

            FirebaseResponse res = await client.GetAsync(sharelink);
            data = JObject.Parse(res.Body);
            if (data.Count == 0)
            {
                TextBox textBox = new TextBox();
                Font font = new Font("League Spartan SemiBold", 24.0f, FontStyle.Bold);
                textBox.Text = "Lỗi! Không thể tải Album truyện.";
                textBox.Font = font;
                textBox.ForeColor = Color.Red;
                return;
            }
            foreach (JProperty property in data.Properties())
            {
                idtruyen.Add(property.Name);
                noidung_album.Add(property.Name, property.Value);
            }

            int sl_truyen = noidung_album.Count;
            if (sl_truyen != 0)
            {
                int i = 0;
                foreach (var id in idtruyen)
                {
                    FirestoreDb ds = FirestoreDb.Create("healtruyen");
                    DocumentReference docReference = ds.Collection("Truyen").Document(id);
                    DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
                    if (snapshot.Exists)
                    {
                        Panel panel = new Panel();
                        panel.Dock = DockStyle.Top;
                        panel.AutoScroll = true;
                        panel.AutoSize = true;
                        panel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        panel.Visible = true;
                        panelAlbumTruyen.Controls.Add(panel);

                        Panel panel1 = new Panel();
                        panel1.Dock = DockStyle.Top;
                        panel1.AutoSize = true;
                        panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        panel1.Visible = true;
                        panel1.Tag = id;

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
                        ten_truyen.Text = UpdateSummaryText(snapshot.GetValue<string>("Ten"), ten_truyen.Font, this.Width - 300);
                        ten_truyen.AutoSize = true;
                        ten_truyen.Location = new Point(anh.Width + 25, 3);
                        ten_truyen.Font = new Font("League Spartan", 20F, FontStyle.Bold);
                        ten_truyen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        ten_truyen.Visible = true;

                        IconButton chuong = new IconButton();
                        chuong.Text = "  " + snapshot.GetValue<int>("So_chuong").ToString() + " chương";
                        chuong.AutoSize = true;
                        chuong.FlatAppearance.BorderSize = 0;
                        chuong.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        chuong.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        chuong.FlatStyle = FlatStyle.Flat;
                        chuong.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                        chuong.IconChar = IconChar.LayerGroup;
                        chuong.IconColor = Color.Black;
                        chuong.IconSize = 32;
                        chuong.IconFont = IconFont.Auto;
                        chuong.ImageAlign = ContentAlignment.MiddleLeft;
                        chuong.TextAlign = ContentAlignment.MiddleLeft;
                        chuong.TextImageRelation = TextImageRelation.ImageBeforeText;
                        chuong.Location = new Point(anh.Width + 25, 45);
                        chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        chuong.Visible = true;

                        IconButton chuong_dangdoc = new IconButton();
                        JObject outerDict = (JObject)noidung_album[id];
                        chuong_dangdoc.Text = "  " + outerDict["Chuong_dang_doc"].ToString() + " chương";
                        chuong_dangdoc.AutoSize = true;
                        chuong_dangdoc.FlatAppearance.BorderSize = 0;
                        chuong_dangdoc.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        chuong_dangdoc.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        chuong_dangdoc.FlatStyle = FlatStyle.Flat;
                        chuong_dangdoc.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                        chuong_dangdoc.IconChar = IconChar.Bookmark;
                        chuong_dangdoc.IconColor = Color.Black;
                        chuong_dangdoc.IconSize = 32;
                        chuong_dangdoc.IconFont = IconFont.Auto;
                        chuong_dangdoc.ImageAlign = ContentAlignment.MiddleLeft;
                        chuong_dangdoc.TextAlign = ContentAlignment.MiddleLeft;
                        chuong_dangdoc.TextImageRelation = TextImageRelation.ImageBeforeText;
                        chuong_dangdoc.Location = new Point(anh.Width + 25, 89);
                        chuong_dangdoc.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        chuong_dangdoc.Visible = true;

                        TableLayoutPanel trang_thai = new TableLayoutPanel();
                        trang_thai.AutoSize = true;
                        trang_thai.Location = new Point(anh.Width + 25, 140);
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
                            trang_thai_truyen.Text = "Tạm dừng";
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
                        delete.IconSize = 32;
                        delete.IconFont = IconFont.Auto;
                        delete.ImageAlign = ContentAlignment.MiddleCenter;
                        delete.TextAlign = ContentAlignment.MiddleCenter;
                        delete.TextImageRelation = TextImageRelation.Overlay;
                        delete.Location = new Point(this.Width - 150, 3);
                        delete.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        delete.Click += deleteButton_Click;
                        delete.MouseEnter += mouse_enter;
                        delete.MouseLeave += mouse_leave;
                        delete.Visible = true;

                        IconButton thong_bao_tat = new IconButton();
                        thong_bao_tat.Text = "";
                        thong_bao_tat.AutoSize = true;
                        thong_bao_tat.FlatAppearance.BorderSize = 0;
                        thong_bao_tat.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        thong_bao_tat.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        thong_bao_tat.FlatStyle = FlatStyle.Flat;
                        thong_bao_tat.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                        thong_bao_tat.IconChar = IconChar.BellSlash;
                        thong_bao_tat.IconColor = Color.Black;
                        thong_bao_tat.IconSize = 32;
                        thong_bao_tat.IconFont = IconFont.Auto;
                        thong_bao_tat.ImageAlign = ContentAlignment.MiddleCenter;
                        thong_bao_tat.TextAlign = ContentAlignment.MiddleCenter;
                        thong_bao_tat.TextImageRelation = TextImageRelation.Overlay;
                        thong_bao_tat.Location = new Point(this.Width - 200, 3);
                        thong_bao_tat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        thong_bao_tat.Click += Notification_turnoff_Click;
                        thong_bao_tat.MouseEnter += mouse_enter;
                        thong_bao_tat.MouseLeave += mouse_leave;
                        thong_bao_tat.Visible = true;
                        thong_bao_tat.Tag = i;


                        IconButton thong_bao_bat = new IconButton();
                        thong_bao_bat.Text = "";
                        thong_bao_bat.AutoSize = true;
                        thong_bao_bat.FlatAppearance.BorderSize = 0;
                        thong_bao_bat.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 247, 253);
                        thong_bao_bat.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 247, 253);
                        thong_bao_bat.FlatStyle = FlatStyle.Flat;
                        thong_bao_bat.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                        thong_bao_bat.IconChar = IconChar.Bell;
                        thong_bao_bat.IconColor = Color.Black;
                        thong_bao_bat.IconSize = 32;
                        thong_bao_bat.IconFont = IconFont.Auto;
                        thong_bao_bat.ImageAlign = ContentAlignment.MiddleCenter;
                        thong_bao_bat.TextAlign = ContentAlignment.MiddleCenter;
                        thong_bao_bat.TextImageRelation = TextImageRelation.Overlay;
                        thong_bao_bat.Location = new Point(950, 3);
                        thong_bao_bat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        thong_bao_bat.Click += Notification_turnon_Click;
                        thong_bao_bat.MouseEnter += mouse_enter;
                        thong_bao_bat.MouseLeave += mouse_leave;
                        thong_bao_bat.Visible = false;
                        thong_bao_bat.Tag = i + 1;

                        if ((bool)outerDict["Thong_bao"])
                        {
                            thong_bao_bat.Visible = true;
                            thong_bao_tat.Visible = false;
                        }

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
                    i += 2;
                }
            }
            else
            {
                panelAlbumTruyen.Visible = false;
            }
        }

        private void mouse_enter(object sender, EventArgs e)
        {
            if (sender is IconButton ibt)
            {
                ibt.IconColor = Color.Red;
            }
        }

        private void mouse_leave(object sender, EventArgs e)
        {
            if (sender is IconButton ibt)
            {
                ibt.IconColor = Color.Black;
            }
        }

        private void ibtnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
