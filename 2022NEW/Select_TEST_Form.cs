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



using System.Runtime.InteropServices;

namespace _2022_Test
{
    public partial class Select_TEST_Form : Form
    {
        string[] tb;
        bool run = false;
        init_Tester_info_Form init_info;
        MainForm rt_form;
        private Thread search_thrun;
        string Report_Route;

        Panel[] COM_LED, ST_LED,FA_LED;
        TextBox [] lbv;

        public bool test_check = false;

        TextBox[] tx;
        public byte[] ReturnD = new byte[1024];
        static byte[] RevQbuffers = new byte[2048];
        static int RevCount_WPt, RevCount_RPt, ReadCount = 0;
        static byte bcc;
        static byte[] btData = new byte[1024];
        static int iLen = 0;

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
        int cnt;


        string _tester_name;
        string _pyunsung_name;
        string _serial_name;
        string _type_of_test;
        string _car_name;
        string _date_time;
        string _type;

        byte SetBit(byte x, int n, int b)
        {
            
            if (b == 1)
                return (byte)(x | (1 << n));
            else
                return (byte)(x & (~(1 << n)));

        }

        private void EXIT_BTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void search_thread()
        {
            CheckForIllegalCrossThreadCalls = false;

            try
            {
                //StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "data" + "\\" + "data.ini");
                //string[] fd = sr.ReadToEnd().Split('!');
                //sr.Close();

                StreamReader sr = new StreamReader(@"reportpath.ini");
                string temp = sr.ReadToEnd();
                sr.Close();


                CultureInfo provider = CultureInfo.InvariantCulture;
                DirectoryInfo DI = new DirectoryInfo(temp);
                FileInfo[] kdd = DI.GetFiles();

                tb = new string[] { Tester_TextBox.Text, Serial_Number_TextBox.Text, Car_Number_TextBox.Text, PyeonSung_Number_TextBox.Text };

                datagridview1.Rows.Clear();

                datagridview1.ColumnCount = 6;

                for (int i = 0; i < datagridview1.ColumnCount; i++)
                {
                    datagridview1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                datagridview1.Columns[0].Name = "DATE";
                datagridview1.Columns[1].Name = "Train Number";
                datagridview1.Columns[2].Name = "Car Number";
                datagridview1.Columns[3].Name = "Serial Number";
                datagridview1.Columns[4].Name = "Type of Test";
                datagridview1.Columns[5].Name = "Tester";
               // datagridview1.Columns[6].Name = "종류";
               // datagridview1.Columns[7].Name = "CTV 유무";

                for (int i = 0; i < kdd.Length; i++)
                {
                    try
                    {
                        string[] nameTemp = kdd[i].ToString().Split(new char[] { '_' });

                        if (nameTemp.Length >= 2)
                        {
                            string timeStart = DateTime.Parse(Start_Picker.Text).ToString("yyyyMMdd0000");
                            string timeEnd = DateTime.Parse(Stop_Picker.Text).ToString("yyyyMMdd") + "2500";


                            if (long.Parse(nameTemp[0]) <= long.Parse(timeEnd) && long.Parse(nameTemp[0]) >= long.Parse(timeStart))
                            {
                                if (Car_Number_TextBox.Text == "" && PyeonSung_Number_TextBox.Text == "" && Tester_TextBox.Text == "" && Serial_Number_TextBox.Text == "" )
                                {
                                    nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                    datagridview1.Rows.Add(nameTemp);
                                }
                                else
                                {

                                    if (Tester_TextBox.Text == nameTemp[1])
                                    {
                                        if (Tester_TextBox.Text != "")
                                        {
                                            nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                            datagridview1.Rows.Add(nameTemp);
                                        }

                                    }
                                    else if (PyeonSung_Number_TextBox.Text == nameTemp[2])
                                    {
                                        if (PyeonSung_Number_TextBox.Text != "")
                                        {
                                            nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                            datagridview1.Rows.Add(nameTemp);
                                        }

                                    }
                                    else if (Car_Number_TextBox.Text == nameTemp[3])
                                    {
                                        if (Car_Number_TextBox.Text != "")
                                        {
                                            nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                            datagridview1.Rows.Add(nameTemp);
                                        }
                                    }
                                    else if (Serial_Number_TextBox.Text == nameTemp[4])
                                    {
                                        if (Serial_Number_TextBox.Text != "")
                                        {
                                            nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                            datagridview1.Rows.Add(nameTemp);
                                        }

                                    }


                                }
                            }
                        }


                    }
                    catch
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                datagridview1.ColumnCount = 6;

                for (int i = 0; i < datagridview1.ColumnCount; i++)
                {
                    datagridview1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                datagridview1.Columns[0].Name = "TEST DATE";
                datagridview1.Columns[1].Name = "Train Nuber";
                datagridview1.Columns[2].Name = "Car Number";
                datagridview1.Columns[3].Name = "Serial Number";
                datagridview1.Columns[4].Name = "Type of Test";
                datagridview1.Columns[5].Name = "Tester";
                //datagridview1.Columns[6].Name = "종류";
                //datagridview1.Columns[7].Name = "CTV 유무";

                ////MessageBox.Show(ex.Message);
                //MessageBox.Show("검색에 실패하였습니다. 다시 시도해 주세요.");
            }
        }

        private void New_TEST_BTN_Click(object sender, EventArgs e)
        {
            rt_form.test_check = false;
            rt_form.new_test_check = false;

            init_info = new init_Tester_info_Form();


            init_info.ShowDialog();

        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {

            if (datagridview1.SelectedCells != null)
            {
                rt_form.test_check = true;
                rt_form.new_test_check = false;

                rt_form.ShowDialog();
            }
            else
            { 
                
            
            }
        }

        int GetBit(byte x, int n)
        {
            return (x & (1 << n)) >> n;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            search_thread();
        }

        byte ReadBit(byte x, int n)
        {

            return (byte)(x & (1 << n));

        }

        public Select_TEST_Form()
        {
            InitializeComponent();

            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }
        public Select_TEST_Form (MainForm _form)
        {
            InitializeComponent();

            rt_form = _form;

            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            search_thread();


            rt_form = new MainForm(this);

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
    
    

   

//

        //----------------------------------------------------DATA Return -------------------------------------------------------------------
     

    }
}
