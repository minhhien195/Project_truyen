using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth.Providers;
using Firebase.Auth;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;

namespace Login
{
    public partial class Forget_Password : Form
    {
        public Forget_Password()
        {
            InitializeComponent();
        }

        private const int AW_BLEND = 0x00080000;
        private const int AW_HIDE = 0x00010000;
        private const int AW_ACTIVATE = 0x00020000;

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hWnd, int time, int flags);

        private void FadeIn(Form form, int duration = 500)
        {
            AnimateWindow(form.Handle, duration, AW_BLEND | AW_ACTIVATE);
        }

        private void FadeOut(Form form, int duration = 500)
        {
            AnimateWindow(form.Handle, duration, AW_BLEND | AW_HIDE);
        }

        private void Forget_Password_Load(object sender, EventArgs e)
        {
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;
            //Bo góc panel
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath path1 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath path2 = new System.Drawing.Drawing2D.GraphicsPath();
            int cornerRadius = 20; // Điều chỉnh giá trị này để thay đổi bán kính bo góc

            // Thêm hình dạng bo góc vào GraphicsPath
            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(panel1.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(panel1.Width - cornerRadius, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(0, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();

            path1.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path1.AddArc(textBox1.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path1.AddArc(textBox1.Width - cornerRadius, textBox1.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path1.AddArc(0, textBox1.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path1.CloseAllFigures();

            panel1.Region = new Region(path);
            textBox1.Region = new Region(path1);

            //Căn giữa panel
            panel1.Location = new Point(
                (this.Width - panel1.Width) / 2, // Tính toán vị trí theo trục X
                (this.Height - panel1.Height) / 2 // Tính toán vị trí theo trục Y
            );
            panel1.Anchor = AnchorStyles.None; // Đảm bảo label không bị ràng buộc bởi các thuộc tính Anchor

            label1.Location = new Point(
                (panel1.Width - label1.Width) / 2, // Tính toán vị trí theo trục X
                13
            );
            label1.Anchor = AnchorStyles.None; // Đảm bảo label không bị ràng buộc bởi các thuộc tính Anchor
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyD4vuUbOi3UxFUXfsmJ1kczNioKwmKaynA",
                AuthDomain = "healtruyen.firebaseapp.com",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                new EmailProvider()
                }
            };

            var client = new FirebaseAuthClient(config);

            try
            {
                string email = textBox1.Text;
                await client.ResetEmailPasswordAsync(email);
                DialogResult dr = MessageBox.Show("Reset Password Succeeded, please check your email to complete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (dr == DialogResult.OK)
                {
                    this.Hide();
                    Login login = new Login();
                    login.Show();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Reset Password Failed, please try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void opacityTimer_Tick(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var r = new System.Text.RegularExpressions.Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            if (r.IsMatch(textBox1.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
            else
            {
                ptrWarning.Visible = true;
                lbemailKhonghople.Visible = true;
            }
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
