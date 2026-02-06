using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using System.IO;
using System.Threading;
using _2022_Test.NSTEK.device;
using Power_Modbus_RTU_SAMPLE;
using static Library.DeviceTools;

namespace _2022_Test
{
    public partial class SelfTestForm : Form
    {
        Thread thRun;
      
        //DeviceTools.DAQ970A_LAN dmm;
        //DeviceTools.DSOX1204G_LAN scope;

        VisaInstrumentApp_2 dmm;
        VisaInstrumentApp_1 scope;

        PLCEnet plc;
        AND_AD310D loadcell;
        DSP_LAN dc_power;
        MT4YMOD timercount;

 
        StartForm sf;
       

     

        public SelfTestForm(StartForm form)
        {
            InitializeComponent();
            sf = form;
        }

        private void Exit_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            sf.Opacity = 1;
            this.Close();
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Control ctrl in Serial_GroupBox.Controls)
                ctrl.BackColor = Color.LemonChiffon;
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Control ctrl in Serial_GroupBox.Controls)
                ctrl.BackColor = Color.Gainsboro;
        }

        private void plc_btn_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            if(btn.BackColor == Color.LemonChiffon)
                btn.BackColor = Color.Gainsboro;
            else
                btn.BackColor = Color.LemonChiffon;
        }

        private void testexecute()
        {
            CheckForIllegalCrossThreadCalls = false;
            try
            {

                if (File.Exists(@"data\setup.ini"))
                {

                    StreamReader sr = new StreamReader(@"data\setup.ini");
                    string temp = sr.ReadToEnd();
                    sr.Close();
                    string[] Port = temp.Split('!');
                     bool[] PAN = new bool[Setting.Equipment.Length];


                    for (int i = 0; i < Setting.Equipment.Length; i++)
                    {

                        if (Setting.Equipment[i] == "focusPX"&& focuspx_btn.BackColor == Color.LemonChiffon)
                        {
                            if (Port[i] != "NONE")
                            {
                                try
                                {
                                    plc = new PLCEnet();
                                    string pan = plc.ReadBit("0");
                                    if (pan == "00" || pan == "01")
                                    {
                                        PAN[i] = true;
                                        plc_btn.BackColor = Color.GreenYellow;
                                    }
                                    else
                                    {
                                        PAN[i] = false;
                                        plc_btn.BackColor = Color.LightCoral;
                                    }
                                }
                                catch
                                {
                                    PAN[i] = false;
                                    plc_btn.BackColor = Color.LightCoral;

                                }
                            }
                            else
                                focuspx_btn.BackColor = Color.LightCoral;

                        } // 직류전원장치


                        if (Setting.Equipment[i] == "plc" && plc_btn.BackColor == Color.LemonChiffon)
                        {
                            try
                            {
                                plc = new PLCEnet();
                                string pan = plc.ReadBit("0");
                                if (pan == "00" || pan == "01")
                                {
                                    PAN[i] = true;
                                    plc_btn.BackColor = Color.GreenYellow;
                                }
                                else
                                {
                                    PAN[i] = false;
                                    plc_btn.BackColor = Color.LightCoral;
                                }
                            }
                            catch
                            {
                                PAN[i] = false;
                                plc_btn.BackColor = Color.LightCoral;

                            }
                        }
                     
                        if (Setting.Equipment[i] == "power" && power_btn.BackColor == Color.LemonChiffon)
                        {
                            try
                            {
                                bool p_result;
                                dc_power = new DSP_LAN(Setting.dsp_ip);

                                p_result = dc_power.PING();

                                if (p_result)
                                {
                                    string pan = dc_power.SelfTest();
                                  
                                    Thread.Sleep(300);
                                  
                                    if (pan.IndexOf("DSP") >= 0)
                                    {
                                        PAN[i] = true;
                                        power_btn.BackColor = Color.GreenYellow;
                                    }
                                    else
                                    {
                                        PAN[i] = false;
                                        power_btn.BackColor = Color.LightCoral;
                                    }

                                    Thread.Sleep(300);
                                    dc_power.Portclose();
                                }
                                else
                                {
                                    PAN[i] = false;
                                    power_btn.BackColor = Color.LightCoral;
                                }
                            }
                            catch
                            {
                                PAN[i] = false;
                                power_btn.BackColor = Color.LightCoral;
                                dc_power.Portclose();
                            }
                        } // 직류전원장치

/*
                        if (Setting.Equipment[i] == "timer" && Timer_btn.BackColor == Color.LemonChiffon)
                        {
                            try
                            {
                                bool p_result;
                                timercount = new MT4YMOD(Port[i], 1);

                                string b =timercount.Read(1003);
                                string[] timer = new string[2];

                                timer = b.Split(':');

                                float kkk = float.Parse(timer[0]);

                                if (kkk>= 0)
                                    {
                                        PAN[i] = true;
                                        Timer_btn.BackColor = Color.GreenYellow;
                                    }
                                    else
                                    {
                                        PAN[i] = false;
                                    Timer_btn.BackColor = Color.LightCoral;
                                    }

                                    Thread.Sleep(300);
                                    timercount.Close();
                                
                            
                            }
                            catch
                            {
                                PAN[i] = false;
                                Timer_btn.BackColor = Color.LightCoral;
                                timercount.Close();
                            }
                        } // 직류전원장치
*/
                    }



                    bool result = true;
                    for (int i = 0; i < Setting.Equipment.Length; i++)
                    {
                        if (!PAN[i])
                            result = false;
                    }

                    string mess = "Self Test Error : ";

                    if (result)
                    {
                        MessageBox.Show("Self Test Complete");

                    }
                    else
                    {
                        for (int i = 0; i < Setting.Equipment.Length; i++)
                        {

                            if (PAN[i] == false)
                                mess = mess + Setting.Equipment[i] + " ";
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            thRun = new Thread(testexecute);
            thRun.Start();
        }

        private void plc_btn_MouseUp(object sender, EventArgs e)
        {

        }
    }
}
