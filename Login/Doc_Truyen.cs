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
/*using System.Windows.Controls;*/
using System.Windows.Forms;
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

        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public Doc_Truyen(string nameTruyen, UserCredential user, int currentChap)
        {
            InitializeComponent();
            this.currentChap = currentChap;
            this.nameTruyen = nameTruyen;
            this.user = user;
        }

        private async void btnExit_Click(object sender, EventArgs e)
        {
            CRUD_lsd cRUD_Lsd = new CRUD_lsd();
            await cRUD_Lsd.Capnhat_lichsudoc(user.User.Uid, nameTruyen, idTruyen);
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
            nameTruyen = nameTruyen.ToUpper();
            Query q = collectionReference.WhereEqualTo("Ten", nameTruyen);
            QuerySnapshot qs = await q.GetSnapshotAsync();
            if (qs.Documents.Count == 0)
            {
                return;
            }
            idTruyen = qs.Documents[0].Id;
            string chapId = "";
            int cnt = 4 - numChapter.Length;
            for (int i = 0; i < cnt; i++)
            {
                chapId += "0";
            }
            chapId += numChapter;
            DocumentReference collectionRef = db.Collection("Truyen").Document(qs.Documents[0].Id).Collection("Chuong").Document(chapId);
            DocumentSnapshot snapshot = await collectionRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                
                /*Interact.Chapter chapter = snapshot.ConvertTo<Interact.Chapter>();*/
                
                contentChap.Text = snapshot.GetValue<string>("Noi_dung"); ;
                labelName.Text = snapshot.GetValue<string>("Tieu_de"); ;
                Task<DocumentSnapshot> res1 = Interact.getInfoNovel(nameTruyen);
                DocumentSnapshot novel = await res1;
                iconButton2.Text = novel.GetValue<string>("Tac_gia");
                numChap = novel.GetValue<int>("So_chuong");
                Task<string> res2 = Interact.getIdNovel(nameTruyen);
                IFirebaseConfig _firebaseConfig = new FirebaseConfig
                {
                    AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                    BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
                };
                IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
                FirebaseResponse res3 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/");
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res3.Body);

                if (dict.Count == 0) return;
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
                    tableLayoutPanel2.ColumnCount = 3;
                    tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76));
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
                    tableLayoutPanel2.Controls.Add(btnLike, 1, 0);
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
                        await client.SetAsync("Truyen/" + idTruyen + "/Binh_luan/" + dembl +"/Luot_thich", so_like);
                        btnLike.BackColor = System.Drawing.Color.Red;
                        btnLike.IconColor = System.Drawing.Color.White;
                        btnLike.Enabled = false;
                        btnLike.Text = so_like.ToString() + "\nThích";
                        btnLike.ForeColor = System.Drawing.Color.Red;
                    };
                    

                    IconButton btnReport = new IconButton();
                    tableLayoutPanel2.Controls.Add(btnReport, 2, 0);
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
            /*Task<Interact.Chapter> res = Interact.getNovel(nameTruyen, "1");*/
            /*Interact.Chapter chapter = new Interact.Chapter();*/
            /*var chapter = await res;*/
            /*contentChap.Text = chapter.Content;
            labelName.Text = chapter.Title;
            Task<Interact.Novel> res1 = Interact.getInfoNovel(nameTruyen);
            Interact.Novel novel = new Interact.Novel();
            novel = await res1;
            iconButton2.Text = novel.author;
            numChap = novel.cntChapter;
            Task<string> res2 = Interact.getIdNovel(nameTruyen);
            idTruyen = await res2;
            IFirebaseConfig _firebaseConfig = new FirebaseConfig
            {
                AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirebaseResponse res3 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/");
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res3.Body);

            if (dict.Count == 0) return;
            //show data
            var dem = 0;
            foreach (var i in dict)
            {
                dem = Convert.ToInt32(i.Key);
            }
            for (int i = 1; i <= dem; i++)
            {
                FirebaseResponse res4 = await client.GetAsync("Truyen/" + idTruyen + "/Binh_luan/" + i.ToString());
                
                Binhluan binhluan = res4.ResultAs<Binhluan>();
                
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

                FirebaseResponse res5 = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Anh_dai_dien");
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

                Label labelName = new Label();
                tableLayoutPanel1.Controls.Add(labelName, 0, 0);
                labelName.AutoSize = true;
                labelName.Font = new Font("League Spartan", 16, FontStyle.Regular);
                labelName.Text = user.User.Info.DisplayName;
                
                Label date = new Label();
                tableLayoutPanel1.Controls.Add(date, 1, 0);
                date.AutoSize = true;
                date.Font = new Font("League Spartan", 16, FontStyle.Regular);
                string dateStr = binhluan.TG_binh_luan.Split(' ').First<string>();
                dateStr = "0" + dateStr;
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


                Label content = new Label();
                panel1.Controls.Add(content);
                content.Font = new Font("League Spartan", 14, FontStyle.Regular);
                content.Dock = DockStyle.Fill;
                content.Text = binhluan.Noi_dung;

                TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
                panel1.Controls.Add(tableLayoutPanel2);
                tableLayoutPanel2.Dock = DockStyle.Bottom;
                tableLayoutPanel2.RowCount = 1;
                tableLayoutPanel2.ColumnCount = 3;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12));

                IconButton btnLike = new IconButton();
                tableLayoutPanel2 .Controls.Add(btnLike, 1, 0);
                btnLike.Dock = DockStyle.Fill;
                btnLike.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btnLike.IconChar = IconChar.Heart;
                btnLike.IconSize = 30;
                btnLike.Text = "Thích";
                btnLike.TextAlign = ContentAlignment.MiddleRight;
                btnLike.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnLike.Click += (s, ev) =>
                {
                    //Hàm click code ở đây
                };

                IconButton btnReport = new IconButton();
                tableLayoutPanel2.Controls.Add(btnLike, 2, 0);
                btnReport.Dock = DockStyle.Fill;
                btnReport.Font = new Font("League Spartan", 12, FontStyle.Regular);
                btnReport.IconChar = IconChar.Flag;
                btnReport.IconSize = 30;
                btnReport.Text = "Tố cáo";
                btnReport.TextAlign = ContentAlignment.MiddleRight;
                btnReport.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnReport.Click += (s, ev) =>
                {
                    //Hàm click code ở đây
                };
            }*/
            
            
        }

        private async void iconButtonRecom_Click(object sender, EventArgs e)
        {
            if (iconButtonRecom.IconColor == System.Drawing.Color.Black)
            {
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

                int Decu = snapshot.GetValue<int>("De_cu");
                Decu++;

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "De_cu", Decu },
                };
                DocumentReference doc = truyen.Document(id);
                await doc.UpdateAsync(updates);
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
            if (currentChap == 1)
            {
                return;
            }
            currentChap--;
            Task<Interact.Chapter> res = Interact.getNovel(nameTruyen, currentChap.ToString());
            Interact.Chapter chapter = new Interact.Chapter();
            chapter = await res;
            contentChap.Text = chapter.Content;
            labelName.Text = chapter.Title;
            Task<DocumentSnapshot> res1 = Interact.getInfoNovel(nameTruyen);
            DocumentSnapshot novel = await res1;
            iconButton2.Text = novel.GetValue<string>("Tac_gia");
        }

        private async void btnPostChap_Click(object sender, EventArgs e)
        {
            if (currentChap == numChap)
            {
                return;
            }
            currentChap++;
            Task<Interact.Chapter> res = Interact.getNovel(nameTruyen, currentChap.ToString());
            Interact.Chapter chapter = new Interact.Chapter();
            chapter = await res;
            contentChap.Text = chapter.Content;
            labelName.Text = chapter.Title;
            Task<DocumentSnapshot> res1 = Interact.getInfoNovel(nameTruyen);
            DocumentSnapshot novel = await res1;
            iconButton2.Text = novel.GetValue<string>("Tac_gia");
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
                foreach (var i in dict)
                {
                    dem = Convert.ToInt32(i.Key);
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

                int So_binhluan = snapshot.GetValue<int>("Danh_gia");

                So_binhluan++;

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "Binh_luan", So_binhluan },
                };
                DocumentReference doc = truyen.Document(id);
                await doc.UpdateAsync(updates);
                textBox1.Text = "";
                Doc_Truyen form = new Doc_Truyen(nameTruyen, user,currentChap);
                this.Close();
                form.Show();
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập gì cả!");
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
