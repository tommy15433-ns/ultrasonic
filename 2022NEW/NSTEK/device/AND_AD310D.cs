using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
//using NationalInstruments.Examples;
using System.Net.Sockets;
using System.Net;
using System.Data;
using System.Net.NetworkInformation;
using Library;

namespace _2022_Test.NSTEK.device
{
    //AND_AD310D
    public class AND_AD310D
    {
        private string PortName;
        private SerialPort spCom;
        private float iLimitSecond;

        public AND_AD310D() { }

        public AND_AD310D(string sPort)
        {
            iLimitSecond = 0.5f;

            PortName = sPort;
            spCom = new SerialPort();
            spCom.PortName = PortName;
            spCom.BaudRate = 38400;
            spCom.StopBits = StopBits.One;
            spCom.Parity = Parity.None;
            spCom.Handshake = Handshake.None;
            spCom.DataBits = 8;
        }




        public string SelfTest()
        {
            if (!spCom.IsOpen)
            {
                spCom.Open();
            }

            SendData("RW");

            Thread.Sleep(50);
            return ReturnValue();
        }


        public string ReadValue()
        {
            SendData("RW");
            return ReturnValue();
        }


        private void SendData(string sCMD)
        {
            try
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                string sFlag = sCMD + Constant.CRLF;
                spCom.Write(sFlag);

                //Thread.Sleep(300);  // Command Execution Time Limit(250)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //  PortClose();
            }
        }

        private string ReturnValue()
        {
            string sRet = "";
            int len = -1;

            DateTime dtStart = new DateTime(DateTime.Now.Ticks);

            do
            {
                Application.DoEvents();

                sRet += spCom.ReadExisting();

                len = sRet.IndexOf(Constant.CRLF);

                TimeSpan ts = DateTime.Now.Subtract(dtStart);
                if (ts.TotalSeconds > iLimitSecond)
                {
                    sRet = iLimitSecond.ToString() + " Seconds Timeout";
                    break;
                }

            } while (len == -1);

            //   spCom.Close();

            return sRet;
        }
        public void Close()
        {
            if (spCom.IsOpen)
                spCom.Close();
        }

        public void Open()
        {
            if (!spCom.IsOpen)
                spCom.Open();
        }
    }

}
