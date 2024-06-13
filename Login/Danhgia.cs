using Firebase.Auth;
using Firebase.Auth.Providers;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thongbao;

namespace Login
{
    public partial class Danhgia : Form
    {
        string idtruyen, nameNovel;
        public Danhgia(string idtruyen, string nameNovel)
        {
            InitializeComponent();
            this.idtruyen = idtruyen;
            this.nameNovel = nameNovel;
        }
        public class Danh_Gia
        {
            public string ID_nguoidung {  get; set; }
            public int Luot_thich {  get; set; }
            public string Noi_dung { get; set; }
            public int Sao_danh_gia { get; set; }
            public string TG_danhgia { get; set; }
            public bool To_cao { get; set; }
        }
        bool rtb = false;
        
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
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

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(155, 227, 243);
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                richTextBox1.ForeColor = Color.Gray;
                richTextBox1.Text = "Để lại bình luận đánh giá";
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            if (rtb == false || richTextBox1.ForeColor == Color.Gray)
            {
                richTextBox1.Text = string.Empty;
                richTextBox1.ForeColor = Color.Black;
                rtb = true;
            }
            panel1.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void Danhgia_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(155, 227, 243);
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                richTextBox1.ForeColor = Color.Gray;
                richTextBox1.Text = "Để lại bình luận đánh giá";
            }
            richTextBox1.Enter += richTextBox1_Enter;
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (rtb == false || richTextBox1.ForeColor == Color.Gray)
            {
                richTextBox1.Text = string.Empty;
                richTextBox1.ForeColor = Color.Black;
                rtb = true;
            }
            panel1.BackColor = Color.FromArgb(64, 64, 64);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (ibtnStar1.ForeColor == Color.DarkOrange || ibtnStar2.ForeColor == Color.DarkOrange
                || ibtnStar3.ForeColor == Color.DarkOrange || ibtnStar4.ForeColor == Color.DarkOrange
                || ibtnStar5.ForeColor == Color.DarkOrange || richTextBox1.ForeColor == Color.Black) 
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

                FirebaseResponse res = await client.GetAsync($"Truyen/{idtruyen}/Danh_gia");
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res.Body);

                //show data
                var dem = 0;
                foreach (var i in dict)
                {
                    dem = Convert.ToInt32(i.Key);
                }
                dem++;

                int Star = 0;

                if (ibtnStar1.ForeColor == Color.DarkOrange)
                {
                    Star = 1;
                }
                if (ibtnStar2.ForeColor == Color.DarkOrange)
                {
                    Star = 2;
                }
                if (ibtnStar3.ForeColor == Color.DarkOrange)
                {
                    Star = 3;
                }
                if (ibtnStar4.ForeColor == Color.DarkOrange)
                {
                    Star = 4;
                }
                if (ibtnStar5.ForeColor == Color.DarkOrange)
                {
                    Star = 5;
                }

                Danh_Gia danh_Gia = new Danh_Gia()
                {
                    ID_nguoidung = userId,
                    Luot_thich = 0,
                    Noi_dung = richTextBox1.Text,
                    Sao_danh_gia = Star,
                    TG_danhgia = DateTime.UtcNow.ToString(),
                    To_cao = false
                };

                await client.SetAsync("Truyen/" + idtruyen + "Danh_gia/" + dem, danh_Gia);

                FirestoreDb db = FirestoreDb.Create("healtruyen");
                CollectionReference truyen = db.Collection("Truyen");
                nameNovel = nameNovel.ToUpper();
                Query q = truyen.WhereEqualTo("Ten", nameNovel);
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
                double danhgia_tb = snapshot.GetValue<double>("Danh_gia_tb");

                So_danhgia++;
                Tong_danhgia += Star;
                danhgia_tb = Tong_danhgia / So_danhgia;
                danhgia_tb = Math.Round(danhgia_tb, 1);

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "Danh_gia", So_danhgia },
                    { "Diem_danhgia", Tong_danhgia },
                    { "Danh_gia_tb", danhgia_tb },
                };
                DocumentReference doc = truyen.Document(id);
                await doc.UpdateAsync(updates);

            }
            else
            {
                MessageBox.Show("Lỗi! Vui lòng nhập đầy đủ thông tin đánh giá.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }

        private void ibtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ibtnClose_MouseHover(object sender, EventArgs e)
        {
            ibtnClose.IconColor = Color.IndianRed;
        }

        private void ibtnClose_MouseLeave(object sender, EventArgs e)
        {
            ibtnClose.IconColor = Color.Black;
        }

        private void ibtnClose_MouseClick(object sender, MouseEventArgs e)
        {
            ibtnClose.IconColor = Color.IndianRed;
        }

        private void Danhgia_Load(object sender, EventArgs e)
        {
            //Bo góc Đăng ký
            System.Drawing.Drawing2D.GraphicsPath path5 = new System.Drawing.Drawing2D.GraphicsPath();
            int cornerRadius = 40; // Điều chỉnh giá trị này để thay đổi bán kính bo góc
            //Bo góc nút Đăng ký
            path5.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path5.AddArc(button1.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path5.AddArc(button1.Width - cornerRadius, button1.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path5.AddArc(0, button1.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path5.CloseAllFigures();

            button1.Region = new Region(path5);
        }
    }
}
