using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

/*using System.Windows.Controls;*/
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2010.Excel;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FontAwesome.Sharp;
using Google.Cloud.Firestore;
using Google.Type;
using Newtonsoft.Json;
using Novel;
using Readinghistory;
using static Login.Danhgia;

namespace Login
{

    
    public partial class Doc_Truyen : Form
    {
        string nameTruyen;
        int currentChap;
        int numChap = 1;
        string idTruyen;
        UserCredential user;

        public class Binhluan
        {
            public string ID_chuong { get; set; }
            public string ID_nguoidung { get; set; }
            public int Luot_thich {  get; set; }
            public string Noi_dung { get; set; }
            public string TG_binh_luan { get; set; }
            public bool To_cao {  get; set; }
        }

        [FirestoreData]

        public class Novel
        {
            [FirestoreProperty("Anh")]
            public string coverImg { get; set; }
            [FirestoreProperty("Binh_luan")]
            public int comment { get; set; }
            [FirestoreProperty("Danh_gia")]
            public int numRating { get; set; }
            [FirestoreProperty("Danh_gia_Tb")]
            public int avgRating { get; set; }
            [FirestoreProperty("De_cu")]
            public int recommend { get; set; }
            [FirestoreProperty("Luot_thich")]
            public int like { get; set; }
            [FirestoreProperty("Luot_xem")]
            public int numRead { get; set; }
            [FirestoreProperty("So_chuong")]
            public int cntChapter { get; set; }
            [FirestoreProperty("TG_Dang")]
            public Timestamp times { get; set; }
            [FirestoreProperty("Tac_gia")]
            public string author { get; set; }
            [FirestoreProperty("Ten")]
            public string nameNovel { get; set; }
            [FirestoreProperty("The_loai")]
            public string[] type { get; set; }
            [FirestoreProperty("Tom_tat")]
            public string description { get; set; }
            [FirestoreProperty("Trang_thai")]
            public int status { get; set; }
            [FirestoreProperty("Diem_danhgia")]
            public int tong_DG { get; set; }
            [FirestoreProperty("ID_nguoi_dang")]
            public string id_nguoidang { get; set; }
        }

        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        private Trang_chu tc;
        public Doc_Truyen(string idTruyen, UserCredential user, int currentChap, Trang_chu trangchu)
        {
            InitializeComponent();
            this.currentChap = currentChap;
            this.idTruyen = idTruyen;
            this.user = user;
            tc = trangchu;
        }

        int so_chuong = 0;
        private async void btnExit_Click(object sender, EventArgs e)
        {
            CRUD_lsd cRUD_Lsd = new CRUD_lsd();
            await cRUD_Lsd.Capnhat_lichsudoc(user.User.Uid, currentChap, idTruyen) ;
            this.Close();
        }
        private static readonly string _from = "nguoiduathuH3A1@gmail.com"; // Email của Sender (của bạn)
        private static readonly string _pass = "wgvohibzrfwgshtf"; // Mật khẩu Email của Sender (của bạn)
        private async void Doc_Truyen_Load(object sender, EventArgs e)
        {
            string numChapter = currentChap.ToString();
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference collectionReference = db.Collection("Truyen");
            string chapId = "";
            int cnt = 4 - numChapter.Length;
            for (int i = 0; i < cnt; i++)
            {
                chapId += "0";
            }
            chapId += numChapter;
            DocumentReference collectionRef = db.Collection("Truyen").Document(idTruyen).Collection("Chuong").Document(chapId);
            DocumentSnapshot snapshot = await collectionRef.GetSnapshotAsync();
            DocumentReference truyen = db.Collection("Truyen").Document(idTruyen);
            DocumentSnapshot name = await truyen.GetSnapshotAsync();
            nameTruyen = name.GetValue<string>("Ten");
            if (snapshot.Exists)
            {
                comboBox1.Text = "Chương " + chapId;


                /*Interact.Chapter chapter = snapshot.ConvertTo<Interact.Chapter>();*/

                contentChap.Text = snapshot.GetValue<string>("Noi_dung"); ;
                labelName.Text = snapshot.GetValue<string>("Tieu_de"); ;
                Task<DocumentSnapshot> res1 = Interact.getInfoNovel(nameTruyen);
                DocumentSnapshot novel = await res1;
                iconButton2.Text = novel.GetValue<string>("Tac_gia");
                numChap = novel.GetValue<int>("So_chuong");
                so_chuong = numChap;
                Task<string> res2 = Interact.getIdNovel(nameTruyen, "Truyen");
                IFirebaseConfig _firebaseConfig = new FirebaseConfig
                {
                    AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                    BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
                };
                IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
                FirebaseResponse res3 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/");
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res3.Body);

                if (dict is null)
                {
                    Panel panel = new Panel();
                    panel5.Controls.Add(panel);
                    panel.Dock = DockStyle.Top;
                    panel.BringToFront();
                    panel.Height = 216;
                    panel.AutoSize = true;

                    Label binhluan = new Label();
                    binhluan.Text = "Hiện không tồn tại bình luận của người dùng. Bạn hãy bình luận giúp truyện nhé!";
                    binhluan.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    binhluan.AutoSize = true;

                    panel.Controls.Add(binhluan);
                    return;
                }
                //show data
                var dem = 0;
                foreach (var i in dict)
                {
                    dem = Convert.ToInt32(i.Key);
                }
                for (int i = 1; i <= dem; i++)
                {
                    string dembl = "0";
                    if (i < 10)
                    {
                        dembl += "0" + i.ToString();
                    }
                    else if (i < 100)
                    {
                        dembl += i.ToString();
                    }
                    else
                    {
                        dembl = i.ToString();
                    }
                    FirebaseResponse res4 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl);

                    Binhluan binhluan = res4.ResultAs<Binhluan>();

                    if (binhluan is null) continue;
                    string id_chuong = idTruyen + chapId;

                    if (binhluan.ID_chuong != id_chuong) continue;

                    Panel panel = new Panel();
                    panel5.Controls.Add(panel);
                    panel.Dock = DockStyle.Top;
                    panel.BringToFront();
                    panel.Height = 216;

                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                    panel.Controls.Add(tableLayoutPanel);
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.RowCount = 1;
                    tableLayoutPanel.ColumnCount = 2;
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));



                    PictureBox pictureBox = new PictureBox();
                    tableLayoutPanel.Controls.Add(pictureBox, 0, 0);
                    pictureBox.Dock = DockStyle.Fill;

                    FirebaseResponse res5 = await client.GetAsync("Nguoi_dung/" + binhluan.ID_nguoidung + "/Anh_dai_dien");
                    string base64String = res5.ResultAs<string>();
                    byte[] imageBytes = Convert.FromBase64String(base64String);

                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        Bitmap bitmap = new Bitmap(memoryStream);
                        pictureBox.Image = bitmap;
                    }

                    Panel panel1 = new Panel();
                    tableLayoutPanel.Controls.Add(panel1);
                    panel1.Dock = DockStyle.Fill;

                    TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
                    panel1.Controls.Add(tableLayoutPanel1);
                    tableLayoutPanel1.Dock = DockStyle.Top;
                    tableLayoutPanel1.BringToFront();
                    tableLayoutPanel1.RowCount = 1;
                    tableLayoutPanel1.ColumnCount = 3;
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

                    FirebaseResponse res6 = await client.GetAsync("Nguoi_dung/" + binhluan.ID_nguoidung + "/TK_dangnhap");
                    Label labelName = new Label();
                    tableLayoutPanel1.Controls.Add(labelName, 0, 0);
                    labelName.AutoSize = true;
                    labelName.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    /*var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(binhluan.ID_nguoidung);*/
                    /*var user1 = client.GetAsync(binhluan.ID_nguoidung);*/
                    labelName.Text = res6.ResultAs<string>();

                    Label date = new Label();
                    tableLayoutPanel1.Controls.Add(date, 1, 0);
                    date.AutoSize = true;
                    date.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    string dateStr = binhluan.TG_binh_luan.ToString();
                    System.DateTime dateTime = System.DateTime.Parse(dateStr, CultureInfo.InvariantCulture);
                    System.DateTime dateNow = System.DateTime.Now;
                    TimeSpan elapsedTime = dateNow - dateTime;
                    int elapsedSeconds = (int)elapsedTime.TotalSeconds;
                    int elapsedMinutes = (int)elapsedTime.TotalMinutes;
                    int elapsedHours = (int)elapsedTime.TotalHours;
                    int elapsedDays = (int)elapsedTime.TotalDays;
                    int elapsedMonth = elapsedDays / 30;
                    int elapsedYear = elapsedMonth / 12;
                    if (elapsedYear > 0)
                    {
                        date.Text = elapsedYear.ToString() + " năm trước";
                    }
                    else if (elapsedMonth > 0)
                    {
                        date.Text = elapsedMonth.ToString() + " tháng trước";
                    }
                    else if (elapsedDays > 0)
                    {
                        date.Text = elapsedDays.ToString() + " ngày trước";
                    }
                    else if (elapsedHours > 0)
                    {
                        date.Text = elapsedHours.ToString() + " giờ trước";
                    }
                    else if (elapsedMinutes > 0)
                    {
                        date.Text = elapsedMinutes.ToString() + " phút trước";
                    }
                    else
                    {
                        date.Text = elapsedSeconds.ToString() + " giây trước";
                    }




                    TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
                    panel1.Controls.Add(tableLayoutPanel2);
                    tableLayoutPanel2.Dock = DockStyle.Bottom;
                    tableLayoutPanel2.RowCount = 1;
                    tableLayoutPanel2.ColumnCount = 4;
                    tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64));
                    tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                    tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                    tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));

                    Label content = new Label();
                    panel1.Controls.Add(content);
                    content.AutoSize = true;
                    content.Font = new Font("League Spartan", 14, FontStyle.Regular);
                    content.Dock = DockStyle.Top;
                    content.Text = binhluan.Noi_dung;
                    content.BringToFront();

                    IconButton btnLike = new IconButton();
                    tableLayoutPanel2.Controls.Add(btnLike, 2, 0);
                    btnLike.Dock = DockStyle.Fill;
                    btnLike.Font = new Font("League Spartan", 12, FontStyle.Regular);
                    btnLike.IconChar = IconChar.Heart;
                    btnLike.IconSize = 30;
                    btnLike.Text = binhluan.Luot_thich.ToString() + "\nThích";
                    btnLike.TextAlign = ContentAlignment.MiddleRight;
                    btnLike.TextImageRelation = TextImageRelation.ImageBeforeText;
                    btnLike.Click += async (s, ev) =>
                    {
                        //Hàm click code ở đây

                        int so_like = binhluan.Luot_thich;
                        so_like++;
                        await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl + "/Luot_thich", so_like);
                        btnLike.BackColor = System.Drawing.Color.Red;
                        btnLike.IconColor = System.Drawing.Color.White;
                        btnLike.Enabled = false;
                        btnLike.Text = so_like.ToString() + "\nThích";
                        btnLike.ForeColor = System.Drawing.Color.Red;
                    };

                    IconButton btndelete = new IconButton();
                    tableLayoutPanel2.Controls.Add(btndelete, 1, 0);
                    btndelete.Dock = DockStyle.Fill;
                    btndelete.Font = new Font("League Spartan", 12, FontStyle.Regular);
                    btndelete.IconChar = IconChar.Trash;
                    btndelete.IconSize = 30;
                    btndelete.Text = "Xóa";
                    btndelete.TextAlign = ContentAlignment.MiddleRight;
                    btndelete.TextImageRelation = TextImageRelation.ImageBeforeText;
                    btndelete.Click += async (s, ev) =>
                    {
                        int role = 0;
                        FirebaseResponse res7 = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Vaitro");
                        role = res7.ResultAs<int>();
                        if (role == 0)
                        {
                            await client.DeleteAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl);
                            FirestoreDb dbs = FirestoreDb.Create("healtruyen");
                            CollectionReference truyens = dbs.Collection("Truyen");
                            nameTruyen = nameTruyen.ToUpper();
                            Query q = truyens.WhereEqualTo("Ten", nameTruyen);
                            QuerySnapshot snapshots = await q.GetSnapshotAsync();
                            string id = "";
                            if (snapshots.Documents.Count > 0)
                            {
                                id = snapshots.Documents[0].Id;
                            }
                            DocumentReference collectionRefs = dbs.Collection("Truyen").Document(id);
                            DocumentSnapshot ss = await collectionRefs.GetSnapshotAsync();

                            int So_binhluan = ss.GetValue<int>("Binh_luan");

                            So_binhluan++;

                            Dictionary<string, object> updates = new Dictionary<string, object>
                            {
                                { "Binh_luan", So_binhluan },
                            };
                            DocumentReference doc = truyens.Document(id);
                            await doc.UpdateAsync(updates);
                            panel.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Bạn không có quyền xóa bình luận!");
                        }
                    };

                    IconButton btnReport = new IconButton();
                    tableLayoutPanel2.Controls.Add(btnReport, 3, 0);
                    btnReport.Dock = DockStyle.Fill;
                    btnReport.Font = new Font("League Spartan", 12, FontStyle.Regular);
                    btnReport.IconChar = IconChar.Flag;
                    btnReport.IconSize = 30;
                    btnReport.Text = "Tố cáo";
                    btnReport.TextAlign = ContentAlignment.MiddleRight;
                    btnReport.TextImageRelation = TextImageRelation.ImageBeforeText;
                    btnReport.Click += async (s, ev) =>
                    {
                        //Hàm click code ở đây 
                        if (user.User.Info.Uid != binhluan.ID_nguoidung)
                        {
                            await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl + "/To_cao", true);
                            /*MessageBox.Show("Tố cáo thành công!Bạn hãy đợi Admin xử lý!");*/
                            btnReport.BackColor = System.Drawing.Color.Yellow;
                            btnReport.Text = "Bị tố cáo";
                            try
                            {
                                MailMessage mail = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                                mail.From = new MailAddress(user.User.Info.Email);
                                mail.To.Add(_from);
                                mail.Subject = $"Bình luận của người dùng có ID là {binhluan.ID_nguoidung} ";
                                mail.IsBodyHtml = true;
                                mail.Body = $"<div> Tên người bình luận: {res6.ResultAs<string>()} </div> <br> " +
                                $"Bị báo cáo bởi người dùng {user.User.Info.Uid} có tên là {user.User.Info.DisplayName} <br> Hãy kiểm tra nội dung bình luận này trong truyện {nameTruyen} <br> Nội dung bình luận: {binhluan.Noi_dung}";

                                mail.Priority = MailPriority.High;

                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(_from, _pass);
                                SmtpServer.EnableSsl = true;

                                SmtpServer.Send(mail);
                                FirebaseResponse re = await client.GetAsync("Vi_pham/");
                                if (re.Body == "null")
                                {
                                    Dictionary<string, string> data = new Dictionary<string, string> {
                                    {"Id_bi_tocao", binhluan.ID_nguoidung.ToString() },
                                    {"Noi_dung_to_cao", "Tên người bình luận: " + res6.ResultAs<string>() +
                                        "bị báo cáo bởi người dùng " + user.User.Info.Uid + " có tên là " + user.User.Info.DisplayName + ". Hãy kiểm tra nội " +
                                            $"dung bình luận này trong " +
                                            "truyện " + nameTruyen + ". Nội dung bình luận: " + binhluan.Noi_dung },
                                    {"So_lan_canh_cao", "0" }
                                };
                                    await client.SetAsync("Vi_pham/001", data);
                                }
                                else
                                {
                                    var dict1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(re.Body);
                                    int dem1 = 0;
                                    foreach (var j in dict1)
                                    {
                                        dem1 = Convert.ToInt32(j.Key);
                                    }
                                    string report = "";
                                    for (int j = 0; j < 3 - dem1.ToString().Length; j++)
                                    {
                                        report += "0";
                                    }
                                    report += dem1.ToString();
                                    Dictionary<string, string> data = new Dictionary<string, string> {
                                    {"Id_bi_tocao", binhluan.ID_nguoidung.ToString() },
                                    {"Noi_dung_to_cao", "Tên người bình luận: " + res6.ResultAs<string>() +
                                        "bị báo cáo bởi người dùng " + user.User.Info.Uid + " có tên là " + user.User.Info.DisplayName + ". Hãy kiểm tra nội " +
                                            $"dung bình luận này trong " +
                                            "truyện " + nameTruyen + ". Nội dung bình luận: " + binhluan.Noi_dung },
                                    {"So_lan_canh_cao", "0" }
                                };
                                    await client.SetAsync("Vi_pham/" + report, data);
                                }
                                MessageBox.Show("Đã gửi tin nhắn đến quản trị viên.\n\r Vui lòng đợi phản hồi.", "Thành công");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi: {ex.ToString()}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    };
                    if (user.User.Info.Uid == binhluan.ID_nguoidung)
                    {
                        btnReport.Enabled = false;
                    }
                    if (binhluan.To_cao == true)
                    {
                        btnReport.BackColor = System.Drawing.Color.Yellow;
                        btnReport.Text = "Bị tố cáo";
                    }

                }
            }
            else
            {
                return;
            }
  
        }

        private async void iconButtonRecom_Click(object sender, EventArgs e)
        {
            if (iconButtonRecom.BackColor == System.Drawing.Color.Transparent)
            {
                

                FirestoreDb db = FirestoreDb.Create("healtruyen");
                CollectionReference truyen = db.Collection("Truyen");
                
                DocumentReference collectionRef = db.Collection("Truyen").Document(idTruyen);
                DocumentSnapshot snapshot = await collectionRef.GetSnapshotAsync();

                int Decu = snapshot.GetValue<int>("De_cu");
                Decu++;

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "De_cu", Decu },
                };
                DocumentReference doc = truyen.Document(idTruyen);
                await doc.UpdateAsync(updates);
                iconButtonRecom.BackColor = System.Drawing.Color.FromArgb(220, 247, 253);
                MessageBox.Show("Bạn đã đề cử thành công!");
            }
            else
            {
                MessageBox.Show("Lỗi! Không thể đề cử.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonRate_Click(object sender, EventArgs e)
        {

            Form formBackground = new Form();
            iconButtonRate.IconColor = System.Drawing.Color.Black;
            try
            {
                using (Danhgia danhgia = new Danhgia(idTruyen, nameTruyen))
                {
                    formBackground.StartPosition = FormStartPosition.CenterScreen;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = System.Drawing.Color.Black;
                    /*formBackground.WindowState = FormWindowState.Maximized;*/
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.Show();

                    danhgia.Owner = formBackground;
                    danhgia.StartPosition = FormStartPosition.CenterScreen;
                    /*danhgia.BringToFront();*/
                    danhgia.ShowDialog();

                    formBackground.Dispose();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private async void btnPrevChap_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            if (currentChap == 1)
            {
                return;
            }
            currentChap--;
            string outp = "";
            for (int i = 0; i < 4 - currentChap.ToString().Length; i++)
            {
                outp += "0";
            }
            comboBox1.Text = "Chương " + outp + currentChap.ToString();
            /*this.Close();
            tc.openChildForm(new Doc_Truyen(idTruyen, user, currentChap, tc));*/
            Task<Interact.Chapter> res = Interact.getNovel(nameTruyen, currentChap.ToString());
            Interact.Chapter chapter = new Interact.Chapter();
            chapter = await res;
            contentChap.Text = chapter.Content;
            labelName.Text = chapter.Title;
            Task<DocumentSnapshot> res1 = Interact.getInfoNovel(nameTruyen);
            DocumentSnapshot novel = await res1;
            iconButton2.Text = novel.GetValue<string>("Tac_gia");

            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirebaseResponse res3 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/");
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res3.Body);

            if (dict is null)
            {
                Panel panel = new Panel();
                panel5.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
                panel.Height = 216;
                panel.AutoSize = true;

                Label binhluan = new Label();
                binhluan.Text = "Hiện không tồn tại bình luận của người dùng. Bạn hãy bình luận giúp truyện nhé!";
                binhluan.Font = new Font("League Spartan", 16, FontStyle.Regular);
                binhluan.AutoSize = true;

                panel.Controls.Add(binhluan);
                return;
            }
            //show data
            var dem = 0;
            foreach (var i in dict)
            {
                dem = Convert.ToInt32(i.Key);
            }
            for (int i = 1; i <= dem; i++)
            {
                string dembl = "0";
                if (i < 10)
                {
                    dembl += "0" + i.ToString();
                }
                else if (i < 100)
                {
                    dembl += i.ToString();
                }
                else
                {
                    dembl = i.ToString();
                }
                FirebaseResponse res4 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl);

                Binhluan binhluan = res4.ResultAs<Binhluan>();
                if (binhluan is null) continue;
                string chapId = "";
                int cnt = 4 - currentChap.ToString().Length;
                for (int id = 0; id < cnt; id++)
                {
                    chapId += "0";
                }
                chapId += currentChap.ToString();

                string id_chuong = idTruyen + chapId;

                if (binhluan.ID_chuong != id_chuong) continue;

                Panel panel = new Panel();
                panel5.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
                panel.Height = 216;

                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                panel.Controls.Add(tableLayoutPanel);
                tableLayoutPanel.Dock = DockStyle.Fill;
                tableLayoutPanel.RowCount = 1;
                tableLayoutPanel.ColumnCount = 2;
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));



                PictureBox pictureBox = new PictureBox();
                tableLayoutPanel.Controls.Add(pictureBox, 0, 0);
                pictureBox.Dock = DockStyle.Fill;

                FirebaseResponse res5 = await client.GetAsync("Nguoi_dung/" + binhluan.ID_nguoidung + "/Anh_dai_dien");
                string base64String = res5.ResultAs<string>();
                byte[] imageBytes = Convert.FromBase64String(base64String);

                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(memoryStream);
                    pictureBox.Image = bitmap;
                }

                Panel panel1 = new Panel();
                tableLayoutPanel.Controls.Add(panel1);
                panel1.Dock = DockStyle.Fill;

                TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
                panel1.Controls.Add(tableLayoutPanel1);
                tableLayoutPanel1.Dock = DockStyle.Top;
                tableLayoutPanel1.BringToFront();
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

                FirebaseResponse res6 = await client.GetAsync("Nguoi_dung/" + binhluan.ID_nguoidung + "/TK_dangnhap");
                Label labelName = new Label();
                tableLayoutPanel1.Controls.Add(labelName, 0, 0);
                labelName.AutoSize = true;
                labelName.Font = new Font("League Spartan", 16, FontStyle.Regular);
                /*var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(binhluan.ID_nguoidung);*/
                /*var user1 = client.GetAsync(binhluan.ID_nguoidung);*/
                labelName.Text = res6.ResultAs<string>();

                Label date = new Label();
                tableLayoutPanel1.Controls.Add(date, 1, 0);
                date.AutoSize = true;
                date.Font = new Font("League Spartan", 16, FontStyle.Regular);
                string dateStr = binhluan.TG_binh_luan.ToString();
                System.DateTime dateTime = System.DateTime.Parse(dateStr, CultureInfo.InvariantCulture);
                System.DateTime dateNow = System.DateTime.Now;
                TimeSpan elapsedTime = dateNow - dateTime;
                int elapsedSeconds = (int)elapsedTime.TotalSeconds;
                int elapsedMinutes = (int)elapsedTime.TotalMinutes;
                int elapsedHours = (int)elapsedTime.TotalHours;
                int elapsedDays = (int)elapsedTime.TotalDays;
                int elapsedMonth = elapsedDays / 30;
                int elapsedYear = elapsedMonth / 12;
                if (elapsedYear > 0)
                {
                    date.Text = elapsedYear.ToString() + " năm trước";
                }
                else if (elapsedMonth > 0)
                {
                    date.Text = elapsedMonth.ToString() + " tháng trước";
                }
                else if (elapsedDays > 0)
                {
                    date.Text = elapsedDays.ToString() + " ngày trước";
                }
                else if (elapsedHours > 0)
                {
                    date.Text = elapsedHours.ToString() + " giờ trước";
                }
                else if (elapsedMinutes > 0)
                {
                    date.Text = elapsedMinutes.ToString() + " phút trước";
                }
                else
                {
                    date.Text = elapsedSeconds.ToString() + " giây trước";
                }




                TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
                panel1.Controls.Add(tableLayoutPanel2);
                tableLayoutPanel2.Dock = DockStyle.Bottom;
                tableLayoutPanel2.RowCount = 1;
                tableLayoutPanel2.ColumnCount = 4;
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));

                Label content = new Label();
                panel1.Controls.Add(content);
                content.AutoSize = true;
                content.Font = new Font("League Spartan", 14, FontStyle.Regular);
                content.Dock = DockStyle.Top;
                content.Text = binhluan.Noi_dung;
                content.BringToFront();

                IconButton btnLike = new IconButton();
                tableLayoutPanel2.Controls.Add(btnLike, 2, 0);
                btnLike.Dock = DockStyle.Fill;
                btnLike.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btnLike.IconChar = IconChar.Heart;
                btnLike.IconSize = 30;
                btnLike.Text = binhluan.Luot_thich.ToString() + "\nThích";
                btnLike.TextAlign = ContentAlignment.MiddleRight;
                btnLike.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnLike.Click += async (s, ev) =>
                {
                    //Hàm click code ở đây

                    int so_like = binhluan.Luot_thich;
                    so_like++;
                    await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl + "/Luot_thich", so_like);
                    btnLike.BackColor = System.Drawing.Color.Red;
                    btnLike.IconColor = System.Drawing.Color.White;
                    btnLike.Enabled = false;
                    btnLike.Text = so_like.ToString() + "\nThích";
                    btnLike.ForeColor = System.Drawing.Color.Red;
                };

                IconButton btndelete = new IconButton();
                tableLayoutPanel2.Controls.Add(btndelete, 1, 0);
                btndelete.Dock = DockStyle.Fill;
                btndelete.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btndelete.IconChar = IconChar.Trash;
                btndelete.IconSize = 30;
                btndelete.Text = "Xóa";
                btndelete.TextAlign = ContentAlignment.MiddleRight;
                btndelete.TextImageRelation = TextImageRelation.ImageBeforeText;
                btndelete.Click += async (s, ev) =>
                {
                    int role = 0;
                    FirebaseResponse res7 = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Vaitro");
                    role = res7.ResultAs<int>();
                    if (role == 0)
                    {
                        await client.DeleteAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl);
                        FirestoreDb db = FirestoreDb.Create("healtruyen");
                        CollectionReference truyen = db.Collection("Truyen");
                        nameTruyen = nameTruyen.ToUpper();
                        Query q = truyen.WhereEqualTo("Ten", nameTruyen);
                        QuerySnapshot snapshots = await q.GetSnapshotAsync();
                        string id = "";
                        if (snapshots.Documents.Count > 0)
                        {
                            id = snapshots.Documents[0].Id;
                        }
                        DocumentReference collectionRef = db.Collection("Truyen").Document(id);
                        DocumentSnapshot snapshot = await collectionRef.GetSnapshotAsync();

                        int So_binhluan = snapshot.GetValue<int>("Binh_luan");

                        So_binhluan++;

                        Dictionary<string, object> updates = new Dictionary<string, object>
                        {
                            { "Binh_luan", So_binhluan },
                        };
                        DocumentReference doc = truyen.Document(id);
                        await doc.UpdateAsync(updates);
                        panel.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Bạn không có quyền xóa bình luận!");
                    }
                };

                IconButton btnReport = new IconButton();
                tableLayoutPanel2.Controls.Add(btnReport, 3, 0);
                btnReport.Dock = DockStyle.Fill;
                btnReport.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btnReport.IconChar = IconChar.Flag;
                btnReport.IconSize = 30;
                btnReport.Text = "Tố cáo";
                btnReport.TextAlign = ContentAlignment.MiddleRight;
                btnReport.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnReport.Click += async (s, ev) =>
                {
                    //Hàm click code ở đây 
                    if (user.User.Info.Uid != binhluan.ID_nguoidung)
                    {
                        await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl + "/To_cao", true);
                        /*MessageBox.Show("Tố cáo thành công!Bạn hãy đợi Admin xử lý!");*/
                        btnReport.BackColor = System.Drawing.Color.Yellow;
                        btnReport.Text = "Bị tố cáo";
                        try
                        {
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                            mail.From = new MailAddress(user.User.Info.Email);
                            mail.To.Add(_from);
                            mail.Subject = $"Bình luận của người dùng có ID là {binhluan.ID_nguoidung} ";
                            mail.IsBodyHtml = true;
                            mail.Body = $"<div> Tên người bình luận: {res6.ResultAs<string>()} </div> <br> " +
                            $"Bị báo cáo bởi người dùng {user.User.Info.Uid} có tên là {user.User.Info.DisplayName} <br> Hãy kiểm tra nội dung bình luận này trong truyện {nameTruyen} <br> Nội dung bình luận: {binhluan.Noi_dung}";

                            mail.Priority = MailPriority.High;

                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(_from, _pass);
                            SmtpServer.EnableSsl = true;

                            SmtpServer.Send(mail);
                            FirebaseResponse re = await client.GetAsync("Vi_pham/");
                            if (re.Body == "null")
                            {
                                Dictionary<string, string> data = new Dictionary<string, string> {
                                    {"Id_bi_tocao", binhluan.ID_nguoidung.ToString() },
                                    {"Noi_dung_to_cao", "Tên người bình luận: " + res6.ResultAs<string>() +
                                        "bị báo cáo bởi người dùng " + user.User.Info.Uid + " có tên là " + user.User.Info.DisplayName + ". Hãy kiểm tra nội " +
                                            $"dung bình luận này trong " +
                                            "truyện " + nameTruyen + ". Nội dung bình luận: " + binhluan.Noi_dung },
                                };
                                await client.SetAsync("Vi_pham/001", data);
                            }
                            else
                            {
                                var dict1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(re.Body);
                                int dem1 = 0;
                                foreach (var j in dict1)
                                {
                                    dem1 = Convert.ToInt32(j.Key);
                                }
                                string report = "";
                                for (int j = 0; j < 3 - dem1.ToString().Length; j++)
                                {
                                    report += "0";
                                }
                                report += dem1.ToString();
                                Dictionary<string, string> data = new Dictionary<string, string> {
                                    {"Id_bi_tocao", binhluan.ID_nguoidung.ToString() },
                                    {"Noi_dung_to_cao", "Tên người bình luận: " + res6.ResultAs<string>() +
                                        "bị báo cáo bởi người dùng " + user.User.Info.Uid + " có tên là " + user.User.Info.DisplayName + ". Hãy kiểm tra nội " +
                                            $"dung bình luận này trong " +
                                            "truyện " + nameTruyen + ". Nội dung bình luận: " + binhluan.Noi_dung },
                                };
                                await client.SetAsync("Vi_pham/" + report, data);
                            }
                            MessageBox.Show("Đã gửi tin nhắn đến quản trị viên.\n\r Vui lòng đợi phản hồi.", "Thành công");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.ToString()}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                if (user.User.Info.Uid == binhluan.ID_nguoidung)
                {
                    btnReport.Enabled = false;
                }
                if (binhluan.To_cao == true)
                {
                    btnReport.BackColor = System.Drawing.Color.Yellow;
                    btnReport.Text = "Bị tố cáo";
                }

            }
            if (panel5.Controls.Count == 0)
            {
                Panel panel = new Panel();
                panel5.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
                panel.Height = 216;
                panel.AutoSize = true;

                Label binhluan = new Label();
                binhluan.Text = "Hiện không tồn tại bình luận của người dùng. Bạn hãy bình luận giúp truyện nhé!";
                binhluan.Font = new Font("League Spartan", 16, FontStyle.Regular);
                binhluan.AutoSize = true;

                panel.Controls.Add(binhluan);
            }
        }

        private async void btnPostChap_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            if (currentChap == numChap)
            {
                return;
            }
            currentChap++;
            string outp = "";
            for (int i = 0; i < 4 - currentChap.ToString().Length; i++)
            {
                outp += "0";
            }
            comboBox1.Text = "Chương " + outp + currentChap.ToString();
            /*this.Close();
            tc.openChildForm(new Doc_Truyen(idTruyen, user, currentChap, tc));*/
            Task<Interact.Chapter> res = Interact.getNovel(nameTruyen, currentChap.ToString());
            Interact.Chapter chapter = new Interact.Chapter();
            chapter = await res;
            contentChap.Text = chapter.Content;
            labelName.Text = chapter.Title;
            Task<DocumentSnapshot> res1 = Interact.getInfoNovel(nameTruyen);
            DocumentSnapshot novel = await res1;
            iconButton2.Text = novel.GetValue<string>("Tac_gia");

            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirebaseResponse res3 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/");
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res3.Body);

            if (dict is null)
            {
                Panel panel = new Panel();
                panel5.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
                panel.Height = 216;
                panel.AutoSize = true;

                Label binhluan = new Label();
                binhluan.Text = "Hiện không tồn tại bình luận của người dùng. Bạn hãy bình luận giúp truyện nhé!";
                binhluan.Font = new Font("League Spartan", 16, FontStyle.Regular);
                binhluan.AutoSize = true;

                panel.Controls.Add(binhluan);
                return;
            }
            //show data
            var dem = 0;
            foreach (var i in dict)
            {
                dem = Convert.ToInt32(i.Key);
            }
            for (int i = 1; i <= dem; i++)
            {
                string dembl = "0";
                if (i < 10)
                {
                    dembl += "0" + i.ToString();
                }
                else if (i < 100)
                {
                    dembl += i.ToString();
                }
                else
                {
                    dembl = i.ToString();
                }
                FirebaseResponse res4 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl);

                Binhluan binhluan = res4.ResultAs<Binhluan>();
                if (binhluan is null) continue;
                string chapId = "";
                int cnt = 4 - currentChap.ToString().Length;
                for (int id = 0; id < cnt; id++)
                {
                    chapId += "0";
                }
                chapId += currentChap.ToString();

                string id_chuong = idTruyen + chapId;
                
                if (binhluan.ID_chuong != id_chuong) continue;
                
                Panel panel = new Panel();
                panel5.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
                panel.Height = 216;

                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                panel.Controls.Add(tableLayoutPanel);
                tableLayoutPanel.Dock = DockStyle.Fill;
                tableLayoutPanel.RowCount = 1;
                tableLayoutPanel.ColumnCount = 2;
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));



                PictureBox pictureBox = new PictureBox();
                tableLayoutPanel.Controls.Add(pictureBox, 0, 0);
                pictureBox.Dock = DockStyle.Fill;

                FirebaseResponse res5 = await client.GetAsync("Nguoi_dung/" + binhluan.ID_nguoidung + "/Anh_dai_dien");
                string base64String = res5.ResultAs<string>();
                byte[] imageBytes = Convert.FromBase64String(base64String);

                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(memoryStream);
                    pictureBox.Image = bitmap;
                }

                Panel panel1 = new Panel();
                tableLayoutPanel.Controls.Add(panel1);
                panel1.Dock = DockStyle.Fill;

                TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
                panel1.Controls.Add(tableLayoutPanel1);
                tableLayoutPanel1.Dock = DockStyle.Top;
                tableLayoutPanel1.BringToFront();
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

                FirebaseResponse res6 = await client.GetAsync("Nguoi_dung/" + binhluan.ID_nguoidung + "/TK_dangnhap");
                Label labelName = new Label();
                tableLayoutPanel1.Controls.Add(labelName, 0, 0);
                labelName.AutoSize = true;
                labelName.Font = new Font("League Spartan", 16, FontStyle.Regular);
                /*var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(binhluan.ID_nguoidung);*/
                /*var user1 = client.GetAsync(binhluan.ID_nguoidung);*/
                labelName.Text = res6.ResultAs<string>();

                Label date = new Label();
                tableLayoutPanel1.Controls.Add(date, 1, 0);
                date.AutoSize = true;
                date.Font = new Font("League Spartan", 16, FontStyle.Regular);
                string dateStr = binhluan.TG_binh_luan.ToString();
                System.DateTime dateTime = System.DateTime.Parse(dateStr, CultureInfo.InvariantCulture);
                System.DateTime dateNow = System.DateTime.Now;
                TimeSpan elapsedTime = dateNow - dateTime;
                int elapsedSeconds = (int)elapsedTime.TotalSeconds;
                int elapsedMinutes = (int)elapsedTime.TotalMinutes;
                int elapsedHours = (int)elapsedTime.TotalHours;
                int elapsedDays = (int)elapsedTime.TotalDays;
                int elapsedMonth = elapsedDays / 30;
                int elapsedYear = elapsedMonth / 12;
                if (elapsedYear > 0)
                {
                    date.Text = elapsedYear.ToString() + " năm trước";
                }
                else if (elapsedMonth > 0)
                {
                    date.Text = elapsedMonth.ToString() + " tháng trước";
                }
                else if (elapsedDays > 0)
                {
                    date.Text = elapsedDays.ToString() + " ngày trước";
                }
                else if (elapsedHours > 0)
                {
                    date.Text = elapsedHours.ToString() + " giờ trước";
                }
                else if (elapsedMinutes > 0)
                {
                    date.Text = elapsedMinutes.ToString() + " phút trước";
                }
                else
                {
                    date.Text = elapsedSeconds.ToString() + " giây trước";
                }




                TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
                panel1.Controls.Add(tableLayoutPanel2);
                tableLayoutPanel2.Dock = DockStyle.Bottom;
                tableLayoutPanel2.RowCount = 1;
                tableLayoutPanel2.ColumnCount = 4;
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));

                Label content = new Label();
                panel1.Controls.Add(content);
                content.AutoSize = true;
                content.Font = new Font("League Spartan", 14, FontStyle.Regular);
                content.Dock = DockStyle.Top;
                content.Text = binhluan.Noi_dung;
                content.BringToFront();

                IconButton btnLike = new IconButton();
                tableLayoutPanel2.Controls.Add(btnLike, 2, 0);
                btnLike.Dock = DockStyle.Fill;
                btnLike.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btnLike.IconChar = IconChar.Heart;
                btnLike.IconSize = 30;
                btnLike.Text = binhluan.Luot_thich.ToString() + "\nThích";
                btnLike.TextAlign = ContentAlignment.MiddleRight;
                btnLike.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnLike.Click += async (s, ev) =>
                {
                    //Hàm click code ở đây

                    int so_like = binhluan.Luot_thich;
                    so_like++;
                    await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl + "/Luot_thich", so_like);
                    btnLike.BackColor = System.Drawing.Color.Red;
                    btnLike.IconColor = System.Drawing.Color.White;
                    btnLike.Enabled = false;
                    btnLike.Text = so_like.ToString() + "\nThích";
                    btnLike.ForeColor = System.Drawing.Color.Red;
                };

                IconButton btndelete = new IconButton();
                tableLayoutPanel2.Controls.Add(btndelete, 1, 0);
                btndelete.Dock = DockStyle.Fill;
                btndelete.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btndelete.IconChar = IconChar.Trash;
                btndelete.IconSize = 30;
                btndelete.Text = "Xóa";
                btndelete.TextAlign = ContentAlignment.MiddleRight;
                btndelete.TextImageRelation = TextImageRelation.ImageBeforeText;
                btndelete.Click += async (s, ev) =>
                {
                    int role = 0;
                    FirebaseResponse res7 = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Vaitro");
                    role = res7.ResultAs<int>();
                    if (role == 0)
                    {
                        await client.DeleteAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl);
                        FirestoreDb db = FirestoreDb.Create("healtruyen");
                        CollectionReference truyen = db.Collection("Truyen");
                        nameTruyen = nameTruyen.ToUpper();
                        Query q = truyen.WhereEqualTo("Ten", nameTruyen);
                        QuerySnapshot snapshots = await q.GetSnapshotAsync();
                        string id = "";
                        if (snapshots.Documents.Count > 0)
                        {
                            id = snapshots.Documents[0].Id;
                        }
                        DocumentReference collectionRef = db.Collection("Truyen").Document(id);
                        DocumentSnapshot snapshot = await collectionRef.GetSnapshotAsync();

                        int So_binhluan = snapshot.GetValue<int>("Binh_luan");

                        So_binhluan++;

                        Dictionary<string, object> updates = new Dictionary<string, object>
                        {
                            { "Binh_luan", So_binhluan },
                        };
                        DocumentReference doc = truyen.Document(id);
                        await doc.UpdateAsync(updates);
                        panel.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Bạn không có quyền xóa bình luận!");
                    }
                };

                IconButton btnReport = new IconButton();
                tableLayoutPanel2.Controls.Add(btnReport, 3, 0);
                btnReport.Dock = DockStyle.Fill;
                btnReport.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btnReport.IconChar = IconChar.Flag;
                btnReport.IconSize = 30;
                btnReport.Text = "Tố cáo";
                btnReport.TextAlign = ContentAlignment.MiddleRight;
                btnReport.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnReport.Click += async (s, ev) =>
                {
                    //Hàm click code ở đây 
                    if (user.User.Info.Uid != binhluan.ID_nguoidung)
                    {
                        await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl + "/To_cao", true);
                        /*MessageBox.Show("Tố cáo thành công!Bạn hãy đợi Admin xử lý!");*/
                        btnReport.BackColor = System.Drawing.Color.Yellow;
                        btnReport.Text = "Bị tố cáo";
                        try
                        {
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                            mail.From = new MailAddress(user.User.Info.Email);
                            mail.To.Add(_from);
                            mail.Subject = $"Bình luận của người dùng có ID là {binhluan.ID_nguoidung} ";
                            mail.IsBodyHtml = true;
                            mail.Body = $"<div> Tên người bình luận: {res6.ResultAs<string>()} </div> <br> " +
                            $"Bị báo cáo bởi người dùng {user.User.Info.Uid} có tên là {user.User.Info.DisplayName} <br> Hãy kiểm tra nội dung bình luận này trong truyện {nameTruyen} <br> Nội dung bình luận: {binhluan.Noi_dung}";

                            mail.Priority = MailPriority.High;

                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(_from, _pass);
                            SmtpServer.EnableSsl = true;

                            SmtpServer.Send(mail);
                            FirebaseResponse re = await client.GetAsync("Vi_pham/");
                            if (re.Body == "null")
                            {
                                Dictionary<string, string> data = new Dictionary<string, string> {
                                    {"Id_bi_tocao", binhluan.ID_nguoidung.ToString() },
                                    {"Noi_dung_to_cao", "Tên người bình luận: " + res6.ResultAs<string>() +
                                        "bị báo cáo bởi người dùng " + user.User.Info.Uid + " có tên là " + user.User.Info.DisplayName + ". Hãy kiểm tra nội " +
                                            $"dung bình luận này trong " +
                                            "truyện " + nameTruyen + ". Nội dung bình luận: " + binhluan.Noi_dung },
                                    {"So_lan_canh_cao", "0" }
                                };
                                await client.SetAsync("Vi_pham/001", data);
                            }
                            else
                            {
                                var dict1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(re.Body);
                                int dem1 = 0;
                                foreach (var j in dict1)
                                {
                                    dem1 = Convert.ToInt32(j.Key);
                                }
                                string report = "";
                                for (int j = 0; j < 3 - dem1.ToString().Length; j++)
                                {
                                    report += "0";
                                }
                                report += dem1.ToString();
                                Dictionary<string, string> data = new Dictionary<string, string> {
                                    {"Id_bi_tocao", binhluan.ID_nguoidung.ToString() },
                                    {"Noi_dung_to_cao", "Tên người bình luận: " + res6.ResultAs<string>() +
                                        "bị báo cáo bởi người dùng " + user.User.Info.Uid + " có tên là " + user.User.Info.DisplayName + ". Hãy kiểm tra nội " +
                                            $"dung bình luận này trong " +
                                            "truyện " + nameTruyen + ". Nội dung bình luận: " + binhluan.Noi_dung },
                                    {"So_lan_canh_cao", "0" }
                                };
                                await client.SetAsync("Vi_pham/" + report, data);
                            }
                            MessageBox.Show("Đã gửi tin nhắn đến quản trị viên.\n\r Vui lòng đợi phản hồi.", "Thành công");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.ToString()}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                if (user.User.Info.Uid == binhluan.ID_nguoidung)
                {
                    btnReport.Enabled = false;
                }
                if (binhluan.To_cao == true)
                {
                    btnReport.BackColor = System.Drawing.Color.Yellow;
                    btnReport.Text = "Bị tố cáo";
                }

            }
            if (panel5.Controls.Count == 0)
            {
                Panel panel = new Panel();
                panel5.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
                panel.Height = 216;
                panel.AutoSize = true;

                Label binhluan = new Label();
                binhluan.Text = "Hiện không tồn tại bình luận của người dùng. Bạn hãy bình luận giúp truyện nhé!";
                binhluan.Font = new Font("League Spartan", 16, FontStyle.Regular);
                binhluan.AutoSize = true;

                panel.Controls.Add(binhluan);
            }

        }

        private void iconButtonRate_MouseClick(object sender, MouseEventArgs e)
        {
            iconButtonRate.IconColor = System.Drawing.Color.IndianRed;
        }

        private void iconButtonRate_MouseHover(object sender, EventArgs e)
        {
            iconButtonRate.IconColor = System.Drawing.Color.IndianRed;
        }

        private void iconButtonRecom_MouseClick(object sender, MouseEventArgs e)
        {
            iconButtonRecom.IconColor = System.Drawing.Color.IndianRed;
        }

        private void iconButtonRecom_MouseHover(object sender, EventArgs e)
        {
            iconButtonRecom.IconColor = System.Drawing.Color.IndianRed;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void btnSendComment_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);

                var config = new FirebaseAuthConfig
                {
                    ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
                    AuthDomain = "healtruyen.firebaseapp.com",
                    Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
                };
                var client1 = new FirebaseAuthClient(config);
                var userId = client1.User.Uid;

                FirebaseResponse res = await client.GetAsync($"Truyen/{idTruyen}/Binh_luan/");
                var dict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(res.Body);

                //show data
                var dem = 0;
                if (!(dict is null))
                {
                    foreach (var i in dict)
                    {
                        dem = Convert.ToInt32(i.Key);
                    }
                }
                dem++;
                string dembl = "0";
                if (dem < 10)
                {
                    dembl += "0" + dem.ToString();
                }
                else if (dem < 100)
                {
                    dembl += dem.ToString();
                }
                else
                {
                    dembl = dem.ToString();
                }

                string chapId = "";
                int cnt = 4 - currentChap.ToString().Length;
                for (int i = 0; i < cnt; i++)
                {
                    chapId += "0";
                }
                chapId += currentChap;
                string idchuong = idTruyen + chapId;
                string tgbl = System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

                Binhluan binhluan = new Binhluan()
                {
                    ID_chuong = idchuong,
                    ID_nguoidung = user.User.Info.Uid,
                    Luot_thich = 0,
                    Noi_dung = textBox1.Text,
                    TG_binh_luan = tgbl,
                    To_cao = false

                };

                await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl, binhluan);

                FirestoreDb db = FirestoreDb.Create("healtruyen");
                CollectionReference truyen = db.Collection("Truyen");
                nameTruyen = nameTruyen.ToUpper();
                Query q = truyen.WhereEqualTo("Ten", nameTruyen);
                QuerySnapshot snapshots = await q.GetSnapshotAsync();
                string id = "";
                if (snapshots.Documents.Count > 0)
                {
                    id = snapshots.Documents[0].Id;
                }
                DocumentReference collectionRef = db.Collection("Truyen").Document(id);
                DocumentSnapshot snapshot = await collectionRef.GetSnapshotAsync();

                int So_binhluan = snapshot.GetValue<int>("Binh_luan");

                So_binhluan++;

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "Binh_luan", So_binhluan },
                };
                DocumentReference doc = truyen.Document(id);
                await doc.UpdateAsync(updates);
                textBox1.Text = "";

                Panel panel = new Panel();
                panel5.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
                panel.Height = 216;

                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                panel.Controls.Add(tableLayoutPanel);
                tableLayoutPanel.Dock = DockStyle.Fill;
                tableLayoutPanel.RowCount = 1;
                tableLayoutPanel.ColumnCount = 2;
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));



                PictureBox pictureBox = new PictureBox();
                tableLayoutPanel.Controls.Add(pictureBox, 0, 0);
                pictureBox.Dock = DockStyle.Fill;

                FirebaseResponse res5 = await client.GetAsync("Nguoi_dung/" + binhluan.ID_nguoidung + "/Anh_dai_dien");
                string base64String = res5.ResultAs<string>();
                byte[] imageBytes = Convert.FromBase64String(base64String);

                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(memoryStream);
                    pictureBox.Image = bitmap;
                }

                Panel panel1 = new Panel();
                tableLayoutPanel.Controls.Add(panel1);
                panel1.Dock = DockStyle.Fill;

                TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
                panel1.Controls.Add(tableLayoutPanel1);
                tableLayoutPanel1.Dock = DockStyle.Top;
                tableLayoutPanel1.BringToFront();
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

                FirebaseResponse res6 = await client.GetAsync("Nguoi_dung/" + binhluan.ID_nguoidung + "/TK_dangnhap");
                Label labelName = new Label();
                tableLayoutPanel1.Controls.Add(labelName, 0, 0);
                labelName.AutoSize = true;
                labelName.Font = new Font("League Spartan", 16, FontStyle.Regular);
                /*var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(binhluan.ID_nguoidung);*/
                /*var user1 = client.GetAsync(binhluan.ID_nguoidung);*/
                labelName.Text = res6.ResultAs<string>();

                Label date = new Label();
                tableLayoutPanel1.Controls.Add(date, 1, 0);
                date.AutoSize = true;
                date.Font = new Font("League Spartan", 16, FontStyle.Regular);
                string dateStr = binhluan.TG_binh_luan.ToString();
                System.DateTime dateTime = System.DateTime.Parse(dateStr, CultureInfo.InvariantCulture);
                System.DateTime dateNow = System.DateTime.Now;
                TimeSpan elapsedTime = dateNow - dateTime;
                int elapsedSeconds = (int)elapsedTime.TotalSeconds;
                int elapsedMinutes = (int)elapsedTime.TotalMinutes;
                int elapsedHours = (int)elapsedTime.TotalHours;
                int elapsedDays = (int)elapsedTime.TotalDays;
                int elapsedMonth = elapsedDays / 30;
                int elapsedYear = elapsedMonth / 12;
                if (elapsedYear > 0)
                {
                    date.Text = elapsedYear.ToString() + " năm trước";
                }
                else if (elapsedMonth > 0)
                {
                    date.Text = elapsedMonth.ToString() + " tháng trước";
                }
                else if (elapsedDays > 0)
                {
                    date.Text = elapsedDays.ToString() + " ngày trước";
                }
                else if (elapsedHours > 0)
                {
                    date.Text = elapsedHours.ToString() + " giờ trước";
                }
                else if (elapsedMinutes > 0)
                {
                    date.Text = elapsedMinutes.ToString() + " phút trước";
                }
                else
                {
                    date.Text = elapsedSeconds.ToString() + " giây trước";
                }


                TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
                panel1.Controls.Add(tableLayoutPanel2);
                tableLayoutPanel2.Dock = DockStyle.Bottom;
                tableLayoutPanel2.RowCount = 1;
                tableLayoutPanel2.ColumnCount = 4;
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));

                Label content = new Label();
                panel1.Controls.Add(content);
                content.AutoSize = true;
                content.Font = new Font("League Spartan", 14, FontStyle.Regular);
                content.Dock = DockStyle.Top;
                content.Text = binhluan.Noi_dung;
                content.BringToFront();

                IconButton btnLike = new IconButton();
                tableLayoutPanel2.Controls.Add(btnLike, 2, 0);
                btnLike.Dock = DockStyle.Fill;
                btnLike.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btnLike.IconChar = IconChar.Heart;
                btnLike.IconSize = 30;
                btnLike.Text = binhluan.Luot_thich.ToString() + "\nThích";
                btnLike.TextAlign = ContentAlignment.MiddleRight;
                btnLike.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnLike.Click += async (s, ev) =>
                {
                    //Hàm click code ở đây

                    int so_like = binhluan.Luot_thich;
                    so_like++;
                    await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl + "/Luot_thich", so_like);
                    btnLike.BackColor = System.Drawing.Color.Red;
                    btnLike.IconColor = System.Drawing.Color.White;
                    btnLike.Enabled = false;
                    btnLike.Text = so_like.ToString() + "\nThích";
                    btnLike.ForeColor = System.Drawing.Color.Red;
                };

                IconButton btndelete = new IconButton();
                tableLayoutPanel2.Controls.Add(btndelete, 1, 0);
                btndelete.Dock = DockStyle.Fill;
                btndelete.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btndelete.IconChar = IconChar.Trash;
                btndelete.IconSize = 30;
                btndelete.Text = "Xóa";
                btndelete.TextAlign = ContentAlignment.MiddleRight;
                btndelete.TextImageRelation = TextImageRelation.ImageBeforeText;
                btndelete.Click += async (s, ev) =>
                {
                    int role = 0;
                    FirebaseResponse res7 = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Vaitro");
                    role = res7.ResultAs<int>();
                    if (role == 0)
                    {
                        await client.DeleteAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl);
                        panel.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Bạn không có quyền xóa bình luận!");
                    }
                };

                IconButton btnReport = new IconButton();
                tableLayoutPanel2.Controls.Add(btnReport, 3, 0);
                btnReport.Dock = DockStyle.Fill;
                btnReport.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btnReport.IconChar = IconChar.Flag;
                btnReport.IconSize = 30;
                btnReport.Text = "Tố cáo";
                btnReport.TextAlign = ContentAlignment.MiddleRight;
                btnReport.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnReport.Click += async (s, ev) =>
                {
                    //Hàm click code ở đây 
                    //Hàm click code ở đây 
                    if (user.User.Info.Uid != binhluan.ID_nguoidung)
                    {
                        await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl + "/To_cao", true);
                        /*MessageBox.Show("Tố cáo thành công!Bạn hãy đợi Admin xử lý!");*/
                        btnReport.BackColor = System.Drawing.Color.Yellow;
                        btnReport.Text = "Bị tố cáo";
                        try
                        {
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                            mail.From = new MailAddress(user.User.Info.Email);
                            mail.To.Add(_from);
                            mail.Subject = $"Bình luận của người dùng có ID là {binhluan.ID_nguoidung} ";
                            mail.IsBodyHtml = true;
                            mail.Body = $"<div> Tên người bình luận: {res6.ResultAs<string>()} </div> <br> " +
                            $"Bị báo cáo bởi người dùng {user.User.Info.Uid} có tên là {user.User.Info.DisplayName} <br> Hãy kiểm tra nội dung bình luận này trong truyện {nameTruyen} <br> Nội dung bình luận: {binhluan.Noi_dung}";

                            mail.Priority = MailPriority.High;

                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(_from, _pass);
                            SmtpServer.EnableSsl = true;

                            SmtpServer.Send(mail);
                            FirebaseResponse re = await client.GetAsync("Vi_pham/");
                            if (re.Body == "null")
                            {
                                Dictionary<string, string> data = new Dictionary<string, string> {
                                    {"Id_bi_tocao", binhluan.ID_nguoidung.ToString() },
                                    {"Noi_dung_to_cao", "Tên người bình luận: " + res6.ResultAs<string>() +
                                        "bị báo cáo bởi người dùng " + user.User.Info.Uid + " có tên là " + user.User.Info.DisplayName + ". Hãy kiểm tra nội " +
                                            $"dung bình luận này trong " +
                                            "truyện " + nameTruyen + ". Nội dung bình luận: " + binhluan.Noi_dung },
                                    {"So_lan_canh_cao", "0" }
                                };
                                await client.SetAsync("Vi_pham/001", data);
                            }
                            else
                            {
                                var dict1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(re.Body);
                                int dem1 = 0;
                                foreach (var j in dict1)
                                {
                                    dem1 = Convert.ToInt32(j.Key);
                                }
                                string report = "";
                                for (int j = 0; j < 3 - dem1.ToString().Length; j++)
                                {
                                    report += "0";
                                }
                                report += dem1.ToString();
                                Dictionary<string, string> data = new Dictionary<string, string> {
                                    {"Id_bi_tocao", binhluan.ID_nguoidung.ToString() },
                                    {"Noi_dung_to_cao", "Tên người bình luận: " + res6.ResultAs<string>() +
                                        "bị báo cáo bởi người dùng " + user.User.Info.Uid + " có tên là " + user.User.Info.DisplayName + ". Hãy kiểm tra nội " +
                                            $"dung bình luận này trong " +
                                            "truyện " + nameTruyen + ". Nội dung bình luận: " + binhluan.Noi_dung },
                                    {"So_lan_canh_cao", "0" }
                                };
                                await client.SetAsync("Vi_pham/" + report, data);
                            }
                            MessageBox.Show("Đã gửi tin nhắn đến quản trị viên.\n\r Vui lòng đợi phản hồi.", "Thành công");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.ToString()}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                if (user.User.Info.Uid == binhluan.ID_nguoidung)
                {
                    btnReport.Enabled = false;
                }
                if (binhluan.To_cao == true)
                {
                    btnReport.BackColor = System.Drawing.Color.Yellow;
                    btnReport.Text = "Bị tố cáo";
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập gì cả!");
            }
            if (panel5.Controls.Count == 0)
            {
                Panel panel = new Panel();
                panel5.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
                panel.Height = 216;
                panel.AutoSize = true;

                Label binhluan = new Label();
                binhluan.Text = "Hiện không tồn tại bình luận của người dùng. Bạn hãy bình luận giúp truyện nhé!";
                binhluan.Font = new Font("League Spartan", 16, FontStyle.Regular);
                binhluan.AutoSize = true;

                panel.Controls.Add(binhluan);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void btnBookMark_Click(object sender, EventArgs e)
        {
          
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);

            //insert data into path Information/[data.Id]
            SetResponse response = await client.SetAsync("Nguoi_dung/" + user.User.Uid + "/Bookmark/" + idTruyen + "/Chuong_Dang_Doc", currentChap);
            MessageBox.Show("Data is inserted!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= so_chuong; i++)
            {
                if (i.ToString().Length == 1)
                    comboBox1.Items.Add("Chương 000" + i.ToString());
                else if (i.ToString().Length == 2)
                    comboBox1.Items.Add("Chương 00" + i.ToString());
                else if (i.ToString().Length == 3)
                    comboBox1.Items.Add("Chương 0" + i.ToString());
                else if (i.ToString().Length == 4)
                    comboBox1.Items.Add("Chương " + i.ToString());
            }
        }

        private void Enter_click(object sender, EventArgs e)
        {
            if (sender is ComboBox cb)
            {
                this.Close();
                string[] cbText = comboBox1.SelectedItem.ToString().Split(' ');
                currentChap = int.Parse(cbText[1]);
                tc.openChildForm(new Doc_Truyen(idTruyen, user, currentChap, tc));
            }
        }

        private async void btnExit_Click(object sender, FormClosedEventArgs e)
        {
            CRUD_lsd cRUD_Lsd = new CRUD_lsd();
            await cRUD_Lsd.Capnhat_lichsudoc(user.User.Uid, currentChap, idTruyen);
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            //truy xuất đến idtruyen 
            DocumentReference docReference = db.Collection("Truyen").Document(idTruyen);
            CollectionReference truyen = db.Collection("Truyen");
            //lấy dữ liệu truyện ra
            DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
            int soluotxem = 0;
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                soluotxem = novel.numRead + 1;
            }
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "Luot_xem", soluotxem }
            };
            DocumentReference doc = truyen.Document(idTruyen);
            await doc.UpdateAsync(updates);
            this.Close();
        }

        private async void Doc_Truyen_FormClosing(object sender, FormClosingEventArgs e)
        {
            CRUD_lsd cRUD_Lsd = new CRUD_lsd();
            await cRUD_Lsd.Capnhat_lichsudoc(user.User.Uid, currentChap, idTruyen);
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            //truy xuất đến idtruyen 
            DocumentReference docReference = db.Collection("Truyen").Document(idTruyen);
            CollectionReference truyen = db.Collection("Truyen");
            //lấy dữ liệu truyện ra
            DocumentSnapshot snapshot = await docReference.GetSnapshotAsync();
            int soluotxem = 0;
            if (snapshot.Exists)
            {
                Novel novel = snapshot.ConvertTo<Novel>();
                soluotxem = novel.numRead + 1;
            }
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "Luot_xem", soluotxem }
            };
            DocumentReference doc = truyen.Document(idTruyen);
            await doc.UpdateAsync(updates);
            this.Close();
        }
    }
}
