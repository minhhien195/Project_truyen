using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth;
using Google.Cloud.Firestore;
using Novel;

namespace Login
{
    public partial class Danh_Sach_Chuong_CTT : Form
    {
        public class Chapter
        {
            [FirestoreProperty("ID_Truyen")]
            public string idBook { get; set; }
            [FirestoreProperty("Noi_dung")]
            public string Content { get; set; }
            [FirestoreProperty("TG_dangtai")]
            public Timestamp Times { get; set; }
            [FirestoreProperty("Tieu_de")]
            public string Title { get; set; }
        }
        CollectionReference chapter;

        private Trang_chu trangchu;
        private UserCredential userCredential;
        private string Idtruyen;

        public Danh_Sach_Chuong_CTT(CollectionReference chapter, Trang_chu tc, UserCredential user, string ID)
        {
            InitializeComponent();
            this.chapter = chapter;
            this.trangchu = tc;
            this.userCredential = user;
            this.Idtruyen = ID;
        }

        int currentPage = 0;
        int so_trang;

        private async void Danh_Sach_Chuong_Load(object sender, EventArgs e)
        {
            QuerySnapshot qs = await chapter.GetSnapshotAsync();
            so_trang = qs.Count / 30; // Tính số trang
            int cout_chuong = 0;
            foreach (var item in qs)
            {
                if (item != null)
                {
                    Dictionary<string, object> chapter = item.ToDictionary();

                    Label label = new Label();
                    panelListChap.Controls.Add(label);
                    label.Text = chapter["Tieu_de"].ToString();
                    label.Name = "labelChapter" + Convert.ToInt32(item.Id).ToString();
                    label.Dock = DockStyle.Top;
                    label.AutoSize = true;
                    label.Font = new Font("League Spartan", 16, FontStyle.Regular);
                    label.Name = "labelChapter" + Convert.ToInt32(item.Id).ToString();
                    label.Click += (S, ev) =>
                    {
                        trangchu.change_color();
                        trangchu.openChildForm(new Doc_Truyen(Idtruyen, userCredential, Convert.ToInt32(item.Id), trangchu));
                    };
                    label.MouseEnter += (S, ev) =>
                    {
                        label.ForeColor = Color.Red;
                    };
                    label.MouseLeave += (S, ev) =>
                    {
                        label.ForeColor = Color.Black;
                    };

                    label.BringToFront();

                    cout_chuong++;
                    if (cout_chuong % 30 == 0)
                    {
                        currentPage++;
                        if (currentPage <= so_trang)
                        {
                            tb1.Text = "Trang " + currentPage.ToString() + "/" + so_trang.ToString();
                            break;
                        }
                    }
                }
            }
        }

        private async void ibtDown_Click(object sender, EventArgs e)
        {
            if (currentPage > 2)
            {
                currentPage--;
                tb1.Text = "Trang " + currentPage.ToString() + "/" + so_trang.ToString();

                panelListChap.Controls.Clear();
                int cout_chuong = 0;

                QuerySnapshot qs = await chapter.GetSnapshotAsync();
                int skipCount = currentPage * 30;
                int takeCount = 30;

                foreach (var item in qs.Skip(skipCount).Take(takeCount))
                {
                    if (item != null)
                    {
                        Dictionary<string, object> chapter = item.ToDictionary();

                        Label label = new Label();
                        panelListChap.Controls.Add(label);
                        label.Text = chapter["Tieu_de"].ToString();
                        label.Name = "labelChapter" + Convert.ToInt32(item.Id).ToString();
                        label.Dock = DockStyle.Top;
                        label.AutoSize = true;
                        label.Font = new Font("League Spartan", 16, FontStyle.Regular);
                        label.Name = "labelChapter" + Convert.ToInt32(item.Id).ToString();
                        label.Click += (S, ev) =>
                        {
                            trangchu.change_color();
                            trangchu.openChildForm(new Doc_Truyen(Idtruyen, userCredential, Convert.ToInt32(item.Id), trangchu));
                        };
                        label.MouseEnter += (S, ev) =>
                        {
                            label.ForeColor = Color.Red;
                        };
                        label.MouseLeave += (S, ev) =>
                        {
                            label.ForeColor = Color.Black;
                        };

                        label.BringToFront();

                        cout_chuong++;
                    }
                }
            }
            else if(currentPage == 2)
            {
                currentPage--;
                tb1.Text = "Trang " + currentPage.ToString() + "/" + so_trang.ToString();
                panelListChap.Controls.Clear();
                QuerySnapshot qs = await chapter.Limit(30).GetSnapshotAsync();
                foreach (var item in qs)
                {
                    if (item != null)
                    {
                        Dictionary<string, object> chapter = item.ToDictionary();

                        Label label = new Label();
                        panelListChap.Controls.Add(label);
                        label.Text = chapter["Tieu_de"].ToString();
                        label.Name = "labelChapter" + Convert.ToInt32(item.Id).ToString();
                        label.Dock = DockStyle.Top;
                        label.AutoSize = true;
                        label.Font = new Font("League Spartan", 16, FontStyle.Regular);
                        label.Name = "labelChapter" + Convert.ToInt32(item.Id).ToString();
                        label.Click += (S, ev) =>
                        {
                            trangchu.change_color();
                            trangchu.openChildForm(new Doc_Truyen(Idtruyen, userCredential, Convert.ToInt32(item.Id), trangchu));
                        };
                        label.MouseEnter += (S, ev) =>
                        {
                            label.ForeColor = Color.Red;
                        };
                        label.MouseLeave += (S, ev) =>
                        {
                            label.ForeColor = Color.Black;
                        };
                        // Thêm label vào Controls của Form
                        /*this.Controls.Add(label);*/
                        label.BringToFront();
                    }
                }
            }
        }

        private async void ibtUp_Click(object sender, EventArgs e)
        {
            if (currentPage < so_trang)
            {
                currentPage++;
                panelListChap.Controls.Clear();
                int cout_chuong = 0;
                tb1.Text = "Trang " + currentPage.ToString() + "/" + so_trang.ToString();
                QuerySnapshot qs = await chapter.GetSnapshotAsync();
                int skipCount = currentPage * 30;
                int takeCount = 30;

                foreach (var item in qs.Skip(skipCount).Take(takeCount))
                {
                    if (item != null)
                    {
                        Dictionary<string, object> chapter = item.ToDictionary();

                        Label label = new Label();
                        panelListChap.Controls.Add(label);
                        label.Text = chapter["Tieu_de"].ToString();
                        label.Name = "labelChapter" + Convert.ToInt32(item.Id).ToString();
                        label.Dock = DockStyle.Top;
                        label.AutoSize = true;
                        label.Font = new Font("League Spartan", 16, FontStyle.Regular);
                        label.Name = "labelChapter" + Convert.ToInt32(item.Id).ToString();
                        label.Click += (S, ev) =>
                        {
                            trangchu.change_color();
                            trangchu.openChildForm(new Doc_Truyen(Idtruyen, userCredential, Convert.ToInt32(item.Id), trangchu));
                        };
                        label.MouseEnter += (S, ev) =>
                        {
                            label.ForeColor = Color.Red;
                        };
                        label.MouseLeave += (S, ev) =>
                        {
                            label.ForeColor = Color.Black;
                        };

                        label.BringToFront();

                        cout_chuong++;
                    }
                }
            }
        }
    }
}
