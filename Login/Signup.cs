using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //Bo góc panel
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int cornerRadius = 20; // Điều chỉnh giá trị này để thay đổi bán kính bo góc

            // Thêm hình dạng bo góc vào GraphicsPath
            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(panel1.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(panel1.Width - cornerRadius, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(0, panel1.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();

            // Thiết lập Region của Panel bằng GraphicsPath
            panel1.Region = new Region(path);


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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
