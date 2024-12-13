using System;
using System.Windows.Forms;
using Library;
using System.IO;
using System.Text;
using System.Threading;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Globalization;
using Power_Modbus_RTU_SAMPLE;
using static Library.DeviceTools;
using System.IO.Ports;

using KSS_Library;
using _2022_Test.NSTEK.device;
using lucidio;

namespace _2022_Test
{
    public partial class MainForm : Form
    {
        Stopwatch sw = new Stopwatch();

        init_Tester_info_Form init_info;
        Select_TEST_Form st_form;

        public bool new_test_check = false;

        public bool test_check = false;

        byte[] SerBuf = new byte[70];
        private Odt odt;


        ComboBox[] cbArray;
        ListView[] LV;
        Panel[] PanelArray;
        TextBox[] Serial_txt_Array;
        CheckBox[] Checkboxes_Array;
        Thread thRun;
        Thread thRun_p2p;

        DSP_LAN dc_power;
        PLCEnet plc;
        AND_AD310D loadcell;
        MT4YMOD timercount;

        CheckBox[] test_check_arr;

        Thread Auto_Test;
        ListView lv;
        Thread AutoBroad;
        string Timepath = "TimeSet.ini";

        bool On;
        bool Connect;
        bool FormMove = false;
        Point prePoint, curPoint;
        Point Pos;

        string _tester_name;
        string _pyunsung_name;
        string _serial_name;
        string _type_of_test;
        string _car_name;
        string _date_time;
        string _type;
        string _CTV;

        string _save_date;

        string f_name;


        public bool CTV = false;

        string path = Setting.Standard_folder;

        string[] pro_test_name = new string[50];
        string[] pro_test_nickname = new string[50];
        string[] pro_standard = new string[50];


        string[] GATE_test_name = new string[20];
        //string[] GATE_test_nickname = new string[100];
        string[] GATE_standard = new string[20];


        string Report_Route;
        string[][] TestItems;
        string[][] Teststandard;
        string[] min_value = new string[10];
        string[] max_value = new string[10];
        string[] standard_path;

        float[] AO_status = new float[10];

        int batch_count = 0;
        int time = 0;

        bool batch_flag = false;

        static Excel.Application ExcelApp = null;
        static Excel.Workbook WorkBook = null;
        static Excel.Worksheet WorkSheet = null;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(init_Tester_info_Form _form)
        {
            InitializeComponent();

            init_info = _form;

            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }

       

        public MainForm(Select_TEST_Form _form)
        {
            InitializeComponent();

            st_form = _form;

            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }


        private void EXIT_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (thRun != null && thRun.IsAlive)
                {
                    thRun.Abort();
                }
            }
            catch
            {

            }
            try
            {
                if (thRun_p2p != null && thRun_p2p.IsAlive)
                {
                    thRun_p2p.Abort();
                }
            }
            catch
            {

            }

            this.Close();
        }





        private void START_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                if (radioButton1.Checked) // 자동
                {
                    thRun = new Thread(TestExecute_Test);
                    thRun.Start();
                }
                else // 수동
                {

                }
            }
            catch
            { 
            }
          
        }
        private void TestExecute_Test()
        {
            CheckForIllegalCrossThreadCalls = false;

            try
            {
                BTN_Enable(1); // 0이 아닌 숫자는 시험 중

                // 서보모터 시험 위치 


                for (int i = 0; i < 6; i++)
                {
                    if (i == 0 && checkBox4.Checked) //압상력
                    {
                        TabControl1.SelectedIndex = 0;

                        for (int j = 0; j < 19; j++)
                        {
                            listView1.Items[0].SubItems[2 + j].Text = (8.5).ToString();
                            listView1.Items[1].SubItems[2 + j].Text = (4.0).ToString();
                            listView1.Items[2].SubItems[2 + j].Text = 25.ToString();
                        }
                    
                    }
                    if (i == 1 && listView3.Items[i-1].Checked) //상승시간
                    {
                        TabControl1.SelectedIndex = 1;

                        listView3.Items[i - 1].SubItems[3].Text = "x" + "ms";
                        listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;

                    }
                    if (i == 2 && listView3.Items[i - 1].Checked) //하강시간
                    {
                        TabControl1.SelectedIndex = 1;
                        listView3.Items[i - 1].SubItems[3].Text = "x" + "ms";
                        listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;


                    }
                    if (i == 3 && listView3.Items[i - 1].Checked) //최저동작 전압
                    {
                        TabControl1.SelectedIndex = 1;
                        listView3.Items[i - 1].SubItems[3].Text = "x" + "V";
                        listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;


                    }
                    if (i == 4 && listView3.Items[i - 1].Checked) //최저하강 공압
                    {
                        TabControl1.SelectedIndex = 1;
                        listView3.Items[i - 1].SubItems[3].Text = "x" + "bar";
                        listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;

                    }
                    if (i == 5 && listView3.Items[i - 1].Checked) //공기누설시험
                    {
                        TabControl1.SelectedIndex = 1;

                        listView3.Items[i - 1].SubItems[3].Text = "x" + "bar";
                        listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;

                    }
                }


                //for (int i = 0; i < LV.Length; i++)
                //{
                //    for (int j = 0; j < LV[i].Items.Count; j++)
                //    {
                       

                //    }
                //}

                MessageBox.Show("TEST Complete");
            }
            catch
            {
                #region LV Color Reset
                for (int i = 0; i < LV.Length; i++)
                {
                    for (int j = 0; j < LV[i].Items.Count; j++)
                    {
                        if (j % 2 == 0)
                        {
                            LV[i].Items[j].SubItems[1].BackColor = Color.AliceBlue;
                            LV[i].Items[j].SubItems[3].BackColor = Color.AliceBlue;
                            LV[i].Items[j].SubItems[2].BackColor = Color.Linen;
                            LV[i].Items[j].SubItems[4].BackColor = Color.Linen;
                        }
                        else
                        {
                            LV[i].Items[j].SubItems[1].BackColor = Color.LightCyan;
                            LV[i].Items[j].SubItems[3].BackColor = Color.LightCyan;
                            LV[i].Items[j].SubItems[2].BackColor = Color.LightYellow;
                            LV[i].Items[j].SubItems[4].BackColor = Color.LightYellow;
                        }
                    }
                }
                #endregion

                MessageBox.Show("시험이 비정상적으로 종료되었습니다.");

                BTN_Enable(0); // 0은 평상 시
            }
            BTN_Enable(0); // 0은 평상 시
        }
        private void STOP_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                if (radioButton1.Checked) // 자동
                {
                    if (thRun != null && thRun.IsAlive)
                    {
                        thRun.Abort();
                    }
                }
                else // 수동
                {
                    if (TabControl1.SelectedIndex == 0)
                    {

                    
                      
                    }

                    else if (TabControl1.SelectedIndex == 1)
                    {



                    }

                }
            }
            catch
            {

            }



            try
            {
                if (thRun != null && thRun.IsAlive)
                {
                    thRun.Abort();
                }
            }
            catch
            {

            }
        }
        private void BTN_Enable(int Number)
        {
            if (Number == 0)
            {
                START_BTN.Enabled = true;
                STOP_BTN.Enabled = false;
                SAVE_BTN.Enabled = true;
                RESET_BTN.Enabled = true;
                EXIT_BTN.Enabled = true;
            } // 평상 시
            else
            {
                START_BTN.Enabled = false;
                STOP_BTN.Enabled = true;
                SAVE_BTN.Enabled = false;
                RESET_BTN.Enabled = false;
                EXIT_BTN.Enabled = false;
            } // 시험 중
        }

        #region 시험항목 리스트 관련 사항
        public class ListViewEx : ListView
        {
            [DllImport("uxtheme", CharSet = CharSet.Auto)]
            static extern Boolean SetWindowTheme(IntPtr hWindow, String subAppName, String subIDList);

            public ListViewEx()
            {
                SetStyle((ControlStyles)0x22010, true);
            }

            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);

                // 핸들이 생성된 후에 테마를 적용한다.
                SetWindowTheme(Handle, "explorer", null);
            }
        }
        #endregion
        private void MainForm_Load(object sender, EventArgs e)
        {
            BTN_Enable(0); // 평상 시

         
            TestItems = new string[][] { Setting.ListName0, Setting.ListName1, Setting.ListName2, Setting.ListName3, Setting.ListName4, Setting.ListName5, Setting.ListName6 };
            Teststandard = new string[][] { Setting.ListMethod0, Setting.ListMethod1, Setting.ListMethod2, Setting.ListMethod3, Setting.ListMethod4, Setting.ListMethod5, Setting.ListMethod6 };

           
            test_check_arr = new CheckBox[] { pau_check, cob_check, sob_check, Emergency_Check, amp_Check };

            Test_Name_Label.Text = Setting.Name;

            try
            {
                StreamReader timeset = new StreamReader(Timepath, Encoding.Default);
                while (!timeset.EndOfStream)
                {
                    string timepath = timeset.ReadLine();
                    string[] path = timepath.Split(',');
                    time = int.Parse(path[0]);
                }
                timeset.Close();
            }
            catch
            {


            }
            try
            {
                StreamReader sr = new StreamReader(@"reportpath.ini");
                string temp = sr.ReadToEnd();
                sr.Close();
                string[] Port = temp.Split('!');
                Report_Route = Port[0];

                if (!(Directory.Exists(Report_Route)))
                    MessageBox.Show("성적서 저장경로를 설정하여 주십시오.");

            }
            catch
            {

            }

            try
            {
                StreamReader sr = new StreamReader(@"data\setup.ini");
                string temp = sr.ReadToEnd();
                sr.Close();
                string[] Port = temp.Split('!');
                for (int i = 0; i < Setting.Equipment.Length; i++)
                {
                    if (Setting.Equipment[i] == "power")
                    {
                        dc_power =new DSP_LAN(Setting.dsp_ip);
                        dc_power.Portopen();
                    }
              
                }
            }
            catch
            {

            }

            try
            {
                if (test_check == true)
                {
                    //StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "data" + "\\" + "data.ini");
                    //string[] fd = sr.ReadToEnd().Split('!');
                    //sr.Close();

                    _save_date = DateTime.Parse(st_form.datagridview1.SelectedRows[0].Cells[0].Value.ToString()).ToString("yyyy-MM-dd");
                    _date_time = DateTime.Parse(st_form.datagridview1.SelectedRows[0].Cells[0].Value.ToString()).ToString("yyyyMMddHHmm");
                    _pyunsung_name = st_form.datagridview1.SelectedRows[0].Cells[1].Value.ToString();
                    _car_name = st_form.datagridview1.SelectedRows[0].Cells[2].Value.ToString();
                    _serial_name = st_form.datagridview1.SelectedRows[0].Cells[3].Value.ToString();
                    _type_of_test = st_form.datagridview1.SelectedRows[0].Cells[4].Value.ToString();
                    _tester_name = st_form.datagridview1.SelectedRows[0].Cells[5].Value.ToString();

                    f_name = _date_time + "_" + _pyunsung_name + "_" + _car_name + "_" + _serial_name + "_" + _type_of_test + "_" + _tester_name + "_.dat";


                }
                else
                {

                    _save_date = DateTime.Parse(init_info.tb_Date.Value.ToString()).ToString("yyyy-MM-dd");
                    _date_time = DateTime.Parse(init_info.tb_Date.Value.ToString()).ToString("yyyyMMddHHmm");
                    _pyunsung_name = init_info.tb_pyunsung.Text.ToString(); ;
                    _car_name = init_info.tb_carnum.Text.ToString();
                    _serial_name = init_info.tb_serial.Text.ToString();
                    _type_of_test = init_info.tb_testlist.Text.ToString();
                    _tester_name = init_info.tb_tester.Text.ToString();

                    // _type = "Pantagraph";

                    f_name = _date_time + "_" + _pyunsung_name + "_" + _car_name + "_" + _serial_name + "_" + _type_of_test + "_" + _tester_name + "_.dat";
                }

            }
            catch
            {

            }

            //try
            //{
            //    string Filename = "";
            //    Filename = path + "\\" + "SIV보호동작기준" + ".csv";

            //    if (File.Exists(Filename))
            //    {
            //        int line = 0;
            //        try
            //        {
            //            StreamReader sd = new StreamReader(Filename, Encoding.Default);
            //            while (!sd.EndOfStream)
            //            {
            //                string data = sd.ReadLine();
            //                string[] data2 = data.Split(',');
            //                pro_test_name[line] = data2[0];
            //                pro_test_nickname[line] = data2[1];
            //                pro_standard[line] = data2[2];
            //                line++;
            //            }
            //            sd.Dispose();
            //            sd.Close();
            //        }
            //        catch
            //        {

            //        }
            //    }
            //}
            //catch
            //{


            //}

            //try
            //{
            //    string Filename = "";
            //    Filename = path + "\\" + "SIVGATE출력기준" + ".csv";

            //    if (File.Exists(Filename))
            //    {
            //        int line = 0;
            //        try
            //        {
            //            StreamReader sd = new StreamReader(Filename, Encoding.Default);
            //            while (!sd.EndOfStream)
            //            {
            //                string data = sd.ReadLine();
            //                string[] data2 = data.Split(',');
            //                GATE_test_name[line] = data2[0];
            //                //GATE_test_nickname[line] = data2[1];
            //                GATE_standard[line] = data2[1];
            //                line++;
            //            }
            //            sd.Dispose();
            //            sd.Close();
            //        }
            //        catch
            //        {

            //        }
            //    }
            //}
            //catch
            //{


            //}
            #region 리스트뷰 정렬
            for (int i = 0; i < Setting.ListName.Length; i++)
            {
                listView3.Items.Add("");
                listView3.Items[i].SubItems.Add(Setting.ListName[i]);
                listView3.Items[i].SubItems.Add(Setting.ListMethod0[i]);
                listView3.Items[i].SubItems.Add("");
                listView3.Items[i].SubItems.Add("");
                listView3.Items[i].SubItems.Add("");
                listView3.Items[i].SubItems.Add("");
                listView3.Items[i].SubItems.Add("");
                listView3.Items[i].UseItemStyleForSubItems = false;


                //if (i % 2 == 0)
                //    ksS_ListView1.Items[i].BackColor = off;
                //else
                //    ksS_ListView1.Items[i].BackColor = back;
                if (i % 2 == 0)
                {
                    listView3.Items[i].SubItems[1].BackColor = Color.AliceBlue;
                    listView3.Items[i].SubItems[2].BackColor = Color.Linen;
                    listView3.Items[i].SubItems[3].BackColor = Color.AliceBlue;
                    listView3.Items[i].SubItems[4].BackColor = Color.Linen;
                    listView3.Items[i].SubItems[5].BackColor = Color.AliceBlue;
                    listView3.Items[i].SubItems[6].BackColor = Color.Linen;
                    listView3.Items[i].SubItems[7].BackColor = Color.AliceBlue;
                }
                else
                {
                    listView3.Items[i].SubItems[1].BackColor = Color.LightCyan;
                    listView3.Items[i].SubItems[3].BackColor = Color.LightCyan;
                    listView3.Items[i].SubItems[2].BackColor = Color.White;
                    listView3.Items[i].SubItems[4].BackColor = Color.White;
                    listView3.Items[i].SubItems[5].BackColor = Color.LightCyan;
                    listView3.Items[i].SubItems[6].BackColor = Color.White;
                    listView3.Items[i].SubItems[7].BackColor = Color.LightCyan;
                }
                listView3.Items[i].Checked = true;

            }

            for (int i = 0; i < Setting.ListName3.Length; i++)
            {
                listView1.Items.Add(""); //0
                listView1.Items[i].SubItems.Add(Setting.ListName3[i]);//1
                listView1.Items[i].SubItems.Add("");//2
                listView1.Items[i].SubItems.Add("");//3
                listView1.Items[i].SubItems.Add("");//4
                listView1.Items[i].SubItems.Add("");//5
                listView1.Items[i].SubItems.Add("");//6
                listView1.Items[i].SubItems.Add("");//7
                listView1.Items[i].SubItems.Add("");//8
                listView1.Items[i].SubItems.Add("");//9
                listView1.Items[i].SubItems.Add("");//10
                listView1.Items[i].SubItems.Add("");//11
                listView1.Items[i].SubItems.Add("");//12
                listView1.Items[i].SubItems.Add("");//13
                listView1.Items[i].SubItems.Add("");//14
                listView1.Items[i].SubItems.Add("");//15
                listView1.Items[i].SubItems.Add("");//16
                listView1.Items[i].SubItems.Add("");//17
                listView1.Items[i].SubItems.Add("");//18
                listView1.Items[i].SubItems.Add("");//19
                listView1.Items[i].SubItems.Add("");//20
                listView1.Items[i].SubItems.Add("");//21
                listView1.Items[i].SubItems.Add("");//22
                listView1.Items[i].UseItemStyleForSubItems = false;


                //if (i % 2 == 0)
                //    ksS_ListView1.Items[i].BackColor = off;
                //else
                //    ksS_ListView1.Items[i].BackColor = back;
                if (i % 2 == 0)
                {
                   // listView1.Items[i].SubItems[1].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[2].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[3].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[4].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[5].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[6].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[7].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[8].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[9].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[10].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[11].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[12].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[13].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[14].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[15].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[16].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[17].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[18].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[19].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[20].BackColor = Color.AliceBlue;
                    listView1.Items[i].SubItems[21].BackColor = Color.Linen;
                    listView1.Items[i].SubItems[22].BackColor = Color.AliceBlue;
                
                }
                else
                {
                   // listView1.Items[i].SubItems[1].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[3].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[2].BackColor = Color.White;
                    listView1.Items[i].SubItems[4].BackColor = Color.White;
                    listView1.Items[i].SubItems[5].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[6].BackColor = Color.White;
                    listView1.Items[i].SubItems[7].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[8].BackColor = Color.White;
                    listView1.Items[i].SubItems[9].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[10].BackColor = Color.White;
                    listView1.Items[i].SubItems[11].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[12].BackColor = Color.White;
                    listView1.Items[i].SubItems[13].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[14].BackColor = Color.White;
                    listView1.Items[i].SubItems[15].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[16].BackColor = Color.White;
                    listView1.Items[i].SubItems[17].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[18].BackColor = Color.White;
                    listView1.Items[i].SubItems[19].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[20].BackColor = Color.White;
                    listView1.Items[i].SubItems[21].BackColor = Color.LightCyan;
                    listView1.Items[i].SubItems[22].BackColor = Color.White;
                }
               // listView5.Items[i].Checked = true;

            }


          
            #endregion

            for (int i = TabControl1.TabCount - 1; i >= 0; i--)
            {
                TabControl1.SelectedIndex = i;
            }




           chart1.Series["Force"].Points.AddXY(0, 0);


            try
            {
                if (test_check == true)
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "reportpath.ini");
                    string[] fd = sr.ReadToEnd().Split('!');
                    sr.Close();

                    LoadTestedData(fd[0] + @"\" + f_name);
                }

            }
            catch
            {



            }




            richbox1.AppendText("Load Test View.\n");
        }

        public void LoadTestedData(string path)
        {
            string fname = path;
            if (File.Exists(fname))
            {
                CultureInfo provider = CultureInfo.InvariantCulture;

                string[] temp = fname.Split('_');
                string[] date_num = temp[0].Split('\\');
                string date = date_num[2];
                odt = new Odt(fname);

                //tb_tester.Text = odt.Inputs["Tester"];
                //tb_pyunsung.Text = odt.Inputs["Organization"];
                //tb_carnum.Text = odt.Inputs["Car"];
                //tb_serial.Text = odt.Inputs["Serial"];
                //textBox1.Text = odt.Inputs["etc2"];


                int length = odt.Inputs.Count;

                try
                {
                    for (int i = 0; i < 20; i++)
                    {
                        listView1.Items[0].SubItems[2 + i].Text = odt.Inputs["udata_" + i.ToString()];
                        listView1.Items[1].SubItems[2 + i].Text = odt.Inputs["ldata_" + i.ToString()];
                        listView1.Items[2].SubItems[2 + i].Text = odt.Inputs["fdata_" + i.ToString()];

                    }
                    for (int i = 0; i < 3; i++) //보호동작
                    {
                        listView1.Items[i].SubItems[22].Text = odt.Inputs["result_" + i.ToString()];
                        listView1.Items[i].SubItems[22].Text = odt.Inputs["result_" + i.ToString() + "_1"];

                    }

                    for (int i = 0; i < 5; i++)
                    {
                        listView3.Items[i].SubItems[3].Text = odt.Inputs["op_data_" + i.ToString()];
                        listView3.Items[i].SubItems[4].Text = odt.Inputs["op_result_" + i.ToString()];
                    }


                }
                catch
                {

                }

            }

            else // 절연 관련
            {

            }

            //test_info.dateTimePicker1.Value = DateTime.ParseExact(date, "yyyyMMddHHmm", provider);
            //test_info.dateTimePicker2.Value = DateTime.ParseExact(date, "yyyyMMddHHmm", provider);

            if (File.Exists(@"D:\Chart\" + _date_time + "_panto_chart.jpg")) //기동시퀀스
            {
                // panel9.Visible = false;
                // panel_pwseq.Visible = false;
                // chart_Powering.Visible = false;
                // panel_seq_oper.BackgroundImage = Image.FromFile(@"D:\Chart\" + date + "_pw_seq_chart.jpg");
            }
            else
            {
                //panel9.Visible = true;
                //panel_pwseq.Visible = true;
                //chart_Powering.Visible = true;
            }
        }

    

        public void set_voltage(int setting_volt)  // SHV300R voltage 설정
        {
            float set_voltage, result;

            set_voltage = setting_volt;

            result = (set_voltage / 60000.0f) * 1000;

            string volt = result.ToString("F0");


        }
        public void set_current(int setting_current) //SHV 300R current 설정
        {
            float set_current, result;

            set_current = setting_current;

            result = (set_current / 5.0f);

            string current = result.ToString("F0");

        }

        delegate void ChartDelegate(System.Windows.Forms.DataVisualization.Charting.Chart ctrl, string series, double x, double y);

        public void SetChart(System.Windows.Forms.DataVisualization.Charting.Chart ctrl, string series, double x, double y)
        {


            if (ctrl.InvokeRequired)
            {
                ChartDelegate t = new ChartDelegate(SetChart);
                try
                {
                    ctrl.Invoke(t, new object[] { ctrl, series, x, y });
                }
                catch
                {

                }
            }
            else
            {
                ctrl.Series[series].Points.AddXY(x, y);
            }
        }

        private static void ReleaseExcelObject(Object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void RESET_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < LV.Length; i++)
            {
                for (int j = 0; j < LV[i].Items.Count; j++)
                {
                    LV[i].Items[j].SubItems[3].Text = "";
                    LV[i].Items[j].SubItems[4].Text = "";
                }
            }
        }

        private void CheckBox01_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox cb = (CheckBox)sender;

            //if (LV[int.Parse(cb.Tag.ToString())].Items.Count > 0)
            //{
            //    for (int i = 0; i < LV[int.Parse(cb.Tag.ToString())].Items.Count; i++)
            //    {
            //        if (Checkboxes_Array[int.Parse(cb.Tag.ToString())].Checked)
            //            LV[int.Parse(cb.Tag.ToString())].Items[i].Checked = true;
            //        else
            //            LV[int.Parse(cb.Tag.ToString())].Items[i].Checked = false;
            //    }
            //}
        }

        private void Serial02_TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            tb.BackColor = SystemColors.Info;
        }

        private void Serial02_TextBox_MouseLeave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            tb.BackColor = SystemColors.Window;
        }
    

        private void Report_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            this.Opacity = 0.8;

            ReportForm RF = new ReportForm(this);
            RF.ShowDialog();
            RF.Dispose();
        }
        private static void KillProcessByName(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);
            if (processList.Length > 0)
            {
                foreach (Process p in processList)
                {
                    p.Kill();
                }
            }
            else
            {
            }
        }

        private void all_check_CheckedChanged(object sender, EventArgs e)
        {
            if (all_check.Checked)
            {
                for (int i = 0; i < test_check_arr.Length; i++)
                    test_check_arr[i].Checked = true;
            }
            else
            {
                for (int i = 0; i < test_check_arr.Length; i++)
                    test_check_arr[i].Checked = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (AutoBroad.IsAlive == true)
                    AutoBroad.Abort();
            }
            catch
            {

            }
        }

        private void button_st_Click(object sender, EventArgs e)
        {
            
        }

        private void SAVE_BTN_Click(object sender, EventArgs e)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

        
            StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "reportpath.ini");
            string[] fd = sr.ReadToEnd().Split('!');
            sr.Close();

            StreamReader cr = new StreamReader(Application.StartupPath + "\\" + "datapath.ini");
            string[] cd = cr.ReadToEnd().Split('!');
            cr.Close();

            string date;
            for (int j = 0; j < 1; j++)
            {

                string panjung = "OK";

                //for (int i = 0; i < listView1.Items.Count; i++)
                //{
                //    if (listView1.Items[i].SubItems[4].Text == "NG")
                //        panjung = "NG";
                //}

                string ddd = "";
                //odt = new Odt(Application.StartupPath + "\\" + "CI_Korail_CTV-O.dat");

                odt = new Odt(Application.StartupPath + "\\" + "panto5.odt");


                Bitmap bmp = new Bitmap(this.chart1.Width, this.chart1.Height);
                this.chart1.DrawToBitmap(bmp, new Rectangle(0, 0, this.chart1.Width, this.chart1.Height));
                bmp.Save(@"D:\Chart\" + _date_time + "_panto_chart.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                date = _date_time;

                //string FileName = date + "_" + test_info.tb_tester.Text + "_" + test_info.tb_pyunsung.Text + "_" + test_info.tb_carnum.Text + "_" + test_info.tb_serial.Text + "_" + ".dat";

                string Filename = "";

                Thread.Sleep(500);

                


                Thread.Sleep(500);
                for (int i = 0; i < 2; i++)
                {
                    odt.Inputs["Date" + i.ToString()] = _save_date;
                    odt.Inputs["Organization" + i.ToString()] = _pyunsung_name;
                    odt.Inputs["Car" + i.ToString()] = _car_name;
                    odt.Inputs["Serial" + i.ToString()] = _serial_name;
                    odt.Inputs["etc2" + i.ToString()] = _type_of_test;
                    odt.Inputs["Tester" + i.ToString()] = _tester_name;
                   // odt.Inputs["Menu" + i.ToString()] = _type;

                }

                try
                {

                    for (int i = 0; i <20; i++)
                    {
                        odt.Inputs["udata_" + i.ToString()] =listView1.Items[0].SubItems[2+i].Text;
                        odt.Inputs["ldata_" + i .ToString()] = listView1.Items[1].SubItems[2 + i].Text;
                        odt.Inputs["fdata_" + i .ToString()] = listView1.Items[2].SubItems[2 + i].Text;
                    }

                    for (int i = 0; i <3; i++)
                    {
                        odt.Inputs["result_" + i.ToString()] = listView1.Items[ i].SubItems[22].Text;
                        odt.Inputs["result_" + i.ToString() + "_1"] = listView1.Items[ i].SubItems[22].Text;
                    }



                    for (int i = 0; i < 5; i++)
                    {
                        odt.Inputs["op_data_" + i.ToString()] = listView3.Items[i].SubItems[3].Text;
                        odt.Inputs["op_result_" + i.ToString()] = listView3.Items[i].SubItems[4].Text;
                    }


                }

                catch
                {


                }


                //odt.Save3(fd[0] + @"\" + f_name, fd[1] + @"\pw_seq_chart.jpg", fd[1] + @"\light_fault_chart.jpg", fd[1] + @"\heavy_fault_chart.jpg", fd[1] + @"\null_file.png");

                try
                {
                    if (File.Exists(fd[0] + @"\" + f_name))
                    {
                        File.Delete(fd[0] + @"\" + f_name);

                        odt.Save3(fd[0] + @"\" + f_name, cd[0] + @"\" + date + "_panto_chart.jpg", cd[0] + @"\null_file.png", date);
                    }
                    else
                    {
                        odt.Save3(fd[0] + @"\" + f_name, cd[0] + @"\" + date + "_panto_chart.jpg", cd[0] + @"\null_file.png", date);
                    }
                    //}
                    MessageBox.Show("저장이 완료되었습니다.");
                }
                catch
                {
                    MessageBox.Show("저장에 실패하였습니다.");
                }

            }
        }

        private void Emergency_Check_CheckedChanged(object sender, EventArgs e)
        {
         

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button7_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //set_button_off();

            //Stop_BTN_PT_Volt.Enabled = true;

       

        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
  

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
          
        }

    }




}
