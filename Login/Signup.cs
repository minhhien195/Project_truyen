using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Login
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }
        private void Signup_Load(object sender, EventArgs e)
        {
            

            // xóa underline
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            //Bo góc panel
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            //Bo góc Tên đăng nhập
            System.Drawing.Drawing2D.GraphicsPath path1 = new System.Drawing.Drawing2D.GraphicsPath();
            //Bo góc MK
            System.Drawing.Drawing2D.GraphicsPath path2 = new System.Drawing.Drawing2D.GraphicsPath();
            //Bo góc XNMK
            System.Drawing.Drawing2D.GraphicsPath path3 = new System.Drawing.Drawing2D.GraphicsPath();
            //Bo góc Email
            System.Drawing.Drawing2D.GraphicsPath path4 = new System.Drawing.Drawing2D.GraphicsPath();
            //Bo góc Đăng ký
            System.Drawing.Drawing2D.GraphicsPath path5 = new System.Drawing.Drawing2D.GraphicsPath();
            int cornerRadius = 20; // Điều chỉnh giá trị này để thay đổi bán kính bo góc

            // Thêm hình dạng bo góc vào GraphicsPath
            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(panel1.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(panel1.Width - cornerRadius, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(0, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();

            int cornerRadius1 = 8;
            // Bo góc Tên đăng nhập
            path1.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
            path1.AddArc(txtTenDN.Width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
            path1.AddArc(txtTenDN.Width - cornerRadius1, txtTenDN.Height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
            path1.AddArc(0, txtTenDN.Height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
            path1.CloseAllFigures();

            // Bo góc mật khẩu
            path2.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
            path2.AddArc(txtMK.Width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
            path2.AddArc(txtMK.Width - cornerRadius1, txtMK.Height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
            path2.AddArc(0, txtMK.Height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
            path2.CloseAllFigures();

            //Bo góc xác nhận mật khẩu
            path3.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
            path3.AddArc(txtXNMK.Width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
            path3.AddArc(txtXNMK.Width - cornerRadius1, txtXNMK.Height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
            path3.AddArc(0, txtXNMK.Height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
            path3.CloseAllFigures();

            //Bo góc email
            path4.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
            path4.AddArc(txtEmail.Width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
            path4.AddArc(txtEmail.Width - cornerRadius1, txtEmail.Height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
            path4.AddArc(0, txtEmail.Height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
            path4.CloseAllFigures();

            //Bo góc nút Đăng ký
            path5.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path5.AddArc(btnDKy.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path5.AddArc(btnDKy.Width - cornerRadius, btnDKy.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path5.AddArc(0, btnDKy.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path5.CloseAllFigures();

            // Thiết lập Region của Panel bằng GraphicsPath
            panel1.Region = new Region(path);
            txtTenDN.Region = new Region(path1);
            txtMK.Region = new Region(path2);
            txtXNMK.Region = new Region(path3);
            txtEmail.Region = new Region(path4);
            btnDKy.Region = new Region(path5);

            //Căn giữa panel
            panel1.Location = new Point(
                (this.Width - panel1.Width) / 2, // Tính toán vị trí theo trục X
                (this.Height - panel1.Height) / 2 // Tính toán vị trí theo trục Y
            );
            panel1.Anchor = AnchorStyles.None; // Đảm bảo label không bị ràng buộc bởi các thuộc tính Anchor

            

            //Căn giữa signup label
            Sign_up_label.Location = new Point(
                (panel1.Width - Sign_up_label.Width) / 2, // Tính toán vị trí theo trục X
                13
            );
            Sign_up_label.Anchor = AnchorStyles.None; // Đảm bảo label không bị ràng buộc bởi các thuộc tính Anchor
        }
<<<<<<< Updated upstream
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private bool isTicked = false;
        private bool isNullDN = false, isNullMK = false, isNULLXNMK = false, isNULLEmail = false, lengthMK = false;
        private void btnTick_Click(object sender, EventArgs e)
        {
            isTicked = !isTicked; // Đảo ngược trạng thái đánh dấu

            if (isTicked)
            {
                btnTick.Image = Properties.Resources.checkmark; // Đặt hình ảnh khi đánh dấu
            }
            else
            {
                btnTick.Image = Properties.Resources.Untick; // Đặt hình ảnh khi không đánh dấu
            }
        }
        private string RemoveDiacritics(string text)
        {
            string normalizedText = text.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            foreach (char c in normalizedText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
        private void txtTenDN_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtTenDN.Text;

            // Chuyển đổi văn bản thành mã ký tự không dấu
            string normalizedText = RemoveDiacritics(inputText);

            // Tắt UTF-8 và cập nhật văn bản trong TextBox
            byte[] asciiBytes = Encoding.ASCII.GetBytes(normalizedText);
            string asciiText = Encoding.ASCII.GetString(asciiBytes);
            txtTenDN.Text = asciiText;

            // Đặt con trỏ văn bản lại vị trí cuối cùng
            txtTenDN.SelectionStart = txtTenDN.Text.Length;

        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtMK.Text;

            // Chuyển đổi văn bản thành mã ký tự không dấu
            string normalizedText = RemoveDiacritics(inputText);

            // Tắt UTF-8 và cập nhật văn bản trong TextBox
            byte[] asciiBytes = Encoding.ASCII.GetBytes(normalizedText);
            string asciiText = Encoding.ASCII.GetString(asciiBytes);
            txtMK.Text = asciiText;

            // Đặt con trỏ văn bản lại vị trí cuối cùng
            txtMK.SelectionStart = txtMK.Text.Length;
        }

        private void txtXNMK_TextChanged(object sender, EventArgs e)
        {

            string inputText = txtXNMK.Text;

            // Chuyển đổi văn bản thành mã ký tự không dấu
            string normalizedText = RemoveDiacritics(inputText);

            // Tắt UTF-8 và cập nhật văn bản trong TextBox
            byte[] asciiBytes = Encoding.ASCII.GetBytes(normalizedText);
            string asciiText = Encoding.ASCII.GetString(asciiBytes);
            txtXNMK.Text = asciiText;

            // Đặt con trỏ văn bản lại vị trí cuối cùng
            txtXNMK.SelectionStart = txtXNMK.Text.Length;
            if (txtXNMK.Text != txtMK.Text)
            {
                ptrNotsame.Visible = true;
                lbNotsame.Visible = true;
            }    
            else
            {
                ptrNotsame.Visible= false;
                lbNotsame.Visible= false;
            }    

        }
        private async Task KT_email(FirebaseAuthClient client)
        {
            string email = txtEmail.Text;
            var result = await client.FetchSignInMethodsForEmailAsync(email);
            if (result.UserExists)
            {
                ptrWarning.Visible = true;
                lbemail.Visible = true;
            }
            else
            {
                ptrWarning.Visible = false;
                lbemail.Visible = false;
            }
        }
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtEmail.Text;

            // Chuyển đổi văn bản thành mã ký tự không dấu
            string normalizedText = RemoveDiacritics(inputText);

            // Tắt UTF-8 và cập nhật văn bản trong TextBox
            byte[] asciiBytes = Encoding.ASCII.GetBytes(normalizedText);
            string asciiText = Encoding.ASCII.GetString(asciiBytes);
            txtEmail.Text = asciiText;

            // Đặt con trỏ văn bản lại vị trí cuối cùng
            txtEmail.SelectionStart = txtEmail.Text.Length;
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
            var r = new System.Text.RegularExpressions.Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            if (r.IsMatch(txtEmail.Text))
            {
                ptrWarning.Visible = false;
                //await KT_email(client);
                lbemailKhonghople.Visible = false;
                lbemail.Visible = false;
            }
            else
            {
                ptrWarning.Visible = true;
                lbemailKhonghople.Visible = true;
            }

        }


        
        
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
                lbemail.Visible = false;
            }
        }

        private void prtHide_Click(object sender, EventArgs e)
        {
            ptrView.BringToFront();
            if (txtMK.PasswordChar == '*')
            {
                txtMK.PasswordChar = '\0';
            }
        }

        private void ptrView_Click(object sender, EventArgs e)
        {
            prtHide.BringToFront();
            if (txtMK.PasswordChar == '\0')
            {
                txtMK.PasswordChar = '*';
            }
        }

        private void ptrHide1_Click(object sender, EventArgs e)
        {
            ptrView1.BringToFront();
            if (txtXNMK.PasswordChar == '*')
            {
                txtXNMK.PasswordChar = '\0';
            }
        }

        private void txtEmail_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
                lbemail.Visible = false;
            }
        }

        private void txtEmail_MouseMove(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                ptrWarning.Visible = false;
                lbemailKhonghople.Visible = false;
                lbemail.Visible = false;
            }
        }

        private void ptrView1_Click(object sender, EventArgs e)
        {
            ptrHide1.BringToFront();
            if (txtXNMK.PasswordChar == '\0')
            {
                txtXNMK.PasswordChar = '*';
            }
        }


        public class Nguoi_dung
        {
            public string TK_dangnhap { get; set; }
            public string Email { get; set; }
            public int Vaitro { get; set; }
            public string ID_Nguoidung { get; set; }
        }
        
        private async void btnDKy_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtTenDN.Text))
            {
                isNullDN = true;
            }    
            else
            {
                isNullDN = false;
            }
            if (string.IsNullOrEmpty(txtMK.Text))
            {
                isNullMK = true;
            }
            else
            {
                isNullMK = false;
            }
            if (string.IsNullOrEmpty(txtXNMK.Text))
            {
                isNULLXNMK = true;
            }
            else
            {
                isNULLXNMK = false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                isNULLEmail = true;
            }
            else
            {
                isNULLEmail = false;
            }
            
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
                IFirebaseConfig _firebaseConfig = new FirebaseConfig
                {
                    AuthSecret = "38QvLmnKMHlQtJ9yZzCqqWytxeXimwt06ZnFfSc2",
                    BasePath = "https://healtruyen-default-rtdb.asia-southeast1.firebasedatabase.app/"
                };
                IFirebaseClient client1 = new FireSharp.FirebaseClient(_firebaseConfig);
                var path = $"Nguoi_dung/";
                if (txtMK.Text.Length < 6)
                {
                    lengthMK = true;

                }
                else
                {
                    lengthMK = false;
                }
                if (ptrWarning.Visible == false && ptrNotsame.Visible == false && lbNotsame.Visible == false && isTicked == true
                    && isNullDN == false && isNullMK == false && isNULLXNMK == false && isNULLEmail == false && lengthMK == false)
                {

                    await client.CreateUserWithEmailAndPasswordAsync(txtEmail.Text, txtMK.Text, txtTenDN.Text);
                    var uid = client.User.Uid;
                    Nguoi_dung nguoi_Dung = new Nguoi_dung()
                    {
                        TK_dangnhap = txtTenDN.Text,
                        Email = txtEmail.Text,
                        Vaitro = 0,
                        ID_Nguoidung = uid
                    };
                    SetResponse res = await client1.SetAsync(path + uid, nguoi_Dung);
                    this.Hide();
                    Login login = new Login();
                    login.Show();
                    //this.Close();
                }
                else if (lengthMK == true)
                {
                    MessageBox.Show("Mật khẩu phải lớn hơn 5 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Bạn chưa thể đăng ký", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FirebaseAuthException ex)
            {
                if (ex.Reason.ToString() == "EmailExists")
                {
                    ptrWarning.Visible = true;
                    lbemail.Visible = true;
                }
            }
               
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.ActiveLinkColor = Color.Red;
            this.Hide();
            Login login = new Login();
            login.Show();
            //this.Close();
        }
=======

>>>>>>> Stashed changes
    }
}
