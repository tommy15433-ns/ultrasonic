namespace _2022_Test
{
    partial class ReportForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Start_Picker = new System.Windows.Forms.DateTimePicker();
            this.Stop_Picker = new System.Windows.Forms.DateTimePicker();
            this.Start_Day_Label = new System.Windows.Forms.Label();
            this.Stop_Day_Label = new System.Windows.Forms.Label();
            this.Serial_Number_Label = new System.Windows.Forms.Label();
            this.Car_Number_Label = new System.Windows.Forms.Label();
            this.PyeonSung_Number_Label = new System.Windows.Forms.Label();
            this.Tester_Label = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.datagridview1 = new Sunny.UI.UIDataGridView();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.Car_Number_TextBox = new System.Windows.Forms.TextBox();
            this.Tester_TextBox = new System.Windows.Forms.TextBox();
            this.Serial_Number_TextBox = new System.Windows.Forms.TextBox();
            this.PyeonSung_Number_TextBox = new System.Windows.Forms.TextBox();
            this.BackColor = new System.Windows.Forms.Panel();
            this.Name_Label = new System.Windows.Forms.Label();
            this.datagridview2 = new Sunny.UI.UIDataGridView();
            this.List = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pan = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Progress_PIC = new System.Windows.Forms.PictureBox();
            this.Save_PDF_btn = new System.Windows.Forms.Button();
            this.Print_BTN = new System.Windows.Forms.Button();
            this.Exit_BTN = new System.Windows.Forms.Button();
            this.Edit_btn = new System.Windows.Forms.Button();
            this.Delete_BTN = new System.Windows.Forms.Button();
            this.Search_BTN = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.BackColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PIC)).BeginInit();
            this.SuspendLayout();
            // 
            // Start_Picker
            // 
            this.Start_Picker.CalendarFont = new System.Drawing.Font("맑은 고딕", 10F);
            this.Start_Picker.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Start_Picker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Start_Picker.Location = new System.Drawing.Point(91, 28);
            this.Start_Picker.Name = "Start_Picker";
            this.Start_Picker.Size = new System.Drawing.Size(117, 25);
            this.Start_Picker.TabIndex = 104;
            this.Start_Picker.TabStop = false;
            // 
            // Stop_Picker
            // 
            this.Stop_Picker.CalendarFont = new System.Drawing.Font("맑은 고딕", 10F);
            this.Stop_Picker.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Stop_Picker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Stop_Picker.Location = new System.Drawing.Point(313, 28);
            this.Stop_Picker.Name = "Stop_Picker";
            this.Stop_Picker.Size = new System.Drawing.Size(116, 25);
            this.Stop_Picker.TabIndex = 105;
            this.Stop_Picker.TabStop = false;
            // 
            // Start_Day_Label
            // 
            this.Start_Day_Label.AutoSize = true;
            this.Start_Day_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Start_Day_Label.Location = new System.Drawing.Point(6, 33);
            this.Start_Day_Label.Name = "Start_Day_Label";
            this.Start_Day_Label.Size = new System.Drawing.Size(71, 17);
            this.Start_Day_Label.TabIndex = 106;
            this.Start_Day_Label.Text = "Start Date";
            // 
            // Stop_Day_Label
            // 
            this.Stop_Day_Label.AutoSize = true;
            this.Stop_Day_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Stop_Day_Label.Location = new System.Drawing.Point(227, 31);
            this.Stop_Day_Label.Name = "Stop_Day_Label";
            this.Stop_Day_Label.Size = new System.Drawing.Size(63, 17);
            this.Stop_Day_Label.TabIndex = 107;
            this.Stop_Day_Label.Text = "End date";
            // 
            // Serial_Number_Label
            // 
            this.Serial_Number_Label.AutoSize = true;
            this.Serial_Number_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Serial_Number_Label.Location = new System.Drawing.Point(212, 72);
            this.Serial_Number_Label.Name = "Serial_Number_Label";
            this.Serial_Number_Label.Size = new System.Drawing.Size(97, 17);
            this.Serial_Number_Label.TabIndex = 109;
            this.Serial_Number_Label.Text = "Serial Number";
            // 
            // Car_Number_Label
            // 
            this.Car_Number_Label.AutoSize = true;
            this.Car_Number_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Car_Number_Label.Location = new System.Drawing.Point(4, 117);
            this.Car_Number_Label.Name = "Car_Number_Label";
            this.Car_Number_Label.Size = new System.Drawing.Size(83, 17);
            this.Car_Number_Label.TabIndex = 110;
            this.Car_Number_Label.Text = "Car Number";
            // 
            // PyeonSung_Number_Label
            // 
            this.PyeonSung_Number_Label.AutoSize = true;
            this.PyeonSung_Number_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PyeonSung_Number_Label.Location = new System.Drawing.Point(214, 118);
            this.PyeonSung_Number_Label.Name = "PyeonSung_Number_Label";
            this.PyeonSung_Number_Label.Size = new System.Drawing.Size(95, 17);
            this.PyeonSung_Number_Label.TabIndex = 114;
            this.PyeonSung_Number_Label.Text = "Train Number";
            // 
            // Tester_Label
            // 
            this.Tester_Label.AutoSize = true;
            this.Tester_Label.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Tester_Label.Location = new System.Drawing.Point(14, 76);
            this.Tester_Label.Name = "Tester_Label";
            this.Tester_Label.Size = new System.Drawing.Size(46, 17);
            this.Tester_Label.TabIndex = 113;
            this.Tester_Label.Text = "Tester";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(43, 43);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // datagridview1
            // 
            this.datagridview1.AllowUserToResizeColumns = false;
            this.datagridview1.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.datagridview1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.datagridview1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridview1.BackgroundColor = System.Drawing.Color.White;
            this.datagridview1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.datagridview1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.datagridview1.ColumnHeadersHeight = 32;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridview1.DefaultCellStyle = dataGridViewCellStyle13;
            this.datagridview1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.datagridview1.EnableHeadersVisualStyles = false;
            this.datagridview1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.datagridview1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.datagridview1.Location = new System.Drawing.Point(7, 59);
            this.datagridview1.Name = "datagridview1";
            this.datagridview1.ReadOnly = true;
            this.datagridview1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview1.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.datagridview1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.datagridview1.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.datagridview1.RowTemplate.Height = 40;
            this.datagridview1.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.datagridview1.ScrollBarRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.datagridview1.SelectedIndex = -1;
            this.datagridview1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridview1.Size = new System.Drawing.Size(1259, 721);
            this.datagridview1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.datagridview1.Style = Sunny.UI.UIStyle.Custom;
            this.datagridview1.TabIndex = 122;
            this.datagridview1.TabStop = false;
            this.datagridview1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.datagridview1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridview1_CellDoubleClick);
            this.datagridview1.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.Start_Picker);
            this.uiGroupBox1.Controls.Add(this.Car_Number_Label);
            this.uiGroupBox1.Controls.Add(this.Car_Number_TextBox);
            this.uiGroupBox1.Controls.Add(this.Stop_Picker);
            this.uiGroupBox1.Controls.Add(this.Serial_Number_Label);
            this.uiGroupBox1.Controls.Add(this.Tester_TextBox);
            this.uiGroupBox1.Controls.Add(this.Start_Day_Label);
            this.uiGroupBox1.Controls.Add(this.Serial_Number_TextBox);
            this.uiGroupBox1.Controls.Add(this.Stop_Day_Label);
            this.uiGroupBox1.Controls.Add(this.PyeonSung_Number_TextBox);
            this.uiGroupBox1.Controls.Add(this.Tester_Label);
            this.uiGroupBox1.Controls.Add(this.PyeonSung_Number_Label);
            this.uiGroupBox1.FillColor = System.Drawing.SystemColors.Control;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.uiGroupBox1.Location = new System.Drawing.Point(13, 788);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Radius = 10;
            this.uiGroupBox1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.uiGroupBox1.Size = new System.Drawing.Size(442, 153);
            this.uiGroupBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox1.TabIndex = 123;
            this.uiGroupBox1.Text = null;
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // Car_Number_TextBox
            // 
            this.Car_Number_TextBox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Car_Number_TextBox.Location = new System.Drawing.Point(91, 112);
            this.Car_Number_TextBox.Name = "Car_Number_TextBox";
            this.Car_Number_TextBox.Size = new System.Drawing.Size(117, 25);
            this.Car_Number_TextBox.TabIndex = 2;
            this.Car_Number_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Car_Number_TextBox.MouseLeave += new System.EventHandler(this.PyeonSung_Number_TextBox_MouseLeave);
            this.Car_Number_TextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PyeonSung_Number_TextBox_MouseMove);
            // 
            // Tester_TextBox
            // 
            this.Tester_TextBox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Tester_TextBox.Location = new System.Drawing.Point(91, 73);
            this.Tester_TextBox.Name = "Tester_TextBox";
            this.Tester_TextBox.Size = new System.Drawing.Size(117, 25);
            this.Tester_TextBox.TabIndex = 3;
            this.Tester_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tester_TextBox.MouseLeave += new System.EventHandler(this.PyeonSung_Number_TextBox_MouseLeave);
            this.Tester_TextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PyeonSung_Number_TextBox_MouseMove);
            // 
            // Serial_Number_TextBox
            // 
            this.Serial_Number_TextBox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Serial_Number_TextBox.Location = new System.Drawing.Point(313, 68);
            this.Serial_Number_TextBox.Name = "Serial_Number_TextBox";
            this.Serial_Number_TextBox.Size = new System.Drawing.Size(116, 25);
            this.Serial_Number_TextBox.TabIndex = 1;
            this.Serial_Number_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Serial_Number_TextBox.MouseLeave += new System.EventHandler(this.PyeonSung_Number_TextBox_MouseLeave);
            this.Serial_Number_TextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PyeonSung_Number_TextBox_MouseMove);
            // 
            // PyeonSung_Number_TextBox
            // 
            this.PyeonSung_Number_TextBox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PyeonSung_Number_TextBox.Location = new System.Drawing.Point(313, 114);
            this.PyeonSung_Number_TextBox.Name = "PyeonSung_Number_TextBox";
            this.PyeonSung_Number_TextBox.Size = new System.Drawing.Size(116, 25);
            this.PyeonSung_Number_TextBox.TabIndex = 4;
            this.PyeonSung_Number_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PyeonSung_Number_TextBox.MouseLeave += new System.EventHandler(this.PyeonSung_Number_TextBox_MouseLeave);
            this.PyeonSung_Number_TextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PyeonSung_Number_TextBox_MouseMove);
            // 
            // BackColor
            // 
            this.BackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.BackColor.Controls.Add(this.Name_Label);
            this.BackColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.BackColor.Location = new System.Drawing.Point(0, 0);
            this.BackColor.Name = "BackColor";
            this.BackColor.Size = new System.Drawing.Size(1278, 45);
            this.BackColor.TabIndex = 125;
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.Name_Label.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name_Label.ForeColor = System.Drawing.Color.White;
            this.Name_Label.Location = new System.Drawing.Point(3, 9);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(130, 25);
            this.Name_Label.TabIndex = 8;
            this.Name_Label.Text = "TEST RESULT";
            // 
            // datagridview2
            // 
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.datagridview2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.datagridview2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridview2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.datagridview2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.datagridview2.ColumnHeadersHeight = 32;
            this.datagridview2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.List,
            this.result,
            this.Pan});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridview2.DefaultCellStyle = dataGridViewCellStyle18;
            this.datagridview2.EnableHeadersVisualStyles = false;
            this.datagridview2.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.datagridview2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.datagridview2.Location = new System.Drawing.Point(519, 799);
            this.datagridview2.MultiSelect = false;
            this.datagridview2.Name = "datagridview2";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview2.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.datagridview2.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.datagridview2.RowTemplate.Height = 23;
            this.datagridview2.ScrollBarRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.datagridview2.SelectedIndex = -1;
            this.datagridview2.Size = new System.Drawing.Size(73, 20);
            this.datagridview2.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.datagridview2.Style = Sunny.UI.UIStyle.Custom;
            this.datagridview2.TabIndex = 128;
            this.datagridview2.TabStop = false;
            this.datagridview2.Visible = false;
            this.datagridview2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // List
            // 
            this.List.HeaderText = "시험항목";
            this.List.Name = "List";
            // 
            // result
            // 
            this.result.HeaderText = "측정값";
            this.result.Name = "result";
            // 
            // Pan
            // 
            this.Pan.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Pan.HeaderText = "판정";
            this.Pan.Items.AddRange(new object[] {
            "NG",
            "OK"});
            this.Pan.Name = "Pan";
            this.Pan.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Pan.Sorted = true;
            this.Pan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Progress_PIC
            // 
            this.Progress_PIC.BackColor = System.Drawing.Color.White;
            this.Progress_PIC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Progress_PIC.Image = global::_2022_Test.Properties.Resources.icons8_iphone_spinner_55;
            this.Progress_PIC.Location = new System.Drawing.Point(505, 354);
            this.Progress_PIC.Name = "Progress_PIC";
            this.Progress_PIC.Size = new System.Drawing.Size(55, 57);
            this.Progress_PIC.TabIndex = 129;
            this.Progress_PIC.TabStop = false;
            this.Progress_PIC.Visible = false;
            // 
            // Save_PDF_btn
            // 
            this.Save_PDF_btn.BackColor = System.Drawing.Color.Gainsboro;
            this.Save_PDF_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save_PDF_btn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Save_PDF_btn.Image = global::_2022_Test.Properties.Resources.pdf;
            this.Save_PDF_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Save_PDF_btn.Location = new System.Drawing.Point(760, 797);
            this.Save_PDF_btn.Name = "Save_PDF_btn";
            this.Save_PDF_btn.Size = new System.Drawing.Size(100, 34);
            this.Save_PDF_btn.TabIndex = 124;
            this.Save_PDF_btn.TabStop = false;
            this.Save_PDF_btn.Text = "PDF 저장";
            this.Save_PDF_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Save_PDF_btn.UseVisualStyleBackColor = true;
            this.Save_PDF_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Save_PDF_btn_MouseUp);
            // 
            // Print_BTN
            // 
            this.Print_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Print_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Print_BTN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Print_BTN.Image = global::_2022_Test.Properties.Resources.icons8_print_44;
            this.Print_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Print_BTN.Location = new System.Drawing.Point(1056, 861);
            this.Print_BTN.Name = "Print_BTN";
            this.Print_BTN.Size = new System.Drawing.Size(100, 80);
            this.Print_BTN.TabIndex = 4;
            this.Print_BTN.TabStop = false;
            this.Print_BTN.Text = "Print";
            this.Print_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Print_BTN.UseVisualStyleBackColor = true;
            this.Print_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Print_BTN_MouseUp);
            // 
            // Exit_BTN
            // 
            this.Exit_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Exit_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_BTN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit_BTN.Image = global::_2022_Test.Properties.Resources.icons8_logout_441;
            this.Exit_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit_BTN.Location = new System.Drawing.Point(1160, 861);
            this.Exit_BTN.Name = "Exit_BTN";
            this.Exit_BTN.Size = new System.Drawing.Size(100, 80);
            this.Exit_BTN.TabIndex = 3;
            this.Exit_BTN.TabStop = false;
            this.Exit_BTN.Text = "Exit";
            this.Exit_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Exit_BTN.UseVisualStyleBackColor = true;
            this.Exit_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Exit_BTN_MouseUp);
            // 
            // Edit_btn
            // 
            this.Edit_btn.BackColor = System.Drawing.Color.Gainsboro;
            this.Edit_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Edit_btn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Edit_btn.Image = global::_2022_Test.Properties.Resources.edit;
            this.Edit_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Edit_btn.Location = new System.Drawing.Point(860, 811);
            this.Edit_btn.Name = "Edit_btn";
            this.Edit_btn.Size = new System.Drawing.Size(100, 20);
            this.Edit_btn.TabIndex = 117;
            this.Edit_btn.TabStop = false;
            this.Edit_btn.Text = "수정";
            this.Edit_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Edit_btn.UseVisualStyleBackColor = true;
            this.Edit_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Edit_btn_MouseUp);
            // 
            // Delete_BTN
            // 
            this.Delete_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Delete_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Delete_BTN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Delete_BTN.Image = global::_2022_Test.Properties.Resources.icons8_trash_can_44;
            this.Delete_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Delete_BTN.Location = new System.Drawing.Point(952, 861);
            this.Delete_BTN.Name = "Delete_BTN";
            this.Delete_BTN.Size = new System.Drawing.Size(100, 80);
            this.Delete_BTN.TabIndex = 5;
            this.Delete_BTN.TabStop = false;
            this.Delete_BTN.Text = "Delete";
            this.Delete_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Delete_BTN.UseVisualStyleBackColor = true;
            this.Delete_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Delete_BTN_MouseUp);
            // 
            // Search_BTN
            // 
            this.Search_BTN.BackColor = System.Drawing.Color.Gainsboro;
            this.Search_BTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Search_BTN.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Search_BTN.Image = global::_2022_Test.Properties.Resources.icons8_search_4411;
            this.Search_BTN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Search_BTN.Location = new System.Drawing.Point(848, 861);
            this.Search_BTN.Name = "Search_BTN";
            this.Search_BTN.Size = new System.Drawing.Size(100, 80);
            this.Search_BTN.TabIndex = 6;
            this.Search_BTN.TabStop = false;
            this.Search_BTN.Text = "Search";
            this.Search_BTN.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Search_BTN.UseVisualStyleBackColor = true;
            this.Search_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Search_BTN_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.panel1.Location = new System.Drawing.Point(0, 956);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1278, 45);
            this.panel1.TabIndex = 130;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 1000);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Progress_PIC);
            this.Controls.Add(this.datagridview2);
            this.Controls.Add(this.BackColor);
            this.Controls.Add(this.Save_PDF_btn);
            this.Controls.Add(this.Print_BTN);
            this.Controls.Add(this.Exit_BTN);
            this.Controls.Add(this.Edit_btn);
            this.Controls.Add(this.Delete_BTN);
            this.Controls.Add(this.Search_BTN);
            this.Controls.Add(this.datagridview1);
            this.Controls.Add(this.uiGroupBox1);
            this.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridview1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.BackColor.ResumeLayout(false);
            this.BackColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PIC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Exit_BTN;
        private System.Windows.Forms.Button Print_BTN;
        private System.Windows.Forms.Button Delete_BTN;
        private System.Windows.Forms.Button Search_BTN;
        private System.Windows.Forms.DateTimePicker Start_Picker;
        private System.Windows.Forms.DateTimePicker Stop_Picker;
        private System.Windows.Forms.Label Start_Day_Label;
        private System.Windows.Forms.Label Stop_Day_Label;
        private System.Windows.Forms.Label Serial_Number_Label;
        private System.Windows.Forms.Label Car_Number_Label;
        private System.Windows.Forms.Label PyeonSung_Number_Label;
        private System.Windows.Forms.Label Tester_Label;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button Edit_btn;
        private Sunny.UI.UIDataGridView datagridview1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.TextBox Car_Number_TextBox;
        private System.Windows.Forms.TextBox Tester_TextBox;
        private System.Windows.Forms.TextBox Serial_Number_TextBox;
        private System.Windows.Forms.TextBox PyeonSung_Number_TextBox;
        private System.Windows.Forms.Button Save_PDF_btn;
        private System.Windows.Forms.Panel BackColor;
        private System.Windows.Forms.Label Name_Label;
        private Sunny.UI.UIDataGridView datagridview2;
        private System.Windows.Forms.DataGridViewTextBoxColumn List;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
        private System.Windows.Forms.DataGridViewComboBoxColumn Pan;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox Progress_PIC;
        private System.Windows.Forms.Panel panel1;
    }
}