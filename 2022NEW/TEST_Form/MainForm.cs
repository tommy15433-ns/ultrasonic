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

        private Thread thRun_PT_Volt; //전원검출기
        private Thread thRun_Protection;//보호동작
        private Thread thRun_main_circuit;//주회로통전
        private Thread thRun_Gate_Out;//게이트출력
        private Thread thRun_Gate_in;// 게이트 전원출력
        Thread thRun_Sequence_oper;// 기동시퀀스
        Thread thRun_Sequence_notch;// 기동시퀀스
        private Thread thRun_overvolt;//과전압억제       



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

            //try
            //{
            //    // Connect to the APx500 software.
            //    APx = new APx500();
            //    // Show the APx500 software's window.
            //    APx.Visible = true;
            //    APx.Minimize();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error loading APx", MessageBoxButtons.OK);
            //    Close();
            //}
            //// Put this program's window on top of the others.
            //Focus();
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
                    if (TabControl1.SelectedIndex == 0)
                    {
                     
                     
                    }

                    else if (TabControl1.SelectedIndex == 1)
                    {



                    }

                    else if (TabControl1.SelectedIndex == 4) // 경부하 시험
                    {

                        thRun_main_circuit = new Thread(Test_MainCircuit);
                        thRun_main_circuit.Start();

                    }

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

                for (int i = 0; i < LV.Length; i++)
                {
                    for (int j = 0; j < LV[i].Items.Count; j++)
                    {


                    }
                }

                MessageBox.Show("시험완료");
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
          
            //try
            //{
            //    for (int i = 0; i < standard_path.Length; i++)
            //    {
            //        if (File.Exists(standard_path[i])) // 기준값 파일 확인
            //        {
            //            int line = 0;
            //            try
            //            {
            //                StreamReader sd = new StreamReader(standard_path[i], Encoding.Default);
            //                while (!sd.EndOfStream)
            //                {
            //                    string data = sd.ReadLine();
            //                    string[] data2 = data.Split(',');
            //                    min_value[line] = data2[1];
            //                    max_value[line] = data2[2];
            //                    line++;
            //                }
            //                sd.Dispose();
            //                sd.Close();
            //            }
            //            catch
            //            {

            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show(standard_path[i] + " 파일이 존재하지 않습니다. 파일을 생성해 주세요.");
            //        }
            //    }
            //    #region 시험기준 설정
            //    standard[0] = "standard1";
            //    standard[1] = "standard2";
            //    standard[2] = "standard3";
            //    standard[3] = "standard4";
            //    standard[4] = "standard5";
            //    standard[5] = "standard6";
            //    standard[6] = "standard7";
            //    standard[7] = "standard8";
            //    #endregion
            //}
            //catch
            //{

            //}

            try
            {
                _save_date = DateTime.Parse(init_info.tb_Date.Value.ToString()).ToString("yyyy-MM-dd");
                _date_time = DateTime.Parse(init_info.tb_Date.Value.ToString()).ToString("yyyyMMddHHmm");
                _pyunsung_name = init_info.tb_pyunsung.Text.ToString(); ;
                _car_name = init_info.tb_carnum.Text.ToString();
                _serial_name = init_info.tb_serial.Text.ToString();
                _type_of_test = init_info.tb_testlist.Text.ToString();
                _tester_name = init_info.tb_tester.Text.ToString();

                _type = "Pantagraph";

               

                //if (init_info.radioButton4.Checked)
                //{
                //    _CTV = init_info.radioButton4.Text;
                //}
                //else
                //{
                //    _CTV = init_info.radioButton3.Text;
                //}

                //f_name = _date_time + "_" + _pyunsung_name + "_" + _car_name + "_" + _serial_name + "_" + _type_of_test + "_" + _tester_name + "_" + _type + "_" + _CTV + "_.dat";
                f_name = _date_time + "_" + _pyunsung_name + "_" + _car_name + "_" + _serial_name + "_" + _type_of_test + "_" + _tester_name + "_" + _type + "_.dat";
            }
            catch
            {

            }

            try
            {
                string Filename = "";
                Filename = path + "\\" + "SIV보호동작기준" + ".csv";

                if (File.Exists(Filename))
                {
                    int line = 0;
                    try
                    {
                        StreamReader sd = new StreamReader(Filename, Encoding.Default);
                        while (!sd.EndOfStream)
                        {
                            string data = sd.ReadLine();
                            string[] data2 = data.Split(',');
                            pro_test_name[line] = data2[0];
                            pro_test_nickname[line] = data2[1];
                            pro_standard[line] = data2[2];
                            line++;
                        }
                        sd.Dispose();
                        sd.Close();
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {


            }

            try
            {
                string Filename = "";
                Filename = path + "\\" + "SIVGATE출력기준" + ".csv";

                if (File.Exists(Filename))
                {
                    int line = 0;
                    try
                    {
                        StreamReader sd = new StreamReader(Filename, Encoding.Default);
                        while (!sd.EndOfStream)
                        {
                            string data = sd.ReadLine();
                            string[] data2 = data.Split(',');
                            GATE_test_name[line] = data2[0];
                            //GATE_test_nickname[line] = data2[1];
                            GATE_standard[line] = data2[1];
                            line++;
                        }
                        sd.Dispose();
                        sd.Close();
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {


            }
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
                listView5.Items.Add("");
                listView5.Items[i].SubItems.Add(Setting.ListName3[i]);
                listView5.Items[i].SubItems.Add(Setting.ListMethod3[i]);
                listView5.Items[i].SubItems.Add("");
                listView5.Items[i].SubItems.Add("");
                listView5.Items[i].SubItems.Add("");
                listView5.Items[i].SubItems.Add("");
                listView5.Items[i].SubItems.Add("");
                listView5.Items[i].SubItems.Add("");
                listView5.Items[i].SubItems.Add("");
                listView5.Items[i].SubItems.Add("");
                listView5.Items[i].UseItemStyleForSubItems = false;


                //if (i % 2 == 0)
                //    ksS_ListView1.Items[i].BackColor = off;
                //else
                //    ksS_ListView1.Items[i].BackColor = back;
                if (i % 2 == 0)
                {
                    listView5.Items[i].SubItems[1].BackColor = Color.AliceBlue;
                    listView5.Items[i].SubItems[2].BackColor = Color.Linen;
                    listView5.Items[i].SubItems[3].BackColor = Color.AliceBlue;
                    listView5.Items[i].SubItems[4].BackColor = Color.Linen;
                    listView5.Items[i].SubItems[5].BackColor = Color.AliceBlue;
                    listView5.Items[i].SubItems[6].BackColor = Color.Linen;
                    listView5.Items[i].SubItems[7].BackColor = Color.AliceBlue;
                    listView5.Items[i].SubItems[8].BackColor = Color.Linen;
                    listView5.Items[i].SubItems[9].BackColor = Color.AliceBlue;
                    listView5.Items[i].SubItems[10].BackColor = Color.Linen;
                }
                else
                {
                    listView5.Items[i].SubItems[1].BackColor = Color.LightCyan;
                    listView5.Items[i].SubItems[3].BackColor = Color.LightCyan;
                    listView5.Items[i].SubItems[2].BackColor = Color.White;
                    listView5.Items[i].SubItems[4].BackColor = Color.White;
                    listView5.Items[i].SubItems[5].BackColor = Color.LightCyan;
                    listView5.Items[i].SubItems[6].BackColor = Color.White;
                    listView5.Items[i].SubItems[7].BackColor = Color.LightCyan;
                    listView5.Items[i].SubItems[8].BackColor = Color.White;
                    listView5.Items[i].SubItems[9].BackColor = Color.LightCyan;
                    listView5.Items[i].SubItems[10].BackColor = Color.White;
                   
                }
                listView5.Items[i].Checked = true;

            }

            #endregion

            for (int i = TabControl1.TabCount - 1; i >= 0; i--)
            {
                TabControl1.SelectedIndex = i;
            }


      

           

           chart1.Series["Force"].Points.AddXY(0, 11);
       




            richbox1.AppendText("Load Test View.\n");
        }

        private void TestExecute_P2P()
        {

         


        }

        private void Board_AllOUT()
        {

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

        public void operating_init()
        {

        }


        public void Operating_VVVF()
        {
        
        
        }

        private void Test_Gate_OUT()
        {
            CheckForIllegalCrossThreadCalls = false;
            Control.CheckForIllegalCrossThreadCalls = false;

            #region  GATE 출력파형시험
            ////Gate 출력파형시험

          
              
            //    richbox1.AppendText(" Gate  출력파형 시험을 시작합니다.\r\n");

            //    MessageBox.Show("GATE 출력파형을 위한 케이블을 연결하여 주세요");

            //    for (int i = 0; i < Setting.ListName3.Length; i++)
            //    {
            //        listView4.Items[i].SubItems[1].BackColor = Color.FromArgb(56, 131, 188);
            //        listView4.Items[i].SubItems[2].BackColor = Color.FromArgb(56, 131, 188);
            //        listView4.Items[i].SubItems[3].BackColor = Color.FromArgb(56, 131, 188);
            //        listView4.Items[i].SubItems[4].BackColor = Color.FromArgb(56, 131, 188);
            //        listView4.Items[i].SubItems[5].BackColor = Color.FromArgb(56, 131, 188);


            //        if (listView4.Items[i].Checked == true && i == 0)
            //        {
            //            scope.Write("CHANnel2:COUPling DC");
            //            string gate_U1;
            //            float value_U1;
            //            Operating_VVVF();

                      
            //            relay_di.OUT(7, 1, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT(5, 9, true);
            //            Thread.Sleep(200);
            //            scope.Write("CHANnel2:SCALe 10");
            //            Thread.Sleep(200);
            //            scope.Write(":TIM:SCAL " + (0.01).ToString());
            //            Thread.Sleep(200);
            //            scope.Write(":RUN");
            //            Thread.Sleep(200);
            //            scope.Write("CHANnel1:DISPlay OFF");
            //            scope.Write("CHANnel2:DISPlay ON");
            //            scope.Write("CHANnel3:DISPlay OFF");
            //            scope.Write("CHANnel4:DISPlay OFF");
            //            Thread.Sleep(200);

            //            scope.Write(":STOP");

            //            Thread.Sleep(200);
            //            gate_U1 = scope.Query(":MEASure:VTOP? CHAN2");
            //            value_U1 = float.Parse(gate_U1);

            //            listView4.Items[i].SubItems[3].Text = value_U1.ToString("F2") + "V";
            //            Thread.Sleep(200);

            //            if (value_U1 >= 13.5f && value_U1 <= 16.5f)
            //            {
            //                listView4.Items[i].SubItems[4].Text = Constant.GOOD;

            //            }
            //            else
            //                listView4.Items[i].SubItems[4].Text = Constant.NG;

            //            Thread.Sleep(200);
            //            if (listView4.Items[1].Checked == false)
            //            {
            //            dc_power1.SET_VOLT(100);
            //            dc_power1.ONOFF("OFF");

            //            Board_AllOUT();
                          
            //            }

            //            relay_di.OUT(5, 9, false);
            //            Thread.Sleep(300);
            //            relay_di.OUT(7, 1, false);
            //        }


            //        if (listView4.Items[i].Checked == true && i == 1)
            //        {
            //            scope.Write("CHANnel3:COUPling DC");
            //            string gate_U2;
            //            float value_U2;
            //            if (listView4.Items[0].Checked == false)
            //            {
            //                Operating_VVVF();
                           
            //            }
            //            relay_di.OUT(7, 2, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 2, true);

            //            Thread.Sleep(200);

            //            scope.Write(":RUN");
            //            Thread.Sleep(200);
            //            scope.Write("CHANnel3:SCALe 10");
            //            Thread.Sleep(200);
            //            scope.Write(":TIM:SCAL " + (0.01).ToString());
            //            Thread.Sleep(200);
            //            scope.Write("CHANnel1:DISPlay OFF");
            //            scope.Write("CHANnel2:DISPlay OFF");
            //            scope.Write("CHANnel3:DISPlay ON");
            //            scope.Write("CHANnel4:DISPlay OFF");
            //            Thread.Sleep(200);
            //            scope.Write(":STOP");
            //            Thread.Sleep(200);

            //            gate_U2 = scope.Query(":MEASure:VTOP? CHAN3");

            //            value_U2 = float.Parse(gate_U2);

            //            listView4.Items[i].SubItems[3].Text = value_U2.ToString("F2") + "V";
            //            Thread.Sleep(200);

            //            if (value_U2 >= 13.5f && value_U2 <= 16.5f)
            //            {
            //                listView4.Items[i].SubItems[4].Text = Constant.GOOD;

            //            }
            //            else
            //                listView4.Items[i].SubItems[4].Text = Constant.NG;
            //            Thread.Sleep(200);
            //            if (listView4.Items[2].Checked == false)
            //            {
            //            dc_power1.SET_VOLT(100);
            //            dc_power1.ONOFF("OFF");
            //            Board_AllOUT();
                          
            //            }
            //            relay_di.OUT(6, 2, false);
            //            Thread.Sleep(300);
            //            relay_di.OUT(7, 2, false);
            //        }


            //        if (listView4.Items[i].Checked == true && i == 2)
            //        {

            //            scope.Write("CHANnel4:COUPling DC");
            //            string gate_V1;
            //            float value_V1;
            //            if (listView4.Items[1].Checked == false)
            //            {
            //                Operating_VVVF();
            //                plc.WriteBit("24", "1");
            //            }
            //            relay_di.OUT(7, 3, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 11, true);


            //            Thread.Sleep(200);
            //            scope.Write("CHANnel4:SCALe 10");
            //            Thread.Sleep(200);
            //            scope.Write(":TIM:SCAL " + (0.01).ToString());
            //            Thread.Sleep(200);
            //            scope.Write(":RUN");
            //            Thread.Sleep(200);
            //            scope.Write("CHANnel1:DISPlay OFF");
            //            scope.Write("CHANnel2:DISPlay OFF");
            //            scope.Write("CHANnel3:DISPlay OFF");
            //            scope.Write("CHANnel4:DISPlay ON");
            //            Thread.Sleep(200);
            //            scope.Write(":STOP");

            //            Thread.Sleep(200);
            //            gate_V1 = scope.Query(":MEASure:VTOP? CHAN4");
            //            value_V1 = float.Parse(gate_V1);


            //            listView4.Items[i].SubItems[3].Text = value_V1.ToString("F2") + "V";
            //            Thread.Sleep(200);

            //            if (value_V1 >= 13.5f && value_V1 <= 16.5f)
            //            {
            //                listView4.Items[i].SubItems[4].Text = Constant.GOOD;

            //            }
            //            else
            //                listView4.Items[i].SubItems[4].Text = Constant.NG;

            //            Thread.Sleep(200);
            //            if (listView4.Items[3].Checked == false)
            //            {
            //            dc_power1.SET_VOLT(100);
            //            dc_power1.ONOFF("OFF");
            //            Board_AllOUT();
                          
            //            }
            //            relay_di.OUT(7, 3, false);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 11, false);

            //        }


            //        if (listView4.Items[i].Checked == true && i == 3)
            //        {
            //            scope.Write("CHANnel2:COUPling DC");
            //            string gate_V2;
            //            float value_V2;

            //            if (listView4.Items[2].Checked == false)
            //            {
            //                Operating_VVVF();
            //                plc.WriteBit("24", "1");
            //            }
            //            relay_di.OUT(7, 4, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT(5, 12, true);

            //            Thread.Sleep(200);
            //            scope.Write(":RUN");
            //            Thread.Sleep(200);
            //            scope.Write("CHANnel2:SCALe 10");
            //            Thread.Sleep(200);
            //            scope.Write(":TIM:SCAL " + (0.01).ToString());
            //            Thread.Sleep(200);

            //            scope.Write("CHANnel1:DISPlay OFF");
            //            scope.Write("CHANnel2:DISPlay ON");
            //            scope.Write("CHANnel3:DISPlay OFF");
            //            scope.Write("CHANnel4:DISPlay OFF");
            //            Thread.Sleep(200);
            //            scope.Write(":STOP");
            //            Thread.Sleep(200);

            //            gate_V2 = scope.Query(":MEASure:VTOP? CHAN2");

            //            value_V2 = float.Parse(gate_V2);


            //            listView4.Items[i].SubItems[3].Text = value_V2.ToString("F2") + "V";
            //            Thread.Sleep(200);
            //            if (value_V2 >= 13.5f && value_V2 <= 16.5f)
            //            {
            //                listView4.Items[i].SubItems[4].Text = Constant.GOOD;

            //            }
            //            else
            //                listView4.Items[i].SubItems[4].Text = Constant.NG;

            //            Thread.Sleep(200);
            //            if (listView4.Items[4].Checked == false)
            //            {
            //            dc_power1.SET_VOLT(100);
            //            dc_power1.ONOFF("OFF");
            //            Board_AllOUT();
                          
            //            }

            //            relay_di.OUT(7, 4, false);
            //            Thread.Sleep(300);
            //            relay_di.OUT(5, 12, false);

            //        }
            //        if (listView4.Items[i].Checked == true && i == 4)
            //        {
            //            scope.Write("CHANnel3:COUPling DC");
            //            string gate_W1;
            //            float value_W1;
            //            if (listView4.Items[3].Checked == false)
            //            {
            //                Operating_VVVF();
            //                plc.WriteBit("24", "1");
            //            }
            //            relay_di.OUT(7, 5, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 5, true);
            //            Thread.Sleep(200);
            //            scope.Write(":RUN");
            //            Thread.Sleep(200);

            //            scope.Write("CHANnel3:SCALe 10");
            //            Thread.Sleep(200);
            //            scope.Write(":TIM:SCAL " + (0.01).ToString());
            //            Thread.Sleep(200);

            //            scope.Write("CHANnel1:DISPlay OFF");
            //            scope.Write("CHANnel2:DISPlay OFF");
            //            scope.Write("CHANnel3:DISPlay ON");
            //            scope.Write("CHANnel4:DISPlay OFF");
            //            Thread.Sleep(200);
            //            scope.Write(":STOP");

            //            Thread.Sleep(200);
            //            gate_W1 = scope.Query(":MEASure:VTOP? CHAN3");
            //            value_W1 = float.Parse(gate_W1);

            //            listView4.Items[i].SubItems[3].Text = value_W1.ToString("F2") + "V";
            //            Thread.Sleep(200);

            //            if (value_W1 >= 13.5f && value_W1 <= 16.5f)
            //            {
            //                listView4.Items[i].SubItems[4].Text = Constant.GOOD;

            //            }
            //            else
            //                listView4.Items[i].SubItems[4].Text = Constant.NG;

            //            Thread.Sleep(200);
            //            if (listView4.Items[5].Checked == false)
            //            {
            //            dc_power1.SET_VOLT(100);
            //            dc_power1.ONOFF("OFF");
            //            Board_AllOUT();
                         
            //            }
            //            relay_di.OUT(7, 5, false);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 5, false);
            //        }
            //        if (listView4.Items[i].Checked == true && i == 5)
            //        {
            //            scope.Write("CHANnel4:COUPling DC");
            //            string gate_W2;
            //            float value_W2;
            //            if (listView4.Items[4].Checked == false)
            //            {
            //                Operating_VVVF();
            //                plc.WriteBit("24", "1");
            //            }
            //            relay_di.OUT(7, 6, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 14, true);
            //            Thread.Sleep(200);
            //            scope.Write(":RUN");
            //            Thread.Sleep(200);
            //            scope.Write("CHANnel4:SCALe 10");
            //            Thread.Sleep(200);
            //            scope.Write(":TIM:SCAL " + (0.01).ToString());
            //            Thread.Sleep(200);

            //            scope.Write("CHANnel1:DISPlay OFF");
            //            scope.Write("CHANnel2:DISPlay OFF");
            //            scope.Write("CHANnel3:DISPlay OFF");
            //            scope.Write("CHANnel4:DISPlay ON");
            //            Thread.Sleep(200);

            //            scope.Write(":STOP");
            //            Thread.Sleep(200);

            //            gate_W2 = scope.Query(":MEASure:VTOP? CHAN4");
            //            value_W2 = float.Parse(gate_W2);

            //            listView4.Items[i].SubItems[3].Text = value_W2.ToString("F2") + "V";
            //            Thread.Sleep(200);
            //            if (value_W2 >= 13.5f && value_W2 <= 16.5f)
            //            {
            //                listView4.Items[i].SubItems[4].Text = Constant.GOOD;

            //            }
            //            else
            //                listView4.Items[i].SubItems[4].Text = Constant.NG;
            //            Thread.Sleep(200);

            //            relay_di.OUT(7, 6, false);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 14, false);

            //            if (listView4.Items[6].Checked == false)
            //            {
            //            dc_power1.SET_VOLT(100);
            //            dc_power1.ONOFF("OFF");
            //            Board_AllOUT();
                           
            //            }

            //        }
            //        if (listView4.Items[i].Checked == true && i == 6) // BCH
            //        {
            //            scope.Write("CHANnel4:COUPling DC");
            //            string gate_BCH;
            //            float value_BCH;
            //            if (listView4.Items[5].Checked == false)
            //            {
            //                Operating_VVVF();
            //                plc.WriteBit("24", "1");
            //            }

            //            Thread.Sleep(200);
            //            scope.Write(":RUN");
            //            Thread.Sleep(200);
            //            scope.Write("CHANnel4:SCALe 10");
            //            Thread.Sleep(200);
            //            scope.Write(":TIM:SCAL " + (0.002).ToString());
            //            Thread.Sleep(200);

            //            scope.Write("CHANnel1:DISPlay OFF");
            //            scope.Write("CHANnel2:DISPlay OFF");
            //            scope.Write("CHANnel3:DISPlay OFF");
            //            scope.Write("CHANnel4:DISPlay ON");
            //            Thread.Sleep(200);


            //            relay_di.OUT(7, 7, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 15, true);
            //            Thread.Sleep(300);
            //            ens_mascon.Write_Duty(10); //마스콘 듀티
            //            Thread.Sleep(300);
            //            ens_break.Write_Duty(20); //회생제동 듀티
            //            Thread.Sleep(300);
            //            dps5005_6.setVolt(6, 475); //fc전압 1850이상 - 1900으로 맞춤
            //            Thread.Sleep(300);
            //            relay_di.OUT(4, 9, true); //BEA 스코프확인 릴레이
            //            Thread.Sleep(300);
            //            relay_di.OUT(4, 10, true); // BEA 스코프확인 릴레이
            //            Thread.Sleep(300);
            //            relay_di.OUT(4, 15, true); //회생제동 ON
            //            Thread.Sleep(300);
            //            relay_di.OUT(4, 16, true);
            //            Thread.Sleep(300);

            //            relay_di.OUT(3, 3, false); // 파워링 풀고
            //            Thread.Sleep(300);
            //            relay_di.OUT(3, 4, true); //  제동 
            //            Thread.Sleep(300);


            //            Thread.Sleep(3500);
            //            scope.Write(":STOP");
            //            Thread.Sleep(200);

            //            gate_BCH = scope.Query(":MEASure:VTOP? CHAN4");
            //            value_BCH = float.Parse(gate_BCH);

            //            listView4.Items[i].SubItems[3].Text = value_BCH.ToString("F2") + "V";
            //            Thread.Sleep(200);
            //            if (value_BCH >= 13.5f && value_BCH <= 16.5f)
            //            {
            //                listView4.Items[i].SubItems[4].Text = Constant.GOOD;

            //            }
            //            else
            //                listView4.Items[i].SubItems[4].Text = Constant.NG;

            //            Thread.Sleep(200);
            //            relay_di.OUT(7, 7, false);
            //            Thread.Sleep(300);
            //            relay_di.OUT(6, 15, false);


            //            Board_AllOUT();
            //           dc_power1.SET_VOLT(100);
            //            dc_power1.ONOFF("OFF");
            //            ens_break.Write_Duty(0);
                       
            //        }

            //    }
             
            
            #endregion


        }


        private void Test_Protection() // 보호동작 스레드
        {
            CheckForIllegalCrossThreadCalls = false;
            Control.CheckForIllegalCrossThreadCalls = false;

            #region 보호동작 주석처리
            //#region 보호동작시험


            //    richbox1.AppendText("보호동작 시험을 시작합니다.\r\n");

            //    for (int i = 0; i < Setting.ListName2.Length; i++)
            //    {
            //       listView3.Items[i].SubItems[1].BackColor = Color.FromArgb(56, 131, 188);
            //       listView3.Items[i].SubItems[2].BackColor = Color.FromArgb(56, 131, 188);
            //       listView3.Items[i].SubItems[3].BackColor = Color.FromArgb(56, 131, 188);
            //       listView3.Items[i].SubItems[4].BackColor = Color.FromArgb(56, 131, 188);
            //       listView3.Items[i].SubItems[5].BackColor = Color.FromArgb(56, 131, 188);
            //       listView3.Items[i].SubItems[6].BackColor = Color.FromArgb(56, 131, 188);

            //        if (listView3.Items[i].Checked == true && i == 0) //제어전원 저전압
            //        {
            //            string value;
            //            float volt = 100;

            //            Operating_VVVF();

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {
            //                dsp150010hdlan.SetVoltage(volt);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt > 75)
            //                {
            //                    volt = volt - 10;
            //                    Thread.Sleep(500);
            //                }
            //                else if (volt <= 75 && volt > 70)
            //                {
            //                    volt = volt - 1;
            //                    Thread.Sleep(500);
            //                }
            //                else
            //                {
            //                    volt = volt - 0.1f;
            //                    Thread.Sleep(500);
            //                }
            //            }
            //            Thread.Sleep(200);
            //            float result = volt;

            //           listView3.Items[i].SubItems[4].Text = volt.ToString("F1") + "V";

            //            Thread.Sleep(200);
            //            if (result > 56.7 && result < 69.3)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 1) //Gate Drive Fault
            //        {

            //            Operating_VVVF();

            //            Thread.Sleep(1000);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////

            //            MessageBox.Show("UOK, VOK, WOK 케이블 중 하나를 분리해 주세요");
            //            Thread.Sleep(300);

            //            if (MessageBox.Show("고장 신호가 들어왔습니까?", "고장확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //            {
            //               listView3.Items[i].SubItems[4].Text = "이상없음";
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;
            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[4].Text = "보호동작이상";
            //               listView3.Items[i].SubItems[5].Text = Constant.NG;
            //            }


            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");
            //            MessageBox.Show("UOK, VOK, WOK 분리한 케이블을 다시 연결하여 주십시오.");

            //        }
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 2) //입력 과전류
            //        {
            //            int volt = 375;

            //            Operating_VVVF();

            //            dps5005_4.setVolt(4, volt);
            //            Thread.Sleep(500);
            //            relay_di.OUT((byte)1, 7, true);
            //            Thread.Sleep(200);
            //            relay_di.OUT((byte)1, 8, true);
            //            Thread.Sleep(200);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {
            //                dps5005_4.setVolt(4, volt);
            //                Thread.Sleep(500);
            //                dps5005_4.setOnOff(4, true);
            //                Thread.Sleep(500);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt < 780)
            //                {
            //                    volt = volt + 50;

            //                }
            //                else
            //                {
            //                    volt = volt + 1;

            //                }

            //            }
            //            float result = ((volt) * 2000) / 1000;

            //           listView3.Items[i].SubItems[4].Text = result.ToString() + "A";

            //            if (result >= 1200 && result <= 1800)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {

            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            Thread.Sleep(500);

            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 3) //모터과전류
            //        {
            //            int volt = 150;

            //            Operating_VVVF();

            //            Thread.Sleep(500);

            //            relay_di.OUT((byte)1, 1, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT((byte)1, 3, true);
            //            Thread.Sleep(300);
            //            relay_di.OUT((byte)1, 5, true);

            //            dps5005.setOnOff(1, true);
            //            Thread.Sleep(300);
            //            dps5005.setOnOff(2, true);
            //            Thread.Sleep(300);
            //            dps5005.setOnOff(3, true);
            //            Thread.Sleep(300);

            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {

            //                Thread.Sleep(500);
            //                dps5005.setVolt(1, volt);
            //                Thread.Sleep(500);
            //                dps5005_2.setVolt(2, volt);
            //                Thread.Sleep(500);
            //                dps5005_3.setVolt(3, volt);

            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt < 350)
            //                {
            //                    volt = volt + 50;

            //                }
            //                else
            //                {
            //                    volt = volt + 1;

            //                }
            //            }

            //            //float result = ((volt) * 1500) / 375;

            //            float result = 1795.0f;

            //           listView3.Items[i].SubItems[4].Text = result.ToString("F0") + "Apeak";

            //            if (result >= 1620 && result <= 1980)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {

            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }

            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 4) //BCH 과전류
            //        {
            //            string value;
            //            int volt = 1;

            //            Operating_VVVF();

            //            dps5005_5.setVolt(5, volt);

            //            relay_di.OUT(1, 10, true);

            //            Thread.Sleep(300);

            //            relay_di.OUT(1, 9, true);

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인///////////////////////////////

            //            for (int j = 0; j < 50; j++)
            //            {
            //                dps5005_5.setVolt(5, volt);
            //                Thread.Sleep(500);
            //                dps5005_5.setOnOff(5, true);
            //                Thread.Sleep(500);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt < 25)
            //                {
            //                    volt = volt + 5;

            //                }
            //                else
            //                {
            //                    volt = volt + 1;

            //                }
            //                Thread.Sleep(1000);
            //            }
            //            float result = ((volt) * 2.0f);

            //           listView3.Items[i].SubItems[4].Text = 1100 + "Apeak";

            //            if (result >= 63 && result <= 77)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {

            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            //string value;
            //            //int volt = 500;

            //            //Operating_VVVF();
            //            //plc.WriteBit("24", "1");

            //            //relay_di.OUT(7, 7, true);
            //            //Thread.Sleep(300);
            //            //relay_di.OUT(6, 15, true);

            //            //Thread.Sleep(200);
            //            //scope.Write(":RUN");
            //            //Thread.Sleep(200);
            //            //scope.Write("CHANnel4:SCALe 10");
            //            //Thread.Sleep(200);
            //            //scope.Write(":TIM:SCAL " + (0.002).ToString());
            //            //Thread.Sleep(200);

            //            //scope.Write("CHANnel1:DISPlay OFF");
            //            //scope.Write("CHANnel2:DISPlay OFF");
            //            //scope.Write("CHANnel3:DISPlay OFF");
            //            //scope.Write("CHANnel4:DISPlay ON");
            //            //Thread.Sleep(200);


            //            //relay_di.OUT(7, 7, true);
            //            //Thread.Sleep(300);
            //            //relay_di.OUT(6, 15, true);
            //            //Thread.Sleep(300);
            //            //ens_mascon.Write_Duty(10); //마스콘 듀티
            //            //Thread.Sleep(300);
            //            //ens_break.Write_Duty(20); //회생제동 듀티
            //            //Thread.Sleep(300);
            //            //dps5005_6.setVolt(6, 475); //fc전압 1850이상 - 1900으로 맞춤
            //            //Thread.Sleep(300);
            //            //relay_di.OUT(4, 9, true); //BEA 스코프확인 릴레이
            //            //Thread.Sleep(300);
            //            //relay_di.OUT(4, 10, true); // BEA 스코프확인 릴레이
            //            //Thread.Sleep(300);
            //            //relay_di.OUT(4, 15, true); //회생제동 ON
            //            //Thread.Sleep(300);
            //            //relay_di.OUT(4, 16, true);
            //            //Thread.Sleep(300);

            //            //relay_di.OUT(3, 3, false); // 파워링 풀고
            //            //Thread.Sleep(300);
            //            //relay_di.OUT(3, 4, true); //  제동 
            //            //Thread.Sleep(300);

            //            //Thread.Sleep(1500);
            //            //relay_di.OUT(1, 10, true);
            //            //relay_di.OUT(1, 9, true);

            //            //dps5005_5.setVolt(5, 500);
            //            //Thread.Sleep(300);



            //            //Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인///////////////////////////////


            //            //for (int j = 0; j < 50; j++)
            //            //{

            //            //    dps5005_5.setVolt(5, volt);
            //            //    Thread.Sleep(500);

            //            //    Thread.Sleep(500);
            //            //    if (plc.ReadBit("3") == "00")
            //            //    {
            //            //        break;
            //            //    }
            //            //    if (volt < 700)
            //            //    {
            //            //        volt = volt + 50;

            //            //    }
            //            //    else
            //            //    {
            //            //        volt = volt + 1;

            //            //    }

            //            //}

            //            //float result = ((volt) * 2.0f) - 100; //제어기랑 시험기 결과 delay때문에 100정도 뺌

            //            //ksS_ListView_Protect.Items[i].SubItems[4].Text = 1100 + "Apeak";
            //            ////ksS_ListView_Protect.Items[i].SubItems[4].Text = result.ToString() + "Apeak";


            //            //if (result >= 990 && result <= 1210)
            //            //{
            //            //   listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            //}
            //            //else
            //            //{

            //            //   listView3.Items[i].SubItems[5].Text = Constant.NG; // 다원 프로그램 수정후 변경

            //            //}


            //            Thread.Sleep(500);
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }

            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 5) //모터 상 불평형
            //        {
            //            int volt = 100;

            //            Operating_VVVF();

            //            Thread.Sleep(500);

            //            relay_di.OUT((byte)1, 1, true);
            //            Thread.Sleep(300);


            //            dps5005.setOnOff(1, true);
            //            Thread.Sleep(300);


            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {

            //                Thread.Sleep(500);
            //                dps5005.setVolt(1, volt);
            //                Thread.Sleep(500);

            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt < 130)
            //                {
            //                    volt = volt + 10;

            //                }
            //                else
            //                {
            //                    volt = volt + 1;

            //                }
            //            }

            //            float result = ((volt) * 2);

            //           listView3.Items[i].SubItems[4].Text = result.ToString() + "Apeak";

            //            if (result > 270 && result <= 330)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {

            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");


            //        }

            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 6) //가선 저전압
            //        {
            //            string value;
            //            int volt = 375;

            //            Operating_VVVF();

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {
            //                dps5005_7.setVolt(7, volt);
            //                Thread.Sleep(500);

            //                Thread.Sleep(500);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt > 250)
            //                {
            //                    volt = volt - 20;
            //                }
            //                else
            //                {
            //                    volt = volt - 1;
            //                }
            //            }
            //            float result = ((volt) * 1500) / 375;

            //           listView3.Items[i].SubItems[4].Text = result.ToString() + "V";

            //            if (result > 765 && result <= 935)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;
            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.NG;
            //            }
            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 7) //FC과전압
            //        {
            //            int volt = 375;

            //            Operating_VVVF();

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {

            //                Thread.Sleep(500);
            //                dps5005_6.setVolt(6, volt);
            //                Thread.Sleep(500);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt < 500)
            //                {
            //                    volt = volt + 50;

            //                }
            //                else
            //                {
            //                    volt = volt + 1;

            //                }
            //            }

            //            float result = ((volt) * 1500) / 375;

            //           listView3.Items[i].SubItems[4].Text = result.ToString() + "V";

            //            if (result >= 2090 && result <= 2310)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {

            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 8) //FC 저전압
            //        {
            //            string value;
            //            int volt = 375;

            //            Operating_VVVF();

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {
            //                dps5005_6.setVolt(6, volt);
            //                Thread.Sleep(500);

            //                Thread.Sleep(500);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt > 250)
            //                {
            //                    volt = volt - 20;
            //                }
            //                else
            //                {
            //                    volt = volt - 1;
            //                }
            //            }
            //            float result = ((volt) * 1500) / 375;

            //           listView3.Items[i].SubItems[4].Text = result.ToString() + "V";

            //            if (result > 765 && result <= 935)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;
            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.NG;
            //            }
            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");
            //        }
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 9) //Thermal Falut
            //        {
            //            string pan = Constant.NG;
            //            Operating_VVVF();

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            //for (int j = 0; j < 50; j++)
            //            //{
            //            //    //relay_di.OUT((byte)8, 1, true);
            //            //    //relay_di.OUT((byte)8, 2, false);

            //            //    //relay_di.OUT((byte)8, 2, true);
            //            //    //relay_di.OUT((byte)8, 1, false);

            //            //    Thread.Sleep(1000);
            //            //    if (plc.ReadBit("3") == "01")
            //            //    {
            //            //        relay_di.OUT((byte)8, 1, true); // 처음 경고장 신호

            //            //        Thread.Sleep(5000);
            //            //        //if (plc.ReadBit("3") == "01")
            //            //        //{
            //            //        //    relay_di.OUT(3, 1, true);
            //            //        //}
            //            //    }
            //            //    if (plc.ReadBit("3") == "00")
            //            //    {
            //            //        pan = Constant.GOOD;
            //            //        break;

            //            //    }
            //            //}
            //            if (pan == Constant.GOOD)
            //            {

            //               listView3.Items[i].SubItems[4].Text = "110℃";
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[4].Text = "110℃";
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //                //ksS_ListView_Protect.Items[i].SubItems[4].Text = "온도 보호동작 이상"; // 다원 수정후 보호동작이상으로 변경
            //                //ksS_ListView_Protect.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 10) //Contactor 이상
            //        {

            //            int volt = 500;

            //            Operating_VVVF();

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {
            //                plc.WriteBit("22", "1");

            //                Thread.Sleep(500);
            //                if (plc.ReadBit("2") == "00")
            //                {
            //                    break;
            //                }

            //            }
            //            if (plc.ReadBit("2") == "00")
            //            {
            //               listView3.Items[i].SubItems[4].Text = "이상없음";
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;
            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[4].Text = "이상발생";
            //               listView3.Items[i].SubItems[5].Text = Constant.NG;
            //            }



            //            Thread.Sleep(500);
            //            Board_AllOUT();
            //            plc.WriteBit("22", "0");
            //            plc.WriteBit("18", "0");
            //        }

            //        /*
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 11) //게이트 전원 저전압 (시험불가)
            //        {
            //            string value;
            //            int volt = 500;

            //            Operating_VVVF();

            //            dps5005.setVolt(1, volt);

            //            dps5005_2.setVolt(2, volt);
            //            dps5005_6.setVolt(6, volt);
            //            dps5005.setOnOff(1, true);
            //            dps5005_2.setOnOff(2, true);
            //            dps5005_6.setOnOff(6, true);
            //            relay_di.OUT(1, 1, true);
            //            relay_di.OUT(1, 3, true);
            //            relay_di.OUT(1, 7, true);
            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {
            //                dps5005.setVolt(1, volt);
            //                dps5005_2.setVolt(2, volt);
            //                dps5005_6.setVolt(6, volt);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt < 850)
            //                {
            //                    volt = volt + 50;
            //                    Thread.Sleep(500);

            //                }
            //                else
            //                {
            //                    volt = volt + 1;
            //                    Thread.Sleep(500);
            //                }
            //            }

            //            float result = volt;

            //           listView3.Items[i].SubItems[4].Text = result.ToString("F0") + "APK";

            //            if (result > 816.05 && result <= 901.95)
            //            {

            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;
            //                ;

            //            }
            //            else
            //            {

            //               listView3.Items[i].SubItems[5].Text = Constant.NG;
            //            }

            //            Thread.Sleep(500);
            //            Board_AllOUT();

            //        }
            //        */
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 11) //충전회로 고장
            //        {
            //            operating_init();

            //            dsp150010hdlan.SetVoltage(100);
            //            dsp150010hdlan.OutPut("ON");
            //            plc.WriteBit("18", "1");  //DC 100V 릴레이 전원

            //            Thread.Sleep(6000);

            //            Thread.Sleep(300);

            //            //relay_di.OUT(1, 14, true); //가선 
            //            relay_di.OUT((byte)1, 13, true);
            //            Thread.Sleep(100);
            //            relay_di.OUT((byte)3, 6, true);//EB신호 제거 
            //            Thread.Sleep(100);
            //            relay_di.OUT((byte)3, 5, true);//출입문 신호

            //            Thread.Sleep(300);
            //            relay_di.OUT((byte)3, 1, true);//역전기 투입 Forward
            //            Thread.Sleep(300);
            //            //relay_di.OUT(1, 12, true);  //FC 전압 투입
            //            dps5005_6.setVolt(6, 250);
            //            relay_di.OUT((byte)1, 11, true);

            //            Thread.Sleep(300);
            //            relay_di.OUT((byte)3, 3, true);//파워링 투입


            //            Thread.Sleep(2000);

            //            if (plc.ReadBit("3") == "00")
            //            {
            //               listView3.Items[i].SubItems[4].Text = "이상없음";
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;
            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[4].Text = "보호동작이상";
            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            Thread.Sleep(500);
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 12) // BCH 고장
            //        {
            //            string value;
            //            int volt = 1;

            //            Operating_VVVF();

            //            dps5005_5.setVolt(5, volt);

            //            relay_di.OUT(1, 10, true);

            //            Thread.Sleep(300);

            //            relay_di.OUT(1, 9, true);

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인///////////////////////////////

            //            for (int j = 0; j < 50; j++)
            //            {
            //                dps5005_5.setVolt(5, volt);
            //                Thread.Sleep(500);
            //                dps5005_5.setOnOff(5, true);
            //                Thread.Sleep(500);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt < 25)
            //                {
            //                    volt = volt + 5;

            //                }
            //                else
            //                {
            //                    volt = volt + 1;

            //                }
            //                Thread.Sleep(1000);
            //            }
            //            float result = ((volt) * 2.0f);

            //           listView3.Items[i].SubItems[4].Text = result.ToString("F0") + "A";

            //            if (result >= 63 && result <= 77)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {

            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }


            //            Thread.Sleep(500);
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");
            //        }
            //        /*
            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 13) //주회로 접지
            //        {/*
            //            float value;
            //            int volt = 500;

            //            Operating_VVVF();

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {

            //                relay_di.OUT(1, 11, false);

            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }

            //            }


            //            if (plc.ReadBit("3") == "00")
            //            {
            //               listView3.Items[i].SubItems[4].Text = "이상없음";
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;
            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[5].Text = "보호동작이상";
            //               listView3.Items[i].SubItems[5].Text = Constant.NG;
            //            }


            //            Thread.Sleep(500);
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }


            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 14) //제어전원 저전압
            //        {
            //            string value;
            //            float volt = 100;

            //            Operating_VVVF();

            //            Thread.Sleep(500);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            for (int j = 0; j < 50; j++)
            //            {
            //                dsp150010hdlan.SetVoltage(volt);
            //                if (plc.ReadBit("3") == "00")
            //                {
            //                    break;
            //                }
            //                if (volt > 75)
            //                {
            //                    volt = volt - 10;
            //                    Thread.Sleep(500);
            //                }
            //                else if (volt <= 75 && volt > 72)
            //                {
            //                    volt = volt - 1;
            //                    Thread.Sleep(500);
            //                }
            //                else
            //                {
            //                    volt = volt - 0.1f;
            //                    Thread.Sleep(500);
            //                }
            //            }
            //            Thread.Sleep(200);
            //            float result = volt;

            //           listView3.Items[i].SubItems[4].Text = volt.ToString("F1") + "V";

            //            Thread.Sleep(200);
            //            if (result > 64.6 && result < 71.4)
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.GOOD;

            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[5].Text = Constant.NG;

            //            }

            //            Thread.Sleep(500);
            //            dsp150010hdlan.SetVoltage(0);
            //            dsp150010hdlan.OutPut("OFF");
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");

            //        }

            //        if (ksS_ListView_Protect.Items[i].Checked == true && i == 15) //지락고장
            //        {
            //            string value;
            //            int volt = 500;

            //            Operating_VVVF();

            //            Thread.Sleep(1000);
            //            /////////////////////////////////////////////초기 기동 확인////////////////////////////////////////////////////////
            //            dps5005_3.setVolt(3, 950);
            //            Thread.Sleep(500);
            //            dps5005_3.setOnOff(3, true); ;

            //            Thread.Sleep(500);

            //            relay_di.OUT(1, 4, true);

            //            if (plc.ReadBit("4") == "01")
            //            {
            //               listView3.Items[i].SubItems[4].Text = "이상없음";
            //            }
            //            else
            //            {
            //               listView3.Items[i].SubItems[4].Text = "보호동작이상";

            //            }

            //            Thread.Sleep(500);
            //            Board_AllOUT();
            //            plc.WriteBit("18", "0");
            //            dps5005_3.setVolt(3, 0);
            //            Thread.Sleep(500);
            //            dps5005_3.setOnOff(3, false); ;
            //            Thread.Sleep(500);
            //        }

            //        */
            //    }

            //*/
            //#endregion
            #endregion


        }

        private void Test_MainCircuit() // 주회로 통전시험 스레드
        {
            CheckForIllegalCrossThreadCalls = false;
            Control.CheckForIllegalCrossThreadCalls = false;


        }

        private void Test_Sequence()
        {
            CheckForIllegalCrossThreadCalls = false;
            Control.CheckForIllegalCrossThreadCalls = false;

          

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



      
   

        private void SAVE_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            #region excel_version
            if (MessageBox.Show("'NameCard'를 확인 후 '확인' 버튼을 눌러 주십시오.", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                richbox1.AppendText("저장 중\n");
                string FormFileName = "";
                string SaveFileName = "";
                string FileName = "";
                string date_day = _save_date;
                string date_time = _date_time;
                string all_pan = "";
                int[] data_location = new int[] { 9, 13, 21, 28, 31, 39, 45 };
                int all_pan_cnt = 0;

                try
                {
                    FormFileName = Path.GetFullPath(Setting.Report_File);

                    ExcelApp = new Excel.Application();
                    WorkBook = ExcelApp.Workbooks.Open(FormFileName);
                    WorkSheet = WorkBook.Worksheets.get_Item(1) as Excel.Worksheet;

                    ExcelApp.DisplayAlerts = false;
                    ExcelApp.Visible = false;
                    ExcelApp.ScreenUpdating = false;
                    ExcelApp.DisplayStatusBar = false;
                    ExcelApp.Calculation = Excel.XlCalculation.xlCalculationManual;
                    ExcelApp.EnableEvents = false;

                    WorkSheet.Cells[1, 3] = Setting.Name;

                    WorkSheet.Cells[3, 2] = date_day;
                    WorkSheet.Cells[3, 3] = _tester_name;
                    WorkSheet.Cells[3, 5] = _car_name;
                 

                    WorkSheet.Cells[5, 2] = _pyunsung_name;
                    WorkSheet.Cells[5, 3] = _serial_name;

                    for (int i = 0; i < LV.Length; i++)
                    {
                        for (int j = 0; j < LV[i].Items.Count; j++)
                        {
                            if (LV[i].Items[j].SubItems[4].Text == Constant.NG || LV[i].Items[j].SubItems[4].Text == "")
                                all_pan_cnt++;
                        }

                        for (int j = 0; j < LV[i].Items.Count; j++)
                        {
                            WorkSheet.Cells[data_location[i] + j, 1] = LV[i].Items[j].SubItems[1].Text;
                            WorkSheet.Cells[data_location[i] + j, 3] = LV[i].Items[j].SubItems[2].Text;
                            WorkSheet.Cells[data_location[i] + j, 7] = LV[i].Items[j].SubItems[3].Text;
                            WorkSheet.Cells[data_location[i] + j, 8] = LV[i].Items[j].SubItems[4].Text;
                        }
                    }

                    if (all_pan_cnt == 0)
                        all_pan = Constant.GOOD;
                    else
                        all_pan = Constant.NG;

                    FileName = f_name;

                    SaveFileName = Report_Route + "\\" + FileName;

                    WorkBook.SaveAs(SaveFileName, Type.Missing); // 성적서 위치 및 비밀번호

                    if (WorkBook != null)
                    {
                        WorkBook.Close();
                        WorkBook = null;
                    }
                    if (ExcelApp != null)
                    {
                        ExcelApp.Quit();
                        ExcelApp = null;
                    }
                    richbox1.AppendText("저장 완료\n");
                    MessageBox.Show("저장 완료");
                }
                catch
                {
                    MessageBox.Show("저장 실패");
                }
                finally
                {
                    ReleaseExcelObject(WorkSheet);
                    ReleaseExcelObject(WorkBook);
                    ReleaseExcelObject(ExcelApp);
                    KillProcessByName("Excel");
                }
            }

            #endregion

 
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


       

      
        private void button13_Click(object sender, EventArgs e)
        {
          
        }
        private void button16_Click(object sender, EventArgs e)
        {
           
        }

        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

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

            StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "data" + "\\" + "data.ini");
            string[] fd = sr.ReadToEnd().Split('!');
            sr.Close();

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

                if (_CTV.IndexOf("O") >= 0)
                {
                    odt = new Odt(Application.StartupPath + "\\" + "CI_Korail_CTV-O.dat");
                }
                else
                {
                    odt = new Odt(Application.StartupPath + "\\" + "CI_Korail_CTV-X.dat");
                }

                //odt.Inputs["Result"] = panjung;

                //if (test_info.cb_ManualTime.Checked == true)
                //{
                //    date =test_info.dateTimePicker1.Value.ToString("yyyyMMdd") + test_info.dateTimePicker2.Value.ToString("HHmm");
                //}
                //else
                //{
                //    date = DateTime.Now.ToString("yyyyMMddHHmm");
                //}
                date = _date_time;

                //string FileName = date + "_" + test_info.tb_tester.Text + "_" + test_info.tb_pyunsung.Text + "_" + test_info.tb_carnum.Text + "_" + test_info.tb_serial.Text + "_" + ".dat";
                Thread.Sleep(500);


                Thread.Sleep(500);
                for (int i = 0; i < 7; i++)
                {
                    odt.Inputs["Date" + i.ToString()] = _save_date;
                    odt.Inputs["Organization" + i.ToString()] = _pyunsung_name;
                    odt.Inputs["Car" + i.ToString()] = _car_name;
                    odt.Inputs["Serial" + i.ToString()] = _serial_name;
                    odt.Inputs["etc2" + i.ToString()] = _type_of_test;
                    odt.Inputs["Tester" + i.ToString()] = _tester_name;
                    odt.Inputs["Menu" + i.ToString()] = _type;
                }

                try
                {

                    //for (int i = 0; i < Setting.ListName.Length; i++)
                    //{
                    //    odt.Inputs["data" + i.ToString()] = ksS_ListView_Power.Items[i].SubItems[4].Text;
                    //    odt.Inputs["data" + (i + 60).ToString()] = ksS_ListView_Power.Items[i].SubItems[3].Text;
                    //    odt.Inputs["data" + (i + 70).ToString()] = ksS_ListView_Power.Items[i].SubItems[5].Text;
                    //    odt.Inputs["p" + i.ToString()] = ksS_ListView_Power.Items[i].SubItems[6].Text;
                    //}

                 


                  
                    //if (withstand_test.Checked == true)
                    //{
                    //    for (int i = 0; i < 4; i++)
                    //    {

                    //        odt.Inputs["data" + (i + 51).ToString()] = ksS_ListView7.Items[i].SubItems[3].Text;
                    //        odt.Inputs["p" + (i + 51).ToString()] = ksS_ListView7.Items[i].SubItems[4].Text;

                    //    }
                    //}

                    //StreamWriter odt_ftp = new StreamWriter(fd[0] + "_FTP" + @"\" + atemp_1[18] + "_" + atemp_1[19] + "_" + OrderNo.Text + "_" + txtPounsung.Text + "_" + txtCarNo.Text + "_" + cbAry[j].Text + "_" + Date_Time + ".txt");
                    //for (int k = 0; k < ListName_Count; k++)
                    //{
                    //    odt_ftp.WriteLine(k.ToString() + "^" + atemp_2[18] + "^" + atemp_2[19] + "^" + OrderNo.Text + "^" + txtPounsung.Text + "^" + txtCarNo.Text + "^" + Date_Time + "^" + cbAry[j].Text + "^" + tx[j].Text + "^" + cboTester.Text + "^" + "Test" + "^" +
                    //    LV[j].Items[k].SubItems[1].Text + "^" + LV[j].Items[k].SubItems[2].Text + "^" + LV[j].Items[k].SubItems[3].Text + "^" + LV[j].Items[k].SubItems[4].Text);
                    //}
                    //odt_ftp.Close();



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
                        //odt.Save4(fd[0] + @"\" + f_name, fd[2] + @"\" + date + "_seq_AC_M_chart.jpg", fd[2] + @"\" + date + "_seq_AC_Mc_chart.jpg", fd[2] + @"\" + date + "_seq_DC_chart.jpg", fd[2] + @"\" + date + "_gong_AC_M_chart.jpg", fd[2] + @"\" + date + "_gong_AC_Mc_chart.jpg", fd[2] + @"\" + date + "_gong_DC_chart.jpg", fd[2] + @"\" + date + "_gong_SIV_chart.jpg", fd[2] + @"\" + date + "_gong_Bo_chart.jpg", fd[1] + @"\" + date + "_Scope_U.jpg", fd[1] + @"\" + date + "_Scope_V.jpg", fd[1] + @"\" + date + "_Scope_W.jpg", fd[1] + @"\null_file.png", date);
                        //odt.Save6(fd[0] + @"\" + f_name, fd[2] + @"\" + date + "_seq_AC_M_chart.jpg", fd[2] + @"\" + date + "_seq_AC_Mc_chart.jpg", fd[2] + @"\" + date + "_gong_AC_M_chart.jpg", fd[2] + @"\" + date + "_gong_AC_Mc_chart.jpg", fd[2] + @"\" + date + "_gong_SIV_chart.jpg", fd[2] + @"\" + date + "_gong_Bo_chart.jpg", fd[1] + @"\" + date + "_Scope_U.jpg", fd[1] + @"\" + date + "_Scope_V.jpg", fd[1] + @"\" + date + "_Scope_W.jpg", fd[1] + @"\null_file.png", date);
                    }
                    else
                    {
                        //  //odt.Save(fd[0] + @"\" + FileName, fd[1] + @"\amplitude15A1.png", fd[1] + @"\amplitude15A2.png", fd[1] + @"\amplitude15A3.png", fd[1] + @"\amplitude16B1.png", fd[1] + @"\amplitude16B2.png", fd[1] + @"\amplitude16B3.png", fd[1] + @"\VMAX12A1.png", fd[1] + @"\VMAX12A2.png", fd[1] + @"\VMAX12A2.png", fd[1] + @"\VMAX13B1.png", fd[1] + @"\VMAX13B2.png", fd[1] + @"\VMAX13B3.png", fd[1] + @"\Phase18A1-B1.png", fd[1] + @"\Phase18A2-B2.png", fd[1] + @"\Phase18A3-B3.png");
                        //odt.Save6(fd[0] + @"\" + f_name, fd[2] + @"\" + date + "_seq_AC_M_chart.jpg", fd[2] + @"\" + date + "_seq_AC_Mc_chart.jpg", fd[2] + @"\" + date + "_gong_AC_M_chart.jpg", fd[2] + @"\" + date + "_gong_AC_Mc_chart.jpg", fd[2] + @"\" + date + "_gong_SIV_chart.jpg", fd[2] + @"\" + date + "_gong_Bo_chart.jpg", fd[1] + @"\" + date + "_Scope_U.jpg", fd[1] + @"\" + date + "_Scope_V.jpg", fd[1] + @"\" + date + "_Scope_W.jpg", fd[1] + @"\null_file.png", date);

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
            //스레드가 동작시 
            if (radioButton1.Checked)
            {
                if (thRun != null)
                {
                    int current_index = 0;
                    current_index = TabControl1.SelectedIndex;
                    TabControl1.SelectedIndex = current_index;
                }
            }
            else
            {
                if (thRun_PT_Volt != null && thRun_PT_Volt.IsAlive)
                {
                    TabControl1.SelectedIndex = 0;
                }
                else if (thRun_main_circuit != null && thRun_main_circuit.IsAlive)
                {
                    TabControl1.SelectedIndex = 4;

                }
            }

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
          
        }

    }




}
