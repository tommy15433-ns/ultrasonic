namespace _2022_Test
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.timer1 = new System.Timers.Timer();
            this.label_focuspx_status = new System.Windows.Forms.Label();
            this.panel_control = new System.Windows.Forms.Panel();
            this.combobox_ascan_sel = new System.Windows.Forms.ComboBox();
            this.combobox_bscan_sel = new System.Windows.Forms.ComboBox();
            this.cb_lawfile = new System.Windows.Forms.ComboBox();
            this.btn_clear_beam = new System.Windows.Forms.Button();
            this.btn_show_config = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_dbg = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.panel_cscan = new System.Windows.Forms.TableLayoutPanel();
            this.panel_cscan_control = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.tb_gateThreshold = new System.Windows.Forms.TextBox();
            this.tb_gateLength = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_gatestart = new System.Windows.Forms.TextBox();
            this.panel_plot_cscan = new System.Windows.Forms.Panel();
            this.panel_focallaw = new System.Windows.Forms.Panel();
            this.table_bscans = new System.Windows.Forms.TableLayoutPanel();
            this.panel_plot_ascan = new System.Windows.Forms.Panel();
            this.tb_pulsewidth = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.panel_control.SuspendLayout();
            this.panel_cscan.SuspendLayout();
            this.panel_cscan_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 61);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(154, 31);
            this.button4.TabIndex = 9;
            this.button4.Text = "Create beam from tab";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(10, 216);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(206, 40);
            this.button5.TabIndex = 21;
            this.button5.Text = "acquire start";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.SynchronizingObject = this;
            // 
            // label_focuspx_status
            // 
            this.label_focuspx_status.AutoSize = true;
            this.label_focuspx_status.Location = new System.Drawing.Point(128, 21);
            this.label_focuspx_status.Name = "label_focuspx_status";
            this.label_focuspx_status.Size = new System.Drawing.Size(96, 12);
            this.label_focuspx_status.TabIndex = 39;
            this.label_focuspx_status.Text = "focus_px_status";
            // 
            // panel_control
            // 
            this.panel_control.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_control.Controls.Add(this.tb_pulsewidth);
            this.panel_control.Controls.Add(this.combobox_ascan_sel);
            this.panel_control.Controls.Add(this.combobox_bscan_sel);
            this.panel_control.Controls.Add(this.cb_lawfile);
            this.panel_control.Controls.Add(this.btn_clear_beam);
            this.panel_control.Controls.Add(this.btn_show_config);
            this.panel_control.Controls.Add(this.button2);
            this.panel_control.Controls.Add(this.btn_dbg);
            this.panel_control.Controls.Add(this.button8);
            this.panel_control.Controls.Add(this.button5);
            this.panel_control.Controls.Add(this.button4);
            this.panel_control.Location = new System.Drawing.Point(12, 48);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(684, 493);
            this.panel_control.TabIndex = 42;
            // 
            // combobox_ascan_sel
            // 
            this.combobox_ascan_sel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_ascan_sel.FormattingEnabled = true;
            this.combobox_ascan_sel.Location = new System.Drawing.Point(560, 29);
            this.combobox_ascan_sel.Name = "combobox_ascan_sel";
            this.combobox_ascan_sel.Size = new System.Drawing.Size(121, 20);
            this.combobox_ascan_sel.TabIndex = 51;
            // 
            // combobox_bscan_sel
            // 
            this.combobox_bscan_sel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_bscan_sel.FormattingEnabled = true;
            this.combobox_bscan_sel.Location = new System.Drawing.Point(560, 3);
            this.combobox_bscan_sel.Name = "combobox_bscan_sel";
            this.combobox_bscan_sel.Size = new System.Drawing.Size(121, 20);
            this.combobox_bscan_sel.TabIndex = 50;
            // 
            // cb_lawfile
            // 
            this.cb_lawfile.FormattingEnabled = true;
            this.cb_lawfile.Location = new System.Drawing.Point(5, 3);
            this.cb_lawfile.Name = "cb_lawfile";
            this.cb_lawfile.Size = new System.Drawing.Size(121, 20);
            this.cb_lawfile.TabIndex = 49;
            // 
            // btn_clear_beam
            // 
            this.btn_clear_beam.Location = new System.Drawing.Point(11, 270);
            this.btn_clear_beam.Name = "btn_clear_beam";
            this.btn_clear_beam.Size = new System.Drawing.Size(111, 49);
            this.btn_clear_beam.TabIndex = 48;
            this.btn_clear_beam.Text = "ClearBeam";
            this.btn_clear_beam.UseVisualStyleBackColor = true;
            this.btn_clear_beam.Click += new System.EventHandler(this.btn_clear_beam_Click);
            // 
            // btn_show_config
            // 
            this.btn_show_config.Location = new System.Drawing.Point(10, 98);
            this.btn_show_config.Name = "btn_show_config";
            this.btn_show_config.Size = new System.Drawing.Size(116, 33);
            this.btn_show_config.TabIndex = 47;
            this.btn_show_config.Text = "Show Config";
            this.btn_show_config.UseVisualStyleBackColor = true;
            this.btn_show_config.Click += new System.EventHandler(this.btn_show_config_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(127, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 73);
            this.button2.TabIndex = 46;
            this.button2.Text = "Create beam from Form";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_dbg
            // 
            this.btn_dbg.Location = new System.Drawing.Point(137, 177);
            this.btn_dbg.Name = "btn_dbg";
            this.btn_dbg.Size = new System.Drawing.Size(75, 23);
            this.btn_dbg.TabIndex = 45;
            this.btn_dbg.Text = "dbg";
            this.btn_dbg.UseVisualStyleBackColor = true;
            this.btn_dbg.Click += new System.EventHandler(this.btn_dbg_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(11, 137);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(116, 34);
            this.button8.TabIndex = 44;
            this.button8.Text = "apply_config";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // panel_cscan
            // 
            this.panel_cscan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_cscan.ColumnCount = 2;
            this.panel_cscan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.02391F));
            this.panel_cscan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.97609F));
            this.panel_cscan.Controls.Add(this.panel_cscan_control, 0, 0);
            this.panel_cscan.Controls.Add(this.panel_plot_cscan, 1, 0);
            this.panel_cscan.Location = new System.Drawing.Point(12, 746);
            this.panel_cscan.Name = "panel_cscan";
            this.panel_cscan.RowCount = 1;
            this.panel_cscan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel_cscan.Size = new System.Drawing.Size(1461, 238);
            this.panel_cscan.TabIndex = 43;
            // 
            // panel_cscan_control
            // 
            this.panel_cscan_control.Controls.Add(this.button11);
            this.panel_cscan_control.Controls.Add(this.tb_gateThreshold);
            this.panel_cscan_control.Controls.Add(this.tb_gateLength);
            this.panel_cscan_control.Controls.Add(this.label9);
            this.panel_cscan_control.Controls.Add(this.label8);
            this.panel_cscan_control.Controls.Add(this.label7);
            this.panel_cscan_control.Controls.Add(this.tb_gatestart);
            this.panel_cscan_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_cscan_control.Location = new System.Drawing.Point(3, 3);
            this.panel_cscan_control.Name = "panel_cscan_control";
            this.panel_cscan_control.Size = new System.Drawing.Size(461, 232);
            this.panel_cscan_control.TabIndex = 1;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(160, 157);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(148, 27);
            this.button11.TabIndex = 6;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // tb_gateThreshold
            // 
            this.tb_gateThreshold.Location = new System.Drawing.Point(160, 97);
            this.tb_gateThreshold.Name = "tb_gateThreshold";
            this.tb_gateThreshold.Size = new System.Drawing.Size(130, 21);
            this.tb_gateThreshold.TabIndex = 5;
            this.tb_gateThreshold.Text = "0";
            // 
            // tb_gateLength
            // 
            this.tb_gateLength.Location = new System.Drawing.Point(160, 63);
            this.tb_gateLength.Name = "tb_gateLength";
            this.tb_gateLength.Size = new System.Drawing.Size(130, 21);
            this.tb_gateLength.TabIndex = 4;
            this.tb_gateLength.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(72, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "Threshold";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(72, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "Length";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "Gate Start";
            // 
            // tb_gatestart
            // 
            this.tb_gatestart.Location = new System.Drawing.Point(160, 29);
            this.tb_gatestart.Name = "tb_gatestart";
            this.tb_gatestart.Size = new System.Drawing.Size(130, 21);
            this.tb_gatestart.TabIndex = 0;
            this.tb_gatestart.Text = "0";
            // 
            // panel_plot_cscan
            // 
            this.panel_plot_cscan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_plot_cscan.Location = new System.Drawing.Point(470, 3);
            this.panel_plot_cscan.Name = "panel_plot_cscan";
            this.panel_plot_cscan.Size = new System.Drawing.Size(988, 232);
            this.panel_plot_cscan.TabIndex = 2;
            // 
            // panel_focallaw
            // 
            this.panel_focallaw.Location = new System.Drawing.Point(36, 618);
            this.panel_focallaw.Name = "panel_focallaw";
            this.panel_focallaw.Size = new System.Drawing.Size(629, 106);
            this.panel_focallaw.TabIndex = 44;
            // 
            // table_bscans
            // 
            this.table_bscans.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table_bscans.ColumnCount = 1;
            this.table_bscans.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_bscans.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table_bscans.Location = new System.Drawing.Point(716, 48);
            this.table_bscans.Name = "table_bscans";
            this.table_bscans.RowCount = 1;
            this.table_bscans.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_bscans.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.table_bscans.Size = new System.Drawing.Size(727, 350);
            this.table_bscans.TabIndex = 45;
            // 
            // panel_plot_ascan
            // 
            this.panel_plot_ascan.Location = new System.Drawing.Point(716, 404);
            this.panel_plot_ascan.Name = "panel_plot_ascan";
            this.panel_plot_ascan.Size = new System.Drawing.Size(728, 170);
            this.panel_plot_ascan.TabIndex = 46;
            // 
            // tb_pulsewidth
            // 
            this.tb_pulsewidth.Location = new System.Drawing.Point(218, 179);
            this.tb_pulsewidth.Name = "tb_pulsewidth";
            this.tb_pulsewidth.Size = new System.Drawing.Size(151, 21);
            this.tb_pulsewidth.TabIndex = 52;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1485, 994);
            this.Controls.Add(this.panel_plot_ascan);
            this.Controls.Add(this.table_bscans);
            this.Controls.Add(this.panel_focallaw);
            this.Controls.Add(this.panel_cscan);
            this.Controls.Add(this.panel_control);
            this.Controls.Add(this.label_focuspx_status);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.panel_control.ResumeLayout(false);
            this.panel_control.PerformLayout();
            this.panel_cscan.ResumeLayout(false);
            this.panel_cscan_control.ResumeLayout(false);
            this.panel_cscan_control.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Timers.Timer timer1;
        private System.Windows.Forms.Label label_focuspx_status;
        private System.Windows.Forms.Panel panel_control;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TableLayoutPanel panel_cscan;
        private System.Windows.Forms.Panel panel_cscan_control;
        private System.Windows.Forms.TextBox tb_gatestart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_gateThreshold;
        private System.Windows.Forms.TextBox tb_gateLength;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Panel panel_plot_cscan;
        private System.Windows.Forms.Button btn_dbg;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_show_config;
        private System.Windows.Forms.Button btn_clear_beam;
        private System.Windows.Forms.ComboBox cb_lawfile;
        private System.Windows.Forms.ComboBox combobox_ascan_sel;
        private System.Windows.Forms.ComboBox combobox_bscan_sel;
        private System.Windows.Forms.Panel panel_focallaw;
        private System.Windows.Forms.TableLayoutPanel table_bscans;
        private System.Windows.Forms.Panel panel_plot_ascan;
        private System.Windows.Forms.TextBox tb_pulsewidth;
    }
}

