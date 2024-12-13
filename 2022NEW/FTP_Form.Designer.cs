namespace _2022_Test
{
    partial class FTP_Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BackColor = new System.Windows.Forms.Panel();
            this.Name_Label = new System.Windows.Forms.Label();
            this.Start_Picker = new System.Windows.Forms.DateTimePicker();
            this.Stop_Picker = new System.Windows.Forms.DateTimePicker();
            this.Stop_Day_Label = new System.Windows.Forms.Label();
            this.Start_Day_Label = new System.Windows.Forms.Label();
            this.IP_Label = new System.Windows.Forms.Label();
            this.ID_Label = new System.Windows.Forms.Label();
            this.PW_Label = new System.Windows.Forms.Label();
            this.ID_TextBox = new System.Windows.Forms.TextBox();
            this.PW_TextBox = new System.Windows.Forms.TextBox();
            this.DataGridView = new Sunny.UI.UIDataGridView();
            this.IP_TextBox = new System.Windows.Forms.TextBox();
            this.Send_BTN = new System.Windows.Forms.Button();
            this.Save_BTN = new System.Windows.Forms.Button();
            this.Search_BTN = new System.Windows.Forms.Button();
            this.Exit_BTN = new System.Windows.Forms.Button();
            this.BackColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BackColor
            // 
            this.BackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.BackColor.Controls.Add(this.Name_Label);
            this.BackColor.Location = new System.Drawing.Point(0, 0);
            this.BackColor.Name = "BackColor";
            this.BackColor.Size = new System.Drawing.Size(1200, 30);
            this.BackColor.TabIndex = 15;
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name_Label.ForeColor = System.Drawing.Color.White;
            this.Name_Label.Location = new System.Drawing.Point(4, 5);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(75, 21);
            this.Name_Label.TabIndex = 6;
            this.Name_Label.Text = "FTP 전송";
            // 
            // Start_Picker
            // 
            this.Start_Picker.CalendarFont = new System.Drawing.Font("맑은 고딕", 10F);
            this.Start_Picker.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Start_Picker.Location = new System.Drawing.Point(88, 747);
            this.Start_Picker.Name = "Start_Picker";
            this.Start_Picker.Size = new System.Drawing.Size(185, 25);
            this.Start_Picker.TabIndex = 105;
            this.Start_Picker.TabStop = false;
            // 
            // Stop_Picker
            // 
            this.Stop_Picker.CalendarFont = new System.Drawing.Font("맑은 고딕", 10F);
            this.Stop_Picker.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Stop_Picker.Location = new System.Drawing.Point(88, 791);
            this.Stop_Picker.Name = "Stop_Picker";
            this.Stop_Picker.Size = new System.Drawing.Size(185, 25);
            this.Stop_Picker.TabIndex = 106;
            this.Stop_Picker.TabStop = false;
            // 
            // Stop_Day_Label
            // 
            this.Stop_Day_Label.AutoSize = true;
            this.Stop_Day_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Stop_Day_Label.Location = new System.Drawing.Point(22, 796);
            this.Stop_Day_Label.Name = "Stop_Day_Label";
            this.Stop_Day_Label.Size = new System.Drawing.Size(60, 17);
            this.Stop_Day_Label.TabIndex = 108;
            this.Stop_Day_Label.Text = "종료날짜";
            // 
            // Start_Day_Label
            // 
            this.Start_Day_Label.AutoSize = true;
            this.Start_Day_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Start_Day_Label.Location = new System.Drawing.Point(22, 752);
            this.Start_Day_Label.Name = "Start_Day_Label";
            this.Start_Day_Label.Size = new System.Drawing.Size(60, 17);
            this.Start_Day_Label.TabIndex = 109;
            this.Start_Day_Label.Text = "시작날짜";
            // 
            // IP_Label
            // 
            this.IP_Label.AutoSize = true;
            this.IP_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.IP_Label.Location = new System.Drawing.Point(302, 752);
            this.IP_Label.Name = "IP_Label";
            this.IP_Label.Size = new System.Drawing.Size(54, 17);
            this.IP_Label.TabIndex = 110;
            this.IP_Label.Text = "IP주소 :";
            // 
            // ID_Label
            // 
            this.ID_Label.AutoSize = true;
            this.ID_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ID_Label.Location = new System.Drawing.Point(326, 797);
            this.ID_Label.Name = "ID_Label";
            this.ID_Label.Size = new System.Drawing.Size(30, 17);
            this.ID_Label.TabIndex = 111;
            this.ID_Label.Text = "ID :";
            // 
            // PW_Label
            // 
            this.PW_Label.AutoSize = true;
            this.PW_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PW_Label.Location = new System.Drawing.Point(524, 796);
            this.PW_Label.Name = "PW_Label";
            this.PW_Label.Size = new System.Drawing.Size(37, 17);
            this.PW_Label.TabIndex = 112;
            this.PW_Label.Text = "PW :";
            // 
            // ID_TextBox
            // 
            this.ID_TextBox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ID_TextBox.Location = new System.Drawing.Point(362, 791);
            this.ID_TextBox.Name = "ID_TextBox";
            this.ID_TextBox.Size = new System.Drawing.Size(150, 25);
            this.ID_TextBox.TabIndex = 113;
            // 
            // PW_TextBox
            // 
            this.PW_TextBox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PW_TextBox.Location = new System.Drawing.Point(567, 791);
            this.PW_TextBox.Name = "PW_TextBox";
            this.PW_TextBox.Size = new System.Drawing.Size(150, 25);
            this.PW_TextBox.TabIndex = 114;
            // 
            // DataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.DataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView.EnableHeadersVisualStyles = false;
            this.DataGridView.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.DataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.DataGridView.Location = new System.Drawing.Point(12, 36);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.DataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DataGridView.RowTemplate.Height = 23;
            this.DataGridView.ScrollBarRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.DataGridView.SelectedIndex = -1;
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.Size = new System.Drawing.Size(1181, 685);
            this.DataGridView.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.DataGridView.TabIndex = 123;
            this.DataGridView.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // IP_TextBox
            // 
            this.IP_TextBox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.IP_TextBox.Location = new System.Drawing.Point(362, 747);
            this.IP_TextBox.Name = "IP_TextBox";
            this.IP_TextBox.Size = new System.Drawing.Size(355, 25);
            this.IP_TextBox.TabIndex = 19;
            // 
            // Send_BTN
            // 
            this.Send_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Send_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Send_BTN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Send_BTN.Image = global::_2022_Test.Properties.Resources.ftp_upload;
            this.Send_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Send_BTN.Location = new System.Drawing.Point(1027, 752);
            this.Send_BTN.Name = "Send_BTN";
            this.Send_BTN.Size = new System.Drawing.Size(80, 70);
            this.Send_BTN.TabIndex = 18;
            this.Send_BTN.Text = "전송";
            this.Send_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Send_BTN.UseVisualStyleBackColor = true;
            // 
            // Save_BTN
            // 
            this.Save_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Save_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save_BTN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Save_BTN.Image = global::_2022_Test.Properties.Resources.icons8_folder_441;
            this.Save_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Save_BTN.Location = new System.Drawing.Point(941, 752);
            this.Save_BTN.Name = "Save_BTN";
            this.Save_BTN.Size = new System.Drawing.Size(80, 70);
            this.Save_BTN.TabIndex = 17;
            this.Save_BTN.Text = "저장";
            this.Save_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Save_BTN.UseVisualStyleBackColor = true;
            this.Save_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Save_BTN_MouseUp);
            // 
            // Search_BTN
            // 
            this.Search_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Search_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Search_BTN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Search_BTN.Image = global::_2022_Test.Properties.Resources.icons8_search_4412;
            this.Search_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Search_BTN.Location = new System.Drawing.Point(855, 752);
            this.Search_BTN.Name = "Search_BTN";
            this.Search_BTN.Size = new System.Drawing.Size(80, 70);
            this.Search_BTN.TabIndex = 16;
            this.Search_BTN.Text = "검색";
            this.Search_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Search_BTN.UseVisualStyleBackColor = true;
            this.Search_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Search_BTN_MouseUp);
            // 
            // Exit_BTN
            // 
            this.Exit_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Exit_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_BTN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit_BTN.Image = global::_2022_Test.Properties.Resources.icons8_logout_445;
            this.Exit_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit_BTN.Location = new System.Drawing.Point(1113, 752);
            this.Exit_BTN.Name = "Exit_BTN";
            this.Exit_BTN.Size = new System.Drawing.Size(80, 70);
            this.Exit_BTN.TabIndex = 14;
            this.Exit_BTN.Text = "종료";
            this.Exit_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Exit_BTN.UseVisualStyleBackColor = true;
            this.Exit_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Exit_BTN_MouseUp);
            // 
            // FTP_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 834);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.PW_TextBox);
            this.Controls.Add(this.ID_TextBox);
            this.Controls.Add(this.PW_Label);
            this.Controls.Add(this.ID_Label);
            this.Controls.Add(this.IP_Label);
            this.Controls.Add(this.Start_Day_Label);
            this.Controls.Add(this.Stop_Day_Label);
            this.Controls.Add(this.Stop_Picker);
            this.Controls.Add(this.Start_Picker);
            this.Controls.Add(this.IP_TextBox);
            this.Controls.Add(this.Send_BTN);
            this.Controls.Add(this.Save_BTN);
            this.Controls.Add(this.Search_BTN);
            this.Controls.Add(this.BackColor);
            this.Controls.Add(this.Exit_BTN);
            this.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FTP_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FTP_Form_Load);
            this.BackColor.ResumeLayout(false);
            this.BackColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Exit_BTN;
        private System.Windows.Forms.Panel BackColor;
        private System.Windows.Forms.Label Name_Label;
        private System.Windows.Forms.Button Search_BTN;
        private System.Windows.Forms.Button Save_BTN;
        private System.Windows.Forms.Button Send_BTN;
        private System.Windows.Forms.DateTimePicker Start_Picker;
        private System.Windows.Forms.DateTimePicker Stop_Picker;
        private System.Windows.Forms.Label Stop_Day_Label;
        private System.Windows.Forms.Label Start_Day_Label;
        private System.Windows.Forms.Label IP_Label;
        private System.Windows.Forms.Label ID_Label;
        private System.Windows.Forms.Label PW_Label;
        private System.Windows.Forms.TextBox ID_TextBox;
        private System.Windows.Forms.TextBox PW_TextBox;
        private Sunny.UI.UIDataGridView DataGridView;
        private System.Windows.Forms.TextBox IP_TextBox;
    }
}