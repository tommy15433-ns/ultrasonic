namespace _2022_Test
{
    partial class Select_TEST_Form
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chart_Powering = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel35 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.datagridview1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Stop_Picker = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.Serial_Number_TextBox = new System.Windows.Forms.TextBox();
            this.Start_Picker = new System.Windows.Forms.DateTimePicker();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.Car_Number_TextBox = new System.Windows.Forms.TextBox();
            this.PyeonSung_Number_TextBox = new System.Windows.Forms.TextBox();
            this.Tester_TextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Edit_btn = new System.Windows.Forms.Button();
            this.Delete_BTN = new System.Windows.Forms.Button();
            this.Progress_PIC = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.New_TEST_BTN = new System.Windows.Forms.Button();
            this.EXIT_BTN = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Powering)).BeginInit();
            this.panel35.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PIC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1278, 50);
            this.panel1.TabIndex = 0;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "USER INFORMATION";
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM3";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 250;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(40, 40);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1534, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "LINK";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(0, 755);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1278, 50);
            this.panel5.TabIndex = 1;
            // 
            // chart_Powering
            // 
            this.chart_Powering.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX.Interval = 1D;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelAutoFitMaxFontSize = 9;
            chartArea2.AxisX.LabelAutoFitMinFontSize = 5;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.Title = "시간(초)";
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisY.Interval = 2D;
            chartArea2.AxisY.Maximum = 22D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            chartArea2.Name = "ChartArea1";
            this.chart_Powering.ChartAreas.Add(chartArea2);
            legend2.AutoFitMinFontSize = 5;
            legend2.DockedToChartArea = "ChartArea1";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Enabled = false;
            legend2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.IsDockedInsideChartArea = false;
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            this.chart_Powering.Legends.Add(legend2);
            this.chart_Powering.Location = new System.Drawing.Point(227, 6);
            this.chart_Powering.Name = "chart_Powering";
            series3.BorderWidth = 5;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series3.LabelBorderWidth = 5;
            series3.Legend = "Legend1";
            series3.MarkerBorderWidth = 5;
            series3.Name = "ACPT";
            series4.BorderWidth = 5;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series4.LabelBorderWidth = 5;
            series4.Legend = "Legend1";
            series4.MarkerBorderWidth = 5;
            series4.Name = "DCPT";
            this.chart_Powering.Series.Add(series3);
            this.chart_Powering.Series.Add(series4);
            this.chart_Powering.Size = new System.Drawing.Size(662, 455);
            this.chart_Powering.TabIndex = 116;
            this.chart_Powering.Text = "chart1";
            // 
            // panel35
            // 
            this.panel35.Controls.Add(this.button1);
            this.panel35.Controls.Add(this.datagridview1);
            this.panel35.Controls.Add(this.groupBox1);
            this.panel35.Controls.Add(this.Edit_btn);
            this.panel35.Controls.Add(this.Delete_BTN);
            this.panel35.Controls.Add(this.Progress_PIC);
            this.panel35.Controls.Add(this.pictureBox1);
            this.panel35.Controls.Add(this.New_TEST_BTN);
            this.panel35.Controls.Add(this.EXIT_BTN);
            this.panel35.Location = new System.Drawing.Point(5, 50);
            this.panel35.Name = "panel35";
            this.panel35.Size = new System.Drawing.Size(1183, 702);
            this.panel35.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Image = global::_2022_Test.Properties.Resources.icons8_search_441;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(584, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 80);
            this.button1.TabIndex = 138;
            this.button1.TabStop = false;
            this.button1.Text = "Search";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // datagridview1
            // 
            this.datagridview1.AllowUserToResizeColumns = false;
            this.datagridview1.AllowUserToResizeRows = false;
            this.datagridview1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridview1.BackgroundColor = System.Drawing.Color.Silver;
            this.datagridview1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.datagridview1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.datagridview1.ColumnHeadersHeight = 30;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridview1.DefaultCellStyle = dataGridViewCellStyle5;
            this.datagridview1.Location = new System.Drawing.Point(6, 165);
            this.datagridview1.Name = "datagridview1";
            this.datagridview1.ReadOnly = true;
            this.datagridview1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.datagridview1.RowTemplate.Height = 40;
            this.datagridview1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.datagridview1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridview1.Size = new System.Drawing.Size(1172, 513);
            this.datagridview1.TabIndex = 137;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Silver;
            this.groupBox1.Controls.Add(this.Stop_Picker);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel10);
            this.groupBox1.Controls.Add(this.Serial_Number_TextBox);
            this.groupBox1.Controls.Add(this.Start_Picker);
            this.groupBox1.Controls.Add(this.panel8);
            this.groupBox1.Controls.Add(this.panel7);
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.Car_Number_TextBox);
            this.groupBox1.Controls.Add(this.PyeonSung_Number_TextBox);
            this.groupBox1.Controls.Add(this.Tester_TextBox);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 153);
            this.groupBox1.TabIndex = 136;
            this.groupBox1.TabStop = false;
            // 
            // Stop_Picker
            // 
            this.Stop_Picker.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Stop_Picker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Stop_Picker.Location = new System.Drawing.Point(431, 49);
            this.Stop_Picker.Name = "Stop_Picker";
            this.Stop_Picker.Size = new System.Drawing.Size(125, 29);
            this.Stop_Picker.TabIndex = 138;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(305, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(120, 29);
            this.panel3.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(16, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "END DATE";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel10.Controls.Add(this.label10);
            this.panel10.Location = new System.Drawing.Point(305, 117);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(120, 29);
            this.panel10.TabIndex = 131;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(3, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "Serial Number";
            // 
            // Serial_Number_TextBox
            // 
            this.Serial_Number_TextBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Serial_Number_TextBox.Location = new System.Drawing.Point(431, 117);
            this.Serial_Number_TextBox.Name = "Serial_Number_TextBox";
            this.Serial_Number_TextBox.Size = new System.Drawing.Size(125, 29);
            this.Serial_Number_TextBox.TabIndex = 132;
            // 
            // Start_Picker
            // 
            this.Start_Picker.CalendarFont = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Start_Picker.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Start_Picker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Start_Picker.Location = new System.Drawing.Point(139, 48);
            this.Start_Picker.Name = "Start_Picker";
            this.Start_Picker.Size = new System.Drawing.Size(125, 29);
            this.Start_Picker.TabIndex = 58;
            this.Start_Picker.Value = new System.DateTime(2022, 11, 8, 11, 43, 13, 0);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel8.Controls.Add(this.label8);
            this.panel8.Location = new System.Drawing.Point(13, 117);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(120, 29);
            this.panel8.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(13, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "Car Number";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel7.Controls.Add(this.label7);
            this.panel7.Location = new System.Drawing.Point(305, 82);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(120, 29);
            this.panel7.TabIndex = 55;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(5, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "Train Number";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel6.Controls.Add(this.label5);
            this.panel6.Location = new System.Drawing.Point(13, 47);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(120, 29);
            this.panel6.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(10, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "START DATE";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(13, 82);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 29);
            this.panel4.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(31, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tester";
            // 
            // Car_Number_TextBox
            // 
            this.Car_Number_TextBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Car_Number_TextBox.Location = new System.Drawing.Point(139, 117);
            this.Car_Number_TextBox.Name = "Car_Number_TextBox";
            this.Car_Number_TextBox.Size = new System.Drawing.Size(125, 29);
            this.Car_Number_TextBox.TabIndex = 56;
            // 
            // PyeonSung_Number_TextBox
            // 
            this.PyeonSung_Number_TextBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PyeonSung_Number_TextBox.Location = new System.Drawing.Point(431, 82);
            this.PyeonSung_Number_TextBox.Name = "PyeonSung_Number_TextBox";
            this.PyeonSung_Number_TextBox.Size = new System.Drawing.Size(125, 29);
            this.PyeonSung_Number_TextBox.TabIndex = 55;
            // 
            // Tester_TextBox
            // 
            this.Tester_TextBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Tester_TextBox.Location = new System.Drawing.Point(139, 82);
            this.Tester_TextBox.Name = "Tester_TextBox";
            this.Tester_TextBox.Size = new System.Drawing.Size(125, 29);
            this.Tester_TextBox.TabIndex = 53;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(572, 42);
            this.panel2.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(239, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "TEST INFO";
            // 
            // Edit_btn
            // 
            this.Edit_btn.BackColor = System.Drawing.Color.Gainsboro;
            this.Edit_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Edit_btn.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Edit_btn.Image = global::_2022_Test.Properties.Resources.icons8_cashbook_44;
            this.Edit_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Edit_btn.Location = new System.Drawing.Point(846, 11);
            this.Edit_btn.Name = "Edit_btn";
            this.Edit_btn.Size = new System.Drawing.Size(100, 80);
            this.Edit_btn.TabIndex = 134;
            this.Edit_btn.TabStop = false;
            this.Edit_btn.Text = "Test";
            this.Edit_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Edit_btn.UseVisualStyleBackColor = true;
            this.Edit_btn.Click += new System.EventHandler(this.Edit_btn_Click);
            // 
            // Delete_BTN
            // 
            this.Delete_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Delete_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Delete_BTN.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Delete_BTN.Image = global::_2022_Test.Properties.Resources.icons8_trash_can_44;
            this.Delete_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Delete_BTN.Location = new System.Drawing.Point(952, 11);
            this.Delete_BTN.Name = "Delete_BTN";
            this.Delete_BTN.Size = new System.Drawing.Size(100, 80);
            this.Delete_BTN.TabIndex = 133;
            this.Delete_BTN.TabStop = false;
            this.Delete_BTN.Text = "Delete";
            this.Delete_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Delete_BTN.UseVisualStyleBackColor = true;
            // 
            // Progress_PIC
            // 
            this.Progress_PIC.BackColor = System.Drawing.Color.White;
            this.Progress_PIC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Progress_PIC.Image = global::_2022_Test.Properties.Resources.icons8_iphone_spinner_55;
            this.Progress_PIC.Location = new System.Drawing.Point(553, 400);
            this.Progress_PIC.Name = "Progress_PIC";
            this.Progress_PIC.Size = new System.Drawing.Size(55, 57);
            this.Progress_PIC.TabIndex = 130;
            this.Progress_PIC.TabStop = false;
            this.Progress_PIC.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::_2022_Test.Properties.Resources.엔에스텍_확정_문서용_;
            this.pictureBox1.Location = new System.Drawing.Point(998, 98);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // New_TEST_BTN
            // 
            this.New_TEST_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.New_TEST_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.New_TEST_BTN.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.New_TEST_BTN.FlatAppearance.BorderSize = 0;
            this.New_TEST_BTN.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.New_TEST_BTN.Image = global::_2022_Test.Properties.Resources.icons8_cashbook_44;
            this.New_TEST_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.New_TEST_BTN.Location = new System.Drawing.Point(740, 11);
            this.New_TEST_BTN.Name = "New_TEST_BTN";
            this.New_TEST_BTN.Size = new System.Drawing.Size(100, 80);
            this.New_TEST_BTN.TabIndex = 41;
            this.New_TEST_BTN.TabStop = false;
            this.New_TEST_BTN.Text = "New Test";
            this.New_TEST_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.New_TEST_BTN.UseVisualStyleBackColor = true;
            this.New_TEST_BTN.Click += new System.EventHandler(this.New_TEST_BTN_Click);
            // 
            // EXIT_BTN
            // 
            this.EXIT_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EXIT_BTN.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EXIT_BTN.Image = global::_2022_Test.Properties.Resources.icons8_logout_44;
            this.EXIT_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.EXIT_BTN.Location = new System.Drawing.Point(1058, 11);
            this.EXIT_BTN.Name = "EXIT_BTN";
            this.EXIT_BTN.Size = new System.Drawing.Size(100, 80);
            this.EXIT_BTN.TabIndex = 40;
            this.EXIT_BTN.TabStop = false;
            this.EXIT_BTN.Text = "EXIT";
            this.EXIT_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.EXIT_BTN.UseVisualStyleBackColor = true;
            this.EXIT_BTN.Click += new System.EventHandler(this.EXIT_BTN_Click);
            // 
            // Select_TEST_Form
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.ControlBox = false;
            this.Controls.Add(this.panel35);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Select_TEST_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Powering)).EndInit();
            this.panel35.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridview1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PIC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button EXIT_BTN;
        private KSS_Library.KSS_BTN ksS_BTN1;
        private KSS_Library.KSS_BTN ksS_BTN24;
        private KSS_Library.KSS_BTN Print_BTN;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Powering;
        private System.Windows.Forms.Panel panel35;
        private System.Windows.Forms.PictureBox Progress_PIC;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Serial_Number_TextBox;
        private System.Windows.Forms.Button Delete_BTN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker Start_Picker;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Car_Number_TextBox;
        private System.Windows.Forms.TextBox PyeonSung_Number_TextBox;
        private System.Windows.Forms.TextBox Tester_TextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker Stop_Picker;
        public System.Windows.Forms.DataGridView datagridview1;
        public System.Windows.Forms.Button New_TEST_BTN;
        public System.Windows.Forms.Button Edit_btn;
        public System.Windows.Forms.Button button1;
    }
}