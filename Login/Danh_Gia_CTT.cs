using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FontAwesome.Sharp;
using Google.Cloud.Firestore;
using Novel;
using Readinghistory;

namespace Login
{
    public partial class Danh_Gia_CTT : Form
    {
        string nameTruyen;
        int currentChap;
        int numChap = 1;
        string idTruyen;
        UserCredential user;
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public Danh_Gia_CTT(string Idtruyen, string nametruyen, UserCredential userCredential)
        {
            InitializeComponent();
            this.idTruyen = Idtruyen;
            this.nameTruyen = nametruyen;
            this.user = userCredential;
        }
        public class Danh_Gia
        {
            public string ID_nguoidung { get; set; }
            public string Noi_dung { get; set; }
            public int Sao_danh_gia { get; set; }
            public string TG_danhgia { get; set; }
        }

        private async void Danh_Gia_CTT_Load(object sender, EventArgs e)
        {
            pictureBox2.Width = (int)(0.1175 * panelRatingEvent.Width);
            pictureBox2.Height = pictureBox2.Width;
            richTextBoxRatingComment.Width = (int)(0.845 * panelRatingEvent.Width);
            richTextBoxRatingComment.Height = (int)(0.57 * panelRatingEvent.Height);

            richTextBoxRatingComment.SizeChanged += (s, ev) =>
            {
                richTextBoxRatingComment.Width = (int)(0.845 * panelRatingEvent.Width);
                richTextBoxRatingComment.Height = (int)(0.57 * panelRatingEvent.Height);
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(_firebaseConfig);
            FirebaseResponse res = await client.GetAsync("Truyen/" + idTruyen + "/Danh_gia/");
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res.Body);

            if (dict is null) return;
            //show data
            label2.Text = dict.Count.ToString() + " đánh giá"; 
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
                FirebaseResponse res4 = await client.GetAsync("Truyen/" + idTruyen + "/Danh_gia/" + dembl);

                Danh_Gia danhgia = res4.ResultAs<Danh_Gia>();


                Panel panel = new Panel();
                this.panelShowRating1.Controls.Add(panel);
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

                FirebaseResponse res5 = await client.GetAsync("Nguoi_dung/" + danhgia.ID_nguoidung + "/Anh_dai_dien");
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
                tableLayoutPanel1.Height = 60;
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

                FirebaseResponse res6 = await client.GetAsync("Nguoi_dung/" + danhgia.ID_nguoidung + "/TK_dangnhap");
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
                string dateStr = danhgia.TG_danhgia.ToString();
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


                Label star = new Label();
                panel1.Controls.Add(star);
                star.AutoSize = true;
                star.Font = new Font("League Spartan", 16, FontStyle.Regular);
                star.Dock = DockStyle.Top;
                star.Text = "☆ " + danhgia.Sao_danh_gia.ToString() + "/5";
                star.TextAlign = ContentAlignment.TopLeft;
                star.ForeColor = Color.DarkOrange;
                star.BringToFront();
                
                

                Label content = new Label();
                panel1.Controls.Add(content);
                content.AutoSize = true;
                content.Font = new Font("League Spartan", 14, FontStyle.Regular);
                content.Dock = DockStyle.Top;
                content.Text = danhgia.Noi_dung;
                content.BringToFront();

                
            }
        }
        private void ibtnStar1_MouseHover(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            if (ibtnStar2.IconColor == Color.DarkOrange)
            {
                ibtnStar2.IconColor = Color.Linen;
            }
            if (ibtnStar3.IconColor == Color.DarkOrange)
            {
                ibtnStar3.IconColor = Color.Linen;
            }
            if (ibtnStar4.IconColor == Color.DarkOrange)
            {
                ibtnStar4.IconColor = Color.Linen;
            }
            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }
        }

        private void ibtnStar2_MouseHover(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            ibtnStar2.IconColor = Color.DarkOrange;
            if (ibtnStar3.IconColor == Color.DarkOrange)
            {
                ibtnStar3.IconColor = Color.Linen;
            }
            if (ibtnStar4.IconColor == Color.DarkOrange)
            {
                ibtnStar4.IconColor = Color.Linen;
            }
            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }
        }

        private void ibtnStar3_MouseHover(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            ibtnStar2.IconColor = Color.DarkOrange;
            ibtnStar3.IconColor = Color.DarkOrange;
            if (ibtnStar4.IconColor == Color.DarkOrange)
            {
                ibtnStar4.IconColor = Color.Linen;
            }
            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }
        }

        private void ibtnStar4_MouseHover(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            ibtnStar2.IconColor = Color.DarkOrange;
            ibtnStar3.IconColor = Color.DarkOrange;
            ibtnStar4.IconColor = Color.DarkOrange;
            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }
        }

        private void ibtnStar5_MouseHover(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            ibtnStar2.IconColor = Color.DarkOrange;
            ibtnStar3.IconColor = Color.DarkOrange;
            ibtnStar4.IconColor = Color.DarkOrange;
            ibtnStar5.IconColor = Color.DarkOrange;
        }

        private void ibtnStar1_MouseLeave(object sender, EventArgs e)
        {
            if (ibtnStar1.IconColor == Color.DarkOrange)
            {
                ibtnStar1.IconColor = Color.Linen;
            }
        }

        private void ibtnStar2_MouseLeave(object sender, EventArgs e)
        {
            if (ibtnStar1.IconColor == Color.DarkOrange)
            {
                ibtnStar1.IconColor = Color.Linen;
            }
            if (ibtnStar2.IconColor == Color.DarkOrange)
            {
                ibtnStar2.IconColor = Color.Linen;
            }

        }

        private void ibtnStar3_MouseLeave(object sender, EventArgs e)
        {
            if (ibtnStar1.IconColor == Color.DarkOrange)
            {
                ibtnStar1.IconColor = Color.Linen;
            }
            if (ibtnStar2.IconColor == Color.DarkOrange)
            {
                ibtnStar2.IconColor = Color.Linen;
            }
            if (ibtnStar3.IconColor == Color.DarkOrange)
            {
                ibtnStar3.IconColor = Color.Linen;
            }

        }

        private void ibtnStar4_MouseLeave(object sender, EventArgs e)
        {
            if (ibtnStar1.IconColor == Color.DarkOrange)
            {
                ibtnStar1.IconColor = Color.Linen;
            }
            if (ibtnStar2.IconColor == Color.DarkOrange)
            {
                ibtnStar2.IconColor = Color.Linen;
            }
            if (ibtnStar3.IconColor == Color.DarkOrange)
            {
                ibtnStar3.IconColor = Color.Linen;
            }
            if (ibtnStar4.IconColor == Color.DarkOrange)
            {
                ibtnStar4.IconColor = Color.Linen;
            }

        }

        private void ibtnStar5_MouseLeave(object sender, EventArgs e)
        {
            if (ibtnStar1.IconColor == Color.DarkOrange)
            {
                ibtnStar1.IconColor = Color.Linen;
            }
            if (ibtnStar2.IconColor == Color.DarkOrange)
            {
                ibtnStar2.IconColor = Color.Linen;
            }
            if (ibtnStar3.IconColor == Color.DarkOrange)
            {
                ibtnStar3.IconColor = Color.Linen;
            }
            if (ibtnStar4.IconColor == Color.DarkOrange)
            {
                ibtnStar4.IconColor = Color.Linen;
            }
            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }
        }

        private void ibtnStar1_Click(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;

            if (ibtnStar2.IconColor == Color.DarkOrange)
            {
                ibtnStar2.IconColor = Color.Linen;
            }
            if (ibtnStar3.IconColor == Color.DarkOrange)
            {
                ibtnStar3.IconColor = Color.Linen;
            }
            if (ibtnStar4.IconColor == Color.DarkOrange)
            {
                ibtnStar4.IconColor = Color.Linen;
            }
            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }

            ibtnStar1.MouseLeave -= ibtnStar1_MouseLeave;

            ibtnStar1.MouseHover -= ibtnStar1_MouseHover;
            ibtnStar2.MouseHover -= ibtnStar2_MouseHover;
            ibtnStar3.MouseHover -= ibtnStar3_MouseHover;
            ibtnStar4.MouseHover -= ibtnStar4_MouseHover;
            ibtnStar5.MouseHover -= ibtnStar5_MouseHover;
        }

        private void ibtnStar2_Click(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            ibtnStar2.IconColor = Color.DarkOrange;

            if (ibtnStar3.IconColor == Color.DarkOrange)
            {
                ibtnStar3.IconColor = Color.Linen;
            }
            if (ibtnStar4.IconColor == Color.DarkOrange)
            {
                ibtnStar4.IconColor = Color.Linen;
            }
            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }

            ibtnStar1.MouseLeave -= ibtnStar1_MouseLeave;
            ibtnStar2.MouseLeave -= ibtnStar2_MouseLeave;

            ibtnStar1.MouseHover -= ibtnStar1_MouseHover;
            ibtnStar2.MouseHover -= ibtnStar2_MouseHover;
            ibtnStar3.MouseHover -= ibtnStar3_MouseHover;
            ibtnStar4.MouseHover -= ibtnStar4_MouseHover;
            ibtnStar5.MouseHover -= ibtnStar5_MouseHover;
        }

        private void ibtnStar3_Click(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            ibtnStar2.IconColor = Color.DarkOrange;
            ibtnStar3.IconColor = Color.DarkOrange;

            if (ibtnStar4.IconColor == Color.DarkOrange)
            {
                ibtnStar4.IconColor = Color.Linen;
            }
            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }

            ibtnStar1.MouseLeave -= ibtnStar1_MouseLeave;
            ibtnStar2.MouseLeave -= ibtnStar2_MouseLeave;
            ibtnStar3.MouseLeave -= ibtnStar3_MouseLeave;

            ibtnStar1.MouseHover -= ibtnStar1_MouseHover;
            ibtnStar2.MouseHover -= ibtnStar2_MouseHover;
            ibtnStar3.MouseHover -= ibtnStar3_MouseHover;
            ibtnStar4.MouseHover -= ibtnStar4_MouseHover;
            ibtnStar5.MouseHover -= ibtnStar5_MouseHover;
        }

        private void ibtnStar4_Click(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            ibtnStar2.IconColor = Color.DarkOrange;
            ibtnStar3.IconColor = Color.DarkOrange;
            ibtnStar4.IconColor = Color.DarkOrange;

            if (ibtnStar5.IconColor == Color.DarkOrange)
            {
                ibtnStar5.IconColor = Color.Linen;
            }

            ibtnStar1.MouseLeave -= ibtnStar1_MouseLeave;
            ibtnStar2.MouseLeave -= ibtnStar2_MouseLeave;
            ibtnStar3.MouseLeave -= ibtnStar3_MouseLeave;
            ibtnStar4.MouseLeave -= ibtnStar4_MouseLeave;

            ibtnStar1.MouseHover -= ibtnStar1_MouseHover;
            ibtnStar2.MouseHover -= ibtnStar2_MouseHover;
            ibtnStar3.MouseHover -= ibtnStar3_MouseHover;
            ibtnStar4.MouseHover -= ibtnStar4_MouseHover;
            ibtnStar5.MouseHover -= ibtnStar5_MouseHover;
        }

        private void ibtnStar5_Click(object sender, EventArgs e)
        {
            ibtnStar1.IconColor = Color.DarkOrange;
            ibtnStar2.IconColor = Color.DarkOrange;
            ibtnStar3.IconColor = Color.DarkOrange;
            ibtnStar4.IconColor = Color.DarkOrange;
            ibtnStar5.IconColor = Color.DarkOrange;
            ibtnStar1.MouseLeave -= ibtnStar1_MouseLeave;
            ibtnStar2.MouseLeave -= ibtnStar2_MouseLeave;
            ibtnStar3.MouseLeave -= ibtnStar3_MouseLeave;
            ibtnStar4.MouseLeave -= ibtnStar4_MouseLeave;
            ibtnStar5.MouseLeave -= ibtnStar5_MouseLeave;

            ibtnStar1.MouseHover -= ibtnStar1_MouseHover;
            ibtnStar2.MouseHover -= ibtnStar2_MouseHover;
            ibtnStar3.MouseHover -= ibtnStar3_MouseHover;
            ibtnStar4.MouseHover -= ibtnStar4_MouseHover;
            ibtnStar5.MouseHover -= ibtnStar5_MouseHover;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            if (ibtnStar1.IconColor == Color.DarkOrange || ibtnStar2.IconColor == Color.DarkOrange
                || ibtnStar3.IconColor == Color.DarkOrange || ibtnStar4.IconColor == Color.DarkOrange
                || ibtnStar5.IconColor == Color.DarkOrange)
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

                FirebaseResponse res = await client.GetAsync($"Truyen/{idTruyen}/Danh_gia/");
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

                int Star = 0;

                if (ibtnStar1.IconColor == Color.DarkOrange)
                {
                    Star = 1;
                }
                if (ibtnStar2.IconColor == Color.DarkOrange)
                {
                    Star = 2;
                }
                if (ibtnStar3.IconColor == Color.DarkOrange)
                {
                    Star = 3;
                }
                if (ibtnStar4.IconColor == Color.DarkOrange)
                {
                    Star = 4;
                }
                if (ibtnStar5.IconColor == Color.DarkOrange)
                {
                    Star = 5;
                }

                string demdg = "0";
                if (dem < 10)
                {
                    demdg += "0" + dem.ToString();
                }
                else if (dem < 99)
                {
                    demdg += dem.ToString();
                }
                else
                {
                    demdg = dem.ToString();
                }

                string tgdg = System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

                Danh_Gia danh_Gia = new Danh_Gia()
                {
                    ID_nguoidung = userId,
                    Noi_dung = richTextBoxRatingComment.Text,
                    Sao_danh_gia = Star,
                    TG_danhgia = tgdg
                };

                await client.SetAsync("Truyen/" + idTruyen + "/Danh_gia/" + demdg, danh_Gia);

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

                int So_danhgia = snapshot.GetValue<int>("Danh_gia");
                int Tong_danhgia = snapshot.GetValue<int>("Diem_danhgia");
                double danhgia_tb = snapshot.GetValue<double>("Danh_gia_Tb");

                So_danhgia++;
                Tong_danhgia += Star;
                danhgia_tb = (double)Tong_danhgia / So_danhgia;
                danhgia_tb = Math.Round(danhgia_tb, 2);

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "Danh_gia", So_danhgia },
                    { "Diem_danhgia", Tong_danhgia },
                    { "Danh_gia_Tb", danhgia_tb },
                };
                DocumentReference doc = truyen.Document(id);
                await doc.UpdateAsync(updates);
                ibtnStar1.IconColor = Color.Linen;
                ibtnStar2.IconColor = Color.Linen;
                ibtnStar3.IconColor = Color.Linen;
                ibtnStar4.IconColor = Color.Linen;
                ibtnStar5.IconColor = Color.Linen;
                richTextBoxRatingComment.Text = "";
            }
            else
            {
                MessageBox.Show("Lỗi! Vui lòng nhập đầy đủ thông tin đánh giá.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
