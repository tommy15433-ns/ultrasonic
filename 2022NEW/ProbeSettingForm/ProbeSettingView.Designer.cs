namespace _2022_Test.ProbeSettingForm
{
    partial class ProbeSettingView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProbeSettingView));
            this.btn_angle_dir = new _2022_Test.uis.ButtonImage();
            this.label_dir = new _2022_Test.uis.CustomLabel();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.customLabel1 = new _2022_Test.uis.CustomLabel();
            this.tb_probe_position = new System.Windows.Forms.TextBox();
            this.customLabel2 = new _2022_Test.uis.CustomLabel();
            this.customLabel3 = new _2022_Test.uis.CustomLabel();
            this.customLabel4 = new _2022_Test.uis.CustomLabel();
            this.customLabel5 = new _2022_Test.uis.CustomLabel();
            this.tb_angle_start = new System.Windows.Forms.TextBox();
            this.tb_angle_end = new System.Windows.Forms.TextBox();
            this.tb_angle_resolution = new System.Windows.Forms.TextBox();
            this.tb_gain = new System.Windows.Forms.TextBox();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_angle_dir
            // 
            this.btn_angle_dir.BackColor = System.Drawing.Color.Transparent;
            this.btn_angle_dir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_angle_dir.BackgroundImage")));
            this.btn_angle_dir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_angle_dir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_angle_dir.ImagePressed = ((System.Drawing.Image)(resources.GetObject("btn_angle_dir.ImagePressed")));
            this.btn_angle_dir.ImageReleased = ((System.Drawing.Image)(resources.GetObject("btn_angle_dir.ImageReleased")));
            this.btn_angle_dir.Location = new System.Drawing.Point(169, 57);
            this.btn_angle_dir.Name = "btn_angle_dir";
            this.btn_angle_dir.Size = new System.Drawing.Size(68, 35);
            this.btn_angle_dir.Status = _2022_Test.uis.CustomButton.StateType.Released;
            this.btn_angle_dir.TabIndex = 1;
            this.btn_angle_dir.UseVisualStyleBackColor = false;
            // 
            // label_dir
            // 
            this.label_dir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.label_dir.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.label_dir.ForeColor = System.Drawing.Color.White;
            this.label_dir.Location = new System.Drawing.Point(28, 57);
            this.label_dir.Name = "label_dir";
            this.label_dir.Size = new System.Drawing.Size(103, 35);
            this.label_dir.TabIndex = 2;
            this.label_dir.Text = "검사방향";
            this.label_dir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.tb_gain);
            this.uiGroupBox1.Controls.Add(this.tb_angle_resolution);
            this.uiGroupBox1.Controls.Add(this.tb_angle_end);
            this.uiGroupBox1.Controls.Add(this.tb_angle_start);
            this.uiGroupBox1.Controls.Add(this.customLabel5);
            this.uiGroupBox1.Controls.Add(this.customLabel4);
            this.uiGroupBox1.Controls.Add(this.customLabel3);
            this.uiGroupBox1.Controls.Add(this.customLabel2);
            this.uiGroupBox1.Controls.Add(this.tb_probe_position);
            this.uiGroupBox1.Controls.Add(this.customLabel1);
            this.uiGroupBox1.Controls.Add(this.label_dir);
            this.uiGroupBox1.Controls.Add(this.btn_angle_dir);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("Gulim", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(293, 385);
            this.uiGroupBox1.TabIndex = 3;
            this.uiGroupBox1.Text = "Probe#";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customLabel1
            // 
            this.customLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.customLabel1.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.customLabel1.ForeColor = System.Drawing.Color.White;
            this.customLabel1.Location = new System.Drawing.Point(28, 107);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(103, 35);
            this.customLabel1.TabIndex = 3;
            this.customLabel1.Text = "검사위치";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_probe_position
            // 
            this.tb_probe_position.Location = new System.Drawing.Point(158, 111);
            this.tb_probe_position.Name = "tb_probe_position";
            this.tb_probe_position.Size = new System.Drawing.Size(103, 29);
            this.tb_probe_position.TabIndex = 177;
            this.tb_probe_position.Text = "62";
            this.tb_probe_position.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // customLabel2
            // 
            this.customLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.customLabel2.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.customLabel2.ForeColor = System.Drawing.Color.White;
            this.customLabel2.Location = new System.Drawing.Point(28, 160);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(103, 35);
            this.customLabel2.TabIndex = 178;
            this.customLabel2.Text = "시작각도";
            this.customLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customLabel3
            // 
            this.customLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.customLabel3.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.customLabel3.ForeColor = System.Drawing.Color.White;
            this.customLabel3.Location = new System.Drawing.Point(28, 216);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Size = new System.Drawing.Size(103, 35);
            this.customLabel3.TabIndex = 179;
            this.customLabel3.Text = "끝각도";
            this.customLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customLabel4
            // 
            this.customLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.customLabel4.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.customLabel4.ForeColor = System.Drawing.Color.White;
            this.customLabel4.Location = new System.Drawing.Point(28, 264);
            this.customLabel4.Name = "customLabel4";
            this.customLabel4.Size = new System.Drawing.Size(103, 35);
            this.customLabel4.TabIndex = 180;
            this.customLabel4.Text = "각해상도";
            this.customLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customLabel5
            // 
            this.customLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.customLabel5.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.customLabel5.ForeColor = System.Drawing.Color.White;
            this.customLabel5.Location = new System.Drawing.Point(28, 313);
            this.customLabel5.Name = "customLabel5";
            this.customLabel5.Size = new System.Drawing.Size(103, 35);
            this.customLabel5.TabIndex = 181;
            this.customLabel5.Text = "Gain";
            this.customLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_angle_start
            // 
            this.tb_angle_start.Location = new System.Drawing.Point(158, 164);
            this.tb_angle_start.Name = "tb_angle_start";
            this.tb_angle_start.Size = new System.Drawing.Size(103, 29);
            this.tb_angle_start.TabIndex = 182;
            this.tb_angle_start.Text = "62";
            this.tb_angle_start.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_angle_end
            // 
            this.tb_angle_end.Location = new System.Drawing.Point(158, 220);
            this.tb_angle_end.Name = "tb_angle_end";
            this.tb_angle_end.Size = new System.Drawing.Size(103, 29);
            this.tb_angle_end.TabIndex = 183;
            this.tb_angle_end.Text = "62";
            this.tb_angle_end.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_angle_resolution
            // 
            this.tb_angle_resolution.Location = new System.Drawing.Point(158, 270);
            this.tb_angle_resolution.Name = "tb_angle_resolution";
            this.tb_angle_resolution.Size = new System.Drawing.Size(103, 29);
            this.tb_angle_resolution.TabIndex = 184;
            this.tb_angle_resolution.Text = "62";
            this.tb_angle_resolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_gain
            // 
            this.tb_gain.Location = new System.Drawing.Point(158, 317);
            this.tb_gain.Name = "tb_gain";
            this.tb_gain.Size = new System.Drawing.Size(103, 29);
            this.tb_gain.TabIndex = 185;
            this.tb_gain.Text = "62";
            this.tb_gain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ProbeSettingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiGroupBox1);
            this.Name = "ProbeSettingView";
            this.Size = new System.Drawing.Size(293, 385);
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private uis.ButtonImage btn_angle_dir;
        private uis.CustomLabel label_dir;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private uis.CustomLabel customLabel1;
        private System.Windows.Forms.TextBox tb_probe_position;
        private uis.CustomLabel customLabel3;
        private uis.CustomLabel customLabel2;
        private uis.CustomLabel customLabel5;
        private uis.CustomLabel customLabel4;
        private System.Windows.Forms.TextBox tb_gain;
        private System.Windows.Forms.TextBox tb_angle_resolution;
        private System.Windows.Forms.TextBox tb_angle_end;
        private System.Windows.Forms.TextBox tb_angle_start;
    }
}
