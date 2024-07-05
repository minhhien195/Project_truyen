﻿using System;
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
            if (lbMKlonhon5kytu.Visible == true)
            {
                lbMKlonhon5kytu.Visible = false;
                ptrGreater.Visible = false;
            }    
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
            public string Anh_dai_dien { get; set; }
            public bool disable {  get; set; }
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
                else if (txtMK.Text.Length >= 6 && txtXNMK.Text == txtMK.Text)
                {
                    lengthMK = false;
                    ptrNotsame.Visible = false;
                    lbNotsame.Visible = false;
                }
                else if (txtMK.Text.Length >= 6 && txtXNMK.Text != txtMK.Text)
                {
                    lengthMK = true;
                    lbMKlonhon5kytu.Visible = false;
                    ptrGreater.Visible = false;
                    lbNotsame.Visible = true;
                    ptrNotsame.Visible = true;
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
                        ID_Nguoidung = uid,
                        Anh_dai_dien = "iVBORw0KGgoAAAANSUhEUgAAAuAAAALgCAIAAABavFVeAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxIAAAsSAdLdfvwAAGA7SURBVHhe7d3netvIsqjhc/+3tvdIpLQkRuWcc7ZnnU+oVm+YlGck2yKb4Pf+4NOEIoGq6kIg+P/+I0mSVBgbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEmSVBwbFEn/YuVHq6ury5/Ej6QffpN+tST9hA2KpE9LXcaHpR+TpA+zQZEkScWxQZH0U6uVdBikkr7wS9KvqMRvTl+QpDE2KJI+LXUZH5Z+TJI+zAZF0k8tvUkXuy4vpy/8kvQrlpfTL11aSl+QpDE2KJJ+KvqJGK9U52U6lThB83H5p/LRlPpvlqRxNiiSfio3Jd1ut9fr9fv9QWX9k+Kn+HF+Cb9qpFmRpHE2KNJsiF4hxNQeByGwtLTUbrfjjAlfZfpnYfVDr+p9QH0cPxj4cZ7ys/QQ9BPb29t7e3v7+/tnZ2fn5+eXl5c3Nzf39/ePj4/Pz88vLy///SR+hB/kx/kl/Cp+Ib+WX86f4A/x5/ij/Gn+Af6NeC3hgy8kOh7G/Ej8OAsDC/lmvhrqPyipZDYo0gyoT65MutXc/YqJOebm/KXQ7XZjIWO+LRbyDa1Wi/mbAd8wHA43NzdpDnZ3dy8uLmgarq+v7+7uHh4eaCa+ffv2vebvSmo3fkP8nvRLK/wh/hx/lD/NP8C/wT/Dv8Q/xr/HP8m/yj/Mv80/n19jvKjoPPiGWAi+IRbGmqlW0qv6l8BYUuFsUKQZwJyaW5A83YKFedKNcXxPfBuTOhiwvNfrDQaDjY2Nra2tvb294+NjWgF6gqenp88eEfmFTuWzP8K/xD/Gv8c/yb/KP8y/zT/PS+CF8HLqry5e78/WRqyQvNLiG2K5pJLZoEgzJqbYOEJQn4NHsJBvYEZnaj84ODg9Pb24uGDWz0dHUi9QiYMZ4/jOkJ6/ST/2YenH3qRfOvZrs/RjFZ7ynfzb/PO8BF4IL4cXxUvjBf7stYOVU60kr3eRZpINijQD8gGA14MDtaMp9XF8tdvt9vv93d1dZvGzs7Obm5vHx0cm+DTbj6lOufz08Ea0C/jsIZB/wK9Kv/TnjU71T/30L/JyeFG8NF4gL5MXy0uOs1p5VbBaeFof56+yMhlIKpwNijQDYqLN4gRHq9WKxoWpN46UHB4enp+fX19fM3+/vLyMz/Ex8Y9LXy5G+rfGpC+/YQkvkxfLS+aF8/LjyAorJBqRuOZmpCMZWZmSymSDIs2AlQoza2Dcqd76Gxe67u/vMz3f3d39w5GS79WJEh5/NtPXVUc3XqUvV2I5v+Qf/srPxE/x4/yStKhS/ZFX1Z/9P+nLb2Ih3xa/JC0dw1dZCawKVki+wJYVNbLqkFarpILZoEgzIE5PLFWYX/v9/lZ1rSvz8ePj48ic/Q9T+B8x3kD8q1/4kU8ZXwOsFlZOXF3L6mKlxdrLK1NS4WxQpCmIPiP273nKnj2PMXG+7uC/XVYSEypPY9e/1+ttbm4eHh5eX18/Pz9/9azfAKwiVhSri5XGqot3AMXKZMWyemPN8zSWxCaIhXGMiuWxFSRNmA2KNAX5iEi+QoIlzIitVovZMaZG5H39nZ0dptjLy8uHh4d8tMAG5V/lVcRKY9WxAlmNrMxYq6zeWM+scFY7K5/Bu5um+nZJE2WDIk1BnhGZBdlH5ymPLIn9eAZgXhwOh3t7eycnJ7e3t4+PjyMdybfPXwsyb0ZWESuQ1cjKZJWyYlm9rORY26z2vAlic8RlyCzhabXRJE2UDYo0HTEpMkHGXjs76wsLC8yLsaTf729vb5+dnT0/P6fZtZpf8b0mfUE/kVZTJdZe+sJ//8uKZfWyklnVsQlY+WwCNkTeKHkzSZo8GxRpCmJPHYzrZxOYFzc2Nti/v7+/Hz9AwvzKQjDdpkX6AFZXrLd6gxJYyKpmhbPaWfkjmyO2kT2KNBU2KNIUsHceO+jMhYuLiwzW1tbiM3FG3i0cTcnrvr9XnPwJsSZHmhWestrjM4DYEGwONgqbpr6lJE2YDYo0BbFrjm63u76+vr+/f3l5ya58mjCrnf6Xl5fcqcS4Pqcy9jjKv4ozO+nJW7fHyoynMa6vRjYBG4LNwUZh06SNVB3okjRhNijSFMTeeb/f39vbu76+zlPmSM9Rn1xD9CXjy/UPfrbSRpbklc/mYKOwadhAeWNJmjAbFOkPWH7D3nY+KcDTeMzXl4Cni4uLvV6P3fS7u7uYOEeOjmiK8uZg07CB2ExsLDYZGy62IJuSDZo3Lo+xxdn0PA0slPSbbFCkPyDOAqTZqcIcFn1JTF3shS8sLLB8Y2Pj6Ogo7miSpkQVLO6ewiZjw7H52IhsymhD2bixlasNnuRgkPSbbFCkPyC6EAZpmqphOWLAJMdsN3KwJHbWPYJSiHc3B0/ZcGy+vCljMIIAYHn0LpJ+kw2K9GfEpIX0vFoSu9edTmdra+v8/Pzp6SnPfAziIs04oZCXa7piW9Q3TV7O5mMjsinZoGxWNu7I5g7puaTfY4Mi/QFMV8xM7DrH3nN14P+1Nel2u2trawcHB3d3dzHPgWmPR3bTGfAYC1Wa+gaKTRbYlGxQNisbN9oUsNFj6xMGLKyCQtJvsUGR/oxoUHiMGYsBExgz2dXVVe5C8oRX7aW/iuVh5Kkmb3yLBMbRr8RyxmxWNm68FTlv8QiAFBCSfo8NivRnMDMh9p77/f7u7u7p6Wn9s/0YPD8/x1Me09RXXfEAJr88/2laYivEFkmbp9pAfInHvPniKRuXTcyGjncjx1E0VOEg6XfZoEh/AJNTxl713t5ePqfDDBdzXjxFfYyYBdMTFWB8i4xsPjZo/gY2NJs7TvdkKSwk/QYbFOkTVms3OIndZTCOg/x8dXt7+/r6eqQFUbOxudnobHoCICJhJDwiYCJ4JH2QDYr0CTEDtVotHjudTq/XYwZqt9uM19fXj46Obm9vc3cysheu5smbmI3OpicACAOCIe6VQngwzgFjgyJ9ig2K9GnVUfx0EIVZB7u7u+fn5/mO9bA7mRP1DU0AEAYEQ0RFPnwS0SLpU2xQpE9gV5iJJw6cxAferq2tsd98d3f3/PwcsxQ70/VORY3H5s6HzQgDgoGQIDAID4IkDqUQNgRPCiNJH2CDIn1CvrwgHjc2Ni4uLnI7wixVvQvEu5vMl/HtTkgQGHHn2RwwETySPsgGRfoE9oNjt7jb7bKX/Pj4GBMS/q6kJxXblMYb2cQjMUB4ECSEShxsI3hSGEn6ABsU6ROYYzqdznA4PD4+zud0wsjkBHap00gNNbKJx2OAICFUCBjCxgZF+hQbFOkTlpaWNjc3r6+v8zzEPnTejWZhPEUs0TxIm7z2EYPxNMYsJGAIG0/xSJ9igyK9I94purCwsFx91N/K2y1iDw8P821OmHg8RqJ/QHhEy0LAEDYEDyFEIBFOBBUDAowwY1wFnaQf2KBI72MWiWPyrcra2trp6en9/X1uSuJ4Sd5plupGwoOwIXgIIQIpIorQIsDi+llJ42xQpHdEdxINytLSUr/fPzs7i5kmixkoPZHGvNu/EkiEU5zuiRizR5HeZYMivaPzdgNQ5o/d3d3r6+v62Rwmnnz0XvoHBAmhUm9keUo4xc3cCDDCzFM80rtsUKR3MG3E+0KZSO7v72NqYbKJ+WZkyvE4ikaMhEfETMRPLCSookchzOJoiqQRNijSOxYXFweDwfHxMRNJnlTyoI6FzD3piVSJdiQ9qanHEqFFgBFmBFsKO0k1NijSO5g2Tk5ORu50Mo5pJqTnUiWFxb8FBgFGmBFsKewk1digSO84Ozt7eHgYmWBeXl7YM/5eXfkYx+09uaN/kIMkB8zIhzSxnDAj2FLYSaqxQdFc63a7cRFAXKjYarWGw+HFxUWaQKSJIOQIvHjvcVygTVgSnFWQSnPKBkVzLa5PXK3eTry8vBxvJ/7XI/PSn0XIxduP436AEZBePKs5Z4OiucZ8wGSwsrLCZDAYDM7Pz+1ONBUEHuFHEBKKcRueaFakuWWDorkWZ3aYCaI7ianCK0s0YTnkokeJ1sT7o2jO2aBorrG3ymQwct3JN982rMmqh1xcj0JYeopHc84GRXNtYWGBHdarq6uYG+J9OjGWJqn+Hh8CkrAkOFOYSnPJBkVzbX19PZ/Zgd2JpqgefoQlwZnCVJpLNiiaa3d3dzEf1I+djNysQvpqOeTqx1EIzhSm0lyyQdFciPfpdDqdQXXXzoWFheFweHt7GzOBVCZClECNcz2EbtwixU8/1pywQdFcWK6kJ//5D0X/9PTUIyUqHCFKoBKuKXDHIllqMBsUzYs4iLK4uNjr9c7OzrzcRDOBQCVcCVpC18Mnmis2KJoLcTc2dj273e7BwcHT01Mq/1LxCFeCltAlgAnjuM+s1Hg2KJoX7H1S4g8PDx8fH71drGYI4UrQEroEMGGcAlpqOhsUzYV2u82u587OzsPDQy76MZBKlgOV0CWACWOCOYW11Gg2KJoXFPe7uzv7Es0oQpcAJoxTQEtNZ4OiudDr9W5ubqLQPz8/R5viZ+6ocBGihCtBG0sIY4I5hbXUaDYoaprqbZiv1xLGeHV1lYL++PiY+5LMoykq3HjEEsYEMyGdP+44rv6OsdQkNihqmngrZrfbZdxqtfr9/snJydPT08vLiw2KZst4xBLGBDMhTWAT3gQ5oU7Ae/GsmscGRU3DriQ7l/GeTKr23t4eZf3bt2/fv3+3QdFsGY9YwphgZkBgE94EOaGej6ZITWKDokbJZ3ZA1d7a2hq5nz2VPaTnUvFSyP4YtAQ24R2tCXLwS41hg6JGoV5Tptvt9uLi4traWnwWYL2yR6FHei4VL4Xsj2HMI+FNkBPq8S56gj+lgdQINihqlGhQlqrPBTw5OYlqPnLf2Hqhl2bCSNDmkCbI8ycI2qCoYWxQ1CjRnXS73aOjo3hnJpXdj91Rw8RlKAwIckKdgI8eJaWB1Ag2KGoUavTi4uL6+np0J/GeTAYeNVFjRDDHO+cZ8EjAE/Y2KGoYGxTNJGpxYLxcvVuHMfuRf/3118bGxu3tbbxnx75EDRYRTqgT8IQ9wU8KkAikQ75sNlRJI80YGxTNJErwaqV6B8PrG3bAwl6vd3R0FGfoo3zHoKrnUkPkwI4BAU/YE/w/y4uUNtJMsUHRTGq321TeTqcThZgBT1l4fHwc79wJuY7HU6kZxgObsCf4f5YXKW2kmWKDopkUO4UUX1CIY8dxOBze39/nqm1fosarRzvBTwqQCKQDSVElx+v7ejyCohllg6KZFFWYQa/Xoxb/9ddf/X7/4uKi/oad+KA1qcHqQU7wkwIkAulAUpAaJEj07lXSSDPGBkUzKe79EAexeWy327u7uxTrXK8ZeChFTVWP7XrMg0QgHXJqkCYMUtpIM8UGRbMq3psQtXhzc/P6+jpqdC7WMUB9LDXAu+EdAxKBdIiuPaeJNItsUDSTWq1WvKPyf/7nf3h6dnZGdca7R01eXl7SSGqEekiPHE0B6UBSkBokCGkSH3oszRwbFM0kyi6P7CN2Op39/f2Hh4eo0ZJIB5KC1IiDKJEs0syxQdFMimtjl5aW1tbWRq6NleZcXC1LasR1WnG1rDRzbFA0k2LvcHV19ejoKB/urp/WkeZQTgGSgtQgQeIoY0obaabYoGgmsV+IuKt9VGS7Ewk5EeL+95EpKW2kmWKDopnEfmGv1zs9Pf1evXOBR8/ySCARclKQIKRJXIkizRwbFM2q3d3d+KRivLy82KBIIBHySU8ShDRJCSPNGhsUzaThcHhxcRFVmD3FOKztWR7NuZwIcRAFpAnJktJGmik2KJpJ7BfmtxbnYyceRNGcG88F0sSDKJpRNigq2nL1XuK4yi/GDLrd7vPz88vLy8ghE4+gaM6NZwRpQrLErVBGUinGUrFsUFS0lZWVKKMxaLfblNrt7W1qLvuINihS3XhGkCYkCylD4pA+JFHc/D4PpGLZoKho+RYO8fHFVNiNjY2rqyv2C/OlJ5kNiubceEaQJiQLKUPikD4kUf5wY++PosLZoKho1NA4ghINCoODg4Mou+MNiqS6eqaQOKRPblBIKxsUFc4GRUWLMzvRoyC/eYeCi6jCkn4mZ0r97TyRU57iUeFsUDQDKKbs+XW73cPDw3zvE0kfR+KQPiQRqURCpdSSCmaDoqJRSeNAdLvdXl9fv7u7o9R67ET6lEgZ0ockIpVIqHzyVCqWDYqKRg3t9XorKytU1e3t7ai2+UaZkj4ipwxJRCqRUPF54CnNpCLZoKhoi4uLcQRlOBze399HqfUIivRZ0aPc3d31+31ak3jXcWSZVCYbFBUtH47e3d19enqiwr5e8meDIn1SZE18Ok80/fG2OKlYNigqXavVWltbu7q68k720m8iia6vr0ko0spTPCqcDYqKtrKysri4yD7f+H1jJX1QPXe+f/9OQpFWNigqnA2KZsDp6SmFlSJLbY1BVWklfchI4pBQpJUNigpng6KiraysbG5uxruLEXVW0qdEa5IbFBKKtEo5JpXKBkVFo0E5Pz+vHzLx8In0C0aSiLTKH8ojlckGRUXrdDpx69h87MSDKNIvyA1KXGxOWnW73ZRmUpFsUFSElZUV9ud4XK4+IoTSyWOr1To+Po4GhfLqHVCk31FPItKK5CLFcrpF6kUaprSUpsoGRUXINZFBfVx/d3Ee2KZIn5JTpp5NJFc91+rjGEjTZYOiIlAT4z0FcQup2LFbX19/fn6OeopcZD3LI31KTpl6c09ykWIkGumWUy+OozCQps4GRUWgJsZ9LbvdLiVyYWGBcnl4eJjrab2w2qBIn1JPmXpOkWIkGulG0sUlKaShDYoKYYOiIow0KIuLi2tra9fX11FJKa/fajdqs0GRPqV+BIVUyk/jrrJx0zYbFJXGBkWliGPL0anwuL+///z8TCVFlNT6nl8MJH1EPXdyQoEUI9Fy0oE0TAkpTZsNiooQ3cnq6iqFst1uDwaD8/Nz6ik1lJJaL68hnkr6iJQ2P+YRycWYRCPdSDpSL7+TLqWlNFU2KCoCNbHT6VAfW60WtXJjY+P29jYqaVVRE0pq/VyPpI8gZepndkLkEYlGupF0pB4JSBraoKgQNigqAvtt8c4dsCd3cHBA6Rypp5L+rEgx0i2f4ok0TGkpTZUNiooQJ3fiCDOPZ2dn1E2PlEhfKlKMdMupF2mY0lKaKhsUFSHqIxgPBoP4dECPoEhfKlKMdCPpSL3IQRsUFcIGRaXIZXF7e/tbdb9LGxTpS0WKkW4kXWSf3YnKYYOiIsQp8Di8fHJyUq+ekr5ITjGSjtTLaZjSUpoqGxQVIWoij8PhMM7veAGKNAGRaCQdqZfTsEpKacpsUFSEeGcjj7u7u1Ex65/CI+mLRKKRdKReTsMqKaUps0FRQVZWVo6Pj6NuPj4+xkDS18mJRuqRgCkVpQLYoKgIq6urCwsLa2trDw8P1EqvPpEmJtKN1CMBSUOvk1UhbFBUBHbdWq3W5ubm09NTrpiSJiDSjdQjAb1Rm8phg6JSLC8v7+3tvby8UCvjMhSvk5W+VD3RSD0S0AtQVA4bFBWBstjpdPIbjG1QpAkYSTQS0M/iUTlsUFQEauJwOLy+vo5CGRXTEz3Sl4oUyw0KCUga2qCoEDYoKsXW1la8oYByGRUz101JXyEnWgxIQNIwJaQ0bTYoKsXe3l7c4X78c+ElfR3SLaceaZgSUpo2GxQVYWVl5eDgIHbjXl5ebFCkiSHd8sXppKHv4lEhbFA0actv8tN2u93tdm9vb6NWRpsiaWJIutgrIA1JRlKynqEhnkoTY4OiSUvVrlb+4iN47u/vqY+5QbFNkSYgp1s0KKRhfChPPUNDPJUmxgZFEzVe6WJJvkLWBkWapJEGJa6TjaxMKVoZXyJ9NRsUTVS9xuWSt7Kycnh4GGfBc4OSB5K+SO5L8oA0JBnjMpScoaE+libABkUTFTWuXv4Ydzqd8/PzqI/xiG/fvtmgSF+KFIv37yAnIMlISpKYkaH1hOVRmhgbFE1Uvd7Fee7V1dVerxdXyGKkXEr6UvVdghiQjKQkiUl6kqQ5YW1QNGE2KJooahzFLj4uldoHxoPBIH/muw2KNEnjDQrJSEqSmJGhpCrjOKBSJbE0ITYomijq3UiD0ul0qIbxIcb1A86e35EmICcaqRdjkpGUJDEjQ0nVaFBiLE2MDYomKle6aFMWFxcZHB0dRYmUVAJSksQkPUcSNqWxNBE2KJqokXrXarV4zB9iLKkEpGROTxsUTYsNiiZqpN5RAbvd7vn5eaqLkgpASpKYNiiaLhsUTRRlDvlS2Xa7PRgMrq+vU12UVABSksQkPaNBiYRFSmNpImxQNFFR5nKDwp7Z+vr6w8NDqouSCkBKkpikpw2KpsgGRZOW6x21j8HW1lbcQ1ZSIUjJuOF9vUFJCSxNig2KJq3eoPB0Z2cnFUVJxSAxSU8bFE2RDYomLSpdblB2d3dTRZRUDBKT9IwGJaetNEk2KJooyhx7Y+12u9fr8YiLiwvvySYVhZQkMSNDI1U9iKLJs0HRREWNo951u10el5aWLi8vbVCkopCSJCbpmVM1J680MTYomqiRBoXx9fW1DYpUFFKSxBxJVRsUTZgNiqaAehef9LG6unp7e2uDIhWFlCQxSU+SlFSNBkWaMBsUTRr7YblB6fV69/f3qShKKgaJSXrmBsXDJ5o8GxRNWjQo7JwtLy8PBoPHx8dUESUVg8QkPUlSUtUGRVNhg6JJqzcoa2trz8/PqSJKKgaJSXraoGiKbFA0aVS6peqzxxivr697G1mpQCQm6TmSsNIk2aBoouJuCt1ul0Gr1drZ2fEIilQgEpP0JElJVRKWtGWQ0liaCBsUTVS9QWm327u7uzYoUoFITNKTJLVB0bTYoGiiokHpdDrRoOzt7XmKRyoQiUl6RoNCwtqgaPJsUDQF0aAsLS3t7+/boEgFIjFJT5I0GpSUutIE2aBoCuIGUBS+g4ODb9++pYooqRgkJukZOxIkbEpdaYJsUDRRK9V7AXik6jE4PDz8/v17qoiSikFikp4kKama05ZHaWJsUDRRIw3K0dGR97mXCkRikp4kqQ2KpsUGRRNVb1B4PD4+tkGRCkRikp45VXPyShNjg6JJW67eyBOPHkGRyhRHUHKq8pgSWJoUGxRNmg2KVD4bFE2dDYomzQZFKp8NiqbOBkWTZoMilc8GRVNng6JJs0GRymeDoqmzQdGk2aBI5bNB0dTZoGjSbFCk8tmgaOpsUDRpNihS+WxQNHU2KJooyly3212qPoiHp3t7e1EKoyZKKkGkJOkZOUvCkrb2KJowGxRNFDWu0+nkBmV3dzdXQ0mFiJQkPSNnSdj4BPIqiaUJsUHRROUGJZ7u7OxEQZRUGtIz8tQGRVNhg6KJyg3KSvW5HltbW36asVQgEpP0JElJVRsUTYUNiiaKGre6upoblI2NjW/fvqWKKKkYJCbpSZJGg0La2qBowmxQNFG5QYl6t7a29vz8nCqipGKQmKTnSMKmNJYmwgZFE0WNqx8xHgwGT09PqSJKKgaJSXqSpPmcrA2KJswGRZM20qA8Pj6miiipGCTmSIOSEliaFBsUTVS73abkxd4YVY8KeHFxkSqipGKQmKQnSZoTluRNaSxNhA2KJirqHYNoUPr9/vn5eaqIkopBYpKeIwlbJbE0ITYomqhc72Lc7XZPT09TRZRUDBIzbvoc2WqDosmzQdFEUebqDUqn0zk+PvZOslJRSEkSM64+iWytZ640GTYomrQoeXGpLI8HBwfeCkUqCilJYuYkzWkrTZINiiYqSh67YnFbBezu7norFKkopCSJGRkaqZo7FWlibFA0UXHTJ+pdvM243W5vb297KxSpKKQkiRnvuYtUJW1J3pTG0kTYoGiiqHH1qsd4a2vLW6FIRSElScyRVLVB0YTZoGiiVlZWoszF48LCQr/fv7u7i7L4dyXGkiapnn2kJIlJeuZU5dFTPJowGxRNVG5QGGBxcbHX693c3ERZlFQCUpLEJD0jT0lYGxRNng2KJm2kQel2u95MVioKKUlijjQoVfpKk2ODokmLeheDVqtF4Ts6Okp18U0cbUZ6LukLpDQbSzRSksQkPevZGgNpYmxQNFHLb5/rwZgK2K4+3WNnZ+f79++pNFZ4Ol40Jf1xJNp49pGSJGa+MLaettLE2KBoonKlA7Uv3nK8trb2/Pxcr5I2KNJkjDQojElGUpLEjLcWV8lqg6IpsEHRRMXtnqLk5bcv9nq9x8fH+v1k6xVT0peqpxtpSDKSkvX3GIO0JXlTGksTYYOiiYoGhUdE7Wu1Wgzu7+9fXl5SjbRBkSaonm6kIclISpKYpCeDyFYbFE2eDYqKsL+/H/Ux3xPF+99LE5ATLaceyZjSUpoqGxQVYXt7O4rjw8NDDGxQpAnIiZZTj2RMaSlNlQ2KirC2thanePKJHhsUaQJyouUEJBlTWkpTZYOiInS73dvb2yiU+Pvvv3OnIunrkGj1d8yRhiRjSktpqmxQVISVlZWzs7NUI6ur9upv6pH0RUi0+kWypCHJmNJSmiobFBVheXn54OAgHzVhly7EU0lfIaXZW6KRgKQhyZjSUpoqGxSVYnt7O1+mFzyIIn2pkRQjAb1CVuWwQVEpNjY28hsdg5ehSF9qJMVIQNIwJaQ0bTYoKsJydef7y8tLqiR7dR47kSYmZxwJGPe2T2kpTZUNigpycnKSK2YMJH21nG4kYEpFqQA2KCoC+23tdjt/rHG+ak/SV4t0I/VIwPwJxtLU2aCoCJ1OZ2Fhod/v39/fR9Gsv/VR0hfJiUbqkYCkIcmY0lKaKhsUFYGdtsXFxfrdULxCVpqAnGhxBxTS0CMoKoQNiooQn5WK3d3dOCNugyJNQCQaSUfqRQ56ozYVwgZFRcgf7D4cDuMsj9fJShMQiUbSkXokIGnou3hUCBsUFYHK2O/3o025vr7OdVPSl4pEI+miNSENScaUltJU2aCoCK1WazAYtNvtxcXF/GZjSZNB0pF6JCBpSDKmtJSmygZFRWCnrdvt8khx3N3djY+A94080peKFCPdSDpSL6dhSktpqmxQVIR8XR7FsdfrXVxc5Oop6YtEipFuJF3uS7xIVoWwQVER4kZtPFIcGezv73uvNmkCSDTSjaQj9XIaprSUpsoGRUWgOLZarU5lcXFxY2MjzvJI+lIkGulG0kX2kYYeQVEhbFBUhOXl5aWlJXbdYh+u3+9fXV2lCirpy5BopFscOAFp6NuMVQgbFBUh6iO7blEcGe/t7XmvNulLkWIkGulG0pF6+SxPlZTSlNmgqAitVqvX61EfKY7RpuQ7tkn6InF/tmhNIvVIQ5IxpaU0VTYoKgX1kULZqT6ojDF18/T0lBr6/ft39vPyNbNePCv9gnoGkVDx/h1SjESLQyakHgkYY6kENigqwurqKsUxBjxSJbvd7t7eHmWUeppFhZX0a1IiVUguUoxEi6Ykn+iJgTR1NigqQq/X4zGKYxxt5ulgMLi9vaWSRmHNN7+PJZI+KKcMSZQTiuQixUg00o2kyzsJkYzS1NmgqAjsxvG4VH2SKhiAJcfHx/lS2TywQZE+JadMPZtIrkg65LzLyShNnQ2KisDeG8Wxfqi5KptLm5ubd3d3uaTGwAZF+pTxBoW0Irkiy3LSkYA8jbE0dTYoKsJydR+UqJWxMxcL2Zk7PT2N8spjrrOSPitnEI+kFckV53RyxkUOxkJp6mxQVISflUUW7u7uPjw8jFRYSZ9Szx0SirT6WcaRjOmJNFU2KCoCNZF9uDi2TInMpbPdbq+vr9/c3ERh/f79e75UVtLHkTjx1mKQUKRVnFFFzjgSkDS0QVEhbFBUtE6nQxnd29uL/T9EhZX0WSmF/v6bhCKt4p5DUrFsUFQ09ueopBsbG3d3dxTWXGdjIOkj6rlDKpFQpFVcdyIVywZFRYuDz6urq4eHh3GAOh+mlvRxOX1IJRIqMiulmVQkGxQVLa5NYW9vOBw+PT1RYfP7JCV9XCQOSRT3tvdaE5XPBkVFYyev0+ksLi5ST6+urv6u3U9W0sfFPWRJIlKJhIpP3klpJhXJBkWlo5Kyq0cx3d3dvb+/9xSP9AtIHNIn3l1MQnmFrMpng6IZEIejKan5pm2SPoXEIX2i3SehUmpJBbNBUemiNeFxYWEh3m/sQRTpU0gZEof0IYlyQqUEk0plg6LSLVfv4qGkxuDk5ISCS7VFLr75M1qlOUci1O/JViXKa2qQOPHmHVIpBinBpFLZoKhoKysrVFIeKans8zHe3d19fHysF1/kcSyX5lPOghgglpMy+eoTUimnVUozqUg2KCpa3tWjmFJb2+12v98/ODiIslsvwfANPppz9RSoZwcpQ+KQPiRR9CWkFclVJZlUKBsUFS129RBVlQrL42AweH5+Hr8SxWtTNOfeTQqShZTJ6TOSU1KxbFBUulxM64X19PQ07tsWJXi8Lktzq54RpAnJUs+dPE4JJpXKBkVFy5U0CmssxPr6+tXVVZTgb5UYS6pnBGlCsqS0qeVRziypWDYoKlruTmIcYnx4eBh37847i/XrUaQ5lFMgkoIEIU1yyoR6QvEoFcsGRUWjhla7fK8HpduVeC/PX3/9tb29fX9/Xz924sf0aM7VU4DUIEFIE5KFlIlrzFHPqZRmUpFsUDSTosJSfOMtx+w42p1IIBHiOAqpQYJEpqS0kWaKDYpmUrz9eDAYnJ+fx9FsinLUZWlu5SwgKUgNEoQ08e3EmlE2KJpJ1Nw417O5uXlzc5Orcwyk+ZRTgKQgNUgQ0sQGRTPKBkWzKs6pdzqdg4OD/JZjSaQDSUFqkCCkSUoYadbYoGgmxfV9PGI4HOYTPdKci5M7JEVkR04WaebYoGgmUXPz1X+rq6tbW1v5RI80z0gE0iFO60SO2KBoRtmgaCblshuDTqdzeHhYf8uxNIdIARIhPvqb1CBB8kCaOTYomkndbjeuQaH4MgB7jff396lOS3OJFCARIiNIjbgGhWRJaSPNFBsUzSp2Dam/rVaLR55Shfv9fv1uKL6pR41XD3KCnxSIdiSnRhxolGaRDYpmFZWXvcPYR+Rpp9Pp9Xqnp6fPz89Rr/MZHzsVNUwO6RzkhD3BTwqQCKRDTg0bFM0uGxTNMIpvYLyysrK6ujoYDOofIhgDGxQ1zHiDQtgT/KRAvvQkVIkizSQbFDVHVOSdnR0vRtFcIeAJ+4j/lAzS7LNB0QxjZzEwpjQvVRcGdjqd/f39fDGK90dRI+XAJtQJeMKe4M/ndKq0eFUlijSTbFA0w1INrh3TpkxTo3u93vn5eRwG9/yOGimHN6FOwBP2BH9kwUhqSDPKBkWNEgWaury+vn5xcRGlvP7WHqkBckgT5IQ6AR+Rn9JAagQbFM2welGudhdXFhYW2I9cXV1tt9vb29uxl/nw8BDVXGqGCGnCmyAn1Al4wp7gjyyIjKhnhzSLbFDUKPEuBupyHPHe29vLnyNYP9fzrZKeSAUbidUcxgQ24R3nNAl4wp7gT2kgNYINipqGMo1q73F5bW3t4uIi1/T6II+lktVjtT4gsAnviPOI+ZQAUlPYoKhR4nB3r9djh7LVarFka2vr/Pw8l3V2RvPbH3yDjwpXj1VCNzcohDSBTXgT5IQ6AU/YE/xVEkgNYYOiRll6e6cxj9RrdLvdjY2N29vbqPWUeBsUzYp6rEZ3woBgJqQJ7IjwHPAEf0oDqRFsUNQ0cdCbASU729vbu7u7i1qfd0PzQCrTeKwSxgRzCutKPealJrFBUaNQr6nU7FbyyG4le5ks4Wmv1zs8PHx8fIwqL80iApgwJpjjwAnhTZDngI9mRWoMGxQ1DZU6jnVXFw6+XjnIU/T7/XqP4uETzYQcqNGdEMYRzyMRTtgzkJrEBkWNEmU670ryNJawi9lqtYbDYf4owXxSXyoWIZovQyF0CWDCmGCuxzYi4PNTqRlsUDQXqN3sZVLZu93u2dlZVPzn5+eXl5fcpjAT1N8oIU0SgVd/ixlPCU5CNJ4StHFVrAdLND9sUDQvKOvUd/Y1t7e3b25uYiZgGqiLyUCalhSIb1hCoBKuBC2hSwDbnWh+2KBoLlDc4yAKj6urq5ubm9fX1zElgDkg+pUQE4M0MfWQG4lGApVwJWhzAOczmFKz2aBoLkR9B+Oo8js7O/f392kSkIpEiBKohCtBS+hWIfzaYVdBLTWcDYrmQm5Qori3Wq1Op7O9vf34+Jj3VtmL9RoUTctI+BGWBCchSqDGPZFHYlhqPBsUzYU4xcOAch+FPo6jHB4e3tzcxKzA48vLS/3oujQxBF6+ZJtHwpLgzIFK0BK6BDBjT/FoTtigaC5EWY/KzmP9OMru7m5cM8usEI8xYUiTlMOPRwKSsKwfO8mhC4KZsdR4NiiaC+yGUuXRrlD6u90uhf6vv/5icHR0lG/gBmaINJImoh5yhCIBSVgSnIQoA8I14jZiOK5HkRrPBkVzLZf77e3tfM3s09NTDKTJyCFHEBKKBGS01FWQSnPKBkVzjTmAndQ4prK3t3d7exvzxPi5Hp5++/YtPZF+yfhV2HFOJ8aEH0EYx0sISxsUzTkbFM21OLUfPQrj/N5jpo0QMwcYe+pHv2mk8a1C7BXjeEcxQRjdSQ5OaW7ZoGiuxYH0bvWhx4zZeWWSOD8/j/kDzCgvLy8eO9EfRDiNvF+MkCPwCD+CkFAkIPPJR2lu2aBoruX5gLmB3VYwMfT7/bu7O6aQmDyiR4mx9Pvq3Qljgo2Qi7ONIBRzx5zCVJpLNihSukEnGDMxMFVsbW2xU1s/cFLf35V+WT2QCDDCjGAj5Ai8kVCU5pwNiuYaEwOP7KqCWSG6k1i4vr5+enpaf0ePPYp+Uz2ECC0CjDCLOAThRxBGNObglOaWDYrmGlNCbkrAmIVMEu12m8FwODw+PvZdx/rjCCpCiwAjzOpXxaZArEIxolGaWzYo0vuYNtiR7ff7R0dHDw8PMa+MHJ9/ebs3uTSCwCA83j1LSDgRVIQWARatsKRxNijSOzrV554wf7Bry0Syv79/d3cXvcg/TDxS+FkjyyOBRDgRVHE2JwebpBE2KNI74mA7j61Wa3FxkfHu7u7V1RUTDHMPU06egVhSb1YkEBLRkSAHDEsIIQKJcCKoCK0cZinsJNXYoEjviA9pi9tRtCvs5q6trZ2dndU/tef5+Zmd4/REqiEwCI/0pPqEHYKHECKQIqIILQIsB5ukETYo0juYP3iMO1IwkSy/ictm88TDPnHeUZbq6rFBwMQlsSmMqtvtxN13crBJGmGDIr2DmSMOvDOX8Mg4nvLIvLK9vR2ne2L6qV9wIKF+BpBQIWAImxxCMcihFW2KpBE2KNI7okFhCmHvNi6VraaV13kllqytrV1cXMTVJ7lTkUKEBOFBkBAq+d06EUWEUyyJuLJBkd5lgyK9I95eweQR43qbwmOr1eIxTvfUL0mRMgIjn9aJgKm3JiMBxqOkETYo0icwwaxWGDDNMMgfLshOc7xZI+anMHJwxWMtM+2ft2a8WycWxof/ER5xmKQKmdeYSWEk6QNsUKTPyZMNu8Xs+3a73Y2NDSakfDM35Ikq3lwaCxnEHIZYolkRW218a8agvkEJA4KBkCAwCI84dhIxkwJI0sfYoEifs1JhkI/VMwON3Mwtz2Q8hmrySmJi0wwZ2WRpo75t4typjNyErX4qJ2JG0sfZoEi/KO8Wx2zEeH19/eTkZOSqFGYv9rBjMlMDsCnjaEp6XmGjs+kJAMIgetZ6hEj6BTYo0ifEfBMzEPvEnU6HJQzy7jJ7zwcHB7e3t/mwf8xnMaUxDvElzYq02Won6RjHlxizudnobPocBoQEgZHfC8aSHDySPsgGRfqE3I4g9pLBADEnxZLhcMj+dP2qlBCTXHqimfLutmMTs6HZ3LHdIzwiHmIJg4iWenhI+ggbFOlXxCQUcw8YM/2wx8xgcXGR3Wj2p7e3ty8uLsbbFDUAm5WNyyZmQ7O52ehs+jiiNhIYSEEj6TNsUKRPiHfuxN4wj7GQAVieD+/H04WFhW63u7e3FxfP4tvYtQuaFflaIrBB2axsXDZxfYvXAyDHRoQKywmeWCjpI2xQpC/X6/X29/fv7+/TXPfeu0LSSAUY2Rz1jcVGZFOyQdOmlfRlbFCkL8cONFPa1tbW6ekpO98jH4Bc7ZP/34wYT5GfetDlj2OV1tdwiKcYeQo2GRuOzcdGZFPmAySSvo4NivSFlt7uYh7H/zudzvb2drwVeaRNCSPzoibp3ZXPZoq3ELPh2HxxEidv0xhI+go2KNIXissO4trJ6FHY+e73+7u7u8x57JR7dKRYbBo2EJuJjcUmY8NFdxJXQ+eNK+mL2KBIXyh2spnPokeJJcxzDKJNOT8/Hz/pA2ZHFtbvt6E/ixXL6mUlj/eILGSjsGmiNWFj5QMn0Z0gNiWPkr6IDYr0hZjJ4h0cMZnFtQuMY8KLoylxbcrDw8NILxIzqA3KF3l39fKUDRHXmsRRk7yx6psPjKNNkfRFbFCkLxRzGDNcvD85Foa4W0YcTRkMBuysHx8f39zcPD09pdlSE8RqZ+WzCdgQbA42SvQlI3cxYQmbMraaDYr0pWxQpC9U3abrdYZjhzv2v2PhaoUljKNNiW9bW1s7PDzM16awQ8/Agyh/XH3FMmCFs9pZ+bF1cmvCBootFVsH1WZ83Y4syQslfQUbFKksTIfswW9vb8d5n5hQwWwa10zUT0z8rHdh0s0TcIPxAuOVpuc/qq+l8VWHOJvDqmaFs9rTBpBUBhsUqSB53z3alK2trYODg+Pj4/GrJVjy/PycnlRith75tvnx7stnFbGi0pMK38ASVikrltUbrQkrPI5jpc0gqQA2KFJZYrIMPO10Ot1u9/DwkH39+ockj/iH7uTdhbPuZ6/0H1o0Vh0rkNXIymSVxhUkaUWPXWsiaepsUKSCMGuyQ5+vcuCRcZx94Etra2t7e3sXFxePj48x6Y5MxvlpTNXZz+bsGfWzVzfyMvNTVhcrjVXHCoy+5N2VHF+SVAgbFKkgzJR54oy5MxbGUzCPxhUqR0dH5+fnz8/PzNAxDQdm5XelLzdCeklj0pcrrBZWDquIFZWvMkkrsbr6NdZtPI2VnBdKKoENilSQPE3GxJnFVxHfELv72NvbOz4+vrq6enh4eHnv3vkZ8/dIKzOL6sdL3sVKYFWwQlgtrJxYS3G8BGklvrd6R75B0tTZoEgFiWkypEXVwvokGnjKwqXqzuvD4TCOqVxcXNzd3T09PT0+Po4cXGlkg8JTXiYvlpfMC+flx/ESVgirJV9ZklZZJdZbDEIsD2mRpALYoEgFaVdiZq1PrnFRJwMWxlcZ189ZgG/me5ibNzY2zs7OLi8v7+/v//mwykzjpfECeZm8WF4yL5yXz0pIq6MSx04Y5JXG97AyGSB/M1+NNc8SSYWwQZEaiImZmXgwGGxubh4cHFxcXDCXPz09/fP5kcLxz/MSeCG8HF4UL40XyMvkxaaXLalBbFCkBlqqxDGDbrfLRL6xsRF3VYmra29vb59/vI1KFieDvlUYB5aEWJ6+9cPip9KvqE7ThLyccfrWH/FP8q/Gta5x5xJeCC8njoLEwQ+kly2pQWxQpGbKZy7is2MYs2R1dTX3Kzs7O3F7Fab/+/v7h4eHp6cn2oWf9Qp10Wd8XPqxn4tmhX+Af4N/hn8pbljCP5k7kjilNf6i0guW1Cw2KFIDxbUXDJi/QyyPcczx9Qme6b/f76+trW1vb+/v75+cnNAiXF5e3tzc3N7e0jHEVbf/fLTjH/Aj/CA/Hte08gv5tfxy/gR/iD/HH+VP8w/wb/DPxL868n/GvxpfCox5mZ7ikRrJBkVqrDyLg4k8tyzjDQqDPI7vZNbHYDAYDof0DXGGaGdnZ69y+knxU/x4nKPhF/Jr+eXxV+Iv5v8NP/s/83fGN8RCSY1kgyI1X8zlSM8rMdnTH3Q6nV6v160wHukA6mI5qi7iE9KP/fx38kfj/TXgn4lLX6t/MP0zIf2MfYk0B2xQpAaKqT3EEiZ1GoW0qJIXIg5UhFgSqgMcr9LP/NgufFb6FW+HZ5D+TCX9+UosiUYk/UwlLxxZHkskNYkNitRAMcGD6TwwZuJnwFeZ0aM/yFN7PB0XXw1VJ5CkRR+WfqySFlXSnxkTX+Wb42n8FP989C7VC3pVvcRX8f2SmsQGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRZIkFccGRWqgpaWl5Zq09EcrNe9+W/paJS36jPidIS16k35pJS16E9+fvlZJX/hRfFvgxaalkhrEBkVqoJGpPc3kNUzqaL9J3zemU7NaE79/3PhX4/fE8pB+XSW+Oi79W+12/J/pn65J3zf2SiU1hg2KNEfoCVKbUInZHenLb1IX8NbHfNzIjyN94SfSN/38EEv6Ryv/0NBIah4bFKmB0vxfdQCxJOb7Vqs1fmQifzWLHwHj1B18TLdSHRxJ0hfeM/KH6lgS/1v8n/E/88/nr8Y3xFcRSyQ1iQ2K1EBp3n7v5EiIb4v5HvF0XPotlfSTlbTo56KlyNKPVdJ3VNKfGZP+rVov8q70W2xQpCayQZEaaGR2ZwqPRiGWZDHNj0hfq8ShjjgWEkdHepXBT/R/FN+cxW+I3xa/Of2ZSvrzP0pfexOvgpeTv1R/pZKaxAZFaqCYtsdVhxteZ3fG0XPQN9BJ7FR2K3t7e/v7+weV6zc3Nze3lbvK4088vUnP38RPxW/gV6Vfen0df4U/xx+Nvx7/STQ30c3wr/IPx39evYh3pJctqUFsUKQGyocZVqvrQpjvh8Ph2tra4eHh0dHR6enp5eUlHQOdxLdv376/+fvvv//7I76av6H+VcbvSl+upEWVtKhaGL8tfnNa+iZ/FXyVf49/kn+Vf5h/m3+el8AL4eXwonhp0bjwYtPLltQgNijSFFTnN15PcOQpNsQxA5a/Hi54O9QR3/muOHgQvyHakV51/mVzc3N3d/f4+JjZ/eHh4eXlpd4lzDReCC+HF8VL4wXyMnmxvGReOC+flcCqYIXklfMuvpOvxsqPb47jSdVGeMVCvhrfieqHJE2UDYo0BTEvxhTITAlmQebIxcXFVqvF8liC+Gp8f13Mo9GObGxs7O3tnZycMGff3t4yeT8/P3+rjnzkST2OScTT2RUvKjdb8aJ4sbxkXjgvn5XAqmCFRMsSaymtsppYq3kNs8JZ7ax8NkFe52yaegcjacJsUKQpYM5j8quLiTCmzBBzJAuZYhcWFqJxYS9/bW1ta2trd3d3f3///Pz86urq/v7+5eUl5ux/0ICDKB95CawKVgirhZXDKmJFsbpYaaw6ViCrkZUZXQurl5WcVnel2gjvbJpqo0maKBsUaTqqvfRXjJkFmTKZO/v9fpzied3xfztrw8w6HA43NjbirA1T7+PjYz46MiIOKjBJ14+gNNVHXizLWV2stDgfxGpkZbJKWbGx2sEKZ7Wz8tkEsdpHNpCkybNBkaYpzYFvmCBjjuRLzJfs+h8eHp6enj48PDDLxombXzgQEqd4fjaFz5B4Fb+2Blh1rEBWIyuTVcqKZfWyklnVrPBY82kzvKk2kaTpsEGRpiAmvzh9EBgjDpbs7OycnJzc3NzkpiRNs29yw8GgPk5fnmP1tVEfpy+/yc0KK5lVzQqPwyqxFdImqV0kVG00SRNlgyJNE5Nfp9MZDAbr6+vs0F9dXd3e3j49PY3MqXmiHZG+PIYJGPmn0tIG4UXFq4tXmpaOqVbSD+Kn0pcrLGSFs9pZ+WwCNgSbg41iXyJNlw2KNAWtVosd9NXV1bW1tf39fabGuMo1ps+YShEz6D9493s+8oMN88vrge8JseZZwoZgc7BR2DRx2SwbK202SRNkgyJ9AjNWHTvZy9WFlvVzAa8nCd6exiC+mUF13uDV9vb24eHh5eUl++4xU6o0bBo2EJuJjZU2W/V2nrw12bjjWzyeRkjwNL4546uSPsgGRfqEkQkpxFSUv8Q0xiOzUa/XYyFjdsEXFxf56sbGxunp6d3dXbzxhB33NBlWu/KxB68pYhOMbBQ2ExuLTcaGY/OxEev3qsl3h4uNzlerQEiNaWCcvxRLJH2EDYr0CTHHMN/UJySmqJixOtXdSPP7hPPCtbW13d3daE1GupDoS3hEWqSpim0x0qmAJdGmsCnZoGxWNi6bON5ylTd9LCQk3g0VHiV9kA2K9AnMMcjTTyzhaZ6EwCBPSFtbW0dHR0xsebZjwE45sx1eL+/88daoKkE0KHnrxLi+BdmgbFY2boRB3uIxJhjGI4THvETSR9igSJ/A3MM0w44yGMTMBGYgHtl1XlhY4HvYwz4+Pr65ualPbGBcf6oZMrLtGLNx2cRsaDY3Gz3f7TeCIdSjhe9JYSTpA2xQpE/IM1B0J+23T5VjOV8dDAY7Ozunp6e3t7fsdqeprBLz2Ui/ohnysy3IhmZzs9HZ9ARADhICo36pbA4SSR9kgyJ9QuwEM+XEmFknbq3GzLS7u3t9fV3vS5jJnp6emNLS8woLke/Ozjh9QYVh08TJHTZWtdF+2FIsH7ldDd9MABAGBEO+7dtIwPAo6YNsUKRPiL1hMGb66XQ6Gxsb+/v7udsAgxiniasSy0cWhtefsU0pyc+2CAt/tmVjeYwJBkKCwCA8CBJCJWIm2hRJH2SDIr1i8qguFUj3D2VeCez1ggETTK/Xi4V8T7/f39nZubi48EYmeheBQXgQJIQKARORU39bcsRVGI9ASTYommt5vzbPEzHI15p0ax97u7i4yKTCEmad8/Pzx8dH9pjTdCSNITwIEkKFgCFsCB5CKC5MqYdWBBvqcUhY8lUG0tyyQdFcYw6oNyhgzJI8iK/GmG8+ODg4Ozt7eHhIU1Dl288/C0bzaSQkCBjChuDJ8RYRFWOCLQ9CfNUGRXPOBkVzLU8GWae641bcBJZd24WFBXZ5B4MBs8vl5eXIIROeMhW9VB+jI2XjdwoGTwkhAolwIqjibcmEGcFGyMXJnSwHpzS3bFA015gneGRXtb5ri2o/Nt3LfGtri93f5+fnNM9U79dg+hmfgaS66F9BwKRF//0vgUQ4EVTRBEekpbB7O3ACxhGc0tyyQdFcW3q78RpTAmP2aMHEsLi4yE7t3t7e9fV1vTUBs06WFkk/kQKlkhZVCCpCiwAjzOLClIg9gpBQjKMpjFOYSnPJBkXzLuYDBkwSiKMm29vbx8fH9/f3aT6p1PeDA7NOHE1Jz6VKHDUZaUowEkIEGGFGsMXRlIhAQjE65io8pfllg6K5Vj+ivly7r0nMLiFakPp8EwtDXijVvRsksTCHU2DsfVOkcTYomgtxaCSOnC9WGAwGA5YsLS399ddffMPa2trZ2ZlXvGoqCDzCjyAkFAlIwpLgJERzxBLGEcBxlEVqPBsUzQv2TeOkPlUeDOKsP4PhcHhwcHB9ff309FTf35UmI46jEH4EIaFIQEZ81s/4EMA85ZEehYVS49mgaC5Q06nsUdzjLZ2MFxYWGGxtbV1eXjI9pLnivWtNpC81En4EJGFJcC4uLhKoDEAYR4dNs1IFtdRwNiiaC1R2yjqtSRwhBxV/fX394uLi7u7OQyYqCgFJWBKcm5ubBG2r1cqneOJQSkS11Gw2KJoLseuJ6E56vd7h4eHt7W2aECrsufp+HE1RXDybnlTu7++Pjo4I1+ps5Osd8YlhGxTNCRsUzYU4cBK7oXFOZ/xiWOYGD6VoiuJKlPTkDYEaZ3wIXQKYMPYaFM0JGxTNBWr66upqXAx7e3tbb0QY159KUzcek3EoZW1tzVM8mh82KJoL7Hdubm5eX1/HHurIBMDC+p7ryNwgfbWfRWN9jJubm42NjTgQKDWeDYpmEg1HXPTKmB1Kxp3qFhF5HE/5Ko+MLy8v6x9BzHxA3bcRUfnqUcr47u7u/Px8JLwRR1biaT7KQoIwJlkYSzPHBkUzKRoRLC0t5fobg7ycHU3q9c7OzsXFxePjY/0CWLsTzYTopAndHK48fXp6IqQJbMKbII93qCGnQAzy8mhWpJljg6KZRGkGlTcKcSxknBfy2Ov19vf32eOMyp5R4pGeSGWjNRl/dw8IbMKbIM8BT/AziFyIxj0WIhZKs8UGRTOJyksJRpTgfFgbUZfjipPY7xx5w07slaYnUvGI2IjkLEKahQQ5oZ7DHiQC6UBSMM45ktJGmik2KJpJlGNQfCnEVOR6Oe52u7u7u/V7nDw/P4/Ud2lGEcm014R0ev7f/xLqBDxhH/FPIuSk4GlkSkobaabYoGgmRTsStZinuVkZDAZHR0e5fOeT93GQfKRNsWtR4d4N2ljCY76sioAn7OOTBaMpISkY5zSpkkaaMTYomlW57EZF7vV6Ozs7l5eXT09PUbVjRzNX8xBfCiNPpdKMNygZywlvviGeEvYEPylAIkS/Htlhd6LZZYOimRQHTgIleHV1tX5aJ1dtRB1PT2xKNLMI3Ry9ufMOOeDjdE9cj5XS48dkkWaIDYqKxr4gpZYKG0WW/cLQqT6OOD6dZDgcnpyc1LsQaW6RCKQDSZEThGSJVMpPI3284ZsKZ4OiotUvgOUxLa2OmlBtqbnr6+uXl5dxMr5+4ESaQ5ECpANJQWrkpiT6+3oeVT2/R1ZUNBsUFS0alBjnkhrdCV/a3t6+vr6O0gwPomjO1VOA1CBBSBOShb6ExMntfvQoPH3NK6lUNigqWv2gdNTW1xM8S0u9Xu/w8DCX47+9db00lggkCGlCskSPQgaRR9GXRCpFlkllskFR0SisPEZ3wjjuXj8cDs/Pzx8fH6MK4+XlxfM7EkiE+p0JSROSpd/v05fQkeSLukgrGxQVzgZFRaOqxg4frclff/3FYHt7++LigsrLbmLc3SQKsaQ6UoMEiaMp9Cg7OzukD0m0sLBAj0Ja0fe/5phUKhsUFa3b7bK3x24fqKcU2evr67gklsrLnmKMQz6yLc2negqQGiRILKFZyZekkEr5dE9KM6lINigqGmU0jkvTqezt7d3c3ERH8vT0lGvxeKciza16XwIGcetCll9dXe3u7sbnC5JWPKY0k4pkg6IiUC5zxaQpAYM4WR7dycnJCXXWEzrSLyN9SCJSiYQirUiu8YyLTGQsTZ0NikpRL46Uy+rik9cbYvb7/cPDw3xJLEU27x1K+ghSJjf3pBIJRVqRXJFl0Z1E9kXLIpXABkVFGwwGx8fH9bs71EutpI8YaetJKNKK5EppJhXJBkVFiAPO3QpP4zhKp9M5Pz+P7oTyWj+zLumz6klEWpFccZ1spFtkH2lIMvJUmjobFBWh1WpRGaNcxs3Z1tfXj46OqKdRW799+1Y/jiLpF5BE+XJykosUI9FIt5EbDlVJKU2ZDYpKQZVk121hYYH9OYrm1dUVNfTdQya5a5H0Ee+mTCQXiUa6kXSkHgnoNSgqhw2KisCuGyVycXGR+ri1tUXRjAtNqKEjp88Z159K+lcjWcM4pxUD0o2kI/VIQNIwDmRKU2eDoiJ0u10qI9iZy5//l+/fkA9K54Gkz6rnUYwjxUDSxXEUkIwpLaWpskHRRLGXtvp26/pWq0Up7PV68ZT9tp2dncvLS4pmfW9P0pci3Ug6Uo8EJA1JRlKSxCQ9SdJ4Stp69kcTZoOiSYubLkSxi3swxFWxm5ubV1dXeSfveyXGkr5CPctIPRKQNCQZSUkSM/YlIlUjbaVJskHRpEW9Y0cNjOOo8tbW1vn5ea6VDF68e730xUgxEq2ed6QhyRhZSXpGnua0lSbJBkUTxX4YlY7HuOMCO2rspVEQHx4e8hsN/q4+ptizPNIEjKQbaUgykpIkZhxHiVSNtE1pLE2EDYomKmocj5S/2EujFN7d3UVxZAeOWpn35yRNxkjqkZL5OEqch83JK02MDYomjTKXra2tXV5eRk2MPbl6d8LTNJL0BeopFj1KPpRCYpKeKVErKYGlSbFB0aRFset0OpubmxcXF1EiKYvUx3p3EkvSE0lfgBTLHQmqFExLSEzSkyQlVSNnUwJLk2KDokmLStfr9Y6Pj6mGUQpzlWQJTxFPJX21yLhIRpCMkYAsIUnjRgA2KJo8GxR9CcrZ8pult89wj5PZPB0MBufn5xRBSmFuTSSVI3KTJCVVSVjSluQlhUnknNTB3kVfxAZFX4KyldsRxnGUuNVqRXfCbln+5L84ZBJjSSWoH1AhVUnY6FHifT2kM0mdWxbGKe2lP8oGRV+C4kUVi0LG03in4sLCQr/fp9jVP7qMvbRcCiWVgJQkMdOT6r3HpC3JSwqTyKQzSR07HiDZq6SX/jAbFH2V2LsC4yhh1LXT09P88R8eOJEKl5OUtCV5ozWJdI7sJs0ZS1/BBkVfgsrVbrcZ9Ho99rT+93//lyX1MzsUvjyWVCaSNPcoca6HRCadSeq4eJY0Z0mV9NIfZoOiLxENClUsdrkYb21tPT4+5rM5IweQ00hSAUZOwsaA5CWFSeTY9yC1SXAbFH0dGxR9lZUKg071McXX19dxYptHRMmLp/VmRdLUjSRmlbIpeUnk+NDjeo5LX8EGRV+C/ar8ce39fp+iRpmLY8U8xv4Z9e75+ZmS91oCJZWExCQ9o0chYXPy8kg6k9SkNglOmscBFemPs0HRb4m3GsZeFGMsV3jKI08pYaenp68Fr7rOLgaSZlFO4bhgNvI9kj3yHTyNmhBj6ZfZoOjPiPLEY9SmeEoJOzw8fHx8jKKWjxhLmkU5hUlqUjv3KPWs5zEVBen32KDot8T1cbkqUaRYApbwdGdnJ+9yeSpHaoCcyKQ2CR7pH1lP+vM02hQGLOFR+mU2KPot1CDqUZyEjgoVnQqP6+vrI59UHGNJs4tEzsdRSHDSPKd87J8wpiDw1AZFv8kGRb+FksTeUuxCdar7xkZtWltbOz8/HzlqYo8izbSRFCbBSXOSPSc+RYBSEDUhDqhIv8wGRb+LwhQNShQmilSv1zs7O8u1LLcpnuWRZtp4LpPmJHvcj7FeB+KoqvQ7bFD0W/IRlDi5Q1Xq9/sHBwf1PS2vjZUapp7UJDspT+LHQZR6TUhlQvolNij6LbG3hOhOGGxubsZdT15eXvIVss/Pz94uVmoAEpl0jjEJHnlNypP4pH/0KFVJeD2aksqE9EtsUPRb2EmiDMXhE56ur69fXV1F8ZI0P0h80p8iEAdRoixURUL6RTYo+gPYbWq1WoPBoH7piaT5ERejUAQoBRSEVBqk32CDot/CfhI7TIuLizweHBzEpXNedCLNlUh50p8ikAuCp3j0m2xQ9Ft6vV6cb97a2rq7u4tq5UEUaa7klKcIUAqiJlAcUpmQfokNin4LO0nt6p07Nzc3lCd2pOxOpDlE4sdxFEpBvKPHIyj6TTYo+i3sJ1GGDg4O8jFe360jzSESP5/hpSBQFigOqUxIv8QGRb9rb2/PjwOUlNOfgkBZSAVC+lU2KPotvV7v9vY2qlK+O4IHUaS5klM+FwHKgteg6DfZoOhDFhYWeOx2u3HLk7C0tHR/f//09FQ/cMK4/lRS441kPWPKAsWBEpGKRXVzFApILibSv7JB0UfVS0xcAbe1tfX4+MjO00htqj+V1HgjWc+YskBxoETEdfQUjbx7U5UT6d/ZoOhDKC6xMxRVhoqzsbFxe3ubL92XpIyyQHGgRFAoKBexb0MBoYzETo70r2xQ9CGdTieNqnfu8PTk5CSVohqbFWluvZv+FArKBUUjlY8fi4n0D2xQ9CHRlKyurrZaLXaDdnd38zt3MsqTDYo0t96tABQKygVFg9JBARlpVqR/YIOiD2m3291uN04nDwaD+LziEe+WJ0lz4mcVgHJB0aB0UEAoIwxSWZH+kQ2KPmSl+mDSuAbl6Ogobhc7Xo/eLU+S5sF4NYgllAuKRlyDkouJ9K9sUPQhvV6P/Z5Wq7W5uZk/c4fq8/3796hBoT6WNFdGSkG9OFA0KB3xQcfeH0UfZIOiH8TR19UKg9jjiQMncQr54uIiKo69iKR/lQsFpYMCQhnJh1LisSo2r9XGUz8aYYOiUSuV+pjysbS0xGB7e/v+/j7KjQ2KpH+VCwWlgwJCGaGYUFKq0vJDnYmxlNmg6AfxDkD2bGLnhqpR7d6sLiwsrK+vX19fx+eBjZzZkaR3UShy0aCAUEYoJlFVoinJ1ca3H2uEDYp+QI2gWMT7AKOCRO1AvjbW7kTSx+UeJa6WjXpCYYn9H8YUHJ7aoGiEDYp+QMmgWIDCEXcsiOvatre3b25uotxEmyJJH5SLBmWEYhJX3FNeKDKUmqrkvJ5ETmVIqtigaBS1g72ZbrdL7WAMKsjV1VV8Til7QrEzJEkflOsGZYRiQkmJ2kKRiWtmGacCJL2xQdEPolJEg8IODWNKydbW1svLS+5L/q7EWJL+Wb1iUEYoJvlDBCkyuUHhMZUhqWKDolFRKeJ8MOPhcHhycjLSkXz79i33K5L0MxSKkZPCFBNKCoWF8kKRodREg1KVH+n/2KDoB+zWUCyWqmvWKBns3xweHtqLSPqDKCkUljhGmwsOxSeVIalig6JRVA0woGr0+/2zs7NUVCTpD6GwUF4oMvWaI9XZoOgH1ItOhTElY2dnJ9+ZTZL+FAoL5SX6kqg50axImQ2KfhAHWqkaDIbD4eXlped3JP1xFBbKC0WGUkPBiZPLqQxJFRsUjaJMxNUnBwcHLy8vlBJvfCLpD4qSQnmhyFBq4kqUVICkNzYo+kEUC6ytrV1fX0c1iTugSNIfkUsKRYZSEzWH4pPKkFSxQdEoKsXq6urh4WEcPoFHUCT9QbmkUGQoNRQcyk4qQNIbGxT9YLl6y99wOLy6uooKIklfh1JDwaHseJZHI2xQ5hS1YKVCXWDfJa6f52nUiJOTEwpHvjlbPpQiSb8vl5QoMhScXJQY5KJUlaj/+7xSzRsblDmVc55BjKMWUBfW1tbicwGpHfEWnpHbyErS74iSQnmJAQUnrkSJKlSvSzGOgeaNDcqcogrkEsA43lqMVqvF3kzuSxjEWJL+oKgtuVOh7FB8ogpFOaI0RY2KseaQDcqcirsOxJmdKAdRCzqdzsPDQ5SMqox4fkfSn5cLS5Qayg7FJ6pQ9CiUJgoUT2OsOWSDMqeiFuSzvHHSlyW7u7uxT5PLh2/hkfTH1d/IwyNlh+JDCapffRINCsUqlS3NGRuUOZV3VmLvpNVqMe71enH1CfIRFETLIkl/RL2k5FJD8aEEUYgoRxSlOMoLG5S5ZYMyp2LvhAHJz5i9lm63u7OzQ+HIezYUDlsTSV+E8pK7E8oOTylBFKK4Wjb6kjiaUhUtzR0blDnFfklkPrsssacyHA7j3cX5Jo9PT0/eQ1bSF6G8UGTymEdKUHw6D0WJ0hT7UTxNZUtzxgZlTrVarTjLEzsrVIH19XWPl0iaIkpQvN84qhN1iU6Fp6lsac7YoMwpMj9O7sQOCuODgwMbFElTRAna39+nKcmndRh4BGVu2aDMKdIesZvSarU2Njbu7u5sUCRNESXo5uZmfX2dHScKFJ1KFKuoWpo3NihzipyPAyfRoBwcHFAd6u/ckaQJiwvz9/f349QzDUqc5UllS3PGBmV+RebH4dOLiwuqg7c8kTRFsY90fn4eZ3nA7hOPUbI0b2xQ5hR9SVx9QvJvbGzc399TFzzFI2mKogTd3d1tbW11Oh1qlFfIzjMblDkVp3h5pAqcnp567ERSCehRXl5ezs/Pe71edCdxlFdzyAZlTpHz3W6XBqXf77O/Ql3wAhRJUxc7Sw8PD4PBgAYln4bWHLJBmVNx+KTVau3v70ddiM8IlKQpyoWI0kSBimO9qWxpztigzKl8fufs7CzKgZ9aLGnqciGiNMVlKDYoc8sGZX612+2tra3Hx8coB5JUDkoTBcqLZOeZDcqcYr9keXk5Pnzne4WB7+KRNEVRgnJFokBRpihWqWxpztigzCn2S7rd7s3NDVXg27dvcVg1ioIkTUWUIMpRXCpLgYoPN05lS3PGBmVOsVOyvb0d53coClEObFAkTVGUIMpRDChQlCmPoMwtG5Q5tbq6enZ2ljuS3KNI0hTl7gQMKFPe6n5u2aDMqW63e3t7SwnI1534Lh5JU5cLUZQmyhTFKpUtzRkblIZbrj4RMMaR561Wiz2Sg4OD/P4ddlnytWmxRJImL0oQ5Sgf0KVMUawoWRSuXMRAWfMGbo1ng9Jw5HA+QBopTZ6T5Kenp8/Pz1ECci2wQZE0RbkE5aJEmaJYUbIoXPXdLcqaDUrj2aA0XDQoPMZgqfrwreFweHNzM96X2KBImqLxWkSZolhRsihclK96NeMxlTk1lA1K8+WUXllZiTfsbW9v58Mnf1dibIMiaYpyCarXJYoVJYvCRfmiiEU1o6xV5U1NZoPSfDmlGbRarU6nc3JykpM/D1AfS9KEvVuOGFCyKFyUr3o1SwVOzWWD0nxkctxIgEGc34n370jSTKBkxVme6EsoaDYo88AGpeHY1YhH8jnO9Wxubr68vIyczfHYiaRCjJQjihUli8JF+aKIUcpyWauKnBrLBqXhcibnBmVnZ4ec//b21uJACbBHkTR1FKL67hNP43J+ChflywZlrtigzIs4Itrv98/PzyPzJWlWULgoX7mUaR7YoDRcHDtB7G1sbGxcX1+njJekGUHhonyN1zQ1mA1Kw7Xb7U6ns/p2H8b9/f18A1lJmhUULsoXRYxSRkGjrMVNE9RgNigNRzJ3u13yeWFhgZS+uLhI6S5JM4XyRRGjlFHQ4t6yqcypoWxQGo6dDDJ5ZWVlcXFxOBw+PDyQ514PK2mGRMmifFHEKGUUNMqaR1Aazwal4Zbfbrm4tLS0s7MT18PboEiaIVGyKF8UsbirE2XNa1Aazwal4djV4DE+w+L4+DiyfeQmKJJUslyyKGKUsnznyarIqbFsUBquU32Ccbvd7vV6+Q3GNiiSZkguWRQxShkFjbJGcUtlTg1lg9JwkcatVmswGLy8vESqe4pH0gyhZEWPQhGjlFHQYtcrlTk1lA1Kw61U2OHY3NyMC1DiUZJmSC5flDIKWlS2VObUUDYoDcd+xtLSErsaBwcHkedxHEWSZkguXJQyChplzVM8jWeD0nBxt4DV1dW4A8r379+fn58jzyVpVlC44iwPpYyCFnd4SmVODWWD0nBxAcpwOLy7uyO3v1WqfJekmZFrF6WMghaXoaQyp4ayQWm4uABlZ2cnDpzELogXyUqaIVGyonxRyihocRlKKnNqKBuUhluuHB0dkduR3vAyFEkzJJesqGMUtKhsqcypoWxQGo79DB5PT0/ZBcknd56enmIgSeXLJYsiRimjoOXipgazQWm+lZWVSO/4HON8RwFJmgmUrDjLE0WMgub5nXlgg9Jwy8vL3W43DpBGm0KeR6pL0kzIVSuKGAWNsuYpnsazQWm4dru9vr4eh0ziOtmc6pI0E3LVyhf7U9Y8xdN4NigNRw7v7e1VOf56+jYfKZWkGULhonzlC+koazYojWeD0nBLS0v5MwLJ8JzekjRb4grZGFPW4jON1WA2KA23urp6f3+fs9oGRdKMyuWLgkZZo7ilMqeGskFpuF6vR1a/vLzEZSiR4blfkaTyRcmK8kUpo6AxprilMqeGskFpuOFwSEo/Pz/HG3miTbFBkTRDomRF+aKUxaWyFLdU5tRQNigNsVJhEG+9i/HS0tLBwUEkcz46KkmzK0oZZY3iFpehjJS+eKoGsEFpiJyW9XsDMD46OopjJ7lB8fCJpJmTC1eUMspa3PA+FTsblCayQWmId3OShWdnZ3FcNB5jYI8iaYZQsuoVLB4pbj+re2mkGWeD0hDkJDsQiOSMQbfbvb6+rp++BfsfNiiSZgglKx8DjlLGEoobJS5K33gNVAPYoDTESHIuLS0xGAwGDw8P9axGnPGRpBmSC1cuZRQ3ShyFLl+JUq+BagAblIaILCU5V1dXY8xgfX09rpCFDYqk2TXeoFDcKHEUutgfY0ABjHEqi5pxNigNQU62222Ss9PpxJh03dzcjOOi9TO4+UipJM2K+imeOEnNEkochY5yR9Gj9FEAY5zKomacDUpDvNugbG9vRybboEiaaeMNCo+UOBuUBrNBaYjclMRTEnV5efns7CxSWpKahxIXp3Wi7uVmJZ5q1tmgNMRIg0LSIn9MoCQ1DyUual3UPRuUhrFBaYjcoESu8siSq6urlMeS1DiUOApdLno2KA1jg9IQ9QYldDqd29vblMeS1DiUOApdKnk2KI1jg9IQ4w1Kr9d7fHxMeSxJjUOJo9ClkmeD0jg2KM1BZsbORFwyNhgMfMOOpAajxFHoKHfxtgAKIGWwKodqAhuU5hhpUNbW1uLNeJLUSJQ4Ch3lzgalkWxQmiMaFBI1GpT19fWUxJLUUBQ6yl3UPRuUhrFBaYhut8sOBPJlKAcHBymDJamhKHRR8XLpoximsqgZZ4PSECMNCksODw9TBktSQ1HoKHe59MEGpTFsUBoirj7BSnVXAB6Pjo5SBktSQ1HoctGrSuDrlSipLGrG2aA0RD5wEllKip6cnKQMlqSGotDF7hmljwLIgGJYFUXNPBuU5ogGJQbdbvf09DRlsCQ1FIUuTnDn6hcDNYANSkNEWuZ9iH6/7wfxSGo8Ch3lbqQA8qgGsEFpiHhrceQn48FgcHFxkTJYkhqKQke5GymAPKoBbFAaIucnew/RoFxeXqYMlqSGotBFg0Lps0FpGBuU5lhdXSUz42rZ4XD49PSUMliSGopCR7mj6OUCmAqiZp8NSnPYoEiaNzYoDWaD0hw2KJLmjQ1Kg9mgNIcNiqR5Y4PSYDYozWGDImne2KA0mA1Kc9igSJo3NigNZoPSHDYokuaNDUqD2aA0hw2KpHljg9JgNigNQX6urKzUG5Tn5+eUwZLUUBS6eoMSN6tMZVEzzgalIWxQJM0hG5QGs0FpCBsUSXPIBqXBbFAawgZF0hyyQWkwG5SGsEGRNIdsUBrMBqUhbFAkzSEblAazQWkIGxRJc8gGpcFsUBrCBkXSHLJBaTAblIaIzIwGhcfBYOCN2iQ1HoWOcpdLX5TBVBY142xQGiJnZmRpv99/fHxMGSxJDUWho9zl0meD0iQ2KA2RM9MGRdL8sEFpMBuUhsiZSZa22+1er/fw8PD333+nJJakxqHEUegodxQ9G5TmsUFpiPEG5f7+/vv37ymPJalxKHEUOhuUprJBaYj8Lh4eW61Wt9u1QZHUbNGgUO4oerkA+i6exrBBaYjxBuXu7u7bt28pjyWpcShxFDoblKayQWmIkQal0+nYoEhqtmhQKHc2KI1kg9IQ5CRZmpOT8dHRUUpiSWooCh3lLmogBZCxDUpj2KA0xHiDcnh4mDJYkhqKQmeD0lQ2KA1BTq6urpKfjHlkvL+/nzJYkhqKQjdS+mxQGsMGpTnIzBhEru7u7qYMlqSGotDloodcBtUANijNQYrGQU4sLS1tb2+nDJakhqLQxbWxiAKYCqJmnw1Ko8SJHrTb7Y2NDe+DIqnBKHEUurhLGzy50zA2KA0R+w3sSUSitlqt9fV132YsqcEocRQ6yl3UPQpgLoZqABuUhojDm7lBWVxcXFtbs0GR1GCUOAod5S7qHgXQszzN8Z///H+TJT49UV7woAAAAABJRU5ErkJggg==",
                        disable = false
                    };
                    SetResponse res = await client1.SetAsync(path + uid, nguoi_Dung);
                    this.Hide();
                    Login login = new Login();
                    login.Show();
                    //this.Close();
                }
                else if (lengthMK == true)
                {
                    if (txtMK.Text.Length < 6)
                    {
                        lbMKlonhon5kytu.Visible = true;
                        ptrGreater.Visible = true;
                        ptrNotsame.Visible = false;
                        lbNotsame.Visible = false;
                    }
                    
                    //MessageBox.Show("Mật khẩu phải lớn hơn 5 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
