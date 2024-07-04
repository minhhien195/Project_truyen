using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FontAwesome.Sharp;
using Google.Cloud.Firestore;
using Firebase.Auth;
using Novel;
using FireSharp.Response;

namespace Login
{
    public partial class Duyet_Truyen : Form
    {
        UserCredential user;
        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        public Duyet_Truyen(UserCredential user)
        {
            InitializeComponent();
            this.user = user;
            client = new FireSharp.FirebaseClient(config);
        }

        async private void Duyet_Truyen_Load(object sender, EventArgs e)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);

            /*Panel childFormPanel = new Panel();
            this.Controls.Add(childFormPanel);
            childFormPanel.Dock = DockStyle.Fill;
            childFormPanel.AutoSize = true;
            childFormPanel.AutoScroll = true;*/

            Panel header = new Panel();
            childFormPanel.Controls.Add(header);
            header.Dock = DockStyle.Top;
            header.AutoSize = true;

            Label hdLabel = new Label();
            header.Controls.Add(hdLabel);
            hdLabel.Dock = DockStyle.Top;
            hdLabel.AutoSize = true;
            hdLabel.Font = new Font("League Spartan", 20, FontStyle.Bold);
            hdLabel.Text = "Duyệt Truyện";

            CollectionReference collection = db.Collection("Dang_truyen");
            QuerySnapshot qs = await collection.GetSnapshotAsync();

            CollectionReference collection1 = db.Collection("Truyen");
            QuerySnapshot qs1 = await collection1.GetSnapshotAsync();

            if (qs.Documents.Count <= 0)
            {
                Panel panelList = new Panel();
                panelList.Dock = DockStyle.Top;
                panelList.AutoSize = true;
                panelList.BringToFront();
                childFormPanel.Controls.Add(panelList);

                Label noti = new Label();
                panelList.Controls.Add(hdLabel);
                noti.Dock = DockStyle.Top;
                noti.AutoSize = true;
                noti.Font = new Font("League Spartan", 20, FontStyle.Bold);
                noti.Text = "Không có truyện cần duyệt";
            } 
            else
            {
                foreach (var item in  qs.Documents)
                {
                    Dictionary<string, object> novel = item.ToDictionary();
                    Panel panelList = new Panel();
                    panelList.Dock = DockStyle.Top;
                    panelList.AutoSize = true;
                    panelList.BringToFront();
                    childFormPanel.Controls.Add(panelList);
                    panelList.BringToFront();


                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                    panelList.Controls.Add(tableLayoutPanel);
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.AutoSize = true;
                    tableLayoutPanel.RowCount = 1;
                    tableLayoutPanel.ColumnCount = 2;
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
                    tableLayoutPanel.BringToFront();

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
                    description.Dock = DockStyle.Top;
                    description.AutoSize = false;
                    description.Enabled = false;
                    description.AllowDrop = true;

                    description.Text = novel["Tom_tat"].ToString();
                    description.Height = 126;
                    description.Font = new Font("League Spartan", 14, FontStyle.Regular);
                    description.BringToFront();

                    TableLayoutPanel info = new TableLayoutPanel();
                    panelRight.Controls.Add(info);
                    info.Dock = DockStyle.Top;
                    info.AutoSize = false;
                    info.RowCount = 1;
                    info.ColumnCount = 3;
                    info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                    info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                    info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                    info.BringToFront();

                    IconButton author = new IconButton();
                    info.Controls.Add(author, 0, 0);
                    author.Dock = DockStyle.Fill;
                    author.FlatStyle = FlatStyle.Flat;
                    author.FlatAppearance.BorderSize = 0;
                    author.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    author.IconChar = IconChar.UserEdit;
                    author.IconFont = IconFont.Auto;
                    author.IconSize = 48;
                    author.Text = novel["Tac_gia"].ToString();
                    author.TextAlign = ContentAlignment.MiddleRight;
                    author.TextImageRelation = TextImageRelation.ImageBeforeText;

                    IconButton recom = new IconButton();
                    info.Controls.Add(recom, 1, 0);
                    recom.Dock = DockStyle.Fill;
                    recom.FlatStyle = FlatStyle.Flat;
                    recom.FlatAppearance.BorderSize = 0;
                    recom.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    recom.IconChar = IconChar.ArrowCircleUp;
                    recom.IconFont = IconFont.Auto;
                    recom.IconSize = 48;
                    recom.Text = novel["De_cu"].ToString();
                    recom.TextAlign = ContentAlignment.MiddleRight;
                    recom.TextImageRelation = TextImageRelation.ImageBeforeText;

                    IconButton type = new IconButton();
                    info.Controls.Add(type, 2, 0);
                    type.Dock = DockStyle.Fill;
                    type.FlatStyle = FlatStyle.Flat;
                    type.FlatAppearance.BorderSize = 0;
                    type.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    object typeList = novel["The_loai"];
                    int sizeArray = 0;
                    if (typeList is List<object> typeList1)
                    {
                        string[] typeStringList = typeList1.Select(j => j.ToString()).ToArray();
                        sizeArray = typeStringList.Length;
                        type.Text = typeStringList[0];
                    }
                    string[] typeStringArray= new string[sizeArray];
                    if (typeList is List<object> typeList2)
                    {
                        typeStringArray = typeList2.Select(j => j.ToString()).ToArray();
                    }
                    type.TextAlign = ContentAlignment.MiddleRight;
                    type.TextImageRelation = TextImageRelation.ImageBeforeText;

                    TableLayoutPanel btn = new TableLayoutPanel();
                    panelRight.Controls.Add(btn);
                    btn.Dock = DockStyle.Top;
                    btn.AutoSize = false;
                    btn.RowCount = 1;
                    btn.ColumnCount = 3;
                    btn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
                    btn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                    btn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

                    Button cancel = new Button();
                    btn.Controls.Add(cancel, 1, 0);
                    cancel.Dock = DockStyle.Fill;
                    cancel.FlatStyle = FlatStyle.Flat;
                    cancel.FlatAppearance.BorderSize = 0;
                    cancel.BackColor = Color.Silver;

                    cancel.Text = "Từ chối";
                    cancel.Size = new Size(100, 60);
                    cancel.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    cancel.Margin = new Padding(10);
                    cancel.Click += (s, ev) =>
                    {
                        Interact.deleteNovel(novel["Ten"].ToString(), "Dang_truyen");
                        MessageBox.Show("Từ chối duyệt truyện thành công");
                        panelList.Visible = false;
                    };

                    Button confirm = new Button();
                    btn.Controls.Add(confirm, 2, 0);
                    confirm.Dock = DockStyle.Fill;
                    confirm.FlatStyle = FlatStyle.Flat;
                    confirm.FlatAppearance.BorderSize = 0;
                    confirm.BackColor = Color.FromArgb(191, 44, 36);
                    confirm.Text = "Xác nhận";
                    confirm.ForeColor = Color.White;
                    confirm.Size = new Size(100, 60);
                    confirm.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    confirm.Margin = new Padding(10);
                    
                    confirm.Click += async (s, ev) =>
                    {
                        confirm.Enabled = false;
                        Task<string> res = Interact.createNovel(
                            novel["Anh"].ToString(), 
                            novel["So_chuong"].ToString(),
                            novel["Tac_gia"].ToString(),
                            novel["Ten"].ToString(),
                            typeStringArray,
                            novel["Tom_tat"].ToString(),
                            "Truyen"
                            );
                        string novelid = await res;
                        FirebaseResponse resf = await client.GetAsync("Nguoi_dung/" + user.User.Uid + "/Truyen_dang");
                        Dictionary<string, string> truyendang = resf.ResultAs<Dictionary<string, string>>();
                        truyendang.Add(novelid, novel["Ten"].ToString());
                        FirebaseResponse response = await client.UpdateAsync("Nguoi_dung/" + user.User.Uid + "/Truyen_dang", truyendang);
                        Interact.deleteNovel(novel["Ten"].ToString().ToUpper(), "Dang_truyen");
                        MessageBox.Show("Đăng truyện thành công!");

                        confirm.Enabled = true;
                        panelList.Visible = false;
                    };

                }
                
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
