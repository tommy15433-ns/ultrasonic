namespace WindowsFormsApp1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_beamcount = new System.Windows.Forms.TextBox();
            this.tb_elementcount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_dlyperpulser = new System.Windows.Forms.TextBox();
            this.cb_ascandatasize = new System.Windows.Forms.ComboBox();
            this.cb_rectificationtype = new System.Windows.Forms.ComboBox();
            this.cb_scalingtype = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel_test = new System.Windows.Forms.Panel();
            this.panel_plot = new System.Windows.Forms.Panel();
            this.timer1 = new System.Timers.Timer();
            this.panel_plot_ascan = new System.Windows.Forms.Panel();
            this.chart_prob = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel_graph = new System.Windows.Forms.TableLayoutPanel();
            this.label_focuspx_status = new System.Windows.Forms.Label();
            this.tab_beamStrat = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel_custom2 = new System.Windows.Forms.Panel();
            this.tb_elementcount_3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_beamcount_3 = new System.Windows.Forms.TextBox();
            this.tb_dlyperpulser_3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel_control = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_dbg = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.tb_probsel = new System.Windows.Forms.TextBox();
            this.textBox_ascan_index = new System.Windows.Forms.TextBox();
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
            this.tabControl2.SuspendLayout();
            this.panel_test.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_prob)).BeginInit();
            this.panel_graph.SuspendLayout();
            this.tab_beamStrat.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel_custom2.SuspendLayout();
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
            // tb_beamcount
            // 
            this.tb_beamcount.Location = new System.Drawing.Point(139, 45);
            this.tb_beamcount.Name = "tb_beamcount";
            this.tb_beamcount.Size = new System.Drawing.Size(89, 21);
            this.tb_beamcount.TabIndex = 5;
            this.tb_beamcount.Text = "10";
            // 
            // tb_elementcount
            // 
            this.tb_elementcount.Location = new System.Drawing.Point(139, 72);
            this.tb_elementcount.Name = "tb_elementcount";
            this.tb_elementcount.Size = new System.Drawing.Size(89, 21);
            this.tb_elementcount.TabIndex = 6;
            this.tb_elementcount.Text = "16";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "beamcount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "elementcount";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(298, 193);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(206, 31);
            this.button4.TabIndex = 9;
            this.button4.Text = "Create beam from tab";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "delayperpulser";
            // 
            // tb_dlyperpulser
            // 
            this.tb_dlyperpulser.Location = new System.Drawing.Point(139, 97);
            this.tb_dlyperpulser.Name = "tb_dlyperpulser";
            this.tb_dlyperpulser.Size = new System.Drawing.Size(89, 21);
            this.tb_dlyperpulser.TabIndex = 10;
            this.tb_dlyperpulser.Text = "10";
            // 
            // cb_ascandatasize
            // 
            this.cb_ascandatasize.FormattingEnabled = true;
            this.cb_ascandatasize.Location = new System.Drawing.Point(548, 193);
            this.cb_ascandatasize.Name = "cb_ascandatasize";
            this.cb_ascandatasize.Size = new System.Drawing.Size(121, 20);
            this.cb_ascandatasize.TabIndex = 18;
            // 
            // cb_rectificationtype
            // 
            this.cb_rectificationtype.FormattingEnabled = true;
            this.cb_rectificationtype.Location = new System.Drawing.Point(548, 219);
            this.cb_rectificationtype.Name = "cb_rectificationtype";
            this.cb_rectificationtype.Size = new System.Drawing.Size(121, 20);
            this.cb_rectificationtype.TabIndex = 19;
            // 
            // cb_scalingtype
            // 
            this.cb_scalingtype.FormattingEnabled = true;
            this.cb_scalingtype.Location = new System.Drawing.Point(548, 245);
            this.cb_scalingtype.Name = "cb_scalingtype";
            this.cb_scalingtype.Size = new System.Drawing.Size(121, 20);
            this.cb_scalingtype.TabIndex = 20;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(298, 271);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(206, 23);
            this.button5.TabIndex = 21;
            this.button5.Text = "acquire start";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(278, 65);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(8, 8);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(0, 0);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(0, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel_test
            // 
            this.panel_test.Controls.Add(this.tabControl2);
            this.panel_test.Location = new System.Drawing.Point(10, 191);
            this.panel_test.Name = "panel_test";
            this.panel_test.Size = new System.Drawing.Size(209, 163);
            this.panel_test.TabIndex = 27;
            // 
            // panel_plot
            // 
            this.panel_plot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_plot.Location = new System.Drawing.Point(5, 5);
            this.panel_plot.Name = "panel_plot";
            this.panel_plot.Size = new System.Drawing.Size(471, 350);
            this.panel_plot.TabIndex = 30;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.SynchronizingObject = this;
            // 
            // panel_plot_ascan
            // 
            this.panel_plot_ascan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_plot_ascan.Location = new System.Drawing.Point(5, 363);
            this.panel_plot_ascan.Name = "panel_plot_ascan";
            this.panel_plot_ascan.Size = new System.Drawing.Size(471, 350);
            this.panel_plot_ascan.TabIndex = 33;
            // 
            // chart_prob
            // 
            this.chart_prob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            chartArea1.Name = "ChartArea1";
            this.chart_prob.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_prob.Legends.Add(legend1);
            this.chart_prob.Location = new System.Drawing.Point(12, 547);
            this.chart_prob.Name = "chart_prob";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.MarkerBorderWidth = 5;
            series1.Name = "Series1";
            this.chart_prob.Series.Add(series1);
            this.chart_prob.Size = new System.Drawing.Size(685, 183);
            this.chart_prob.TabIndex = 0;
            this.chart_prob.Text = "chart1";
            // 
            // panel_graph
            // 
            this.panel_graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_graph.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.panel_graph.ColumnCount = 1;
            this.panel_graph.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel_graph.Controls.Add(this.panel_plot_ascan, 0, 1);
            this.panel_graph.Controls.Add(this.panel_plot, 0, 0);
            this.panel_graph.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel_graph.Location = new System.Drawing.Point(702, 12);
            this.panel_graph.Name = "panel_graph";
            this.panel_graph.RowCount = 2;
            this.panel_graph.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel_graph.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel_graph.Size = new System.Drawing.Size(481, 718);
            this.panel_graph.TabIndex = 38;
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
            // tab_beamStrat
            // 
            this.tab_beamStrat.Controls.Add(this.tabPage1);
            this.tab_beamStrat.Controls.Add(this.tabPage2);
            this.tab_beamStrat.Dock = System.Windows.Forms.DockStyle.Top;
            this.tab_beamStrat.Location = new System.Drawing.Point(0, 0);
            this.tab_beamStrat.Name = "tab_beamStrat";
            this.tab_beamStrat.SelectedIndex = 0;
            this.tab_beamStrat.Size = new System.Drawing.Size(684, 183);
            this.tab_beamStrat.TabIndex = 41;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel_custom2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(676, 157);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel_custom2
            // 
            this.panel_custom2.Controls.Add(this.tb_elementcount_3);
            this.panel_custom2.Controls.Add(this.label3);
            this.panel_custom2.Controls.Add(this.label5);
            this.panel_custom2.Controls.Add(this.tb_beamcount_3);
            this.panel_custom2.Controls.Add(this.tb_dlyperpulser_3);
            this.panel_custom2.Controls.Add(this.label6);
            this.panel_custom2.Controls.Add(this.tb_elementcount);
            this.panel_custom2.Controls.Add(this.label2);
            this.panel_custom2.Controls.Add(this.label1);
            this.panel_custom2.Controls.Add(this.tb_beamcount);
            this.panel_custom2.Controls.Add(this.tb_dlyperpulser);
            this.panel_custom2.Controls.Add(this.label4);
            this.panel_custom2.Location = new System.Drawing.Point(6, 6);
            this.panel_custom2.Name = "panel_custom2";
            this.panel_custom2.Size = new System.Drawing.Size(664, 145);
            this.panel_custom2.TabIndex = 42;
            // 
            // tb_elementcount_3
            // 
            this.tb_elementcount_3.Location = new System.Drawing.Point(405, 72);
            this.tb_elementcount_3.Name = "tb_elementcount_3";
            this.tb_elementcount_3.Size = new System.Drawing.Size(89, 21);
            this.tb_elementcount_3.TabIndex = 14;
            this.tb_elementcount_3.Text = "16";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "elementcount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(331, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "beamcount";
            // 
            // tb_beamcount_3
            // 
            this.tb_beamcount_3.Location = new System.Drawing.Point(405, 45);
            this.tb_beamcount_3.Name = "tb_beamcount_3";
            this.tb_beamcount_3.Size = new System.Drawing.Size(89, 21);
            this.tb_beamcount_3.TabIndex = 13;
            this.tb_beamcount_3.Text = "10";
            // 
            // tb_dlyperpulser_3
            // 
            this.tb_dlyperpulser_3.Location = new System.Drawing.Point(405, 97);
            this.tb_dlyperpulser_3.Name = "tb_dlyperpulser_3";
            this.tb_dlyperpulser_3.Size = new System.Drawing.Size(89, 21);
            this.tb_dlyperpulser_3.TabIndex = 17;
            this.tb_dlyperpulser_3.Text = "10";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(310, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "delayperpulser";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(676, 157);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel_control
            // 
            this.panel_control.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_control.Controls.Add(this.button2);
            this.panel_control.Controls.Add(this.btn_dbg);
            this.panel_control.Controls.Add(this.button8);
            this.panel_control.Controls.Add(this.tb_probsel);
            this.panel_control.Controls.Add(this.textBox_ascan_index);
            this.panel_control.Controls.Add(this.tab_beamStrat);
            this.panel_control.Controls.Add(this.button5);
            this.panel_control.Controls.Add(this.panel_test);
            this.panel_control.Controls.Add(this.button4);
            this.panel_control.Controls.Add(this.cb_scalingtype);
            this.panel_control.Controls.Add(this.cb_rectificationtype);
            this.panel_control.Controls.Add(this.cb_ascandatasize);
            this.panel_control.Location = new System.Drawing.Point(12, 48);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(684, 493);
            this.panel_control.TabIndex = 42;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(298, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(206, 35);
            this.button2.TabIndex = 46;
            this.button2.Text = "Create beam from Form";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_dbg
            // 
            this.btn_dbg.Location = new System.Drawing.Point(514, 385);
            this.btn_dbg.Name = "btn_dbg";
            this.btn_dbg.Size = new System.Drawing.Size(75, 23);
            this.btn_dbg.TabIndex = 45;
            this.btn_dbg.Text = "dbg";
            this.btn_dbg.UseVisualStyleBackColor = true;
            this.btn_dbg.Click += new System.EventHandler(this.btn_dbg_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(10, 360);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(116, 34);
            this.button8.TabIndex = 44;
            this.button8.Text = "apply_config";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // tb_probsel
            // 
            this.tb_probsel.Location = new System.Drawing.Point(415, 412);
            this.tb_probsel.Name = "tb_probsel";
            this.tb_probsel.Size = new System.Drawing.Size(73, 21);
            this.tb_probsel.TabIndex = 43;
            // 
            // textBox_ascan_index
            // 
            this.textBox_ascan_index.Location = new System.Drawing.Point(415, 439);
            this.textBox_ascan_index.Name = "textBox_ascan_index";
            this.textBox_ascan_index.Size = new System.Drawing.Size(75, 21);
            this.textBox_ascan_index.TabIndex = 42;
            this.textBox_ascan_index.Text = "0";
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
            this.panel_cscan.Size = new System.Drawing.Size(1171, 238);
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
            this.panel_cscan_control.Size = new System.Drawing.Size(369, 232);
            this.panel_cscan_control.TabIndex = 1;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(130, 172);
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
            this.panel_plot_cscan.Location = new System.Drawing.Point(378, 3);
            this.panel_plot_cscan.Name = "panel_plot_cscan";
            this.panel_plot_cscan.Size = new System.Drawing.Size(790, 232);
            this.panel_plot_cscan.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 994);
            this.Controls.Add(this.panel_cscan);
            this.Controls.Add(this.panel_control);
            this.Controls.Add(this.label_focuspx_status);
            this.Controls.Add(this.panel_graph);
            this.Controls.Add(this.chart_prob);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl2.ResumeLayout(false);
            this.panel_test.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_prob)).EndInit();
            this.panel_graph.ResumeLayout(false);
            this.tab_beamStrat.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel_custom2.ResumeLayout(false);
            this.panel_custom2.PerformLayout();
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
        private System.Windows.Forms.TextBox tb_beamcount;
        private System.Windows.Forms.TextBox tb_elementcount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_dlyperpulser;
        private System.Windows.Forms.ComboBox cb_ascandatasize;
        private System.Windows.Forms.ComboBox cb_rectificationtype;
        private System.Windows.Forms.ComboBox cb_scalingtype;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel_test;
        private System.Windows.Forms.Panel panel_plot;
        private System.Timers.Timer timer1;
        private System.Windows.Forms.Panel panel_plot_ascan;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_prob;
        private System.Windows.Forms.TableLayoutPanel panel_graph;
        private System.Windows.Forms.Label label_focuspx_status;
        private System.Windows.Forms.TabControl tab_beamStrat;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel_control;
        private System.Windows.Forms.Panel panel_custom2;
        private System.Windows.Forms.TextBox textBox_ascan_index;
        private System.Windows.Forms.TextBox tb_elementcount_3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_beamcount_3;
        private System.Windows.Forms.TextBox tb_dlyperpulser_3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_probsel;
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
    }
}

