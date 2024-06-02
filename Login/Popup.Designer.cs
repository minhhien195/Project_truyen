namespace Login
{
    partial class Popup
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
            this.ibtnDecu = new FontAwesome.Sharp.IconButton();
            this.ibtnDanhgia = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // ibtnDecu
            // 
            this.ibtnDecu.AutoSize = true;
            this.ibtnDecu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ibtnDecu.FlatAppearance.BorderSize = 0;
            this.ibtnDecu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.ibtnDecu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.ibtnDecu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ibtnDecu.Font = new System.Drawing.Font("League Spartan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibtnDecu.IconChar = FontAwesome.Sharp.IconChar.Sun;
            this.ibtnDecu.IconColor = System.Drawing.Color.Black;
            this.ibtnDecu.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.ibtnDecu.IconSize = 38;
            this.ibtnDecu.Location = new System.Drawing.Point(745, 101);
            this.ibtnDecu.Name = "ibtnDecu";
            this.ibtnDecu.Size = new System.Drawing.Size(119, 84);
            this.ibtnDecu.TabIndex = 1;
            this.ibtnDecu.Text = "Đề cử";
            this.ibtnDecu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ibtnDecu.UseVisualStyleBackColor = false;
            this.ibtnDecu.Click += new System.EventHandler(this.ibtnDecu_Click);
            this.ibtnDecu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ibtnDecu_MouseClick);
            this.ibtnDecu.MouseHover += new System.EventHandler(this.ibtnDecu_MouseHover);
            // 
            // ibtnDanhgia
            // 
            this.ibtnDanhgia.AutoSize = true;
            this.ibtnDanhgia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ibtnDanhgia.FlatAppearance.BorderSize = 0;
            this.ibtnDanhgia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.ibtnDanhgia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.ibtnDanhgia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ibtnDanhgia.Font = new System.Drawing.Font("League Spartan", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibtnDanhgia.IconChar = FontAwesome.Sharp.IconChar.Star;
            this.ibtnDanhgia.IconColor = System.Drawing.Color.Black;
            this.ibtnDanhgia.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.ibtnDanhgia.IconSize = 38;
            this.ibtnDanhgia.Location = new System.Drawing.Point(275, 101);
            this.ibtnDanhgia.Name = "ibtnDanhgia";
            this.ibtnDanhgia.Size = new System.Drawing.Size(119, 84);
            this.ibtnDanhgia.TabIndex = 0;
            this.ibtnDanhgia.Text = "Đánh giá";
            this.ibtnDanhgia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ibtnDanhgia.UseVisualStyleBackColor = false;
            this.ibtnDanhgia.Click += new System.EventHandler(this.ibtnDanhgia_Click);
            this.ibtnDanhgia.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ibtnDanhgia_MouseClick);
            this.ibtnDanhgia.MouseHover += new System.EventHandler(this.ibtnDanhgia_MouseHover);
            // 
            // Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1377, 617);
            this.Controls.Add(this.ibtnDecu);
            this.Controls.Add(this.ibtnDanhgia);
            this.Name = "Popup";
            this.Text = "Popup";
            this.Load += new System.EventHandler(this.Popup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton ibtnDanhgia;
        private FontAwesome.Sharp.IconButton ibtnDecu;
    }
}