namespace Login
{
    partial class Login
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
            this.showPwd = new System.Windows.Forms.PictureBox();
            this.hidePwd = new System.Windows.Forms.PictureBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMK = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ptrWarning = new System.Windows.Forms.PictureBox();
            this.lbemailKhonghople = new System.Windows.Forms.Label();
            this.ptrWarning1 = new System.Windows.Forms.PictureBox();
            this.labelMKinvalid = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showPwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hidePwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptrWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptrWarning1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.labelMKinvalid);
            this.panel1.Controls.Add(this.ptrWarning1);
            this.panel1.Controls.Add(this.lbemailKhonghople);
            this.panel1.Controls.Add(this.ptrWarning);
            this.panel1.Controls.Add(this.hidePwd);
            this.panel1.Controls.Add(this.linkLabel2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.showPwd);
            this.panel1.Controls.Add(this.txtMK);
            this.panel1.Location = new System.Drawing.Point(634, 205);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 516);
            this.panel1.TabIndex = 0;
            // 
            // showPwd
            // 
            this.showPwd.BackColor = System.Drawing.Color.White;
            this.showPwd.Image = global::Login.Properties.Resources.view;
            this.showPwd.Location = new System.Drawing.Point(496, 231);
            this.showPwd.Name = "showPwd";
            this.showPwd.Size = new System.Drawing.Size(43, 35);
            this.showPwd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.showPwd.TabIndex = 10;
            this.showPwd.TabStop = false;
            this.showPwd.Click += new System.EventHandler(this.showPwd_Click);
            // 
            // hidePwd
            // 
            this.hidePwd.BackColor = System.Drawing.Color.White;
            this.hidePwd.Image = global::Login.Properties.Resources.hide;
            this.hidePwd.Location = new System.Drawing.Point(496, 231);
            this.hidePwd.Name = "hidePwd";
            this.hidePwd.Size = new System.Drawing.Size(43, 35);
            this.hidePwd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.hidePwd.TabIndex = 1;
            this.hidePwd.TabStop = false;
            this.hidePwd.Click += new System.EventHandler(this.hidePwd_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.Red;
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("League Spartan", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabel2.Location = new System.Drawing.Point(349, 439);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(91, 35);
            this.linkLabel2.TabIndex = 9;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Đăng ký";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("League Spartan", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(164, 439);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 35);
            this.label4.TabIndex = 8;
            this.label4.Text = "Người dùng mới?";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Red;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("League Spartan", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabel1.Location = new System.Drawing.Point(390, 284);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(161, 35);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Quên mật khẩu";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("League Spartan SemiBold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(66, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 35);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mật khẩu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("League Spartan SemiBold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(66, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 35);
            this.label2.TabIndex = 4;
            this.label2.Text = "Email";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("League Spartan SemiBold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(72, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(479, 56);
            this.button1.TabIndex = 3;
            this.button1.Text = "Đăng nhập";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMK
            // 
            this.txtMK.Font = new System.Drawing.Font("League Spartan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMK.Location = new System.Drawing.Point(72, 231);
            this.txtMK.Multiline = true;
            this.txtMK.Name = "txtMK";
            this.txtMK.PasswordChar = '*';
            this.txtMK.Size = new System.Drawing.Size(479, 38);
            this.txtMK.TabIndex = 2;
            this.txtMK.TextChanged += new System.EventHandler(this.txtMK_TextChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("League Spartan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtEmail.Location = new System.Drawing.Point(72, 134);
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(479, 36);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            this.txtEmail.MouseLeave += new System.EventHandler(this.txtEmail_MouseLeave);
            this.txtEmail.MouseHover += new System.EventHandler(this.txtEmail_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("League Spartan", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(208, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đăng nhập";
            // 
            // ptrWarning
            // 
            this.ptrWarning.Image = global::Login.Properties.Resources.exclamation;
            this.ptrWarning.Location = new System.Drawing.Point(72, 176);
            this.ptrWarning.Name = "ptrWarning";
            this.ptrWarning.Size = new System.Drawing.Size(28, 24);
            this.ptrWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptrWarning.TabIndex = 11;
            this.ptrWarning.TabStop = false;
            this.ptrWarning.Visible = false;
            // 
            // lbemailKhonghople
            // 
            this.lbemailKhonghople.AutoSize = true;
            this.lbemailKhonghople.Font = new System.Drawing.Font("League Spartan SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.lbemailKhonghople.ForeColor = System.Drawing.Color.IndianRed;
            this.lbemailKhonghople.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbemailKhonghople.Location = new System.Drawing.Point(106, 179);
            this.lbemailKhonghople.Name = "lbemailKhonghople";
            this.lbemailKhonghople.Size = new System.Drawing.Size(124, 21);
            this.lbemailKhonghople.TabIndex = 25;
            this.lbemailKhonghople.Text = "Email không hợp lệ";
            this.lbemailKhonghople.Visible = false;
            // 
            // ptrWarning1
            // 
            this.ptrWarning1.Image = global::Login.Properties.Resources.exclamation;
            this.ptrWarning1.Location = new System.Drawing.Point(72, 281);
            this.ptrWarning1.Name = "ptrWarning1";
            this.ptrWarning1.Size = new System.Drawing.Size(28, 24);
            this.ptrWarning1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptrWarning1.TabIndex = 26;
            this.ptrWarning1.TabStop = false;
            this.ptrWarning1.Visible = false;
            // 
            // labelMKinvalid
            // 
            this.labelMKinvalid.AutoSize = true;
            this.labelMKinvalid.Font = new System.Drawing.Font("League Spartan SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.labelMKinvalid.ForeColor = System.Drawing.Color.IndianRed;
            this.labelMKinvalid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMKinvalid.Location = new System.Drawing.Point(106, 284);
            this.labelMKinvalid.Name = "labelMKinvalid";
            this.labelMKinvalid.Size = new System.Drawing.Size(150, 21);
            this.labelMKinvalid.TabIndex = 28;
            this.labelMKinvalid.Text = "Mật khẩu không hợp lệ";
            this.labelMKinvalid.Visible = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.BackgroundImage = global::Login.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("League Spartan", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Login";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showPwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hidePwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptrWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptrWarning1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMK;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox hidePwd;
        private System.Windows.Forms.PictureBox showPwd;
        private System.Windows.Forms.PictureBox ptrWarning;
        private System.Windows.Forms.PictureBox ptrWarning1;
        private System.Windows.Forms.Label lbemailKhonghople;
        private System.Windows.Forms.Label labelMKinvalid;
    }
}

