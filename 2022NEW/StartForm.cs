using System;
using System.Windows.Forms;
using Library;
using System.Runtime.InteropServices;
using _2022_Test.ProbeSettingForm;
using System.Diagnostics;
using _2022_Test.NSTEK.Device;
namespace _2022_Test
{
    public partial class StartForm : Form
    {
        Select_TEST_Form st_form;
        MainForm Main_form;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect
                                                    , int nTopRect
                                                    , int nRightRect
                                                    , int nBottomRect
                                                    , int nWidthEllipse
                                                    , int nHeightEllipse);


        private ProbeSetting psvm = new ProbeSetting();
        public StartForm()
        {
            InitializeComponent();
        }

        private void Exit_Button_MouseUp(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            Name_Label.Text = Setting.Name;
        }

        private void Exit_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            PortClose();
        }

        private void PortClose()
        {
            try
            {

            }
            catch
            {

            }
        }

        private void Test_BTN_MouseUp(object sender, MouseEventArgs e)
        {


            //init_Tester_info_Form iF = new init_Tester_info_Form();
            //iF.ShowDialog();
            //iF.Dispose();
        }
        private void FTP_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            FTP_Form FF = new FTP_Form();
            FF.ShowDialog();
            FF.Dispose();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PLC_Test_Form PTF = new PLC_Test_Form();
            PTF.ShowDialog();
            PTF.Dispose();
        }
        private void Test_BTN_Click(object sender, EventArgs e)
        {
            MainForm MF = new MainForm();
            MF.ShowDialog();
            MF.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            st_form = new Select_TEST_Form();
            st_form.ShowDialog();
        }

        private void Exit_BTN_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Form1 f1 = new Form1();
            //f1.Show();
            //MessageBox.Show(psvm.UserName.ToString());
        }
        private NSTEK.Device.FocusPx focusPx;

        private void OnConnected(object sender, EventArgs e)
        {
            Debug.WriteLine("FocusPx connected");
            loadingform.Hide();
            Main_form = new MainForm();
            Main_form.ShowDialog();
        }
        private void OnDiscoveryFail(object sender, EventArgs e)
        {
            loadingform.Hide();
            Debug.WriteLine("Timedout");
        }
        private void OnDiscoveryTimeout(object sender, EventArgs e)
        {
            Debug.WriteLine("timedout");
        }
        private void ConnectFocusPx()
        {
            FocusPx.DiscoveryEvents discoveryEvents = new FocusPx.DiscoveryEvents(OnDiscoveryTimeout, OnConnected, OnDiscoveryFail);
            focusPx = new NSTEK.Device.FocusPx();
            try
            {
                focusPx.DiscoveryStart("192.168.0.1", 60000, discoveryEvents);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void StopDiscover()
        {
            focusPx.DiscoveryStop();
            //focusPx.deviceDiscovery.Interrupt();
        }

        private LoadingForm loadingform = new LoadingForm();
        private void Test_BTN_Click_1(object sender, EventArgs e)
        {
            CadView.CadView cv = new CadView.CadView(panel_dxf);
            cv.Display(@"D:\Tester\ultrasonic_dnetfw\초음파탐상기\img\defect.dxf");
            //if (loadingform.Visible == false)
            //{
            //    loadingform.Show();
            //    ConnectFocusPx();
            //}
            //else
            //{
            //    StopDiscover();
                
            //}
                
        }

        private void SelfTest_BTN_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.8;

            SelfTestForm STF = new SelfTestForm(this);
            STF.ShowDialog();
            STF.Dispose();
        }

        private void Report_BTN_Click(object sender, EventArgs e)
        {
            ReportForm RF = new ReportForm(this);
            RF.ShowDialog();
            RF.Dispose();
        }

        private void Setting_BTN_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.8;

            SettingForm sf = new SettingForm(this);
            sf.ShowDialog();
            sf.Dispose();
        }
    }
}
