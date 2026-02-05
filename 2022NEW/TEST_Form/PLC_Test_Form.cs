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

//using PJH;
using Library;
using lucidio;
using Power_Modbus_RTU_SAMPLE;
using System.Runtime.InteropServices.ComTypes;
using _2022_Test.NSTEK.device;

using System.Reflection.Emit;

namespace _2022_Test
{
    public partial class PLC_Test_Form : Form
    {

        private ManualResetEvent _PLCEvent = new ManualResetEvent(true);

        //System.IO.Ports.SerialPort spCOM;
        private Thread thRun;
        Panel[] COM_LED, ST_LED, FA_LED;

        TextBox[] tx;
        public byte[] ReturnD = new byte[1024];
        static byte[] RevQbuffers = new byte[2048];
        static int RevCount_WPt, RevCount_RPt, ReadCount = 0;
        static byte bcc;
        static byte[] btData = new byte[1024];
        static int iLen = 0;

        DeviceTools.DAQ970A_LAN dmm;
        DeviceTools.DSOX1204G_LAN osc;
        DeviceTools.EX150_12_LAN ex150_lan;
    
        NetworkStream ns;
        PLCEnet plc;
        DSP_LAN dc_power;
        AND_AD310D loadcell;
        byte[] SerBuf = new byte[70];
       

        
        ValueDI1 v;

        Thread read_di;

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
                //plc_e.WriteBit_P_xgt(cb_name[1], "1");
            }
            else
            {
                //plc_e.WriteBit_P_xgt(cb_name[1], "0");
            }

        }

        private void AO_CheckBox_Control(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //ac_source.SetMEMMode();

            //ac_source.SetVoltageAC(Convert.ToDouble(textBox1.Text));
            //ac_source.SetCurrHigh();
            //ac_source.OUTPUT_ON();
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
            dc_power.SetVoltage(float.Parse(textBox2.Text));
            dc_power.OutPut("on");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadcell.SendData("MZ");
            
            
            //afg2225.AFGSET(1, true, 1000, 10, 0);
            //Thread.Sleep(1000);
            //afg2225.phase_set(1, 90);
            //Thread.Sleep(1000);
            //afg2225.AFGSET(2, true, 1000, 10, 0);
            //Thread.Sleep(1000);
            //afg2225.phase_set(2, 0);
            //Thread.Sleep(1000);

            //afg2225.Output1("ON");
            //afg2225.Output2("ON");
            //keysight33500.SIN(Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox4.Text), 0);
            //keysight33500.SQU(Convert.ToInt32(textBox5.Text),Convert.ToInt32(textBox4.Text), 0, 50);
            //keysight33500.OutPutON(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //ac_source.OUTPUT_OFF();
        }

        private void PLC_Test_Form_FormClosing(object sender, FormClosingEventArgs e)
        {


            Thread.Sleep(1000);

            if (read_di != null && read_di.IsAlive == true)
            {
                read_di.Abort();
            
            }

            loadcell.Close();



        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
           
        }
        public void set_voltage(int setting_volt)  // SHV300R voltage 설정
        {
        }
        public void set_current(int setting_current) //SHV 300R current 설정
        {
           

        }
        private void button6_Click(object sender, EventArgs e)
        {

            plc.WriteWord("102", textBox6.Text);
          
        }

        private void button8_Click(object sender, EventArgs e)
        {
         
        }

        private void button9_Click(object sender, EventArgs e)
        {
          
        }


        public string CHECK_VOLT(string addr)
        {
            string value = "";
            dmm.Check_Volt(addr);
            return value;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //pwm_board.set_pwm_freq(400);
            //pwm_board.set_pwm_duty(float.Parse(textBox16.Text));
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            // plc_e.WriteWord_D_xgt("202", tb_202.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // plc_e.WriteWord_D_xgt("200", tb_200.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // plc_e.WriteWord_D_xgt("201", tb_201.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void transmit(byte write_bytes)
        {
            try
            {
                ns.WriteTimeout = 100;
                ns.Write(SerBuf, 0, write_bytes);
                ns.Flush();
            }
            catch
            {
                MessageBox.Show("write fail");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
         

        }


        private void Board_AllOUT()
        {

          
        

        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        byte ReadBit(byte x, int n)
        {

            return (byte)(x & (1 << n));

        }

        private void button15_Click_1(object sender, EventArgs e)
        {
        

        }

        private void button22_Click(object sender, EventArgs e)
        {
            Board_AllOUT();
        }

        private void button23_Click(object sender, EventArgs e)
        {
          
        }

        private void button24_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dc_power.SetVoltage(0);
            dc_power.OutPut("OFF");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            loadcell.Zero_point();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            loadcell.SendData("MT");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            loadcell.SendData("CT");
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            loadcell.SendData("MN");
           string sres =  loadcell.ReadValue();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("202", "1");
            button1.BackColor = Color.Green;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("202", "0");
            button1.BackColor = SystemColors.Control;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("203", "1");
            button3.BackColor = Color.Green;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("203", "0");
            button3.BackColor = SystemColors.Control;
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("212", "1");
            button7.BackColor = Color.Green;
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("212", "0");
            button7.BackColor = SystemColors.Control;
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("213", "1");
            button4.BackColor = Color.Green;
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("213", "0");
            button4.BackColor = SystemColors.Control;
        }

        private void button19_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("542", "1");
            button19.BackColor = Color.Green;
        }

        private void button19_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("542", "0");
            button19.BackColor = SystemColors.Control;
        }

        private void button18_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("543", "1");
            button18.BackColor = Color.Green;
        }

        private void button18_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("543", "0");
            button18.BackColor = SystemColors.Control;
        }

        private void button17_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("552", "1");
            button17.BackColor = Color.Green;
        }

        private void button17_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("552", "0");
            button17.BackColor = SystemColors.Control;
        }

        private void button16_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("553", "1");
            button16.BackColor = Color.Green;
        }

        private void button16_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("553", "0");
            button16.BackColor = SystemColors.Control;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                plc.WriteBit("107", "1");
                plc.WriteBit("108", "0");
            }
            else
            {
                plc.WriteBit("108", "1");
                plc.WriteBit("107", "0");

            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                plc.WriteBit("123", "1");
                plc.WriteBit("124", "0");
            }
            else
            {
                plc.WriteBit("124", "1");
                plc.WriteBit("123", "0");

            }
        }

        private void button11_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1202", "1");
            button11.BackColor = Color.Green;
        }

        private void button11_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1202", "0");
            button11.BackColor = SystemColors.Control;
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1203", "1");
            button10.BackColor = Color.Green;
        }

        private void button10_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1203", "0");
            button10.BackColor = SystemColors.Control;
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1212", "1");
            button9.BackColor = Color.Green;
        }

        private void button9_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1212", "0");
            button9.BackColor = SystemColors.Control;
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {

            plc.WriteBit("1213", "1");
            button8.BackColor = Color.Green;
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1213", "0");
            button8.BackColor = SystemColors.Control;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                plc.WriteBit("109", "1");
                plc.WriteBit("110", "0");
            }
            else
            {
                plc.WriteBit("110", "1");
                plc.WriteBit("109", "0");

            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton12.Checked)
            {
                plc.WriteBit("125", "1");
                plc.WriteBit("126", "0");
            }
            else
            {
                plc.WriteBit("125", "0");
                plc.WriteBit("126", "1");
            
            }
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton14.Checked)
            {
                plc.WriteBit("42", "1");
                plc.WriteBit("43", "0");
            }
            else
            {
                plc.WriteBit("42", "0");
                plc.WriteBit("43", "1");

            }
        }

        private void button15_MouseDown(object sender, MouseEventArgs e)
        {

            plc.WriteBit("1542", "1");
            button15.BackColor = Color.Green;
        }

        private void button15_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1542", "0");
            button15.BackColor = SystemColors.Control;
        }

        private void button14_MouseDown(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1543", "1");
            button14.BackColor = Color.Green;
        }

        private void button14_MouseUp(object sender, MouseEventArgs e)
        {
            plc.WriteBit("1543", "0");
            button14.BackColor = SystemColors.Control;
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
                    plc = new PLCEnet();
                }

                //if (Setting.Equipment[i] == "power")
                //{
                //    dc_power = new DSP_LAN(Setting.dsp_ip);
                //    dc_power.Portopen();
                //}
                //if (Setting.Equipment[i] == "Loadcell")
                //{
                //    loadcell = new AND_AD310D(Port[i]);
                //    loadcell.Open();
                //}                                       
            }


            //eth_8020_1 = new ETH_8020("192.168.0.201");
            //eth_8020_2 = new ETH_8020_2("192.168.0.202");
            //ds_relay1 = new ds2824("192.168.0.123");





            //read_di = new Thread(plc_Read);
            //read_di.Start();

        }

        private void plc_Read()
        {

            CheckForIllegalCrossThreadCalls = false;

            //string manual = plc.ReadBit("160");
            //string auto = plc.ReadBit("161");
            //string EMG = plc.ReadBit("162");
            //string servo_mc = plc.ReadBit("163");

            try
            {
                while (true)
                {
                    string temp = loadcell.ReadValue();

                    if (temp.IndexOf('+') > 0)
                    {
                        string[] tempAry = temp.Split('+');
                        string[] tempAry2 = tempAry[1].Split('k');
                        try
                        {
                            float a = float.Parse(tempAry2[0]);
                            float N = a * 9.8f;
                           
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
                            
                        }
                        catch
                        {
                        }
                    }

                }
            }
            catch
            {
                // MessageBox.Show("M");
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


        private void plc_ry_Checkedchanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            string[] cb_name = cb.Name.Split('_');

            byte[] stream = new byte[7];


            if (cb.Checked != true)
               plc.WriteBit(int.Parse(cb_name[1]).ToString(), "0");
            else
                plc.WriteBit(int.Parse(cb_name[1]).ToString(), "1");

        }
    }
}
