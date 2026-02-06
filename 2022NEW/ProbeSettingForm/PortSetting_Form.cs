using System;
using System.Windows.Forms;
using Library;
using System.Runtime.InteropServices;
using System.Drawing;
namespace _2022_Test
{
    public partial class PortSetting_Form : Form
    {
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
            //name.Text = Setting.Name;
            chart1.Series[0].Points.AddXY(0, 0);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           PictureBox pb = (PictureBox)sender;

            string[] pb_name = pb.Name.Split('_');

            byte[] stream = new byte[7];

            if (pb.BackColor == Color.Green)
            {
                pb.BackColor = Color.WhiteSmoke;
            }
            else
            {
                pb.BackColor = Color.Green;
            }

        }
    }
}
