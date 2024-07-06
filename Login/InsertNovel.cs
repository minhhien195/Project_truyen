﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Novel;

namespace Login
{
    public partial class InsertNovel : Form
    {

        UserCredential user;
        FirebaseAuthClient client;
        IFirebaseClient ifclient;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
            BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        string base64Text = "";
        private Trang_chu tc;
        public InsertNovel(UserCredential usercredials, Trang_chu tc)
        {
            InitializeComponent();
            this.user = usercredials;
            this.tc = tc;
        }


        private Form activeForm = null;
        private Panel panelAll;

        private void openChildForm(Form childForm)
        {
            // Check if there's an active form and close it
            if (activeForm != null)
            {
                activeForm.Close();
            }

            // Set the new active form
            activeForm = childForm;

            // Prepare the child form for docking
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Create a panel to hold the child form
            panelAll = new Panel
            {
                Size = new Size(815, 355),
                Location = new Point(0, 67),
                Visible = true,
                AutoScroll = true,
                AutoSize = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Tag = childForm
            };

            // Add the child form to the panel and bring it to the front
            this.Controls.Add(panelAll);
            panelAll.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
        private void ibtninsertImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.JPG;*.PNG)|*.JPG;*.PNG";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ibtninsertImage.Text = dialog.FileName;
                byte[] imageArray = System.IO.File.ReadAllBytes(dialog.FileName);
                base64Text = Convert.ToBase64String(imageArray);
                if (base64Text == "")
                {
                    ptrThemanh.Visible = true;
                    lbTA.Visible = true;
                }
                else
                {
                    ptrThemanh.Visible = false;
                    lbTA.Visible = false;
                }
            }

        }

        private async void btnDang_Click(object sender, EventArgs e)
        {
            if (ibtninsertImage.Text == "Thêm ảnh truyện(.jpg, .png)" || ibtninsertImage.Text == "")
            {
                ptrThemanh.Visible = true;
                lbTA.Visible = true;
            }
            if (txtTacgia.Text == "")
            {
                ptrTacgia.Visible = true;
                lbTG.Visible = true;
            }
            if (txtTentruyen.Text == "")
            {
                ptrTentruyen.Visible = true;
                lbTT.Visible = true;
            }
            if (rtbGioithieu.Text == "")
            {
                ptrGioithieu.Visible = true;
                lbGT.Visible = true;
            }
            if (cbTheloai.Text == "")
            {
                lbTL.Visible = true;
                ptrTheloai.Visible = true;
            }
            if (cbBoicanh.Text == "")
            {
                ptrBoicanh.Visible = true;
                lbBC.Visible = true;
            }
            if (cbLuuphai.Text == "")
            {
                ptrLuuphai.Visible = true;
                lbLP.Visible = true;
            }
            if (lbTA.Visible == false && lbTG.Visible == false && lbTL.Visible == false && lbGT.Visible == false
                && lbBC.Visible == false && lbLP.Visible == false && lbTT.Visible == false)
            {
                string[] theloai = new string[] { cbTheloai.Text, cbBoicanh.Text, cbLuuphai.Text };
                await Interact.createNovel(base64Text, "0", txtTacgia.Text, txtTentruyen.Text.ToUpper(), theloai, rtbGioithieu.Text, "Dang_truyen", user.User.Uid);

                upload_realtime();

                panelFormInsert.Visible = false;
                btnInsert.BackColor = Color.FromArgb(220, 247, 253);
                btnInsert.ForeColor = Color.Black;
                btnInserted.BackColor = Color.FromArgb(191, 44, 36);
                btnInserted.ForeColor = Color.White;
                openChildForm(new Inserted(user, tc));
            }
            

        }

        private async void upload_realtime()
        {
            FirestoreDb db = FirestoreDb.Create("healtruyen");

            CollectionReference truyen2 = db.Collection("Truyen");
            QuerySnapshot qs2 = await truyen2.GetSnapshotAsync();
            int order = qs2.Count + 1;
            string novelId = "";
            for (int i = 0; i < 3 - order.ToString().Length; i++)
            {
                novelId += "0";
            }
            novelId += order.ToString();
            Dictionary<string, string> noidung_up = new Dictionary<string, string>();
            noidung_up.Add(novelId, txtTentruyen.Text);



        }

        private void txtTacgia_TextChanged(object sender, EventArgs e)
        {
            if (lbTG.Visible)
            {
                ptrTacgia.Visible = false;
                lbTG.Visible = false;
            }
        }

        private void txtTentruyen_TextChanged(object sender, EventArgs e)
        {
            if (lbTT.Visible)
            {
                ptrTentruyen.Visible = false;
                lbTT.Visible = false;
            }
        }

        private void cbTheloai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTL.Visible)
            {
                ptrTheloai.Visible = false;
                lbTL.Visible = false;
                ptrBoicanh.Visible = false;
                lbBC.Visible = false;
                ptrLuuphai.Visible = false;
                lbLP.Visible = false;
            }
        }

        private void cbBoicanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbBC.Visible)
            {
                ptrTheloai.Visible = false;
                lbTL.Visible = false;
                ptrBoicanh.Visible = false;
                lbBC.Visible = false;
                ptrLuuphai.Visible = false;
                lbLP.Visible = false;
            }
        }

        private void cbLuuphai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbLP.Visible)
            {
                ptrTheloai.Visible = false;
                lbTL.Visible = false;
                ptrBoicanh.Visible = false;
                lbBC.Visible = false;
                ptrLuuphai.Visible = false;
                lbLP.Visible = false;
            }
        }

        private void rtbGioithieu_TextChanged(object sender, EventArgs e)
        {
            if (lbGT.Visible)
            {
                ptrGioithieu.Visible = false;
                lbGT.Visible = false;
            }
        }

        private void btnInserted_Click(object sender, EventArgs e)
        {
            if (btnInserted.ForeColor == Color.Black)
            {
                panelFormInsert.Visible = false;
                btnInsert.BackColor = Color.FromArgb(220, 247, 253);
                btnInsert.ForeColor = Color.Black;
                btnInserted.BackColor = Color.FromArgb(191, 44, 36);
                btnInserted.ForeColor = Color.White;
                openChildForm(new Inserted(user, tc));
            }  
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (btnInsert.ForeColor == Color.Black)
            {
                panelAll.Visible = false;
                btnInserted.BackColor = Color.FromArgb(220, 247, 253);
                btnInserted.ForeColor = Color.Black;
                btnInsert.BackColor = Color.FromArgb(191, 44, 36);
                btnInsert.ForeColor = Color.White;
                ptrTheloai.Visible = false;
                lbTL.Visible = false;
                ptrBoicanh.Visible = false;
                lbBC.Visible = false;
                ptrLuuphai.Visible = false;
                lbLP.Visible = false;
                ptrGioithieu.Visible = false;
                lbGT.Visible = false;
                ptrTentruyen.Visible = false;
                lbTT.Visible = false;
                ptrTacgia.Visible = false;
                lbTG.Visible = false;
                activeForm.Close();
                panelFormInsert.Visible = true;
            }
        }

        private void ibtnDangtruyen_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
