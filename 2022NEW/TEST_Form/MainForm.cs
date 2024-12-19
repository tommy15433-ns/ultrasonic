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
using System.Reflection.Emit;
using System.Deployment.Application;



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
        Thread thRun2;
        Thread thRun3;

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

        //float ChX = 0;
        float ChY = 0;
        float gong = 0;

        string _tester_name;
        string _pyunsung_name;
        string _serial_name;
        string _type_of_test;
        string _car_name;
        string _date_time;
        string _type;
        string _CTV;


        string _up_press;
        string _down_press;
        string _diff_press;
        string _raising_min_time;
        string _raising_max_time;
        string _lowing_min_time;
        string _lowing_max_time;
        string _leakage;
        string _min_volt;
        string _min_Air;

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
        double height = 0;
        bool batch_flag = false;

        static Excel.Application ExcelApp = null;
        static Excel.Workbook WorkBook = null;
        static Excel.Worksheet WorkSheet = null;

        ManualResetEvent _loadEvent = new ManualResetEvent(true);


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
            
            }
            catch
            {

            }

            this.Close();
        }


        void GoZero()
        {
            if (plc.ReadBit("163") == "01")
            {
                //plc.WriteBit("21", "1");
            }

            richbox1.AppendText("Origine Setting\r");
            //Name_Label.Text = "영점 조절 진행중";
            plc.WriteBit("25", "1");//암원점
            while (true)
            {
                if (plc.ReadBit("164") == "01")//암원점확인
                    break;
                Thread.Sleep(100);
            }
            Thread.Sleep(1000);
            richbox1.AppendText ("Origine Setting Setting Complete\r");
            
            Thread.Sleep(500);
                   
        }

        void GoZero_one()
        {

            richbox1.AppendText("Origine Setting Setting\r");
            //Name_Label.Text = "영점 조절 진행중";
            plc.WriteBit("25", "1");//암원점
            while (true)
            {
                if (plc.ReadBit("164") == "01")//암원점확인
                    break;
                Thread.Sleep(100);
            }
            Thread.Sleep(1000);
            richbox1.AppendText("Origine Setting Complete\r");

            Thread.Sleep(500);

        }


        //public async void hei()
        //{
        //    CheckForIllegalCrossThreadCalls = false;
        //    try
        //    { byte[] data = new byte[4];

        //        while (_loadEvent.WaitOne())
        //        {
        //            try
        //            {
        //                data = plc.ReadDouble_byte("502");

        //                height = ((((double)data[2]) * 256) + (double)data[3]);

        //                s2.Value = height.ToString();

        //                Thread.Sleep(100);
        //            }
        //            catch
        //            { 
                    
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        public async void TestExecute2()
        {
            CheckForIllegalCrossThreadCalls = false;

            try
            {
                while (_loadEvent.WaitOne())
                {
                    try
                    {
                        string temp = loadcell.ReadValue();

                        if (temp.IndexOf('+') > 0)
                        {
                            string[] tempAry = temp.Split('+');
                            string[] tempAry2 = tempAry[1].Split('k');
                            try
                            {
                                float load = float.Parse(tempAry2[0]);
                                float N = load * 9.8f;
                                s1.Value =N.ToString("F1");
                            }
                            catch
                            {

                            }
                        }
                        else
                        {

                            string[] tempAry = temp.Split('-');
                            string[] tempAry2 = tempAry[1].Split('k');
                            try
                            {
                                float b = float.Parse(tempAry2[0]);
                                float M = b * 9.8f;
                                s1.Value = M.ToString("F1");
                            }
                            catch
                            {
                            }
                        }

                        Thread.Sleep(50);
                    }
                    catch
                    { 
                    
                    }
                    try
                    {
                        if (plc.ReadBit("162") == "01")
                        {
                            if (thRun != null)
                            {
                                thRun.Abort();                              
                            }
                        
                        }

                        byte[] data = new byte[4];
                        data = plc.ReadDouble_byte("502");

                        height = ((((double)data[2]) * 256) + (double)data[3]);

                        s2.Value = height.ToString();

                        Thread.Sleep(50);
                    }
                    catch
                    {

                    }

                    Thread.Sleep(200);
                }
            }
            catch
            {
                // MessageBox.Show("M");
            }



    
        }


        private void START_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                thRun = new Thread(TestExecute_Test);
                thRun.Start();
            }
            catch
            { 
            }
          
        }

        delegate void ListDelegate(KSS_ListView ctrl, int item, int sub, string txt);

        public void SetList(KSS_ListView ctrl, int item, int sub, string txt)
        {


            if (ctrl.InvokeRequired)
            {
                ListDelegate t = new ListDelegate(SetList);
                try
                {
                    ctrl.Invoke(t, new object[] { ctrl, item, sub, txt });
                }
                catch
                {

                }
            }
            else
            {

                ctrl.Items[item].SubItems[sub].Text = txt;
            }
        }


        delegate void ChartDelegate(System.Windows.Forms.DataVisualization.Charting.Chart ctrl, int series, double x, double y);

        public void SetChart(System.Windows.Forms.DataVisualization.Charting.Chart ctrl, int series, double x, double y)
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


        private void TestExecute_Test()
        {
            CheckForIllegalCrossThreadCalls = false;

            try
            {
                BTN_Enable(1); // 0이 아닌 숫자는 시험 중

                // 서보모터 시험 위치 
                //서보드라이버의 전원이 안켜져있으면 켜기

                if (plc.ReadBit("160") != "01")               
                {
                    plc.WriteBit("8", "1");



                    if (plc.ReadBit("163") == "01")
                    {
                        //plc.WriteBit("21", "1");
                    }



                    if (plc.ReadBit("164") !="01")//원점 확인
                    { 
                        GoZero();
                    }

                    // comp 전원 on 
                   
                    MessageBox.Show("Press the compressor ON button on the back of the tester for 3 seconds.");

                    Thread.Sleep(3000);

                    for (int i = 0; i < 6; i++)
                    {
                        if (i == 0 && checkBox4.Checked) //압상력
                        {
                            Thread.Sleep(3000);
                            TabControl1.SelectedIndex = 0;

                            button3_Click_1(null, null); // 판토 상승

                            Thread.Sleep(5000);

                            //int step = 21;
                            //nt[] step_aty = new int[] {400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000, 2100, 2200, 2300 };

                            int step = 19;
                            int[] step_aty = new int[] { 400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000, 2100};


                            // 조그 하강

                            plc.WriteDouble("200", "4531");//설정위치 2400mm
                            Thread.Sleep(200);
                            plc.WriteDouble("201", "500");//본체속도
                            Thread.Sleep(200);
                            
                            plc.WriteBit("26", "1"); //본체기동

                            for (int j =0; j<100;j++)
                            {
                                if (plc.ReadBit("167") == "01" )//도착확인
                                {
                                    break;

                                }

                                Thread.Sleep(500);
                            }
                            Thread.Sleep(3000);

                            listview1_test_start(0);


                            richbox1.AppendText("lowering start\r");

                            chart1.Series[0].Points.Clear();

                            plc.WriteDouble("200", "79560");//설정위치 300mm
                            Thread.Sleep(200);
                            plc.WriteDouble("201", "500");//본체속도
                            Thread.Sleep(200);

                            plc.WriteBit("26", "1"); //본체기동

                            try
                            {
                                while (true)
                                {
                                    ChY = float.Parse(s1.Value.ToString());
                                   
                                    //chart1.Series[0].Points.AddXY(height, ChY);
                                     SetChart(chart1, 0, height, ChY);
                                    try
                                    {
                                        if (height <= step_aty[step - 2])
                                        {
                                            listView1.Items[0].SubItems[step].Text = ChY.ToString("F1");
                                            step--;
                                        }

                                      

                                    }
                                    catch
                                    {


                                    }

                                    if (plc.ReadBit("167") == "01")//도착확인
                                    {
                                        break;

                                    }

                                    //if (height <= 400)//목표확인
                                    //{

                                    //    //plc.WriteBit("23", "0");
                                    //    break;
                                    //}

                                    Thread.Sleep(1000);
                                }
                            }
                            catch
                            { 
                            
                            }

                            bool pan = true;

                            for (int kl = 2; kl < step_aty.Length + 2; kl++)
                            {
                                if (float.Parse(listView1.Items[0].SubItems[kl].Text) > float.Parse(_up_press))
                                    pan = false;
                            }
                            if (pan)
                                listView1.Items[0].SubItems[20].Text = Constant.GOOD;
                            else
                                listView1.Items[0].SubItems[20].Text = Constant.NG;


                            listview1_test_stop(0);


                            Thread.Sleep(5000);
                            

                            listview1_test_start(1);
                            listview1_test_start(2);


                            //조그 상승 


                            richbox1.AppendText("Rising start\r");

                            plc.WriteDouble("200", "4530");//설정위치
                            Thread.Sleep(200);
                            plc.WriteDouble("201", "500");//본체속도
                            Thread.Sleep(200);

                            plc.WriteBit("26", "1"); //본체기동

                            step = 2;

                            try
                            {

                                while (true)
                                {

                                    ChY = float.Parse(s1.Value.ToString());


                                    //chart1.Series[0].Points.AddXY(height, ChY);
                                    SetChart(chart1, 0, height, ChY);
                                    try
                                    {
                                        if (height >= step_aty[step - 2])
                                        {
                                            listView1.Items[1].SubItems[step].Text = ChY.ToString("F1");
                                            listView1.Items[2].SubItems[step].Text = Math.Abs(float.Parse(listView1.Items[1].SubItems[step].Text) - float.Parse(listView1.Items[0].SubItems[step].Text)).ToString("F1");
                                            label4.Text = step.ToString();
                                            
                                            step++;
                                            
                                        }

                                       
                                    }
                                    catch
                                    {


                                    }
                                    if (plc.ReadBit("167") == "01") //도착위치확인
                                    {
                                        break;

                                    }

                                    Thread.Sleep(1000);
                                }
                            }
                            catch
                            { 
                            
                            }

                            pan = true;
                            for (int kl = 2; kl < step_aty.Length + 2; kl++)
                            {
                                if (float.Parse(listView1.Items[1].SubItems[kl].Text) < float.Parse(_down_press))
                                    pan = false;
                            }
                            if (pan)
                                listView1.Items[1].SubItems[20].Text = Constant.GOOD;
                            else
                                listView1.Items[1].SubItems[20].Text = Constant.NG;

                            pan = true;
                            for (int kl = 2; kl < step_aty.Length + 2; kl++)
                            {
                                if (float.Parse(listView1.Items[2].SubItems[kl].Text) > float.Parse(_diff_press))
                                    pan = false;
                            }
                            if (pan)
                                listView1.Items[2].SubItems[20].Text = Constant.GOOD;
                            else
                                listView1.Items[2].SubItems[20].Text = Constant.NG;


                            listview1_test_stop(1);
                            listview1_test_stop(2);

                            Thread.Sleep(2000);

                            //판토 하강
                            button4_Click(null, null);




                            GoZero();


                        }
                        if (i == 1 && listView3.Items[i - 1].Checked) //상승시간
                        {
                            listView3.Items[i - 1].SubItems[1].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[3].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[2].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[4].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.White;

                            Thread.Sleep(3000);
                            //만약 판도가 상승하여있다면 판토를 하강해라

                            if (plc.ReadBit("166") != "01")
                            {
                                button4_Click(null, null);

                            }
                            
                            
                            if (plc.ReadBit("165") != "01")
                            {

                                plc.WriteBit("14", "1");// 리셋 릴레이
                                Thread.Sleep(1000);
                                plc.WriteBit("14", "0");// 리셋 릴레이

                                plc.WriteWord("102", "0");//3.9바kpa 넣기 
                                Thread.Sleep(1000);
                                plc.WriteBit("18", "1"); // 입력 sol on
                                Thread.Sleep(1000);
                                for (int k = 0; k < 100; k++)
                                {
                                    int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                                    if (a <= 100)
                                    {
                                        //plc.WriteBit("18", "0"); //입력 sol off
                                        break;
                                    }
                                    Thread.Sleep(1000);
                                }
                                Thread.Sleep(1000);

                                plc.WriteBit("19", "1"); // 배기sol on
                                Thread.Sleep(200);
                                plc.WriteBit("20", "1"); // 공급 sol on
                                Thread.Sleep(200);

                                dc_power.SetVoltage(100);
                                dc_power.SetCurrent(3);
                                dc_power.OutPut("ON");
                                Thread.Sleep(1000);

                                for (int k = 0; k < 10; k++)
                                {
                                    listView3.Items[i - 1].SubItems[3].Text = k.ToString() + " sec";

                                    Thread.Sleep(1000);
                                }

                                listView3.Items[i - 1].SubItems[3].Text =  "";

                                plc.WriteBit("29", "1");// 펼침시간 시험 항목
                                Thread.Sleep(1000);
                                plc.WriteBit("17", "1");// 접힘시간 시험 항목
                                Thread.Sleep(1000);

                                //dc_power.OutPut("OFF");

                                for (int k = 0; k < 100; k++)
                                {
                                    if (plc.ReadBit("165") == "01")
                                    {
                                        break;
                                    }
                                    Thread.Sleep(200);
                                }

                                byte[] time_list = new byte[4];

                                time_list = plc.ReadDouble_byte("500");

                                double time = ((((double)time_list[2]) * 256) + (double)time_list[3]);


                                listView3.Items[i - 1].SubItems[3].Text = (time / 1000).ToString("F2") + " sec";


                                if ((time / 1000) >= float.Parse(_raising_min_time) && (time / 1000) <= float.Parse(_raising_max_time))
                                {
                                    listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;
                                }
                                else
                                {
                                    listView3.Items[i - 1].SubItems[4].Text = Constant.NG;
                                }

                                plc.WriteBit("18", "0"); // 입력 sol on
                                plc.WriteBit("19", "0"); // 배기sol off
                                Thread.Sleep(200);
                                plc.WriteBit("20", "0"); // 공급 sol off

                                plc.WriteBit("17", "0");// 접힘시간 시험 항목
                                Thread.Sleep(200);
                                plc.WriteBit("29", "0");// 접힘시간 시험 항목

                                Thread.Sleep(2000);
                                ///판토 하강
                                button4_Click(null, null);

                            }

                            if ((i-1) % 2 == 0)
                            {
                                listView3.Items[i - 1].SubItems[1].BackColor = Color.AliceBlue;
                                listView3.Items[i - 1].SubItems[3].BackColor = Color.AliceBlue;
                                listView3.Items[i - 1].SubItems[2].BackColor = Color.Linen;
                                listView3.Items[i - 1].SubItems[4].BackColor = Color.Linen;
                            }
                            else
                            {
                                listView3.Items[i - 1].SubItems[1].BackColor = Color.LightCyan;
                                listView3.Items[i - 1].SubItems[3].BackColor = Color.LightCyan;
                                listView3.Items[i - 1].SubItems[2].BackColor = Color.LightYellow;
                                listView3.Items[i - 1].SubItems[4].BackColor = Color.LightYellow;
                            }


                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.Black;


                        }
                        if (i == 2 && listView3.Items[i - 1].Checked) //하강시간
                        {

                            listView3.Items[i - 1].SubItems[1].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[3].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[2].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[4].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.White;
                            Thread.Sleep(3000);


                            //TabControl1.SelectedIndex = 1;
                            plc.WriteBit("14", "1");// 리셋 릴레이
                            Thread.Sleep(1000);
                            plc.WriteBit("14", "0");// 리셋 릴레이


                            //판토 상승

                            button3_Click_1(null, null);
                            Thread.Sleep(1000); 


                            plc.WriteWord("102", "7000");//6kpa 넣기 




                            plc.WriteBit("18", "1"); // 입력 sol on
                            Thread.Sleep(1000);

                            for (int k = 0; k < 100; k++)
                            {

                                int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                                if (a >= 6700)
                                {
                                    //plc.WriteBit("18", "0"); //입력 sol off
                                    //plc.WriteBit("18", "0"); //입력 sol off
                                    break;
                                }

                              
                                Thread.Sleep(1000);
                            }

                            Thread.Sleep(1000);
                            plc.WriteBit("30", "1");// 접힘시간 시험 항목
                            Thread.Sleep(200);
                            plc.WriteBit("20", "1");// 접힘시간 시험 항목
                            Thread.Sleep(200);

                            for (int k = 0; k < 100; k++)
                            {
                                if (plc.ReadBit("166") == "01")
                                {
                                    break;
                                }
                                Thread.Sleep(200);
                            }

                            byte[] time_list = new byte[4];

                            time_list =  plc.ReadDouble_byte("501");

                           double time = ((((double)time_list[2]) * 256) + (double)time_list[3]);

                           
                            listView3.Items[i - 1].SubItems[3].Text = (time/1000).ToString("F2") + " sec";

                            if ((time / 1000) >= float.Parse( _lowing_min_time) && (time / 1000) <= float.Parse(_lowing_max_time))
                            {
                                listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;
                            }
                            else
                            {
                                listView3.Items[i - 1].SubItems[4].Text = Constant.NG;
                            }
                            

                            plc.WriteBit("20", "0");// 접힘시간 시험 항목
                            Thread.Sleep(200);
                            plc.WriteBit("30", "0");// 접힘시간 시험 항목

                            ///판토 하강

                            button4_Click(null, null); 

                            if ((i - 1) % 2 == 0)
                            {
                                listView3.Items[i - 1].SubItems[1].BackColor = Color.AliceBlue;
                                listView3.Items[i - 1].SubItems[3].BackColor = Color.AliceBlue;
                                listView3.Items[i - 1].SubItems[2].BackColor = Color.Linen;
                                listView3.Items[i - 1].SubItems[4].BackColor = Color.Linen;
                            }
                            else
                            {
                                listView3.Items[i - 1].SubItems[1].BackColor = Color.LightCyan;
                                listView3.Items[i - 1].SubItems[3].BackColor = Color.LightCyan;
                                listView3.Items[i - 1].SubItems[2].BackColor = Color.LightYellow;
                                listView3.Items[i - 1].SubItems[4].BackColor = Color.LightYellow;
                            }

                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.Black;


                        }
                        if (i == 3 && listView3.Items[i - 1].Checked) //최저동작 전압
                        {
                            listView3.Items[i - 1].SubItems[1].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[3].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[2].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[4].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.White;
                            Thread.Sleep(3000);
                          
                            if (plc.ReadBit("166") != "01")
                            {
                                button4_Click(null, null);

                            }

                            if (plc.ReadBit("165") != "01")
                            {

                                plc.WriteBit("14", "1");// 리셋 릴레이
                                Thread.Sleep(1000);
                                plc.WriteBit("14", "0");// 리셋 릴레이

                                plc.WriteWord("102", "0");//3.9바kpa 넣기 
                                Thread.Sleep(1000);
                                plc.WriteBit("18", "1"); // 입력 sol on
                                Thread.Sleep(1000);
                                for (int k = 0; k < 100; k++)
                                {
                                    int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                                    if (a <= 100)
                                    {
                                        //plc.WriteBit("18", "0"); //입력 sol off
                                        break;
                                    }
                                    Thread.Sleep(1000);
                                }
                                Thread.Sleep(1000);

                                plc.WriteBit("19", "1"); // 배기sol on
                                Thread.Sleep(200);
                                plc.WriteBit("20", "1"); // 공급 sol on
                                Thread.Sleep(200);

                                dc_power.SetVoltage(77);
                                dc_power.SetCurrent(2);
                                dc_power.OutPut("ON");
                                Thread.Sleep(1000);

                                for (int k = 0; k < 10; k++)
                                {
                                    listView3.Items[i - 1].SubItems[3].Text = k.ToString() + " sec";

                                    Thread.Sleep(1000);
                                }

                                plc.WriteBit("17", "1"); // 100V 투입 릴레이
                                Thread.Sleep(500);
                                //plc.WriteBit("17", "0"); // 100V 투입 릴레이
                                Thread.Sleep(300);
                                //dc_power.OutPut("OFF");

                                for (int k = 0; k < 100; k++)
                                {
                                    if (plc.ReadBit("165") == "01")
                                    {
                                        listView3.Items[i - 1].SubItems[3].Text = _min_volt + "V";

                                        listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;


                                        break;
                                    }
                                    else if (plc.ReadBit("166") == "01")
                                    {
                                        listView3.Items[i - 1].SubItems[3].Text = "최저동작 이상";
                                        listView3.Items[i - 1].SubItems[4].Text = Constant.NG;
                                        break;
                                    }
                                    Thread.Sleep(1000);
                                }

                                plc.WriteBit("19", "0"); // 배기sol on
                                Thread.Sleep(200);
                                plc.WriteBit("20", "0"); // 공급 sol on



                                if ((i - 1) % 2 == 0)
                                {
                                    listView3.Items[i - 1].SubItems[1].BackColor = Color.AliceBlue;
                                    listView3.Items[i - 1].SubItems[3].BackColor = Color.AliceBlue;
                                    listView3.Items[i - 1].SubItems[2].BackColor = Color.Linen;
                                    listView3.Items[i - 1].SubItems[4].BackColor = Color.Linen;
                                }
                                else
                                {
                                    listView3.Items[i - 1].SubItems[1].BackColor = Color.LightCyan;
                                    listView3.Items[i - 1].SubItems[3].BackColor = Color.LightCyan;
                                    listView3.Items[i - 1].SubItems[2].BackColor = Color.LightYellow;
                                    listView3.Items[i - 1].SubItems[4].BackColor = Color.LightYellow;
                                }

                                listView3.Items[i - 1].SubItems[1].ForeColor = Color.Black;
                                listView3.Items[i - 1].SubItems[3].ForeColor = Color.Black;
                                listView3.Items[i - 1].SubItems[2].ForeColor = Color.Black;
                                listView3.Items[i - 1].SubItems[4].ForeColor = Color.Black;

                            }
                            //// 판토 접기 
                            button4_Click(null, null);

                        }
                        if (i == 4 && listView3.Items[i - 1].Checked) //최저하강 공압
                        {
                            listView3.Items[i - 1].SubItems[1].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[3].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[2].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[4].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.White;
                            Thread.Sleep(3000);

                            if (plc.ReadBit("165") != "01")
                            {
                                button3_Click_1(null, null);

                            }


                            if (plc.ReadBit("166") != "01")
                            {
                                plc.WriteWord("102", "4000");//3.9바kpa 넣기 

                                plc.WriteBit("18", "1"); // 입력 sol on
                                Thread.Sleep(1000);


                                for (int k = 0; k < 100; k++)
                                {
                                    int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                                    if (a >= 3900)
                                    {
                                        //plc.WriteBit("18", "0"); //입력 sol off
                                        break;
                                    }
                                    Thread.Sleep(1000);
                                }

                                plc.WriteBit("20", "1"); // 공급 sol on
                                
                                
                                Thread.Sleep(2000);

                                for (int k = 0; k < 100; k++)
                                {
                                    if (plc.ReadBit("166") == "01")
                                    {
                                        Thread.Sleep(5000);

                                        listView3.Items[i - 1].SubItems[3].Text = _min_Air + "kPa";

                                        listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;

                                        break;
                                    }

                                    else if (plc.ReadBit("165") == "01")
                                    {
                                        listView3.Items[i - 1].SubItems[3].Text = "최저동작 이상";
                                        listView3.Items[i - 1].SubItems[4].Text = Constant.NG;
                                        break;
                                    }

                                    Thread.Sleep(1000);
                                }
                                plc.WriteBit("18", "0"); // 입력 sol on
                                plc.WriteBit("20", "0"); // 공급 sol on

                            }
                            if ((i - 1) % 2 == 0)
                            {
                                listView3.Items[i - 1].SubItems[1].BackColor = Color.AliceBlue;
                                listView3.Items[i - 1].SubItems[3].BackColor = Color.AliceBlue;
                                listView3.Items[i - 1].SubItems[2].BackColor = Color.Linen;
                                listView3.Items[i - 1].SubItems[4].BackColor = Color.Linen;
                            }
                            else
                            {
                                listView3.Items[i - 1].SubItems[1].BackColor = Color.LightCyan;
                                listView3.Items[i - 1].SubItems[3].BackColor = Color.LightCyan;
                                listView3.Items[i - 1].SubItems[2].BackColor = Color.LightYellow;
                                listView3.Items[i - 1].SubItems[4].BackColor = Color.LightYellow;
                            }

                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.Black;

                        }
                        if (i == 5 && listView3.Items[i - 1].Checked) //공기누설시험
                        {
                            listView3.Items[i - 1].SubItems[1].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[3].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[2].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[4].BackColor = Color.Green;
                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.White;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.White;
                           
                            Thread.Sleep(3000);

                            int first_press = 0;
                            int second_press = 0;

                            int diffrent_press = 0;

                            // 시험 압력 8.8kpa


                            plc.WriteWord("102", "9050");//8.8kpa 넣기 

                            plc.WriteBit("18", "1"); // 입력 sol on
                            Thread.Sleep(1000);
                            Thread.Sleep(1000);

                            plc.WriteBit("19", "1"); // 입력 sol on

                            Thread.Sleep(2000);


                            plc.WriteBit("19", "0"); // 입력 sol on


                            plc.WriteBit("20", "1"); // 입력 sol on


                            for (int k = 0; k < 100; k++)
                            {
                                int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                                if (a >= 8800)
                                {
                                    plc.WriteBit("18", "0"); //입력 sol off
                                    break;
                                }
                                Thread.Sleep(1000);
                            }

                            Thread.Sleep(1000);

                            first_press = Convert.ToInt32(plc.ReadWord("101"), 16);


                            for (int k = 0; k < (60*float.Parse(comboBox1.Text)); k++)
                            {
                                listView3.Items[i - 1].SubItems[3].Text = (k + 1).ToString() + " sec";

                                Thread.Sleep(1000);
                            }

                            second_press = Convert.ToInt32(plc.ReadWord("101"), 16);

                            diffrent_press =  Math.Abs(first_press- second_press);

                                                                                    
                            Thread.Sleep(1000);

                            listView3.Items[i - 1].SubItems[3].Text = (diffrent_press/1000).ToString("F2") + "kPa";

                            if ((diffrent_press/1000) <= float.Parse(_leakage))
                            {
                                listView3.Items[i - 1].SubItems[4].Text = Constant.GOOD;
                            }
                            else
                                listView3.Items[i - 1].SubItems[4].Text = Constant.NG;


                            //배기

                            plc.WriteWord("102", "0");//3.9바kpa 넣기 


                            plc.WriteBit("20", "1"); // 입력 sol on
                            plc.WriteBit("18", "1"); // 입력 sol on
                            plc.WriteBit("19", "1"); // 입력 sol on
                            Thread.Sleep(1000);

                            for (int k = 0; k < 100; k++)
                            {
                                int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                                if (a <= 100)
                                {

                                    Thread.Sleep(5000);

                                    Thread.Sleep(5000);

                                    plc.WriteBit("20", "0"); // 입력 sol off
                                    plc.WriteBit("18", "0"); //입력 sol off
                                    plc.WriteBit("19", "0"); // 입력 sol on

                                    break;

                                }
                                Thread.Sleep(1000);
                            }

                            if ((i - 1) % 2 == 0)
                            {
                                listView3.Items[i - 1].SubItems[1].BackColor = Color.AliceBlue;
                                listView3.Items[i - 1].SubItems[3].BackColor = Color.AliceBlue;
                                listView3.Items[i - 1].SubItems[2].BackColor = Color.Linen;
                                listView3.Items[i - 1].SubItems[4].BackColor = Color.Linen;
                            }
                            else
                            {
                                listView3.Items[i - 1].SubItems[1].BackColor = Color.LightCyan;
                                listView3.Items[i - 1].SubItems[3].BackColor = Color.LightCyan;
                                listView3.Items[i - 1].SubItems[2].BackColor = Color.LightYellow;
                                listView3.Items[i - 1].SubItems[4].BackColor = Color.LightYellow;
                            }

                            listView3.Items[i - 1].SubItems[1].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[3].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[2].ForeColor = Color.Black;
                            listView3.Items[i - 1].SubItems[4].ForeColor = Color.Black;

                        }
                    }
                    plc.WriteBit("8", "0");
                    MessageBox.Show("TEST Complete");
                }
                else
                {
                    MessageBox.Show("This is manual mode.  Please change to automatic mode.");
                
                }

                //for (int i = 0; i < LV.Length; i++)
                //{
                //    for (int j = 0; j < LV[i].Items.Count; j++)
                //    {
                       

                //    }
                //}

             
            }
            catch(Exception err)
            {

                plc.WriteBit("8", "0");

                //#region LV Color Reset
                //for (int i = 0; i < LV.Length; i++)
                //{
                //    for (int j = 0; j < LV[i].Items.Count; j++)
                //    {
                //        if (j % 2 == 0)
                //        {
                //            LV[i].Items[j].SubItems[1].BackColor = Color.AliceBlue;
                //            LV[i].Items[j].SubItems[3].BackColor = Color.AliceBlue;
                //            LV[i].Items[j].SubItems[2].BackColor = Color.Linen;
                //            LV[i].Items[j].SubItems[4].BackColor = Color.Linen;
                //        }
                //        else
                //        {
                //            LV[i].Items[j].SubItems[1].BackColor = Color.LightCyan;
                //            LV[i].Items[j].SubItems[3].BackColor = Color.LightCyan;
                //            LV[i].Items[j].SubItems[2].BackColor = Color.LightYellow;
                //            LV[i].Items[j].SubItems[4].BackColor = Color.LightYellow;
                //        }
                //    }
                //}
                //#endregion

                plc.WriteBit("8", "0");
                
                MessageBox.Show("시험이 비정상적으로 종료되었습니다.");

                MessageBox.Show(err.Message);

                BTN_Enable(0); // 0은 평상 시
            }
            BTN_Enable(0); // 0은 평상 시
        }
        private void STOP_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (thRun != null && thRun.IsAlive)
                {
                    try
                    {
                        thRun.Abort();

                        plc.WriteBit("26", "0"); //본체기동

                    }
                    catch
                    {

                    }
                }
                BTN_Enable(0); // 0은 평상 시
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
                //plc.WriteBit("8", "0");
                button3.Enabled = true;
                button4.Enabled = true;

            } // 평상 시
            else
            {
                START_BTN.Enabled = false;
                STOP_BTN.Enabled = true;
                SAVE_BTN.Enabled = false;
                RESET_BTN.Enabled = false;
                EXIT_BTN.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                // plc.WriteBit("8", "1");
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

                    if (Setting.Equipment[i] == "Loadcell")
                    {
                        loadcell = new AND_AD310D(Port[i]);
                        loadcell.Open();

                        loadcell.SendData("MT");
                    }

                    if (Setting.Equipment[i] == "plc")
                    {
                        plc = new PLCEnet();
                    }

                    if (Setting.Equipment[i] == "power")
                    {
                        dc_power =new DSP_LAN(Setting.dsp_ip);
                        dc_power.Portopen();
                    }

                    //if (Setting.Equipment[i] == "timer")
                    //{
                    //    timercount = new MT4YMOD(Port[i],1);
                    //    timercount.Open();
                    //}
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

                    _raising_min_time = init_info.tb_raimin.Text.ToString();
                    _raising_max_time = init_info.tb_raimax.Text.ToString();
                    _lowing_max_time = init_info.tb_lowmax.Text.ToString();
                    _lowing_min_time   =init_info.tb_lowmin.Text.ToString();
                    _diff_press = init_info.tb_diff.Text.ToString();
                    _up_press = init_info.tb_uplimit.Text.ToString();
                    _down_press = init_info.tb_lowlimit.Text.ToString();
                    _min_volt = init_info.tb_Volt.Text.ToString();
                    _min_Air = init_info.tb_Air.Text.ToString();
                    _leakage = init_info.tb_leak.Text.ToString();


                    f_name = _date_time + "_" + _pyunsung_name + "_" + _car_name + "_" + _serial_name + "_" + _type_of_test + "_" + _tester_name + "_.dat";
                }

            }
            catch
            {

            }

   
            string[] method = new string[3];
            method[0] = _up_press+ "N less";
            method[1] = _down_press + "N more";
            method[2] = "Within " + _diff_press;


            string[] method_operating = new string[5];
            method_operating[0] = _raising_min_time + " ~ " +_raising_max_time + " sec" ;
            method_operating[1] = _lowing_min_time + " ~ " + _lowing_max_time + " sec";
            method_operating[2] = _min_volt+ "V Less";
            method_operating[3] = _min_Air + "kPa Less";
            method_operating[4] =  _leakage + "kPa Less";


            #region 리스트뷰 정렬
            for (int i = 0; i < Setting.ListName.Length; i++)
            {
                listView3.Items.Add("");
                listView3.Items[i].SubItems.Add(Setting.ListName[i]);
                listView3.Items[i].SubItems.Add(method_operating[i]);
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
                listView1.Items[i].SubItems.Add(method[i]);//1
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
                // listView1.Items[i].SubItems.Add("");//21
                //listView1.Items[i].SubItems.Add("");//22
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
                    //listView1.Items[i].SubItems[21].BackColor = Color.Linen;
                    //listView1.Items[i].SubItems[22].BackColor = Color.AliceBlue;
                
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
                    //listView1.Items[i].SubItems[21].BackColor = Color.LightCyan;
                    //listView1.Items[i].SubItems[22].BackColor = Color.White;
                }
               // listView5.Items[i].Checked = true;

            }



            #endregion




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


            for (int i = TabControl1.TabCount - 1; i >= 0; i--)
            {
                TabControl1.SelectedIndex = i;
            }

            
            label4.Text = "-";

            chart1.Series[0].Points.AddXY(0, 0);

            if (plc.ReadBit("163") == "01")
            {
                plc.WriteBit("21", "1");

                plc.WriteBit("14", "1");
            }
            for (int i =300;i<2379;i=i+15)
            {
                chart1.Series[1].Points.AddXY(i, 93);
                chart1.Series[2].Points.AddXY(i, 44);
            }

            for (float i = 44; i < 94; i = i+3)
            {
                chart1.Series[3].Points.AddXY(300,  i);
                chart1.Series[4].Points.AddXY(2378, i);
            }

            thRun2 = new Thread(TestExecute2);
            thRun2.Start();

            //thRun3 = new Thread(hei);
            //thRun3.Start();

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


                _up_press = odt.Inputs["lift_data_0"];
                _down_press = odt.Inputs["lift_data_1"];
                _diff_press = odt.Inputs["lift_data_2"];

                _raising_min_time = odt.Inputs["rai_min_data"];
                _raising_max_time = odt.Inputs["rai_max_data"];
                _lowing_max_time = odt.Inputs["low_max_data"];
                _lowing_min_time = odt.Inputs["low_min_data"];
                
                _min_volt = odt.Inputs["volt_data"];
                _min_Air = odt.Inputs["press_data"];
                _leakage = odt.Inputs["leak_data"];



                int length = odt.Inputs.Count;

                try
                {
                    for (int i = 0; i < 18; i++)
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
                 chart1.Visible = false;
                 panel2.BackgroundImage = Image.FromFile(@"D:\Chart\" + _date_time + "_panto_chart.jpg");
            }
            else
            {
                //panel9.Visible = true;
                //panel_pwseq.Visible = true;
                chart1.Visible = true;
            }
        }

    


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


                odt.Inputs["lift_data_0"] = _up_press ;
                odt.Inputs["lift_data_1"]= _down_press;
                odt.Inputs["lift_data_2"] = _diff_press;
                odt.Inputs["lift_data_0_1"] = _up_press;
                odt.Inputs["lift_data_1_1"] = _down_press;
                odt.Inputs["lift_data_2_1"] = _diff_press;
                odt.Inputs["rai_min_data"] = _raising_min_time;
                odt.Inputs["rai_max_data"] = _raising_max_time;
                odt.Inputs["low_max_data"] = _lowing_max_time;
                odt.Inputs["low_min_data"] = _lowing_min_time;
                odt.Inputs["volt_data"] = _min_volt;
                odt.Inputs["press_data"] = _min_Air;
                odt.Inputs["leak_data"] = _leakage;


                try
                {

                    for (int i = 0; i <18; i++)
                    {
                        odt.Inputs["udata_" + i.ToString()] =listView1.Items[0].SubItems[2+i].Text;
                        odt.Inputs["ldata_" + i .ToString()] = listView1.Items[1].SubItems[2 + i].Text;
                        odt.Inputs["fdata_" + i .ToString()] = listView1.Items[2].SubItems[2 + i].Text;
                    }

                    for (int i = 0; i <3; i++)
                    {
                        odt.Inputs["result_" + i.ToString()] = listView1.Items[ i].SubItems[20].Text;
                        odt.Inputs["result_" + i.ToString() + "_1"] = listView1.Items[ i].SubItems[20].Text;
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GoZero_one();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                for (int i = 0; i < Setting.ListName.Length; i++)
                {
                    listView3.Items[i].Checked = true;

                }
            }
            else
            {
                for (int i = 0; i < Setting.ListName.Length; i++)
                {
                    listView3.Items[i].Checked = false;

                }

            }

        }

        public void listview1_test_start(int i)
        {

            listView1.Items[i].SubItems[2].BackColor = Color.Green;
            listView1.Items[i].SubItems[3].BackColor = Color.Green;
            listView1.Items[i].SubItems[4].BackColor = Color.Green;
            listView1.Items[i].SubItems[5].BackColor = Color.Green;
            listView1.Items[i].SubItems[6].BackColor = Color.Green;
            listView1.Items[i].SubItems[7].BackColor = Color.Green;
            listView1.Items[i].SubItems[8].BackColor = Color.Green;
            listView1.Items[i].SubItems[9].BackColor = Color.Green;
            listView1.Items[i].SubItems[10].BackColor = Color.Green;
            listView1.Items[i].SubItems[11].BackColor = Color.Green;
            listView1.Items[i].SubItems[12].BackColor = Color.Green;
            listView1.Items[i].SubItems[13].BackColor = Color.Green;
            listView1.Items[i].SubItems[14].BackColor = Color.Green;
            listView1.Items[i].SubItems[15].BackColor = Color.Green;
            listView1.Items[i].SubItems[16].BackColor = Color.Green;
            listView1.Items[i].SubItems[17].BackColor = Color.Green;
            listView1.Items[i].SubItems[18].BackColor = Color.Green;
            listView1.Items[i].SubItems[19].BackColor = Color.Green;
            listView1.Items[i].SubItems[20].BackColor = Color.Green;
            //listView1.Items[i].SubItems[21].BackColor = Color.Green;
            //listView1.Items[i].SubItems[22].BackColor = Color.Green;



            listView1.Items[i].SubItems[2].ForeColor = Color.White;
            listView1.Items[i].SubItems[3].ForeColor = Color.White;
            listView1.Items[i].SubItems[4].ForeColor = Color.White;
            listView1.Items[i].SubItems[5].ForeColor = Color.White;
            listView1.Items[i].SubItems[6].ForeColor = Color.White;
            listView1.Items[i].SubItems[7].ForeColor = Color.White;
            listView1.Items[i].SubItems[8].ForeColor = Color.White;
            listView1.Items[i].SubItems[9].ForeColor = Color.White;
            listView1.Items[i].SubItems[10].ForeColor = Color.White;
            listView1.Items[i].SubItems[11].ForeColor = Color.White;
            listView1.Items[i].SubItems[12].ForeColor = Color.White;
            listView1.Items[i].SubItems[13].ForeColor = Color.White;
            listView1.Items[i].SubItems[14].ForeColor = Color.White;
            listView1.Items[i].SubItems[15].ForeColor = Color.White;
            listView1.Items[i].SubItems[16].ForeColor = Color.White;
            listView1.Items[i].SubItems[17].ForeColor = Color.White;
            listView1.Items[i].SubItems[18].ForeColor = Color.White;
            listView1.Items[i].SubItems[19].ForeColor = Color.White;
            listView1.Items[i].SubItems[20].ForeColor = Color.White;
            //listView1.Items[i].SubItems[21].ForeColor = Color.White;
            //listView1.Items[i].SubItems[22].ForeColor = Color.White;


        }

        public void listview1_test_stop(int i)
        {

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
                //listView1.Items[i].SubItems[21].BackColor = Color.Linen;
                //listView1.Items[i].SubItems[22].BackColor = Color.AliceBlue;

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
                //listView1.Items[i].SubItems[21].BackColor = Color.LightCyan;
                //listView1.Items[i].SubItems[22].BackColor = Color.White;
            }


            listView1.Items[i].SubItems[2].ForeColor = Color.Black;
            listView1.Items[i].SubItems[3].ForeColor = Color.Black;
            listView1.Items[i].SubItems[4].ForeColor = Color.Black;
            listView1.Items[i].SubItems[5].ForeColor = Color.Black;
            listView1.Items[i].SubItems[6].ForeColor = Color.Black;
            listView1.Items[i].SubItems[7].ForeColor = Color.Black;
            listView1.Items[i].SubItems[8].ForeColor = Color.Black;
            listView1.Items[i].SubItems[9].ForeColor = Color.Black;
            listView1.Items[i].SubItems[10].ForeColor = Color.Black;
            listView1.Items[i].SubItems[11].ForeColor = Color.Black;
            listView1.Items[i].SubItems[12].ForeColor = Color.Black;
            listView1.Items[i].SubItems[13].ForeColor = Color.Black;
            listView1.Items[i].SubItems[14].ForeColor = Color.Black;
            listView1.Items[i].SubItems[15].ForeColor = Color.Black;
            listView1.Items[i].SubItems[16].ForeColor = Color.Black;
            listView1.Items[i].SubItems[17].ForeColor = Color.Black;
            listView1.Items[i].SubItems[18].ForeColor = Color.Black;
            listView1.Items[i].SubItems[19].ForeColor = Color.Black;
            listView1.Items[i].SubItems[20].ForeColor = Color.Black;
            //listView1.Items[i].SubItems[21].ForeColor = Color.Black;
            //listView1.Items[i].SubItems[22].ForeColor = Color.Black;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;


            label4.Text = "panto up";

            button3.BackColor = Color.Green;
            button3.ForeColor = Color.White;

          

            if (plc.ReadBit("165") != "01")
            {
               
                //Air공급 배기 같이 투입


                plc.WriteWord("102", "0");//3.9바kpa 넣기 
                Thread.Sleep(1000);
                plc.WriteBit("18", "1"); // 입력 sol on
                Thread.Sleep(1000);
                for (int k = 0; k < 100; k++)
                {
                    int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                    if (a <= 100)
                    {
                        //plc.WriteBit("18", "0"); //입력 sol off
                        break;
                    }
                    Thread.Sleep(1000);
                }

                plc.WriteBit("19", "1"); // 배기sol on
                Thread.Sleep(200);
                plc.WriteBit("20", "1"); // 공급 sol on
                Thread.Sleep(200);


                Thread.Sleep(10000);


                dc_power.SetVoltage(100);
                dc_power.SetCurrent(2);
                dc_power.OutPut("ON");
                Thread.Sleep(1000);

                plc.WriteBit("17", "1"); // 100V 투입 릴레이
                Thread.Sleep(500);
                //plc.WriteBit("17", "0"); // 100V 투입 릴레이
                Thread.Sleep(300);
                //dc_power.OutPut("OFF");

                Thread.Sleep(1000);
                //판토 펼치기
                for (int k = 0; k < 100; k++)
                {
                    if (plc.ReadBit("165") == "01")
                    {
                        plc.WriteBit("18", "0"); //입력 sol off
                        plc.WriteBit("19", "0"); // 배기 sol off
                        Thread.Sleep(200);
                        plc.WriteBit("20", "0"); // 공급 sol off
                        Thread.Sleep(200);

                        break;
                    }
                    Thread.Sleep(1000);
                }
                Thread.Sleep(300);
          
            }

            button3.BackColor = Color.Gainsboro;
            
            button3.ForeColor = Color.Black;
            
            label4.Text = "-";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label4.Text = "panto down";
           
            button4.BackColor = Color.Green;
            
            button4.ForeColor = Color.White;

           

            if (plc.ReadBit("166") != "01")
            {

                Thread.Sleep(300);
                plc.WriteWord("102", "7000");//3.9바kpa 넣기 
                Thread.Sleep(1000);
                plc.WriteBit("18", "1"); // 입력 sol on
                Thread.Sleep(1000);
                for (int k = 0; k < 100; k++)
                {
                    int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                    if (a >= 6700)
                    {
                        //plc.WriteBit("18", "0"); //입력 sol off
                        break;
                    }
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
                plc.WriteBit("20", "1"); // 공급 sol on

                for (int k = 0; k < 100; k++)
                {
                    if (plc.ReadBit("166") == "01")
                    {
                        plc.WriteBit("20", "0"); // 공급 sol on
                        break;
                    }
                    Thread.Sleep(1000);
                }


                plc.WriteWord("102", "0");//3.9바kpa 넣기 

                plc.WriteBit("18", "1"); // 입력 sol on
                plc.WriteBit("19", "1"); // 입력 sol on
                Thread.Sleep(1000);
                for (int k = 0; k < 100; k++)
                {
                    int a = Convert.ToInt32(plc.ReadWord("101"), 16);

                    if (a <= 100)
                    {

                        Thread.Sleep(5000);

                        Thread.Sleep(5000);
                        plc.WriteBit("18", "0"); //입력 sol off
                        plc.WriteBit("19", "0"); // 입력 sol on

                        break;
                      
                    }
                    Thread.Sleep(1000);
                }

                
                   
                
            }
            Thread.Sleep(300);
            button4.BackColor = Color.Gainsboro;
            Thread.Sleep(300);
            button4.ForeColor = Color.Black;


            label4.Text = "-";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                checkBox4.Checked = true;
                checkBox3.Checked = true;
            }
            else
            {
                checkBox4.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if(thRun != null)
                {
                    thRun.Abort();


                }
            }
            catch
            { 
            
            }
            try
            {
                if (thRun2 != null)
                {
                    thRun2.Abort();


                }
            }
            catch
            {

            }
            try
            {
                loadcell.Close();
               
            }
            catch
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            list_set_zero();
        }

        public void list_set_zero()
        {



            #region 리스트뷰 정렬
            for (int i = 0; i < Setting.ListName.Length; i++)
            {
                listView3.Items[i].SubItems[3].Text = "";
                listView3.Items[i].SubItems[4].Text = "";
            }

            for (int i = 0; i < Setting.ListName3.Length; i++)
            {
                // listView1.Items[i].SubItems[1].BackColor = Color.AliceBlue;
                for (int j = 2; j < 23; j++)
                {
                    listView1.Items[i].SubItems[j].Text = "";
                }
          

            }



            #endregion





        }



    }




}
