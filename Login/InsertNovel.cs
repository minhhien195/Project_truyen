using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Novel;

namespace Login
{
    public partial class InsertNovel : Form
    {
        string base64Text = "";
        public InsertNovel()
        {
            InitializeComponent();
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
            panelFormInsert.Controls.Add(childForm);
            panelFormInsert.Tag = childForm;
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

        private void btnDang_Click(object sender, EventArgs e)
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
                Interact.createNovel(base64Text, "0", txtTacgia.Text, txtTentruyen.Text, theloai, rtbGioithieu.Text, "Dang_truyen");

                panelFormInsert.Visible = false;
                btnInsert.BackColor = Color.FromArgb(220, 247, 253);
                btnInsert.ForeColor = Color.Black;
                btnInserted.BackColor = Color.FromArgb(191, 44, 36);
                btnInserted.ForeColor = Color.White;
                openChildForm(new Inserted());
            }
            

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
            panelFormInsert.Visible = false;
            btnInsert.BackColor = Color.FromArgb(220, 247, 253);
            btnInsert.ForeColor = Color.Black;
            btnInserted.BackColor = Color.FromArgb(191, 44, 36);
            btnInserted.ForeColor = Color.White;
            openChildForm(new Inserted());
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            
            if (btnInsert.ForeColor == Color.White)
            {
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
