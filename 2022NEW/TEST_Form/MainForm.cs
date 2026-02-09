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

using _2022_Test.NSTEK.device;
using _2022_Test.NSTEK.Plot;
using System.Reflection.Emit;
using System.Deployment.Application;





namespace _2022_Test
{
    public partial class MainForm : Form
    {
        CScanPlot plot_cscan;

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

            plot_cscan = new CScanPlot(panel_plotCScan);
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
                                double load = Convert.ToDouble(tempAry2[0]);
                                double N = load * 9.8f;
                               
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
                                double b = Convert.ToDouble(tempAry2[0]);
                                double M = b * 9.8f;
                               
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
                list_set_zero();

                thRun = new Thread(TestExecute_Test);
                thRun.Start();
            }
            catch
            { 
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



                  

                    // comp 전원 on 
                   
                    MessageBox.Show("Press the compressor ON button on the back of the tester for 3 seconds.");

                    Thread.Sleep(3000);

             
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
                
                MessageBox.Show("Test Not Completed Please Check the Mechine.");

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
           

            } // 평상 시
            else
            {
                START_BTN.Enabled = false;
                STOP_BTN.Enabled = true;
                SAVE_BTN.Enabled = false;
                RESET_BTN.Enabled = false;
                EXIT_BTN.Enabled = false;
               
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

           
            //test_check_arr = new CheckBox[] { pau_check, cob_check, check, Emergency_Check, amp_Check };

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
                    MessageBox.Show("Please Setting Report Directory.");

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
                    try
                    {
                        if (Setting.Equipment[i] == "Loadcell")
                        {
                            loadcell = new AND_AD310D(Port[i]);
                            loadcell.Open();

                            loadcell.SendData("MT");
                        }
                    }
                    catch
                    { 
                    }
                    try
                    {
                        if (Setting.Equipment[i] == "plc")
                        {
                            plc = new PLCEnet();
                        }
                    }
                    catch
                    { 
                    
                    }
                    try
                    {
                        if (Setting.Equipment[i] == "power")
                        {
                            dc_power = new DSP_LAN(Setting.dsp_ip);
                            dc_power.Portopen();
                        }
                    }
                    catch
                    { 
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

                    //_save_date = DateTime.Parse(init_info.tb_Date.Value.ToString()).ToString("yyyy-MM-dd");
                    //_date_time = DateTime.Parse(init_info.tb_Date.Value.ToString()).ToString("yyyyMMddHHmm");
                    //_pyunsung_name = init_info.tb_pyunsung.Text.ToString(); ;
                    //_car_name = init_info.tb_carnum.Text.ToString();
                    //_serial_name = init_info.tb_serial.Text.ToString();
                    //_type_of_test = init_info.tb_testlist.Text.ToString();
                    //_tester_name = init_info.tb_tester.Text.ToString();

                    //// _type = "Pantagraph";

                    //_raising_min_time = init_info.tb_raimin.Text.ToString();
                    //_raising_max_time = init_info.tb_raimax.Text.ToString();
                    //_lowing_max_time = init_info.tb_lowmax.Text.ToString();
                    //_lowing_min_time   =init_info.tb_lowmin.Text.ToString();
                    //_diff_press = init_info.tb_diff.Text.ToString();
                    //_up_press = init_info.tb_uplimit.Text.ToString();
                    //_down_press = init_info.tb_lowlimit.Text.ToString();
                    //_min_volt = init_info.tb_Volt.Text.ToString();
                    //_min_Air = init_info.tb_Air.Text.ToString();
                    //_leakage = init_info.tb_leak.Text.ToString();


                    //f_name = _date_time + "_" + _pyunsung_name + "_" + _car_name + "_" + _serial_name + "_" + _type_of_test + "_" + _tester_name + "_.dat";
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

           
            }

            else // 절연 관련
            {

            }

            //test_info.dateTimePicker1.Value = DateTime.ParseExact(date, "yyyyMMddHHmm", provider);
            //test_info.dateTimePicker2.Value = DateTime.ParseExact(date, "yyyyMMddHHmm", provider);

      
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

                odt = new Odt(Application.StartupPath + "\\" + "panto7.odt");



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
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox3.Checked == true)
            //{
            //    for (int i = 0; i < Setting.ListName.Length; i++)
            //    {
            //        listView3.Items[i].Checked = true;

            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < Setting.ListName.Length; i++)
            //    {
            //        listView3.Items[i].Checked = false;

            //    }

            //}

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
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

           

        }

        private void button4_Click(object sender, EventArgs e)
        {
          

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
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
          
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

        private void RESET_BTN_Click(object sender, EventArgs e)
        {
         
        }

        private void EXIT_BTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void START_BTN_Click(object sender, EventArgs e)
        {

        }

        public void list_set_zero()
        {

        }
    }
}
