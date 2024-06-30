using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using System.IO;
using FontAwesome.Sharp;

namespace Login
{
    public partial class Search : Form
    {
        string tentruyen = "";
        public Search(string name)
        {
            InitializeComponent();
            tentruyen = name;
        }
       
        private async void Tim_truyen()
        {
            API_Tim_Kiem search = new API_Tim_Kiem();
            Task<List<string>> truyen = search.Search(tentruyen);
            var tim_truyen = await truyen;
            Panel result = new Panel();
            result.Dock = DockStyle.Top;
            result.AutoSize = true;
            result.Size = new Size(1149, 508);
            result.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            result.Visible = true;
            result.Margin = new Padding(0, 0, 0, 50);
            result.Height = Convert.ToInt32(this.Height / 9);
            this.Controls.Add(result);
            
            Label space = new Label();
            space.AutoSize = true;
            space.Font = new Font("League Spartan", 23F, FontStyle.Regular);
            space.Text = " ";

            Label text_result = new Label();
            text_result.Text = "Kết quả cho:";
            text_result.Font = new Font("League Spartan", 12F, FontStyle.Regular);
            text_result.AutoSize = true;
            text_result.Location = new Point(13, 13);
            text_result.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            text_result.Visible = true;

            Label search_name = new Label();
            search_name.Text = tentruyen.ToString();
            search_name.Font = new Font("League Spartan", 12F, FontStyle.Bold);
            search_name.AutoSize = true;
            search_name.Location = new Point(143, 13);
            search_name.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            search_name.Visible = true;

            Label duong_ke = new Label();
            duong_ke.FlatStyle = FlatStyle.Flat;
            duong_ke.BorderStyle = BorderStyle.Fixed3D;
            duong_ke.Location = new Point(0, 49);
            duong_ke.Size = new Size(1149, 1);
            duong_ke.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            duong_ke.Visible = true;

            result.Controls.Add(text_result);
            result.Controls.Add(search_name);
            result.Controls.Add(duong_ke);
            result.Controls.Add(space);
            foreach (var idtruyen in tim_truyen)
            {
                FirestoreDb db = FirestoreDb.Create("healtruyen");
                DocumentReference docReference = db.Collection("Truyen").Document(idtruyen);
                DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    //Novel novel = snapshot.ConvertTo<Novel>();
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

                    string gthieu_truyen = snapshot.GetValue<string>("Tom_tat");
                    Label tom_tat = new Label();
                    tom_tat.Font = new Font("League Spartan", 10F, FontStyle.Regular);
                    tom_tat.AutoSize = true;
                    tom_tat.Location = new Point(anh.Width + 25, 43);
                    tom_tat.Text = UpdateSummaryText(gthieu_truyen, tom_tat.Font, this.Size.Width - 20);
                    tom_tat.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    tom_tat.Visible = true;

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
                    chuong.Location = new Point(anh.Width + 25, 78);
                    chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    chuong.Visible = true;

                    TableLayoutPanel trang_thai = new TableLayoutPanel();
                    trang_thai.AutoSize = true;
                    trang_thai.Location = new Point(anh.Width + 25, 119);
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
                    //tac_gia.Location = new Point(3, 13);
                    tac_gia.Margin = new Padding(3, 3, 6 ,3);
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
                    //trang_thai_truyen.Location = new Point(tac_gia.Location.X + 10, 13);
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
                    //the_loai.Location = new Point(tac_gia.Width + 3 + trang_thai_truyen.Width + the_loai.Width, 13);
                    the_loai.Margin = new Padding(3, 3, 10, 3);
                    the_loai.Text = snapshot.GetValue<List<string>>("The_loai")[0];
                    the_loai.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    the_loai.Visible = true;

                    Label space1 = new Label();
                    space1.AutoSize = true;
                    space1.Font = new Font("League Spartan", 20F, FontStyle.Regular);
                    space1.Text = " ";

                    trang_thai.Controls.Add(tac_gia);
                    trang_thai.Controls.Add(trang_thai_truyen);
                    trang_thai.Controls.Add(the_loai);

                    panel1.Controls.Add(panel2);
                    panel1.Controls.Add(ten_truyen);
                    panel1.Controls.Add(tom_tat);
                    panel1.Controls.Add(chuong);
                    panel1.Controls.Add(trang_thai);
                    panel1.Controls.Add(space1);
                    panel.BringToFront();
                }
            }
        }
/*        private void Search_SizeChanged(object sender, EventArgs e)
        {
            UpdateSummaryText();
        }*/

        private string UpdateSummaryText(string gthieu, Font label, int width)
        {
            return GetSummaryText(gthieu, label, width);
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
                    // Tìm độ dài tối đa của đoạn tóm tắt
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

                    // Trả về đoạn tóm tắt
                    return originalText.Substring(0, maxSummaryLength) + "...";
                }
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
            Tim_truyen();
        }
    }
}
