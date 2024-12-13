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
using static Library.DeviceTools;

using PJH;
using Library;

namespace NSTEK
{
    public partial class PLC_Test_Form : Form
    {

        private ManualResetEvent _PLCEvent = new ManualResetEvent(true);

        //System.IO.Ports.SerialPort spCOM;
        private Thread thRun;
        Panel[] COM_LED, ST_LED,FA_LED;
     
        TextBox[] tx;
        public byte[] ReturnD = new byte[1024];
        static byte[] RevQbuffers = new byte[2048];
        static int RevCount_WPt, RevCount_RPt, ReadCount = 0;
        static byte bcc;
        static byte[] btData = new byte[1024];
        static int iLen = 0;



        PLCEnet_XGK plc_e;
        DSP150010HDLAN dsp_lan;
        Tonghui_AC_Source ac_source;
        KEYSIGHT33500 keysight33500;
        CI_PWM_BOARD pwm_board;
        Tonghui_Resistance resistance;
         VisaInstrumentApp_2 daq970a;
        Thread read_di;
        ENS_Encoder_MASCON notch_pwm;
        AFG2225 afg2225;

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

        private void EXIT_BTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckBox_Control(object sender, EventArgs e)
        {

            CheckBox cb = (CheckBox)sender;

            string[] cb_name = cb.Name.Split('_');

            if (cb.Checked == true)
            {
                plc_e.WriteBit_P_xgt(cb_name[1], "1");
            }
            else 
            {
                plc_e.WriteBit_P_xgt(cb_name[1], "0");
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ac_source.SetMEMMode();
            
            ac_source.SetVoltageAC(Convert.ToDouble(textBox1.Text));
            ac_source.SetCurrHigh();
            ac_source.OUTPUT_ON();
        }

        private void EXIT_BTN_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        int GetBit(byte x, int n)
        {
            return (x & (1 << n)) >> n;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dsp_lan.SetVoltage(Convert.ToDouble(textBox2.Text));
            dsp_lan.SetCurrent(Convert.ToDouble(textBox3.Text));
            dsp_lan.OutPut("ON");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            afg2225.AFGSET(1, true, 1000, 10, 0);
            Thread.Sleep(1000);
            afg2225.phase_set(1, 90);
            Thread.Sleep(1000);
            afg2225.AFGSET(2, true, 1000, 10, 0);
            Thread.Sleep(1000);
            afg2225.phase_set(2, 0);
            Thread.Sleep(1000);

            afg2225.Output1("ON");
            afg2225.Output2("ON");
            //keysight33500.SIN(Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox4.Text), 0);
            //keysight33500.SQU(Convert.ToInt32(textBox5.Text),Convert.ToInt32(textBox4.Text), 0, 50);
            //keysight33500.OutPutON(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            ac_source.OUTPUT_OFF();
        }

        private void PLC_Test_Form_FormClosing(object sender, FormClosingEventArgs e)
        {

            _PLCEvent.Reset();

            Thread.Sleep(1000);

            read_di.Abort();


            dsp_lan.Portclose();

            ac_source.Close();

            pwm_board.Close();
            //notch_pwm.PortClose();
            resistance.Close();
            afg2225.Close();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            keysight33500.OutPutOFF(1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string[] bt_name = bt.Name.Split('_');

            if (bt.BackColor == Color.Gainsboro)
            {

                string[] tb = new string[] {tb_200.Text,tb_201.Text,tb_202.Text, tb_203.Text, tb_300.Text,tb_301.Text,tb_302.Text,tb_400.Text,tb_401.Text,tb_403.Text }; 

                plc_e.WriteWord_D_xgt(bt_name[1], tb[bt.TabIndex]);

                plc_e.WriteBit_xgt(bt_name[1], "1");

                bt.BackColor = Color.FromArgb(42, 89, 162);

            }
            else
            {
                plc_e.WriteWord_D_xgt(bt_name[1], "0");
                plc_e.WriteBit_xgt(bt_name[1], "0");
                bt.BackColor = Color.Gainsboro;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            resistance.STOP();
            Thread.Sleep(1000);

            resistance.SetVoltage(Convert.ToInt32(textBox6.Text));
            resistance.ONOFF(1);
            resistance.BUS();

            Thread.Sleep(4000);

            string result = resistance.Result_Resistance();

            string[] temp = result.Split(',');


            if (Convert.ToDouble(temp[0]) / 1000000000000 >= 0)
            {

                label31.Text = (Convert.ToDouble(temp[0]) / 1000000000000).ToString() + "TΩ";

            }
            else if (Convert.ToDouble(temp[0]) / 1000000000000 < 0 && Convert.ToDouble(temp[0]) / 100000000000 >= 0)
            {
                label31.Text = (Convert.ToDouble(temp[0]) / 100000000000).ToString() + "GΩ";

            }
            else
            {
                label31.Text = (Convert.ToDouble(temp[0]) / 10000000000).ToString() + "MΩ";

            }
            //resistance.test_ONOFF();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            resistance.STOP();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CHECK_VOLT(textBox8.Text);
        }


      public string CHECK_VOLT(string addr)
        {
            daq970a.Query("*RST");
            string a;
            daq970a.Write("CONF:VOLT AUTO,DEF, (@" + addr + ")");
            Thread.Sleep(300);
            daq970a.Write("ROUT:MON (@" + addr + ")");
            Thread.Sleep(300);
            daq970a.Write("ROUT:MON:STAT ON");
            Thread.Sleep(1000);
            Thread.Sleep(1000);
            a = daq970a.Query("ROUT:MON:DATA?");

            return a;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            pwm_board.set_pwm_freq(400);
            pwm_board.set_pwm_duty(float.Parse(textBox16.Text));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pwm_board.set_pwm_freq(float.Parse(textBox9.Text));
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            plc_e.WriteWord_D_xgt("202", tb_202.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            plc_e.WriteWord_D_xgt("200", tb_200.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            plc_e.WriteWord_D_xgt("201", tb_201.Text);
        }

        byte ReadBit(byte x, int n)
        {

            return (byte)(x & (1 << n));

        }

        public PLC_Test_Form()
        {
            InitializeComponent();

            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }

    

        private void MainForm_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "data" + "\\" + "setup.ini");
            string temp = sr.ReadToEnd();
            sr.Close();
            string[] Port = temp.Split('!');
            for (int i = 0; i < Setting.Equipment.Length; i++)
            {
                if (Setting.Equipment[i] == "plc")
                {
                    plc_e = new PLCEnet_XGK();
                }
                if (Setting.Equipment[i] == "dc_power")
                {
                    dsp_lan = new DSP150010HDLAN();
                    dsp_lan.Portopen();
                }
                if (Setting.Equipment[i] == "DAQ970A")
                {
                    daq970a = new VisaInstrumentApp_2();

                }
                if (Setting.Equipment[i] == "KEYSIGHT33500")
                {
                    keysight33500 = new KEYSIGHT33500();
                    keysight33500.Portopen();
                }
                if (Setting.Equipment[i] == "AC_Source")
                {
                    ac_source = new Tonghui_AC_Source(Port[i]);
                    ac_source.Open();
                }
                if (Setting.Equipment[i] == "Resistance")
                {
                    resistance = new Tonghui_Resistance(Port[i]);
                    resistance.Open();
                }

                if (Setting.Equipment[i] == "pwm_board")
                {
                    pwm_board = new CI_PWM_BOARD(Port[i]);
                    pwm_board.Open();
                }
                if (Setting.Equipment[i] == "AFG-2225")
                {
                    afg2225= new AFG2225(Port[i]);
                    afg2225.Open();
                }
                //if (Setting.Equipment[i] == "pwm_board")
                //{
                //    notch_pwm = new ENS_Encoder_MASCON(Port[i]);
                //    notch_pwm.PortOpen();
                //}

            }

                for (int j = 0; j < Setting.ListName6.Length; j++)
            {
                listview_DI.Items.Add(Setting.ListName6[j]);
                listview_DI.Items[j].SubItems.Add(Setting.ListName7[j]);
                listview_DI.Items[j].SubItems.Add("");
                listview_DI.Items[j].SubItems.Add("");
               
            }


            read_di = new Thread(plc_Read);
            read_di.Start();

        }

        private void plc_Read()
        {

            CheckForIllegalCrossThreadCalls = false;


            while (_PLCEvent.WaitOne())
            {

                listview_DI.Items[0].SubItems[2].Text = plc_e.ReadBit_P_xgt("80");
                listview_DI.Items[1].SubItems[2].Text = plc_e.ReadBit_P_xgt("81");
                listview_DI.Items[2].SubItems[2].Text = plc_e.ReadBit_P_xgt("82");
                listview_DI.Items[3].SubItems[2].Text = plc_e.ReadBit_P_xgt("83");
                listview_DI.Items[4].SubItems[2].Text = plc_e.ReadBit_P_xgt("84");
                listview_DI.Items[5].SubItems[2].Text = plc_e.ReadBit_P_xgt("85");
                listview_DI.Items[6].SubItems[2].Text = plc_e.ReadBit_P_xgt("86");
                listview_DI.Items[7].SubItems[2].Text = plc_e.ReadBit_P_xgt("87");
                listview_DI.Items[8].SubItems[2].Text = plc_e.ReadBit_P_xgt("88");
                listview_DI.Items[9].SubItems[2].Text = plc_e.ReadBit_P_xgt("89");
                listview_DI.Items[10].SubItems[2].Text = plc_e.ReadBit_P_xgt("8A");

            }

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
