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
using System.Windows.Media.Media3D;

namespace Login
{
    public partial class Inserted : Form
    {
        UserCredential usercredials;
        private Trang_chu tc;
        public Inserted(UserCredential usercredials, Trang_chu tc)
        {
            InitializeComponent();
            this.usercredials = usercredials;
            this.tc = tc;
        }
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        private void Inserted_Load(object sender, EventArgs e)
        {
            dangTruyen();
        }

        List<string> list = new List<string>();

        Dictionary<string, string> trangthai_id = new Dictionary<string, string>();
        private async void dangTruyen()
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
            string userId = client.User.Uid;
            IFirebaseClient client1 = new FireSharp.FirebaseClient(_firebaseConfig);
            var path = "Nguoi_dung/" + userId + "/Truyen_dang";
            FirebaseResponse res = await client1.GetAsync(path);

            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(res.Body);
            if (data is null)
            {
                MessageBox.Show("Không có truyện nào đã đăng!");
                return;
            }
            else
            {
                foreach (var id in data.Keys)
                {
                    list.Add(id.ToString());
                }
            }
            dsTruyen();
        }
        private async void dsTruyen()
        {   
            FirestoreDb db = FirestoreDb.Create("healtruyen");
            CollectionReference collection = db.Collection("Truyen");

            List<DocumentSnapshot> documents = new List<DocumentSnapshot>();
            foreach (string id in list)
            {
                DocumentSnapshot document = await collection.Document(id).GetSnapshotAsync();
                if (document.Exists)
                {
                    documents.Add(document);
                }
            }

            foreach (var id in documents)
            {
                Dictionary<string, object> novel = id.ToDictionary();

                Panel panel = new Panel();
                panel.Dock = DockStyle.Top;
                panel.AutoSize = true;
                panel.Size = new Size(1223, 59);
                panel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panel.Visible = true;
                panel.Margin = new Padding(0, 0, 0, 50);
                panel.Tag = novel["Ten"];
                panelTruyendang.Controls.Add(panel);

                Label tentruyen = new Label();
                tentruyen.AutoSize = true;
                tentruyen.Location = new Point(12, 12);
                tentruyen.Text = novel["Ten"].ToString();
                tentruyen.Visible = true;
                tentruyen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                tentruyen.Font = new Font("League Spartan", 12F, FontStyle.Regular);

                Label chuong = new Label();
                chuong.AutoSize = true;
                chuong.Location = new Point(408, 12);
                chuong.Text = novel["So_chuong"].ToString() + " chuong";
                chuong.Visible = true;
                chuong.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                chuong.Font = new Font("League Spartan", 12F, FontStyle.Regular);

                ComboBox trangthai = new ComboBox();
                trangthai.AutoSize = true;
                trangthai.Font = new Font("League Spartan", 12F, FontStyle.Regular);
                trangthai.BackColor = Color.FromArgb(155, 227, 243);
                trangthai.FlatStyle = FlatStyle.Flat;
                trangthai.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                trangthai.Location = new Point(639, 12);
                trangthai.Items.Add("Tạm dừng");
                trangthai.Items.Add("Đang tiến hành");
                trangthai.Items.Add("Hoàn thành");
                trangthai.SelectedIndex = Convert.ToInt32(novel["Trang_thai"]);
/*                trangthai.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
*/                trangthai.SelectedValueChanged += comboBox1_SelectedIndexChanged;
                int tt = Convert.ToInt32(novel["Trang_thai"]);
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

                trangthai_id.Add(novel["Ten"].ToString(), trangthai.Text);

                panel.Controls.Add(tentruyen);
                panel.Controls.Add(chuong);
                panel.Controls.Add(trangthai);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {   
            foreach (var item in trangthai_id)
            {
                int tt = 2;
                if (item.Value == "Tạm dừng")
                {
                    tt = 0;
                }
                else if (item.Value == "Đang tiến hành")
                {
                    tt = 1;
                }    

                Interact.editNovel(item.Key, "Trang_thai",tt.ToString());
            }
            tb2 tb = new tb2();
            tb.ShowDialog();
            btnUpdate.BackColor = Color.FromArgb(220, 247, 253);
            btnUpdate.ForeColor = Color.Black;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Panel panel1 = comboBox.Parent as System.Windows.Forms.Panel;
            string ten = "";
            if (panel1 != null && panel1.Tag != null)
            {
                ten = panel1.Tag as string;
                // Rest of the code
            }

            if (comboBox.Text != trangthai_id[ten])
            {
                trangthai_id[ten] = comboBox.Text;
            }

            btnUpdate.BackColor = Color.FromArgb(191, 44, 36);
            btnUpdate.ForeColor = Color.White;
        }

        private void btnInsertChap_Click(object sender, EventArgs e)
        {
            tc.change_color();
            tc.openChildForm(new InsertChapter(usercredials, tc));
        }
    }
}
