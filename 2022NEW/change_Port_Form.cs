using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using static System.Console;


namespace _2022_Test
{
    public partial class init_Tester_info_Form : Form
    {

        //System.IO.Ports.SerialPort spCOM;
        private Thread thRun;
        Panel[] COM_LED, ST_LED,FA_LED;
        TextBox [] lbv;
        TextBox[] tx;
        public byte[] ReturnD = new byte[1024];
        static byte[] RevQbuffers = new byte[2048];
        static int RevCount_WPt, RevCount_RPt, ReadCount = 0;
        static byte bcc;
        static byte[] btData = new byte[1024];
        static int iLen = 0;

       MainForm run_form;
     


        bool half = false;
        bool full = false;
        bool EF1 = false;
        bool EF2 = false;
        bool CF1 = false;
        bool CF2 = false;

        int communication_count;


        bool On;
        bool Connect;
        bool FormMove = false;
        Point prePoint, curPoint;
        Point Pos;


        byte SetBit(byte x, int n, int b)
        {
            
            if (b == 1)
                return (byte)(x | (1 << n));
            else
                return (byte)(x & (~(1 << n)));

        }

        int GetBit(byte x, int n)
        {
            return (x & (1 << n)) >> n;

        }
        byte ReadBit(byte x, int n)
        {

            return (byte)(x & (1 << n));

        }

        private void Test_BTN_Click(object sender, EventArgs e)
        {         
          
        }


        private void Exit_BTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

  

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            //    DialogResult dialogResult = MessageBox.Show("사용자 정보를 올바르게 입력하셨습니까? 시험화면으로 접속할까요?", "check", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        run_form = new RunTest_Form();
            //        run_form.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("취소되었습니다. 사용자 정보를 다시 확인후 OK버튼을 눌러주세요");
            //    }

          
            //run_form.new_test_check = true;
            //run_form.test_check = false;
           

            run_form.ShowDialog();
            run_form.Dispose();
        }

        public init_Tester_info_Form()
        {
            InitializeComponent();

           

            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }

        public init_Tester_info_Form(MainForm _form)
        {
            InitializeComponent();

            run_form = _form;

            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        
            
            tb_Date.Value = monthCalendar1.SelectionEnd;
        }

        private void EXIT_BTN_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void init_Tester_info_Form_Load(object sender, EventArgs e)
        {
            run_form = new MainForm(this);

            tb_Date.Value = DateTime.Now;
            monthCalendar1.TodayDate = tb_Date.Value;


        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            curPoint = Cursor.Position;

            FormMove = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            FormMove = true;
            prePoint = Cursor.Position;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = "dat";
            fd.InitialDirectory = "C\\data";
            fd.Filter = "DataFile(*.dat)|*.dat";
            fd.ShowDialog();

            if (fd.FileNames.Length > 0)

            {
                foreach (string fn in fd.FileNames)
                {
                    tb_oldDataFile.Text = fn;
                   //run_form.LoadTestedData(fn);
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormMove)
            {
                curPoint = Cursor.Position;

                int form_x = this.Location.X - (prePoint.X - curPoint.X);
                int form_y = this.Location.Y - (prePoint.Y - curPoint.Y);

                this.Location = new Point(form_x, form_y);

                if (this.WindowState != FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Normal;

                }
                prePoint = curPoint;

            }
        }

        //--------------------------------------------------------------------------------------------------------------------


    }
}
