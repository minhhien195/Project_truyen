namespace Login
{
    partial class Inserted
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
            this.lbTruyen = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lbTrangthai = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbChuong = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelTruyendang = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnInsertChap = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelTruyendang.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTruyen
            // 
            this.lbTruyen.AutoSize = true;
            this.lbTruyen.Font = new System.Drawing.Font("League Spartan Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTruyen.Location = new System.Drawing.Point(8, 4);
            this.lbTruyen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTruyen.Name = "lbTruyen";
            this.lbTruyen.Size = new System.Drawing.Size(55, 23);
            this.lbTruyen.TabIndex = 0;
            this.lbTruyen.Text = "Truyện";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("League Spartan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Tạm Dừng",
            "Đang Tiến Hành",
            "Hoàn Thành"});
            this.comboBox1.Location = new System.Drawing.Point(639, 8);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 31);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Text = "Tạm Dừng";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lbTrangthai
            // 
            this.lbTrangthai.AutoSize = true;
            this.lbTrangthai.Font = new System.Drawing.Font("League Spartan Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTrangthai.Location = new System.Drawing.Point(635, 4);
            this.lbTrangthai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTrangthai.Name = "lbTrangthai";
            this.lbTrangthai.Size = new System.Drawing.Size(82, 23);
            this.lbTrangthai.TabIndex = 2;
            this.lbTrangthai.Text = "Trạng Thái";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("League Spartan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Quang âm chi ngoại";
            // 
            // lbChuong
            // 
            this.lbChuong.AutoSize = true;
            this.lbChuong.Font = new System.Drawing.Font("League Spartan Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChuong.Location = new System.Drawing.Point(408, 6);
            this.lbChuong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbChuong.Name = "lbChuong";
            this.lbChuong.Size = new System.Drawing.Size(84, 23);
            this.lbChuong.TabIndex = 1;
            this.lbChuong.Text = "Số Chương";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("League Spartan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(408, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "1222 chương";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.lbTruyen);
            this.panel1.Controls.Add(this.lbChuong);
            this.panel1.Controls.Add(this.lbTrangthai);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(815, 29);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(815, 41);
            this.panel2.TabIndex = 7;
            this.panel2.Visible = false;
            // 
            // panelTruyendang
            // 
            this.panelTruyendang.AutoSize = true;
            this.panelTruyendang.Controls.Add(this.panel2);
            this.panelTruyendang.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTruyendang.Location = new System.Drawing.Point(0, 29);
            this.panelTruyendang.Margin = new System.Windows.Forms.Padding(2);
            this.panelTruyendang.Name = "panelTruyendang";
            this.panelTruyendang.Size = new System.Drawing.Size(815, 41);
            this.panelTruyendang.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.tableLayoutPanel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 70);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(815, 58);
            this.panel4.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnInsertChap, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUpdate, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(557, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(229, 52);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // btnInsertChap
            // 
            this.btnInsertChap.AutoSize = true;
            this.btnInsertChap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(44)))), ((int)(((byte)(36)))));
            this.btnInsertChap.FlatAppearance.BorderSize = 0;
            this.btnInsertChap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertChap.Font = new System.Drawing.Font("League Spartan", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertChap.ForeColor = System.Drawing.SystemColors.Control;
            this.btnInsertChap.Location = new System.Drawing.Point(2, 2);
            this.btnInsertChap.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsertChap.Name = "btnInsertChap";
            this.btnInsertChap.Size = new System.Drawing.Size(110, 48);
            this.btnInsertChap.TabIndex = 1;
            this.btnInsertChap.Text = "Thêm chương";
            this.btnInsertChap.UseVisualStyleBackColor = false;
            this.btnInsertChap.Click += new System.EventHandler(this.btnInsertChap_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("League Spartan", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Location = new System.Drawing.Point(116, 2);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(111, 48);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Inserted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(815, 355);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelTruyendang);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Inserted";
            this.Text = "Inserted";
            this.Load += new System.EventHandler(this.Inserted_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelTruyendang.ResumeLayout(false);
            this.panelTruyendang.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTruyen;
        private System.Windows.Forms.Label lbTrangthai;
        private System.Windows.Forms.Label lbChuong;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelTruyendang;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnInsertChap;
        private System.Windows.Forms.Button btnUpdate;
    }
}