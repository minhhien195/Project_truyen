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
using FontAwesome.Sharp;
using Google.Cloud.Firestore;

namespace Login
{
    public partial class Bang_Xep_Hang : Form
    {
        public Bang_Xep_Hang()
        {
            InitializeComponent();
        }

        private void btnTrend_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childFormPanel.Controls.Add(childForm);
            childFormPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void iconButtonTrend_Click(object sender, EventArgs e)
        {
            iconButtonTrend.BackColor = Color.FromArgb(191, 44, 36);
            iconButtonTrend.ForeColor = Color.White;
            iconButtonTrend.IconColor = Color.White;

            iconButtonRead.BackColor = Color.White;
            iconButtonRead.ForeColor = Color.Black;
            iconButtonRead.IconColor = Color.Black;

            iconButtonRecom.BackColor = Color.White;
            iconButtonRecom.ForeColor = Color.Black;
            iconButtonRecom.IconColor = Color.Black;

            activeForm.Close();
            panelList1.Visible = true;
        }

        private void iconButtonRead_Click(object sender, EventArgs e)
        {
            panelList1.Visible = false;
            iconButtonRead.BackColor = Color.FromArgb(191, 44, 36);
            iconButtonRead.ForeColor = Color.White;
            iconButtonRead.IconColor = Color.White;

            iconButtonTrend.BackColor = Color.White;
            iconButtonTrend.ForeColor = Color.Black;
            iconButtonTrend.IconColor = Color.Black;

            iconButtonRecom.BackColor = Color.White;
            iconButtonRecom.ForeColor = Color.Black;
            iconButtonRecom.IconColor = Color.Black;

            openChildForm(new BXH_Doc_Nhieu());
        }

        private void iconButtonRecom_Click(object sender, EventArgs e)
        {
            panelList1.Visible = false;
            iconButtonRecom.BackColor = Color.FromArgb(191, 44, 36);
            iconButtonRecom.ForeColor = Color.White;
            iconButtonRecom.IconColor = Color.White;

            iconButtonTrend.BackColor = Color.White;
            iconButtonTrend.ForeColor = Color.Black;
            iconButtonTrend.IconColor = Color.Black;

            iconButtonRead.BackColor = Color.White;
            iconButtonRead.ForeColor = Color.Black;
            iconButtonRead.IconColor = Color.Black;

            openChildForm(new BXH_De_Cu());
        }

        private void iconButton3_MouseEnter(object sender, EventArgs e)
        {
            iconButton3.BackColor = Color.Transparent;
        }

        private void iconButton3_MouseHover(object sender, EventArgs e)
        {
            iconButton3.BackColor = Color.Transparent;
        }

        async private void Bang_Xep_Hang_Load(object sender, EventArgs e)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            List<Novel> list = new List<Novel>();

            /*Panel childFormPanel = new Panel();
            this.Controls.Add(childFormPanel);
            childFormPanel.Dock = DockStyle.Fill;
            childFormPanel.AutoSize = true;
            childFormPanel.AutoScroll = true;*/

            Panel header = new Panel();
            childFormPanel.Controls.Add(header);
            header.Dock = DockStyle.Top;
            header.AutoSize = true;

            /*Label hdLabel = new Label();
            header.Controls.Add(hdLabel);
            hdLabel.Dock = DockStyle.Top;
            hdLabel.AutoSize = true;
            hdLabel.Font = new Font("League Spartan", 20, FontStyle.Bold);
            hdLabel.Text = "Bảng xếp hạng đọc nhiều";*/

            CollectionReference collection = db.Collection("Truyen");
            Query q = collection.OrderByDescending("Luot_thich");
            QuerySnapshot qs = await collection.GetSnapshotAsync();



            if (qs.Documents.Count <= 0)
            {
                Panel panelList = new Panel();
                panelList.Dock = DockStyle.Top;
                panelList.AutoSize = true;
                panelList.BringToFront();
                childFormPanel.Controls.Add(panelList);

                Label noti = new Label();
                panelList.Controls.Add(noti);
                noti.Dock = DockStyle.Top;
                noti.AutoSize = true;
                noti.Font = new Font("League Spartan", 20, FontStyle.Bold);
                noti.Text = "Không có truyện cần duyệt";
            }
            else
            {
                int i = 1;
                foreach (var item in qs.Documents)
                {
                    Dictionary<string, object> novel = item.ToDictionary();
                    Panel panelList = new Panel();
                    panelList.Dock = DockStyle.Top;
                    panelList.AutoSize = true;
                    panelList.BringToFront();
                    childFormPanel.Controls.Add(panelList);
                    panelList.BringToFront();

                    Label rank = new Label();
                    panelList.Controls.Add(rank);
                    rank.Dock = DockStyle.Top;
                    rank.AutoSize = true;
                    rank.Font = new Font("League Spartan", 14, FontStyle.Regular);
                    rank.Text = "Hạng " + i.ToString();
                    i++;


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
                    info.ColumnCount = 3;
                    info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
                    info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
                    info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
                    info.BringToFront();

                    IconButton author = new IconButton();
                    info.Controls.Add(author, 0, 0);
                    author.Dock = DockStyle.Fill;
                    author.FlatStyle = FlatStyle.Flat;
                    author.FlatAppearance.BorderSize = 0;
                    author.Font = new Font("League Spartan", 14, FontStyle.Regular);
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
                    recom.Font = new Font("League Spartan", 14, FontStyle.Regular);
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
}
