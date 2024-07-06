using FontAwesome.Sharp;
using Google.Cloud.Firestore;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security.AntiXss;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Login
{
    

    public partial class Pro_search : Form
    {
        public Pro_search()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (tbsearch.Text == string.Empty)
            {
                if (cbdanhgia.SelectedIndex == 0 && cbTheloai.SelectedIndex == 0 && cbtinhtrang.SelectedIndex == 0 && tbtacgia.Text == string.Empty)
                {
                    tb1 tb = new tb1();
                    tb.ShowDialog();
                }
                else
                {
                    double dg = Danh_gia(cbdanhgia.Text);
                    int tt = TinhTrang(cbtinhtrang.Text);
                    API_Tim_Kiem search = new API_Tim_Kiem();
                    Task<List<string>> truyen = search.AdvanceSearch(tbsearch.Text, dg, tbtacgia.Text, cbTheloai.Text, tt);
                    Output(truyen, false, tbsearch.Text);
                }
            }
            else
            {
                if (cbdanhgia.SelectedIndex == 0 && cbTheloai.SelectedIndex == 0 && cbtinhtrang.SelectedIndex == 0 && tbtacgia.Text == string.Empty)
                {
                    double dg = Danh_gia(cbdanhgia.Text);
                    API_Tim_Kiem search = new API_Tim_Kiem();
                    Task<List<string>> truyen = search.Search(tbsearch.Text);
                    Output(truyen, true, tbsearch.Text);
                }
                else
                {
                    double dg = Danh_gia(cbdanhgia.Text);
                    int tt = TinhTrang(cbtinhtrang.Text);
                    API_Tim_Kiem search = new API_Tim_Kiem();
                    Task<List<string>> truyen = search.AdvanceSearch(tbsearch.Text, dg, tbtacgia.Text, cbTheloai.Text, tt);
                    Output(truyen, true, tbsearch.Text);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cbdanhgia.SelectedIndex = 0;
            cbTheloai.SelectedIndex = 0;
            cbtinhtrang.SelectedIndex = 0;
            tbtacgia.Text = string.Empty;
            tbsearch.Text = string.Empty;
            reset_panel();
        }

        private void reset_panel()
        {
            foreach (Control control in pnAll2.Controls)
            {
                control.Visible = false;
            }
        }

        private async void Output(Task<List<string>> output, bool c_t, string name)
        {
            reset_panel();

            var truyen = await output;

            if (truyen is null)
            {
                MessageBox.Show("Không có");
                return;
            }

            foreach (var idtruyen in truyen)
            {
                FirestoreDb db = FirestoreDb.Create("healtruyen");
                DocumentReference docReference = db.Collection("Truyen").Document(idtruyen);
                DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();

                if(snapshot.Exists) {

                    Panel panel1 = new Panel()
                    {
                        Dock = DockStyle.Top,
                        AutoSize = true,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };
                    pnAll2.Controls.Add(panel1);

                    Panel panel2 = new Panel()
                    {
                        Location = new Point(0, 0),
                        Width = 149,
                        Height = 183,
                        Padding = new Padding(10, 0, 0, 0),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };
                    
                    // Chuyển chuỗi Base64 thành mảng byte
                    byte[] imageBytes = Convert.FromBase64String(snapshot.GetValue<string>("Anh"));
                    // Tạo MemoryStream từ mảng byte
                    Image image;
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        // Đọc hình ảnh từ MemoryStream
                        image = Image.FromStream(ms);

                    }

                    PictureBox anh = new PictureBox()
                    {
                        Location = new Point(0, 0),
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = image,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };
                    panel2.Controls.Add(anh);

                    Label ten_truyen = new Label()
                    {
                        Text = snapshot.GetValue<string>("Ten"),
                        AutoSize = true,
                        Location = new Point(anh.Width + 25, 3),
                        Font = new Font("League Spartan", 16F, FontStyle.Bold),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };

                    string gthieu_truyen = snapshot.GetValue<string>("Tom_tat");

                    Label tom_tat = new Label()
                    {
                        Font = new Font("League Spartan", 10F, FontStyle.Regular),
                        AutoSize = true,
                        Location = new Point(anh.Width + 25, 43),
                        Text = UpdateSummaryText(gthieu_truyen, Font, this.Size.Width - 20),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };

                    TableLayoutPanel trang_thai_1 = new TableLayoutPanel()
                    {
                        AutoSize = true,
                        Location = new Point(anh.Width + 25, 78),
                        Width = 340,
                        Height = 61,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                        RowCount = 1,
                        ColumnCount = 4,
                    };

                    IconButton chuong = new IconButton()
                    {
                        Text = "  " + snapshot.GetValue<int>("So_chuong").ToString() + " chương",
                        AutoSize = true,
                        FlatAppearance = {
                            BorderSize = 0,
                            MouseDownBackColor = Color.FromArgb(220, 247, 253),
                            MouseOverBackColor = Color.FromArgb(220, 247, 253),
                        },
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("League Spartan", 10F, FontStyle.Regular),
                        IconChar = IconChar.LayerGroup,
                        IconColor = Color.Black,
                        IconSize = 32,
                        IconFont = IconFont.Auto,
                        ImageAlign = ContentAlignment.MiddleLeft,
                        TextAlign = ContentAlignment.MiddleLeft,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        /*Location = new Point(anh.Width + 25, 78),*/
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };

                    IconButton view = new IconButton()
                    {
                        Text = "  " + snapshot.GetValue<int>("Luot_xem").ToString(),
                        AutoSize = true,
                        FlatAppearance = {
                            BorderSize = 0,
                            MouseDownBackColor = Color.FromArgb(220, 247, 253),
                            MouseOverBackColor = Color.FromArgb(220, 247, 253),
                        },
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("League Spartan", 10F, FontStyle.Regular),
                        IconChar = IconChar.Eye,
                        IconColor = Color.Black,
                        IconSize = 32,
                        IconFont = IconFont.Auto,
                        ImageAlign = ContentAlignment.MiddleLeft,
                        TextAlign = ContentAlignment.MiddleLeft,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        /*Location = new Point(anh.Width + 60, 78),*/
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };

                    IconButton DGTB = new IconButton()
                    {
                        Text = "  " + snapshot.GetValue<double>("Danh_gia_Tb").ToString(),
                        AutoSize = true,
                        FlatAppearance = {
                            BorderSize = 0,
                            MouseDownBackColor = Color.FromArgb(220, 247, 253),
                            MouseOverBackColor = Color.FromArgb(220, 247, 253),
                        },
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("League Spartan", 10F, FontStyle.Regular),
                        IconChar = IconChar.Star,
                        IconColor = Color.Black,
                        IconSize = 32,
                        IconFont = IconFont.Auto,
                        ImageAlign = ContentAlignment.MiddleLeft,
                        TextAlign = ContentAlignment.MiddleLeft,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        /*Location = new Point(anh.Width + 60, 78),*/
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };

                    IconButton Like = new IconButton()
                    {
                        Text = "  " + snapshot.GetValue<int>("Luot_xem").ToString(),
                        AutoSize = true,
                        FlatAppearance = {
                            BorderSize = 0,
                            MouseDownBackColor = Color.FromArgb(220, 247, 253),
                            MouseOverBackColor = Color.FromArgb(220, 247, 253),
                        },
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("League Spartan", 10F, FontStyle.Regular),
                        IconChar = IconChar.ThumbsUp,
                        IconColor = Color.Black,
                        IconSize = 32,
                        IconFont = IconFont.Auto,
                        ImageAlign = ContentAlignment.MiddleLeft,
                        TextAlign = ContentAlignment.MiddleLeft,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        /*Location = new Point(anh.Width + 60, 78),*/
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };

                    TableLayoutPanel trang_thai = new TableLayoutPanel()
                    {
                        AutoSize = true,
                        Location = new Point(anh.Width + 25, 140),
                        Width = 340,
                        Height = 61,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                        RowCount = 1,
                        ColumnCount = 3,
                    };
                    trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    trang_thai.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));


                    Button tac_gia = new Button()
                    {
                        AutoSize = true,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("League Spartan SemiBold", 10F, FontStyle.Bold),
                        ForeColor = Color.Red,
                        //tac_gia.Location = new Point(3, 13);
                        Margin = new Padding(3, 3, 6, 3),
                        Text = snapshot.GetValue<string>("Tac_gia"),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };
                    tac_gia.FlatAppearance.BorderSize = 4;
                    tac_gia.FlatAppearance.MouseOverBackColor = Color.White;
                    tac_gia.FlatAppearance.MouseDownBackColor = Color.White;

                    Button trang_thai_truyen = new Button()
                    {
                        AutoSize = true,
                        FlatAppearance = {
                            BorderSize = 4,
                            MouseOverBackColor = Color.White,
                            MouseDownBackColor = Color.White, 
                        },
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("League Spartan SemiBold", 10F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(0, 110, 0),
                        //trang_thai_truyen.Location = new Point(tac_gia.Location.X + 10, 13);
                        Margin = new Padding(3, 3, 6, 3),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };
                    try
                    {
                        int trangThai = snapshot.GetValue<int>("Trang_thai");
                        if (trangThai == 0)
                            trang_thai_truyen.Text = "Tạm dừng";
                        else if (trangThai == 1)
                            trang_thai_truyen.Text = "Đang tiến hành";
                        else if (trangThai == 2)
                            trang_thai_truyen.Text = "Hoàn thành";
                    }
                    catch (System.Exception ex)
                    {
                        trang_thai_truyen.Text = "Unknown status";
                    }

                    Button the_loai = new Button()
                    {
                        AutoSize = true,
                        FlatAppearance = {
                            BorderSize = 4,
                            MouseOverBackColor = Color.White,
                            MouseDownBackColor = Color.White,
                        },
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("League Spartan SemiBold", 10F, FontStyle.Bold),
                        ForeColor = Color.Blue,
                        //the_loai.Location = new Point(tac_gia.Width + 3 + trang_thai_truyen.Width + the_loai.Width, 13);
                        Margin = new Padding(3, 3, 10, 3),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left,
                        Visible = true,
                    };

                    try
                    {
                        List<string> theLoaiList = snapshot.GetValue<List<string>>("The_loai");
                        if (theLoaiList.Count > 0)
                        {
                            the_loai.Text = theLoaiList[0];
                        }
                        else
                        {
                            the_loai.Text = "Unknown";
                        }
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        // Handle the case where the "The_loai" field is not found in the document
                        the_loai.Text = "Unknown";
                    }


                    Label space1 = new Label()
                    {
                        AutoSize = true,
                        Font = new Font("League Spartan", 20F, FontStyle.Regular),
                        Text = " ",
                    };
                    

                    trang_thai.Controls.Add(tac_gia);
                    trang_thai.Controls.Add(trang_thai_truyen);
                    trang_thai.Controls.Add(the_loai);

                    trang_thai_1.Controls.Add(chuong);
                    trang_thai_1.Controls.Add(view);
                    trang_thai_1.Controls.Add(Like);
                    trang_thai_1.Controls.Add(DGTB);

                    panel1.Controls.Add(panel2);
                    panel1.Controls.Add(ten_truyen);
                    panel1.Controls.Add(tom_tat);
                    panel1.Controls.Add(trang_thai_1);
                    panel1.Controls.Add(trang_thai);
                    panel1.Controls.Add(space1);
                    pnAll2.BringToFront();
                }
            }
        }

        private int TinhTrang(string tinhtrang)
        {
            if (tinhtrang.ToUpper() == "TẠM DỪNG")
            {
                return 0;
            }
            else if (tinhtrang.ToUpper() == "ĐANG TIẾN HÀNH")
            {
                return 1;
            }
            else if (tinhtrang.ToUpper() == "HOÀN THÀNH")
            {
                return 2;
            }
            return -1;
        }

        private double Danh_gia(string danhgia)
        {
            if (danhgia == "Không")
            {
                return -1;
            }
            else if (danhgia == "Dưới 1 sao")
            {
                return 1;
            }
            else if (danhgia == "Dưới 2 sao")
            {
                return 2;
            }
            else if (danhgia == "Dưới 3 sao")
            {
                return 3;
            }
            else if (danhgia == "Dưới 4 sao")
            {
                return 4;
            }
            else if (danhgia == "Dưới 5 sao")
            {
                return 5;
            }
            return -1;
        }

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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
