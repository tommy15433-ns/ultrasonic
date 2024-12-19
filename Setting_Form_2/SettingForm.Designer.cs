namespace _2022_Test
{
    partial class SettingForm
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
            this.BackColor = new System.Windows.Forms.Panel();
            this.Name_Label = new System.Windows.Forms.Label();
            this.Path_txt = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Save_Path_btn = new Sunny.UI.UIImageButton();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.AutoSet_Button = new System.Windows.Forms.Button();
            this.BackColor.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Save_Path_btn)).BeginInit();
            this.SuspendLayout();
            // 
            // BackColor
            // 
            this.BackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.BackColor.Controls.Add(this.Name_Label);
            this.BackColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.BackColor.Location = new System.Drawing.Point(0, 0);
            this.BackColor.Name = "BackColor";
            this.BackColor.Size = new System.Drawing.Size(434, 45);
            this.BackColor.TabIndex = 1;
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.Name_Label.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name_Label.ForeColor = System.Drawing.Color.White;
            this.Name_Label.Location = new System.Drawing.Point(6, 7);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(76, 25);
            this.Name_Label.TabIndex = 8;
            this.Name_Label.Text = "Setting";
            // 
            // Path_txt
            // 
            this.Path_txt.Font = new System.Drawing.Font("돋움", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Path_txt.Location = new System.Drawing.Point(14, 28);
            this.Path_txt.Name = "Path_txt";
            this.Path_txt.Size = new System.Drawing.Size(331, 20);
            this.Path_txt.TabIndex = 115;
            this.Path_txt.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 20;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Save_Path_btn);
            this.groupBox3.Controls.Add(this.Path_txt);
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(17, 155);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 61);
            this.groupBox3.TabIndex = 138;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Save Path";
            // 
            // Save_Path_btn
            // 
            this.Save_Path_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save_Path_btn.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.Save_Path_btn.Image = global::_2022_Test.Properties.Resources.icons8_search_folder_33;
            this.Save_Path_btn.Location = new System.Drawing.Point(356, 20);
            this.Save_Path_btn.Name = "Save_Path_btn";
            this.Save_Path_btn.Size = new System.Drawing.Size(35, 36);
            this.Save_Path_btn.TabIndex = 139;
            this.Save_Path_btn.TabStop = false;
            this.Save_Path_btn.Text = null;
            this.Save_Path_btn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.Save_Path_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Save_Path_btn_MouseUp);
            // 
            // Exit_Button
            // 
            this.Exit_Button.BackColor = System.Drawing.Color.Gainsboro;
            this.Exit_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_Button.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit_Button.Image = global::_2022_Test.Properties.Resources.icons8_logout_442;
            this.Exit_Button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit_Button.Location = new System.Drawing.Point(296, 64);
            this.Exit_Button.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(120, 70);
            this.Exit_Button.TabIndex = 117;
            this.Exit_Button.TabStop = false;
            this.Exit_Button.Text = "EXIT";
            this.Exit_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Exit_Button.UseVisualStyleBackColor = true;
            this.Exit_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Exit_Button_MouseUp);
            // 
            // save_button
            // 
            this.save_button.BackColor = System.Drawing.Color.WhiteSmoke;
            this.save_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.save_button.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.save_button.Image = global::_2022_Test.Properties.Resources.icons8_folder_441;
            this.save_button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.save_button.Location = new System.Drawing.Point(159, 64);
            this.save_button.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(120, 70);
            this.save_button.TabIndex = 113;
            this.save_button.TabStop = false;
            this.save_button.Text = "Standard Setting";
            this.save_button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.save_button_MouseUp);
            // 
            // AutoSet_Button
            // 
            this.AutoSet_Button.BackColor = System.Drawing.Color.Gainsboro;
            this.AutoSet_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AutoSet_Button.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.AutoSet_Button.Image = global::_2022_Test.Properties.Resources.icons8_settings_44_21;
            this.AutoSet_Button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AutoSet_Button.Location = new System.Drawing.Point(22, 64);
            this.AutoSet_Button.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.AutoSet_Button.Name = "AutoSet_Button";
            this.AutoSet_Button.Size = new System.Drawing.Size(120, 70);
            this.AutoSet_Button.TabIndex = 112;
            this.AutoSet_Button.TabStop = false;
            this.AutoSet_Button.Text = "Port Setting";
            this.AutoSet_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AutoSet_Button.UseVisualStyleBackColor = true;
            this.AutoSet_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AutoSet_Button_MouseUp);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 237);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Exit_Button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.AutoSet_Button);
            this.Controls.Add(this.BackColor);
            this.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.BackColor.ResumeLayout(false);
            this.BackColor.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Save_Path_btn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackColor;
        private System.Windows.Forms.Label Name_Label;
        private System.Windows.Forms.TextBox Path_txt;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button AutoSet_Button;
        private System.Windows.Forms.Button Exit_Button;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private Sunny.UI.UIImageButton Save_Path_btn;
    }
}