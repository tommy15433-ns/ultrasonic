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
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.tb_gain = new System.Windows.Forms.TextBox();
            this.tb_angle_resolution = new System.Windows.Forms.TextBox();
            this.tb_angle_end = new System.Windows.Forms.TextBox();
            this.tb_angle_start = new System.Windows.Forms.TextBox();
            this.tb_probe_position = new System.Windows.Forms.TextBox();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.tb_gain);
            this.uiGroupBox1.Controls.Add(this.tb_angle_resolution);
            this.uiGroupBox1.Controls.Add(this.tb_angle_end);
            this.uiGroupBox1.Controls.Add(this.tb_angle_start);
            this.uiGroupBox1.Controls.Add(this.tb_probe_position);
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
            // tb_gain
            // 
            this.tb_gain.Location = new System.Drawing.Point(158, 317);
            this.tb_gain.Name = "tb_gain";
            this.tb_gain.Size = new System.Drawing.Size(103, 29);
            this.tb_gain.TabIndex = 185;
            this.tb_gain.Text = "62";
            this.tb_gain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // tb_angle_end
            // 
            this.tb_angle_end.Location = new System.Drawing.Point(158, 220);
            this.tb_angle_end.Name = "tb_angle_end";
            this.tb_angle_end.Size = new System.Drawing.Size(103, 29);
            this.tb_angle_end.TabIndex = 183;
            this.tb_angle_end.Text = "62";
            this.tb_angle_end.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // tb_probe_position
            // 
            this.tb_probe_position.Location = new System.Drawing.Point(158, 111);
            this.tb_probe_position.Name = "tb_probe_position";
            this.tb_probe_position.Size = new System.Drawing.Size(103, 29);
            this.tb_probe_position.TabIndex = 177;
            this.tb_probe_position.Text = "62";
            this.tb_probe_position.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
