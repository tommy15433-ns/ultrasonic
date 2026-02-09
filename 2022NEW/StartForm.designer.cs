
namespace _2022_Test
{
    partial class StartForm
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
            this.BackColor = new System.Windows.Forms.Panel();
            this.Name_Label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SelfTest_BTN = new System.Windows.Forms.Button();
            this.Report_BTN = new System.Windows.Forms.Button();
            this.Test_BTN = new System.Windows.Forms.Button();
            this.Exit_BTN = new System.Windows.Forms.Button();
            this.Setting_BTN = new System.Windows.Forms.Button();
            this.panel_dxf = new System.Windows.Forms.Panel();
            this.BackColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BackColor
            // 
            this.BackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.BackColor.Controls.Add(this.Name_Label);
            this.BackColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.BackColor.Location = new System.Drawing.Point(0, 0);
            this.BackColor.Name = "BackColor";
            this.BackColor.Size = new System.Drawing.Size(1392, 45);
            this.BackColor.TabIndex = 0;
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name_Label.ForeColor = System.Drawing.Color.White;
            this.Name_Label.Location = new System.Drawing.Point(3, 10);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(74, 21);
            this.Name_Label.TabIndex = 6;
            this.Name_Label.Text = "시험기명";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.panel1.Location = new System.Drawing.Point(0, 366);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 45);
            this.panel1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Image = global::_2022_Test.Properties.Resources.icons8_documents_44;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(196, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 15);
            this.button1.TabIndex = 67;
            this.button1.Text = "장비이력관리";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::_2022_Test.Properties.Resources.엔에스텍_확정_문서용_;
            this.pictureBox1.Location = new System.Drawing.Point(34, 259);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 65;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SelfTest_BTN
            // 
            this.SelfTest_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.SelfTest_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelfTest_BTN.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.SelfTest_BTN.FlatAppearance.BorderSize = 0;
            this.SelfTest_BTN.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SelfTest_BTN.Image = global::_2022_Test.Properties.Resources.icons8_circular_arrows_44;
            this.SelfTest_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SelfTest_BTN.Location = new System.Drawing.Point(196, 165);
            this.SelfTest_BTN.Name = "SelfTest_BTN";
            this.SelfTest_BTN.Size = new System.Drawing.Size(150, 80);
            this.SelfTest_BTN.TabIndex = 64;
            this.SelfTest_BTN.Text = "자가진단";
            this.SelfTest_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SelfTest_BTN.UseVisualStyleBackColor = true;
            this.SelfTest_BTN.Click += new System.EventHandler(this.SelfTest_BTN_Click);
            // 
            // Report_BTN
            // 
            this.Report_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Report_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Report_BTN.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.Report_BTN.FlatAppearance.BorderSize = 0;
            this.Report_BTN.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Report_BTN.Image = global::_2022_Test.Properties.Resources.icons8_search_441;
            this.Report_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Report_BTN.Location = new System.Drawing.Point(196, 71);
            this.Report_BTN.Name = "Report_BTN";
            this.Report_BTN.Size = new System.Drawing.Size(150, 80);
            this.Report_BTN.TabIndex = 63;
            this.Report_BTN.Text = "결과조회";
            this.Report_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Report_BTN.UseVisualStyleBackColor = true;
            this.Report_BTN.Click += new System.EventHandler(this.Report_BTN_Click);
            // 
            // Test_BTN
            // 
            this.Test_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Test_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Test_BTN.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.Test_BTN.FlatAppearance.BorderSize = 0;
            this.Test_BTN.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Test_BTN.Image = global::_2022_Test.Properties.Resources.icons8_cashbook_441;
            this.Test_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Test_BTN.Location = new System.Drawing.Point(34, 71);
            this.Test_BTN.Name = "Test_BTN";
            this.Test_BTN.Size = new System.Drawing.Size(150, 80);
            this.Test_BTN.TabIndex = 62;
            this.Test_BTN.Text = "시험 시작";
            this.Test_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Test_BTN.UseVisualStyleBackColor = true;
            this.Test_BTN.Click += new System.EventHandler(this.Test_BTN_Click_1);
            // 
            // Exit_BTN
            // 
            this.Exit_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Exit_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_BTN.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.Exit_BTN.FlatAppearance.BorderSize = 0;
            this.Exit_BTN.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit_BTN.Image = global::_2022_Test.Properties.Resources.icons8_logout_44;
            this.Exit_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit_BTN.Location = new System.Drawing.Point(196, 259);
            this.Exit_BTN.Name = "Exit_BTN";
            this.Exit_BTN.Size = new System.Drawing.Size(150, 80);
            this.Exit_BTN.TabIndex = 61;
            this.Exit_BTN.Text = "종료";
            this.Exit_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Exit_BTN.UseVisualStyleBackColor = true;
            this.Exit_BTN.Click += new System.EventHandler(this.Exit_BTN_Click);
            // 
            // Setting_BTN
            // 
            this.Setting_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Setting_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Setting_BTN.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.Setting_BTN.FlatAppearance.BorderSize = 0;
            this.Setting_BTN.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Setting_BTN.Image = global::_2022_Test.Properties.Resources.icons8_settings_44_2;
            this.Setting_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Setting_BTN.Location = new System.Drawing.Point(34, 165);
            this.Setting_BTN.Name = "Setting_BTN";
            this.Setting_BTN.Size = new System.Drawing.Size(150, 80);
            this.Setting_BTN.TabIndex = 60;
            this.Setting_BTN.Text = "환경설정";
            this.Setting_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Setting_BTN.UseVisualStyleBackColor = true;
            this.Setting_BTN.Click += new System.EventHandler(this.Setting_BTN_Click);
            // 
            // panel_dxf
            // 
            this.panel_dxf.Location = new System.Drawing.Point(21, 439);
            this.panel_dxf.Name = "panel_dxf";
            this.panel_dxf.Size = new System.Drawing.Size(1140, 387);
            this.panel_dxf.TabIndex = 68;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 908);
            this.Controls.Add(this.panel_dxf);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SelfTest_BTN);
            this.Controls.Add(this.Report_BTN);
            this.Controls.Add(this.Test_BTN);
            this.Controls.Add(this.Exit_BTN);
            this.Controls.Add(this.Setting_BTN);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BackColor);
            this.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartForm_FormClosed);
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.BackColor.ResumeLayout(false);
            this.BackColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel BackColor;
        private System.Windows.Forms.Label Name_Label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button SelfTest_BTN;
        private System.Windows.Forms.Button Report_BTN;
        private System.Windows.Forms.Button Test_BTN;
        private System.Windows.Forms.Button Exit_BTN;
        private System.Windows.Forms.Button Setting_BTN;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel_dxf;
    }
}