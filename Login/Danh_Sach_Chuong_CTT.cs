using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public Danh_Sach_Chuong_CTT(CollectionReference chapter)
        {
            InitializeComponent();
            this.chapter = chapter;
        }

        private async void Danh_Sach_Chuong_Load(object sender, EventArgs e)
        {
            QuerySnapshot qs = await chapter.GetSnapshotAsync();
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
                    // Thêm label vào Controls của Form
                    /*this.Controls.Add(label);*/
                    label.BringToFront();
                }
            }
        }
    }
}
