namespace _2022_Test
{
    partial class EditForm
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
            this.datagridview1 = new Sunny.UI.UIDataGridView();
            this.List = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pan = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.edit_btn = new Sunny.UI.UIButton();
            this.close_btn = new Sunny.UI.UIButton();
            this.uiPrg_FileCreate = new Sunny.UI.UIRoundProcess();
            this.uiButton1 = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview1)).BeginInit();
            this.SuspendLayout();
            // 
            // datagridview1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.datagridview1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.datagridview1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridview1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.datagridview1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.datagridview1.ColumnHeadersHeight = 32;
            this.datagridview1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.List,
            this.result,
            this.Pan});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridview1.DefaultCellStyle = dataGridViewCellStyle3;
            this.datagridview1.EnableHeadersVisualStyles = false;
            this.datagridview1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.datagridview1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.datagridview1.Location = new System.Drawing.Point(2, 2);
            this.datagridview1.MultiSelect = false;
            this.datagridview1.Name = "datagridview1";
            this.datagridview1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.datagridview1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.datagridview1.RowTemplate.Height = 23;
            this.datagridview1.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.datagridview1.ScrollBarRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(89)))), ((int)(((byte)(162)))));
            this.datagridview1.SelectedIndex = -1;
            this.datagridview1.Size = new System.Drawing.Size(400, 446);
            this.datagridview1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.datagridview1.Style = Sunny.UI.UIStyle.Custom;
            this.datagridview1.TabIndex = 127;
            this.datagridview1.TabStop = false;
            this.datagridview1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            // edit_btn
            // 
            this.edit_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.edit_btn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edit_btn.LightStyle = true;
            this.edit_btn.Location = new System.Drawing.Point(234, 454);
            this.edit_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.edit_btn.Name = "edit_btn";
            this.edit_btn.Size = new System.Drawing.Size(81, 36);
            this.edit_btn.TabIndex = 128;
            this.edit_btn.Text = "수정";
            this.edit_btn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.edit_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.edit_btn_MouseUp);
            // 
            // close_btn
            // 
            this.close_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_btn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_btn.LightStyle = true;
            this.close_btn.Location = new System.Drawing.Point(321, 454);
            this.close_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(81, 36);
            this.close_btn.TabIndex = 129;
            this.close_btn.Text = "닫기";
            this.close_btn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.close_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.close_btn_MouseUp);
            // 
            // uiPrg_FileCreate
            // 
            this.uiPrg_FileCreate.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.uiPrg_FileCreate.Location = new System.Drawing.Point(123, 168);
            this.uiPrg_FileCreate.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPrg_FileCreate.Name = "uiPrg_FileCreate";
            this.uiPrg_FileCreate.Size = new System.Drawing.Size(156, 130);
            this.uiPrg_FileCreate.TabIndex = 130;
            this.uiPrg_FileCreate.Text = "0.0%";
            this.uiPrg_FileCreate.Visible = false;
            this.uiPrg_FileCreate.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiButton1.LightStyle = true;
            this.uiButton1.Location = new System.Drawing.Point(65, 454);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(81, 36);
            this.uiButton1.TabIndex = 131;
            this.uiButton1.Text = "수정";
            this.uiButton1.Visible = false;
            this.uiButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 498);
            this.Controls.Add(this.uiButton1);
            this.Controls.Add(this.uiPrg_FileCreate);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.edit_btn);
            this.Controls.Add(this.datagridview1);
            this.Name = "EditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "데이터 수정";
            this.Load += new System.EventHandler(this.EditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridview1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIDataGridView datagridview1;
        private Sunny.UI.UIButton edit_btn;
        private Sunny.UI.UIButton close_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn List;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
        private System.Windows.Forms.DataGridViewComboBoxColumn Pan;
        private Sunny.UI.UIRoundProcess uiPrg_FileCreate;
        private Sunny.UI.UIButton uiButton1;
    }
}