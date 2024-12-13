using System;
using System.Windows.Forms;
using Library;
using System.Runtime.InteropServices;
namespace _2022_Test
{
    public partial class PortSetting_Form : Form
    {
        Select_TEST_Form st_form;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect
                                                    , int nTopRect
                                                    , int nRightRect
                                                    , int nBottomRect
                                                    , int nWidthEllipse
                                                    , int nHeightEllipse);

        public PortSetting_Form()
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

        }

        private void Setting_BTN_MouseUp(object sender, MouseEventArgs e)
        {
          
        }

        private void FTP_BTN_MouseUp(object sender, MouseEventArgs e)
        {
          
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
        }
    }
}
