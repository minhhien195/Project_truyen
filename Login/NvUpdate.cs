using Firebase.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FontAwesome.Sharp;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Login
{

    public partial class NVupdate : Form
    {

        UserCredential user;
        FirebaseAuthClient client;
        IFirebaseClient ifclient;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        public NVupdate(UserCredential userCredential, FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            ifclient = new FireSharp.FirebaseClient(config);
            this.user = userCredential;
            this.client = firebaseAuthClient;
        }

        public NVupdate()
        {
            InitializeComponent();
        }

        List<string> list = new List<string>();

        private async void NVupdate_Load(object sender, EventArgs e)
        {
            string ID = user.User.Info.Uid;
            string path = "Nguoi_dung/" + ID + "/Truyen_dang";
            FirebaseResponse res = await ifclient.GetAsync(path);
            if (res != null)
            {
                Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(res.Body);

                // Đếm số lượng phần tử trong Dictionary
                int count = data.Count;

                // Làm gì đó với số lượng dữ 
                if (count > 0)
                {
                    label1.Text = "Số truyện đã đăng: " + count.ToString();
                    foreach(var item in data)
                    {
                        list.Add(item.Key); 
                    }
                }
                else
                {
                    label1.Text = "Chưa có đăng truyện nào!";
                    return;
                }
            }
            load_truyendang();
        }

        private async void load_truyendang()
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            
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

            foreach (var item in documents) 
            {
                Dictionary<string, object> novel = item.ToDictionary();

                
                
                Panel panelList = new Panel();
                panelList.Dock = DockStyle.Top;
                panelList.AutoSize = true;
                panelList.BringToFront();
                panelAll.Controls.Add(panelList);


                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    RowCount = 1,
                    ColumnCount = 2,
                };
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
                tableLayoutPanel.BringToFront();
                panelList.Controls.Add(tableLayoutPanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                byte[] imageBytes = Convert.FromBase64String(novel["Anh"].ToString());

                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(memoryStream);
                    pictureBox.Image = bitmap;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                tableLayoutPanel.Controls.Add(pictureBox, 0, 0);

                Panel panelRight = new Panel();
                panelRight.Dock = DockStyle.Fill;
                panelRight.AutoSize = true;
                tableLayoutPanel.Controls.Add(panelRight, 1, 0);

                Label tenTruyen = new Label();
                panelRight.Controls.Add(tenTruyen);
                tenTruyen.Dock = DockStyle.Top;
                tenTruyen.AutoSize = true;
                tenTruyen.Text = novel["Ten"].ToString();
                tenTruyen.Font = new Font("League Spartan", 16, FontStyle.Bold);
                tenTruyen.BringToFront();

                Label description = new Label();
                panelRight.Controls.Add(description);
                description.Dock = DockStyle.Fill;
                description.AutoSize = false;
                description.Text = novel["Tom_tat"].ToString();
                description.Height = 126;
                description.Font = new Font("League Spartan", 14, FontStyle.Regular);
                description.BringToFront();

                TableLayoutPanel info = new TableLayoutPanel();
                panelRight.Controls.Add(info);
                info.Dock = DockStyle.Bottom;
                info.AutoSize = false;
                info.RowCount = 1;
                info.ColumnCount = 4;
                info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26));
                info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24));
                info.BringToFront();

                IconButton view = new IconButton() 
                {
                    Dock = DockStyle.Fill,
                    FlatStyle = FlatStyle.Flat,
                    
                    Font = new Font("League Spartan", 14, FontStyle.Regular),
                    IconChar = IconChar.Eye,
                    IconFont = IconFont.Auto,
                    IconSize = 48,
                    Text = novel["Luot_xem"].ToString(),
                    TextAlign = ContentAlignment.MiddleRight,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                };
                view.FlatAppearance.BorderSize = 0;
                info.Controls.Add(view, 0, 0);

                IconButton tt = new IconButton()
                {
                    Dock = DockStyle.Fill,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("League Spartan", 13, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleRight,
                    TextImageRelation= TextImageRelation.ImageBeforeText,
                };

                string ttt = novel["Trang_thai"].ToString();
                if (ttt == "0")
                {
                    tt.Text = "Tạm dừng";
                }
                else if (ttt == "1")
                {
                    tt.Text = "Đang tiến hành";
                }
                else if (ttt == "2")
                {
                    tt.Text = "Hoàn thành";
                }
                tt.FlatAppearance.BorderSize = 0;
                info.Controls.Add(tt, 2, 0);

                IconButton recom = new IconButton()
                {
                    Dock = DockStyle.Fill,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("League Spartan", 14, FontStyle.Regular),
                    IconChar = IconChar.Star,
                    IconFont = IconFont.Auto,
                    IconSize = 48,
                    Text = novel["Danh_gia_Tb"].ToString(),
                    TextAlign = ContentAlignment.MiddleRight,
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                };
                recom.FlatAppearance.BorderSize = 0;
                info.Controls.Add(recom, 1, 0);

                IconButton type = new IconButton();
                info.Controls.Add(type, 3, 0);
                type.Dock = DockStyle.Fill;
                type.FlatStyle = FlatStyle.Flat;
                type.FlatAppearance.BorderSize = 0;
                type.Font = new Font("League Spartan", 14, FontStyle.Regular);
                object typeList = novel["The_loai"];
                if (typeList is List<object> typeList1)
                {
                    List<string> typeStringList = typeList1.Select(j => j.ToString()).ToList();
                    type.Text = typeStringList[0];
                }
                type.TextAlign = ContentAlignment.MiddleRight;
                type.TextImageRelation = TextImageRelation.ImageBeforeText;
            }
        }
    }
}
