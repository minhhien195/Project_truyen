namespace Login
{
    partial class Bookmark
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButtonDel = new FontAwesome.Sharp.IconButton();
            this.labelChuongDangDoc = new System.Windows.Forms.Label();
            this.labelTenTruyen = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.iconButtonDel);
            this.panel1.Controls.Add(this.labelChuongDangDoc);
            this.panel1.Controls.Add(this.labelTenTruyen);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1281, 183);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // iconButtonDel
            // 
            this.iconButtonDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButtonDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(44)))), ((int)(((byte)(36)))));
            this.iconButtonDel.FlatAppearance.BorderSize = 0;
            this.iconButtonDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonDel.Font = new System.Drawing.Font("League Spartan", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.iconButtonDel.ForeColor = System.Drawing.Color.White;
            this.iconButtonDel.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.iconButtonDel.IconColor = System.Drawing.Color.White;
            this.iconButtonDel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonDel.Location = new System.Drawing.Point(1063, 124);
            this.iconButtonDel.Name = "iconButtonDel";
            this.iconButtonDel.Size = new System.Drawing.Size(215, 56);
            this.iconButtonDel.TabIndex = 5;
            this.iconButtonDel.Text = "Xóa Bookmark";
            this.iconButtonDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonDel.UseVisualStyleBackColor = false;
            // 
            // labelChuongDangDoc
            // 
            this.labelChuongDangDoc.AutoSize = true;
            this.labelChuongDangDoc.Font = new System.Drawing.Font("League Spartan", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelChuongDangDoc.Location = new System.Drawing.Point(139, 78);
            this.labelChuongDangDoc.Name = "labelChuongDangDoc";
            this.labelChuongDangDoc.Size = new System.Drawing.Size(158, 41);
            this.labelChuongDangDoc.TabIndex = 3;
            this.labelChuongDangDoc.Text = "Chương 100";
            // 
            // labelTenTruyen
            // 
            this.labelTenTruyen.AutoSize = true;
            this.labelTenTruyen.Font = new System.Drawing.Font("League Spartan", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelTenTruyen.Location = new System.Drawing.Point(139, 3);
            this.labelTenTruyen.Name = "labelTenTruyen";
            this.labelTenTruyen.Size = new System.Drawing.Size(695, 45);
            this.labelTenTruyen.TabIndex = 2;
            this.labelTenTruyen.Text = "Theo Giang Hồ Bắt Đầu, Liều Thành Võ Đạo Chân Quân";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(130, 177);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Login.Properties.Resources.Album_truyen;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 177);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Bookmark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1281, 558);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Bookmark";
            this.Text = "Bookmark";
            this.Load += new System.EventHandler(this.Bookmark_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelChuongDangDoc;
        private System.Windows.Forms.Label labelTenTruyen;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton iconButtonDel;
    }
}