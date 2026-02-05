using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using Library;
using System.IO;
using System.Threading;
using _2022_Test;
using System.Diagnostics;
using _2022_Test.NSTEK.device;

namespace _2022_Test
{
    public partial class SettingForm : Form
    {
        Thread thRun;

        PLCEnet plc;
        AND_AD310D loadcell;
        DeviceTools.P2PE p2p;
        DSP_LAN dc_power;
        DeviceTools.DSOX1204G_LAN dsox1204g_lan;

        StartForm sf;

        public SettingForm(StartForm form)
        {
            InitializeComponent();

            sf = form;
        }
        private void testexecute()
        {
            CheckForIllegalCrossThreadCalls = false;
            try
            {
                string[] Port = System.IO.Ports.SerialPort.GetPortNames();
                string[] PAN = new string[Setting.Equipment.Length];

                for (int i = 0; i < Setting.Equipment.Length; i++)
                {
                    AutoSet_Button.Text = (i + 1).ToString() + " 번 설정 중....";

                    if (Setting.Equipment[i] == "Loadcell")
                    {
                        for (int j = 0; j < Port.Length; j++)
                        {

                            loadcell = new AND_AD310D(Port[i]); Thread.Sleep(100);
                            loadcell.Open(); Thread.Sleep(100);

                            string sRes = loadcell.SelfTest();

                            if (sRes.IndexOf("kg") >= 0)
                            {
                                PAN[i] = Port[j];
                                break;
                            }
                           
                            loadcell.Close();

                        }
                                     
                    } // 직류전원장치

                    if (Setting.Equipment[i] == "plc")
                    {
                        try
                        {
                            plc = new PLCEnet();
                            string pan = plc.ReadBit("0");
                            if (pan == "00" || pan == "01")
                            {
                                PAN[i] = "LAN";
                            }
                        }
                        catch
                        {

                        }
                    }
                    if (Setting.Equipment[i] == "power")
                    {
                        try
                        {
                            bool p_result;

                            dc_power = new DSP_LAN(Setting.dsp_ip);

                            p_result = dc_power.PING();
                            Thread.Sleep(300);

                            if (p_result)
                            {
                                string pan = dc_power.SelfTest();

                                Thread.Sleep(300);

                                if (pan.IndexOf("DSP") >= 0)
                                {
                                    PAN[i] = "LAN";
                                    dc_power.Portclose();
                                }
                                else
                                {
                                    PAN[i] = "";
                                }

                            }
                        }
                        catch
                        {
                            PAN[i] = "";
                        }
                    }

                    if (Setting.Equipment[i] == "osc")
                    {
                      
                    }
                    Thread.Sleep(100);
                }
                bool result = true;

                for (int i = 0; i < Setting.Equipment.Length; i++)
                {
                    if (PAN[i] == "" || PAN[i] == null)
                        result = false;

                }

                if (result)
                {
                    try
                    {
                        if ((Directory.Exists(@"data\")))
                        {
                            StreamWriter sw = new StreamWriter(@"data\setup.ini");
                            string data = "";
                            for (int k = 0; k < Setting.Equipment.Length; k++)
                                data = data + PAN[k] + "!";
                            sw.Write(data);
                            sw.Close();
                        }
                        else
                        {
                            Directory.CreateDirectory(@"data\");
                            StreamWriter sw = new StreamWriter(@"data\setup.ini");
                            string data = "";
                            for (int k = 0; k < Setting.Equipment.Length; k++)
                                data = data + PAN[k] + "!";
                            sw.Write(data);
                            sw.Close();
                        }
                        MessageBox.Show("포트설정완료");
                    }
                    catch
                    {
                        MessageBox.Show("엑세스가 거부되었습니다");

                    }

                }
                else
                    MessageBox.Show("포트설정실패");

                AutoSet_Button.Text = "포트 설정";
            }
            catch
            {
                AutoSet_Button.Text = "포트 설정";
            }
        }

        private void AutoSet_Button_MouseUp(object sender, MouseEventArgs e)
        {
            thRun = new Thread(testexecute);
            thRun.Start();
        }

        private void Exit_Button_MouseUp(object sender, MouseEventArgs e)
        {
            sf.Opacity = 1;
            this.Close();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(@"reportpath.ini");
                string temp = sr.ReadToEnd();
                sr.Close();
                string[] Path = temp.Split('!');
                Path_txt.Text = Path[0];
            }
            catch
            {

            }
        }

        private void save_button_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void ksS_BTN1_MouseUp(object sender, MouseEventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            Path_txt.Text = folderBrowserDialog1.SelectedPath;
        }

        private void ckp_txt_min7_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            tb.BackColor = SystemColors.Info;
        }

        private void ckp_txt_min7_MouseLeave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            tb.BackColor = SystemColors.Window;
        }

        private void ksS_BTN2_MouseUp(object sender, MouseEventArgs e)
        {
          
        }

        private void uiImageButton1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        private void Save_Path_btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Path_txt.Text = folderBrowserDialog1.SelectedPath;
                try
                {
                    if (File.Exists("reportpath.ini"))
                    {
                        StreamWriter sw = new StreamWriter("reportpath.ini");
                        string data = Path_txt.Text;
                        sw.Write(data);
                        sw.Close();
                    }
                    else
                    {
                        File.Create("reportpath.ini");

                        StreamWriter sw = new StreamWriter("reportpath.ini");
                        string data = Path_txt.Text;
                        sw.Write(data);
                        sw.Close();
                    }

                    MessageBox.Show("성적서 저장경로 설정이 완료되었습니다.");
                }
                catch
                {
                    MessageBox.Show("성적서 저장경로 설정이 실패되었습니다.");
                }
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            PortSetting_Form pf = new PortSetting_Form();
            pf.ShowDialog();
        }
    }
}

