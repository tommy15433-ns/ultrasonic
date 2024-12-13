using System;
using System.Windows.Forms;
using Library;
using System.Runtime.InteropServices;
namespace _2022_Test
{
    public partial class StartForm : Form
    {
        Select_TEST_Form st_form;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect
                                                    , int nTopRect
                                                    , int nRightRect
                                                    , int nBottomRect
                                                    , int nWidthEllipse
                                                    , int nHeightEllipse);

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
            st_form = new Select_TEST_Form();
            st_form.ShowDialog();

            //init_Tester_info_Form iF = new init_Tester_info_Form();
            //iF.ShowDialog();
            //iF.Dispose();
        }

        private void Report_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            ReportForm RF = new ReportForm(this);
            RF.ShowDialog();
            RF.Dispose();
        }

        private void SelfTest_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            this.Opacity = 0.8;

            SelfTestForm STF = new SelfTestForm(this);
            STF.ShowDialog();
            STF.Dispose();         
        }

        private void Setting_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            this.Opacity = 0.8;

            SettingForm sf = new SettingForm(this);
            sf.ShowDialog();
            sf.Dispose();
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
    }
}
