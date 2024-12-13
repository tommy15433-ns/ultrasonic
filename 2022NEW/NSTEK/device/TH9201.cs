using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using lucidio;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using Library;

namespace Power_Modbus_RTU_SAMPLE
{



    public class TH9201
    {

        private float iLimitSecond;
        private string PortName;
        private SerialPort spCom;
        public TH9201(string sPort)
        {
            iLimitSecond = 1f;
            PortName = sPort;
            spCom = new SerialPort();
            spCom.PortName = PortName;
            spCom.BaudRate = 19200;
            spCom.StopBits = StopBits.Two;
            spCom.Parity = Parity.None;
            spCom.DataBits = 8;
        }
        public void PortOpen()
        {
            try
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            catch
            {

            }
        }
        public void PortClose()
        {
            try
            {
                if (spCom.IsOpen)
                {
                    spCom.Close();
                }
            }
            catch
            {

            }
        }
        public string SelfTest()
        {
            string sRes = "";
            SendData("*IDN?");
            sRes = ReturnValue();
            return sRes;
        }

        public string Result_2()
        {
            string sRes = "";
            //SendData(":TEST:FETCH2?");
            SendData(":TEST:DATAR?");
            sRes = ReturnValue();
            sRes = sRes.Replace(" ", ""); // 판정, 전압, 전류 순서대로 데이터가 입력된다.
            string[] Result = sRes.Split(',');
            return Result[0];
        }

        public string Result_3()
        {
            string sRes = "";
            //SendData(":TEST:FETCH2?");
            SendData(":TEST:DATAI?");
            sRes = ReturnValue();
            sRes = sRes.Replace(" ", ""); // 판정, 전압, 전류 순서대로 데이터가 입력된다.
            string[] Result = sRes.Split(',');
            return Result[0];
        }

        public string Result()
        {
            string sRes = "";
            SendData(":TEST:FETCH2?");
            //SendData(":TEST:DATAR?");
            sRes = ReturnValue();
            sRes = sRes.Replace(" ", ""); // 판정, 전압, 전류 순서대로 데이터가 입력된다.
            string[] Result = sRes.Split(',');
            if (Result[0] == "0")
            {
                Result[0] = "READY";
            }
            else if (Result[0] == "1")
            {
                Result[0] = "TEST";
            }
            else if (Result[0] == "2")
            {
                Result[0] = "PASS";
            }
            else
            {
                Result[0] = "FAIL";
            }
            return Result[0];
        }
        public void SendData(string sCMD)
        {
            try
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                string sFlag = sCMD;
                spCom.Write(sFlag + Constant.CRLF);
                Thread.Sleep(500);
            }
            catch
            {
                //PortClose();
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
                len = sRet.IndexOf(Constant.LF);
                TimeSpan ts = DateTime.Now.Subtract(dtStart);
                if (ts.TotalSeconds > iLimitSecond)
                {
                    sRet = iLimitSecond.ToString() + " Seconds Timeout";
                    break;
                }
            } while (len == -1);
            //spCom.Close();
            return sRet;
        }


    }
}