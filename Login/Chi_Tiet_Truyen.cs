using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Chi_Tiet_Truyen : Form
    {
        public Chi_Tiet_Truyen()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Chi_Tiet_Truyen_Load(object sender, EventArgs e)
        {
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
            panel6.Visible = false;
            openChildForm(new Danh_Sach_Chuong_CTT());
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
            panel6.Visible = true;
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
            panel6.Visible = false;
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
    }
}
