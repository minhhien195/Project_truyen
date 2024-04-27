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
    public partial class Danh_Gia_CTT : Form
    {
        public Danh_Gia_CTT()
        {
            InitializeComponent();
        }

        private void Danh_Gia_CTT_Load(object sender, EventArgs e)
        {
            pictureBox2.Width = (int)(0.1175 * panelRatingEvent.Width);
            pictureBox2.Height = pictureBox2.Width;
            richTextBoxRatingComment.Width = (int)(0.845 * panelRatingEvent.Width);
            richTextBoxRatingComment.Height = (int)(0.57 * panelRatingEvent.Height);
            textBoxRatingPoint.Location = new System.Drawing.Point(
                label3.Location.X + label3.Width,
                8
             );
            label1.Location = new System.Drawing.Point(
                textBoxRatingPoint.Location.X + textBoxRatingPoint.Width,
                8
             );

            pictureBox1.Location = new System.Drawing.Point(
                label1.Location.X + label1.Width,
                15
             );
            richTextBoxRatingComment.SizeChanged += (s, ev) =>
            {
                richTextBoxRatingComment.Width = (int)(0.845 * panelRatingEvent.Width);
                richTextBoxRatingComment.Height = (int)(0.57 * panelRatingEvent.Height);
            };
        }
    }
}
