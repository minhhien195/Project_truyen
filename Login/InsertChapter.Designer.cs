namespace Login
{
    partial class InsertChapter
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
            this.ibtnDangtruyen = new FontAwesome.Sharp.IconButton();
            this.lbspace = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ptrTruyen = new System.Windows.Forms.PictureBox();
            this.lbT = new System.Windows.Forms.Label();
            this.txtTruyen = new System.Windows.Forms.TextBox();
            this.lbTacgia = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ptrSochuong = new System.Windows.Forms.PictureBox();
            this.lbSC = new System.Windows.Forms.Label();
            this.txtChuong = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ptrTenchuong = new System.Windows.Forms.PictureBox();
            this.lbTC = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ptrThemchuongmoi = new System.Windows.Forms.PictureBox();
            this.lbTCM = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ibtninsertDoc = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrTruyen)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrSochuong)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrTenchuong)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrThemchuongmoi)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.ibtnDangtruyen);
            this.panel1.Controls.Add(this.lbspace);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1222, 77);
            this.panel1.TabIndex = 8;
            // 
            // ibtnDangtruyen
            // 
            this.ibtnDangtruyen.AutoSize = true;
            this.ibtnDangtruyen.FlatAppearance.BorderSize = 0;
            this.ibtnDangtruyen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ibtnDangtruyen.Font = new System.Drawing.Font("League Spartan Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibtnDangtruyen.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.ibtnDangtruyen.IconColor = System.Drawing.Color.Black;
            this.ibtnDangtruyen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ibtnDangtruyen.IconSize = 38;
            this.ibtnDangtruyen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ibtnDangtruyen.Location = new System.Drawing.Point(3, 3);
            this.ibtnDangtruyen.Name = "ibtnDangtruyen";
            this.ibtnDangtruyen.Size = new System.Drawing.Size(302, 71);
            this.ibtnDangtruyen.TabIndex = 0;
            this.ibtnDangtruyen.Text = "Thêm chương";
            this.ibtnDangtruyen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ibtnDangtruyen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ibtnDangtruyen.UseVisualStyleBackColor = true;
            this.ibtnDangtruyen.Click += new System.EventHandler(this.ibtnDangtruyen_Click);
            // 
            // lbspace
            // 
            this.lbspace.BackColor = System.Drawing.Color.Gray;
            this.lbspace.Font = new System.Drawing.Font("League Spartan", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbspace.Location = new System.Drawing.Point(-6, 52);
            this.lbspace.Name = "lbspace";
            this.lbspace.Size = new System.Drawing.Size(1186, 2);
            this.lbspace.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.ptrTruyen);
            this.panel3.Controls.Add(this.lbT);
            this.panel3.Controls.Add(this.txtTruyen);
            this.panel3.Controls.Add(this.lbTacgia);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 77);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1222, 107);
            this.panel3.TabIndex = 10;
            // 
            // ptrTruyen
            // 
            this.ptrTruyen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ptrTruyen.Image = global::Login.Properties.Resources.exclamation;
            this.ptrTruyen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ptrTruyen.Location = new System.Drawing.Point(20, 83);
            this.ptrTruyen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ptrTruyen.Name = "ptrTruyen";
            this.ptrTruyen.Size = new System.Drawing.Size(22, 22);
            this.ptrTruyen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptrTruyen.TabIndex = 26;
            this.ptrTruyen.TabStop = false;
            this.ptrTruyen.Visible = false;
            // 
            // lbT
            // 
            this.lbT.AutoSize = true;
            this.lbT.Font = new System.Drawing.Font("League Spartan SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.lbT.ForeColor = System.Drawing.Color.IndianRed;
            this.lbT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbT.Location = new System.Drawing.Point(46, 83);
            this.lbT.Name = "lbT";
            this.lbT.Size = new System.Drawing.Size(183, 23);
            this.lbT.TabIndex = 25;
            this.lbT.Text = "Vui lòng nhập tên truyện";
            this.lbT.Visible = false;
            // 
            // txtTruyen
            // 
            this.txtTruyen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.txtTruyen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTruyen.Font = new System.Drawing.Font("League Spartan", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTruyen.Location = new System.Drawing.Point(18, 38);
            this.txtTruyen.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.txtTruyen.Multiline = true;
            this.txtTruyen.Name = "txtTruyen";
            this.txtTruyen.Size = new System.Drawing.Size(1168, 35);
            this.txtTruyen.TabIndex = 6;
            this.txtTruyen.TextChanged += new System.EventHandler(this.txtTruyen_TextChanged);
            // 
            // lbTacgia
            // 
            this.lbTacgia.AutoSize = true;
            this.lbTacgia.Font = new System.Drawing.Font("League Spartan Medium", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTacgia.Location = new System.Drawing.Point(12, 3);
            this.lbTacgia.Name = "lbTacgia";
            this.lbTacgia.Size = new System.Drawing.Size(77, 32);
            this.lbTacgia.TabIndex = 5;
            this.lbTacgia.Text = "Truyện";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.ptrSochuong);
            this.panel2.Controls.Add(this.lbSC);
            this.panel2.Controls.Add(this.txtChuong);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 184);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1222, 110);
            this.panel2.TabIndex = 11;
            // 
            // ptrSochuong
            // 
            this.ptrSochuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ptrSochuong.Image = global::Login.Properties.Resources.exclamation;
            this.ptrSochuong.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ptrSochuong.Location = new System.Drawing.Point(20, 86);
            this.ptrSochuong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ptrSochuong.Name = "ptrSochuong";
            this.ptrSochuong.Size = new System.Drawing.Size(22, 22);
            this.ptrSochuong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptrSochuong.TabIndex = 26;
            this.ptrSochuong.TabStop = false;
            this.ptrSochuong.Visible = false;
            // 
            // lbSC
            // 
            this.lbSC.AutoSize = true;
            this.lbSC.Font = new System.Drawing.Font("League Spartan SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.lbSC.ForeColor = System.Drawing.Color.IndianRed;
            this.lbSC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbSC.Location = new System.Drawing.Point(46, 86);
            this.lbSC.Name = "lbSC";
            this.lbSC.Size = new System.Drawing.Size(182, 23);
            this.lbSC.TabIndex = 25;
            this.lbSC.Text = "Vui lòng nhập số chương";
            this.lbSC.Visible = false;
            // 
            // txtChuong
            // 
            this.txtChuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.txtChuong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChuong.Font = new System.Drawing.Font("League Spartan", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChuong.Location = new System.Drawing.Point(18, 38);
            this.txtChuong.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.txtChuong.Multiline = true;
            this.txtChuong.Name = "txtChuong";
            this.txtChuong.Size = new System.Drawing.Size(1168, 35);
            this.txtChuong.TabIndex = 6;
            this.txtChuong.TextChanged += new System.EventHandler(this.txtChuong_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("League Spartan Medium", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Số chương";
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.ptrTenchuong);
            this.panel4.Controls.Add(this.lbTC);
            this.panel4.Controls.Add(this.txtTen);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 294);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1222, 110);
            this.panel4.TabIndex = 12;
            // 
            // ptrTenchuong
            // 
            this.ptrTenchuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ptrTenchuong.Image = global::Login.Properties.Resources.exclamation;
            this.ptrTenchuong.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ptrTenchuong.Location = new System.Drawing.Point(20, 86);
            this.ptrTenchuong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ptrTenchuong.Name = "ptrTenchuong";
            this.ptrTenchuong.Size = new System.Drawing.Size(22, 22);
            this.ptrTenchuong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptrTenchuong.TabIndex = 26;
            this.ptrTenchuong.TabStop = false;
            this.ptrTenchuong.Visible = false;
            // 
            // lbTC
            // 
            this.lbTC.AutoSize = true;
            this.lbTC.Font = new System.Drawing.Font("League Spartan SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.lbTC.ForeColor = System.Drawing.Color.IndianRed;
            this.lbTC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbTC.Location = new System.Drawing.Point(46, 86);
            this.lbTC.Name = "lbTC";
            this.lbTC.Size = new System.Drawing.Size(190, 23);
            this.lbTC.TabIndex = 25;
            this.lbTC.Text = "Vui lòng nhập tên chương";
            this.lbTC.Visible = false;
            // 
            // txtTen
            // 
            this.txtTen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.txtTen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTen.Font = new System.Drawing.Font("League Spartan", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.Location = new System.Drawing.Point(18, 38);
            this.txtTen.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.txtTen.Multiline = true;
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(1168, 35);
            this.txtTen.TabIndex = 6;
            this.txtTen.TextChanged += new System.EventHandler(this.txtTen_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("League Spartan Medium", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tên Chương";
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.Controls.Add(this.ptrThemchuongmoi);
            this.panel6.Controls.Add(this.lbTCM);
            this.panel6.Controls.Add(this.btnAdd);
            this.panel6.Controls.Add(this.ibtninsertDoc);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 404);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1222, 119);
            this.panel6.TabIndex = 13;
            // 
            // ptrThemchuongmoi
            // 
            this.ptrThemchuongmoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ptrThemchuongmoi.Image = global::Login.Properties.Resources.exclamation;
            this.ptrThemchuongmoi.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ptrThemchuongmoi.Location = new System.Drawing.Point(20, 74);
            this.ptrThemchuongmoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ptrThemchuongmoi.Name = "ptrThemchuongmoi";
            this.ptrThemchuongmoi.Size = new System.Drawing.Size(22, 22);
            this.ptrThemchuongmoi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptrThemchuongmoi.TabIndex = 26;
            this.ptrThemchuongmoi.TabStop = false;
            this.ptrThemchuongmoi.Visible = false;
            // 
            // lbTCM
            // 
            this.lbTCM.AutoSize = true;
            this.lbTCM.Font = new System.Drawing.Font("League Spartan SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.lbTCM.ForeColor = System.Drawing.Color.IndianRed;
            this.lbTCM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbTCM.Location = new System.Drawing.Point(46, 74);
            this.lbTCM.Name = "lbTCM";
            this.lbTCM.Size = new System.Drawing.Size(192, 23);
            this.lbTCM.TabIndex = 25;
            this.lbTCM.Text = "Vui lòng thêm chương mới";
            this.lbTCM.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(44)))), ((int)(((byte)(36)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("League Spartan Medium", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Location = new System.Drawing.Point(1042, 54);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(213, 62);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Thêm Chương";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ibtninsertDoc
            // 
            this.ibtninsertDoc.AutoSize = true;
            this.ibtninsertDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ibtninsertDoc.FlatAppearance.BorderSize = 0;
            this.ibtninsertDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ibtninsertDoc.Font = new System.Drawing.Font("League Spartan Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibtninsertDoc.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.ibtninsertDoc.IconColor = System.Drawing.Color.Black;
            this.ibtninsertDoc.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ibtninsertDoc.IconSize = 38;
            this.ibtninsertDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ibtninsertDoc.Location = new System.Drawing.Point(18, 15);
            this.ibtninsertDoc.Name = "ibtninsertDoc";
            this.ibtninsertDoc.Size = new System.Drawing.Size(556, 71);
            this.ibtninsertDoc.TabIndex = 0;
            this.ibtninsertDoc.Text = "Thêm chương(.doc, .docx, .txt)";
            this.ibtninsertDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ibtninsertDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ibtninsertDoc.UseVisualStyleBackColor = false;
            this.ibtninsertDoc.Click += new System.EventHandler(this.ibtninsertDoc_Click);
            // 
            // InsertChapter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1222, 549);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "InsertChapter";
            this.Text = "InsertChapter";
            this.Load += new System.EventHandler(this.InsertChapter_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrTruyen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrSochuong)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrTenchuong)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrThemchuongmoi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton ibtnDangtruyen;
        private System.Windows.Forms.Label lbspace;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtTruyen;
        private System.Windows.Forms.Label lbTacgia;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtChuong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnAdd;
        private FontAwesome.Sharp.IconButton ibtninsertDoc;
        private System.Windows.Forms.PictureBox ptrTruyen;
        private System.Windows.Forms.Label lbT;
        private System.Windows.Forms.PictureBox ptrSochuong;
        private System.Windows.Forms.Label lbSC;
        private System.Windows.Forms.PictureBox ptrTenchuong;
        private System.Windows.Forms.Label lbTC;
        private System.Windows.Forms.PictureBox ptrThemchuongmoi;
        private System.Windows.Forms.Label lbTCM;
    }
}