namespace _2022_Test
{
    partial class SelfTestForm
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
            this.OK_BTN = new System.Windows.Forms.Button();
            this.NG_BTN = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.Exit_BTN = new System.Windows.Forms.Button();
            this.Serial_GroupBox = new System.Windows.Forms.GroupBox();
            this.Timer_btn = new System.Windows.Forms.Button();
            this.power_btn = new System.Windows.Forms.Button();
            this.plc_btn = new System.Windows.Forms.Button();
            this.focuspx_btn = new System.Windows.Forms.Button();
            this.BackColor.SuspendLayout();
            this.Serial_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackColor
            // 
            this.BackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.BackColor.Controls.Add(this.Name_Label);
            this.BackColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.BackColor.Location = new System.Drawing.Point(0, 0);
            this.BackColor.Name = "BackColor";
            this.BackColor.Size = new System.Drawing.Size(416, 45);
            this.BackColor.TabIndex = 0;
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.Name_Label.Font = new System.Drawing.Font("Malgun Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name_Label.ForeColor = System.Drawing.Color.White;
            this.Name_Label.Location = new System.Drawing.Point(3, 8);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(103, 25);
            this.Name_Label.TabIndex = 7;
            this.Name_Label.Text = "SELF TEST";
            // 
            // OK_BTN
            // 
            this.OK_BTN.BackColor = System.Drawing.Color.GreenYellow;
            this.OK_BTN.Enabled = false;
            this.OK_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OK_BTN.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OK_BTN.Location = new System.Drawing.Point(24, 247);
            this.OK_BTN.Name = "OK_BTN";
            this.OK_BTN.Size = new System.Drawing.Size(160, 35);
            this.OK_BTN.TabIndex = 2;
            this.OK_BTN.Text = "RESULT GOOD";
            this.OK_BTN.UseVisualStyleBackColor = false;
            // 
            // NG_BTN
            // 
            this.NG_BTN.BackColor = System.Drawing.Color.LightCoral;
            this.NG_BTN.Enabled = false;
            this.NG_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NG_BTN.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NG_BTN.Location = new System.Drawing.Point(227, 247);
            this.NG_BTN.Name = "NG_BTN";
            this.NG_BTN.Size = new System.Drawing.Size(160, 35);
            this.NG_BTN.TabIndex = 9;
            this.NG_BTN.Text = "RESULT FAIL";
            this.NG_BTN.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.LemonChiffon;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button5.Location = new System.Drawing.Point(24, 294);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(160, 35);
            this.button5.TabIndex = 10;
            this.button5.Text = "ALL CHECK";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button5_MouseUp);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Gainsboro;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.Location = new System.Drawing.Point(227, 294);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(160, 35);
            this.button6.TabIndex = 11;
            this.button6.Text = "ALL CANCEL";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button6_MouseUp);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Gainsboro;
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button9.Image = global::_2022_Test.Properties.Resources.icons8_circled_play_44__1_;
            this.button9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button9.Location = new System.Drawing.Point(153, 379);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(80, 70);
            this.button9.TabIndex = 14;
            this.button9.Text = "START";
            this.button9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Gainsboro;
            this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button8.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button8.Image = global::_2022_Test.Properties.Resources.icons8_minus_44;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button8.Location = new System.Drawing.Point(239, 379);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(80, 70);
            this.button8.TabIndex = 13;
            this.button8.Text = "STOP";
            this.button8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // Exit_BTN
            // 
            this.Exit_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Exit_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_BTN.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit_BTN.Image = global::_2022_Test.Properties.Resources.icons8_logout_443;
            this.Exit_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit_BTN.Location = new System.Drawing.Point(324, 379);
            this.Exit_BTN.Name = "Exit_BTN";
            this.Exit_BTN.Size = new System.Drawing.Size(80, 70);
            this.Exit_BTN.TabIndex = 12;
            this.Exit_BTN.Text = "EXIT";
            this.Exit_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Exit_BTN.UseVisualStyleBackColor = true;
            this.Exit_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Exit_BTN_MouseUp);
            // 
            // Serial_GroupBox
            // 
            this.Serial_GroupBox.Controls.Add(this.Timer_btn);
            this.Serial_GroupBox.Controls.Add(this.power_btn);
            this.Serial_GroupBox.Controls.Add(this.plc_btn);
            this.Serial_GroupBox.Controls.Add(this.focuspx_btn);
            this.Serial_GroupBox.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Serial_GroupBox.Location = new System.Drawing.Point(12, 54);
            this.Serial_GroupBox.Name = "Serial_GroupBox";
            this.Serial_GroupBox.Size = new System.Drawing.Size(389, 170);
            this.Serial_GroupBox.TabIndex = 15;
            this.Serial_GroupBox.TabStop = false;
            this.Serial_GroupBox.Text = "Equipment";
            // 
            // Timer_btn
            // 
            this.Timer_btn.BackColor = System.Drawing.Color.Gainsboro;
            this.Timer_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Timer_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Timer_btn.Font = new System.Drawing.Font("Dotum", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Timer_btn.Location = new System.Drawing.Point(13, 161);
            this.Timer_btn.Name = "Timer_btn";
            this.Timer_btn.Size = new System.Drawing.Size(363, 35);
            this.Timer_btn.TabIndex = 3;
            this.Timer_btn.Text = "Timer";
            this.Timer_btn.UseVisualStyleBackColor = false;
            this.Timer_btn.Visible = false;
            this.Timer_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plc_btn_MouseUp);
            // 
            // power_btn
            // 
            this.power_btn.BackColor = System.Drawing.Color.LemonChiffon;
            this.power_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.power_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.power_btn.Font = new System.Drawing.Font("Dotum", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.power_btn.Location = new System.Drawing.Point(13, 119);
            this.power_btn.Name = "power_btn";
            this.power_btn.Size = new System.Drawing.Size(363, 35);
            this.power_btn.TabIndex = 2;
            this.power_btn.Text = "DC POWER";
            this.power_btn.UseVisualStyleBackColor = false;
            this.power_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plc_btn_MouseUp);
            // 
            // plc_btn
            // 
            this.plc_btn.BackColor = System.Drawing.Color.LemonChiffon;
            this.plc_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.plc_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plc_btn.Font = new System.Drawing.Font("Dotum", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.plc_btn.Location = new System.Drawing.Point(13, 77);
            this.plc_btn.Name = "plc_btn";
            this.plc_btn.Size = new System.Drawing.Size(363, 35);
            this.plc_btn.TabIndex = 1;
            this.plc_btn.Text = "PLC";
            this.plc_btn.UseVisualStyleBackColor = false;
            this.plc_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plc_btn_MouseUp);
            // 
            // focuspx_btn
            // 
            this.focuspx_btn.BackColor = System.Drawing.Color.LemonChiffon;
            this.focuspx_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.focuspx_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.focuspx_btn.Font = new System.Drawing.Font("Dotum", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.focuspx_btn.Location = new System.Drawing.Point(13, 35);
            this.focuspx_btn.Name = "focuspx_btn";
            this.focuspx_btn.Size = new System.Drawing.Size(363, 35);
            this.focuspx_btn.TabIndex = 0;
            this.focuspx_btn.Text = "Focus PX";
            this.focuspx_btn.UseVisualStyleBackColor = false;
            this.focuspx_btn.Click += new System.EventHandler(this.plc_btn_MouseUp);
            // 
            // SelfTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 463);
            this.Controls.Add(this.Serial_GroupBox);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.Exit_BTN);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.NG_BTN);
            this.Controls.Add(this.OK_BTN);
            this.Controls.Add(this.BackColor);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Dotum", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SelfTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelfTestForm";
            this.BackColor.ResumeLayout(false);
            this.BackColor.PerformLayout();
            this.Serial_GroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackColor;
        private System.Windows.Forms.Label Name_Label;
        private System.Windows.Forms.Button OK_BTN;
        private System.Windows.Forms.Button NG_BTN;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button Exit_BTN;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox Serial_GroupBox;
        private System.Windows.Forms.Button plc_btn;
        private System.Windows.Forms.Button focuspx_btn;
        private System.Windows.Forms.Button Timer_btn;
        private System.Windows.Forms.Button power_btn;
    }
}