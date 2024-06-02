using Firebase.Auth;
using Firebase.Auth.Providers;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Newtonsoft.Json;
using Novel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Inserted : Form
    {
        public Inserted()
        {
            InitializeComponent();
        }
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        private void Inserted_Load(object sender, EventArgs e)
        {
            dsTruyen();
        }
        string userId = "";
        Dictionary<string, string> trangthai_id = new Dictionary<string, string>();
        private async Task<string> dangTruyen()
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
            userId = client.User.Uid;
            IFirebaseClient client1 = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "Nguoi_dung/" + userId;
            FirebaseResponse res = await client1.GetAsync(path);
            Dictionary<string, Dictionary<string, object>> user = new Dictionary<string, Dictionary<string, object>>();
            if (res.Body != "null")
            {
                user = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(res.Body);
            }
            foreach (var item in user)
            {
                if (item.Key == "Dangtruyen")
                {
                    return item.Value.ToString();
                }
            }
            return null;

        }
        private async void dsTruyen()
        {
            string truyen = "";
            Task<string> dstruyen = dangTruyen();
            var result = await dstruyen;
            if (result != null)
            {
                truyen = result;
            }
            string[] ds = truyen.Split(',');
            foreach (string s in ds)
            {
                FirestoreDb db = FirestoreDb.Create("healtruyen");
                string docPath = "Truyen/" + s;
                DocumentReference docRef = db.Document(docPath);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    Panel panel = new Panel();
                    panel.Dock = DockStyle.Top;
                    panel.AutoSize = true;
                    panel.Size = new Size(1223, 59);
                    panel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    panel.Visible = true;
                    panel.Margin = new Padding(0, 0, 0, 50);
                    panel.Tag = snapshot.GetValue<string>("Ten");
                    panelTruyendang.Controls.Add(panel);

                    Label tentruyen = new Label();
                    tentruyen.AutoSize = true;
                    tentruyen.Location = new Point(12, 12);
                    tentruyen.Text = snapshot.GetValue<string>("Ten");
                    tentruyen.Visible = true;
                    tentruyen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    tentruyen.Font = new Font("League Spartan", 12F, FontStyle.Regular);

                    Label chuong = new Label();
                    chuong.AutoSize = true;
                    chuong.Location = new Point(612, 12);
                    chuong.Text = snapshot.GetValue<int>("So_chuong") + " chuong";
                    chuong.Visible = true;
                    chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    chuong.Font = new Font("League Spartan", 12F, FontStyle.Regular);

                    ComboBox trangthai = new ComboBox();
                    trangthai.AutoSize = true;
                    trangthai.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                    trangthai.BackColor = Color.FromArgb(155, 227, 243);
                    trangthai.FlatStyle = FlatStyle.Flat;
                    trangthai.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    trangthai.Location = new Point(959, 12);
                    trangthai.SelectedIndex = 0;
                    trangthai.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
                    int tt = snapshot.GetValue<int>("Trang_thai");
                    if (tt == 0)
                    {
                        trangthai.Text = "Tạm dừng";
                    }
                    else if (tt == 1)
                    {
                        trangthai.Text = "Đang tiến hành";
                    }
                    else
                    {
                        trangthai.Text = "Hoàn thành";
                    }
                    
                    trangthai.Items.Add("Tạm dừng");
                    trangthai.Items.Add("Đang Tiến Hành");
                    trangthai.Items.Add("Hoàn thành");

                    panel.Controls.Add(tentruyen);
                    panel.Controls.Add(chuong);
                    panel.Controls.Add(trangthai);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (var item in trangthai_id)
            {
                if (item.Key == "Tạm dừng")
                {
                    Interact.editNovel(item.Value, "Trang_thai", "0");
                }
                else if (item.Key == "Đang tiến hành")
                {
                    Interact.editNovel(item.Value, "Trang_thai", "1");
                }
                else if (item.Key == "Hoàn thành")
                {
                    Interact.editNovel(item.Value, "Trang_thai", "2");
                }
            }
            MessageBox.Show("Đã cập nhật trạng thái truyện thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnInsertChap.BackColor = Color.FromArgb(220, 247, 253);
            btnInsertChap.ForeColor = Color.Black;
            btnUpdate.BackColor = Color.FromArgb(191, 44, 36);
            btnUpdate.ForeColor = Color.White;
            ComboBox comboBox = (ComboBox)sender;
            System.Windows.Forms.Panel panel = comboBox.Parent as System.Windows.Forms.Panel;
            string ten = panel.Tag as string;
            if (comboBox.SelectedIndex == 0)
            {

                trangthai_id.Add("Tạm dừng", ten);
            }
            else if (comboBox.SelectedIndex == 1)
            {
                trangthai_id.Add("Đang tiến hành", ten);
            }
            else
            {
                trangthai_id.Add("Hoàn thành", ten);
            }
        }
    }
}
