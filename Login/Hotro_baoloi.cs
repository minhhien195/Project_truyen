using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace Login
{
    public partial class Hotro_baoloi : Form
    {
        UserCredential user;
        public Hotro_baoloi(UserCredential user)
        {
            InitializeComponent();
            customDesign();
            this.user = user;
        }
        private void customDesign()
        {
            panelSubmenuBaocaosuco.Visible = false;
            panelSubmenuBinhluan.Visible = false;
            panelSubmenuChat.Visible = false;
            panelSubmenuDangnhap.Visible = false;
            panelSubmenuDanhdau.Visible = false;
            panelSubmenuDanhgia.Visible = false;
            panelSubmenuDecu.Visible = false;
            panelSubmenuThaydoi.Visible = false;
            panelSubmenuTimkiem.Visible = false;
            paneSubmenuTuongtactruyen.Visible = false;
            panelSubmenuThemAlbum.Visible = false;
            panelSubmenuQLTK.Visible = false;
            panelSubmenuLichsudoc.Visible = false;
            panelSubmenuHDBaocao.Visible = false;
            panelSubmenuSudungchat.Visible = false;
        }
        private void hideSubMenu(Panel subMenu)
        {
            if (subMenu == panelSubmenuQLTK)
            {
                if (panelSubmenuDangnhap.Visible)
                {
                    panelSubmenuDangnhap.Visible = false;
                }
                if (panelSubmenuThaydoi.Visible)
                {
                    panelSubmenuThaydoi.Visible = false;
                }
            }    
            if (subMenu == paneSubmenuTuongtactruyen)
            {
                if (panelSubmenuBinhluan.Visible)
                {
                    panelSubmenuBinhluan.Visible = false;
                }
                if (panelSubmenuDanhdau.Visible)
                {
                    panelSubmenuDanhdau.Visible = false;
                }
                if (panelSubmenuDanhgia.Visible)
                {
                    panelSubmenuDanhgia.Visible = false;
                }
                if (panelSubmenuDecu.Visible)
                {
                    panelSubmenuDecu.Visible = false;
                }

                if (panelSubmenuTimkiem.Visible)
                {
                    panelSubmenuTimkiem.Visible = false;
                }
                if (panelSubmenuLichsudoc.Visible)
                {
                    panelSubmenuLichsudoc.Visible = false;
                }
            }    
            if (subMenu == panelSubmenuChat)
            {
                if (panelSubmenuSudungchat.Visible)
                {
                    panelSubmenuSudungchat.Visible = false;
                }
            }    
            if (subMenu == panelSubmenuBaocaosuco)
            {
                if (panelSubmenuHDBaocao.Visible)
                {
                    panelSubmenuHDBaocao.Visible = false;
                }
            }    
            /*if (panelSubmenuBaocaosuco.Visible)
            {
                panelSubmenuBaocaosuco.Visible = false;
            }
            
            if (panelSubmenuChat.Visible)
            {
                panelSubmenuChat.Visible = false;
            }
            
            
            if (paneSubmenuTuongtactruyen.Visible)
            {
                paneSubmenuTuongtactruyen.Visible = false;
            }
            if (panelSubmenuThemAlbum.Visible)
            {
                panelSubmenuThemAlbum.Visible = false;
            }
            if (panelSubmenuQLTK.Visible)
            {
                panelSubmenuQLTK.Visible = false;
            }*/
            
            
            

        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                //hideSubMenu(subMenu);
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private static readonly string _from = "nguoiduathuH3A1@gmail.com"; // Email của Sender (của bạn)
        private static readonly string _pass = "wgvohibzrfwgshtf"; // Mật khẩu Email của Sender (của bạn)
        private void btnXacnhan_Click(object sender, EventArgs e)
        {
            string ID = user.User.Info.Uid;
            DialogResult result;
            if (rtbChitiet.Text == "")
                MessageBox.Show("Vui lòng nhập lỗi chi tiết!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (cbBoxchude.Text == "    Chọn chủ đề" && richTextBox1.Text == "Khác...")
                MessageBox.Show("Vui lòng chọn lỗi gặp phải!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            result = MessageBox.Show("Bạn xác nhận gửi tin nhắn báo lỗi cho quản trị viên sao?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(_from);
                    mail.To.Add("22520415@gm.uit.edu.vn");
                    if (cbBoxchude.Text != "    Chọn chủ đề" && richTextBox1.Text != "Khác..." && richTextBox1.Text != "")
                        mail.Subject = $"{cbBoxchude.Text} - {richTextBox1.Text}";
                    else if (cbBoxchude.Text != "    Chọn chủ đề" && richTextBox1.Text == "Khác..." && richTextBox1.Text == "")
                        mail.Subject = $"{cbBoxchude.Text}";
                    else if (cbBoxchude.Text == "    Chọn chủ đề" && richTextBox1.Text != "Khác..." && richTextBox1.Text != "")
                        mail.Subject = $"{richTextBox1.Text}";
                    mail.IsBodyHtml = true;
                    mail.Body = $"<div> ID người đọc: {ID} </div> <br> Lỗi gặp phải: {rtbChitiet.Text}";

                    mail.Priority = MailPriority.High;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(_from, _pass);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    MessageBox.Show("Đã gửi tin nhắn đến quản trị viên.\n\r Vui lòng đợi phản hồi.", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.ToString()}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }    
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Bạn muốn xóa nội dung tin nhắn báo lỗi sao?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                rtbChitiet.Text = "Vui lòng chia sẻ chi tiết nhất có thể...";
                richTextBox1.Text = "Khác...";
                cbBoxchude.Text = "    Chọn chủ đề";
            }
        }

        private void ibtnQuanlyTK_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuQLTK);
            if (panelSubmenuQLTK.Visible)
                ibtnQuanlyTK.IconChar = IconChar.AngleUp;
            else
                ibtnQuanlyTK.IconChar= IconChar.AngleDown;
        }

        private void ibtnDangnhap_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuDangnhap);
            if ( panelSubmenuDangnhap.Visible)
                ibtnDangnhap.IconChar = IconChar.AngleUp;
            else
                ibtnDangnhap.IconChar= IconChar.AngleDown;
        }

        private void ibtnTennguoidung_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuThaydoi);
            if (panelSubmenuThaydoi.Visible)
                ibtnTennguoidung.IconChar = IconChar.AngleUp;
            else
                ibtnTennguoidung.IconChar= IconChar.AngleDown;
        }

        private void ibtnTuongtactruyen_Click(object sender, EventArgs e)
        {
            showSubMenu(paneSubmenuTuongtactruyen);
            if (paneSubmenuTuongtactruyen.Visible)
                ibtnTuongtactruyen.IconChar = IconChar.AngleUp;
            else
                ibtnTuongtactruyen.IconChar= IconChar.AngleDown;
        }

        private void ibtnTimkiemtruyen_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuTimkiem);
            if (panelSubmenuTimkiem.Visible)
                ibtnTimkiemtruyen.IconChar = IconChar.AngleUp;
            else
                ibtnTimkiemtruyen.IconChar = IconChar.AngleDown;
        }

        private void ibtnBinhluan_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuBinhluan);
            if (panelSubmenuBinhluan.Visible)
                ibtnBinhluan.IconChar = IconChar.AngleUp;
            else
                ibtnBinhluan.IconChar = IconChar.AngleDown;
        }

        private void ibtnDanhgia_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuDanhgia);
            if (panelSubmenuDanhgia.Visible)
                ibtnDanhgia.IconChar = IconChar.AngleUp;
            else
                ibtnDanhgia.IconChar = IconChar.AngleDown;
        }

        private void ibtnDecu_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuDecu);
            if (panelSubmenuDecu.Visible)
                ibtnDecu.IconChar = IconChar.AngleUp;
            else
                ibtnDecu.IconChar= IconChar.AngleDown;
        }

        private void ibtnThemAlbum_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuThemAlbum);
            if (panelSubmenuThemAlbum.Visible) 
                ibtnThemAlbum.IconChar = IconChar.AngleUp;
            else
                ibtnThemAlbum.IconChar = IconChar.AngleDown;
        }

        private void ibtnDanhdau_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuDanhdau);
            if (panelSubmenuDanhdau.Visible)
                ibtnDanhdau.IconChar = IconChar.AngleUp;
            else
                ibtnDanhdau.IconChar = IconChar.AngleDown;
        }

        private void ibtnLichsudoc_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuLichsudoc);
            if (panelSubmenuLichsudoc.Visible)
                ibtnLichsudoc.IconChar = IconChar.AngleUp;
            else
                ibtnLichsudoc.IconChar = IconChar.AngleDown;
        }

        private void ibtnChat_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuChat);
            if (panelSubmenuChat.Visible)
                ibtnChat.IconChar = IconChar.AngleUp;
            else
                ibtnChat.IconChar = IconChar.AngleDown;
        }

        private void ibtnSudungChat_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuSudungchat);
            if (panelSubmenuSudungchat.Visible)
                ibtnSudungChat.IconChar = IconChar.AngleUp;
            else
                ibtnSudungChat.IconChar = IconChar.AngleDown;
        }

        private void ibtnBaocao_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuBaocaosuco);
            if (panelSubmenuBaocaosuco.Visible)
                ibtnBaocao.IconChar = IconChar.AngleUp;
            else
                ibtnBaocao.IconChar = IconChar.AngleDown;
        }

        private void ibtnBaocaosuco_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubmenuHDBaocao);
            if (panelSubmenuHDBaocao.Visible)
                ibtnBaocaosuco.IconChar = IconChar.AngleUp;
            else
                ibtnBaocaosuco.IconChar = IconChar.AngleDown;
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (richTextBox1.Text == "Khác...")
                richTextBox1.Text = "";
        }

        private void rtbChitiet_MouseClick(object sender, MouseEventArgs e)
        {
            if (rtbChitiet.Text == "Vui lòng chia sẻ chi tiết nhất có thể...")
                rtbChitiet.Text = "";
        }
        private void rtbChitiet_Leave(object sender, EventArgs e)
        {
            if (rtbChitiet.Text == "")
                rtbChitiet.Text = "Vui lòng chia sẻ chi tiết nhất có thể...";
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
                richTextBox1.Text = "Khác...";
        }

        private void Hotro_baoloi_Load(object sender, EventArgs e)
        {
            /*rtbChitiet.Location = new System.Drawing.Point(
                75, lbChitiet.Location.Y + lbChitiet.Height + 3
               );*/
            Font font = new Font("League Spartan", 12, FontStyle.Regular);
            lbCaithien.Font = new Font("League Spartan Medium", 12, FontStyle.Bold);
            rtbChitiet.Font = font;
            cbBoxchude.Font = font;
            richTextBox1.Font = font;
                

        }

        private void Hotro_baoloi_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Normal)
            {
                cbBoxchude.Font = new Font("League Spartan", 20, FontStyle.Regular);
                rtbChitiet.Font = new Font("League Spartan", 12, FontStyle.Regular);
                richTextBox1.Font = new Font("League Spartan", 12, FontStyle.Regular);
                cbBoxchude.Height = 75;
                rtbChitiet.Height = 250;
                richTextBox1.Height = 75;

            }    
        }
    }
}
