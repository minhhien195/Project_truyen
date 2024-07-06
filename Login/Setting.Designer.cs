namespace Login
{
    partial class Setting
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.tbcurpw = new System.Windows.Forms.TextBox();
            this.tbrenewpw = new System.Windows.Forms.TextBox();
            this.tbnewpw = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbNotsame = new System.Windows.Forms.Label();
            this.ptrNotsame = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrNotsame)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Location = new System.Drawing.Point(21, 14);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1360, 63);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbcurpw, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbrenewpw, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tbnewpw, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(21, 92);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1045, 245);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("League Spartan SemiBold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1041, 28);
            this.label7.TabIndex = 0;
            this.label7.Text = "Thay đổi mật khẩu";
            // 
            // tbcurpw
            // 
            this.tbcurpw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcurpw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.tbcurpw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbcurpw.Font = new System.Drawing.Font("League Spartan SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcurpw.Location = new System.Drawing.Point(0, 73);
            this.tbcurpw.Margin = new System.Windows.Forms.Padding(0);
            this.tbcurpw.Multiline = true;
            this.tbcurpw.Name = "tbcurpw";
            this.tbcurpw.Size = new System.Drawing.Size(1045, 37);
            this.tbcurpw.TabIndex = 1;
            this.tbcurpw.Text = "Mật khẩu hiện tại";
            this.tbcurpw.TextChanged += new System.EventHandler(this.tbcurpw_TextChanged);
            // 
            // tbrenewpw
            // 
            this.tbrenewpw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbrenewpw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.tbrenewpw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbrenewpw.Font = new System.Drawing.Font("League Spartan SemiBold", 16.2F, System.Drawing.FontStyle.Bold);
            this.tbrenewpw.Location = new System.Drawing.Point(2, 195);
            this.tbrenewpw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbrenewpw.Multiline = true;
            this.tbrenewpw.Name = "tbrenewpw";
            this.tbrenewpw.Size = new System.Drawing.Size(1041, 37);
            this.tbrenewpw.TabIndex = 3;
            this.tbrenewpw.Text = "Nhập lại mật khẩu mới";
            this.tbrenewpw.TextChanged += new System.EventHandler(this.txtXNMK_TextChanged);
            // 
            // tbnewpw
            // 
            this.tbnewpw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbnewpw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.tbnewpw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbnewpw.Font = new System.Drawing.Font("League Spartan SemiBold", 16.2F, System.Drawing.FontStyle.Bold);
            this.tbnewpw.Location = new System.Drawing.Point(2, 134);
            this.tbnewpw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbnewpw.Multiline = true;
            this.tbnewpw.Name = "tbnewpw";
            this.tbnewpw.Size = new System.Drawing.Size(1041, 37);
            this.tbnewpw.TabIndex = 2;
            this.tbnewpw.Text = "Mật khẩu mới";
            this.tbnewpw.TextChanged += new System.EventHandler(this.txtMK_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("League Spartan SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(929, 370);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 47);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cập nhật";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbNotsame
            // 
            this.lbNotsame.AutoSize = true;
            this.lbNotsame.Font = new System.Drawing.Font("League Spartan SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.lbNotsame.ForeColor = System.Drawing.Color.IndianRed;
            this.lbNotsame.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbNotsame.Location = new System.Drawing.Point(45, 339);
            this.lbNotsame.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNotsame.Name = "lbNotsame";
            this.lbNotsame.Size = new System.Drawing.Size(140, 17);
            this.lbNotsame.TabIndex = 25;
            this.lbNotsame.Text = "Mật khẩu không trùng khớp";
            this.lbNotsame.Visible = false;
            // 
            // ptrNotsame
            // 
            this.ptrNotsame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ptrNotsame.Image = global::Login.Properties.Resources.exclamation;
            this.ptrNotsame.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ptrNotsame.Location = new System.Drawing.Point(27, 342);
            this.ptrNotsame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ptrNotsame.Name = "ptrNotsame";
            this.ptrNotsame.Size = new System.Drawing.Size(15, 15);
            this.ptrNotsame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptrNotsame.TabIndex = 26;
            this.ptrNotsame.TabStop = false;
            this.ptrNotsame.Visible = false;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1110, 461);
            this.Controls.Add(this.ptrNotsame);
            this.Controls.Add(this.lbNotsame);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Setting";
            this.Text = "Setting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptrNotsame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox tbrenewpw;
        private System.Windows.Forms.TextBox tbnewpw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbcurpw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbNotsame;
        private System.Windows.Forms.PictureBox ptrNotsame;
    }
}