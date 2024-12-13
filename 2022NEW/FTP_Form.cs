using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using Library;
using System.Net; //FTP
namespace _2022_Test
{
    public partial class FTP_Form : Form
    {
        public FTP_Form()
        {
            InitializeComponent();
        }

        private void Exit_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void Search_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                CultureInfo Provider = CultureInfo.InvariantCulture;
            }
            catch
            {

            }
        }

        private void Save_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (File.Exists(Setting.FTP_File))
                {
                    File.Delete(Setting.FTP_File);
                }
                StreamWriter FTP = new StreamWriter(Setting.FTP_File);
                FTP.Write(IP_TextBox.Text + "," + ID_TextBox.Text + "," + PW_TextBox.Text);
                FTP.Close();
                MessageBox.Show("IP, ID, PW가 저장되었습니다.");
            }
            catch
            {
                MessageBox.Show("IP, ID, PW 저장에 실패하였습니다.");
            }
        }

        private void FTP_Form_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Setting.FTP_File))
                {
                    StreamReader sr2 = new StreamReader(Setting.FTP_File);
                    string temp = sr2.ReadToEnd();
                    sr2.Close();
                    string[] atemp = temp.Split(',');
                    IP_TextBox.Text = atemp[0];
                    ID_TextBox.Text = atemp[1];
                    PW_TextBox.Text = atemp[2];
                }
                else
                {
                    MessageBox.Show("저장된 파일이 없습니다.");
                }
            }
            catch
            {

            }
        }
    }
}
