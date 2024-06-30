using Firebase.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Login.Signup;

namespace Login
{
    public partial class Trang_chu : Form
    {

        UserCredential user;
        FirebaseAuthClient client;
        IFirebaseClient ifclient;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        //Fields
        // private IconButton currentBtn;
        private Panel leftBorderBtn;
        // private Form currentChildForm

        public Trang_chu(UserCredential userCredential, FirebaseAuthClient firebaseAuthClient)
        {
            InitializeComponent();
            ifclient = new FireSharp.FirebaseClient(config);
            this.user = userCredential;
            this.client = firebaseAuthClient;

        }

        public Trang_chu()
        {
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            if (panelMenu != null)
            {
                panelMenu.Controls.Add(leftBorderBtn);
            }
            customDesign();
            panelAll.BringToFront();
            this.Controls.SetChildIndex(panelAll, 0);
            this.Controls.SetChildIndex(panelClose, 1);
            // Make sure panelAll is on top of PanelMenu and PanelClose
            /*  Form
                this.Text = string.Empty;
                this.ControlBox = false;
                this.DoubleBuffered = true;
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;*/
        }

        private async void Trang_chu_Load(object sender, EventArgs e)
        {
            try
            {
                string ID = user.User.Info.Uid;
                GraphicsPath grpath = new GraphicsPath();
                grpath.AddEllipse(0, 0, pbAvt.Width, pbAvt.Height);
                Region rg = new Region(grpath);
                pbAvt.Region = rg;
                string path_pic = "Nguoi_dung/" + ID + "/Anh_dai_dien";
                FirebaseResponse response = await ifclient.GetAsync(path_pic);
                string base64String = response.ResultAs<string>();
                byte[] imageBytes = Convert.FromBase64String(base64String);

                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(memoryStream);
                    pbAvt.Image = bitmap;
                    pbAvt.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!");
            }
            LoadDeCu();
            LoadDanhGia();
            LoadDocNhieu();
            LoadGanNhat();
        }

        private string format_out(string s)
        {
            if (s.Length > 16)
            {
                return s.Substring(0, 16) + "...";
            }
            else
            {
                return s;
            }
        }

        private async void LoadDeCu()
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);

            // Tham chiếu đến bộ sưu tập "Truyen" và sắp xếp theo biến "De_cu" giảm dần
            CollectionReference collection = db.Collection("Truyen");
            Query query = collection.OrderByDescending("De_cu").Limit(5);
            QuerySnapshot qs = await query.GetSnapshotAsync();

            if (qs.Documents.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu để tải");
            }
            else
            {
                int i = 1;
                foreach (var item in qs.Documents)
                {
                    if (i <= 5) // Chỉ xử lý đến PictureBox truyen5
                    {
                        Dictionary<string, object> novel = item.ToDictionary();
                        byte[] imageBytes = Convert.FromBase64String(novel["Anh"].ToString());

                        using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                        {
                            Bitmap bitmap = new Bitmap(memoryStream);
                            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(237, 327));
                            PictureBox pictureBox = Controls.Find("truyen" + i, true).FirstOrDefault() as PictureBox;

                            if (pictureBox != null)
                            {
                                pictureBox.Image = resizedBitmap;
                                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }
                        i++;
                    }
                    else
                    {
                        break; // Thoát khỏi vòng lặp nếu đã xử lý đủ 5 PictureBox
                    }
                }
            }
        }

        private async void LoadDanhGia()
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);

            CollectionReference collection = db.Collection("Truyen");
            Query query = collection.OrderByDescending("Danh_gia_Tb").Limit(2);
            QuerySnapshot qs = await query.GetSnapshotAsync();

            if (qs.Documents.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu để tải");
            }
            else
            {
                int i = 1;
                foreach (var item in qs.Documents)
                {
                    if (i <= 2)
                    {
                        Dictionary<string, object> novel = item.ToDictionary();
                        byte[] imageBytes = Convert.FromBase64String(novel["Anh"].ToString());

                        using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                        {
                            Bitmap bitmap = new Bitmap(memoryStream);
                            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(90, 125));

                            PictureBox pictureBox = Controls.Find("tt0" + i, true).FirstOrDefault() as PictureBox;
                            if (pictureBox != null)
                            {
                                pictureBox.Image = resizedBitmap;
                                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }

                        // Hiển thị thông tin về tên truyện, số chương và tác giả
                        string tenTruyen = novel.ContainsKey("Ten") ? novel["Ten"].ToString() : "N/A";
                        Label ttt = Controls.Find("ttt0" + i, true).FirstOrDefault() as Label;
                        ttt.Text = format_out(tenTruyen);

                        string soChuong = novel.ContainsKey("So_chuong") ? novel["So_chuong"].ToString() : "N/A";
                        Label sc = Controls.Find("sc0" + i, true).FirstOrDefault() as Label;
                        sc.Text = soChuong;

                        string tacGia = novel.ContainsKey("Tac_gia") ? novel["Tac_gia"].ToString() : "N/A";
                        Label tg = Controls.Find("tg0" + i, true).FirstOrDefault() as Label;
                        tg.Text = tacGia;

                        string DanhGia = novel.ContainsKey("Danh_gia_Tb") ? novel["Danh_gia_Tb"].ToString() : "N/A";
                        Label dg = Controls.Find("ps0" + i, true).FirstOrDefault() as Label;
                        dg.Text = DanhGia;

                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private async void LoadDocNhieu()
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);

            CollectionReference collection = db.Collection("Truyen");
            Query query = collection.OrderByDescending("Luot_xem").Limit(2);
            QuerySnapshot qs = await query.GetSnapshotAsync();

            if (qs.Documents.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu để tải");
            }
            else
            {
                int i = 3;
                foreach (var item in qs.Documents)
                {
                    if (i <= 4)
                    {
                        Dictionary<string, object> novel = item.ToDictionary();
                        byte[] imageBytes = Convert.FromBase64String(novel["Anh"].ToString());

                        using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                        {
                            Bitmap bitmap = new Bitmap(memoryStream);
                            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(90, 125));
                            PictureBox pictureBox = Controls.Find("tt0" + i, true).FirstOrDefault() as PictureBox;
                            if (pictureBox != null)
                            {
                                pictureBox.Image = resizedBitmap;
                                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }

                        // Hiển thị thông tin về tên truyện, số chương và tác giả
                        string tenTruyen = novel.ContainsKey("Ten") ? novel["Ten"].ToString() : "N/A";
                        Label ttt = Controls.Find("ttt0" + i, true).FirstOrDefault() as Label;
                        ttt.Text = format_out(tenTruyen);

                        string soChuong = novel.ContainsKey("So_chuong") ? novel["So_chuong"].ToString() : "N/A";
                        Label sc = Controls.Find("sc0" + i, true).FirstOrDefault() as Label;
                        sc.Text = soChuong;

                        string tacGia = novel.ContainsKey("Tac_gia") ? novel["Tac_gia"].ToString() : "N/A";
                        Label tg = Controls.Find("tg0" + i, true).FirstOrDefault() as Label;
                        tg.Text = tacGia;

                        string Luotxem = novel.ContainsKey("Luot_xem") ? novel["Luot_xem"].ToString() : "N/A";
                        Label dg = Controls.Find("ps0" + i, true).FirstOrDefault() as Label;
                        dg.Text = Luotxem;

                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private async void LoadGanNhat()
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);

            CollectionReference collection = db.Collection("Truyen");
            QuerySnapshot qs = await collection.GetSnapshotAsync();

            if (qs.Documents.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu để tải");
            }
            else
            {
                var latestup = new List<(DocumentSnapshot story, DocumentSnapshot chapter)>();

                foreach (var item in qs.Documents)
                {
                    string storyId = item.Id;

                    // Fetch the latest chapter for this story
                    CollectionReference chaptersCollection = db.Collection($"Truyen/{storyId}/Chuong");
                    Query query = chaptersCollection.OrderByDescending("TG_dangtai").Limit(1);
                    QuerySnapshot chapters = await query.GetSnapshotAsync();

                    if (chapters.Documents.Count > 0)
                    {
                        var chapterDoc = chapters.Documents.First();
                        latestup.Add((item, chapterDoc));
                    }
                }

                // Sort stories by the latest chapter timestamp
                latestup = latestup
                  .OrderByDescending(x => x.chapter.GetValue<Timestamp>("TG_dangtai"))
                  .Take(2)
                  .ToList();

                int i = 5;
                foreach (var (story, chapter) in latestup)
                {
                    if (i <= 6)
                    {
                        Dictionary<string, object> storyData = story.ToDictionary();
                        Dictionary<string, object> chapterData = chapter.ToDictionary();

                        byte[] imageBytes = Convert.FromBase64String(storyData["Anh"].ToString());

                        using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                        {
                            Bitmap bitmap = new Bitmap(memoryStream);
                            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(90, 125));
                            PictureBox pictureBox = Controls.Find("tt0" + i, true).FirstOrDefault() as PictureBox;
                            if (pictureBox != null)
                            {
                                pictureBox.Image = resizedBitmap;
                                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }

                        string tenTruyen = storyData.ContainsKey("Ten") ? story.GetValue<string>("Ten") : "N/A";
                        Label ttt = Controls.Find("ttt0" + i, true).FirstOrDefault() as Label;
                        ttt.Text = format_out(tenTruyen);

                        string tacgia = storyData.ContainsKey("Tac_gia") ? story.GetValue<string>("Tac_gia") : "N/A";
                        Label tg = Controls.Find("tg0" + i, true).FirstOrDefault() as Label;
                        tg.Text = tacgia;

                        /*CollectionReference chaptersCollection = db.Collection($"Truyen/{story.Id}/Chuong");
                        QuerySnapshot chaptersSnapshot = await chaptersCollection.GetSnapshotAsync();
                        int sochuong = chaptersSnapshot.Count;
                        Label sc = Controls.Find("sc0" + i, true).FirstOrDefault() as Label;
                        sc.Text = sochuong.ToString();*/

                        string soChuong = storyData.ContainsKey("So_chuong") ? storyData["So_chuong"].ToString() : "N/A";
                        Label sc = Controls.Find("sc0" + i, true).FirstOrDefault() as Label;
                        sc.Text = soChuong;

                        string chuongmoi = chapterData.ContainsKey("Tieu_de") ? chapter.GetValue<string>("Tieu_de") : "N/A";
                        Label ps = Controls.Find("ps0" + i, true).FirstOrDefault() as Label;
                        ps.Text = format_out(chuongmoi);

                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void customDesign()
        {
            if (panelTheodoiSubmenu != null)
            {
                panelTheodoiSubmenu.Visible = false;
            }
            if (panelTieudeSubmenu != null)
            {
                panelTieudeSubmenu.Visible = false;
            }
            if (panelHEALTruyenSubmenu != null)
            {
                panelHEALTruyenSubmenu.Visible = false;
            }
            
        }
        private void hideSubMenu()
        {
            if (panelTheodoiSubmenu.Visible)
            {
                panelTheodoiSubmenu.Visible = false;
            }
            if (panelTieudeSubmenu.Visible)
            {
                panelTieudeSubmenu.Visible = false;
            }
            if (panelHEALTruyenSubmenu.Visible)
            {
                panelHEALTruyenSubmenu.Visible = false;
            }
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                //hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private void btnbars_Click(object sender, EventArgs e)
        {
            // panelClose.Visible = true;
            CollapseMenu();
        }
        private void ibtnXmark_Click(object sender, EventArgs e)
        {
            //panelClose.Visible = false;
            CollapseMenu();
        }
        private void CollapseMenu()
        {
            if (this.panelMenu.Width >= 250)
            {
                panelAll.BringToFront();
                panelAll.Size = new Size(1980, 935);
                panelAll.Location = new Point(0, 106);
                panelMenu.Width = 0;
                truyen1.Left -= 30;
                truyen2.Left -= 30;
                truyen3.Left -= 30;
                truyen4.Left -= 30;
                truyen5.Left += 300;
                panelMenu.Visible = false;
                panelClose.Left -= 250;
                panelClose.Visible = true;
                
                dangxem.Location = new Point(686, 504);
                

            }
            else if (this.panelMenu.Width == 0)
            {
                panelMenu.Width = 250;
                panelMenu.Visible = true;
                panelClose.Left += 250;
                panelClose.Visible = false;
                truyen1.Left += 30;
                truyen2.Left += 30;
                truyen3.Left += 30;
                truyen4.Left += 30;
                truyen5.Left -= 300;
                panelAll.Size = new Size(1652, 935);
                panelAll.Location = new Point(256, 106);
                dangxem.Location = new Point(547, 504);

            }
        }


        private void ibtnTheodoi_Click(object sender, EventArgs e)
        {
            showSubMenu(panelTheodoiSubmenu);
        }

        private void btnAlbum_Click(object sender, EventArgs e)
        {

        }

        private void btnBookmark_Click(object sender, EventArgs e)
        {

        }

        private void btnLichsudoc_Click(object sender, EventArgs e)
        {

        }

        private void ibtnTieude_Click(object sender, EventArgs e)
        {
            showSubMenu(panelTieudeSubmenu);
        }

        private void btnadvancedsearch_Click(object sender, EventArgs e)
        {

        }
        private void btnBXH_Click(object sender, EventArgs e)
        {

        }

        private void ibtnHEALTruyen_Click(object sender, EventArgs e)
        {
            showSubMenu(panelHEALTruyenSubmenu);
        }

        private void btnHelpandError_Click(object sender, EventArgs e)
        {

        }

        private void btnChat_Click(object sender, EventArgs e)
        {

        }

        private void btnThongbao_Click(object sender, EventArgs e)
        {

        }

        private void pbAvt_Click(object sender, EventArgs e)
        {
            setting_up up = new setting_up(user, client, this);
            up.Left = 1720;
            up.Top = 120;
            up.ShowDialog();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            Search tb = new Search(tbsearch.Text);
            tb.ShowDialog();
        }
    }
}
