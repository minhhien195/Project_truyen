﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
/*using System.Windows.Controls;*/
using System.Windows.Forms;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FontAwesome.Sharp;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Novel;
using Readinghistory;

namespace Login
{
    public partial class Chi_Tiet_Truyen : Form
    {
        string nameTruyen;
        int currentChap;
        int numChap = 1;
        string idTruyen;
        IFirebaseConfig _firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        UserCredential user;
        public Chi_Tiet_Truyen(string nameTruyen, UserCredential user)
        {
            InitializeComponent();
            this.nameTruyen = nameTruyen;
            this.user = user;
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void Chi_Tiet_Truyen_Load(object sender, EventArgs e)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference collectionReference = db.Collection("Truyen");
            nameTruyen = nameTruyen.ToUpper();
            Query q = collectionReference.WhereEqualTo("Ten", nameTruyen);
            QuerySnapshot qs = await q.GetSnapshotAsync();
            if (qs.Documents.Count == 0)
            {
                return;
            }
            idTruyen = qs.Documents[0].Id;
            tableLayoutPanel2.Width = this.Width / 2;
            panelChildForm.Width = (int)(this.Width * 0.9);
            introContent.Width = (int)(panelChildForm.Width * 0.6);
            panel6.Width = (int)(panelChildForm.Width * 0.37);
            pictureAuthorIntro.Width = panel6.Width / 4;
            pictureAuthorIntro.Height = pictureAuthorIntro.Width;

            labelShortName.Location = new Point(
                (panelShortName.Width - labelShortName.Width) / 2, // Tính toán vị trí theo trục X
                (panelShortName.Height - labelShortName.Height) / 2 // Tính toán vị trí theo trục Y
            );
            labelShortName.Anchor = AnchorStyles.None;

            labelAuthor.Location = new Point(
                (panelAuthor.Width - labelAuthor.Width) / 2, // Tính toán vị trí theo trục X
                (panelAuthor.Height - labelAuthor.Height) / 2 // Tính toán vị trí theo trục Y
            );
            labelAuthor.Anchor = AnchorStyles.None;

            labelStatus.Location = new Point(
                (panelStatus.Width - labelStatus.Width) / 2, // Tính toán vị trí theo trục X
                (panelStatus.Height - labelStatus.Height) / 2 // Tính toán vị trí theo trục Y
            );
            labelStatus.Anchor = AnchorStyles.None;

            pictureAuthorIntro.Location = new Point(
                (panel6.Width - pictureAuthorIntro.Width) / 2, // Tính toán vị trí theo trục X
                70 // Tính toán vị trí theo trục Y
            );
            pictureAuthorIntro.Anchor = AnchorStyles.None;

            labelAuthorIntro.Location = new Point(
                (panel6.Width - labelAuthorIntro.Width) / 2, // Tính toán vị trí theo trục X
                210 // Tính toán vị trí theo trục Y
            );
            labelAuthorIntro.Anchor = AnchorStyles.None;

            pictureBox15.Location = new Point(
                (panel6.Width - pictureBox15.Width) / 4, // Tính toán vị trí theo trục X
                270 // Tính toán vị trí theo trục Y
            );
            pictureBox15.Anchor = AnchorStyles.None;

            label7.Location = new Point(
                (panel6.Width - label7.Width) / 5, // Tính toán vị trí theo trục X
                310 // Tính toán vị trí theo trục Y
            );
            label7.Anchor = AnchorStyles.None;

            labelBookAuthorInfo.Location = new Point(
                (panel6.Width - labelBookAuthorInfo.Width) / 4, // Tính toán vị trí theo trục X
                360 // Tính toán vị trí theo trục Y
            );
            labelBookAuthorInfo.Anchor = AnchorStyles.None;

            pictureBox16.Location = new Point(
                ((panel6.Width - pictureBox16.Width) * 3) / 4, // Tính toán vị trí theo trục X
                270 // Tính toán vị trí theo trục Y
            );
            pictureBox16.Anchor = AnchorStyles.None;


            label10.Location = new Point(
                ((panel6.Width - label10.Width) * 4) / 5, // Tính toán vị trí theo trục X
                310 // Tính toán vị trí theo trục Y
            );
            label10.Anchor = AnchorStyles.None;

            labelChapterAuthorInfo.Location = new Point(
                 (int)((panel6.Width - labelChapterAuthorInfo.Width) * 3.1) / 4, // Tính toán vị trí theo trục X
                 360 // Tính toán vị trí theo trục Y
             );
            labelChapterAuthorInfo.Anchor = AnchorStyles.None;

            pictureBox17.Location = new Point(
                (panel6.Width - pictureBox17.Width) / 2, // Tính toán vị trí theo trục X
                panel6.Height - 220 // Tính toán vị trí theo trục Y
            );

            pictureBox18.Location = new Point(
                pictureBox17.Location.X - 60, // Tính toán vị trí theo trục X
                pictureBox17.Location.Y + 40 // Tính toán vị trí theo trục Y
            );

            pictureBox19.Location = new Point(
                pictureBox17.Location.X + 120, // Tính toán vị trí theo trục X
                pictureBox17.Location.Y + 40 // Tính toán vị trí theo trục Y
            );

            label12.Location = new Point(
                (panel6.Width - label12.Width) / 2, // Tính toán vị trí theo trục X
                pictureBox17.Location.Y + pictureBox17.Height + 20 // Tính toán vị trí theo trục Y
            );
            Dictionary<string, object> novel = qs.Documents[0].ToDictionary();
            byte[] imageBytes = Convert.FromBase64String(novel["Anh"].ToString());

            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                Bitmap bitmap = new Bitmap(memoryStream);
                coverBookImg.Image = bitmap;
                coverBookImg.SizeMode = PictureBoxSizeMode.Zoom;
            }
            labelAuthor.Text = novel["Tac_gia"].ToString();
            nameBook.Text = novel["Ten"].ToString();
            labelShortName.Text = novel["Tac_gia"].ToString();
            if (novel["Trang_thai"].ToString() == "0")  
                labelStatus.Text = "Dừng cập nhật";
            else if (novel["Trang_thai"].ToString() == "1")
                labelStatus.Text = "Đang ra";
            else labelStatus.Text = "Hoàn thành";
            object typeList = novel["The_loai"];
            if (typeList is List<object> typeList1)
            {
                List<string> typeStringList = typeList1.Select(j => j.ToString()).ToList();
                labelType.Text = typeStringList[0];
            }
            labelNumChapter.Text = novel["So_chuong"].ToString();
            label5.Text = novel["De_cu"].ToString();
            labelRating.Text = novel["Danh_gia_Tb"].ToString();
            introContent.Text = novel["Tom_tat"].ToString();
            labelremNum.Text = novel["De_cu"].ToString();
            labelLike.Text = novel["Luot_thich"].ToString();
        }

        private Form activeForm = null;
        private void openChildForm (Form childForm)
        {
            if (activeForm != null )
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add( childForm );
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btnNumChapMenu_Click(object sender, EventArgs e)
        {
            string project = "healtruyen";
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference collectionReference = db.Collection("Truyen").Document(idTruyen).Collection("Chuong");
            panel6.Visible = false;
            openChildForm(new Danh_Sach_Chuong_CTT(collectionReference));
            btnNumChapMenu.ForeColor = Color.Red;
            btnNumChapMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);

            btnIntroMenu.ForeColor = Color.Black;
            btnIntroMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
            btnCommentMenu.ForeColor = Color.Black;
            btnCommentMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
            btnRatingMenu.ForeColor = Color.Black;
            btnRatingMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
        }

        private void btnIntroMenu_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            activeForm.Close();
            btnNumChapMenu.ForeColor = Color.Black;
            btnNumChapMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);

            btnIntroMenu.ForeColor = Color.Red;
            btnIntroMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
            btnCommentMenu.ForeColor = Color.Black;
            btnCommentMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
            btnRatingMenu.ForeColor = Color.Black;
            btnRatingMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
        }

        private void btnRatingMenu_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            openChildForm(new Danh_Gia_CTT());
            btnNumChapMenu.ForeColor = Color.Black;
            btnNumChapMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);

            btnIntroMenu.ForeColor = Color.Black;
            btnIntroMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
            btnCommentMenu.ForeColor = Color.Black;
            btnCommentMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
            btnRatingMenu.ForeColor = Color.Red;
            btnRatingMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
        }

        private void btnCommentMenu_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            openChildForm(new Binh_Luan_CTT(nameTruyen,user,idTruyen));
            btnNumChapMenu.ForeColor = Color.Black;
            btnNumChapMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);

            btnIntroMenu.ForeColor = Color.Black;
            btnIntroMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
            btnCommentMenu.ForeColor = Color.Red;
            btnCommentMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
            btnRatingMenu.ForeColor = Color.Black;
            btnRatingMenu.Font = new Font("League Spartan", 16, FontStyle.Regular);
        }
    }
}
