namespace _2022_Test
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.imageList_nusul = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.EXIT_BTN = new System.Windows.Forms.Button();
            this.RESET_BTN = new System.Windows.Forms.Button();
            this.SAVE_BTN = new System.Windows.Forms.Button();
            this.STOP_BTN = new System.Windows.Forms.Button();
            this.START_BTN = new System.Windows.Forms.Button();
            this.Test_Name_Label = new System.Windows.Forms.Label();
            this.BackColor = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.BackColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList_nusul
            // 
            this.imageList_nusul.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_nusul.ImageSize = new System.Drawing.Size(35, 35);
            this.imageList_nusul.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(35, 40);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // serialPort1
            // 
            this.serialPort1.Parity = System.IO.Ports.Parity.Even;
            this.serialPort1.PortName = "COM3";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(31, 601);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 39;
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(45, 40);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Image = global::_2022_Test.Properties.Resources.icons8_search_441;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(390, 912);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 75);
            this.button1.TabIndex = 198;
            this.button1.TabStop = false;
            this.button1.Text = "결과조회";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // EXIT_BTN
            // 
            this.EXIT_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EXIT_BTN.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EXIT_BTN.Image = global::_2022_Test.Properties.Resources.icons8_logout_444;
            this.EXIT_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.EXIT_BTN.Location = new System.Drawing.Point(1522, 912);
            this.EXIT_BTN.Name = "EXIT_BTN";
            this.EXIT_BTN.Size = new System.Drawing.Size(125, 75);
            this.EXIT_BTN.TabIndex = 195;
            this.EXIT_BTN.TabStop = false;
            this.EXIT_BTN.Text = "복귀";
            this.EXIT_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.EXIT_BTN.UseVisualStyleBackColor = true;
            // 
            // RESET_BTN
            // 
            this.RESET_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RESET_BTN.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RESET_BTN.Image = global::_2022_Test.Properties.Resources.icons8_rotate_44_2;
            this.RESET_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RESET_BTN.Location = new System.Drawing.Point(516, 912);
            this.RESET_BTN.Name = "RESET_BTN";
            this.RESET_BTN.Size = new System.Drawing.Size(125, 75);
            this.RESET_BTN.TabIndex = 194;
            this.RESET_BTN.TabStop = false;
            this.RESET_BTN.Text = "초기화";
            this.RESET_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.RESET_BTN.UseVisualStyleBackColor = true;
            this.RESET_BTN.Visible = false;
            this.RESET_BTN.Click += new System.EventHandler(this.RESET_BTN_Click);
            // 
            // SAVE_BTN
            // 
            this.SAVE_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SAVE_BTN.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SAVE_BTN.Image = global::_2022_Test.Properties.Resources.icons8_folder_44;
            this.SAVE_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SAVE_BTN.Location = new System.Drawing.Point(263, 912);
            this.SAVE_BTN.Name = "SAVE_BTN";
            this.SAVE_BTN.Size = new System.Drawing.Size(125, 75);
            this.SAVE_BTN.TabIndex = 193;
            this.SAVE_BTN.TabStop = false;
            this.SAVE_BTN.Text = "결과저장";
            this.SAVE_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SAVE_BTN.UseVisualStyleBackColor = true;
            // 
            // STOP_BTN
            // 
            this.STOP_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.STOP_BTN.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STOP_BTN.Image = global::_2022_Test.Properties.Resources.icons8_minus_441;
            this.STOP_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.STOP_BTN.Location = new System.Drawing.Point(136, 912);
            this.STOP_BTN.Name = "STOP_BTN";
            this.STOP_BTN.Size = new System.Drawing.Size(125, 75);
            this.STOP_BTN.TabIndex = 192;
            this.STOP_BTN.TabStop = false;
            this.STOP_BTN.Text = "시험 정지";
            this.STOP_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.STOP_BTN.UseVisualStyleBackColor = true;
            // 
            // START_BTN
            // 
            this.START_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.START_BTN.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.START_BTN.Image = global::_2022_Test.Properties.Resources.icons8_circled_play_44__1_1;
            this.START_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.START_BTN.Location = new System.Drawing.Point(9, 912);
            this.START_BTN.Name = "START_BTN";
            this.START_BTN.Size = new System.Drawing.Size(125, 75);
            this.START_BTN.TabIndex = 191;
            this.START_BTN.TabStop = false;
            this.START_BTN.Text = "시험 시작";
            this.START_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.START_BTN.UseVisualStyleBackColor = true;
            // 
            // Test_Name_Label
            // 
            this.Test_Name_Label.AutoSize = true;
            this.Test_Name_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.Test_Name_Label.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Test_Name_Label.ForeColor = System.Drawing.Color.White;
            this.Test_Name_Label.Location = new System.Drawing.Point(9, 6);
            this.Test_Name_Label.Name = "Test_Name_Label";
            this.Test_Name_Label.Size = new System.Drawing.Size(49, 25);
            this.Test_Name_Label.TabIndex = 8;
            this.Test_Name_Label.Text = "Test";
            // 
            // BackColor
            // 
            this.BackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.BackColor.Controls.Add(this.Test_Name_Label);
            this.BackColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.BackColor.Location = new System.Drawing.Point(0, 0);
            this.BackColor.Name = "BackColor";
            this.BackColor.Size = new System.Drawing.Size(1660, 35);
            this.BackColor.TabIndex = 32;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 995);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1660, 35);
            this.panel5.TabIndex = 202;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1660, 1030);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.EXIT_BTN);
            this.Controls.Add(this.RESET_BTN);
            this.Controls.Add(this.SAVE_BTN);
            this.Controls.Add(this.STOP_BTN);
            this.Controls.Add(this.START_BTN);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.BackColor);
            this.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.BackColor.ResumeLayout(false);
            this.BackColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList_nusul;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button EXIT_BTN;
        private System.Windows.Forms.Button RESET_BTN;
        private System.Windows.Forms.Button SAVE_BTN;
        private System.Windows.Forms.Button STOP_BTN;
        private System.Windows.Forms.Button START_BTN;
        private System.Windows.Forms.Label Test_Name_Label;
        private System.Windows.Forms.Panel BackColor;
        private System.Windows.Forms.Panel panel5;
    }
}