using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Change_Password : Form
    {
        public Change_Password()
        {
            InitializeComponent();
        }


        private void Change_Password_Load(object sender, EventArgs e)
        {
            //Bo góc panel
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            //Bo góc MK
            System.Drawing.Drawing2D.GraphicsPath path1 = new System.Drawing.Drawing2D.GraphicsPath();
            //Bo góc XNMK
            System.Drawing.Drawing2D.GraphicsPath path2 = new System.Drawing.Drawing2D.GraphicsPath();
            //Bo góc Xác nhận
            System.Drawing.Drawing2D.GraphicsPath path3 = new System.Drawing.Drawing2D.GraphicsPath();
            int cornerRadius = 20; // Điều chỉnh giá trị này để thay đổi bán kính bo góc

            // Thêm hình dạng bo góc vào GraphicsPath
            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(panel1.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(panel1.Width - cornerRadius, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(0, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();

            int cornerRadius1 = 8;

            // Bo góc mật khẩu
            path1.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
            path1.AddArc(txtMK.Width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
            path1.AddArc(txtMK.Width - cornerRadius1, txtMK.Height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
            path1.AddArc(0, txtMK.Height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
            path1.CloseAllFigures();

            //Bo góc xác nhận mật khẩu
            path2.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
            path2.AddArc(txtXNMK.Width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
            path2.AddArc(txtXNMK.Width - cornerRadius1, txtXNMK.Height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
            path2.AddArc(0, txtXNMK.Height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
            path2.CloseAllFigures();

            //Bo góc nút Đăng ký
            path3.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path3.AddArc(btnXacnhan.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path3.AddArc(btnXacnhan.Width - cornerRadius, btnXacnhan.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path3.AddArc(0, btnXacnhan.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path3.CloseAllFigures();

            // Thiết lập Region của Panel bằng GraphicsPath
            panel1.Region = new Region(path);
            txtMK.Region = new Region(path1);
            txtXNMK.Region = new Region(path2);
            btnXacnhan.Region = new Region(path3);

            //Căn giữa panel
            panel1.Location = new Point(
                (this.Width - panel1.Width) / 2, // Tính toán vị trí theo trục X
                (this.Height - panel1.Height) / 2 // Tính toán vị trí theo trục Y
            );
            panel1.Anchor = AnchorStyles.None; // Đảm bảo label không bị ràng buộc bởi các thuộc tính Anchor



            //Căn giữa signup label
            Changepwd_label.Location = new Point(
                (panel1.Width - Changepwd_label.Width) / 2, // Tính toán vị trí theo trục X
                13
            );
            Changepwd_label.Anchor = AnchorStyles.None; // Đảm bảo label không bị ràng buộc bởi các thuộc tính Anchor
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

        private void ptrView1_Click(object sender, EventArgs e)
        {
            ptrHide1.BringToFront();
            if (txtXNMK.PasswordChar == '\0')
            {
                txtXNMK.PasswordChar = '*';
            }
        }
    }
}
