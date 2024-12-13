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

namespace Library
{
    public partial class DeviceTools
    {
        public enum MODE_ONOFF { ON, OFF }

        public class EX150_12_LAN
        {

            Socket Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketAsyncEventArgs arg = new SocketAsyncEventArgs();

            string IP_Adress;
            public EX150_12_LAN(string sPort)
            {
                IP_Adress = sPort;
            }
            public void PortClose()
            {
                try
                {
                    if (Client.Connected == true)
                    {
                        Client.Close();
                    }
                }
                catch
                {

                }
            }

            public void PortOpen()
            {
                try
                {
                    IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                    IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5000);
                    arg.RemoteEndPoint = serverEndPoint;
                    Client.ConnectAsync(arg);
                }
                catch
                {

                }
            }

            public string SelfTest()
            {
                SendData("*IDN?");
                Thread.Sleep(300);
                string sRes = ReturnValue();
                return sRes;
            }

            public void OnOff(string sCMD)
            {
                SendData("OUTP " + sCMD);
                Thread.Sleep(100);
            }

            public void SetVolt(float sCMD)
            {
                SendData("VOLT " + sCMD);
                Thread.Sleep(100);
            }
            public void SetCurr(float sCMD)
            {
                SendData("CURR " + sCMD);
                Thread.Sleep(100);
            }

            public bool PING()
            {
                bool res = false;
                Ping sender2 = new Ping();
                PingReply reply = sender2.Send(IP_Adress, 1);
                if (reply.Status == IPStatus.Success)
                {
                    res = true;
                }
                return res;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                    if (Client.Connected == true)
                    {
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\n"); // LF
                        arg.SetBuffer(bytesBuff, 0, bytesBuff.Length);
                        Client.SendAsync(arg);
                    }
                    else
                    {
                        IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                        IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5000);
                        arg.RemoteEndPoint = serverEndPoint;
                        Client.ConnectAsync(arg);
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\n");
                        arg.SetBuffer(bytesBuff, 0, bytesBuff.Length);
                        Client.SendAsync(arg);
                    }
                }
                catch
                {

                }
            }

            private string ReturnValue()
            {
                try
                {
                    if (Client.Connected)
                    {
                        SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                        byte[] receipveBUffer = new byte[4096];
                        int byteBytesRecvd = Client.Receive(receipveBUffer);
                        byte[] receipve = new byte[byteBytesRecvd];
                        Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);
                        string receiveStr = Encoding.Default.GetString(receipve);
                        return receiveStr;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {
                    return "";
                }
            }

        }

        public class PSU_LAN
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP_Adress;
            public PSU_LAN(string sPort)
            {
                IP_Adress = sPort;
            }
            public void Portclose()
            {
                try
                {
                    if (client.Connected == true)
                    {
                        client.Close();
                    }
                }
                catch
                {

                }
            }
            public void OutPut(string sOnOff)
            {
                try
                {
                    SendData("OUTP:STAT:IMM " + sOnOff.ToString());
                }
                catch
                {

                }
            }
            public void Portopen()
            {
                try
                {
                    if (client.Connected == false)
                    {
                        IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                        IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2268);
                        client.Connect(serverEndPoint);
                    }
                }
                catch
                {

                }
            }
            public bool PING()
            {
                bool res = false;
                Ping sender2 = new Ping();
                PingReply reply = sender2.Send(IP_Adress, 1);
                if (reply.Status == IPStatus.Success)
                {
                    res = true;
                }
                return res;
            }
            public string SelfTest()
            {
                SendData("*IDN?");
                string sRes = ReturnValue();
                return sRes;
            }
            public void SendData(string sCMD)
            {
                try
                {
                    if (client.Connected)
                    {
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);
                    }
                    else
                    {
                        IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                        IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2268);
                        client.Connect(serverEndPoint);
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);
                    }
                }
                catch
                {

                }
            }

            public string MeaSureVoltage()
            {

                SendData("MEASURE:VOLTage?");


                string sRes = ReturnValue();

                return sRes;

            }
            public string MeaSureCuurent()
            {

                SendData("MEASURE:CURRent?");

                string sRes = ReturnValue();

                return sRes;
            }


            private string ReturnValue()
            {
                byte[] receipveBUffer = new byte[1024];
                int byteBytesRecvd = client.Receive(receipveBUffer);
                byte[] receipve = new byte[byteBytesRecvd];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);
                string receiveStr = Encoding.Default.GetString(receipve);
                return receiveStr;
            }
            public void SetVolt(float Volt)
            {
                try
                {
                    SendData("SOUR:VOLT:LEV:IMM:AMPL " + Volt.ToString());
                }
                catch
                {

                }
            }
        } // GW INSTEK PSU 150-10          
        public class ENS_Encoder_MASCON
        {

            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public ENS_Encoder_MASCON(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;
                //PortName = "COM8";

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 38400;
            }

            public void PortOpen()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }
            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                string pan = "NG";

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte[] btREAD = new byte[8];

                btREAD[0] = 0xF2;
                btREAD[1] = 0x57;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                byte btBCC = 0x00;

                for (int i = 0; i < 8; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[7] = btBCC;

                spCom.Write(btREAD, 0, 8);

                Thread.Sleep(200);

                pan = SelfTest_ReturnData();

                Thread.Sleep(20);


                return pan;
            }
            private byte[] ReturnData()
            {

                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[8];


                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xF2) //시작
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x57 || ndata == 0x52) //CMD
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 2)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 5)
                    {

                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }

                    else if (ReadCount == 6)
                    {

                        if (ndata == 0xF3) //끝
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 7)
                    {
                        if (ndata == bcc)
                        {
                            btData[ReadCount] = ndata;
                            // 수신 ok
                            btRes = new byte[8];
                            for (int x = 0; x < 8; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }


                return btData;
            }
            private string SelfTest_ReturnData()
            {
                string pan = "NG";
                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[8];


                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xF2) //시작
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x57 || ndata == 0x52) //CMD
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 2)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 5)
                    {

                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }

                    else if (ReadCount == 6)
                    {

                        if (ndata == 0xF3) //끝
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 7)
                    {
                        if (ndata == bcc)
                        {
                            pan = "OK";
                            // 수신 ok
                            btRes = new byte[8];
                            for (int x = 0; x < 8; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }


                return pan;
            }
            public void Write_Duty(int duty)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                byte[] btREAD = new byte[8];
                byte btBCC = 0x00;

                int MSB = ((duty * 100) / 256);
                int LSB = ((duty * 100) % 256);

                btREAD[0] = 0xF2;
                btREAD[1] = 0x57;
                btREAD[2] = Convert.ToByte(LSB);
                btREAD[3] = Convert.ToByte(MSB); ;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                for (int i = 0; i < 8; i++)
                {
                    btBCC = (byte)(btBCC ^ btREAD[i]);

                }

                btREAD[7] = btBCC;

                Thread.Sleep(200);

                spCom.Write(btREAD, 0, 8);

            }
            public string READ_Duty()
            {
                string duty = "";

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                byte[] btREAD = new byte[8];
                byte[] reData = new byte[8];
                byte btBCC = 0x00;

                btREAD[0] = 0xF2;
                btREAD[1] = 0x52;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                for (int i = 0; i < 8; i++)
                {
                    btBCC = (byte)(btBCC ^ btREAD[i]);

                }

                btREAD[7] = btBCC;

                Thread.Sleep(200);

                spCom.Write(btREAD, 0, 8);

                Thread.Sleep(200);

                reData = ReturnData();

                string value = reData[3].ToString("X2") + reData[2].ToString("X2");

                float real_value = Convert.ToInt32(value, 16);

                duty = (real_value / 100).ToString("F0");

                return duty;
            }

        }

        public class ENS_Encoder_PWM
        {

            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public ENS_Encoder_PWM(string sPort)
            {
                iLimitSecond = 0.3f;

                //PortName = sPort;
                PortName = "COM9";

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 38400;
            }

            public void PortOpen()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }
            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                string pan = "NG";

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte[] btREAD = new byte[8];

                btREAD[0] = 0xF2;
                btREAD[1] = 0x57;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                byte btBCC = 0x00;

                for (int i = 0; i < 8; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[7] = btBCC;

                spCom.Write(btREAD, 0, 8);

                Thread.Sleep(200);

                pan = SelfTest_ReturnData();

                Thread.Sleep(20);


                return pan;
            }
            private byte[] ReturnData()
            {

                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[8];


                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xF2) //시작
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x57 || ndata == 0x52) //CMD
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 2)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 5)
                    {

                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }

                    else if (ReadCount == 6)
                    {

                        if (ndata == 0xF3) //끝
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 7)
                    {
                        if (ndata == bcc)
                        {
                            btData[ReadCount] = ndata;
                            // 수신 ok
                            btRes = new byte[8];
                            for (int x = 0; x < 8; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }


                return btData;
            }
            private string SelfTest_ReturnData()
            {
                string pan = "NG";
                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[8];


                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xF2) //시작
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x57 || ndata == 0x52) //CMD
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 2)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 5)
                    {

                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }

                    else if (ReadCount == 6)
                    {

                        if (ndata == 0xF3) //끝
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 7)
                    {
                        if (ndata == bcc)
                        {
                            pan = "OK";
                            // 수신 ok
                            btRes = new byte[8];
                            for (int x = 0; x < 8; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }


                return pan;
            }
            public void Write_Duty(int duty)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                byte[] btREAD = new byte[8];
                byte btBCC = 0x00;

                int MSB = ((duty * 100) / 256);
                int LSB = ((duty * 100) % 256);

                btREAD[0] = 0xF2;
                btREAD[1] = 0x57;
                btREAD[2] = Convert.ToByte(LSB);
                btREAD[3] = Convert.ToByte(MSB); ;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                for (int i = 0; i < 8; i++)
                {
                    btBCC = (byte)(btBCC ^ btREAD[i]);

                }

                btREAD[7] = btBCC;

                Thread.Sleep(200);

                spCom.Write(btREAD, 0, 8);

            }
            public string READ_Duty()
            {
                string duty = "";

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                byte[] btREAD = new byte[8];
                byte[] reData = new byte[8];
                byte btBCC = 0x00;

                btREAD[0] = 0xF2;
                btREAD[1] = 0x52;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                for (int i = 0; i < 8; i++)
                {
                    btBCC = (byte)(btBCC ^ btREAD[i]);

                }

                btREAD[7] = btBCC;

                Thread.Sleep(200);

                spCom.Write(btREAD, 0, 8);

                Thread.Sleep(200);

                reData = ReturnData();

                string value = reData[3].ToString("X2") + reData[2].ToString("X2");

                float real_value = Convert.ToInt32(value, 16);

                duty = (real_value / 100).ToString("F0");

                return duty;
            }

        }


        public class ENS_Encoder_BREAK
        {

            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public ENS_Encoder_BREAK(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;
                PortName = "COM11";

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 38400;
            }

            public void PortOpen()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }
            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                string pan = "NG";

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte[] btREAD = new byte[8];

                btREAD[0] = 0xF2;
                btREAD[1] = 0x57;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                byte btBCC = 0x00;

                for (int i = 0; i < 8; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[7] = btBCC;

                spCom.Write(btREAD, 0, 8);

                Thread.Sleep(200);

                pan = SelfTest_ReturnData();

                Thread.Sleep(20);


                return pan;
            }
            private byte[] ReturnData()
            {

                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[8];


                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xF2) //시작
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x57 || ndata == 0x52) //CMD
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 2)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 5)
                    {

                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }

                    else if (ReadCount == 6)
                    {

                        if (ndata == 0xF3) //끝
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 7)
                    {
                        if (ndata == bcc)
                        {
                            btData[ReadCount] = ndata;
                            // 수신 ok
                            btRes = new byte[8];
                            for (int x = 0; x < 8; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }


                return btData;
            }
            private string SelfTest_ReturnData()
            {
                string pan = "NG";
                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[8];


                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xF2) //시작
                        {
                            bcc = ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x57 || ndata == 0x52) //CMD
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 2)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 5)
                    {

                        bcc ^= ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }

                    else if (ReadCount == 6)
                    {

                        if (ndata == 0xF3) //끝
                        {
                            bcc ^= ndata;
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 7)
                    {
                        if (ndata == bcc)
                        {
                            pan = "OK";
                            // 수신 ok
                            btRes = new byte[8];
                            for (int x = 0; x < 8; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }


                return pan;
            }
            public void Write_Duty(int duty)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                byte[] btREAD = new byte[8];
                byte btBCC = 0x00;

                int MSB = ((duty * 100) / 256);
                int LSB = ((duty * 100) % 256);

                btREAD[0] = 0xF2;
                btREAD[1] = 0x57;
                btREAD[2] = Convert.ToByte(LSB);
                btREAD[3] = Convert.ToByte(MSB); ;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                for (int i = 0; i < 8; i++)
                {
                    btBCC = (byte)(btBCC ^ btREAD[i]);

                }

                btREAD[7] = btBCC;

                Thread.Sleep(200);

                spCom.Write(btREAD, 0, 8);

            }
            public string READ_Duty()
            {
                string duty = "";

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                byte[] btREAD = new byte[8];
                byte[] reData = new byte[8];
                byte btBCC = 0x00;

                btREAD[0] = 0xF2;
                btREAD[1] = 0x52;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;
                btREAD[5] = 0x00;
                btREAD[6] = 0xF3;

                for (int i = 0; i < 8; i++)
                {
                    btBCC = (byte)(btBCC ^ btREAD[i]);

                }

                btREAD[7] = btBCC;

                Thread.Sleep(200);

                spCom.Write(btREAD, 0, 8);

                Thread.Sleep(200);

                reData = ReturnData();

                string value = reData[3].ToString("X2") + reData[2].ToString("X2");

                float real_value = Convert.ToInt32(value, 16);

                duty = (real_value / 100).ToString("F0");

                return duty;
            }

        }


        //ENS_Relay
        public class ENS_Relay
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public ENS_Relay(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 38400;
            }



            public void Open()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public bool SelfTest(byte btID)
            {


                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte[] btREAD = new byte[8];

                btREAD[0] = 0xF2;
                btREAD[1] = btID;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;


                btREAD[5] = 0xF3;

                byte btBCC = 0x00;

                for (int i = 3; i < 5; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[6] = btBCC;

                for (int i = 0; i < 6; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[7] = btBCC;
                bool pan = false;
                for (int k = 0; k < 5; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        spCom.Write(btREAD, i, 1);
                    }
                    Thread.Sleep(20);




                    pan = ReturnValue2();
                    if (pan)
                        break;
                    Thread.Sleep(20);

                }




                return pan;

            }

            public byte[] OUTREAD(byte btID) //0-10(0-255)
            {


                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte[] btREAD = new byte[8];

                btREAD[0] = 0xF2;
                btREAD[1] = btID;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;


                btREAD[5] = 0xF3;

                byte btBCC = 0x00;

                for (int i = 3; i < 5; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[6] = btBCC;

                for (int i = 0; i < 6; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[7] = btBCC;
                byte[] btSend = new byte[8];
                for (int k = 0; k < 5; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        spCom.Write(btREAD, i, 1);
                    }
                    Thread.Sleep(20);



                    btSend = ReturnValue();

                    if (btREAD[0] == btSend[0] && btREAD[5] == btSend[5])
                        break;
                    Thread.Sleep(20);
                }
                return btSend;


            }
            public void OUT(byte btID, int addr, bool onoff) //0-10(0-255)
            {


                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte[] btREAD = new byte[8];

                btREAD[0] = 0xF2;
                btREAD[1] = btID;
                btREAD[2] = 0x00;
                btREAD[3] = 0x00;
                btREAD[4] = 0x00;


                btREAD[5] = 0xF3;

                byte btBCC = 0x00;

                for (int i = 3; i < 5; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[6] = btBCC;

                for (int i = 0; i < 6; i++)
                {
                    btBCC ^= btREAD[i];
                }
                btREAD[7] = btBCC;


                for (int i = 0; i < 8; i++)
                {
                    spCom.Write(btREAD, i, 1);
                }
                Thread.Sleep(20);



                byte[] btSend = ReturnValue();

                if (btSend[0] != 0xf2)
                {

                    for (int k = 0; k < 10; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            spCom.Write(btREAD, i, 1);
                        }
                        Thread.Sleep(40);



                        btSend = ReturnValue();
                        if (btSend[0] == 0xf2)
                        {
                            break;
                        }
                        Thread.Sleep(40);
                    }
                }
                //byte[] btSend =new byte[8];
                Thread.Sleep(20);
                if (onoff)
                {
                    if (addr == 0)
                    {
                        btSend[3] = 0;
                        btSend[4] = 0;
                    }
                    else if (addr < 9)
                        btSend[3] |= (byte)(0x01 << (byte)(addr - 1));
                    else
                        btSend[4] |= (byte)(0x01 << (byte)(addr - 9));


                }
                else
                {
                    if (addr == 0)
                    {
                        btSend[3] = 0;
                        btSend[4] = 0;
                    }
                    else if (addr < 9)
                        btSend[3] &= (byte)(~(0x01 << addr - 1));
                    else
                        btSend[4] &= (byte)(~(0x01 << addr - 9));
                }

                btSend[0] = 0xF2;
                btSend[1] = btID;
                btSend[2] = 0x01;
                btSend[5] = 0xF3;

                btBCC = 0x00;

                for (int i = 3; i < 5; i++)
                {
                    btBCC ^= btSend[i];
                }
                btSend[6] = btBCC;

                btBCC = 0;
                for (int i = 0; i < 6; i++)
                {
                    btBCC ^= btSend[i];
                }
                btSend[7] = btBCC;
                byte[] btrr = new byte[8];
                for (int k = 0; k < 5; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        spCom.Write(btSend, i, 1);
                    }
                    Thread.Sleep(20);
                    btrr = ReturnValue();
                    if (btSend[0] == btrr[0] && btSend[1] == btrr[1] && btSend[2] == btrr[2] && btSend[3] == btrr[3] &&
                        btSend[4] == btrr[4] && btSend[5] == btrr[5] && btSend[6] == btrr[6] && btSend[7] == btrr[7])
                        break;
                    Thread.Sleep(20);
                }
            }

            public void ALLOUT(byte btID, bool onoff) //0-10(0-255)
            {
                try
                {
                    Thread.Sleep(30);
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }



                    byte[] btSend = new byte[8];

                    btSend[0] = 0xF2;
                    btSend[1] = btID;
                    btSend[2] = 0x01;
                    btSend[3] = 0x00;
                    btSend[4] = 0x00;


                    btSend[5] = 0xF3;
                    //byte[] btSend =new byte[8];
                    Thread.Sleep(20);
                    if (onoff)
                    {

                        btSend[3] = 0xFF;

                        btSend[4] = 0xFF;


                    }
                    else
                    {
                        btSend[3] = 0x00;

                        btSend[4] = 0x00;
                    }

                    btSend[0] = 0xF2;
                    btSend[1] = btID;
                    btSend[2] = 0x01;
                    btSend[5] = 0xF3;

                    byte btBCC = 0x00;

                    for (int i = 3; i < 5; i++)
                    {
                        btBCC ^= btSend[i];
                    }
                    btSend[6] = btBCC;

                    btBCC = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        btBCC ^= btSend[i];
                    }
                    btSend[7] = btBCC;
                    byte[] btrr = new byte[8];
                    for (int k = 0; k < 5; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            spCom.Write(btSend, i, 1);
                        }
                        Thread.Sleep(30);
                        btrr = ReturnValue();
                        if (btSend[0] == btrr[0] && btSend[1] == btrr[1] && btSend[2] == btrr[2] && btSend[3] == btrr[3] &&
                            btSend[4] == btrr[4] && btSend[5] == btrr[5] && btSend[6] == btrr[6] && btSend[7] == btrr[7])
                            break;
                        Thread.Sleep(30);
                    }
                    Thread.Sleep(50);
                }

                catch
                {
                    try
                    {
                        Thread.Sleep(1000);
                        if (!spCom.IsOpen)
                        {
                            spCom.Open();
                        }



                        byte[] btSend = new byte[8];

                        btSend[0] = 0xF2;
                        btSend[1] = btID;
                        btSend[2] = 0x01;
                        btSend[3] = 0x00;
                        btSend[4] = 0x00;


                        btSend[5] = 0xF3;
                        //byte[] btSend =new byte[8];
                        Thread.Sleep(20);
                        if (onoff)
                        {

                            btSend[3] = 0xFF;

                            btSend[4] = 0xFF;


                        }
                        else
                        {
                            btSend[3] = 0x00;

                            btSend[4] = 0x00;
                        }


                        btSend[0] = 0xF2;
                        btSend[1] = btID;
                        btSend[2] = 0x01;
                        btSend[5] = 0xF3;

                        byte btBCC = 0x00;

                        for (int i = 3; i < 5; i++)
                        {
                            btBCC ^= btSend[i];
                        }
                        btSend[6] = btBCC;

                        btBCC = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            btBCC ^= btSend[i];
                        }
                        btSend[7] = btBCC;
                        byte[] btrr = new byte[8];
                        for (int k = 0; k < 5; k++)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                spCom.Write(btSend, i, 1);
                            }
                            Thread.Sleep(30);
                            btrr = ReturnValue();
                            if (btSend[0] == btrr[0] && btSend[1] == btrr[1] && btSend[2] == btrr[2] && btSend[3] == btrr[3] &&
                                btSend[4] == btrr[4] && btSend[5] == btrr[5] && btSend[6] == btrr[6] && btSend[7] == btrr[7])
                                break;
                            Thread.Sleep(30);
                        }
                        Thread.Sleep(50);
                    }

                    catch
                    {


                    }

                }
            }

            private byte[] ReturnValue()
            {




                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[13];



                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xF2) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {

                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 2)
                    {

                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 5)
                    {
                        if (ndata == 0xF3)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }

                    else if (ReadCount == 6)
                    {

                        bcc = (byte)(btData[3] ^ btData[4]);

                        if (bcc == ndata)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 7)
                    {

                        bcc = (byte)(btData[0] ^ btData[1] ^ btData[2] ^ btData[3] ^ btData[4] ^ btData[5]);

                        if (bcc == ndata)
                        {

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            btRes = new byte[8];
                            for (int x = 0; x < 8; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }


                return btData;
            }

            private bool ReturnValue2()
            {


                bool pan = false;

                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[13];



                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xF2) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {

                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 2)
                    {

                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 5)
                    {
                        if (ndata == 0xF3)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }

                    else if (ReadCount == 6)
                    {

                        bcc = (byte)(btData[3] ^ btData[4]);

                        if (bcc == ndata)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 7)
                    {

                        bcc = (byte)(btData[0] ^ btData[1] ^ btData[2] ^ btData[3] ^ btData[4] ^ btData[5]);

                        if (bcc == ndata)
                        {

                            pan = true;
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }


                return pan;
            }


        }

        public class P2PE
        {
            string Address_P2P = "192.168.1.2";

            public bool Selftest()
            {
                bool pan = false;

                try
                {
                    Socket client_P2P = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress ipAddr_P2P = IPAddress.Parse(Address_P2P);
                    IPEndPoint serverEndPoint = new IPEndPoint(ipAddr_P2P, 2100); //P2PE
                    client_P2P.Connect(serverEndPoint);

                    if (client_P2P.Connected)
                    {
                        byte[] receipveBUffer = new byte[4096];
                        int byteBytesRecvd = client_P2P.Receive(receipveBUffer);
                        byte[] receipve = new byte[byteBytesRecvd];
                        Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);
                        string receiveStr = Encoding.Default.GetString(receipve);

                        if (receipve[0] == 242 && receipve[25] == 243)
                        {
                            pan = true;
                        }
                        client_P2P.Close();
                        Thread.Sleep(200);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return pan;
            }

            public byte[] ReturnValue()
            {
                byte[] receive = new byte[20];

                Socket client_P2P = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddr_P2P = IPAddress.Parse(Address_P2P);
                IPEndPoint serverEndPoint = new IPEndPoint(ipAddr_P2P, 2100); //P2PE
                client_P2P.Connect(serverEndPoint);

                if (client_P2P.Connected)
                {

                    byte[] receiveBUffer = new byte[4096];
                    int byteBytesRecvd = client_P2P.Receive(receiveBUffer);
                    receive = new byte[byteBytesRecvd];
                    Array.ConstrainedCopy(receiveBUffer, 0, receive, 0, byteBytesRecvd);
                    string receiveStr = Encoding.Default.GetString(receive);
                    client_P2P.Close();
                    Thread.Sleep(200);
                }
                return receive;
            }
        }

        public class P2P
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public P2P(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 115200;
            }



            public void Open()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }


            public bool Selftest(int data_length)
            {

                bool pan = false;

                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[data_length + 3];
                byte ndata;


                iLen = spCom.BytesToRead;
                if (iLen > 0)
                {

                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt++] = (byte)spCom.ReadByte();
                        if (RevCount_WPt >= 4096) RevCount_WPt = 0;


                    }


                }


                while (RevCount_WPt != RevCount_RPt)
                {
                    ndata = RevQbuffers[RevCount_RPt++];


                    if (RevCount_RPt >= 4096) RevCount_RPt = 0;
                    btData[ReadCount] = ndata;

                    if (ReadCount == 0)
                    {

                        if (ndata == 0xf2) //시작
                        {
                            ReadCount++;
                        }

                        else
                        {
                            ReadCount = 0;


                        }
                    }
                    else if (ReadCount >= 1 && ReadCount <= data_length)
                    {


                        ReadCount++;

                    }
                    else if (ReadCount == data_length + 1)
                    {


                        if (ndata == 0xf3) //끝
                        {
                            ReadCount++;
                        }

                        else
                        {
                            ReadCount = 0;


                        }

                    }

                    else if (ReadCount == data_length + 2)
                    {
                        byte BCC = 0;
                        for (int k = 0; k < ReadCount; k++)
                        {
                            BCC = (byte)((byte)BCC ^ (byte)btData[k]);

                        }
                        if (ndata == BCC) //시작
                        {
                            ReadCount++;

                            pan = true;


                            ReadCount = 0;
                        }
                        else
                        {
                            ReadCount = 0;

                        }


                    }


                }


                return pan;
            }


        }
        //ENS_Relay
        public class ENS_AC_3_PHASE
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;
            byte bcc_check;

            public ENS_AC_3_PHASE(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public ushort CRC16_ENSBoard(byte[] btSend, int length)
            {
                ushort crc16 = 0x0000;

                for (int j = 0; j < length; j++)
                {
                    crc16 = CRC16_Update(crc16, btSend[j]);
                }

                return crc16;
            }

            private ushort CRC16_Update(ushort crc16, byte bt)
            {
                int i;

                crc16 ^= bt;
                for (i = 0; i < 8; i++)
                {
                    if ((crc16 & 0x0001) != 0)
                    {
                        crc16 = (ushort)((crc16 >> 1) ^ 0xA001);
                    }
                    else
                    {
                        crc16 = (ushort)(crc16 >> 1);
                    }
                }

                return crc16;
            }

            public void Write(int volt) //0-20(0x00-0x64)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte[] btREAD = new byte[12];
                btREAD[0] = 0xF2;
                btREAD[1] = 0x57;
                btREAD[2] = 0x64;
                //btREAD[2] = Convert.ToByte(volt.ToString("X4").Substring(0, 2), 16);
                btREAD[3] = 0xF3;

                for (int i = 0; i < 4; i++)
                {
                    bcc_check = (byte)(bcc_check ^ btREAD[i]);
                }

                btREAD[4] = bcc_check;


                spCom.Write(btREAD, 0, 5);

                spCom.ReadExisting();

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

            }
            public bool SelfTest()
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0xF2, 0x57, 0x00, 0xF3, 0x56 };
                    bool pan;
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    pan = ReturnValue();


                    return pan;

                }
                catch
                {
                    return false;
                }
            }
            private bool ReturnValue()
            {


                bool pan = false;

                byte bcc = 0;
                int iLen = 0;
                byte[] RevQbuffers = new byte[4096];
                int RevCount_WPt = 0;
                int RevCount_RPt = 0;
                int ReadCount = 0;
                byte[] btRes;
                byte[] btData = new byte[13];



                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0xE2) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {

                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 2)
                    {

                        btData[ReadCount] = ndata;
                        ReadCount++;

                    }
                    else if (ReadCount == 3)
                    {
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 4)
                    {
                        if (ndata == 0xE3) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 5)
                    {

                        bcc = (byte)(btData[0] ^ btData[1] ^ btData[2] ^ btData[3] ^ btData[4]);

                        if (bcc == ndata)
                        {

                            pan = true;
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                return pan;
            }
        }
        public class ENS_PWM
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public ENS_PWM(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }



            public void Open()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public ushort CRC16_ENSBoard(byte[] btSend, int length)
            {
                ushort crc16 = 0x0000;

                for (int j = 0; j < length; j++)
                {
                    crc16 = CRC16_Update(crc16, btSend[j]);
                }

                return crc16;
            }

            private ushort CRC16_Update(ushort crc16, byte bt)
            {
                int i;

                crc16 ^= bt;
                for (i = 0; i < 8; i++)
                {
                    if ((crc16 & 0x0001) != 0)
                    {
                        crc16 = (ushort)((crc16 >> 1) ^ 0xA001);
                    }
                    else
                    {
                        crc16 = (ushort)(crc16 >> 1);
                    }
                }

                return crc16;
            }

            public void OUT(int frq, int duty) //0-10(0-255)
            {


                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte[] btREAD = new byte[12];

                btREAD[0] = 0x02;
                btREAD[1] = 0x00;
                btREAD[2] = Convert.ToByte(frq.ToString("X4").Substring(0, 2), 16);
                btREAD[3] = Convert.ToByte(frq.ToString("X4").Substring(2, 2), 16);
                btREAD[4] = (byte)duty;
                btREAD[5] = 0x00;
                btREAD[6] = 0x00;
                btREAD[7] = 0x00;
                btREAD[8] = 0x00;

                btREAD[9] = 0x03;

                ushort sh3 = CRC16_ENSBoard(btREAD, 10);

                btREAD[10] = Convert.ToByte(sh3.ToString("X4").Substring(0, 2), 16);
                btREAD[11] = Convert.ToByte(sh3.ToString("X4").Substring(2, 2), 16);



                for (int i = 0; i < 12; i++)
                {
                    spCom.Write(btREAD, i, 1);
                }
                Thread.Sleep(50);

                spCom.ReadExisting();

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

            }



        }
        //KEYSIGHT33500
        public class KEYSIGHT33500
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            public KEYSIGHT33500()
            {

            }

            public void Portclose()
            {
                if (client.Connected == true)
                    client.Close();

            }
            public void Portopen()
            {
                if (client.Connected == false)
                {
                    //   IPAddress ipAddr = IPAddress.Parse(Setting.KEYSIGHT33500);

                    // IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);

                    //  client.Connect(serverEndPoint);
                }
            }

            public string SelfTest()
            {

                SendData("*IDN?"); ;
                string sRes = ReturnValue();

                return sRes;
            }

            public bool PING()
            {
                bool res = false;

                Ping sender2 = new Ping();
                // PingReply reply = sender2.Send(Setting.KEYSIGHT33500, 1);
                //if (reply.Status == IPStatus.Success)
                //{
                //    res = true;
                //}

                return res;


            }
            public void OutPutON(int sOnOff)
            {
                SendData("OUTP" + sOnOff.ToString() + " ON");
            }

            public void OutPutOFF(int sOnOff)
            {
                SendData("OUTP" + sOnOff.ToString() + " OFF");
            }
            public void SIN(int FRQ, int Volt, int PHASE)
            {
                SendData("AM:STAT 0");
                SendData("FUNC SIN");
                SendData("FREQ " + FRQ.ToString());
                SendData("VOLTage " + Volt.ToString());

                SendData("PHAS " + PHASE.ToString());
            }

            public void SQU1(int FRQ, float Volt, int PHASE, int DUTY)
            {
                SendData("AM:STAT 0");
                SendData("SOUR1:FUNC SQU");
                SendData("SOUR1:FUNC:SQU:DCYCle " + DUTY.ToString());
                SendData("SOUR1:FREQ " + FRQ.ToString());
                SendData("SOUR1:VOLTage " + Volt.ToString("F1"));

                SendData("SOUR1:PHAS " + PHASE.ToString());


            }
            public void SQU2(int FRQ, float Volt, int PHASE, int DUTY)
            {
                SendData("AM:STAT 0");
                SendData("SOUR2:FUNC SQU");
                SendData("SOUR2:FUNC:SQU:DCYCle " + DUTY.ToString());
                SendData("SOUR2:FREQ " + FRQ.ToString());
                SendData("SOUR2:VOLTage " + Volt.ToString("F1"));

                SendData("SOUR2:PHAS " + PHASE.ToString());


            }

            public void AM(int MODE, int FRQ, int Volt, int AMFRQ)
            {
                SendData("AM:DSSC 1");
                SendData("AM:STAT 1");
                if (MODE == 0)
                    SendData("FUNC SIN");
                if (MODE == 1)
                    SendData("FUNC SQU");
                SendData("FREQ " + FRQ.ToString());
                SendData("VOLTage " + Volt.ToString());
                SendData("AM:SOUR INT");

                SendData("SOUR1:AM:INT:FREQ " + AMFRQ.ToString());

            }

            private void SendData(string sCMD)
            {
                try
                {

                    if (client.Connected)
                    {
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);
                    }
                    else
                    {
                        // IPAddress ipAddr = IPAddress.Parse(Setting.KEYSIGHT33500);

                        // IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);

                        //  client.Connect(serverEndPoint);
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);

                    }

                }
                catch
                {

                }
            }

            private string ReturnValue()
            {
                byte[] receipveBUffer = new byte[4096];

                int byteBytesRecvd = client.Receive(receipveBUffer);


                byte[] receipve = new byte[byteBytesRecvd];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);

                string receiveStr = Encoding.Default.GetString(receipve);

                return receiveStr;
            }
        }

        //KEYSIGHT53210
        public class KEYSIGHT53210
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            public KEYSIGHT53210()
            {

            }

            public void Portclose()
            {
                if (client.Connected == true)
                    client.Close();

            }
            public void Portopen()
            {
                if (client.Connected == false)
                {
                    //IPAddress ipAddr = IPAddress.Parse(Setting.KEYSIGHT53210);

                    // IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);

                    //    client.Connect(serverEndPoint);
                }
            }

            public string SelfTest()
            {

                SendData("*IDN?"); ;
                string sRes = ReturnValue();

                return sRes;
            }




            public bool PING()
            {
                bool res = false;

                Ping sender2 = new Ping();
                // PingReply reply = sender2.Send(Setting.KEYSIGHT53210, 1);
                //if (reply.Status == IPStatus.Success)
                //{
                //    res = true;
                //}

                return res;


            }
            public void Config_FREQ()
            {

                SendData("CONF:FREQ");

            }
            public void Config_RAT()
            {

                SendData("CONF:FREQ:RAT");

            }
            public void Config_PERIOD()
            {

                SendData("CONF:PER");

            }
            public string MEASURE_FREQ(int Channel)
            {
                SendData("INP1:LEV:AUTO ON");
                SendData("MEAS:FREQ? " + "(@" + Channel.ToString() + ")");
                string res = ReturnValue();
                return res;
            }
            public string MEASURE_RAT()
            {
                SendData("INP1:LEV:AUTO ON");
                SendData("MEAS:FREQ:RAT?");
                string res = ReturnValue();
                return res;
            }
            public string MEASURE_PERIOD(int Channel)
            {
                SendData("INP1:LEV:AUTO ON");
                SendData("MEAS:PER? " + "(@" + Channel.ToString() + ")");
                string res = ReturnValue();
                return res;
            }

            private void SendData(string sCMD)
            {
                try
                {

                    if (client.Connected)
                    {
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);
                    }
                    else
                    {
                        //   IPAddress ipAddr = IPAddress.Parse(Setting.KEYSIGHT53210);

                        //  IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);

                        //  client.Connect(serverEndPoint);
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);

                    }

                }
                catch
                {

                }
            }

            private string ReturnValue()
            {
                byte[] receipveBUffer = new byte[4096];

                int byteBytesRecvd = client.Receive(receipveBUffer);


                byte[] receipve = new byte[byteBytesRecvd];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);

                string receiveStr = Encoding.Default.GetString(receipve);

                return receiveStr;
            }
        }


        //keysight KEYSIGHT34461
        public class KEYSIGHT34461
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
            public KEYSIGHT34461()
            {

            }
            public string SelfTest()
            {
                SendData("*IDN?"); //GW-Inc
                Thread.Sleep(100);
                return ReturnValue();
            }
            public string Front()
            {
                SendData("ROUT:TERM?"); //GW-Inc
                Thread.Sleep(100);
                return ReturnValue();
            }
            public void Portclose()
            {

                client.DisconnectAsync(arg);


            }
            public void Portopen()
            {
                try
                {

                    // IPAddress ipAddr = IPAddress.Parse(Setting.KEYSIGHT34461);

                    // IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5024);



                    // arg.RemoteEndPoint = serverEndPoint;
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    while (true)
                    {
                        try
                        {
                            client.ConnectAsync(arg);
                            Thread.Sleep(100);
                            string dfdf = SelfTest();
                            if (sw.Elapsed.Seconds >= 10)
                            {
                                MessageBox.Show("DMM Connect Failed");

                                break;
                            }
                            if (dfdf.IndexOf("Keysight") >= 0)
                            {

                                break;
                            }
                            else
                            {

                                Thread.Sleep(100);
                            }

                            client.Dispose();
                            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        }
                        catch
                        {
                            client.Dispose();
                            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        }


                    }


                }
                catch
                {
                    MessageBox.Show("DMM Connect Failed");
                }
            }



            public void SetAutoRange(bool b)
            {
                if (b)
                {
                    SendData(":CONFigure:AUTo 1");
                }
                else
                {
                    SendData(":CONFigure:AUTo 0");
                }

            }

            public void SetCurrentDC()
            {
                SendData(":CONFigure:CURRent:DC 0");
            }

            public void SetCurrentAC()
            {
                SendData(":CONFigure:CURRent:AC 0");
            }

            public void SetCurrentACDC()
            {
                SendData(":CONFigure:CURRent:ACDC 0");
            }

            public void SetDiode()
            {
                SendData(":CONFigure:DIODe");
            }

            public void SetFrequency()
            {
                SendData(":CONFigure:SFRequency");
            }

            public void SetResistance()
            {
                SendData(":CONFigure:RESistance AUTo");

            }

            public void SetVoltageDC()
            {
                SendData(":CONFigure:Voltage:DC 0");
            }

            public void SetVoltageAC()
            {
                SendData(":CONFigure:Voltage:AC 0");
            }

            public void SetVoltageACDC()
            {
                SendData(":CONFigure:Voltage:ACDC 0");
            }

            public void SetVoltageDCAC()
            {
                SendData(":CONFigure:Voltage:DCAC 0");
            }
            public void CHANGE()
            {
                SendData(":INIT");
            }
            public string ReadDC()
            {
                string sRes = "";

                SendData("MEAS:VOLT:DC? 100,0.01");
                Thread.Sleep(20);
                sRes = ReturnValue();
                sRes = sRes.Replace("34461A>", "");
                sRes = sRes.Replace("MEAS:VOLT:DC? 100,0.01", "");
                sRes = sRes.Replace("\r\n", "");
                return sRes;
            }
            public string ReadFRQ()
            {
                string sRes = "";

                SendData("MEAS:FREQ? 100,0.01");
                Thread.Sleep(20);

                sRes = ReturnValue();
                sRes = sRes.Replace("34461A>", "");
                sRes = sRes.Replace("MEAS:FREQ? 100,0.01", "");
                sRes = sRes.Split('\r')[0];
                return sRes;
            }
            public string ReadAC()
            {
                string sRes = "";

                SendData("MEAS:VOLT:AC? 100,0.01");
                Thread.Sleep(50);
                sRes = ReturnValue();
                sRes = sRes.Replace("34461A>", "");
                sRes = sRes.Replace("MEAS:VOLT:AC? 100,0.01", "");
                sRes = sRes.Replace("\r\n", "");
                return sRes;
            }
            public string ReadREGIST()
            {

                string sRes = "";

                SendData("MEAS:RES? 10000,0.01");
                Thread.Sleep(50);
                sRes = ReturnValue();
                sRes = sRes.Replace("34461A>", "");
                sRes = sRes.Replace("MEAS:RES? 10000,0.01", "");
                sRes = sRes.Replace("MEAS:RES? 10000,0.01", "");
                sRes = sRes.Replace("\r\n", "");
                if (sRes == "+9.90000000E+37 ")
                    sRes = "ouer";

                return sRes;
            }
            public string ReadCREGIST()
            {

                string sRes = "";

                SendData("MEAS:RES? 10,0.00001");
                Thread.Sleep(50);
                sRes = ReturnValue();
                sRes = sRes.Replace("34461A>", "");
                sRes = sRes.Replace("MEAS:RES? 10,0.00001", "");
                sRes = sRes.Replace("MEAS:RES? 10,0.00001", "");
                sRes = sRes.Replace("\r\n", "");
                if (sRes == "+9.90000000E+37 ")
                    sRes = "ouer";

                return sRes;
            }
            public string Value()
            {
                string sRes = "";

                SendData(":VALUE?");
                sRes = ReturnValue();
                return sRes;
            }

            public string SValue()
            {
                string sRes = "";

                SendData(":SVALue?");
                sRes = ReturnValue();
                return sRes;
            }


            public bool PING()
            {
                bool res = false;
                try
                {
                    Ping sender2 = new Ping();
                    //  PingReply reply = sender2.Send(Setting.KEYSIGHT34461, 1);
                    //if (reply.Status == IPStatus.Success)
                    //{
                    //    res = true;
                    //}

                    return res;
                }
                catch
                {
                    return res;

                }

            }




            private void SendData(string sCMD)
            {
                try
                {


                    SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                    byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                    arg.SetBuffer(bytesBuff, 0, bytesBuff.Length);

                    client.SendAsync(arg);


                }
                catch
                {

                }
            }

            private string ReturnValue()
            {
                try
                {

                    if (client.Connected)
                    {
                        SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                        byte[] receipveBUffer = new byte[4096];

                        int byteBytesRecvd = client.Receive(receipveBUffer);


                        byte[] receipve = new byte[byteBytesRecvd];
                        Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);

                        string receiveStr = Encoding.Default.GetString(receipve);

                        return receiveStr;
                    }
                    else
                        return "";
                }
                catch
                {
                    return "";
                }
            }
        }

        //AFG2225(Funtion generator)
        public class AFG2225
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public AFG2225() { }


            public AFG2225(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }
            public void AFGSET(int CH, bool SINE, int FRQ, float VOLT, float OFFSET)
            {
                string SIN = "";
                if (SINE)
                    SIN = "SIN";
                else
                    SIN = "SQU";
                if (CH == 1)
                    SendData("SOUR1:APPL:" + SIN + " " + FRQ.ToString() + "HZ," + VOLT.ToString("F1") + "," + OFFSET.ToString("F1") + Constant.CRLF); //GW-Inc
                else
                    SendData("SOUR2:APPL:" + SIN + " " + FRQ.ToString() + "HZ," + VOLT.ToString("F1") + "," + OFFSET.ToString("F1") + Constant.CRLF); //GW-Inc
                Thread.Sleep(300);

            }
            public void AFGSET2(int CH, int DUTY)
            {

                SendData("SOUR1:SQU:DCYC " + DUTY.ToString() + Constant.CRLF); //GW-Inc

                Thread.Sleep(300);

            }

            public void Output1(string ONOFF)
            {
                SendData("OUTP1 " + ONOFF + Constant.CRLF); //GW-Inc

            }
            public void Output2(string ONOFF)
            {
                SendData("OUTP2 " + ONOFF + Constant.CRLF); //GW-Inc

            }


            public string SelfTest()
            {
                SendData("*IDN?" + Constant.CRLF); //GW-Inc
                //Thread.Sleep(300);
                return ReturnValue();
            }


            public string Read()
            {
                string sRes = "";

                SendData(":READ?");
                sRes = ReturnValue();

                return sRes;
            }

            public string Value()
            {
                string sRes = "";

                SendData(":VALUE?");
                sRes = ReturnValue();
                return sRes;
            }

            public string SValue()
            {
                string sRes = "";

                SendData(":SVALue?");
                sRes = ReturnValue();
                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {

                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                    Thread.Sleep(100);
                    string sFlag = sCMD + Constant.LF;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                                        //spCom.Write(sFlag);

                }
                catch
                {

                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;



                sRet += spCom.ReadExisting();



                if (spCom.IsOpen)
                    spCom.Close();

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

        public class MEGURO
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public MEGURO()
            {


            }

            public MEGURO(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 38400;
                spCom.StopBits = StopBits.One;
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
                    // System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public void PortClose()
            {
                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();
                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public string SelfTest()
            {
                string sRes = "";



                SendData("*IDN?"); //ID

                sRes = ReturnValue();
                // sRes = ReturnValue();

                return sRes;
            }

            public string Value()
            {
                string sRes = "";



                SendData("RE?"); //ID
                sRes = ReturnValue();


                return sRes;
            }

            public string Output(string onoff)
            {
                string sRes = "";

                if (onoff == "on" || onoff == "ON" || onoff == "On")
                {
                    SendData("APON"); //ID
                }
                if (onoff == "off" || onoff == "OFF" || onoff == "Off")
                {
                    SendData("APOFF"); //ID
                }
                return sRes;
            }
            public string Rel(string onoff)
            {
                string sRes = "";

                if (onoff == "on" || onoff == "ON" || onoff == "On")
                {
                    SendData("RR1"); //ID
                }
                if (onoff == "off" || onoff == "OFF" || onoff == "Off")
                {
                    SendData("RR0"); //ID
                }
                return sRes;
            }
            public string Volt(string volt)
            {
                string sRes = "";
                SendData("AP" + volt + "DB");
                return sRes;

            }
            public string Frq(string Frq)
            {
                string sRes = "";
                SendData("FR" + Frq + "HZ");
                return sRes;

            }
            public string Mode(string mode)
            {

                string sRes = "";

                if (mode == "1")
                {
                    SendData("MM1"); //ID
                }
                if (mode == "2")
                {
                    SendData("MM2"); //ID
                }
                if (mode == "3")
                {
                    SendData("MM3"); //ID
                }
                return sRes;

            }
            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + Constant.CRLF);

                    Thread.Sleep(80);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
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
                    if (ts.TotalSeconds > 0.05f)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);



                return sRet;
            }
        }
        //패턴분석기CPHD
        public class CPHD
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public CPHD() { }


            public CPHD(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;


            }


            public string SelfTest()
            {

                SendData("URTCNT 0 AD"); //GW-Inc
                Thread.Sleep(50);
                //SendData("TMIX 1 AD 9 ");
                //SendData(" 9 ");
                //SendData("MEAS:FREQ:CEN 1000 MHZ");
                return ReturnValue();
            }

            public string TimingSelection(string sIndex)
            {
                string sRes = "";

                SendData("TMIX 1 AD " + sIndex + " ");
                sRes = ReturnValue();

                return sRes;
            }

            public string PatternSelection(string sIndex)
            {
                string sRes = "";

                SendData("PTIX 1 AD " + sIndex + " ");
                sRes = ReturnValue();

                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;// +Constant.LF;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    //bts[7] = 0;
                    //bts[5] = 1;
                    //bts[10] = 2;
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

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

        //GDM8246(DMM)
        public class GDM8246
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public GDM8246() { }


            public GDM8246(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }


            public string SelfTest()
            {
                SendData("*IDN?" + Constant.CRLF); //GW-Inc
                Thread.Sleep(300);
                return ReturnValue();
            }

            public void CLS()
            {
                SendData("*CLS");
                Close();
            }


            public string IDN()
            {
                SendData("*IDN?"); //GW-Inc

                return ReturnValue();
            }

            public void RST()
            {
                SendData("*RST");
                Close();
            }

            public void SetAutoRange(bool b)
            {
                if (b)
                {
                    SendData(":CONFigure:AUTo 1");
                }
                else
                {
                    SendData(":CONFigure:AUTo 0");
                }

                Close();
            }

            public void SetCurrentDC()
            {
                SendData(":CONFigure:CURRent:DC 0");
                Close();
            }

            public void SetCurrentAC()
            {
                SendData(":CONFigure:CURRent:AC 0");
                Close();
            }

            public void SetCurrentACDC()
            {
                SendData(":CONFigure:CURRent:ACDC 0");
                Close();
            }

            public void SetDiode()
            {
                SendData(":CONFigure:DIODe");
                Close();
            }

            public void SetFrequency()
            {
                SendData(":CONFigure:SFRequency");
                Close();
            }

            public void SetResistance()
            {
                SendData(":CONFigure:RESistance 0");
                Close();
            }

            public void SetVoltageDC()
            {
                SendData(":CONFigure:Voltage:DC 0");
                Close();
            }

            public void SetVoltageAC()
            {
                SendData(":CONFigure:Voltage:AC 0");
                Close();
            }

            public void SetVoltageACDC()
            {
                SendData(":CONFigure:Voltage:ACDC 0");
                Close();
            }

            public void SetVoltageDCAC()
            {
                SendData(":CONFigure:Voltage:DCAC 0");
                Close();
            }

            public string Read()
            {
                string sRes = "";

                SendData(":READ?");
                sRes = ReturnValue();

                return sRes;
            }

            public string Value()
            {
                string sRes = "";

                SendData(":VALUE?");
                sRes = ReturnValue();
                return sRes;
            }

            public string SValue()
            {
                string sRes = "";

                SendData(":SVALue?");
                sRes = ReturnValue();
                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.LF;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;



                sRet += spCom.ReadExisting();



                spCom.Close();

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

        //GDM8246(DMM)
        public class MATSUSADA
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public MATSUSADA() { }


            public MATSUSADA(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }


            public string SelfTest()
            {
                SendData("#01 REN");
                SendData("#01 UNIT?" + Constant.CRLF); //GW-Inc
                Thread.Sleep(50);
                return ReturnValue();
            }



            public void SetCurrent(int addr, float value)
            {
                SendData("#" + addr.ToString() + " REN");
                SendData("#" + addr.ToString() + " ISET " + value.ToString("F3"));
                Close();
            }



            public void SetVoltage(int addr, float value)
            {
                SetCurrent(addr, 1);
                SendData("#" + addr.ToString() + " REN");
                SendData("#" + addr.ToString() + " VSET " + value.ToString("F2"));
                if (value == 0)
                    Output(addr, false);
                else
                    Output(addr, true);
                Close();
            }



            public void Output(int addr, bool onoff)
            {
                if (onoff)
                {
                    SendData("#" + addr.ToString() + " REN");
                    SendData("#" + addr.ToString() + " SW1");
                }

                else
                {
                    SendData("#" + addr.ToString() + " REN");
                    SendData("#" + addr.ToString() + " SW0");
                }

                Close();
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
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(500);
                        if (!spCom.IsOpen)
                        {
                            spCom.Open();
                        }

                        string sFlag = sCMD + Constant.CRLF;
                        byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                        spCom.Write(bts, 0, bts.Length);

                        Thread.Sleep(100);  // Command Execution Time Limit(250)
                        //spCom.Write(sFlag);
                    }
                    catch
                    {

                    }
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;



                sRet += spCom.ReadExisting();



                spCom.Close();

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
        //GDM8246(DMM)
        public class GDM8261A
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public GDM8261A() { }


            public GDM8261A(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }


            public string SelfTest()
            {
                SendData("*IDN?"); //GW-Inc
                Thread.Sleep(50);
                return ReturnValue();
            }

            public void CLS()
            {
                SendData("*CLS");
                Close();
            }


            public string IDN()
            {
                SendData("*IDN?"); //GW-Inc

                return ReturnValue();
            }

            public void RST()
            {
                SendData("*RST");
                Close();
            }

            public void SetAutoRange(bool b)
            {
                if (b)
                {
                    SendData(":CONFigure:AUTo 1");
                }
                else
                {
                    SendData(":CONFigure:AUTo 0");
                }

                Close();
            }

            public void SetCurrentDC()
            {
                SendData(":CONFigure:CURRent:DC 0");
                Close();
            }

            public void SetCurrentAC()
            {
                SendData(":CONFigure:CURRent:AC 0");
                Close();
            }

            public void SetCurrentACDC()
            {
                SendData(":CONFigure:CURRent:ACDC 0");
                Close();
            }

            public void SetDiode()
            {
                SendData(":CONFigure:DIODe");
                Close();
            }

            public void SetFrequency()
            {
                SendData(":CONFigure:SFRequency");
                Close();
            }

            public void SetResistance()
            {
                SendData(":CONFigure:RESistance 0");
                Close();
            }

            public void SetVoltageDC()
            {
                SendData(":CONFigure:Voltage:DC 0");
                Close();
            }

            public void SetVoltageAC()
            {
                SendData(":CONFigure:Voltage:AC 0");
                Close();
            }

            public void SetVoltageACDC()
            {
                SendData(":CONFigure:Voltage:ACDC 0");
                Close();
            }

            public void SetVoltageDCAC()
            {
                SendData(":CONFigure:Voltage:DCAC 0");
                Close();
            }

            public string Read()
            {
                string sRes = "";

                SendData(":READ?");
                sRes = ReturnValue();

                return sRes;
            }

            public string Value()
            {
                string sRes = "";

                SendData(":VALUE?");
                sRes = ReturnValue();
                return sRes;
            }

            public string SValue()
            {
                string sRes = "";

                SendData(":SVALue?");
                sRes = ReturnValue();
                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.LF;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

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
        //내압기 GPI725A
        public class GPI725A
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public GPI725A(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public GPI725A(string sPort, int iBaudRate)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
            }

            public string SelfTest()
            {
                spCom.Open();
                spCom.Write("*IDN?" + Constant.CRLF);
                Thread.Sleep(50);
                return ReturnValue();
            }

            public bool Initialize(int iIRVolt, int iIRMin, int iIRMax, int iIRTime)
            {
                bool bRes = false;

                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();

                    SendData("*CLS");
                    Thread.Sleep(1000);

                    SendData(":FUNC:TEST:MOD 3");
                    Thread.Sleep(1000);

                    SendData(":IRES:VOLT " + iIRVolt.ToString());
                    Thread.Sleep(1000);

                    SendData(":IRES:RMIN " + iIRMin.ToString());
                    //SendData(":IRES:RMIN " + "3");
                    Thread.Sleep(1000);

                    SendData(":IRES:RMAX " + iIRMax.ToString());
                    //SendData(":IRES:RMAX " + 9999.ToString());
                    Thread.Sleep(1000);

                    SendData(":IRES:TTIM " + iIRTime.ToString());
                    Thread.Sleep(1000);

                    bRes = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                return bRes;
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public bool InitializeACW(float iIRVolt, float iIRMin, float iIRMax, int iIRTime)
            {
                bool bRes = false;

                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();

                    SendData("*CLS");
                    Thread.Sleep(1000);

                    SendData(":FUNC:TEST:MOD 1");
                    Thread.Sleep(1000);

                    SendData(":ACWS:VOLT " + (iIRVolt).ToString());
                    Thread.Sleep(1000);

                    SendData(":ACWS:CMIN " + (iIRMin).ToString());
                    Thread.Sleep(1000);

                    //SendData(":ACWS:CMAX " + (iIRMax).ToString());
                    SendData(":ACWS:CMAX " + (5).ToString());
                    Thread.Sleep(1000);

                    SendData(":ACWS:RTIM " + 1.ToString());
                    Thread.Sleep(1000);

                    SendData(":ACWS:TTIM " + iIRTime.ToString());
                    Thread.Sleep(1000);

                    bRes = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                return bRes;
            }

            public string GetInsulationRegist(int iIRTime)
            {
                string sRes = "";
                try
                {

                    SendData(":FUNC:TEST:STAT 1");  //시험시작
                    Thread.Sleep(1000);

                    for (int i = 1; i <= iIRTime; i++)
                    {
                        Application.DoEvents();
                        Thread.Sleep(1000);
                    }
                    spCom.DiscardInBuffer();

                    SendData(":MEAS?"); // 1:ACW, 2:DCW, 3:IR, ~
                    Thread.Sleep(1000);

                    sRes = ReturnValue();

                    SendData("FUNC:TEST:STAT 0");  // 시험초기화
                    Thread.Sleep(1000);
                    spCom.DiscardInBuffer();

                    string[] sResAry = sRes.Split(new char[] { ',' });

                    if (sResAry.Length < 3)
                        return "-1";
                    else
                        return sResAry[3].Trim();  //0:시험상태, 1:시험종류, 2, 3:측정값, 4: 시험시간

                }
                catch (Exception ex)
                {
                    spCom.Close();
                    sRes = ex.Message;
                }

                return sRes;
            }


            public string GetInsulationCompress(float fSec)
            {
                string sRes = "";
                try
                {

                    SendData(":FUNC:TEST:STAT 1");  //시험시작
                    for (int i = 1; i <= (int)fSec + 4; i++)
                    {
                        Application.DoEvents();
                        Thread.Sleep(1000);
                    }
                    spCom.DiscardInBuffer();

                    SendData(":MEAS?"); //  1:ACW, 2:DCW, 3:IR, 
                    sRes = ReturnValue();

                    SendData("FUNC:TEST:STAT 0");  // 시험초기화
                    spCom.DiscardInBuffer();

                    string[] sResAry = sRes.Split(new char[] { ',' });

                    if (sResAry.Length < 3)
                        return "-1";
                    else
                        return sResAry[3].Trim();  //0:시험상태, 1:시험종류, 2, 3:측정값, 4: 시험시간

                }
                catch (Exception ex)
                {
                    spCom.Close();
                    sRes = ex.Message;
                }

                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.LF;
                    spCom.Write(sFlag);

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

                return sRet;
            }

        }

        //스펙트럼분석기 GSP810
        public class GSP810
        {
            public GSP810() { }

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int GetSerialNum();

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int SetFreq(int Freq);

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int SetRBW(int rbw);

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int SetReflvl(int reflvl);

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int SetSpan(int span);

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int ChangeComPort(int ComPort);

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int GetAnalyzerTraceData(ref byte[] btAry);

            //[DllImport("c:\\windows\\system32\\gsp810.dll")]
            //public static extern int GetAnalyzerTraceData(ref Data data);


            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int MarkerToCenter();

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int MarkerToPeak();

            [DllImport("c:\\windows\\system32\\gsp810.dll")]
            public static extern int GetMarkerLevel(int i);
        }

        //DSP-150-010HD 파워
        public class DSP150010HD
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public DSP150010HD()
            {

            }


            public DSP150010HD(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                SendData("SYSTem:REMote");
                SendData("*IDN?"); Thread.Sleep(50);
                string sRes = ReturnValue();
                SendData("SYSTem:LOCal");
                return sRes;
            }

            public float[] GetFetch()
            {
                float[] fValue = new float[2];

                SendData("SYSTem:REMote");
                SendData("FETCh");
                string sValue = ReturnValue();
                SendData("SYSTem:LOCal");


                string[] sValueAry = sValue.Split(new char[] { ',' });
                if (sValueAry.Length >= 2)
                {
                    fValue[0] = float.Parse(sValueAry[0]); //current
                    fValue[1] = float.Parse(sValueAry[1]); //voltage
                }


                return fValue;
            }

            public void OutPut(string sOnOff)
            {
                SendData("SYSTem:REMote");
                SendData("OUTPut " + sOnOff);
                SendData("SYSTem:LOCal");
            }

            public void SetVoltage(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage " + fVolt.ToString());
                SendData("SYSTem:LOCal");
            }

            public void SetCurrent(float fCurr)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent " + fCurr.ToString());
                SendData("SYSTem:LOCal");
            }

            public string GetCurrent()
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent?");
                string sRes = ReturnValue();
                SendData("SYSTem:LOCal");

                return sRes;
            }

            public void SetVoltProtection(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage:LIMit:LOW " + fVolt.ToString("F0"));
                SendData("SYSTem:LOCal");
            }

            public void Reset()
            {
                SendData("SYSTem:REMote");
                SendData("*RST");
                SendData("SYSTem:LOCal");
            }

            public void SetVoltProtectionLevel(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage:PROTection:LEVel " + fVolt.ToString("F0"));
                SendData("SYSTem:LOCal");

            }

            public void SetCurrentProtectionLevel(float fCurr)
            {
                string sCurr;
                if (fCurr >= 999)
                {
                    sCurr = "MIN";
                }
                else
                {
                    sCurr = fCurr.ToString("F0");
                }
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent:PROTection:LEVel " + sCurr);
                SendData("SYSTem:LOCal");
            }


            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + "\n");

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }
        }

        //DSP-150-010HD 파워 485통신
        public class DSP150010HD485
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public DSP150010HD485()
            {

            }


            public DSP150010HD485(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 115200;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest(string addr)
            {
                SendData("A00" + addr + "SYSTem:REMote");
                SendData("A00" + addr + "*IDN?"); Thread.Sleep(50);
                string sRes = ReturnValue();
                SendData("A00" + addr + "SYSTem:LOCal");
                return sRes;
            }

            public float[] GetFetch()
            {
                float[] fValue = new float[2];

                SendData("A001SYSTem:REMote");
                SendData("A001FETCh");
                string sValue = ReturnValue();
                SendData("A001SYSTem:LOCal");


                string[] sValueAry = sValue.Split(new char[] { ',' });
                if (sValueAry.Length >= 2)
                {
                    fValue[0] = float.Parse(sValueAry[0]); //current
                    fValue[1] = float.Parse(sValueAry[1]); //voltage
                }


                return fValue;
            }

            public void OutPut(string sOnOff)
            {
                SendData("A001SYSTem:REMote");
                SendData("A001OUTPut " + sOnOff);
                SendData("A001SYSTem:LOCal");
            }

            public void SetVoltage(float fVolt)
            {
                SendData("A001SYSTem:REMote");
                SendData("A001SOURce:VOLTage " + fVolt.ToString());
                SendData("A001SYSTem:LOCal");
            }

            public void SetCurrent(float fCurr)
            {
                SendData("A001SYSTem:REMote");
                SendData("A001SOURce:CURRent " + fCurr.ToString());
                SendData("A001SYSTem:LOCal");
            }

            public string GetCurrent()
            {
                SendData("A001SYSTem:REMote");
                SendData("A001SOURce:CURRent?");
                string sRes = ReturnValue();
                SendData("A001SYSTem:LOCal");

                return sRes;
            }

            public void SetVoltProtection(float fVolt)
            {
                SendData("A001SYSTem:REMote");
                SendData("A001SOURce:VOLTage:LIMit:LOW " + fVolt.ToString("F0"));
                SendData("A001SYSTem:LOCal");
            }

            public void Reset()
            {
                SendData("A001SYSTem:REMote");
                SendData("A001*RST");
                SendData("A001SYSTem:LOCal");
            }

            public void SetVoltProtectionLevel(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage:PROTection:LEVel " + fVolt.ToString("F0"));
                SendData("SYSTem:LOCal");
            }

            public void SetCurrentProtectionLevel(float fCurr)
            {
                string sCurr;
                if (fCurr >= 999)
                {
                    sCurr = "MIN";
                }
                else
                {
                    sCurr = fCurr.ToString("F0");
                }
                SendData("A001SYSTem:REMote");
                SendData("A001SOURce:CURRent:PROTection:LEVel " + sCurr);
                SendData("A001SYSTem:LOCal");
            }


            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + "\n");

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }
        }

        //DSP-150-010HD 파워 LAN통신
        public class DSP150010HDLAN
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
            public DSP150010HDLAN()
            {

            }

            public void Portclose()
            {
                if (client.Connected == true)
                {
                    SendData("SYSTem:LOCal");
                    client.Close();
                }
            }
            public void Portopen()
            {
                try
                {

                    //   IPAddress ipAddr = IPAddress.Parse(Setting.DSPIP);

                    //  IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);



                    // arg.RemoteEndPoint = serverEndPoint;
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    while (true)
                    {
                        try
                        {
                            client.ConnectAsync(arg);
                            Thread.Sleep(100);
                            string dfdf = SelfTest();
                            if (sw.Elapsed.Seconds >= 10)
                            {
                                MessageBox.Show("DCPOWER Connect Failed");

                                break;
                            }
                            if (dfdf.IndexOf("IDRC") >= 0)
                            {

                                break;
                            }
                            else
                            {

                                Thread.Sleep(100);
                            }

                            client.Dispose();
                            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        }
                        catch
                        {
                            client.Dispose();
                            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        }


                    }


                }
                catch
                {
                    MessageBox.Show("DCPOWER Connect Failed");
                }
            }
            public void SetVoltProtectionLevel(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage:PROTection:LEVel " + fVolt.ToString("F0"));

            }

            public void SetCurrentProtectionLevel(float fCurr)
            {
                string sCurr;
                if (fCurr >= 999)
                {
                    sCurr = "MIN";
                }
                else
                {
                    sCurr = fCurr.ToString("F0");
                }
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent:PROTection:LEVel " + sCurr);
            }
            public string SelfTest()
            {
                SendData("SYSTem:REMote");
                SendData("*IDN?");
                string sRes = ReturnValue();

                return sRes;
            }


            public bool PING()
            {
                bool res = false;

                Ping sender2 = new Ping();
                //    PingReply reply = sender2.Send(Setting.DSPIP, 1);
                //if (reply.Status == IPStatus.Success)
                //{
                //    res = true;
                //}

                return res;


            }
            public void OutPut(string sOnOff)
            {
                SendData("SYSTem:REMote");
                SendData("OUTPut " + sOnOff);

            }

            public void SetVoltage(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage " + fVolt.ToString());

            }

            public void SetCurrent(float fCurr)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent " + fCurr.ToString());

            }



            private void SendData(string sCMD)
            {
                try
                {


                    SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                    byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                    arg.SetBuffer(bytesBuff, 0, bytesBuff.Length);

                    client.SendAsync(arg);


                }
                catch
                {

                }
            }

            private string ReturnValue()
            {
                try
                {

                    if (client.Connected)
                    {
                        SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                        byte[] receipveBUffer = new byte[4096];

                        int byteBytesRecvd = client.Receive(receipveBUffer);


                        byte[] receipve = new byte[byteBytesRecvd];
                        Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);

                        string receiveStr = Encoding.Default.GetString(receipve);

                        return receiveStr;
                    }
                    else
                        return "";
                }
                catch
                {
                    return "";
                }
            }
        }
        public class DSP150010HDLAN2
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
            public DSP150010HDLAN2()
            {

            }

            public void Portclose()
            {
                if (client.Connected == true)
                {
                    SendData("SYSTem:LOCal");
                    client.Close();
                }
            }
            public void Portopen()
            {
                try
                {

                    //  IPAddress ipAddr = IPAddress.Parse(Setting.DSPIP2);

                    //  IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);



                    //   arg.RemoteEndPoint = serverEndPoint;
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    while (true)
                    {
                        try
                        {
                            client.ConnectAsync(arg);
                            Thread.Sleep(100);
                            string dfdf = SelfTest();
                            if (sw.Elapsed.Seconds >= 10)
                            {
                                MessageBox.Show("DCPOWER Connect Failed");

                                break;
                            }
                            if (dfdf.IndexOf("IDRC") >= 0)
                            {

                                break;
                            }
                            else
                            {

                                Thread.Sleep(100);
                            }

                            client.Dispose();
                            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        }
                        catch
                        {
                            client.Dispose();
                            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        }


                    }


                }
                catch
                {
                    MessageBox.Show("DCPOWER Connect Failed");
                }
            }
            public void SetVoltProtectionLevel(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage:PROTection:LEVel " + fVolt.ToString("F0"));

            }

            public void SetCurrentProtectionLevel(float fCurr)
            {
                string sCurr;
                if (fCurr >= 999)
                {
                    sCurr = "MIN";
                }
                else
                {
                    sCurr = fCurr.ToString("F0");
                }
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent:PROTection:LEVel " + sCurr);
            }
            public string SelfTest()
            {
                SendData("SYSTem:REMote");
                SendData("*IDN?");
                string sRes = ReturnValue();

                return sRes;
            }


            public bool PING()
            {
                bool res = false;

                Ping sender2 = new Ping();
                //   PingReply reply = sender2.Send(Setting.DSPIP, 1);
                //if (reply.Status == IPStatus.Success)
                //{
                //    res = true;
                //}

                return res;


            }
            public void OutPut(string sOnOff)
            {
                SendData("SYSTem:REMote");
                SendData("OUTPut " + sOnOff);

            }

            public void SetVoltage(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage " + fVolt.ToString());

            }

            public void SetCurrent(float fCurr)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent " + fCurr.ToString());

            }



            private void SendData(string sCMD)
            {
                try
                {


                    SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                    byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                    arg.SetBuffer(bytesBuff, 0, bytesBuff.Length);

                    client.SendAsync(arg);


                }
                catch
                {

                }
            }

            private string ReturnValue()
            {
                try
                {

                    if (client.Connected)
                    {
                        SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                        byte[] receipveBUffer = new byte[4096];

                        int byteBytesRecvd = client.Receive(receipveBUffer);


                        byte[] receipve = new byte[byteBytesRecvd];
                        Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);

                        string receiveStr = Encoding.Default.GetString(receipve);

                        return receiveStr;
                    }
                    else
                        return "";
                }
                catch
                {
                    return "";
                }
            }
        }


        //TCMS/TGIS
        public class TCMS
        {

            private string PortName;
            private SerialPort spCom;

            public TCMS() { }

            public TCMS(string sPort)
            {

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                string sRes = "";

                return sRes;
            }
        }

        //TCMS/TGIS
        public class TGIS
        {
            private string PortName;
            private SerialPort spCom;

            public TGIS() { }

            public TGIS(string sPort)
            {

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                string sRes = "";

                return sRes;
            }


        }

        // Time Counter
        public class TC2000
        {
            private const char NULL = (char)0x00;   // null
            private const char STX = (char)0x02;   // End of Text
            private const char ETX = (char)0x03; //Convert.ToChar(03);   // End of Text
            private const char EOT = (char)0x04; //Convert.ToChar(04);   // End of Transmission
            private const char ENQ = (char)0x05; //Convert.ToChar(05);   // Enquiry
            private const char ACK = (char)0x06; // Convert.ToChar(06);   // Acknowledge
            private const char NAK = (char)0x15; //Convert.ToChar(21);   // Not Acknowledge

            private const string LF = "\n"; //Convert.ToInt16("10", 16),   // End of Text
            private const string CR = "\r"; //Convert.ToInt16("13", 16),   // End of Text
            private const string CRLF = "\r\n"; //Convert.ToInt16("13", 16) + Convert.ToInt16("10", 16)   // End of Text

            private string PortName;
            private SerialPort spCom;

            public TC2000() { }

            public TC2000(string sPort)
            {

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
                string sRes = TimeCountRead();
                if (sRes != null && sRes.Length > 0 && sRes.IndexOf("R") >= 0)
                {
                    return "TC2000";
                }
                else
                {
                    return "";
                }
            }


            private void SendData(byte[] bts)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(bts.Length * 10);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            static byte[] RevQbuffers = new byte[4096];
            static int RevCount_WPt, RevCount_RPt, ReadCount = 0;
            byte[] btRes;
            byte[] btData = new byte[13];
            private byte[] ReturnValue()
            {


                byte bcc = 0;
                int iLen = 0;

                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0x02) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x52 || ndata == 0x57) // R/W
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 2)
                    {
                        if (ndata == 0x30 || ndata == 0x31) // "0":대기및측정완료, "1":측정중
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 3)
                    {
                        if (ndata == 0x31 || ndata == 0x32 || ndata == 0x33) // 펑션모드("1" ~ "3")
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 4)
                    {
                        if (ndata == 0x30 || ndata == 0x31)  //시간단위("0":us, "1":ms)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount < 10) // 데이터
                    {
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 10)
                    {
                        if (ndata == 0x03)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 11)
                    {
                        bcc = 0;
                        ushort sh3 = CRC16_ENSBoard(btData, 11);
                        bcc = Convert.ToByte(sh3.ToString("X4").Substring(0, 2), 16);

                        if (bcc == ndata)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 12)
                    {
                        bcc = 0;
                        ushort sh4 = CRC16_ENSBoard(btData, 11);
                        bcc = Convert.ToByte(sh4.ToString("X4").Substring(2, 2), 16);

                        if (bcc == ndata)
                        {

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            btRes = new byte[13];
                            for (int x = 0; x < 13; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                System.Threading.Thread.Sleep(100);

                return btData;
            }


            public ushort CRC16_ENSBoard(byte[] btSend, int length)
            {
                ushort crc16 = 0x0000;

                for (int j = 0; j < length; j++)
                {
                    crc16 = CRC16_Update(crc16, btSend[j]);
                }

                return crc16;
            }

            private ushort CRC16_Update(ushort crc16, byte bt)
            {
                int i;

                crc16 ^= bt;
                for (i = 0; i < 8; i++)
                {
                    if ((crc16 & 0x0001) != 0)
                    {
                        crc16 = (ushort)((crc16 >> 1) ^ 0xA001);
                    }
                    else
                    {
                        crc16 = (ushort)(crc16 >> 1);
                    }
                }

                return crc16;
            }


            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string TimeCountRead()
            {
                byte[] btSend = new byte[7];
                btSend[0] = 0x02;  //STX
                btSend[1] = 0x52; //'R'
                btSend[2] = 0x00; //NULL
                btSend[3] = 0x00; // NULL
                btSend[4] = 0x03; // ETX
                btSend[5] = 0x79; //CRC16(상위)
                btSend[6] = 0x29;//CRC16(하위)

                SendData(btSend);

                byte[] btRes = ReturnValue();

                string sRes = System.Text.ASCIIEncoding.ASCII.GetString(btRes);
                //sRes = sRes.Substring(5, 5);
                return sRes;
            }

            public void TimeCountWrite(byte btMode, byte btUnit)
            {

                byte[] btSend = new byte[7];
                btSend[0] = 0x02;  //STX
                btSend[1] = 0x57; //'W'
                btSend[2] = btMode; //Mode(31,32,33)
                btSend[3] = btUnit; // Unit(30, 31)
                btSend[4] = 0x03; // ETX
                ushort usCRC = CRC16_ENSBoard(btSend, 5);
                btSend[5] = Convert.ToByte(usCRC.ToString("X4").Substring(0, 2), 16);//CRC16(상위)
                btSend[6] = Convert.ToByte(usCRC.ToString("X4").Substring(2, 2), 16);//CRC16(하위)

                SendData(btSend);

                byte[] btRes = ReturnValue();
            }
        }

        // Time Counter OLD
        public class EnSTechTimeCounter
        {
            private const char NULL = (char)0x00;   // null
            private const char STX = (char)0x02;   // End of Text
            private const char ETX = (char)0x03; //Convert.ToChar(03);   // End of Text
            private const char EOT = (char)0x04; //Convert.ToChar(04);   // End of Transmission
            private const char ENQ = (char)0x05; //Convert.ToChar(05);   // Enquiry
            private const char ACK = (char)0x06; // Convert.ToChar(06);   // Acknowledge
            private const char NAK = (char)0x15; //Convert.ToChar(21);   // Not Acknowledge

            private const string LF = "\n"; //Convert.ToInt16("10", 16),   // End of Text
            private const string CR = "\r"; //Convert.ToInt16("13", 16),   // End of Text
            private const string CRLF = "\r\n"; //Convert.ToInt16("13", 16) + Convert.ToInt16("10", 16)   // End of Text

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public EnSTechTimeCounter() { }

            public EnSTechTimeCounter(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }

            public string SelfTest()
            {
                return TimeCountRead("1", "0");//("00", "0");
            }


            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(bts.Length * 10);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

                return sRet;
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string TimeCountRead(string sAddr, string sUnit)
            {
                //                  0x02                00~99                     R/W   00000     U/M     0x03               0x0A0D
                //string sCMD = STX.ToString() + sAddr.PadLeft(2, '0') + "R" + "0000" + sUnit + ETX.ToString() + CRLF.ToString();

                //                  0x02                R/W  0~9                        00000      0/1      0x03               0x0A0D
                string sCMD = STX.ToString() + "R" + sAddr.PadLeft(1, '0') + "00000" + sUnit + ETX.ToString() + CRLF.ToString();

                SendData(sCMD);

                return ReturnValue();
            }

            public void TimeCountWrite(string sAddr, string sUnit, string sValue)
            {
                //                  0x02                00~99                     R/W   00000     U/M     0x03               0x0A0D
                //string sCMD = STX.ToString() + sAddr.PadLeft(2, '0') + "R" + "0000" + sUnit + ETX.ToString() + CRLF.ToString();

                //                  0x02                R/W  0~9                        00000      0/1      0x03               0x0A0D
                string sCMD = STX.ToString() + "W" + sAddr.PadLeft(1, '0') + sValue.PadLeft(5, '0') + sUnit + ETX.ToString() + CRLF.ToString();

                SendData(sCMD);

            }

            public string ActionTimeCount()
            {
                //                  0x02                R/W   0~9  00000              0/1    0x03                0x0A0D
                string sCMD = STX.ToString() + "W" + "1" + "A" + "ON00" + "0" + ETX.ToString() + CRLF.ToString();

                SendData(sCMD);

                return ReturnValue();
            }

            public string StopTimeCount()
            {
                //                  0x02                R/W   0~9  0000             0/1    0x03                0x0A0D
                string sCMD = STX.ToString() + "W" + "1" + "A" + "OFF0" + "0" + ETX.ToString() + CRLF.ToString();

                SendData(sCMD);

                return ReturnValue();
            }

            public void TimeCountReset(string sAddr, string sUnit)
            {
                //                  0x02                00~99                     R/W   0000                            U/M     0x03                0x0A0D
                string sCMD = STX.ToString() + sAddr.PadLeft(2, '0') + "R" + "RST" + NULL.ToString() + sUnit + ETX.ToString() + CRLF.ToString();

                SendData(sCMD);
            }

            public void TimeCountStart(string sAddr, string sUnit)
            {
                //                  0x02                00~99                     R/W   0000                            U/M     0x03                0x0A0D
                string sCMD = STX.ToString() + sAddr.PadLeft(2, '0') + "R" + "STR" + NULL.ToString() + sUnit + ETX.ToString() + CRLF.ToString();

                SendData(sCMD);
            }

            public void TimeCountStop(string sAddr, string sUnit)
            {
                //                  0x02                00~99                     R/W   0000                            U/M     0x03                0x0A0D
                string sCMD = STX.ToString() + sAddr.PadLeft(2, '0') + "R" + "STO" + NULL.ToString() + sUnit + ETX.ToString() + CRLF.ToString();

                SendData(sCMD);
            }
        }

        //CF-1000EP AC 파워
        public class CF1000EP
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public CF1000EP()
            {

            }


            public CF1000EP(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                SendData("SYSTem:REMote");

                SendData("*IDN?"); Thread.Sleep(50);
                string sRes = ReturnValue();
                SendData("SYSTem:LOCal");
                return sRes;
            }

            public float[] GetFetch()
            {
                float[] fValue = new float[4];

                SendData("SYSTem:REMote");
                SendData("FETCh?");
                string sValue = ReturnValue();
                SendData("SYSTem:LOCal");


                string[] sValueAry = sValue.Split(new char[] { ',' });
                if (sValueAry.Length >= 4)
                {
                    fValue[0] = float.Parse(sValueAry[0]); //current
                    fValue[1] = float.Parse(sValueAry[1]); //voltage
                    fValue[2] = float.Parse(sValueAry[2]); //voltage
                    fValue[3] = float.Parse(sValueAry[3]); //voltage
                }


                return fValue;
            }

            public void OutPut(string sOnOff)
            {
                SendData("SYSTem:REMote");
                SendData("OUTPut " + sOnOff);
                SendData("SYSTem:LOCal");
            }

            public void SetVoltage(float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage " + fVolt.ToString());
                SendData("SYSTem:LOCal");
            }

            public string GetVoltage()
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:VOLTage?");
                string sRes = ReturnValue();
                SendData("SYSTem:LOCal");

                return sRes;
            }

            public void SetCurrent(float fCurr)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent " + fCurr.ToString());
                SendData("SYSTem:LOCal");
            }

            public void SetFrequency(float fFreq)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:FREQuency " + fFreq.ToString());
                SendData("SYSTem:LOCal");
            }

            public string GetCurrent()
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent?");
                string sRes = ReturnValue();
                SendData("SYSTem:LOCal");

                return sRes;
            }

            public string GetFrequency()
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:FREQuency?");
                string sRes = ReturnValue();
                SendData("SYSTem:LOCal");

                return sRes;
            }

            public void SetVoltProtection(float fRange, float fVolt)
            {
                SendData("SYSTem:REMote");
                //SendData("SOURce:VOLTage:RANGe " + fRange.ToString("F0"));    
                SendData("SOURce:VOLTage:LIMit:HIGH " + fVolt.ToString("F0"));
                SendData("SYSTem:LOCal");
            }

            public void SetFreqProtection(float fRange, float fVolt)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:FREQuency:RANGe " + fRange.ToString("F0"));
                SendData("SOURce:FREQuency:LIMit:HIGH " + fVolt.ToString("F0"));
                SendData("SYSTem:LOCal");
            }

            public void SetCurrentProtection(float fCurr)
            {
                string sCurr;
                if (fCurr >= 999)
                {
                    sCurr = "MIN";
                }
                else
                {
                    sCurr = fCurr.ToString("F0");
                }
                SendData("SYSTem:REMote");
                SendData("SOURce:CURRent:LIMit:HIGH " + sCurr);
                SendData("SYSTem:LOCal");
            }

            public void Reset()
            {
                SendData("SYSTem:REMote");
                SendData("*RST");
                SendData("SYSTem:LOCal");
            }

            public void CLS()
            {
                SendData("SYSTem:REMote");
                SendData("*CLS");
                SendData("SYSTem:LOCal");
            }


            public void RampEnable(string sRAMPZERO)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:RTENable " + sRAMPZERO);
                SendData("SYSTem:LOCal");
            }

            public void RampOutputTime(string sTime)
            {
                SendData("SYSTem:REMote");
                SendData("SOURce:RTIMe:UP " + sTime);
                SendData("SYSTem:LOCal");
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + "\n");

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }
        }

        //PMC-1HS
        public class PMC1HS
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public PMC1HS() { }


            public PMC1HS(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }


            public string SelfTest()
            {
                spCom.Open();

                SendData("VER");
                Thread.Sleep(50);
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

                    string sFlag = sCMD + Constant.CR;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                    len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

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

            public void SetPRG(int iNO)
            {
                SendData("PRG X" + iNO.ToString("00"));
            }

            public void SetJOG(string sDirect, string sX)
            {
                SendData("JOG " + sDirect + sX);
            }

            public void SetPAB(int iX, int iY)
            {
                if (iX != 0 && iY != 0)
                {
                    SendData("PAB " + iX.ToString() + "," + iY.ToString());
                }
                else if (iX != 0)
                {
                    SendData("PAB " + iX.ToString());
                }
                else if (iY != 0)
                {
                    SendData("PAB ," + iY.ToString());
                }
            }

            public void SetPABZero()
            {
                SendData("PAB 0");
            }

            public void SetPIC(int iX, int iY)
            {
                if (iX != 0 && iY != 0)
                {
                    SendData("PIC " + iX.ToString() + "," + iY.ToString());
                }
                else if (iX != 0)
                {
                    SendData("PIC " + iX.ToString());
                }
                else if (iY != 0)
                {
                    SendData("PIC ," + iY.ToString());
                }
            }

            public void SetCLL(string sXY)
            {
                SendData("CLL " + sXY);
            }

            public void SetCLR(string sXY)
            {
                SendData("CLR " + sXY);
            }

            public void SetSPD(int iX, int iY)
            {
                if (iX != 0 && iY != 0)
                {
                    SendData("SPD " + iX.ToString() + "," + iY.ToString());
                }
                else if (iX != 0)
                {
                    SendData("SPD " + iX.ToString());
                }
                else if (iY != 0)
                {
                    SendData("SPD ," + iY.ToString());
                }
            }

            public string GetSPD()
            {
                SendData("SPD");

                return ReturnValue();
            }

            public string GetPOS()
            {
                SendData("POS");

                return ReturnValue();
            }

            public void SetHOM(string sXY)
            {
                SendData("HOM " + sXY);
            }

            public void SetSTO(string sXY)
            {
                SendData("STO " + sXY);
            }

            public string GetVer()
            {
                SendData("VER");

                return ReturnValue();
            }

            public string GetIDC(string sXY)
            {
                SendData("IDC " + sXY);

                return ReturnValue();
            }

            public void SetSSM(string sXY, string sSpeed)
            {
                SendData("SSM " + sXY + sSpeed);
            }

            public string GetINR(string sXY)
            {
                SendData("INR " + sXY);

                return ReturnValue();
            }

            public void SetOUT(string sHex)
            {
                SendData("OUT " + sHex);
            }

            public void SetRST()
            {
                SendData("RST");
            }

            public string GetSCI()
            {
                SendData("SCI");

                return ReturnValue();
            }

            public void SetSCI(string sBaud, string sDataBits, string sStopBit, string sParity)
            {
                SendData("SCI " + sBaud + "," + sDataBits + "," + sStopBit + "," + sParity);
            }

            public void SetOGE(string sXY)
            {
                SendData("OGE " + sXY);
            }

            public void SetPSP(string sXY)
            {
                SendData("PSP " + sXY);
            }

            public void SetEDP(string sXY)
            {
                SendData("EDP " + sXY);
            }

            public void SetPRS(string sXY)
            {
                SendData("PRS " + sXY);
            }

            public void SetPST(string sXY)
            {
                SendData("PST " + sXY);
            }

            public string GetERD(string sXY)
            {
                SendData("ERD " + sXY);

                return ReturnValue();
            }
        }

        //ARSv1
        public class ARSv1
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public ARSv1() { }


            public ARSv1(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 115200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }


            public string SelfTest()
            {
                spCom.Open();

                SendData("<CAT>");
                Thread.Sleep(50);
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

                    string sFlag = sCMD;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                    len = sRet.IndexOf(">");

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

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

            public string[] GetData()
            {
                string sRes = "";

                SendData("<CAH>");

                sRes = ReturnValue();

                if (sRes != "")
                {
                    sRes = sRes.Replace("<", "").Replace(">", "").Replace(" ", "");

                    SendData("<CAE>");

                    return sRes.Split(new char[] { ',' });
                }
                else
                {
                    return null;
                }


            }
        }

        //HM8112
        public class HM8112
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public HM8112()
            {

            }

            public HM8112(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.Handshake = Handshake.XOnXOff;
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
                    // System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public void PortClose()
            {
                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();
                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public string SelfTest()
            {
                string sRes = "";


                SendData("02F0"); //Revision
                Thread.Sleep(50);
                sRes = ReturnValue();


                return sRes;
            }

            public void SetVDC(string sUnit)
            {
                SendData("000" + sUnit); //100mV, 1V, 10V, 100V, 600V 설정
                Thread.Sleep(1000);
            }

            public void SetVAC(string sUnit)
            {
                SendData("001" + sUnit); //100mVDC, 1VDC, 10VDC, 100VDC, 600VDC, spare, 1VAC, 10VAC, 100VAC, 600VAC 설정
                Thread.Sleep(1000);
            }

            public void SetIDC(string sUnit)
            {
                SendData("002" + sUnit); //0.1mA, 1mA, 10mA, 100mA,1A 설정
                Thread.Sleep(1000);
            }

            public void SetIAC(string sUnit)
            {
                SendData("003" + sUnit); //0.1mA, 1mA, 10mA, 100mA,1A 설정
                Thread.Sleep(1000);
            }

            public string GetData()
            {
                string sRes = "0";

                SendData("01A1"); //버퍼사용
                Thread.Sleep(200);
                SendData("01A5"); //자동제거
                Thread.Sleep(200);
                SendData("0198"); // 1개 읽기
                Thread.Sleep(200);
                SendData("01A3"); // 1개 읽기
                Thread.Sleep(200);
                sRes = ReturnValue();
                sRes = sRes.Split(new char[] { '\n' })[0];
                SendData("01A0"); // 버퍼사용중지
                Thread.Sleep(200);
                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + "\n");

                    Thread.Sleep(80);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        //sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }

        }

        //HMF2525
        public class HMF2525
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public HMF2525()
            {

            }

            public HMF2525(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
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
                    // System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public void PortClose()
            {
                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();
                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public string SelfTest()
            {
                string sRes = "";


                SendData("*IDN?"); //ID
                Thread.Sleep(50);
                sRes = ReturnValue();


                return sRes;
            }
            public void SetAMONOFF(string sValue)
            {
                SendData("AM:STATe " + sValue);
            }
            public void SetAMValue(float fValue)
            {
                Function("SINUsoid");
                SetFrequency("990");
                //SetModulation("SINusoid");
                SetModulation("AM", "FREQuency", fValue.ToString("F1"));
                SetAMDepth("80");
            }

            public void RESET()
            {
                SendData("*RST");
            }

            public void Clear()
            {
                SendData("*CLS");
            }

            public void Function(string sConfig)
            {
                SendData("FUNCtion " + sConfig);
            }

            public void OUTPUT(string sOnOff)
            {
                SendData("OUTPut " + sOnOff.ToUpper());
            }

            public void SetFrequency(string sValue)
            {
                SendData("FREQuency " + sValue);
            }

            public void SetPeriod(string sValue)
            {
                SendData("PERiod " + sValue);
            }

            public void SetVoltage(string sValue)
            {
                SendData("VOLTage " + sValue);
            }

            public void SetVoltageLimit(string sDiv, string sValue)
            {
                SendData("VOLTage:" + sDiv.ToUpper() + " " + sValue);
            }

            public void SetVoltageOffset(string sValue)
            {
                SendData("VOLTage:OFFSet " + sValue);
            }

            public void SetSqureDcycle(string sValue)
            {
                SendData("FUNCtion:SQUare:DCYCle " + sValue);
            }

            public void SetSqureRampSymmetry(string sValue)
            {
                SendData("FUNCtion:RAMP:CYMmetry " + sValue);
            }

            public void SetPulseWidth(string sDiv, string sValue)
            {
                SendData("FUNCtion:WIDTh:" + sDiv + " " + sValue);
            }

            public void SetPulseDcycle(string sValue)
            {
                SendData("FUNCtion:PULSe:DCYCle " + sValue);
            }

            public void SetPulseEtime(string sValue)
            {
                SendData("FUNCtion:PULSe:ETIMe " + sValue);
            }

            public void SetModulation(string sDiv, string sDiv2, string sValue)
            {
                //Div:AM, FM, PM, FSK(Func, Freq없음), PWM              
                //Div2:FUNCtion, FREQuency, SOURce, STATe

                if (sDiv2 == "FUNCtion" || sDiv2 == "FREQuency")
                    SendData(sDiv + ":INTernal:" + sDiv2 + " " + sValue);
                else
                    SendData(sDiv + ":" + sDiv2 + " " + sValue);
            }

            public void SetModulation(string sValue)
            {
                SendData("AM:INTernal:FUNCtion " + sValue);
            }

            public void SetAMDepth(string sValue)
            {
                SendData("AM:DEPTh " + sValue);
            }

            public void SetFMDeviation(string sValue)
            {
                SendData("FM:DEViation " + sValue);
            }

            public void SetPMDeviation(string sValue)
            {
                SendData("PM:DEViation " + sValue);
            }

            public void SetFSKFrequency(string sValue)
            {
                SendData("FSK:FREQuency " + sValue);
            }

            public void SetFSKRate(string sValue)
            {
                SendData("FSK:INTernal:RATE " + sValue);
            }

            public void SetFSKDCyle(string sValue)
            {
                SendData("FSK:DCYCle " + sValue);
            }

            public void SetPWMDCyle(string sValue)
            {
                SendData("PWM:DCYCle " + sValue);
            }


            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + "\n");

                    Thread.Sleep(80);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        //sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }
        }

        //카운터
        public class HM8123
        {

            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public HM8123()
            {

            }

            public HM8123(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
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
                    // System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public void PortClose()
            {
                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();
                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public string SelfTest()
            {
                //spCom.Open();
                string sRes = "";


                SendData("IDN"); //ID
                Thread.Sleep(50);
                sRes = ReturnValue();


                return sRes;
            }

            public string RESET()
            {
                spCom.Open();
                string sRes = "";


                SendData("RES"); //ID




                return sRes;
            }

            public string Triger()
            {
                spCom.Open();
                string sRes = "";


                SendData("TRG"); //ID
                return sRes;

            }
            public string Measurement()
            {
                //spCom.Open();
                string sRes = "";


                SendData("LVA?"); //ID
                sRes = ReturnValue();
                return sRes;

            }
            public string Coupling(string sCH, string couple)
            {
                spCom.Open();
                string sRes = "";


                SendData(couple + sCH); //ID




                return sRes;
            }



            public string Attenuator(string sCH, string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("A" + sCH + value); //ID




                return sRes;
            }
            public string Slope(string sCH, string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("S" + sCH + value); //ID




                return sRes;
            }
            public string Lowpass50(string sCH, string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("F" + sCH + value); //ID
                return sRes;
            }

            public string Gatetime(string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("SMT" + value); //ID
                return sRes;
            }




            public string Aramed(string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("AR" + value); //ID
                return sRes;
            }
            public string HOLD(string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("DH" + value); //ID
                return sRes;
            }
            public string NPC(string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("NPC" + value); //ID
                return sRes;
            }

            public string StartCount()
            {
                //spCom.Open();
                string sRes = "";


                SendData("STR"); //ID
                return sRes;
            }

            public string StopCount()
            {
                spCom.Open();
                string sRes = "";


                SendData("STP"); //ID
                return sRes;
            }

            public string Gated(string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("GT" + value); //ID
                return sRes;
            }


            public string impedance(string sCH, string highlow)
            {
                spCom.Open();
                string sRes = "";


                SendData("O" + sCH + highlow); //ID




                return sRes;
            }

            public string TrigerLevel(string sCH, string value)
            {
                spCom.Open();
                string sRes = "";


                SendData("LV" + sCH + value); //ID




                return sRes;
            }



            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + Constant.CR);

                    Thread.Sleep(80);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        //sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }

        }

        //HMO2024
        public class HMO2024
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public HMO2024()
            {


            }

            public HMO2024(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
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
                    // System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public void PortClose()
            {
                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();
                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public string SelfTest()
            {
                string sRes = "";


                SendData("*IDN?"); //ID
                Thread.Sleep(50);
                sRes = ReturnValue();


                return sRes;
            }



            public void RESET()
            {
                SendData("*RST");
            }

            public void Clear()
            {
                SendData("*CLS");
            }


            public string GetMeasureAll(string sCH)
            {


                string sRes = "MEASurement" + sCH + ":ARESult?";
                SendData(sRes);
                sRes = ReturnValue();
                return sRes;


            }

            public string GetMeasureFreq(string sCH)
            {


                string sRes = "MEASurement" + sCH + ":RESult? FREQ";
                SendData(sRes);
                sRes = ReturnValue();
                return sRes;


            }
            public string GetMeasureRMS(string sCH)
            {


                string sRes = "MEASurement" + sCH + ":MAIN RMS";
                SendData(sRes);
                sRes = ReturnValue();
                return sRes;


            }

            public string GetMeasureRMS2(string sCH)
            {


                string sRes = "MEASurement" + sCH + ":RESult? RMS";
                SendData(sRes);
                sRes = ReturnValue();
                return sRes;


            }
            public string AUTOSET(string sCH)
            {


                string sRes = "TRIGger:A:MODE AUTO";
                SendData(sRes);
                sRes = ReturnValue();
                return sRes;

            }


            public string TRIGERAUTO(string sCH)
            {


                string sRes = "ACQuire:WRATe AUTO";
                SendData(sRes);
                sRes = ReturnValue();
                return sRes;

            }
            public string SetDisplayCH(string sCH, string ONF)
            {
                string sRes = "";
                SendData("");
                return sRes;

            }
            public string SetCoupling(string sCH, string ACDC)
            {
                string sRes = "CHANnel" + sCH + ":COUPling " + ACDC;
                SendData(sRes);
                return sRes;

            }

            public string SetScale(string sCH, string value)
            {
                string sRes = "CHANnel" + sCH + ":Scale " + value;
                SendData(sRes);
                return sRes;

            }

            public string SetRange(string sCH, string value)
            {
                string sRes = "CHANnel" + sCH + ":RANGe " + value;
                SendData(sRes);
                return sRes;




            }



            public string SetBandwidth(string sCH, string value)
            {
                string sRes = "CHANnel" + sCH + ":Scale " + value;
                SendData(sRes);
                return sRes;

            }
            public string SetTriggerCH(string sCH)
            {
                string sRes = "";
                SendData("");
                return sRes;

            }
            public string SetCH(string sCH, string onoff)
            {
                string sRes = "CHANnel " + sCH + ":STATe " + onoff;
                SendData(sRes);
                return sRes;

            }
            public string SetTriggerTYPE(string TYPE)
            {
                string sRes = "*";
                SendData("");
                return sRes;

            }

            public string SetTrigger50Level()
            {
                string sRes = "";
                SendData("");
                return sRes;
            }

            public int SetVertical(int scale)
            {
                int sRes = 0;
                SendData("");
                return sRes;
            }
            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + "\n");

                    Thread.Sleep(80);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        //sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }

        }

        //LSINV
        public class LSINV
        {
            //public byte[] btRes = new byte[19];


            static byte[] RevQbuffers = new byte[4096];
            static int RevCount_WPt, RevCount_RPt, ReadCount = 0;

            private string PortName;
            private SerialPort spCom;

            public LSINV() { }

            public LSINV(string sPort)
            {

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = Parity.None;
                spCom.RtsEnable = true;
                spCom.DataBits = 8;
            }

            public byte[] MeterRead()
            {
                byte[] btRes;
                //02303152583050302B3030303030303003ㅠ5
                byte[] btCmd = new byte[18];
                btCmd[0] = (byte)Constant.STX; //시작
                btCmd[1] = (byte)'0'; //국번(01)
                btCmd[2] = (byte)'1';
                btCmd[3] = (byte)'R'; //명령(00)
                btCmd[4] = (byte)'X';
                btCmd[5] = (byte)'0';
                btCmd[6] = (byte)'P';
                btCmd[7] = (byte)'0';
                btCmd[8] = (byte)'+';
                btCmd[9] = (byte)'0';
                btCmd[10] = (byte)'0';
                btCmd[11] = (byte)'0';
                btCmd[12] = (byte)'0';
                btCmd[13] = (byte)'0';
                btCmd[14] = (byte)'0';
                btCmd[15] = (byte)'0';
                btCmd[16] = (byte)Constant.ETX;
                btCmd[17] = 0xB5;

                //int x = 0;
                //while (true)
                //{
                SendData(btCmd);
                Thread.Sleep(200);

                btRes = ReturnMeterData();

                //sRes = ReturnValue();

                //    if (x > 0) break;

                //    x++;
                //}

                return btRes;
            }

            public byte[] ReturnMeterData()
            {
                byte[] btData = new byte[19];
                byte[] btRes = new byte[19];

                int iLen = 0;

                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0x06) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x02) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount <= 16)
                    {
                        if (ndata >= 0x00) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 17)
                    {
                        if (ndata == 0x03) // 
                        {

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            for (int x = 0; x < 18; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;
                    }


                    if (RevCount_WPt > 0 && (RevCount_WPt == RevCount_RPt))
                    {
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                System.Threading.Thread.Sleep(100);

                ////if (spCom.IsOpen)
                ////    spCom.Close();

                return btRes;
            }


            public void InvSetDirection(string sForRev)
            {
                if (sForRev == "F")
                {
                    spCom.Write(Constant.ENQ + "01W00061000271" + Constant.EOT);
                }
                else if (sForRev == "R")
                {
                    spCom.Write(Constant.ENQ + "01W00061000473" + Constant.EOT);
                }
                else
                {
                    spCom.Write(Constant.ENQ + "01W00061000170" + Constant.EOT);
                }

                System.Threading.Thread.Sleep(500);
            }

            public string InvSetHz(string sAddr, string sHz)
            {

                string btResS;
                byte[] btCmd = new byte[16];
                btCmd[0] = (byte)Constant.ENQ; //시작
                btCmd[1] = (byte)'0'; //국번(01)
                btCmd[2] = (byte)'1';
                btCmd[3] = (byte)'W'; //명령(00)
                btCmd[4] = (byte)char.Parse(sAddr.Substring(0, 1)); //번지
                btCmd[5] = (byte)char.Parse(sAddr.Substring(1, 1));
                btCmd[6] = (byte)char.Parse(sAddr.Substring(2, 1));
                btCmd[7] = (byte)char.Parse(sAddr.Substring(3, 1));
                btCmd[8] = (byte)'1'; //개수
                btCmd[9] = (byte)char.Parse(sHz.Substring(0, 1)); //주파수
                btCmd[10] = (byte)char.Parse(sHz.Substring(1, 1));
                btCmd[11] = (byte)char.Parse(sHz.Substring(2, 1));
                btCmd[12] = (byte)char.Parse(sHz.Substring(3, 1));

                byte btSum = 0x00;
                for (int i = 1; i < 13; i++)
                {
                    btSum += btCmd[i];
                }
                btCmd[13] = Convert.ToByte(btSum.ToString("X2").Substring(0, 1), 16);
                btCmd[14] = Convert.ToByte(btSum.ToString("X2").Substring(1, 1), 16);
                btCmd[15] = (byte)Constant.EOT;
                spCom.Write(Constant.ENQ + "01W0381107007C" + Constant.EOT); //0700-->59.7Hz
                btResS = spCom.ReadExisting();


                //SendData(btCmd);
                //Thread.Sleep(200);

                //btRes = ReturnData();

                return btResS;
            }


            public string InvReadCurrent(string sAddr)
            {

                string btResS;
                byte[] btCmd = new byte[12];
                btCmd[0] = (byte)Constant.ENQ; //시작
                btCmd[1] = (byte)'0'; //국번(01)
                btCmd[2] = (byte)'2';
                btCmd[3] = (byte)'R'; //명령(00)
                btCmd[4] = (byte)char.Parse(sAddr.Substring(0, 1)); //번지
                btCmd[5] = (byte)char.Parse(sAddr.Substring(1, 1));
                btCmd[6] = (byte)char.Parse(sAddr.Substring(2, 1));
                btCmd[7] = (byte)char.Parse(sAddr.Substring(3, 1));
                btCmd[8] = (byte)'1'; //개수
                byte btSum = 0x00;
                for (int i = 1; i < 9; i++)
                {
                    btSum += btCmd[i];
                }
                btCmd[9] = Convert.ToByte(btSum.ToString("X2").Substring(0, 1), 16);
                btCmd[10] = Convert.ToByte(btSum.ToString("X2").Substring(1, 1), 16);

                btCmd[11] = (byte)Constant.EOT;


                spCom.Write(Constant.ENQ + "02R00091AE" + Constant.EOT);
                System.Threading.Thread.Sleep(1500);
                btResS = spCom.ReadExisting();


                //SendData(btCmd);
                //Thread.Sleep(200);

                //btRes = ReturnData();

                return btResS;
            }

            public string InvReadVolt(string sAddr)
            {

                string btResS;
                byte[] btCmd = new byte[12];
                btCmd[0] = (byte)Constant.ENQ; //시작
                btCmd[1] = (byte)'0'; //국번(01)
                btCmd[2] = (byte)'2';
                btCmd[3] = (byte)'R'; //명령(00)
                btCmd[4] = (byte)char.Parse(sAddr.Substring(0, 1)); //번지
                btCmd[5] = (byte)char.Parse(sAddr.Substring(1, 1));
                btCmd[6] = (byte)char.Parse(sAddr.Substring(2, 1));
                btCmd[7] = (byte)char.Parse(sAddr.Substring(3, 1));
                btCmd[8] = (byte)'1'; //개수
                byte btSum = 0x00;
                for (int i = 1; i < 9; i++)
                {
                    btSum += btCmd[i];
                }
                btCmd[9] = Convert.ToByte(btSum.ToString("X2").Substring(0, 1), 16);
                btCmd[10] = Convert.ToByte(btSum.ToString("X2").Substring(1, 1), 16);

                btCmd[11] = (byte)Constant.EOT;


                spCom.Write(Constant.ENQ + "02R000B1B7" + Constant.EOT);
                System.Threading.Thread.Sleep(1500);
                btResS = spCom.ReadExisting();


                //SendData(btCmd);
                //Thread.Sleep(200);

                //btRes = ReturnData();

                return btResS;
            }


            /// <summary>
            /// 모니터링하고싶은 주소 등록 메소드
            /// </summary>
            /// <param name="sAddr">번지지정배열("XXXX", "YYYY", "ZZZZ", ...)</param>
            /// <param name="iCount">번지개수</param>
            /// <returns>등록확인여부</returns>
            public byte[] InvMonitorAdd(string[] sAddr, int iCount)
            {
                byte[] btRes;

                byte[] btCmd = new byte[8 + (4 * iCount)];
                btCmd[0] = (byte)Constant.ENQ; //시작
                btCmd[1] = (byte)'0'; //국번(01)
                btCmd[2] = (byte)'2';
                btCmd[3] = (byte)'X'; //명령(00)
                btCmd[4] = (byte)'1'; //개수
                int j = 0;
                for (j = 0; j < iCount; j++)
                {
                    btCmd[5 + (j * 4)] = Convert.ToByte(sAddr[j].Substring(0, 1)); //번지
                    btCmd[6 + (j * 4)] = Convert.ToByte(sAddr[j].Substring(1, 1));
                    btCmd[7 + (j * 4)] = Convert.ToByte(sAddr[j].Substring(2, 1));
                    btCmd[8 + (j * 4)] = Convert.ToByte(sAddr[j].Substring(3, 1));
                }
                j--; //증가된채로 반복문을 탈출하기 때문에...

                byte btSum = 0x00;
                for (int i = 1; i <= 8 + (j * 4); i++)
                {
                    btSum += btCmd[i];
                }
                btCmd[9 + (j * 4)] = Convert.ToByte(btSum.ToString("X2").Substring(0, 1));
                btCmd[10 + (j * 4)] = Convert.ToByte(btSum.ToString("X2").Substring(1, 1));
                btCmd[11 + (j * 4)] = (byte)Constant.EOT;

                SendData(btCmd);
                Thread.Sleep(200);
                btRes = ReturnData1();

                return btRes;
            }

            public byte[] InvMonitorExec(int iCount)
            {
                byte[] btRes;

                byte[] btCmd = new byte[6];
                btCmd[0] = (byte)Constant.ENQ; //시작
                btCmd[1] = (byte)'0'; //국번(01)
                btCmd[2] = (byte)'1';
                btCmd[3] = (byte)'Y'; //명령(00)

                byte btSum = 0x00;
                for (int i = 1; i <= 3; i++)
                {
                    btSum += btCmd[i];
                }
                btCmd[4] = Convert.ToByte(btSum.ToString("X2").Substring(0, 1));
                btCmd[5] = Convert.ToByte(btSum.ToString("X2").Substring(1, 1));
                btCmd[6] = (byte)Constant.EOT;

                SendData(btCmd);
                Thread.Sleep(200);
                btRes = ReturnData2(iCount);

                return btRes;
            }

            public void Open()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            private void SendData(byte[] bts)
            {
                try
                {
                    ////if (!spCom.IsOpen)
                    ////{
                    ////    spCom.Open();
                    ////}

                    spCom.DiscardInBuffer();
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch
                {

                }
            }

            public byte[] ReturnData()
            {
                byte[] btData = new byte[11];
                byte[] btRes = new byte[11];

                int iLen = 0;

                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == Constant.ACK) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == (byte)'0') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 2)
                    {
                        if (ndata == (byte)'1') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 3)
                    {
                        if (ndata == (byte)'R') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount <= 7)
                    {
                        if (ndata >= 0x00) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 8)
                    {
                        if (ndata == GetSum(btData, 1)) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 9)
                    {
                        if (ndata == GetSum(btData, 2))
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 10)
                    {
                        if (ndata == Constant.EOT) // 
                        {

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            for (int x = 0; x < 11; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;
                    }



                    if (RevCount_WPt > 0 && (RevCount_WPt == RevCount_RPt))
                    {
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                System.Threading.Thread.Sleep(100);

                if (spCom.IsOpen)
                    spCom.Close();

                return btRes; ;
            }

            public byte[] ReturnData1()
            {
                byte[] btData = new byte[7];
                byte[] btRes = new byte[7];

                int iLen = 0;

                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == Constant.ACK) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == (byte)'0') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 2)
                    {
                        if (ndata == (byte)'1') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 3)
                    {
                        if (ndata == (byte)'X') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 4)
                    {
                        if (ndata == GetSum(btData, 1)) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 5)
                    {
                        if (ndata == GetSum(btData, 2))
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 6)
                    {
                        if (ndata == Constant.EOT) // 
                        {

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            for (int x = 0; x < 7; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;
                    }

                    if (RevCount_WPt > 0 && (RevCount_WPt == RevCount_RPt))
                    {
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                System.Threading.Thread.Sleep(100);

                if (spCom.IsOpen)
                    spCom.Close();

                return btRes; ;
            }

            public byte[] ReturnData2(int iCount)
            {
                byte[] btData = new byte[7 + (iCount * 4)];
                byte[] btRes = new byte[7 + (iCount * 4)];

                int iLen = 0;

                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == Constant.ACK) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == (byte)'0') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 2)
                    {
                        if (ndata == (byte)'1') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 3)
                    {
                        if (ndata == (byte)'Y') // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount <= 3 + (iCount * 4))
                    {
                        if (ndata >= 0x00) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 4 + (iCount * 4))
                    {
                        if (ndata == GetSum(btData, 1)) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 5 + (iCount * 4))
                    {
                        if (ndata == GetSum(btData, 2))
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 6 + (iCount * 4))
                    {
                        if (ndata == Constant.EOT) // 
                        {

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            for (int x = 0; x < 7 + (iCount * 4); x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;
                    }



                    if (RevCount_WPt > 0 && (RevCount_WPt == RevCount_RPt))
                    {
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                System.Threading.Thread.Sleep(100);

                if (spCom.IsOpen)
                    spCom.Close();

                return btRes; ;
            }


            private byte GetSum(byte[] btData, int iLoc)
            {
                byte btSum = 0x00;

                for (int i = 1; i < btData.Length - 3; i++)
                    btSum += btData[i];

                if (iLoc == 1)
                    btSum = Convert.ToByte(btSum.ToString("X2").Substring(0, 1));
                else
                    btSum = Convert.ToByte(btSum.ToString("X2").Substring(1, 1));

                return btSum;
            }

        }

        public class KPC803A
        {
            private string PortName;
            private SerialPort spCom;

            public KPC803A(string sPort)
            {

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.RtsEnable = true;

            }

            public KPC803A(string sPort, int iBaudRate)
            {

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
                //spCom.RtsEnable = true;

            }

            public bool IsOpen()
            {
                return spCom.IsOpen;
            }

            public void Open()
            {
                if (!spCom.IsOpen)
                {

                    spCom.Open();
                }
            }

            public void Close()
            {
                if (spCom.IsOpen)
                {
                    spCom.Close();
                }
            }

            public string SelfTest()
            {

                byte bt = 0xFF;
                int iCnt = 0;
                while (true)
                {
                    iCnt++;
                    bt = ReturnStatus();
                    Thread.Sleep(100);

                    if (bt != 0x00 || iCnt > 10) break;
                }


                if (bt == 0x11) return Constant.GOOD;
                else return Constant.NG;
            }

            public void STOP()
            {


                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x00; //CMD
                btSend[3] = 0x00; //Data1
                btSend[4] = 0x00; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);


                ReturnValue();
            }

            public void RUN()
            {


                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x01; //CMD
                btSend[3] = 0x00; //Data1
                btSend[4] = 0x00; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);


                ReturnValue();
            }


            public void LoadAData(short shData)
            {

                ushort uData = (ushort)(65535 / 160 * shData);
                byte bt1 = Convert.ToByte(uData.ToString("X4").Substring(0, 2), 16);
                byte bt2 = Convert.ToByte(uData.ToString("X4").Substring(2, 2), 16);

                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x02; //CMD
                btSend[3] = bt1; //Data1
                btSend[4] = bt2; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);


                byte[] bt = ReturnValue();


            }
            public void LoadAData2(ushort shData)
            {

                ushort uData = shData;
                byte bt1 = Convert.ToByte(uData.ToString("X4").Substring(0, 2), 16);
                byte bt2 = Convert.ToByte(uData.ToString("X4").Substring(2, 2), 16);

                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x02; //CMD
                btSend[3] = bt1; //Data1
                btSend[4] = bt2; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);


                byte[] bt = ReturnValue();


            }

            public void LoadCData(short shData)
            {
                ushort uData = (ushort)(65535 / 50 * shData);
                byte bt1 = Convert.ToByte(uData.ToString("X4").Substring(0, 2), 16);
                byte bt2 = Convert.ToByte(uData.ToString("X4").Substring(2, 2), 16);

                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x03; //CMD
                btSend[3] = bt1; //Data1
                btSend[4] = bt2; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);


                ReturnValue();
            }

            public void RESET()
            {


                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x04; //CMD
                btSend[3] = 0x00; //Data1
                btSend[4] = 0x00; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);

                ReturnValue();
            }

            public byte ReturnStatus()
            {


                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x11; //CMD
                btSend[3] = 0x00; //Data1
                btSend[4] = 0x00; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);

                byte[] btRes = ReturnValue();



                return btRes[2];
            }

            public ushort ReturnAData()
            {

                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x12; //CMD
                btSend[3] = 0x00; //Data1
                btSend[4] = 0x00; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);

                byte[] btRes = ReturnValue();


                ushort shRes = Convert.ToUInt16(btRes[3].ToString("X2") + btRes[4].ToString("X2"), 16);
                shRes = (ushort)(160 / 65535.0f * shRes);

                return shRes;

            }

            public ushort ReturnCData()
            {


                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x13; //CMD
                btSend[3] = 0x00; //Data1
                btSend[4] = 0x00; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);

                byte[] btRes = ReturnValue();



                ushort shRes = Convert.ToUInt16(btRes[3].ToString("X2") + btRes[4].ToString("X2"), 16);
                shRes = (ushort)(50 / 65535.0f * shRes);

                return shRes;
            }

            public ushort ReturnCVData()
            {

                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x14; //CMD
                btSend[3] = 0x00; //Data1
                btSend[4] = 0x00; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);

                byte[] btRes = ReturnValue();



                ushort shRes = Convert.ToUInt16(btRes[3].ToString("X2") + btRes[4].ToString("X2"), 16);
                shRes = (ushort)(160 / 65535.0f * shRes);

                return shRes;
            }

            public ushort ReturnCCData()
            {

                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = _btID; //ID
                btSend[2] = 0x15; //CMD
                btSend[3] = 0x00; //Data1
                btSend[4] = 0x00; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);

                byte[] btRes = ReturnValue();

                ushort shRes = Convert.ToUInt16(btRes[3].ToString("X2") + btRes[4].ToString("X2"), 16);
                shRes = (ushort)(50 / 65535.0f * shRes);


                return shRes;
            }

            byte _btID = 0x31;
            public void SetID(byte btID)
            {


                if (btID == 0x00) btID = 0x31;

                _btID = btID;

                byte[] btSend = new byte[7];
                btSend[0] = 0x02; //STX
                btSend[1] = btID; //ID
                btSend[2] = 0x55; //CMD
                btSend[3] = btID; //Data1
                btSend[4] = btID; //Data2
                btSend[5] = (byte)(~(btSend[1] + btSend[2] + btSend[3] + btSend[4]) + 0x01); //CheckSum
                btSend[6] = 0x03; //ETX

                SendData(btSend);

                byte[] btRes = ReturnValue();
            }


            private void SendData(byte[] btCMD)
            {
                try
                {

                    spCom.Write(btCMD, 0, btCMD.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            static byte[] RevQbuffers = new byte[4096];
            static int RevCount_WPt, RevCount_RPt, ReadCount = 0;

            private byte[] ReturnValue()
            {
                byte[] btData = new byte[7];
                byte[] btRes = new byte[7];
                int iLen = 0;

                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0x02) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata >= 0x31) // Board ID
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 2)
                    {
                        if (ndata >= 0x00) // CMD
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 3)
                    {
                        if (ndata >= 0x00) // Data1
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 4)
                    {
                        if (ndata >= 0x00) // Data2
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 5)
                    {
                        if (ndata >= 0x00) // CS
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 6)
                    {
                        if (ndata == 0x03) // ETX
                        {
                            btData[ReadCount] = ndata;

                            for (int x = 0; x < 7; x++)
                                btRes[x] = btData[x];

                        }


                        ReadCount = 0;
                    }



                    if (RevCount_WPt > 0 && (RevCount_WPt == RevCount_RPt))
                    {
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }

                //System.Threading.Thread.Sleep(100);

                return btRes;
            }
        }

        //XT15
        public class XT15
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;
            public XT15()
            {
            }

            public XT15(string sPort)
            {

                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
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
                    // System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public void PortClose()
            {
                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();
                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public string SelfTest()
            {
                string sRes = "";

                SendData("RM1");
                SendData("*IDN?"); //ID
                Thread.Sleep(50);
                sRes = ReturnValue();


                return sRes;
            }
            public void Volt(string Volt)
            {
                SendData("VPP:" + Volt);
            }

            public void RESET()
            {
                SendData("*RST");
            }


            public void SetFRQ(float frq)
            {
                SendData("FRQ:" + frq.ToString());
            }
            public void OUT(float frq)
            {
                SendData("OT1");
            }
            public void OUTOFF(float frq)
            {
                SendData("OT0");
            }
            public void Clear()
            {
                SendData("*CLS");
            }


            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + "\n");

                    Thread.Sleep(80);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        //sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }

        }

        //BS5200
        public class BS5200
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public BS5200() { }

            public BS5200(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }

            public BS5200(string sPort, int iBaudRate)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
            }


            public string SelfTest()
            {
                SendData("1"); Thread.Sleep(50);
                return ReturnValue();
            }


            public string ReadValue()
            {
                string sRes = "0";
                while (true)
                {
                    SendData("1");
                    sRes = ReturnValue();

                    if (sRes.Length > 20)
                    {
                        sRes = sRes.Substring(9, 9).Replace(" ", "");
                        break;
                    }
                    Thread.Sleep(100);
                }
                return sRes;
            }


            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    byte[] bts = new byte[] { 0x01 }; // System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            private string ReturnValue()
            {

                //Open();

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

                Close();

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

        // DC Power XG150
        public class XG150
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public XG150() { }

            public XG150(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }

            public XG150(string sPort, int iBaudRate)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
            }

            public void SetVoltage(float fVolt)
            {
                SendData("*ADR 1");
                SendData(":VOLT " + fVolt.ToString());
            }

            public void SetCurrent(float fCurrent)
            {
                SendData("*ADR 1");
                SendData(":CURR " + fCurrent.ToString());
            }

            public string SelfTest()
            {
                spCom.Open();

                SendData("*ADR 1");
                SendData("*IDN?");
                Thread.Sleep(50);
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

                    string sFlag = sCMD + Constant.CR;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                    len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        // sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

        }

        // Power Supply PSP2010
        public class PSP2010
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public PSP2010(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 2400;
                spCom.DtrEnable = true;
            }

            public PSP2010(string sPort, int iBaudRate)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
                spCom.DtrEnable = true;
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                spCom.Open();
                spCom.Write("V" + Constant.CR);
                Thread.Sleep(50);
                return ReturnValue();
            }

            public string GetPresentOutputVoltage()
            {
                SendData("V");
                string sRes = ReturnValue();

                return sRes;
            }

            public string GetPresentOutputCurrent()
            {
                SendData("A");
                string sRes = ReturnValue();

                return sRes;
            }

            public string GetPresentOutputLoad()
            {
                SendData("W");
                string sRes = ReturnValue();

                return sRes;
            }

            public string GetOutputMaxVoltageLimit()
            {
                SendData("U");
                string sRes = ReturnValue();

                return sRes;
            }

            public string GetOutputMaxCurrentLimit()
            {
                SendData("I");
                string sRes = ReturnValue();

                return sRes;
            }

            public string GetOutputMaxLoadLimit()
            {
                SendData("P");
                string sRes = ReturnValue();

                return sRes;
            }

            public string[] GetPowerSupplyState()
            {
                SendData("F");
                string sRes = ReturnValue();
                string[] sResAry = new string[6];

                if (sRes.Length > 6)
                {
                    sResAry[0] = "Relay Status " + ((sRes.Substring(0, 1) == "0") ? "OFF" : "ON");
                    sResAry[1] = "Temperature Status " + ((sRes.Substring(1, 1) == "0") ? "Normal" : "Overheat");
                    sResAry[2] = "Wheel knob Status " + ((sRes.Substring(2, 1) == "0") ? "Normal" : "Fine");
                    sResAry[3] = "Wheel knob Status " + ((sRes.Substring(3, 1) == "0") ? "Lock" : "UnLock");
                    sResAry[4] = "Remote Status " + ((sRes.Substring(4, 1) == "0") ? "Normal" : "Remote");
                    sResAry[5] = "Lock Status " + ((sRes.Substring(5, 1) == "0") ? "UnLock" : "Lock");
                }

                return sResAry;
            }

            public void SetOutputVoltage(float fVoltage)
            {
                SendData("SV " + fVoltage.ToString("F2").PadLeft(5, '0'));

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetOutputVoltageLimit(int iVoltage)
            {
                SendData("SU " + iVoltage.ToString());

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetOutputCurrentLimit(float fCurrent)
            {
                SendData("SI " + fCurrent.ToString("F2").PadLeft(4, '0'));

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetOutputLoadLimit(int iWatt)
            {
                SendData("SP " + iWatt.ToString());

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetIncreaseVoltage()
            {
                SendData("SV+");

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetDecreaseVoltage()
            {
                SendData("SV-");

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetIncreaseVoltageLimit()
            {
                SendData("SU+");

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetDecreaseVoltageLimit()
            {
                SendData("SU-");

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetIncreaseCurrentLimit()
            {
                SendData("SI+");

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetDecreaseCurrentLimit()
            {
                SendData("SI-");

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetIncreaseLoadLimit()
            {
                SendData("SP+");

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetDecreaseLoadLimit()
            {
                SendData("SP-");

                Thread.Sleep(300);

                spCom.Close();
            }

            public void SetRelayStatus(DeviceTools.MODE_ONOFF oMode)
            {
                if (oMode == MODE_ONOFF.ON)
                {
                    SendData("KOE");
                }
                else
                {
                    SendData("KOD");
                }

                Thread.Sleep(300);

                spCom.Close();
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.CR;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

                return sRet;
            }
        }

        //접촉저항계(mili Ohm)
        public class Hioki3540
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public Hioki3540(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                spCom.Open();
                spCom.Write("RESET" + Constant.CRLF);
                Thread.Sleep(50);
                return ReturnValue();
            }

            public string GetResistanceMeasurement()
            {
                try
                {
                    SendData("AUTO 1");
                    spCom.DiscardInBuffer();

                    SendData("FUNC 0");
                    spCom.DiscardInBuffer();
                    //Thread.Sleep(500);

                    SendData("COMP 0");
                    spCom.DiscardInBuffer();

                    SendData("RMES");
                    string sRes = ReturnValue();

                    if (sRes.IndexOf(",") >= 0)
                    {
                        string[] sAry = sRes.Split(new char[] { ',' });
                        sRes = sAry[0].Trim();
                    }

                    return sRes;
                }
                catch (Exception ex)
                {
                    PortClose();

                    return ex.Message;
                }
            }

            public void SetMeasurementRange(int iValue)
            {
                SendData("RNG " + iValue.ToString());
                string sRes = ReturnValue();

                if (sRes.IndexOf("OK") >= 0)
                    spCom.DiscardInBuffer();
                else
                    MessageBox.Show("측정범위 설정 시 문제가 있습니다.");
            }

            public void SetPowerSpplyFrequency(int iValue)
            {
                SendData("HZ" + iValue.ToString());

                string sRes = ReturnValue();

                if (sRes.IndexOf("OK") >= 0)
                    spCom.DiscardInBuffer();
                else
                    MessageBox.Show("측정범위 설정 시 문제가 있습니다.");
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

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    PortClose();
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

                    len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }

        }

        //Meter Escort3136A
        public class Escort3136A
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public Escort3136A(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public Escort3136A(string sPort, int iBaudRate)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
            }


            public string SelfTest()
            {
                spCom.Open();
                spCom.Write("RV" + Constant.CRLF);
                Thread.Sleep(50);
                return ReturnValue();
            }

            public void SetDCMode()
            {
                SendData("K1");

                Thread.Sleep(1000);
                spCom.DiscardInBuffer();

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }
            }

            public object GetDCVoltage()
            {
                SendData("K1");

                Thread.Sleep(1000);
                spCom.DiscardInBuffer();

                SendData("R1");
                string sRes = ReturnValue();

                object fRes = ValueFilter(sRes);

                return fRes;
            }

            public void Initialize()
            {
                SendData("K5");

                Thread.Sleep(1000);
                spCom.DiscardInBuffer();
            }

            public string GetRegist()
            {

                for (int i = 0; i < 2; i++)
                {
                    SendData("R1");
                    //Thread.Sleep(2000);
                }

                string sRes = ReturnValue();

                sRes = ValueFilter(sRes);

                return sRes;
            }

            private string ValueFilter(string sRes)
            {
                string sValue = sRes.Substring(0, sRes.IndexOf(Constant.CR));
                string sData = "";

                try
                {
                    float fValue = float.Parse(sValue);
                    if (fValue >= 9000000000)
                        fValue = 0.0f;

                    sData = fValue.ToString();
                }
                catch (Exception ex)
                {
                    sData = ex.Message;
                }

                return sData;
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

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

                return sRet;
            }

        }

        //절연저항계(Mega Ohm)
        public class HiokiST5520
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public HiokiST5520(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                spCom.Open();
                spCom.Write("*IDN?" + Constant.CRLF);
                Thread.Sleep(50);
                return ReturnValue();
            }

            public string GetResistanceMeasurement()
            {
                try
                {

                    SendData(":VOLTage 500");   //500
                    spCom.DiscardInBuffer();
                    //Thread.Sleep(500);

                    SendData(":TIMer 3"); //1
                    spCom.DiscardInBuffer();



                    SendData(":STARt");
                    spCom.DiscardInBuffer();

                    //SendData(":STop");
                    //spCom.DiscardInBuffer();
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(1000);
                        Application.DoEvents();
                    }

                    SendData(":MEASure?");
                    string sRes = ReturnValue();

                    if (sRes.IndexOf(",") >= 0)
                    {
                        string[] sAry = sRes.Split(new char[] { ' ' });
                        sRes = sAry[0].Trim();
                    }

                    sRes = sRes.Replace(Constant.CRLF, "");
                    //if (float.Parse(sRes) >= 9999000000)
                    //    sRes = "0";
                    return (float.Parse(sRes) / 1000000).ToString();
                }
                catch (Exception ex)
                {
                    PortClose();

                    return ex.Message;
                }
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

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    PortClose();
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

                    len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }

        //RPMMeter
        public class RPMMeter
        {
            //public byte[] btRes = new byte[19];


            static byte[] RevQbuffers = new byte[4096];
            static int RevCount_WPt, RevCount_RPt, ReadCount = 0;

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public RPMMeter() { }

            public RPMMeter(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = Parity.None;
                //spCom.DtrEnable = true;
                //spCom.Handshake = Handshake.None;
                spCom.RtsEnable = true;
                spCom.DataBits = 8;
            }

            public byte[] MeterRead()
            {

                //02303152583050302B3030303030303003ㅠ5
                byte[] btCmd = new byte[18];
                btCmd[0] = (byte)Constant.STX; //시작
                btCmd[1] = (byte)'0'; //국번(01)
                btCmd[2] = (byte)'2';
                btCmd[3] = (byte)'R'; //명령(00)
                btCmd[4] = (byte)'X';
                btCmd[5] = (byte)'0';
                btCmd[6] = (byte)'P';
                btCmd[7] = (byte)'0';
                btCmd[8] = (byte)'+';
                btCmd[9] = (byte)'0';
                btCmd[10] = (byte)'0';
                btCmd[11] = (byte)'0';
                btCmd[12] = (byte)'0';
                btCmd[13] = (byte)'0';
                btCmd[14] = (byte)'0';
                btCmd[15] = (byte)'0';
                btCmd[16] = (byte)Constant.ETX;
                btCmd[17] = 0xB6;

                //int x = 0;
                //while (true)
                //{
                SendData(btCmd);
                Thread.Sleep(200);

                byte[] btRes = ReturnMeterData();

                //sRes = ReturnValue();

                //    if (x > 0) break;

                //    x++;
                //}

                return btRes;
            }


            public void Open()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }


            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }


            private void SendData(byte[] bts)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.DiscardInBuffer();
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            public byte[] ReturnMeterData()
            {
                byte[] btData = new byte[19];
                byte[] btRes = new byte[19];

                int iLen = 0;

                iLen = spCom.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCom.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0x06) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x02) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount <= 16)
                    {
                        if (ndata >= 0x00) // 
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 17)
                    {
                        if (ndata == 0x03) // 
                        {

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            for (int x = 0; x < 18; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;
                    }


                    if (RevCount_WPt > 0 && (RevCount_WPt == RevCount_RPt))
                    {
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                System.Threading.Thread.Sleep(100);

                if (spCom.IsOpen)
                    spCom.Close();

                return btRes;
            }


            private string ReturnValue()
            {
                string sRet = "";


                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        // sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (sRet.Length >= 18);

                if (spCom.IsOpen)
                    spCom.Close();

                if (sRet.Length >= 18)
                    sRet = sRet.Substring(sRet.IndexOf("RD0P0") + 5, 7);

                return sRet;
            }

        }

        //A34970A
        public class A34970A
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public A34970A(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 4800;
            }

            public A34970A(string sPort, int iBaudRate)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
            }


            public string SelfTest()
            {
                spCom.Open();
                spCom.Write("*IDN?" + Constant.CRLF);
                Thread.Sleep(50);
                return ReturnValue();
            }



            public void Close()
            {
                if (spCom.IsOpen)
                {
                    spCom.Close();
                }
            }

            //public object GetDCVoltage()
            //{
            //    return new object();
            //}

            public object GetVoltage(string sAddr)
            {
                //SendData("K1");

                Thread.Sleep(1000);
                spCom.DiscardInBuffer();

                SendData("ROUT:MON:STATe ON");
                SendData("ROUT:MONitor (@" + sAddr + ")");
                SendData("meas:volt:dc? 10, 0.003, (@" + sAddr + ")");
                SendData("meas:volt:dc? 10, 0.003, (@" + sAddr + ")");

                string sRes = ReturnValue();

                object fRes = ValueFilter(sRes);

                return fRes;
            }

            public string GetDCVoltage(string sAddr)
            {
                //SendData("K1");

                Thread.Sleep(1000);
                spCom.DiscardInBuffer();

                SendData("ROUT:MON:STATe ON");
                SendData("ROUT:MONitor (@" + sAddr + ")");
                SendData("meas:volt:dc? 10, 0.003, (@" + sAddr + ")");
                SendData("meas:volt:dc? 10, 0.003, (@" + sAddr + ")");

                string sRes = ReturnValue();

                object fRes = ValueFilter(sRes);

                return fRes.ToString();
            }



            private string ValueFilter(string sRes)
            {
                string sValue = sRes.Substring(0, sRes.IndexOf(Constant.CR));
                string sData = "";

                try
                {
                    float fValue;
                    bool bCheck = float.TryParse(sValue, out fValue);

                    if (!bCheck || fValue >= 9000000000)
                        fValue = 0.0f;

                    sData = fValue.ToString();
                }
                catch (Exception ex)
                {
                    sData = ex.Message;
                }

                return sData;
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

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

                return sRet;
            }

        }

        //Extech7410
        public class EXTech7410
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public EXTech7410(string sPort)
            {
                iLimitSecond = 0.5f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public EXTech7410(string sPort, int iBaudRate)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
            }

            /// <summary>
            /// 초단위의 시간지연 메소드
            /// </summary>
            /// <param name="iSecond">초</param>
            public void Delay(int iSecond)
            {
                for (int i = 1; i <= iSecond; i++)
                {
                    Thread.Sleep(500);
                }
            }

            public string SelfTest()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                spCom.Write("FQ" + Constant.CRLF);
                Thread.Sleep(50);
                return ReturnValue();
            }

            public void SetIR(float fVolt, float fSec)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("FQ" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("S5 50" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("S6 8" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("FE" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("SS " + fVolt.ToString() + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("SW " + fSec.ToString() + Constant.CRLF);


                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

            }

            public void SetACW(float fVolt, float fSec)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("FQ" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("S5 50" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("S6 8" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("FC" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("SA " + fVolt.ToString() + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("SE " + fSec.ToString() + Constant.CRLF);

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

            }

            public string GetMeasure(float fSec)
            {
                string sRes;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("FA" + Constant.CRLF);

                Delay((int)fSec);

                SendData("?K");
                sRes = ReturnValue();

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("FB" + Constant.CRLF);
                spCom.Write("FR" + Constant.CRLF);

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

                return sRes;
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.LF;
                    spCom.Write(sFlag);

                    Thread.Sleep(1000);  // Command Execution Time Limit(250)
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
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

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }

        }

        //Extech7410
        public class EXTECH6700
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public EXTECH6700(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public EXTECH6700(string sPort, int iBaudRate)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = iBaudRate;
            }

            /// <summary>
            /// 초단위의 시간지연 메소드
            /// </summary>
            /// <param name="iSecond">초</param>
            public void Delay(int iSecond)
            {
                for (int i = 1; i <= iSecond; i++)
                {
                    Thread.Sleep(1000);
                }
            }

            public string SelfTest()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
                spCom.Write("*IDN?" + Constant.CRLF);

                return ReturnValue();
            }

            public void SetIR(float fVolt, float fSec)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("FQ" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("S5 50" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("S6 8" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("FE" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("SS " + fVolt.ToString() + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("SW " + fSec.ToString() + Constant.CRLF);


                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

            }

            public void SetACW(float fVolt, float fSec)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("FQ" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("S5 50" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("S6 8" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("FC" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("SA " + fVolt.ToString() + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("SE " + fSec.ToString() + Constant.CRLF);

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

            }

            public string GetMeasure(float fSec)
            {
                string sRes;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("FA" + Constant.CRLF);

                Delay((int)fSec);

                SendData("?K");
                sRes = ReturnValue();

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("FB" + Constant.CRLF);
                spCom.Write("FR" + Constant.CRLF);

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

                return sRes;
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.LF;
                    spCom.Write(sFlag);

                    Thread.Sleep(1000);  // Command Execution Time Limit(250)
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
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
                    if (ts.TotalSeconds > 0.05)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }
                } while (len == -1);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }

        }

        //스펙트럼 분석기 NEX1
        public class NEX1
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public NEX1(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 38400;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                spCom.Open();
                string sCMD = "*IDN?";
                ////////////////////////////////////////////
                // 보내기
                ////////////////////////////////////////////
                for (int i = 0; i < sCMD.Length; i++)
                {
                    spCom.Write(sCMD.ToCharArray(), i, 1);
                    Thread.Sleep(20);
                }

                byte[] bts = new byte[] { (byte)0x0a };
                for (int i = 0; i < bts.Length; i++)
                {
                    spCom.Write(bts, i, 1);
                    Thread.Sleep(20);
                }
                Thread.Sleep(100);

                return ReturnValue();
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

                    len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }

        //패턴제너레이터 LT447
        public class LT447
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public LT447(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                spCom.Open();
                string sCMD = "@COM, 1";
                ////////////////////////////////////////////
                // 보내기
                ////////////////////////////////////////////
                for (int i = 0; i < sCMD.Length; i++)
                {
                    spCom.Write(sCMD.ToCharArray(), i, 1);
                    Thread.Sleep(20);
                }

                byte[] bts = new byte[] { (byte)0x0d, (byte)0x0a };
                for (int i = 0; i < bts.Length; i++)
                {
                    spCom.Write(bts, i, 1);
                    Thread.Sleep(20);
                }
                Thread.Sleep(100);

                return ReturnValue();
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

                    len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }

        //AO제어
        public class SIVAOCtrl
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public SIVAOCtrl(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            public byte[] SelfTest(byte iID, float iVolt)
            {
                AOCommand(iID, iVolt);
                byte[] btResAry = ReturnValue();

                return btResAry;
            }

            public void Open()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public void AOCommand(byte btID, float fData) //0-10(0-255)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                byte btData;
                if (btID == 9)
                {
                    btData = (byte)(255 / 23.5f * fData); //byte btData = (byte)(255 / 10.0f * fData);
                }
                else
                {
                    btData = (byte)(255 / 23.5f * fData); //byte btData = (byte)(255 / 10.0f * fData);
                }



                byte[] btSend = new byte[8];
                btSend[0] = 0xF2;
                btSend[1] = btID;
                btSend[2] = btData;
                btSend[3] = 0x00;
                btSend[4] = 0x00;
                btSend[5] = 0x00;
                btSend[6] = 0xF3;

                byte btBCC = 0x00;
                for (int i = 0; i < 7; i++)
                {
                    btBCC ^= btSend[i];
                }
                btSend[7] = btBCC;

                for (int i = 0; i < 8; i++)
                {
                    spCom.Write(btSend, i, 1);
                }
                Thread.Sleep(80);
            }

            public byte[] ReturnValue()
            {
                string sRet = "";


                DateTime dtStart = new DateTime(DateTime.Now.Ticks);
                byte[] btResAry = new byte[8];
                int j = 0;
                do
                {
                    Application.DoEvents();

                    if (spCom.BytesToRead > 0)
                    {
                        for (int i = j; i < j + spCom.BytesToRead; i++)
                        {
                            byte btRes = (byte)spCom.ReadByte();
                            btResAry[j++] = btRes;
                        }
                    }

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (btResAry[6] != 0xF3);

                if (spCom.IsOpen)
                    spCom.Close();

                return btResAry;
            }

            public byte[] SetAO(byte btID, float fVolt)
            {
                AOCommand(btID, fVolt);

                return ReturnValue();
            }

        }

        //SIVTest
        public class SIVTest
        {
            private string PortName;
            private SerialPort spCom;


            public SIVTest()
            {

            }



            public SIVTest(string sPort)
            {


                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
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
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public void PortClose()
            {
                try
                {
                    if (spCom.IsOpen)
                        spCom.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            public void SetVoltage(string sCH, float fValue)
            {

                string sCMD = "V" + sCH + fValue.ToString("F0") + "\r";

                spCom.Write(sCMD);
            }

            public void SetFreq(string sCH, float fValue)
            {
                string sCMD = "F" + sCH + fValue.ToString("F0") + "\r";

                spCom.Write(sCMD);
            }

            public void SetPh(string sCH, float fValue)
            {
                string sCMD = "P" + sCH + fValue.ToString("F0") + "\r";

                spCom.Write(sCMD);
            }


        }

        //절연저항계TOS7200
        public class TOS7200
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public TOS7200(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
                spCom.StopBits = StopBits.Two;
                //spCom.Handshake = System.IO.Ports.Handshake.XOnXOff;
            }

            public void PortOpen()
            {
                if (!spCom.IsOpen)
                    spCom.Open();
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                PortOpen();
                spCom.Write("*IDN?" + Constant.CRLF);
                Thread.Sleep(80);

                return ReturnValue();
            }

            public void CLS()
            {
                SendData("*CLS");
            }

            public void RST()
            {
                SendData("*RST");
            }

            public void IDN()
            {
                SendData("*IDN?");
            }

            public string GetMonitor()
            {
                SendData("MON?");

                return ReturnValue();
            }

            public string GetResistanceValue()
            {
                SendData("RDATA?");

                return ReturnValue();
            }

            public string GetTestVoltage()
            {
                SendData("TES?");

                return ReturnValue();
            }

            public void Timer(int iSec, int iOnOff)
            {
                SendData("TIMER " + iSec.ToString() + "," + iOnOff.ToString());
            }

            public void Start()
            {
                SendData("START");
            }

            public void Stop()
            {
                SendData("STOP");
            }

            public void SetTestVoltage(int iVolt)
            {
                SendData("TESTV " + iVolt.ToString());
            }

            public string GetVoltage()
            {
                SendData("VDATA?");

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
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);



                sRet += spCom.ReadExisting();

                len = sRet.IndexOf(Constant.CRLF);



                //  spCom.Close();

                return sRet;
            }
        }

        //GOM802(DMM)
        public class GOM802
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public GOM802() { }


            public GOM802(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }


            public string SelfTest()
            {
                SendData("*IDN?"); //GW-Inc
                Thread.Sleep(50);
                return ReturnValue();
            }

            public void CLS()
            {
                SendData("*CLS");
                Close();
            }


            public string IDN()
            {
                SendData("*IDN?"); //GW-Inc

                return ReturnValue();
            }

            public void RST()
            {
                SendData("*RST");
                Close();
            }

            public void SetAutoRange(bool b)
            {
                if (b)
                {
                    SendData(":CONFigure:AUTo 1");
                }
                else
                {
                    SendData(":CONFigure:AUTo 0");
                }

                Close();
            }

            public void SetCurrentDC()
            {
                SendData(":CONFigure:CURRent:DC 0");
                Close();
            }

            public void SetCurrentAC()
            {
                SendData(":CONFigure:CURRent:AC 0");
                Close();
            }

            public void SetCurrentACDC()
            {
                SendData(":CONFigure:CURRent:ACDC 0");
                Close();
            }

            public void SetDiode()
            {
                SendData(":CONFigure:DIODe");
                Close();
            }

            public void SetFrequency()
            {
                SendData(":CONFigure:SFRequency");
                Close();
            }

            public void SetResistance()
            {
                SendData(":CONFigure:RESistance 0");
                Close();
            }

            public void SetVoltageDC()
            {
                SendData(":CONFigure:Voltage:DC 0");
                Close();
            }

            public void SetVoltageAC()
            {
                SendData(":CONFigure:Voltage:AC 0");
                Close();
            }

            public void SetVoltageACDC()
            {
                SendData(":CONFigure:Voltage:ACDC 0");
                Close();
            }

            public void SetVoltageDCAC()
            {
                SendData(":CONFigure:Voltage:DCAC 0");
                Close();
            }

            public string Read()
            {
                string sRes = "";

                SendData(":READ?");
                sRes = ReturnValue();

                return sRes;
            }

            public string Value()
            {
                string sRes = "";

                SendData(":VALUE?");
                sRes = ReturnValue();
                return sRes;
            }

            public string SValue()
            {
                string sRes = "";

                SendData(":SVALue?");
                sRes = ReturnValue();
                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.LF;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

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

        public class MP9201
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public MP9201(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                spCom.Open();
                spCom.Write(":SYST:VERS?" + Constant.CRLF);

                return ReturnValue();
            }

            public string Start()
            {
                spCom.Open();
                spCom.Write(":SOUR:SAFE:START" + Constant.CRLF); return ReturnValue();
            }
            public string Stop()
            {
                spCom.Open();
                spCom.Write(":SOUR:SAFE:STOP" + Constant.CRLF); return ReturnValue();
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

                    Thread.Sleep(300);  // Command Execution Time Limit(250)
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    PortClose();
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
                    len = sRet.IndexOf(Constant.CR);
                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > 0.05f)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }
                } while (len == -1);





                spCom.Close();

                return sRet;
            }
        }

        //DACELL DN-50W
        public class DACELL
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public DACELL() { }

            public DACELL(string sPort)
            {
                iLimitSecond = 0.5f;

                PortName = sPort;
                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
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
                Thread.Sleep(50);
                return ReturnValue();
            }


            public string ReadValue()
            {
                SendData("ID01P");
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

                    len = sRet.IndexOf(Constant.CR);

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

        //FLUKE8246(DMM)
        public class FLUKE8246
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public FLUKE8246() { }


            public FLUKE8246(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }


            public string SelfTest()
            {
                SendData("*IDN?"); //GW-Inc
                Thread.Sleep(50);
                return ReturnValue();
            }

            public void CLS()
            {
                SendData("*CLS");
                Close();
            }


            public string IDN()
            {
                SendData("*IDN?"); //GW-Inc

                return ReturnValue();
            }

            public void RST()
            {
                SendData("*RST");
                Close();
            }

            public void SetAutoRange(bool b)
            {
                if (b)
                {
                    SendData(":CONFigure:AUTo 1");
                }
                else
                {
                    SendData(":CONFigure:AUTo 0");
                }

                Close();
            }

            public void SetCurrentDC()
            {
                SendData(":CONFigure:CURRent:DC 0");
                Close();
            }

            public void SetCurrentAC()
            {
                SendData(":CONFigure:CURRent:AC 0");
                Close();
            }

            public void SetCurrentACDC()
            {
                SendData(":CONFigure:CURRent:ACDC 0");
                Close();
            }

            public void SetDiode()
            {
                SendData(":CONFigure:DIODe");
                Close();
            }

            public void SetFrequency()
            {
                SendData(":CONFigure:SFRequency");
                Close();
            }

            public void SetResistance()
            {
                SendData(":CONFigure:RESistance 0");
                Close();
            }

            public void SetVoltageDC()
            {
                SendData(":CONFigure:Voltage:DC 0");
                Close();
            }

            public void SetVoltageAC()
            {
                SendData(":CONFigure:Voltage:AC 0");
                Close();
            }

            public void SetVoltageACDC()
            {
                SendData(":CONFigure:Voltage:ACDC 0");
                Close();
            }

            public void SetVoltageDCAC()
            {
                SendData(":CONFigure:Voltage:DCAC 0");
                Close();
            }

            public string Read()
            {
                string sRes = "";

                SendData(":READ?");
                sRes = ReturnValue();

                return sRes;
            }

            public string Value()
            {
                string sRes = "";

                SendData(":VALUE?");
                sRes = ReturnValue();
                return sRes;
            }

            public string SValue()
            {
                string sRes = "";

                SendData(":SVALue?");
                sRes = ReturnValue();
                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.LF;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

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

        //KEYSIGHT34970(DMM)
        public class KEYSIGHT34970
        {

            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public KEYSIGHT34970() { }


            public KEYSIGHT34970(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;
            }


            public string SelfTest()
            {
                SendData("*IDN?"); //GW-Inc
                Thread.Sleep(50);
                return ReturnValue();
            }

            public void CLS()
            {
                SendData("*CLS");
                Close();
            }


            public string IDN()
            {
                SendData("*IDN?"); //GW-Inc

                return ReturnValue();
            }

            public void RST()
            {
                SendData("*RST");
                Close();
            }

            public void SetAutoRange(bool b)
            {
                if (b)
                {
                    SendData(":CONFigure:AUTo 1");
                }
                else
                {
                    SendData(":CONFigure:AUTo 0");
                }

                Close();
            }

            public void SetCurrentDC()
            {
                SendData(":CONFigure:CURRent:DC 0");
                Close();
            }

            public void SetCurrentAC()
            {
                SendData(":CONFigure:CURRent:AC 0");
                Close();
            }

            public void SetCurrentACDC()
            {
                SendData(":CONFigure:CURRent:ACDC 0");
                Close();
            }

            public void SetDiode()
            {
                SendData(":CONFigure:DIODe");
                Close();
            }

            public void SetFrequency()
            {
                SendData(":CONFigure:SFRequency");
                Close();
            }

            public void SetResistance()
            {
                SendData(":CONFigure:RESistance 0");
                Close();
            }

            public void SetVoltageDC()
            {
                SendData(":CONFigure:Voltage:DC 0");
                Close();
            }

            public void SetVoltageAC()
            {
                SendData(":CONFigure:Voltage:AC 0");
                Close();
            }

            public void SetVoltageACDC()
            {
                SendData(":CONFigure:Voltage:ACDC 0");
                Close();
            }

            public void SetVoltageDCAC()
            {
                SendData(":CONFigure:Voltage:DCAC 0");
                Close();
            }

            public string Read()
            {
                string sRes = "";

                SendData(":READ?");
                sRes = ReturnValue();

                return sRes;
            }

            public string Value()
            {
                string sRes = "";

                SendData(":VALUE?");
                sRes = ReturnValue();
                return sRes;
            }

            public string SValue()
            {
                string sRes = "";

                SendData(":SVALue?");
                sRes = ReturnValue();
                return sRes;
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD + Constant.LF;
                    byte[] bts = System.Text.Encoding.Default.GetBytes(sFlag.ToCharArray());
                    spCom.Write(bts, 0, bts.Length);

                    Thread.Sleep(100);  // Command Execution Time Limit(250)
                    //spCom.Write(sFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                spCom.Close();

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

        //고전류발생기
        public class AMETEK
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public AMETEK()
            {

            }


            public AMETEK(string sPort)
            {
                iLimitSecond = 5;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
                //spCom.RtsEnable = true;

            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {
                string sRes = "";

                SendData("*IDN?");

                sRes = ReturnValue();

                return sRes;
            }

            public void Reset()
            {
                SendData("*RST");
            }

            public void CLS()
            {
                SendData("*CLS");
            }

            public string GetMeasure(string sType)
            {
                string sRes = "";

                if (sType.ToUpper().IndexOf("CURR") >= 0)
                {
                    SendData("MEASure:CURRent?");

                    sRes = ReturnValue();
                }
                else if (sType.ToUpper().IndexOf("VOLT") >= 0)
                {
                    SendData("MEASure:VOLTage?");

                    sRes = ReturnValue();
                }
                else
                {
                    sRes = "";
                }

                return sRes;
            }

            public bool SetSource(string sType, string sValue)
            {
                bool bRes = false;
                string sRes = "";

                for (int i = 0; i < 3; i++)
                {
                    if (sType.ToUpper().IndexOf("CURR") >= 0)
                    {
                        SendData("SOURce:CURRent " + sValue);
                        Thread.Sleep(1000);

                        SendData("SOURce:CURRent?");
                        sRes = ReturnValue();
                        if (sRes.Length > 0)
                            bRes = true;

                    }
                    else if (sType.ToUpper().IndexOf("VOLT") >= 0)
                    {
                        SendData("SOURce:VOLTage " + sValue);
                        Thread.Sleep(1000);

                        SendData("SOURce:VOLTage?");
                        sRes = ReturnValue();
                        if (sRes.Length > 0)
                            bRes = true;
                    }
                    else
                    {
                        bRes = false;
                    }

                    if (bRes) break;
                }

                return bRes;
            }

            public void Output(string sOnOff)
            {
                if (sOnOff.ToUpper().IndexOf("ON") >= 0)
                {
                    SendData("OUTPut:STATe 1");
                }
                else
                {
                    SendData("OUTPut:STATe 0");
                }
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + "\r");

                    Thread.Sleep(200);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }
        }

        //DCS150 파워
        public class DCS150
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public DCS150()
            {

            }


            public DCS150(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 19200;
                // spCom.Handshake = Handshake.XOnXOff;
                //spCom.Handshake = true;


            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            public string SelfTest()
            {

                SendData("*IDN?"); Thread.Sleep(50);
                string sRes = ReturnValue();

                return sRes;
            }

            public float[] GetFetch()
            {
                float[] fValue = new float[2];


                SendData("FETCh");
                string sValue = ReturnValue();


                string[] sValueAry = sValue.Split(new char[] { ',' });
                if (sValueAry.Length >= 2)
                {
                    fValue[0] = float.Parse(sValueAry[0]); //current
                    fValue[1] = float.Parse(sValueAry[1]); //voltage
                }


                return fValue;
            }

            public void OutPut(string sOnOff)
            {

                SendData("OUTPut " + sOnOff);

            }

            public void SetVoltage(float fVolt)
            {

                SendData("SOURce:VOLTage " + fVolt.ToString());

            }

            public void SetCurrent(float fCurr)
            {
                SendData("SOURce:CURRent " + fCurr.ToString());
            }

            public string GetCurrent()
            {
                SendData("SOURce:CURRent?");
                string sRes = ReturnValue();

                return sRes;
            }

            public void SetVoltProtection(float fVolt)
            {
                SendData("SOURce:VOLTage:LIMit:LOW " + fVolt.ToString("F0"));
            }

            public void Reset()
            {
                SendData("*RST");
            }

            public void SetVoltProtectionLevel(float fVolt)
            {
                SendData("SOURce:VOLTage:PROTection:LEVel " + fVolt.ToString("F0"));

            }

            public void SetCurrentProtectionLevel(float fCurr)
            {
                string sCurr;
                if (fCurr >= 999)
                {
                    sCurr = "MIN";
                }
                else
                {
                    sCurr = fCurr.ToString("F0");
                }
                SendData("SOURce:CURRent:PROTection:LEVel " + sCurr);
            }


            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag);

                    spCom.Write(Constant.EOF.ToString());
                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }
        }

        //NEW AO
        public class AO
        {

            public byte[] btRes = new byte[15];
            public byte[] btSend = new byte[15];

            static byte[] RevQbuffers = new byte[4096];
            static int RevCount_WPt, RevCount_RPt, ReadCount = 0;



            System.IO.Ports.SerialPort spCOM;

            public AO() { }

            public AO(string sPort)
            {
                spCOM = new System.IO.Ports.SerialPort();
                spCOM.PortName = sPort;
                spCOM.BaudRate = 38400; //38400; //

            }

            public bool SelfTest(byte ch)
            {
                bool sRes = false;

                string svolt1 = "00.00";

                string[] voltAry1 = svolt1.Split('.');
                string svolt2 = "00.00";
                string[] voltAry2 = svolt2.Split('.');

                btSend[0] = 0x02; //STX
                btSend[1] = ch; //BoardID
                btSend[2] = 0x57;//Read/Write
                btSend[3] = byte.Parse(voltAry1[0], System.Globalization.NumberStyles.HexNumber);
                btSend[4] = byte.Parse(voltAry1[1], System.Globalization.NumberStyles.HexNumber);

                btSend[5] = byte.Parse(voltAry2[0], System.Globalization.NumberStyles.HexNumber);
                btSend[6] = byte.Parse(voltAry2[1], System.Globalization.NumberStyles.HexNumber);
                btSend[7] = 0x03;
                byte btBCC = 0x00;
                for (int i = 0; i < 8; i++)
                {
                    btBCC ^= btSend[i];
                }
                btSend[8] = btBCC;


                for (int i = 0; i < 3; i++)
                {
                    SendData();

                    sRes = ReturnData();

                    if (sRes) break;
                }

                return sRes;


            }

            public bool SetVolt(byte ch, float volt1, float volt2)
            {
                bool sRes = false;

                string svolt1 = volt1.ToString("F2");

                string[] voltAry1 = svolt1.Split('.');
                string svolt2 = volt2.ToString("F2");
                string[] voltAry2 = svolt2.Split('.');

                btSend[0] = 0x02; //STX
                btSend[1] = ch; //BoardID
                btSend[2] = 0x57;//Read/Write
                btSend[3] = byte.Parse(voltAry1[0], System.Globalization.NumberStyles.HexNumber);
                btSend[4] = byte.Parse(voltAry1[1], System.Globalization.NumberStyles.HexNumber);

                btSend[5] = byte.Parse(voltAry2[0], System.Globalization.NumberStyles.HexNumber);
                btSend[6] = byte.Parse(voltAry2[1], System.Globalization.NumberStyles.HexNumber);
                btSend[7] = 0x03;
                byte btBCC = 0x00;
                for (int i = 0; i < 8; i++)
                {
                    btBCC ^= btSend[i];
                }
                btSend[8] = btBCC;


                for (int i = 0; i < 3; i++)
                {
                    SendData();

                    sRes = ReturnData();

                    if (sRes) break;
                }

                return sRes;


            }

            public void PortOpen()
            {
                if (spCOM.IsOpen) spCOM.Close();

                if (!spCOM.IsOpen)
                {
                    spCOM.Open();
                }
            }

            public void PortClose()
            {
                if (spCOM.IsOpen)
                {
                    spCOM.DiscardInBuffer(); //입력버퍼 클리어
                    spCOM.DiscardOutBuffer(); //출력버퍼 클리어
                    spCOM.Close();
                }
            }

            public void SendData()
            {
                for (int i = 0; i < 9; i++)
                {
                    spCOM.Write(btSend, i, 1);
                }
                System.Threading.Thread.Sleep(50);
            }

            public bool ReturnData()
            {
                bool bRes = false;
                byte[] btData = new byte[10];


                int iLen = 0;

                iLen = spCOM.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCOM.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == btSend[ReadCount]) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == btSend[ReadCount]) // Board ID(PWM)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 2)
                    {
                        if (ndata == btSend[ReadCount]) // Board ID(PWM)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 3)
                    {
                        if (ndata == btSend[ReadCount]) // Board ID(PWM)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 4)
                    {
                        if (ndata == btSend[ReadCount]) // Board ID(PWM)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 5)
                    {
                        if (ndata == btSend[ReadCount]) // Board ID(PWM)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 6)
                    {
                        if (ndata == btSend[ReadCount]) // Board ID(PWM)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 7)
                    {
                        if (ndata == btSend[ReadCount]) // Board ID(PWM)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }


                    else if (ReadCount == 8)
                    {


                        if (ndata == btSend[ReadCount]) // Board ID(PWM)
                        {
                            bRes = true;

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            for (int x = 0; x < 10; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }


                    if (RevCount_WPt > 0 && (RevCount_WPt == RevCount_RPt))
                    {
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                System.Threading.Thread.Sleep(100);

                return bRes;
            }






        }

        //PWM 속도제어
        public class ENSBoard
        {
            public byte[] btDIRes = new byte[12];
            public byte[] btDORes = new byte[12];
            public byte[] btRes = new byte[15];
            public byte[] btSend = new byte[15];

            static byte[] RevQbuffers = new byte[4096];
            static int RevCount_WPt, RevCount_RPt, ReadCount = 0;

            static byte[] btDO1Ary = new byte[8];
            static byte[] btDO2Ary = new byte[8];

            System.IO.Ports.SerialPort spCOM;


            public ENSBoard() { }

            public ENSBoard(string sPort)
            {
                spCOM = new System.IO.Ports.SerialPort();
                spCOM.PortName = sPort;
                spCOM.BaudRate = 38400; //38400; //
                spCOM.RtsEnable = true;
            }


            #region PWM
            public bool SetPWM(byte btRunMode, byte btDirection, short shWheelSize, short shSpeed, byte btTacho)
            {
                bool sRes = false;

                ///////////////////////////////////////////////////////////////////
                btSend[0] = 0x02; //STX
                btSend[1] = 0x31; //BoardID
                btSend[2] = 0x00;//Address
                btSend[3] = 0x00;//Read/Write
                btSend[4] = 0x07;//Data Length
                btSend[5] = 0x00; //(사용안함) btRunMode;//Data1
                btSend[6] = btDirection;//Data2
                btSend[7] = Convert.ToByte(shWheelSize.ToString("X4").Substring(0, 2), 16);//Data3 Wheel Size High
                btSend[8] = Convert.ToByte(shWheelSize.ToString("X4").Substring(2, 2), 16); ;//Data4 Wheel Size Low
                btSend[9] = Convert.ToByte(shSpeed.ToString("X4").Substring(0, 2), 16); //Data5 Speed High
                btSend[10] = Convert.ToByte(shSpeed.ToString("X4").Substring(2, 2), 16);//Data6 Speed Low
                btSend[11] = btTacho;//Data7
                btSend[12] = 0x03;
                ushort sh3 = Constant.CRC16_ENSBoard(btSend, 13);
                btSend[13] = Convert.ToByte(sh3.ToString("X4").Substring(0, 2), 16);
                btSend[14] = Convert.ToByte(sh3.ToString("X4").Substring(2, 2), 16);
                ////////////////////////////////////////////////////////////////////////////

                SendData();

                sRes = ReturnData();

                return sRes;
            }

            public bool SetPWM(byte btRunMode, byte btDirection, byte btTachoCH, short shFrequency, byte btTacho)
            {
                bool sRes = false;

                ///////////////////////////////////////////////////////////////////
                btSend[0] = 0x02; //STX
                btSend[1] = 0x31; //BoardID
                btSend[2] = 0x00;//Address
                btSend[3] = 0x01;//Read/Write
                btSend[4] = 0x07;//Data Length
                btSend[5] = 0x00; //사용안함(주파수 0이면 정지) btRunMode;//Data1
                btSend[6] = btDirection;//Data2
                btSend[7] = btTachoCH;//Data3 Tacho Channel
                btSend[8] = 0x00;//Data4 (스페어)
                btSend[9] = Convert.ToByte(shFrequency.ToString("X4").Substring(0, 2), 16); //Data5 Frequency High
                btSend[10] = Convert.ToByte(shFrequency.ToString("X4").Substring(2, 2), 16);//Data6 Frequency Low
                btSend[11] = btTacho;//Data7(Tacho Power State)
                btSend[12] = 0x03;
                ushort sh3 = Constant.CRC16_ENSBoard(btSend, 13);
                btSend[13] = Convert.ToByte(sh3.ToString("X4").Substring(0, 2), 16);
                btSend[14] = Convert.ToByte(sh3.ToString("X4").Substring(2, 2), 16);
                ////////////////////////////////////////////////////////////////////////////

                for (int i = 0; i < 3; i++)
                {
                    SendData();

                    sRes = ReturnData();

                    if (sRes) break;
                }

                return sRes;
            }

            public bool SetPWM(short shSpeed)
            {
                //(속도 * 1000) / ((휠 / 1000 * 3.14) / 200) / 3600 * 10
                short shFrequency = (short)((shSpeed * 1000) / ((0.86f * 3.141592f) / 200.0f) / 3600.0f * 10);

                //shFrequency = 3000;

                bool sRes = false;

                ///////////////////////////////////////////////////////////////////
                btSend[0] = 0x02; //STX
                btSend[1] = 0x31; //BoardID
                btSend[2] = 0x01;//Address
                btSend[3] = 0x01;//Read/Write
                btSend[4] = 0x07;//Data Length
                btSend[5] = 0x00; //사용안함(주파수 0이면 정지) btRunMode;//Data1
                btSend[6] = 0x00;//Data2(0x00:F, 0x01:R)
                btSend[7] = 0x0F;//Data3 Tacho Channel
                btSend[8] = 0x00;//Data4 (스페어)
                btSend[9] = Convert.ToByte(shFrequency.ToString("X4").Substring(0, 2), 16); //Data5 Frequency High
                btSend[10] = Convert.ToByte(shFrequency.ToString("X4").Substring(2, 2), 16);//Data6 Frequency Low
                btSend[11] = 0x01;//Data7(Tacho Power State)
                btSend[12] = 0x03;
                ushort sh3 = Constant.CRC16_ENSBoard(btSend, 13);
                btSend[13] = Convert.ToByte(sh3.ToString("X4").Substring(0, 2), 16);
                btSend[14] = Convert.ToByte(sh3.ToString("X4").Substring(2, 2), 16);
                ////////////////////////////////////////////////////////////////////////////

                for (int i = 0; i < 3; i++)
                {
                    SendData();

                    sRes = ReturnData();

                    if (sRes) break;
                }

                return sRes;
            }

            #endregion

            public bool SelfTest()
            {
                bool sRes = false;
                try
                {
                    ///////////////////////////////////////////////////////////////////
                    PortOpen();
                    sRes = SetPWM(10);
                    PortClose();
                }
                catch
                {
                    PortClose();
                }

                return sRes;
            }

            #region 정보 요청/응답
            public void PortOpen()
            {
                if (spCOM.IsOpen) spCOM.Close();

                if (!spCOM.IsOpen)
                {
                    spCOM.Open();
                }
            }

            public void PortClose()
            {
                if (spCOM.IsOpen)
                {
                    spCOM.DiscardInBuffer(); //입력버퍼 클리어
                    spCOM.DiscardOutBuffer(); //출력버퍼 클리어
                    spCOM.Close();
                }
            }

            public void SendData()
            {
                for (int i = 0; i < 15; i++)
                {
                    spCOM.Write(btSend, i, 1);
                }
                System.Threading.Thread.Sleep(50);
            }

            public bool ReturnData()
            {
                bool bRes = false;
                byte[] btData = new byte[16];


                byte bcc = 0;
                int iLen = 0;

                iLen = spCOM.BytesToRead;

                if (iLen > 0)
                {
                    for (int j = 0; j < iLen; j++)
                    {
                        RevQbuffers[RevCount_WPt + j] = (byte)spCOM.ReadByte();
                    }
                    RevCount_WPt = RevCount_WPt + iLen;
                    if (RevCount_WPt >= 4096) RevCount_WPt = 4095;  // 이전것 처리될때까지 이후에 수신되는 값은 버림...
                }

                while (RevCount_WPt != RevCount_RPt)
                {
                    byte ndata = RevQbuffers[RevCount_RPt];
                    RevCount_RPt++;

                    if (ReadCount == 0)
                    {
                        if (ndata == 0x02) //시작
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 1)
                    {
                        if (ndata == 0x31) // Board ID(PWM)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 2)
                    {
                        if (ndata >= 0x00) // Address
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 3)
                    {
                        if (ndata == 0x00 || ndata == 0x01) // Read/Write
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 4)
                    {
                        if (ndata == 0x07)  //Data Length
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 5)
                    {
                        if (ndata == 0x00 || ndata == 0x01)  //Run/Stop
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 6)
                    {
                        if (ndata == 0x00 || ndata == 0x01)  //Forward/Reverse
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount < 12) // 타코채널(1), 더미(1), 주파수(2), 타코전원(1) <----대차 휠 지름(2) / 속도(2) / 타코전원(1)
                    {
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else if (ReadCount == 12)
                    {
                        if (ndata == 0x03)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;
                    }
                    else if (ReadCount == 13)
                    {
                        bcc = 0;
                        ushort sh3 = Constant.CRC16_ENSBoard(btData, 13);
                        bcc = Convert.ToByte(sh3.ToString("X4").Substring(0, 2), 16);

                        if (bcc == ndata)
                        {
                            btData[ReadCount] = ndata;
                            ReadCount++;
                        }
                        else ReadCount = 0;

                    }
                    else if (ReadCount == 14)
                    {
                        bcc = 0;
                        ushort sh4 = Constant.CRC16_ENSBoard(btData, 13);
                        bcc = Convert.ToByte(sh4.ToString("X4").Substring(2, 2), 16);

                        if (bcc == ndata)
                        {
                            bRes = true;

                            btData[ReadCount] = ndata;

                            // 수신 ok
                            for (int x = 0; x < 15; x++)
                                btRes[x] = btData[x];
                        }

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }


                    if (RevCount_WPt > 0 && (RevCount_WPt == RevCount_RPt))
                    {
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;
                    }

                }
                System.Threading.Thread.Sleep(100);

                return bRes;
            }

            #endregion


            public bool IsGetPWM2Value()
            {
                bool bRes = false;

                bRes = !(btRes[9] == 0 && btRes[10] == 0 && btRes[11] == 0 && btRes[12] == 0);

                return bRes;
            }
        }

        //USN60
        public class USN60
        {
            private float iLimitSecond;
            private string PortName;
            private SerialPort spCom;

            public USN60(string sPort)
            {
                iLimitSecond = 0.3f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 115200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.Handshake = Handshake.RequestToSend;
                spCom.DataBits = 8;
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    sRet += spCom.ReadExisting();

                    len = sRet.Length;

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len <= 0);

                if (spCom.IsOpen)
                    spCom.Close();

                return sRet;
            }

            public void BRIGHTNESS(string Value)      //  1~20
            {

                SendData("CX " + Value);
            }

            public void ALARAMreset()    //write only = 1(reset)
            {
                SendData("AV " + "1");
            }
            public void AMPLcorrect(string Value)    // -20~20dB
            {
                SendData("AV " + Value);
            }
            public void AMPLITUDE(string Value)    // 0=% screem height  ,  1=dB threshold
            {
                SendData("SA " + Value);
            }
            public void ANAOUT(string Value)    // 0=0V  , 1=5V
            {
                SendData("OS " + Value);
            }
            public void ASCANmode(string Value)    // 0=hollow , 1=filled, 2=smart hollw, 3=smart filled
            {
                SendData("FI " + Value);
            }
            public void AWSmode(string Value)    // 0=off  1=on
            {
                SendData("WS " + Value);
            }
            public void ASTART(string Value)    // 0~27990mm
            {
                SendData("AD " + Value);
            }
            public void BSTART(string Value)    // 0~27990mm
            {
                SendData("BA " + Value);
            }
            public void BAUDrate(string Value)
            {
                SendData("BR " + Value);
            }
            public void BWgain(string Value)  // 0.0~ 110.0 dB
            {
                SendData("BG " + Value);
            }
            public void CLEARreading(string Value)
            {
                SendData("CD ");
            }
            public void COLOR(string Value)    // 1~4
            {
                SendData("CR " + Value);
            }
            public void COLORLEG(string Value)    //0=off, 1=ascan, 2=grid
            {
                SendData("AC " + Value);
            }
            public void COPYmode(string Value)  // 0=report, 1=paradump 2=log to file 3=log to port
            {
                SendData("CM " + Value);
            }
            public void COUNT(string Value)  // 1~16
            {
                SendData("NC " + Value);
            }
            public void CREATEnew(string Value)  // 0=off , 1=on
            {
                SendData("SD " + Value);
            }
            public void DAMPING(string Value)  // 0=50ohm 1=75, 2=150, 3=1000ohm
            {
                SendData("PG " + Value);
            }
            public void DATE(string Value)  // read only
            {
                SendData("DT " + Value);
            }
            public void dBstep(string Value)  // 0=lock, 1=0.1, 2=0.5, 3=1.0, 4=1.5, 5=6.0, 6=user selectable
            {
                SendData("ST " + Value);
            }
            public void DELAYvelocity(string Value)  // 250~16000m/sec(for probes with delay line for calculation of DGS data)
            {
                SendData("DV " + Value);
            }
            public void DELETEcurve(string Value)  // 0=off , 1=on
            {
                SendData("CC " + Value);
            }
            public void DELETEdgsREFERENCE(string Value)  // 0=off , 1=on
            {
                SendData("XR " + Value);
            }
            public void DELETEfile(string Value)  // 0=off , 1=on
            {
                SendData("DF " + Value);
            }
            public void DGScurve(string Value)  // 0.3mm ~ effective diameter
            {
                SendData("ES " + Value);
            }
            public void DGSmode(string Value)  // 0=off , 1=on
            {
                SendData("MD " + Value);
            }
            public void DGSrecord(string Value)  // 0=off , 1=on
            {
                SendData("RR " + Value);
            }
            public void DGSreferenceECHO(string Value)  // 0=SDH , 1=FBH, 2=BW
            {
                SendData("RE " + Value);
            }
            public void DGSxtalDIAMETER(string Value)  // 20~3809mm(flat)
            {
                SendData("XD " + Value);
            }
            public void DIAMETER(string Value)  // max.3810mm(flat)
            {
                SendData("OD " + Value);
            }
            public void DIRECTORYentries(string Value)  // N/A
            {
                SendData("DE " + Value);
            }
            public void DISPLAYdelay(string Value)  // -20 ~3500μs (1/1000000)
            {
                SendData("DD " + Value);
            }
            public void DISPLAYstart(string Value)  // 0=IP, 1=IF
            {
                SendData("TR " + Value);
            }
            public void DUAL(string Value)  // 0=off, 1=on, 2=through
            {
                SendData("DM " + Value);
            }
            public void ENERGY(string Value)  // 0=low, 1=high
            {
                SendData("PI " + Value);
            }
            public void EVALRESULT(string Value)  // 0=Amp to curve, 1=ERS
            {
                SendData("ER " + Value);
            }
            public void FILEdirectory(string Value)  // READ ONLY
            {
                SendData("DR " + Value);
            }
            public void FILEupload(string Value)  // N/A
            {
                SendData("FU " + Value);
            }
            public void FILEname(string Value)  // READ ONLY
            {
                SendData("NF " + Value);
            }
            public void FREEZEMODE(string Value)  // 0=all, 1=peak, 2=envelope 0.5s, 3=envelope 1s, 4=envelope 2s, 5=envelope peak
            {
                SendData("PC " + Value);
            }
            public void FREQUENCY(string Value)  // 0=1MHz, 1=2MHz, 2=2.25MHz, 3=4MHz, 4=5MHz, 5=10MHz, 6=15MHz, 7=0.25~2MHz, 8=10~25MHz, 9=2~25MHz,
            {
                SendData("FR " + Value);
            }
            public void GAIN(string Value)  // 1~110.0
            {
                SendData("DB " + Value);
            }
            public void GATEaLOGIC(string Value)  // 0=off, 1=positive, 2=negative, 3=measure
            {
                SendData("AM " + Value);
            }
            public void GATEaSTARTMODE(string Value)  // 0=IP, 1=IF
            {
                SendData("AS " + Value);
            }
            public void GATEaTHRESHOLD(string Value)  // 0~100
            {
                SendData("AT " + Value);
            }
            public void GATEaWIDTH(string Value)  // 0.25~28000mm
            {
                SendData("AW " + Value);
            }
            public void GATEbLOGIC(string Value)  // 0=off, 1=positive, 2=negative, 3=measure
            {
                SendData("BM " + Value);
            }
            public void GATEbSTARTMODE(string Value)  // 0=IP, 1=IF, 2=gate A
            {
                SendData("BS " + Value);
            }
            public void GATEbTHRESHOLD(string Value)  // 5~100
            {
                SendData("BT " + Value);
            }
            public void GATEbWIDTH(string Value)  // 0.25~28000mm
            {
                SendData("BW " + Value);
            }
            public void GATEselect(string Value)  // 0=gate A, 1=gate B, 2=IF gate
            {
                SendData("GS " + Value);
            }
            public void GRID(string Value)  // 0=off, 1=cross, 2=grat
            {
                SendData("GR " + Value);
            }
            public void HEADER(string Value)  // 1~10
            {
                SendData("H# " + Value);
            }
            public void HEADERtitel(string Value)  // read only
            {
                SendData("HT " + Value);
            }
            public void HEADERinformation(string Value)  // read only
            {
                SendData("HI " + Value);
            }
            public void HIGHlimit(string Value)  // 0~28m
            {
                SendData("HL " + Value);
            }
            public void HORN(string Value)  // 0=on, 1=off
            {
                SendData("AH " + Value);

            }
            public void IFdelayMODE(string Value)  // 0=contact, 1=immersion
            {
                SendData("MI " + Value);
            }
            public void IFlogic(string Value)  // 0=off, 1=positive, 2=negative, 3=measure
            {
                SendData("IM " + Value);
            }

            public void IFoffset(string Value)  // -999.9997~ 999.9997μsec
            {
                SendData("OF " + Value);
            }
            public void IFSTART(string Value)  // 0~28000mm
            {
                SendData("IS " + Value);
            }
            public void IFthreshold(string Value)  // 1~100
            {
                SendData("IT " + Value);
            }
            public void IFwidth(string Value)  // 0.25~28000mm
            {
                SendData("IW " + Value);
            }
            public void INSTconfig(string Value)  // READ ONLY
            {
                SendData("IC " + Value);
            }
            public void INSTident(string Value)  // read only
            {
                SendData("ID " + Value);
            }
            public void LANGUAGE(string Value)  // 0=English
            {
                SendData("DG " + Value);
            }
            public void LOSSofSIGNAL(string Value)  // 0=off/low, off/high  ,, 1=on/low, on/high
            {
                SendData("OS " + Value);
            }
            public void LOWlimit(string Value)  // 0~28m
            {
                SendData("LL " + Value);
            }
            public void LRGdisp(string Value)  // 0=off, 1=reading1, 2=reading2, 3=reading3, 4=reading4
            {
                SendData("LC " + Value);
            }
            public void MAGNIFYgate(string Value)  // 0=gateA, 1=gateB, 2=IF gate
            {
                SendData("MG " + Value);
            }
            public void MAGNIFY(string Value)  // 0=off, 1=on
            {
                SendData("MA " + Value);
            }
            public void MASTERLOCK(string Value)  // 0=off, 1=on
            {
                SendData("ML " + Value);
            }
            public void MATERIAL(string Value)  // 메뉴얼 참조
            {
                SendData("MV " + Value);
            }
            public void MEASUREMENTindexREADING1(string Value)  // 0=English
            {
                SendData("R1 " + Value);
            }
            public void MEASUREMENTindexREADING2(string Value)  // 0=English
            {
                SendData("R2 " + Value);
            }
            public void MEASUREMENTindexREADING3(string Value)  // 0=English
            {
                SendData("R3 " + Value);
            }
            public void MEASUREMENTindexREADING4(string Value)  // 0=English
            {
                SendData("R4 " + Value);
            }
            public void MEMO(string Value)  // READ ONLY
            {
                SendData("MO " + Value);
            }
            public void MODE(string Value)  // 0=off, 1=on
            {
                SendData("BO " + Value);
            }
            public void NOTEshap(string Value)  // 1~7
            {
                SendData("N# " + Value);
            }
            public void MEASUREMENTindexREADING(string Value)  // 0=English
            {
                SendData("R2 " + Value);
            }
            public void NOTE(string Value)  // READ ONLY
            {
                SendData("NO " + Value);
            }
            public void NUMofFILES(string Value)  // READ ONLY
            {
                SendData("DL " + Value);
            }
            public void PRFmode(string Value)  // 0=autolow, 1=autohigh, 2=manual, 3=external
            {
                SendData("PF " + Value);
            }
            public void PRFvalue(string Value)  // 15~6000
            {
                SendData("PV " + Value);
            }
            public void PRINTER(string Value)  // 0=Epson, 1=HP Laserjet, 2=HP Deskjet
            {
                SendData("PR " + Value);
            }
            public void PROBEangle(string Value)  // 0~90
            {
                SendData("PA " + Value);
            }
            public void PROBEDELAY(string Value)  // 0~999.99μs
            {
                SendData("PD " + Value);
            }
            public void PROBEname(string Value)  // user defined
            {
                SendData("PN " + Value);
            }
            public void PULSERtype(string Value)  // 0=SQUARE, 1=SPIKE
            {
                SendData("PY " + Value);
            }
            public void RANGE(string Value)  // 1~28000mm
            {
                SendData("DW " + Value);
            }
            public void READING1(string Value)  // READ ONLY
            {
                SendData("S1 " + Value);
            }
            public void READING2(string Value)  // READ ONLY
            {
                SendData("S2 " + Value);
            }
            public void READING3(string Value)  // READ ONLY
            {
                SendData("S3 " + Value);
            }
            public void READING4(string Value)  // READ ONLY
            {
                SendData("S4 " + Value);
            }
            public void RECALLcurrentFILE(string Value)  // 0=off, 1=on
            {
                SendData("RD " + Value);
            }
            public void RECORD(string Value)  // 0=off, 1=on
            {
                SendData("TS " + Value);
            }
            public void RECTIFY(string Value)  // 0=positive HW, 1=negative HW, 2=fullwave, 3=RF
            {
                SendData("RF " + Value);
            }
            public void REFatten(string Value)  // 0~100dB/m
            {
                SendData("AR " + Value);
            }
            public void REFERENCEtype(string Value)  // READ ONLY
            {
                SendData("S3 " + Value);
            }
            public void REFSIZE(string Value)  // 0.5~10mm
            {
                SendData("RS " + Value);
            }
            public void REJEKT(string Value)  // 0~80
            {
                SendData("RJ " + Value);
            }
            public void SERIALnumber(string Value)  // READ ONLY
            {
                SendData("SN " + Value);
            }
            public void S_REF2(string Value)  // 0~12190MM
            {
                SendData("KR " + Value);
            }
            public void S_REF1(string Value)  // 0~12190MM
            {
                SendData("NR " + Value);
            }
            public void TCG_AMPLITUDE(string Value)  // 0~210dB
            {
                SendData("TV " + Value);
            }
            public void TCG_ATTENUATION(string Value)  // -40~40dB per μs
            {
                SendData("TN " + Value);
            }
            public void TCG_CURVE_FINISH(string Value)  // 0=OFF, 1=ON
            {
                SendData("FH " + Value);
            }
            public void TCG_DISPLAY(string Value)  // 0=OFF, 1=ON
            {
                SendData("TY " + Value);
            }
            public void TCG_MODE(string Value)  // 0=OFF, 1=TCG, 2=DAC
            {
                SendData("TM " + Value);
            }
            public void TCG_OFFSET(string Value)  // 0~110dB
            {
                SendData("TO " + Value);
            }
            public void TCG_POINT(string Value)  // 1~16
            {
                SendData("PT " + Value);
            }
            public void TCG_START_MODE(string Value)  // 0=IP, 1=IF
            {
                SendData("SM " + Value);
            }
            public void TCG_TIME(string Value)  // 0~28m
            {
                SendData("TT " + Value);
            }
            public void TEST_ATTEN(string Value)  // 0~100dB/m
            {
                SendData("DN " + Value);
            }
            public void THICKNESS(string Value)  // 1~28000mm
            {
                SendData("TH " + Value);
            }
            public void TIME(string Value)  // read only
            {
                SendData("TI " + Value);
            }
            public void TOF_GATE_A(string Value)  // 0=FLANK,1=PEAK
            {
                SendData("D1 " + Value);
            }
            public void TOF_GATE_B(string Value)  // 0=FLANK,1=PEAK
            {
                SendData("D2 " + Value);
            }
            public void TOF_GATE_EE(string Value)  // 0=FLANK,1=PEAK
            {
                SendData("D3 " + Value);
            }
            public void TRANSFER_CORRECTION(string Value)  // -110~110dB(for DAC/TCG, dependant on gain setting)
            {
                SendData("TC " + Value);
            }
            public void TTL_1(string Value)  // 0=OFF, 1=ON
            {
                SendData("T1 " + Value);
            }
            public void TTL_2(string Value)  // 0=OFF, 1=ON
            {
                SendData("T2 " + Value);
            }
            public void TTL_3(string Value)  // 0=OFF, 1=ON
            {
                SendData("T3 " + Value);
            }
            public void TTL_MODE(string Value)  // 0=timed 0.5s, 1=timed0.5s, 2=timed1s, 3=timed2s, 4=instantaneous, 5=latched
            {
                SendData("AL " + Value);
            }
            public void TRANSFER_CORR(string Value)  // -30~30dB(for DGS)
            {
                SendData("LS " + Value);
            }
            public void UNIT(string Value)  // 0=mm, 1=inch, 2=μs
            {
                SendData("UN " + Value);
            }
            public void USER_GAIN_STEP(string Value)  // 0.0~24.0
            {
                SendData("DS " + Value);
            }
            public void VELOCITY(string Value)  // 1000~16000μs
            {
                SendData("SV " + Value);
            }
            public string SelfTest()  // read only
            {
                SendData("VR");
                string retuneValue = ReturnValue();
                return retuneValue;
            }
            public void VOLTAGE(string Value)  // 50~450volt
            {
                SendData("PO " + Value);
            }
            public void WATER_PATH_VEL(string Value)  // 1000~16000m/sec(for probes with fixed delay line or immersion testing with IF gate on)
            {
                SendData("WV " + Value);
            }
            public void WIDTH(string Value)  // 50~1000ns
            {
                SendData("PW " + Value);
            }
            public void WINDOW(string Value)  // 1~16
            {
                SendData("NW " + Value);
            }
            public void XTAL_DIAMETER(string Value)  // 3~35mm
            {
                SendData("XD " + Value);
            }
            public void XTAL_FREQU(string Value)  // 0.5~10MHz
            {
                SendData("XF " + Value);
            }
            public void X_VALUE(string Value)  // 0~5080mm
            {
                SendData("XV " + Value);
            }




            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    byte[] cx1 = new byte[1] { 0x1B };
                    spCom.Write(cx1, 0, 1);
                    spCom.Write(sFlag);
                    spCom.Write(Constant.EOF.ToString());
                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

        }


        //MP5YMOD
        public class MP5YMOD
        {
            public SerialPort spCOM;
            public int ADDR;

            public MP5YMOD(string sPortName, int addr)
            {
                try
                {
                    spCOM = new SerialPort();
                    spCOM.BaudRate = 9600;
                    spCOM.DataBits = 8;
                    spCOM.StopBits = StopBits.One;
                    spCOM.Parity = Parity.None;
                    spCOM.ReadTimeout = 500;

                    if (sPortName == "")
                    {
                        throw new Exception("현재 시스템에 사용 가능한 포트가 존재하지 않습니다.");
                    }
                    else
                    {
                        spCOM.PortName = sPortName;
                    }
                    ADDR = addr;

                }
                catch
                {

                }
            }

            public bool IsOpen
            {
                get
                {
                    return spCOM.IsOpen;
                }
            }

            public void Open()
            {
                try
                {
                    if (!spCOM.IsOpen)
                    {
                        spCOM.Open();
                    }
                }
                catch
                {
                }
            }

            public void Close()
            {
                try
                {
                    if (spCOM.IsOpen)
                    {
                        spCOM.Close();
                    }
                }
                catch
                {
                }
            }

            public static ushort CRC16_MOD(byte[] nData, int wLength)
            {
                ushort[] wCRCTable = new ushort[]{
                0x0000, 0xC0C1, 0xC181, 0x0140, 0xC301, 0x03C0, 0x0280, 0xC241,
                0xC601, 0x06C0, 0x0780, 0xC741, 0x0500, 0xC5C1, 0xC481, 0x0440,
                0xCC01, 0x0CC0, 0x0D80, 0xCD41, 0x0F00, 0xCFC1, 0xCE81, 0x0E40,
                0x0A00, 0xCAC1, 0xCB81, 0x0B40, 0xC901, 0x09C0, 0x0880, 0xC841,
                0xD801, 0x18C0, 0x1980, 0xD941, 0x1B00, 0xDBC1, 0xDA81, 0x1A40,
                0x1E00, 0xDEC1, 0xDF81, 0x1F40, 0xDD01, 0x1DC0, 0x1C80, 0xDC41,
                0x1400, 0xD4C1, 0xD581, 0x1540, 0xD701, 0x17C0, 0x1680, 0xD641,
                0xD201, 0x12C0, 0x1380, 0xD341, 0x1100, 0xD1C1, 0xD081, 0x1040,
                0xF001, 0x30C0, 0x3180, 0xF141, 0x3300, 0xF3C1, 0xF281, 0x3240,
                0x3600, 0xF6C1, 0xF781, 0x3740, 0xF501, 0x35C0, 0x3480, 0xF441,
                0x3C00, 0xFCC1, 0xFD81, 0x3D40, 0xFF01, 0x3FC0, 0x3E80, 0xFE41,
                0xFA01, 0x3AC0, 0x3B80, 0xFB41, 0x3900, 0xF9C1, 0xF881, 0x3840,
                0x2800, 0xE8C1, 0xE981, 0x2940, 0xEB01, 0x2BC0, 0x2A80, 0xEA41,
                0xEE01, 0x2EC0, 0x2F80, 0xEF41, 0x2D00, 0xEDC1, 0xEC81, 0x2C40,
                0xE401, 0x24C0, 0x2580, 0xE541, 0x2700, 0xE7C1, 0xE681, 0x2640,
                0x2200, 0xE2C1, 0xE381, 0x2340, 0xE101, 0x21C0, 0x2080, 0xE041,
                0xA001, 0x60C0, 0x6180, 0xA141, 0x6300, 0xA3C1, 0xA281, 0x6240,
                0x6600, 0xA6C1, 0xA781, 0x6740, 0xA501, 0x65C0, 0x6480, 0xA441,
                0x6C00, 0xACC1, 0xAD81, 0x6D40, 0xAF01, 0x6FC0, 0x6E80, 0xAE41,
                0xAA01, 0x6AC0, 0x6B80, 0xAB41, 0x6900, 0xA9C1, 0xA881, 0x6840,
                0x7800, 0xB8C1, 0xB981, 0x7940, 0xBB01, 0x7BC0, 0x7A80, 0xBA41,
                0xBE01, 0x7EC0, 0x7F80, 0xBF41, 0x7D00, 0xBDC1, 0xBC81, 0x7C40,
                0xB401, 0x74C0, 0x7580, 0xB541, 0x7700, 0xB7C1, 0xB681, 0x7640,
                0x7200, 0xB2C1, 0xB381, 0x7340, 0xB101, 0x71C0, 0x7080, 0xB041,
                0x5000, 0x90C1, 0x9181, 0x5140, 0x9301, 0x53C0, 0x5280, 0x9241,
                0x9601, 0x56C0, 0x5780, 0x9741, 0x5500, 0x95C1, 0x9481, 0x5440,
                0x9C01, 0x5CC0, 0x5D80, 0x9D41, 0x5F00, 0x9FC1, 0x9E81, 0x5E40,
                0x5A00, 0x9AC1, 0x9B81, 0x5B40, 0x9901, 0x59C0, 0x5880, 0x9841,
                0x8801, 0x48C0, 0x4980, 0x8941, 0x4B00, 0x8BC1, 0x8A81, 0x4A40,
                0x4E00, 0x8EC1, 0x8F81, 0x4F40, 0x8D01, 0x4DC0, 0x4C80, 0x8C41,
                0x4400, 0x84C1, 0x8581, 0x4540, 0x8701, 0x47C0, 0x4680, 0x8641,
                0x8201, 0x42C0, 0x4380, 0x8341, 0x4100, 0x81C1, 0x8081, 0x4040
            };

                byte nTemp;
                ushort wCRCWord = 0xffff;
                for (int i = 0; i < wLength; i++)
                {
                    nTemp = (byte)(((byte)nData[i]) ^ wCRCWord);
                    wCRCWord >>= 8;
                    wCRCWord ^= wCRCTable[nTemp];
                }

                return wCRCWord;
            }

            public bool ReadData(byte type, int sAddr, out string rev_data)
            {
                byte[] rev = new byte[9];
                bool pan = true;
                int temp1;

                try
                {
                    byte[] data = new byte[8];

                    if (!spCOM.IsOpen)
                        spCOM.Open();
                    spCOM.DiscardInBuffer();
                    spCOM.DiscardOutBuffer();
                    data[0] = (byte)ADDR; //주소
                    data[1] = type; //펑션코드
                    data[2] = (byte)(sAddr >> 8); //주소상위
                    data[3] = (byte)(sAddr); //주소하위
                    data[4] = 0x00;
                    data[5] = 0x02;
                    data[6] = (byte)(CRC16_MOD(data, 6));
                    data[7] = (byte)(CRC16_MOD(data, 6) >> 8);
                    spCOM.Write(data, 0, data.Length);
                    Thread.Sleep(50);
                    int iLen = spCOM.BytesToRead;
                    if (iLen > 0)
                    {
                        for (int i = 0; i < iLen; i++)
                        {
                            rev[i] = (byte)spCOM.ReadByte();

                        }
                        for (int i = 0; i < 2; i++)
                        {
                            if (rev[i] != data[i])
                                pan = false;
                        }
                        if (type == 0x04) //측정값
                        {
                            if (rev[7] != (byte)(CRC16_MOD(rev, 7)))
                                pan = false;
                            if (rev[8] != (byte)(CRC16_MOD(rev, 7) >> 8))
                                pan = false;
                        }
                    }
                    else
                    {
                        pan = false;
                    }
                }
                catch
                {
                    pan = false;

                }

                if (pan)
                {
                    temp1 = rev[6] << 16 | rev[5] << 32 | rev[4] | rev[3] << 8;
                    rev_data = temp1.ToString();
                }
                else
                {
                    rev_data = "";
                }

                return pan;
            }
            public string Read()
            {
                string sData = "";
                int sAddr = 0x03E9;
                try
                {

                    for (int i = 0; i < 10; i++)
                    {

                        if (ReadData(0x04, sAddr, out sData))
                            break;
                        else
                            Thread.Sleep(50);
                    }
                }
                catch
                {

                }
                return sData;
            }

        }
        public class DSOX1204G_LAN
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP_Adress;
            public DSOX1204G_LAN(string sPort)
            {
                IP_Adress = sPort;
            }
            public void Portclose()
            {
                try
                {
                    if (client.Connected == true)
                    {
                        client.Close();
                    }
                }
                catch
                {

                }
            }
            public void OutPut(string sOnOff)
            {
                try
                {
                    SendData("OUTP:STAT:IMM " + sOnOff.ToString());
                }
                catch
                {

                }
            }

            public void Portopen()
            {
                try
                {
                    if (client.Connected == false)
                    {
                        IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                        IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);
                        client.Connect(serverEndPoint);
                        Thread.Sleep(50);
                    }
                }
                catch
                {

                }
            }
            public bool PING()
            {
                bool res = false;
                Ping sender2 = new Ping();
                PingReply reply = sender2.Send(IP_Adress, 1);
                if (reply.Status == IPStatus.Success)
                {
                    res = true;
                }
                return res;
            }
            public string SelfTest()
            {
                SendData("*IDN?");
                string sRes = ReturnValue();
                return sRes;
            }
            public string Checkvolt(string ch)
            {
                SendData(":MEASure:VTOP? CHAN" + ch);
                string sRes = ReturnValue();
                return sRes;
            }
            public string Checkamp(string ch)
            {
                SendData(":MEASure:VAMPlitude? CHAN" + ch);
                string sRes = ReturnValue();
                return sRes;
            }

            public string CheckRMS(string ch)
            {
                SendData(":MEASure:VRMS? CHAN" + ch);
                string sRes = ReturnValue();
                return sRes;
            }

            public void SendData(string sCMD)
            {
                try
                {
                    if (client.Connected)
                    {
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);
                    }
                    else
                    {
                        IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                        IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);
                        client.Connect(serverEndPoint);
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);
                    }
                }
                catch
                {

                }
            }
            private string ReturnValue()
            {
                byte[] receipveBUffer = new byte[1024];
                int byteBytesRecvd = client.Receive(receipveBUffer);
                byte[] receipve = new byte[byteBytesRecvd];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);
                string receiveStr = Encoding.Default.GetString(receipve);
                return receiveStr;
            }
            public void SetVolt(float Volt)
            {
                try
                {
                    SendData("SOUR:VOLT:LEV:IMM:AMPL " + Volt.ToString());
                }
                catch
                {

                }
            }
        }

        public class DAQ970A_LAN
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP_Adress;
            public DAQ970A_LAN(string sPort)
            {
                IP_Adress = sPort;
            }
            public void Portclose()
            {
                try
                {
                    if (client.Connected == true)
                    {
                        client.Close();
                    }
                }
                catch
                {

                }
            }
            public void Portopen()
            {
                try
                {
                    if (client.Connected == false)
                    {
                        IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                        IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);
                        client.Connect(serverEndPoint);
                    }
                }
                catch
                {

                }
            }
            public bool PING()
            {
                bool res = false;
                Ping sender2 = new Ping();
                PingReply reply = sender2.Send(IP_Adress, 1);
                if (reply.Status == IPStatus.Success)
                {
                    res = true;
                }
                return res;
            }
            public string SelfTest()
            {
                SendData("*IDN?");
                string sRes = ReturnValue();
                return sRes;
            }
            public void SendData(string sCMD)
            {
                try
                {
                    if (client.Connected)
                    {
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);
                    }
                    else
                    {
                        IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                        IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);
                        client.Connect(serverEndPoint);
                        byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                        client.Send(bytesBuff);
                    }
                }
                catch
                {

                }
            }


            private string ReturnValue()
            {
                byte[] receipveBUffer = new byte[1024];
                int byteBytesRecvd = client.Receive(receipveBUffer);
                byte[] receipve = new byte[byteBytesRecvd];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);
                string receiveStr = Encoding.Default.GetString(receipve);
                return receiveStr;
            }
            public string Check_Volt(string addr)
            {
                SendData("*RST");
                string Value;
                SendData("CONF:VOLT AUTO,DEF,(@" + addr + ")");
                Thread.Sleep(300);
                SendData("ROUT:MON (@" + addr + ")");
                Thread.Sleep(300);
                SendData("ROUT:MON:STAT ON");
                Thread.Sleep(1000);
                Thread.Sleep(1000);
                SendData("ROUT:MON:DATA?");
                Value = ReturnValue();
                SendData("*RST");
                return Value;
            }
            public string CHECK_RES(string addr)
            {
                SendData("*RST");

                string Value;

                SendData("CONF:RES AUTO,DEF,(@" + addr + ")");
                //daq970a.Write("SAMP:COUN 500");                                
                Thread.Sleep(100);
                SendData("ROUT:MON (@" + addr + ")");
                Thread.Sleep(100);
                SendData("ROUT:MON:STAT ON");
                SendData("ROUT:MON:DATA?");
                Value = ReturnValue();
                return Value;
            }
            public string Check_Current(string addr)
            {
                SendData("*RST");
                string Value;
                SendData("CONF:CURR AUTO,DEF,(@" + addr + ")");
                SendData("ROUT:MON (@" + addr + ")");
                SendData("ROUT:MON:STAT ON");
                SendData("ROUT:MON:DATA?");
                Value = ReturnValue();
                //SendData("*RST");
                return Value;
            }
        } // DAQ970A_LAN   

        // DAQ970A - DMM
        public class VisaInstrumentApp_2
        {
            private VisaInstrument myScope;

            public VisaInstrumentApp_2(string IP)
            {
                myScope = new
                  VisaInstrument("TCPIP0::" + IP + "::hislip0::INSTR");
            }
            public string SelfTest()
            {
                string sRes = "";
                try
                {
                    StringBuilder strResults;
                    strResults = myScope.DoQueryString("*IDN?");
                    sRes = strResults.ToString();
                }
                catch
                {

                }
                return sRes;
            }
            public void PortClose()
            {
                myScope.Close();
            }
        
            public string Query(string name)
            {
                string sRes = "";
                try
                {
                    StringBuilder strResults;
                    strResults = myScope.DoQueryString(name);
                    sRes = strResults.ToString();
                }
                catch
                {

                }
                return sRes;
            }
            public void Write(string name)
            {

                try
                {
                    Thread.Sleep(100);
                    myScope.DoCommand(name);
                    Thread.Sleep(100);
                }
                catch
                {

                }

            }

            public void Single()
            {

                try
                {

                    myScope.DoCommand(":SINGle");

                }
                catch
                {

                }

            }
            public void Capture(string path)
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    //  myScope.DoCommand(":MEASure:SOURce CHANnel1");
                    //  Console.WriteLine("Measure source: {0}",
                    //      myScope.DoQueryString(":MEASure:SOURce?"));
                    ////  myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    //  double fResult;
                    //  myScope.DoCommand(":MEASure:FREQuency");
                    //  fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    //  Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    //  myScope.DoCommand(":MEASure:VAMPlitude");
                    //  fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    //  Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    //  // Download the screen image.
                    //  // -----------------------------------------------------------
                    //  myScope.DoCommand(":HARDcopy:INKSaver OFF");

                    //  // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor",
                         out ResultsArray);

                    // Store the screen data to a file.
                    strPath = path;
                    FileStream fStream = File.Open(strPath, FileMode.Create);
                    fStream.Write(ResultsArray, 0, nLength);
                    fStream.Close();
                    Console.WriteLine("Screen image ({0} bytes) written to {1}",
                        nLength, strPath);

                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    //myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    //Console.WriteLine("Waveform points mode: {0}",
                    //    myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    //// Get the number of waveform points available.
                    //myScope.DoCommand(":WAVeform:POINts 10240");
                    //Console.WriteLine("Waveform points available: {0}",
                    //    myScope.DoQueryString(":WAVeform:POINts?"));

                    //// Set the waveform source.
                    //myScope.DoCommand(":WAVeform:SOURce CHANnel1");
                    //Console.WriteLine("Waveform source: {0}",
                    //    myScope.DoQueryString(":WAVeform:SOURce?"));

                    //// Choose the format of the data returned (WORD, BYTE, ASCII):
                    //myScope.DoCommand(":WAVeform:FORMat BYTE");
                    //Console.WriteLine("Waveform format: {0}",
                    //    myScope.DoQueryString(":WAVeform:FORMat?"));

                    //// Display the waveform settings:
                    //double[] fResultsArray;
                    //fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    //double fFormat = fResultsArray[0];
                    //if (fFormat == 0.0)
                    //{
                    //    Console.WriteLine("Waveform format: BYTE");
                    //}
                    //else if (fFormat == 1.0)
                    //{
                    //    Console.WriteLine("Waveform format: WORD");
                    //}
                    //else if (fFormat == 2.0)
                    //{
                    //    Console.WriteLine("Waveform format: ASCii");
                    //}

                    //double fType = fResultsArray[1];
                    //if (fType == 0.0)
                    //{
                    //    Console.WriteLine("Acquire type: NORMal");
                    //}
                    //else if (fType == 1.0)
                    //{
                    //    Console.WriteLine("Acquire type: PEAK");
                    //}
                    //else if (fType == 2.0)
                    //{
                    //    Console.WriteLine("Acquire type: AVERage");
                    //}
                    //else if (fType == 3.0)
                    //{
                    //    Console.WriteLine("Acquire type: HRESolution");
                    //}

                    //double fPoints = fResultsArray[2];
                    //Console.WriteLine("Waveform points: {0:e}", fPoints);

                    //double fCount = fResultsArray[3];
                    //Console.WriteLine("Waveform average count: {0:e}", fCount);

                    //double fXincrement = fResultsArray[4];
                    //Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    //double fXorigin = fResultsArray[5];
                    //Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    //double fXreference = fResultsArray[6];
                    //Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    //double fYincrement = fResultsArray[7];
                    //Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    //double fYorigin = fResultsArray[8];
                    //Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    //double fYreference = fResultsArray[9];
                    //Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    //// Read waveform data.
                    //nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                    //    out ResultsArray);
                    //Console.WriteLine("Number of data values: {0}", nLength);

                    //// Set up output file:
                    //strPath = "c:\\scope\\data\\waveform_data.csv";
                    //if (File.Exists(strPath)) File.Delete(strPath);

                    //// Open file for output.
                    //StreamWriter writer = File.CreateText(strPath);

                    //// Output waveform data in CSV format.
                    //for (int i = 0; i < nLength - 1; i++)
                    //    writer.WriteLine("{0:f9}, {1:f6}",
                    //        fXorigin + ((float)i * fXincrement),
                    //        (((float)ResultsArray[i] - fYreference) *
                    //        fYincrement) + fYorigin);

                    //// Close output file.
                    //writer.Close();
                    //Console.WriteLine("Waveform format BYTE data written to {0}",
                    //    strPath);
                }
                catch
                {


                }
            }
            public double[,] Analyze()
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":MEASure:SOURce CHANnel1");
                    Console.WriteLine("Measure source: {0}",
                        myScope.DoQueryString(":MEASure:SOURce?"));
                    // myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    double fResult;
                    myScope.DoCommand(":MEASure:FREQuency");
                    fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    myScope.DoCommand(":MEASure:VAMPlitude");
                    fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    // Download the screen image.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":HARDcopy:INKSaver OFF");

                    // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor",
                        out ResultsArray);

                    // Store the screen data to a file.



                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    Console.WriteLine("Waveform points mode: {0}",
                        myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    // Get the number of waveform points available.
                    myScope.DoCommand(":WAVeform:POINts 1024");
                    Console.WriteLine("Waveform points available: {0}",
                        myScope.DoQueryString(":WAVeform:POINts?"));

                    // Set the waveform source.
                    myScope.DoCommand(":WAVeform:SOURce CHANnel1");
                    Console.WriteLine("Waveform source: {0}",
                        myScope.DoQueryString(":WAVeform:SOURce?"));

                    // Choose the format of the data returned (WORD, BYTE, ASCII):
                    myScope.DoCommand(":WAVeform:FORMat BYTE");
                    Console.WriteLine("Waveform format: {0}",
                        myScope.DoQueryString(":WAVeform:FORMat?"));

                    // Display the waveform settings:
                    double[] fResultsArray;
                    fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    double fFormat = fResultsArray[0];
                    if (fFormat == 0.0)
                    {
                        Console.WriteLine("Waveform format: BYTE");
                    }
                    else if (fFormat == 1.0)
                    {
                        Console.WriteLine("Waveform format: WORD");
                    }
                    else if (fFormat == 2.0)
                    {
                        Console.WriteLine("Waveform format: ASCii");
                    }

                    double fType = fResultsArray[1];
                    if (fType == 0.0)
                    {
                        Console.WriteLine("Acquire type: NORMal");
                    }
                    else if (fType == 1.0)
                    {
                        Console.WriteLine("Acquire type: PEAK");
                    }
                    else if (fType == 2.0)
                    {
                        Console.WriteLine("Acquire type: AVERage");
                    }
                    else if (fType == 3.0)
                    {
                        Console.WriteLine("Acquire type: HRESolution");
                    }

                    double fPoints = fResultsArray[2];
                    Console.WriteLine("Waveform points: {0:e}", fPoints);

                    double fCount = fResultsArray[3];
                    Console.WriteLine("Waveform average count: {0:e}", fCount);

                    double fXincrement = fResultsArray[4];
                    Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    double fXorigin = fResultsArray[5];
                    Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    double fXreference = fResultsArray[6];
                    Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    double fYincrement = fResultsArray[7];
                    Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    double fYorigin = fResultsArray[8];
                    Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    double fYreference = fResultsArray[9];
                    Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    // Read waveform data.
                    nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                        out ResultsArray);
                    Console.WriteLine("Number of data values: {0}", nLength);

                    // Set up output file:

                    // Open file for output.

                    double[,] value = new double[2, nLength];
                    // Output waveform data in CSV format.
                    for (int i = 0; i < nLength - 1; i++)
                    {
                        value[0, i] = fXorigin + ((float)i * fXincrement);
                        value[1, i] = (((float)ResultsArray[i] - fYreference) * fYincrement) + fYorigin;

                    }
                    // Close output file.

                    return value;

                }
                catch
                {
                    double[,] value = new double[2, 10000];

                    return value;
                }

            }

            public double[,] Analyze2()
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":MEASure:SOURce CHANnel2");
                    Console.WriteLine("Measure source: {0}",
                        myScope.DoQueryString(":MEASure:SOURce?"));
                    // myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    double fResult;
                    myScope.DoCommand(":MEASure:FREQuency");
                    fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    myScope.DoCommand(":MEASure:VAMPlitude");
                    fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    // Download the screen image.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":HARDcopy:INKSaver OFF");

                    // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor",
                        out ResultsArray);

                    // Store the screen data to a file.



                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    Console.WriteLine("Waveform points mode: {0}",
                        myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    // Get the number of waveform points available.
                    myScope.DoCommand(":WAVeform:POINts 10240");
                    Console.WriteLine("Waveform points available: {0}",
                        myScope.DoQueryString(":WAVeform:POINts?"));

                    // Set the waveform source.
                    myScope.DoCommand(":WAVeform:SOURce CHANnel2");
                    Console.WriteLine("Waveform source: {0}",
                        myScope.DoQueryString(":WAVeform:SOURce?"));

                    // Choose the format of the data returned (WORD, BYTE, ASCII):
                    myScope.DoCommand(":WAVeform:FORMat BYTE");
                    Console.WriteLine("Waveform format: {0}",
                        myScope.DoQueryString(":WAVeform:FORMat?"));

                    // Display the waveform settings:
                    double[] fResultsArray;
                    fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    double fFormat = fResultsArray[0];
                    if (fFormat == 0.0)
                    {
                        Console.WriteLine("Waveform format: BYTE");
                    }
                    else if (fFormat == 1.0)
                    {
                        Console.WriteLine("Waveform format: WORD");
                    }
                    else if (fFormat == 2.0)
                    {
                        Console.WriteLine("Waveform format: ASCii");
                    }

                    double fType = fResultsArray[1];
                    if (fType == 0.0)
                    {
                        Console.WriteLine("Acquire type: NORMal");
                    }
                    else if (fType == 1.0)
                    {
                        Console.WriteLine("Acquire type: PEAK");
                    }
                    else if (fType == 2.0)
                    {
                        Console.WriteLine("Acquire type: AVERage");
                    }
                    else if (fType == 3.0)
                    {
                        Console.WriteLine("Acquire type: HRESolution");
                    }

                    double fPoints = fResultsArray[2];
                    Console.WriteLine("Waveform points: {0:e}", fPoints);

                    double fCount = fResultsArray[3];
                    Console.WriteLine("Waveform average count: {0:e}", fCount);

                    double fXincrement = fResultsArray[4];
                    Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    double fXorigin = fResultsArray[5];
                    Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    double fXreference = fResultsArray[6];
                    Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    double fYincrement = fResultsArray[7];
                    Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    double fYorigin = fResultsArray[8];
                    Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    double fYreference = fResultsArray[9];
                    Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    // Read waveform data.
                    nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                        out ResultsArray);
                    Console.WriteLine("Number of data values: {0}", nLength);

                    // Set up output file:

                    // Open file for output.

                    double[,] value = new double[2, nLength];
                    // Output waveform data in CSV format.
                    for (int i = 0; i < nLength - 1; i++)
                    {
                        value[0, i] = fXorigin + ((float)i * fXincrement);
                        value[1, i] = (((float)ResultsArray[i] - fYreference) * fYincrement) + fYorigin;

                    }
                    // Close output file.

                    return value;

                }
                catch
                {
                    double[,] value = new double[2, 10000];

                    return value;
                }

            }

            class VisaInstrument
            {
                private int m_nResourceManager;
                private int m_nSession;
                private string m_strVisaAddress;

                // Constructor.
                public VisaInstrument(string strVisaAddress)
                {
                    // Save VISA addres in member variable.
                    m_strVisaAddress = strVisaAddress;

                    // Open the default VISA resource manager.
                    OpenResourceManager();

                    // Open a VISA resource session.
                    OpenSession();

                    // Clear the interface.
                    int nViStatus;
                    nViStatus = visa32.viClear(m_nSession);
                }

                public void DoCommand(string strCommand)
                {
                    // Send the command.
                    VisaSendCommandOrQuery(strCommand);

                    // Check for inst errors.
                    CheckInstrumentErrors(strCommand);
                }

                public int DoCommandIEEEBlock(string strCommand,
                  byte[] DataArray)
                {
                    // Send the command to the device.
                    string strCommandAndLength;
                    int nViStatus, nLength, nBytesWritten;

                    nLength = DataArray.Length;
                    strCommandAndLength = String.Format("{0} #8%08d",
                      strCommand);

                    // Write first part of command to formatted I/O write buffer.
                    nViStatus = visa32.viPrintf(m_nSession, strCommandAndLength,
                      nLength);
                    CheckVisaStatus(nViStatus);

                    // Write the data to the formatted I/O write buffer.
                    nViStatus = visa32.viBufWrite(m_nSession, DataArray, nLength,
                      out nBytesWritten);
                    CheckVisaStatus(nViStatus);

                    // Check for inst errors.
                    CheckInstrumentErrors(strCommand);

                    return nBytesWritten;
                }

                public StringBuilder DoQueryString(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    StringBuilder strResults = new StringBuilder(1000);
                    strResults = VisaGetResultString();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return strResults;
                }

                public double DoQueryNumber(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    double fResults;
                    fResults = VisaGetResultNumber();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return fResults;
                }

                public double[] DoQueryNumbers(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    double[] fResultsArray;
                    fResultsArray = VisaGetResultNumbers();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return fResultsArray;
                }

                public int DoQueryIEEEBlock(string strQuery,
                  out byte[] ResultsArray)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    int length;   // Number of bytes returned from instrument.
                    length = VisaGetResultIEEEBlock(out ResultsArray);

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return length;
                }

                private void VisaSendCommandOrQuery(string strCommandOrQuery)
                {
                    // Send command or query to the device.
                    string strWithNewline;
                    strWithNewline = String.Format("{0}\n", strCommandOrQuery);
                    int nViStatus;
                    nViStatus = visa32.viPrintf(m_nSession, strWithNewline);
                    CheckVisaStatus(nViStatus);
                }

                private StringBuilder VisaGetResultString()
                {
                    StringBuilder strResults = new StringBuilder(1000);

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%1000t", strResults);
                    CheckVisaStatus(nViStatus);

                    return strResults;
                }

                private double VisaGetResultNumber()
                {
                    double fResults = 0;

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%lf", out fResults);
                    CheckVisaStatus(nViStatus);

                    return fResults;
                }

                private double[] VisaGetResultNumbers()
                {
                    double[] fResultsArray;
                    fResultsArray = new double[10];

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%,10lf\n",
                        fResultsArray);
                    CheckVisaStatus(nViStatus);

                    return fResultsArray;
                }

                private int VisaGetResultIEEEBlock(out byte[] ResultsArray)
                {
                    // Results array, big enough to hold a PNG.
                    ResultsArray = new byte[300000];
                    int length;   // Number of bytes returned from instrument.

                    // Set the default number of bytes that will be contained in
                    // the ResultsArray to 300,000 (300kB).
                    length = 300000;

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%#b", ref length,
                      ResultsArray);
                    CheckVisaStatus(nViStatus);

                    // Write and read buffers need to be flushed after IEEE block?
                    nViStatus = visa32.viFlush(m_nSession, visa32.VI_WRITE_BUF);
                    CheckVisaStatus(nViStatus);

                    nViStatus = visa32.viFlush(m_nSession, visa32.VI_READ_BUF);
                    CheckVisaStatus(nViStatus);

                    return length;
                }

                private void CheckInstrumentErrors(string strCommand)
                {
                    // Check for instrument errors.
                    StringBuilder strInstrumentError = new StringBuilder(1000);
                    bool bFirstError = true;

                    do   // While not "0,No error"
                    {
                        VisaSendCommandOrQuery(":SYSTem:ERRor?");
                        strInstrumentError = VisaGetResultString();

                        if (!strInstrumentError.ToString().StartsWith("+0,"))
                        {
                            if (bFirstError)
                            {
                                Console.WriteLine("ERROR(s) for command '{0}': ",
                                  strCommand);
                                bFirstError = false;
                            }
                            Console.Write(strInstrumentError);
                        }
                    } while (!strInstrumentError.ToString().StartsWith("+0,"));
                }

                private void OpenResourceManager()
                {
                    int nViStatus;
                    nViStatus =
                      visa32.viOpenDefaultRM(out this.m_nResourceManager);
                    if (nViStatus < visa32.VI_SUCCESS)
                        throw new
                          ApplicationException("Failed to open Resource Manager");
                }

                private void OpenSession()
                {
                    int nViStatus;
                    nViStatus = visa32.viOpen(this.m_nResourceManager,
                      this.m_strVisaAddress, visa32.VI_NO_LOCK,
                      visa32.VI_TMO_IMMEDIATE, out this.m_nSession);
                    CheckVisaStatus(nViStatus);
                }

                public void SetTimeoutSeconds(int nSeconds)
                {
                    int nViStatus;
                    nViStatus = visa32.viSetAttribute(this.m_nSession,
                      visa32.VI_ATTR_TMO_VALUE, nSeconds * 1000);
                    CheckVisaStatus(nViStatus);
                }

                public void CheckVisaStatus(int nViStatus)
                {
                    // If VISA error, throw exception.
                    if (nViStatus < visa32.VI_SUCCESS)
                    {
                        StringBuilder strError = new StringBuilder(256);
                        visa32.viStatusDesc(this.m_nResourceManager, nViStatus,
                          strError);
                        throw new ApplicationException(strError.ToString());
                    }
                }

                public void Close()
                {
                    if (m_nSession != 0)
                        visa32.viClose(m_nSession);
                    if (m_nResourceManager != 0)
                        visa32.viClose(m_nResourceManager);
                }
            }
        }






        // DSOX1204A_#1 오실로스코프
        public class VisaInstrumentApp_1
        {
            private VisaInstrument myScope;

            public VisaInstrumentApp_1(string IP)
            {
                myScope = new VisaInstrument("TCPIP0::" + IP + "::hislip0::INSTR");

                //myScope = new
                //  VisaInstrument("TCPIP0::192.168.0.20::hislip0::INSTR");
            }
            public string SelfTest()
            {
                string sRes = "";
                try
                {
                    StringBuilder strResults;
                    strResults = myScope.DoQueryString("*IDN?");
                    sRes = strResults.ToString();
                }
                catch
                {

                }
                return sRes;
            }
            public void PortClose()
            {
                myScope.Close();
            }
            public string Query(string name)
            {
                string sRes = "";
                try
                {
                    StringBuilder strResults;
                    strResults = myScope.DoQueryString(name);
                    sRes = strResults.ToString();
                }
                catch
                {

                }
                return sRes;
            }

            public void Write(string name)
            {

                try
                {
                    Thread.Sleep(100);
                    myScope.DoCommand(name);
                    Thread.Sleep(100);
                }
                catch
                {

                }

            }

            public void Single()
            {

                try
                {

                    myScope.DoCommand(":SINGle");

                }
                catch
                {

                }

            }
            public void Capture(string path)
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    //  myScope.DoCommand(":MEASure:SOURce CHANnel1");
                    //  Console.WriteLine("Measure source: {0}",
                    //      myScope.DoQueryString(":MEASure:SOURce?"));
                    ////  myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    //  double fResult;
                    //  myScope.DoCommand(":MEASure:FREQuency");
                    //  fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    //  Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    //  myScope.DoCommand(":MEASure:VAMPlitude");
                    //  fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    //  Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    //  // Download the screen image.
                    //  // -----------------------------------------------------------
                    //  myScope.DoCommand(":HARDcopy:INKSaver OFF");

                    //  // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor",
                         out ResultsArray);

                    // Store the screen data to a file.
                    strPath = path;
                    FileStream fStream = File.Open(strPath, FileMode.Create);
                    fStream.Write(ResultsArray, 0, nLength);
                    fStream.Close();
                    Console.WriteLine("Screen image ({0} bytes) written to {1}",
                        nLength, strPath);

                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    //myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    //Console.WriteLine("Waveform points mode: {0}",
                    //    myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    //// Get the number of waveform points available.
                    //myScope.DoCommand(":WAVeform:POINts 10240");
                    //Console.WriteLine("Waveform points available: {0}",
                    //    myScope.DoQueryString(":WAVeform:POINts?"));

                    //// Set the waveform source.
                    //myScope.DoCommand(":WAVeform:SOURce CHANnel1");
                    //Console.WriteLine("Waveform source: {0}",
                    //    myScope.DoQueryString(":WAVeform:SOURce?"));

                    //// Choose the format of the data returned (WORD, BYTE, ASCII):
                    //myScope.DoCommand(":WAVeform:FORMat BYTE");
                    //Console.WriteLine("Waveform format: {0}",
                    //    myScope.DoQueryString(":WAVeform:FORMat?"));

                    //// Display the waveform settings:
                    //double[] fResultsArray;
                    //fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    //double fFormat = fResultsArray[0];
                    //if (fFormat == 0.0)
                    //{
                    //    Console.WriteLine("Waveform format: BYTE");
                    //}
                    //else if (fFormat == 1.0)
                    //{
                    //    Console.WriteLine("Waveform format: WORD");
                    //}
                    //else if (fFormat == 2.0)
                    //{
                    //    Console.WriteLine("Waveform format: ASCii");
                    //}

                    //double fType = fResultsArray[1];
                    //if (fType == 0.0)
                    //{
                    //    Console.WriteLine("Acquire type: NORMal");
                    //}
                    //else if (fType == 1.0)
                    //{
                    //    Console.WriteLine("Acquire type: PEAK");
                    //}
                    //else if (fType == 2.0)
                    //{
                    //    Console.WriteLine("Acquire type: AVERage");
                    //}
                    //else if (fType == 3.0)
                    //{
                    //    Console.WriteLine("Acquire type: HRESolution");
                    //}

                    //double fPoints = fResultsArray[2];
                    //Console.WriteLine("Waveform points: {0:e}", fPoints);

                    //double fCount = fResultsArray[3];
                    //Console.WriteLine("Waveform average count: {0:e}", fCount);

                    //double fXincrement = fResultsArray[4];
                    //Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    //double fXorigin = fResultsArray[5];
                    //Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    //double fXreference = fResultsArray[6];
                    //Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    //double fYincrement = fResultsArray[7];
                    //Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    //double fYorigin = fResultsArray[8];
                    //Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    //double fYreference = fResultsArray[9];
                    //Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    //// Read waveform data.
                    //nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                    //    out ResultsArray);
                    //Console.WriteLine("Number of data values: {0}", nLength);

                    //// Set up output file:
                    //strPath = "c:\\scope\\data\\waveform_data.csv";
                    //if (File.Exists(strPath)) File.Delete(strPath);

                    //// Open file for output.
                    //StreamWriter writer = File.CreateText(strPath);

                    //// Output waveform data in CSV format.
                    //for (int i = 0; i < nLength - 1; i++)
                    //    writer.WriteLine("{0:f9}, {1:f6}",
                    //        fXorigin + ((float)i * fXincrement),
                    //        (((float)ResultsArray[i] - fYreference) *
                    //        fYincrement) + fYorigin);

                    //// Close output file.
                    //writer.Close();
                    //Console.WriteLine("Waveform format BYTE data written to {0}",
                    //    strPath);
                }
                catch
                {


                }
            }
            public double[,] Analyze()
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":MEASure:SOURce CHANnel1");
                    Console.WriteLine("Measure source: {0}",
                        myScope.DoQueryString(":MEASure:SOURce?"));
                    // myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    double fResult;
                    myScope.DoCommand(":MEASure:FREQuency");
                    fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    myScope.DoCommand(":MEASure:VAMPlitude");
                    fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    // Download the screen image.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":HARDcopy:INKSaver OFF");

                    // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor",
                        out ResultsArray);

                    // Store the screen data to a file.



                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    Console.WriteLine("Waveform points mode: {0}",
                        myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    // Get the number of waveform points available.
                    myScope.DoCommand(":WAVeform:POINts 1024");
                    Console.WriteLine("Waveform points available: {0}",
                        myScope.DoQueryString(":WAVeform:POINts?"));

                    // Set the waveform source.
                    myScope.DoCommand(":WAVeform:SOURce CHANnel1");
                    Console.WriteLine("Waveform source: {0}",
                        myScope.DoQueryString(":WAVeform:SOURce?"));

                    // Choose the format of the data returned (WORD, BYTE, ASCII):
                    myScope.DoCommand(":WAVeform:FORMat BYTE");
                    Console.WriteLine("Waveform format: {0}",
                        myScope.DoQueryString(":WAVeform:FORMat?"));

                    // Display the waveform settings:
                    double[] fResultsArray;
                    fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    double fFormat = fResultsArray[0];
                    if (fFormat == 0.0)
                    {
                        Console.WriteLine("Waveform format: BYTE");
                    }
                    else if (fFormat == 1.0)
                    {
                        Console.WriteLine("Waveform format: WORD");
                    }
                    else if (fFormat == 2.0)
                    {
                        Console.WriteLine("Waveform format: ASCii");
                    }

                    double fType = fResultsArray[1];
                    if (fType == 0.0)
                    {
                        Console.WriteLine("Acquire type: NORMal");
                    }
                    else if (fType == 1.0)
                    {
                        Console.WriteLine("Acquire type: PEAK");
                    }
                    else if (fType == 2.0)
                    {
                        Console.WriteLine("Acquire type: AVERage");
                    }
                    else if (fType == 3.0)
                    {
                        Console.WriteLine("Acquire type: HRESolution");
                    }

                    double fPoints = fResultsArray[2];
                    Console.WriteLine("Waveform points: {0:e}", fPoints);

                    double fCount = fResultsArray[3];
                    Console.WriteLine("Waveform average count: {0:e}", fCount);

                    double fXincrement = fResultsArray[4];
                    Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    double fXorigin = fResultsArray[5];
                    Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    double fXreference = fResultsArray[6];
                    Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    double fYincrement = fResultsArray[7];
                    Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    double fYorigin = fResultsArray[8];
                    Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    double fYreference = fResultsArray[9];
                    Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    // Read waveform data.
                    nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                        out ResultsArray);
                    Console.WriteLine("Number of data values: {0}", nLength);

                    // Set up output file:

                    // Open file for output.

                    double[,] value = new double[2, nLength];
                    // Output waveform data in CSV format.
                    for (int i = 0; i < nLength - 1; i++)
                    {
                        value[0, i] = fXorigin + ((float)i * fXincrement);
                        value[1, i] = (((float)ResultsArray[i] - fYreference) * fYincrement) + fYorigin;

                    }
                    // Close output file.

                    return value;

                }
                catch
                {
                    double[,] value = new double[2, 10000];

                    return value;
                }

            }

            public double[,] Analyze2()
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":MEASure:SOURce CHANnel2");
                    Console.WriteLine("Measure source: {0}",
                        myScope.DoQueryString(":MEASure:SOURce?"));
                    // myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    double fResult;
                    myScope.DoCommand(":MEASure:FREQuency");
                    fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    myScope.DoCommand(":MEASure:VAMPlitude");
                    fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    // Download the screen image.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":HARDcopy:INKSaver OFF");

                    // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor",
                        out ResultsArray);

                    // Store the screen data to a file.



                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    Console.WriteLine("Waveform points mode: {0}",
                        myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    // Get the number of waveform points available.
                    myScope.DoCommand(":WAVeform:POINts 10240");
                    Console.WriteLine("Waveform points available: {0}",
                        myScope.DoQueryString(":WAVeform:POINts?"));

                    // Set the waveform source.
                    myScope.DoCommand(":WAVeform:SOURce CHANnel2");
                    Console.WriteLine("Waveform source: {0}",
                        myScope.DoQueryString(":WAVeform:SOURce?"));

                    // Choose the format of the data returned (WORD, BYTE, ASCII):
                    myScope.DoCommand(":WAVeform:FORMat BYTE");
                    Console.WriteLine("Waveform format: {0}",
                        myScope.DoQueryString(":WAVeform:FORMat?"));

                    // Display the waveform settings:
                    double[] fResultsArray;
                    fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    double fFormat = fResultsArray[0];
                    if (fFormat == 0.0)
                    {
                        Console.WriteLine("Waveform format: BYTE");
                    }
                    else if (fFormat == 1.0)
                    {
                        Console.WriteLine("Waveform format: WORD");
                    }
                    else if (fFormat == 2.0)
                    {
                        Console.WriteLine("Waveform format: ASCii");
                    }

                    double fType = fResultsArray[1];
                    if (fType == 0.0)
                    {
                        Console.WriteLine("Acquire type: NORMal");
                    }
                    else if (fType == 1.0)
                    {
                        Console.WriteLine("Acquire type: PEAK");
                    }
                    else if (fType == 2.0)
                    {
                        Console.WriteLine("Acquire type: AVERage");
                    }
                    else if (fType == 3.0)
                    {
                        Console.WriteLine("Acquire type: HRESolution");
                    }

                    double fPoints = fResultsArray[2];
                    Console.WriteLine("Waveform points: {0:e}", fPoints);

                    double fCount = fResultsArray[3];
                    Console.WriteLine("Waveform average count: {0:e}", fCount);

                    double fXincrement = fResultsArray[4];
                    Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    double fXorigin = fResultsArray[5];
                    Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    double fXreference = fResultsArray[6];
                    Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    double fYincrement = fResultsArray[7];
                    Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    double fYorigin = fResultsArray[8];
                    Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    double fYreference = fResultsArray[9];
                    Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    // Read waveform data.
                    nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                        out ResultsArray);
                    Console.WriteLine("Number of data values: {0}", nLength);

                    // Set up output file:

                    // Open file for output.

                    double[,] value = new double[2, nLength];
                    // Output waveform data in CSV format.
                    for (int i = 0; i < nLength - 1; i++)
                    {
                        value[0, i] = fXorigin + ((float)i * fXincrement);
                        value[1, i] = (((float)ResultsArray[i] - fYreference) * fYincrement) + fYorigin;

                    }
                    // Close output file.

                    return value;

                }
                catch
                {
                    double[,] value = new double[2, 10000];

                    return value;
                }

            }

            class VisaInstrument
            {
                private int m_nResourceManager;
                private int m_nSession;
                private string m_strVisaAddress;

                // Constructor.
                public VisaInstrument(string strVisaAddress)
                {
                    // Save VISA addres in member variable.
                    m_strVisaAddress = strVisaAddress;

                    // Open the default VISA resource manager.
                    OpenResourceManager();

                    // Open a VISA resource session.
                    OpenSession();

                    // Clear the interface.
                    int nViStatus;
                    nViStatus = visa32.viClear(m_nSession);
                }

                public void DoCommand(string strCommand)
                {
                    // Send the command.
                    VisaSendCommandOrQuery(strCommand);

                    // Check for inst errors.
                    CheckInstrumentErrors(strCommand);
                }

                public int DoCommandIEEEBlock(string strCommand,
                  byte[] DataArray)
                {
                    // Send the command to the device.
                    string strCommandAndLength;
                    int nViStatus, nLength, nBytesWritten;

                    nLength = DataArray.Length;
                    strCommandAndLength = String.Format("{0} #8%08d",
                      strCommand);

                    // Write first part of command to formatted I/O write buffer.
                    nViStatus = visa32.viPrintf(m_nSession, strCommandAndLength,
                      nLength);
                    CheckVisaStatus(nViStatus);

                    // Write the data to the formatted I/O write buffer.
                    nViStatus = visa32.viBufWrite(m_nSession, DataArray, nLength,
                      out nBytesWritten);
                    CheckVisaStatus(nViStatus);

                    // Check for inst errors.
                    CheckInstrumentErrors(strCommand);

                    return nBytesWritten;
                }

                public StringBuilder DoQueryString(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    StringBuilder strResults = new StringBuilder(1000);
                    strResults = VisaGetResultString();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return strResults;
                }

                public double DoQueryNumber(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    double fResults;
                    fResults = VisaGetResultNumber();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return fResults;
                }

                public double[] DoQueryNumbers(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    double[] fResultsArray;
                    fResultsArray = VisaGetResultNumbers();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return fResultsArray;
                }

                public int DoQueryIEEEBlock(string strQuery,
                  out byte[] ResultsArray)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    int length;   // Number of bytes returned from instrument.
                    length = VisaGetResultIEEEBlock(out ResultsArray);

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return length;
                }

                private void VisaSendCommandOrQuery(string strCommandOrQuery)
                {
                    // Send command or query to the device.
                    string strWithNewline;
                    strWithNewline = String.Format("{0}\n", strCommandOrQuery);
                    int nViStatus;
                    nViStatus = visa32.viPrintf(m_nSession, strWithNewline);
                    CheckVisaStatus(nViStatus);
                }

                private StringBuilder VisaGetResultString()
                {
                    StringBuilder strResults = new StringBuilder(1000);

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%1000t", strResults);
                    CheckVisaStatus(nViStatus);

                    return strResults;
                }

                private double VisaGetResultNumber()
                {
                    double fResults = 0;

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%lf", out fResults);
                    CheckVisaStatus(nViStatus);

                    return fResults;
                }

                private double[] VisaGetResultNumbers()
                {
                    double[] fResultsArray;
                    fResultsArray = new double[10];

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%,10lf\n",
                        fResultsArray);
                    CheckVisaStatus(nViStatus);

                    return fResultsArray;
                }

                private int VisaGetResultIEEEBlock(out byte[] ResultsArray)
                {
                    // Results array, big enough to hold a PNG.
                    ResultsArray = new byte[300000];
                    int length;   // Number of bytes returned from instrument.

                    // Set the default number of bytes that will be contained in
                    // the ResultsArray to 300,000 (300kB).
                    length = 300000;

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%#b", ref length,
                      ResultsArray);
                    CheckVisaStatus(nViStatus);

                    // Write and read buffers need to be flushed after IEEE block?
                    nViStatus = visa32.viFlush(m_nSession, visa32.VI_WRITE_BUF);
                    CheckVisaStatus(nViStatus);

                    nViStatus = visa32.viFlush(m_nSession, visa32.VI_READ_BUF);
                    CheckVisaStatus(nViStatus);

                    return length;
                }

                private void CheckInstrumentErrors(string strCommand)
                {
                    // Check for instrument errors.
                    StringBuilder strInstrumentError = new StringBuilder(1000);
                    bool bFirstError = true;

                    do   // While not "0,No error"
                    {
                        VisaSendCommandOrQuery(":SYSTem:ERRor?");
                        strInstrumentError = VisaGetResultString();

                        if (!strInstrumentError.ToString().StartsWith("+0,"))
                        {
                            if (bFirstError)
                            {
                                Console.WriteLine("ERROR(s) for command '{0}': ",
                                  strCommand);
                                bFirstError = false;
                            }
                            Console.Write(strInstrumentError);
                        }
                    } while (!strInstrumentError.ToString().StartsWith("+0,"));
                }

                private void OpenResourceManager()
                {
                    int nViStatus;
                    nViStatus =
                      visa32.viOpenDefaultRM(out this.m_nResourceManager);
                    if (nViStatus < visa32.VI_SUCCESS)
                        throw new
                          ApplicationException("Failed to open Resource Manager");
                }

                private void OpenSession()
                {
                    int nViStatus;
                    nViStatus = visa32.viOpen(this.m_nResourceManager,
                      this.m_strVisaAddress, visa32.VI_NO_LOCK,
                      visa32.VI_TMO_IMMEDIATE, out this.m_nSession);
                    CheckVisaStatus(nViStatus);
                }

                public void SetTimeoutSeconds(int nSeconds)
                {
                    int nViStatus;
                    nViStatus = visa32.viSetAttribute(this.m_nSession,
                      visa32.VI_ATTR_TMO_VALUE, nSeconds * 1000);
                    CheckVisaStatus(nViStatus);
                }

                public void CheckVisaStatus(int nViStatus)
                {
                    // If VISA error, throw exception.
                    if (nViStatus < visa32.VI_SUCCESS)
                    {
                        StringBuilder strError = new StringBuilder(256);
                        visa32.viStatusDesc(this.m_nResourceManager, nViStatus,
                          strError);
                        throw new ApplicationException(strError.ToString());
                    }
                }

                public void Close()
                {
                    if (m_nSession != 0)
                        visa32.viClose(m_nSession);
                    if (m_nResourceManager != 0)
                        visa32.viClose(m_nResourceManager);
                }
            }
        }



        public class VisaInstrumentApp
        {
            private VisaInstrument myScope;

            public VisaInstrumentApp()
            {
                myScope = new VisaInstrument("USB0::0x0957::0x1796::MY56202041::0::INSTR");

            }


            public string SelfTest()
            {
                string sRes = "";
                try
                {
                    StringBuilder strResults;
                    strResults = myScope.DoQueryString("*IDN?");
                    sRes = strResults.ToString();
                }
                catch
                {

                }
                return sRes;
            }
            public string Query(string name)
            {
                string sRes = "";
                try
                {
                    StringBuilder strResults;
                    strResults = myScope.DoQueryString(name);
                    sRes = strResults.ToString();
                }
                catch
                {

                }
                return sRes;
            }
            public void Write(string name)
            {

                try
                {
                    Thread.Sleep(100);
                    myScope.DoCommand(name);
                    Thread.Sleep(100);
                }
                catch
                {

                }

            }

            public void Single()
            {

                try
                {

                    myScope.DoCommand(":SINGle");

                }
                catch
                {

                }

            }
            public void Capture(string path)
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    //  myScope.DoCommand(":MEASure:SOURce CHANnel1");
                    //  Console.WriteLine("Measure source: {0}",
                    //      myScope.DoQueryString(":MEASure:SOURce?"));
                    ////  myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    //  double fResult;
                    //  myScope.DoCommand(":MEASure:FREQuency");
                    //  fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    //  Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    //  myScope.DoCommand(":MEASure:VAMPlitude");
                    //  fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    //  Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    //  // Download the screen image.
                    //  // -----------------------------------------------------------
                    // myScope.DoCommand(":HARDcopy:INKSaver OFF");
                    //  // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor", out ResultsArray);
                    //Store the screen data to a file.

                    strPath = path;
                    // strPath = @"c:\data\123.png";
                    FileStream fStream = File.Open(strPath, FileMode.Create);
                    fStream.Write(ResultsArray, 0, nLength);
                    fStream.Close();
                    Console.WriteLine("Screen image ({0} bytes) written to {1}", nLength, strPath);

                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    //myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    //Console.WriteLine("Waveform points mode: {0}",
                    //    myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    //// Get the number of waveform points available.
                    //myScope.DoCommand(":WAVeform:POINts 10240");
                    //Console.WriteLine("Waveform points available: {0}",
                    //    myScope.DoQueryString(":WAVeform:POINts?"));

                    //// Set the waveform source.
                    //myScope.DoCommand(":WAVeform:SOURce CHANnel1");
                    //Console.WriteLine("Waveform source: {0}",
                    //    myScope.DoQueryString(":WAVeform:SOURce?"));

                    //// Choose the format of the data returned (WORD, BYTE, ASCII):
                    //myScope.DoCommand(":WAVeform:FORMat BYTE");
                    //Console.WriteLine("Waveform format: {0}",
                    //    myScope.DoQueryString(":WAVeform:FORMat?"));

                    //// Display the waveform settings:
                    //double[] fResultsArray;
                    //fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    //double fFormat = fResultsArray[0];
                    //if (fFormat == 0.0)
                    //{
                    //    Console.WriteLine("Waveform format: BYTE");
                    //}
                    //else if (fFormat == 1.0)
                    //{
                    //    Console.WriteLine("Waveform format: WORD");
                    //}
                    //else if (fFormat == 2.0)
                    //{
                    //    Console.WriteLine("Waveform format: ASCii");
                    //}

                    //double fType = fResultsArray[1];
                    //if (fType == 0.0)
                    //{
                    //    Console.WriteLine("Acquire type: NORMal");
                    //}
                    //else if (fType == 1.0)
                    //{
                    //    Console.WriteLine("Acquire type: PEAK");
                    //}
                    //else if (fType == 2.0)
                    //{
                    //    Console.WriteLine("Acquire type: AVERage");
                    //}
                    //else if (fType == 3.0)
                    //{
                    //    Console.WriteLine("Acquire type: HRESolution");
                    //}

                    //double fPoints = fResultsArray[2];
                    //Console.WriteLine("Waveform points: {0:e}", fPoints);

                    //double fCount = fResultsArray[3];
                    //Console.WriteLine("Waveform average count: {0:e}", fCount);

                    //double fXincrement = fResultsArray[4];
                    //Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    //double fXorigin = fResultsArray[5];
                    //Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    //double fXreference = fResultsArray[6];
                    //Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    //double fYincrement = fResultsArray[7];
                    //Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    //double fYorigin = fResultsArray[8];
                    //Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    //double fYreference = fResultsArray[9];
                    //Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    //// Read waveform data.
                    //nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                    //    out ResultsArray);
                    //Console.WriteLine("Number of data values: {0}", nLength);

                    //// Set up output file:
                    //strPath = "c:\\scope\\data\\waveform_data.csv";
                    //if (File.Exists(strPath)) File.Delete(strPath);

                    //// Open file for output.
                    //StreamWriter writer = File.CreateText(strPath);

                    //// Output waveform data in CSV format.
                    //for (int i = 0; i < nLength - 1; i++)
                    //    writer.WriteLine("{0:f9}, {1:f6}",
                    //        fXorigin + ((float)i * fXincrement),
                    //        (((float)ResultsArray[i] - fYreference) *
                    //        fYincrement) + fYorigin);

                    //// Close output file.
                    //writer.Close();
                    //Console.WriteLine("Waveform format BYTE data written to {0}",
                    //    strPath);
                }
                catch
                {


                }
            }
            public double[,] Analyze()
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":MEASure:SOURce CHANnel1");
                    Console.WriteLine("Measure source: {0}",
                        myScope.DoQueryString(":MEASure:SOURce?"));
                    // myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    double fResult;
                    myScope.DoCommand(":MEASure:FREQuency");
                    fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    myScope.DoCommand(":MEASure:VAMPlitude");
                    fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    // Download the screen image.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":HARDcopy:INKSaver OFF");

                    // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor",
                        out ResultsArray);

                    // Store the screen data to a file.



                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    Console.WriteLine("Waveform points mode: {0}",
                        myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    // Get the number of waveform points available.
                    myScope.DoCommand(":WAVeform:POINts 1024");
                    Console.WriteLine("Waveform points available: {0}",
                        myScope.DoQueryString(":WAVeform:POINts?"));

                    // Set the waveform source.
                    myScope.DoCommand(":WAVeform:SOURce CHANnel1");
                    Console.WriteLine("Waveform source: {0}",
                        myScope.DoQueryString(":WAVeform:SOURce?"));

                    // Choose the format of the data returned (WORD, BYTE, ASCII):
                    myScope.DoCommand(":WAVeform:FORMat BYTE");
                    Console.WriteLine("Waveform format: {0}",
                        myScope.DoQueryString(":WAVeform:FORMat?"));

                    // Display the waveform settings:
                    double[] fResultsArray;
                    fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    double fFormat = fResultsArray[0];
                    if (fFormat == 0.0)
                    {
                        Console.WriteLine("Waveform format: BYTE");
                    }
                    else if (fFormat == 1.0)
                    {
                        Console.WriteLine("Waveform format: WORD");
                    }
                    else if (fFormat == 2.0)
                    {
                        Console.WriteLine("Waveform format: ASCii");
                    }

                    double fType = fResultsArray[1];
                    if (fType == 0.0)
                    {
                        Console.WriteLine("Acquire type: NORMal");
                    }
                    else if (fType == 1.0)
                    {
                        Console.WriteLine("Acquire type: PEAK");
                    }
                    else if (fType == 2.0)
                    {
                        Console.WriteLine("Acquire type: AVERage");
                    }
                    else if (fType == 3.0)
                    {
                        Console.WriteLine("Acquire type: HRESolution");
                    }

                    double fPoints = fResultsArray[2];
                    Console.WriteLine("Waveform points: {0:e}", fPoints);

                    double fCount = fResultsArray[3];
                    Console.WriteLine("Waveform average count: {0:e}", fCount);

                    double fXincrement = fResultsArray[4];
                    Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    double fXorigin = fResultsArray[5];
                    Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    double fXreference = fResultsArray[6];
                    Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    double fYincrement = fResultsArray[7];
                    Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    double fYorigin = fResultsArray[8];
                    Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    double fYreference = fResultsArray[9];
                    Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    // Read waveform data.
                    nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                        out ResultsArray);
                    Console.WriteLine("Number of data values: {0}", nLength);

                    // Set up output file:

                    // Open file for output.

                    double[,] value = new double[2, nLength];
                    // Output waveform data in CSV format.
                    for (int i = 0; i < nLength - 1; i++)
                    {
                        value[0, i] = fXorigin + ((float)i * fXincrement);
                        value[1, i] = (((float)ResultsArray[i] - fYreference) * fYincrement) + fYorigin;

                    }
                    // Close output file.

                    return value;

                }
                catch
                {
                    double[,] value = new double[2, 10000];

                    return value;
                }

            }

            public double[,] Analyze2()
            {

                byte[] ResultsArray;   // Results array.
                int nLength;   // Number of bytes returned from instrument.
                string strPath;
                try
                {
                    // Make a couple of measurements.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":MEASure:SOURce CHANnel2");
                    Console.WriteLine("Measure source: {0}",
                        myScope.DoQueryString(":MEASure:SOURce?"));
                    // myScope.DoCommand(":HORIZONTAL:MAIN:SECdiv 100");
                    double fResult;
                    myScope.DoCommand(":MEASure:FREQuency");
                    fResult = myScope.DoQueryNumber(":MEASure:FREQuency?");
                    Console.WriteLine("Frequency: {0:F4} kHz", fResult / 1000);

                    myScope.DoCommand(":MEASure:VAMPlitude");
                    fResult = myScope.DoQueryNumber(":MEASure:VAMPlitude?");
                    Console.WriteLine("Vertial amplitude: {0:F2} V", fResult);

                    // Download the screen image.
                    // -----------------------------------------------------------
                    myScope.DoCommand(":HARDcopy:INKSaver OFF");

                    // Get the screen data.
                    nLength = myScope.DoQueryIEEEBlock(":DISPlay:DATA? PNG, COLor",
                        out ResultsArray);

                    // Store the screen data to a file.



                    // Download waveform data.
                    // -----------------------------------------------------------

                    // Set the waveform points mode.
                    myScope.DoCommand(":WAVeform:POINts:MODE RAW");
                    Console.WriteLine("Waveform points mode: {0}",
                        myScope.DoQueryString(":WAVeform:POINts:MODE?"));

                    // Get the number of waveform points available.
                    myScope.DoCommand(":WAVeform:POINts 10240");
                    Console.WriteLine("Waveform points available: {0}",
                        myScope.DoQueryString(":WAVeform:POINts?"));

                    // Set the waveform source.
                    myScope.DoCommand(":WAVeform:SOURce CHANnel2");
                    Console.WriteLine("Waveform source: {0}",
                        myScope.DoQueryString(":WAVeform:SOURce?"));

                    // Choose the format of the data returned (WORD, BYTE, ASCII):
                    myScope.DoCommand(":WAVeform:FORMat BYTE");
                    Console.WriteLine("Waveform format: {0}",
                        myScope.DoQueryString(":WAVeform:FORMat?"));

                    // Display the waveform settings:
                    double[] fResultsArray;
                    fResultsArray = myScope.DoQueryNumbers(":WAVeform:PREamble?");

                    double fFormat = fResultsArray[0];
                    if (fFormat == 0.0)
                    {
                        Console.WriteLine("Waveform format: BYTE");
                    }
                    else if (fFormat == 1.0)
                    {
                        Console.WriteLine("Waveform format: WORD");
                    }
                    else if (fFormat == 2.0)
                    {
                        Console.WriteLine("Waveform format: ASCii");
                    }

                    double fType = fResultsArray[1];
                    if (fType == 0.0)
                    {
                        Console.WriteLine("Acquire type: NORMal");
                    }
                    else if (fType == 1.0)
                    {
                        Console.WriteLine("Acquire type: PEAK");
                    }
                    else if (fType == 2.0)
                    {
                        Console.WriteLine("Acquire type: AVERage");
                    }
                    else if (fType == 3.0)
                    {
                        Console.WriteLine("Acquire type: HRESolution");
                    }

                    double fPoints = fResultsArray[2];
                    Console.WriteLine("Waveform points: {0:e}", fPoints);

                    double fCount = fResultsArray[3];
                    Console.WriteLine("Waveform average count: {0:e}", fCount);

                    double fXincrement = fResultsArray[4];
                    Console.WriteLine("Waveform X increment: {0:e}", fXincrement);

                    double fXorigin = fResultsArray[5];
                    Console.WriteLine("Waveform X origin: {0:e}", fXorigin);

                    double fXreference = fResultsArray[6];
                    Console.WriteLine("Waveform X reference: {0:e}", fXreference);

                    double fYincrement = fResultsArray[7];
                    Console.WriteLine("Waveform Y increment: {0:e}", fYincrement);

                    double fYorigin = fResultsArray[8];
                    Console.WriteLine("Waveform Y origin: {0:e}", fYorigin);

                    double fYreference = fResultsArray[9];
                    Console.WriteLine("Waveform Y reference: {0:e}", fYreference);

                    // Read waveform data.
                    nLength = myScope.DoQueryIEEEBlock(":WAVeform:DATA?",
                        out ResultsArray);
                    Console.WriteLine("Number of data values: {0}", nLength);

                    // Set up output file:

                    // Open file for output.

                    double[,] value = new double[2, nLength];
                    // Output waveform data in CSV format.
                    for (int i = 0; i < nLength - 1; i++)
                    {
                        value[0, i] = fXorigin + ((float)i * fXincrement);
                        value[1, i] = (((float)ResultsArray[i] - fYreference) * fYincrement) + fYorigin;

                    }
                    // Close output file.

                    return value;

                }
                catch
                {
                    double[,] value = new double[2, 10000];

                    return value;
                }

            }

            class VisaInstrument
            {
                private int m_nResourceManager;
                private int m_nSession;
                private string m_strVisaAddress;

                // Constructor.
                public VisaInstrument(string strVisaAddress)
                {
                    // Save VISA addres in member variable.
                    m_strVisaAddress = strVisaAddress;

                    // Open the default VISA resource manager.
                    OpenResourceManager();

                    // Open a VISA resource session.
                    OpenSession();

                    // Clear the interface.
                    int nViStatus;
                    nViStatus = visa32.viClear(m_nSession);
                }

                public void DoCommand(string strCommand)
                {
                    // Send the command.
                    VisaSendCommandOrQuery(strCommand);

                    // Check for inst errors.
                    CheckInstrumentErrors(strCommand);
                }

                public int DoCommandIEEEBlock(string strCommand,
                  byte[] DataArray)
                {
                    // Send the command to the device.
                    string strCommandAndLength;
                    int nViStatus, nLength, nBytesWritten;

                    nLength = DataArray.Length;
                    strCommandAndLength = String.Format("{0} #8%08d",
                      strCommand);

                    // Write first part of command to formatted I/O write buffer.
                    nViStatus = visa32.viPrintf(m_nSession, strCommandAndLength,
                      nLength);
                    CheckVisaStatus(nViStatus);

                    // Write the data to the formatted I/O write buffer.
                    nViStatus = visa32.viBufWrite(m_nSession, DataArray, nLength,
                      out nBytesWritten);
                    CheckVisaStatus(nViStatus);

                    // Check for inst errors.
                    CheckInstrumentErrors(strCommand);

                    return nBytesWritten;
                }

                public StringBuilder DoQueryString(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    StringBuilder strResults = new StringBuilder(1000);
                    strResults = VisaGetResultString();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return strResults;
                }

                public double DoQueryNumber(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    double fResults;
                    fResults = VisaGetResultNumber();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return fResults;
                }

                public double[] DoQueryNumbers(string strQuery)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    double[] fResultsArray;
                    fResultsArray = VisaGetResultNumbers();

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return fResultsArray;
                }

                public int DoQueryIEEEBlock(string strQuery,
                  out byte[] ResultsArray)
                {
                    // Send the query.
                    VisaSendCommandOrQuery(strQuery);

                    // Get the result string.
                    int length;   // Number of bytes returned from instrument.
                    length = VisaGetResultIEEEBlock(out ResultsArray);

                    // Check for inst errors.
                    CheckInstrumentErrors(strQuery);

                    // Return string results.
                    return length;
                }

                private void VisaSendCommandOrQuery(string strCommandOrQuery)
                {
                    // Send command or query to the device.
                    string strWithNewline;
                    strWithNewline = String.Format("{0}\n", strCommandOrQuery);
                    int nViStatus;
                    nViStatus = visa32.viPrintf(m_nSession, strWithNewline);
                    CheckVisaStatus(nViStatus);
                }

                private StringBuilder VisaGetResultString()
                {
                    StringBuilder strResults = new StringBuilder(1000);

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%1000t", strResults);
                    CheckVisaStatus(nViStatus);

                    return strResults;
                }

                private double VisaGetResultNumber()
                {
                    double fResults = 0;

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%lf", out fResults);
                    CheckVisaStatus(nViStatus);

                    return fResults;
                }

                private double[] VisaGetResultNumbers()
                {
                    double[] fResultsArray;
                    fResultsArray = new double[10];

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%,10lf\n",
                        fResultsArray);
                    CheckVisaStatus(nViStatus);

                    return fResultsArray;
                }

                private int VisaGetResultIEEEBlock(out byte[] ResultsArray)
                {
                    // Results array, big enough to hold a PNG.
                    ResultsArray = new byte[300000];
                    int length;   // Number of bytes returned from instrument.

                    // Set the default number of bytes that will be contained in
                    // the ResultsArray to 300,000 (300kB).
                    length = 300000;

                    // Read return value string from the device.
                    int nViStatus;
                    nViStatus = visa32.viScanf(m_nSession, "%#b", ref length,
                      ResultsArray);
                    CheckVisaStatus(nViStatus);

                    // Write and read buffers need to be flushed after IEEE block?
                    nViStatus = visa32.viFlush(m_nSession, visa32.VI_WRITE_BUF);
                    CheckVisaStatus(nViStatus);

                    nViStatus = visa32.viFlush(m_nSession, visa32.VI_READ_BUF);
                    CheckVisaStatus(nViStatus);

                    return length;
                }

                private void CheckInstrumentErrors(string strCommand)
                {
                    // Check for instrument errors.
                    StringBuilder strInstrumentError = new StringBuilder(1000);
                    bool bFirstError = true;

                    do   // While not "0,No error"
                    {
                        VisaSendCommandOrQuery(":SYSTem:ERRor?");
                        strInstrumentError = VisaGetResultString();

                        if (!strInstrumentError.ToString().StartsWith("+0,"))
                        {
                            if (bFirstError)
                            {
                                Console.WriteLine("ERROR(s) for command '{0}': ",
                                  strCommand);
                                bFirstError = false;
                            }
                            Console.Write(strInstrumentError);
                        }
                    } while (!strInstrumentError.ToString().StartsWith("+0,"));
                }

                private void OpenResourceManager()
                {
                    int nViStatus;
                    nViStatus =
                      visa32.viOpenDefaultRM(out this.m_nResourceManager);
                    if (nViStatus < visa32.VI_SUCCESS)
                        throw new
                          ApplicationException("Failed to open Resource Manager");
                }

                private void OpenSession()
                {
                    int nViStatus;
                    nViStatus = visa32.viOpen(this.m_nResourceManager,
                      this.m_strVisaAddress, visa32.VI_NO_LOCK,
                      visa32.VI_TMO_IMMEDIATE, out this.m_nSession);
                    CheckVisaStatus(nViStatus);
                }

                public void SetTimeoutSeconds(int nSeconds)
                {
                    int nViStatus;
                    nViStatus = visa32.viSetAttribute(this.m_nSession,
                      visa32.VI_ATTR_TMO_VALUE, nSeconds * 1000);
                    CheckVisaStatus(nViStatus);
                }

                public void CheckVisaStatus(int nViStatus)
                {
                    // If VISA error, throw exception.
                    if (nViStatus < visa32.VI_SUCCESS)
                    {
                        StringBuilder strError = new StringBuilder(256);
                        visa32.viStatusDesc(this.m_nResourceManager, nViStatus,
                          strError);
                        throw new ApplicationException(strError.ToString());
                    }
                }

                public void Close()
                {
                    if (m_nSession != 0)
                        visa32.viClose(m_nSession);
                    if (m_nResourceManager != 0)
                        visa32.viClose(m_nResourceManager);
                }
            }
        }

        public class DPS5005_1
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public DPS5005_1(string sPort)
            {
                iLimitSecond = 20.0f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            private void SendData(byte[] sCMD, int len)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(sCMD, 0, len);

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }


            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }

            public void setOnOff(int addr, bool onoff)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x09, 0x00, 0x01, 0x98, 0x08 };
                if (onoff == true)
                {
                    // 01 06 00 09 00 01 98 08
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x01;
                }
                else
                {
                    // 01 06 00 09 00 00 59 C8
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x00;
                }

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }


            public void setVolt(int addr, int volt)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x00, 0x04, 0xB0, 0x8A, 0xBE };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(volt / 256);
                sdata[5] = (byte)(volt % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);

            }

            public void setCurrent(int addr, int current)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x01, 0x02, 0xBC, 0xD8, 0xDB };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(current / 256);
                sdata[5] = (byte)(current % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }

            public string SelfTest(int addr)
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0x01, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x33 };

                    sCMD[0] = (byte)addr;
                    ushort crc = Constant.CRC16_0_b(sCMD, 7);
                    sCMD[7] = (byte)(crc & 0xFF);
                    sCMD[8] = (byte)(crc >> 8);
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    return ReturnValue();
                }
                catch
                {
                    return "-1";
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;
                byte[] temp = new byte[100];

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    int cnt = spCom.BytesToRead;

                    spCom.ReadTimeout = 1000;

                    // sRet += spCom.ReadExisting();
                    len = spCom.Read(temp, 0, cnt);

                    if (temp[0] != 0x00)
                    {
                        sRet = temp[0].ToString();
                    }
                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }
        public class DPS5005_2
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public DPS5005_2(string sPort)
            {
                iLimitSecond = 20.0f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            private void SendData(byte[] sCMD, int len)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(sCMD, 0, len);

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }

            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            public void setOnOff(int addr, bool onoff)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x09, 0x00, 0x01, 0x98, 0x08 };
                if (onoff == true)
                {
                    // 01 06 00 09 00 01 98 08
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x01;
                }
                else
                {
                    // 01 06 00 09 00 00 59 C8
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x00;
                }

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }


            public void setVolt(int addr, int volt)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x00, 0x04, 0xB0, 0x8A, 0xBE };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(volt / 256);
                sdata[5] = (byte)(volt % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);

            }

            public void setCurrent(int addr, int current)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x01, 0x02, 0xBC, 0xD8, 0xDB };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(current / 256);
                sdata[5] = (byte)(current % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }

            public string SelfTest(int addr)
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0x01, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x33 };

                    sCMD[0] = (byte)addr;
                    ushort crc = Constant.CRC16_0_b(sCMD, 7);
                    sCMD[7] = (byte)(crc & 0xFF);
                    sCMD[8] = (byte)(crc >> 8);
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    return ReturnValue();
                }
                catch
                {
                    return "-1";
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;
                byte[] temp = new byte[100];

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    int cnt = spCom.BytesToRead;

                    spCom.ReadTimeout = 1000;

                    // sRet += spCom.ReadExisting();
                    len = spCom.Read(temp, 0, cnt);

                    if (temp[0] != 0x00)
                    {
                        sRet = temp[0].ToString();
                    }
                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }
        public class DPS5005_3
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public DPS5005_3(string sPort)
            {
                iLimitSecond = 20.0f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            private void SendData(byte[] sCMD, int len)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(sCMD, 0, len);

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }


            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            public void setOnOff(int addr, bool onoff)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x09, 0x00, 0x01, 0x98, 0x08 };
                if (onoff == true)
                {
                    // 01 06 00 09 00 01 98 08
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x01;
                }
                else
                {
                    // 01 06 00 09 00 00 59 C8
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x00;
                }

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }


            public void setVolt(int addr, int volt)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x00, 0x04, 0xB0, 0x8A, 0xBE };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(volt / 256);
                sdata[5] = (byte)(volt % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);

            }

            public void setCurrent(int addr, int current)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x01, 0x02, 0xBC, 0xD8, 0xDB };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(current / 256);
                sdata[5] = (byte)(current % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }

            public string SelfTest(int addr)
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0x01, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x33 };

                    sCMD[0] = (byte)addr;
                    ushort crc = Constant.CRC16_0_b(sCMD, 7);
                    sCMD[7] = (byte)(crc & 0xFF);
                    sCMD[8] = (byte)(crc >> 8);
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    return ReturnValue();
                }
                catch
                {
                    return "-1";
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;
                byte[] temp = new byte[100];

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    int cnt = spCom.BytesToRead;

                    spCom.ReadTimeout = 1000;

                    // sRet += spCom.ReadExisting();
                    len = spCom.Read(temp, 0, cnt);

                    if (temp[0] != 0x00)
                    {
                        sRet = temp[0].ToString();
                    }
                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }
        public class DPS5005_4
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public DPS5005_4(string sPort)
            {
                iLimitSecond = 20.0f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            private void SendData(byte[] sCMD, int len)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(sCMD, 0, len);

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }


            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            public void setOnOff(int addr, bool onoff)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x09, 0x00, 0x01, 0x98, 0x08 };
                if (onoff == true)
                {
                    // 01 06 00 09 00 01 98 08
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x01;
                }
                else
                {
                    // 01 06 00 09 00 00 59 C8
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x00;
                }

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }


            public void setVolt(int addr, int volt)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x00, 0x04, 0xB0, 0x8A, 0xBE };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(volt / 256);
                sdata[5] = (byte)(volt % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);

            }

            public void setCurrent(int addr, int current)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x01, 0x02, 0xBC, 0xD8, 0xDB };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(current / 256);
                sdata[5] = (byte)(current % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }

            public string SelfTest(int addr)
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0x01, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x33 };

                    sCMD[0] = (byte)addr;
                    ushort crc = Constant.CRC16_0_b(sCMD, 7);
                    sCMD[7] = (byte)(crc & 0xFF);
                    sCMD[8] = (byte)(crc >> 8);
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    return ReturnValue();
                }
                catch
                {
                    return "-1";
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;
                byte[] temp = new byte[100];

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    int cnt = spCom.BytesToRead;

                    spCom.ReadTimeout = 1000;

                    // sRet += spCom.ReadExisting();
                    len = spCom.Read(temp, 0, cnt);

                    if (temp[0] != 0x00)
                    {
                        sRet = temp[0].ToString();
                    }
                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }
        public class DPS5005_5
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public DPS5005_5(string sPort)
            {
                iLimitSecond = 20.0f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            private void SendData(byte[] sCMD, int len)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(sCMD, 0, len);

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }


            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            public void setOnOff(int addr, bool onoff)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x09, 0x00, 0x01, 0x98, 0x08 };
                if (onoff == true)
                {
                    // 01 06 00 09 00 01 98 08
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x01;
                }
                else
                {
                    // 01 06 00 09 00 00 59 C8
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x00;
                }

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }


            public void setVolt(int addr, int volt)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x00, 0x04, 0xB0, 0x8A, 0xBE };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(volt / 256);
                sdata[5] = (byte)(volt % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);

            }

            public void setCurrent(int addr, int current)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x01, 0x02, 0xBC, 0xD8, 0xDB };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(current / 256);
                sdata[5] = (byte)(current % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }

            public string SelfTest(int addr)
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0x01, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x33 };

                    sCMD[0] = (byte)addr;
                    ushort crc = Constant.CRC16_0_b(sCMD, 7);
                    sCMD[7] = (byte)(crc & 0xFF);
                    sCMD[8] = (byte)(crc >> 8);
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    return ReturnValue();
                }
                catch
                {
                    return "-1";
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;
                byte[] temp = new byte[100];

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    int cnt = spCom.BytesToRead;

                    spCom.ReadTimeout = 1000;

                    // sRet += spCom.ReadExisting();
                    len = spCom.Read(temp, 0, cnt);

                    if (temp[0] != 0x00)
                    {
                        sRet = temp[0].ToString();
                    }
                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }
        public class DPS5005_6
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public DPS5005_6(string sPort)
            {
                iLimitSecond = 20.0f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            private void SendData(byte[] sCMD, int len)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(sCMD, 0, len);

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }


            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            public void setOnOff(int addr, bool onoff)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x09, 0x00, 0x01, 0x98, 0x08 };
                if (onoff == true)
                {
                    // 01 06 00 09 00 01 98 08
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x01;
                }
                else
                {
                    // 01 06 00 09 00 00 59 C8
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x00;
                }

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }


            public void setVolt(int addr, int volt)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x00, 0x04, 0xB0, 0x8A, 0xBE };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(volt / 256);
                sdata[5] = (byte)(volt % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);

            }

            public void setCurrent(int addr, int current)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x01, 0x02, 0xBC, 0xD8, 0xDB };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(current / 256);
                sdata[5] = (byte)(current % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }

            public string SelfTest(int addr)
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0x01, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x33 };

                    sCMD[0] = (byte)addr;
                    ushort crc = Constant.CRC16_0_b(sCMD, 7);
                    sCMD[7] = (byte)(crc & 0xFF);
                    sCMD[8] = (byte)(crc >> 8);
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    return ReturnValue();
                }
                catch
                {
                    return "-1";
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;
                byte[] temp = new byte[100];

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    int cnt = spCom.BytesToRead;

                    spCom.ReadTimeout = 1000;

                    // sRet += spCom.ReadExisting();
                    len = spCom.Read(temp, 0, cnt);

                    if (temp[0] != 0x00)
                    {
                        sRet = temp[0].ToString();
                    }
                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }
        public class DPS5005_7
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public DPS5005_7(string sPort)
            {
                iLimitSecond = 20.0f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            private void SendData(byte[] sCMD, int len)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(sCMD, 0, len);

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }


            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            public void setOnOff(int addr, bool onoff)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x09, 0x00, 0x01, 0x98, 0x08 };
                if (onoff == true)
                {
                    // 01 06 00 09 00 01 98 08
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x01;
                }
                else
                {
                    // 01 06 00 09 00 00 59 C8
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x00;
                }

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }


            public void setVolt(int addr, int volt)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x00, 0x04, 0xB0, 0x8A, 0xBE };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(volt / 256);
                sdata[5] = (byte)(volt % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);

            }

            public void setCurrent(int addr, int current)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x01, 0x02, 0xBC, 0xD8, 0xDB };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(current / 256);
                sdata[5] = (byte)(current % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }

            public string SelfTest(int addr)
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0x01, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x33 };

                    sCMD[0] = (byte)addr;
                    ushort crc = Constant.CRC16_0_b(sCMD, 7);
                    sCMD[7] = (byte)(crc & 0xFF);
                    sCMD[8] = (byte)(crc >> 8);
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    return ReturnValue();
                }
                catch
                {
                    return "-1";
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;
                byte[] temp = new byte[100];

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    int cnt = spCom.BytesToRead;

                    spCom.ReadTimeout = 1000;

                    // sRet += spCom.ReadExisting();
                    len = spCom.Read(temp, 0, cnt);

                    if (temp[0] != 0x00)
                    {
                        sRet = temp[0].ToString();
                    }
                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }
        public class DPS5005_8
        {
            private float iLimitSecond = 5000;
            private string PortName;
            private SerialPort spCom;

            public DPS5005_8(string sPort)
            {
                iLimitSecond = 20.0f;

                PortName = sPort;

                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
            }

            private void SendData(byte[] sCMD, int len)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    spCom.Write(sCMD, 0, len);

                    Thread.Sleep(100);
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    PortClose();
                }
            }


            public void PortClose()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            public void setOnOff(int addr, bool onoff)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x09, 0x00, 0x01, 0x98, 0x08 };
                if (onoff == true)
                {
                    // 01 06 00 09 00 01 98 08
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x01;
                }
                else
                {
                    // 01 06 00 09 00 00 59 C8
                    sdata[0] = (byte)addr;
                    sdata[5] = 0x00;
                }

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }


            public void setVolt(int addr, int volt)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x00, 0x04, 0xB0, 0x8A, 0xBE };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(volt / 256);
                sdata[5] = (byte)(volt % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);

            }

            public void setCurrent(int addr, int current)
            {
                byte[] sdata = { 0x01, 0x06, 0x00, 0x01, 0x02, 0xBC, 0xD8, 0xDB };

                sdata[0] = (byte)addr;
                sdata[4] = (byte)(current / 256);
                sdata[5] = (byte)(current % 256);

                ushort crc = Constant.CRC16_0_b(sdata, 6);
                sdata[6] = (byte)(crc & 0xFF);
                sdata[7] = (byte)(crc >> 8);

                SendData(sdata, sdata.Length);
            }

            public string SelfTest(int addr)
            {
                // test
                // return "1";

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();

                    }
                    byte[] sCMD = { 0x01, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x33 };

                    sCMD[0] = (byte)addr;
                    ushort crc = Constant.CRC16_0_b(sCMD, 7);
                    sCMD[7] = (byte)(crc & 0xFF);
                    sCMD[8] = (byte)(crc >> 8);
                    //byte[] sCMD = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xCB};
                    //byte[] sCMD = { 0x01, 0x06, 0x00, 0x00, 0x03, 0xE8, 0x89, 0x74 };

                    ////////////////////////////////////////////
                    // 보내기
                    ////////////////////////////////////////////

                    spCom.Write(sCMD, 0, sCMD.Length);
                    Thread.Sleep(500);

                    return ReturnValue();
                }
                catch
                {
                    return "-1";
                }
            }

            private string ReturnValue()
            {
                string sRet = "";
                int len = -1;
                byte[] temp = new byte[100];

                DateTime dtStart = new DateTime(DateTime.Now.Ticks);

                do
                {
                    Application.DoEvents();

                    int cnt = spCom.BytesToRead;

                    spCom.ReadTimeout = 1000;

                    // sRet += spCom.ReadExisting();
                    len = spCom.Read(temp, 0, cnt);

                    if (temp[0] != 0x00)
                    {
                        sRet = temp[0].ToString();
                    }
                    //len = sRet.IndexOf(Constant.CR);

                    TimeSpan ts = DateTime.Now.Subtract(dtStart);
                    if (ts.TotalSeconds > iLimitSecond)
                    {
                        sRet = iLimitSecond.ToString() + " Seconds Timeout";
                        break;
                    }

                } while (len == -1);

                spCom.Close();

                return sRet;
            }
        }
        // 절연저항 절연내압기
        //SE 7430
        public class SE7430
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;

            public SE7430(string sPort)
            {

                // iLimitSecond = 0.3f;
                // iLimitSecond = 0.3f;
                iLimitSecond = 1.0f;

                PortName = sPort;
                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 9600;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                //spCom.Handshake = Handshake.None;
                spCom.DataBits = 8;

            }
            public void Open()
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }
            }
            /// <summary>
            /// 초단위의 시간지연 메소드
            /// </summary>
            /// <param name="iSecond">초</param>
            public void Delay(int iSecond)
            {
                for (int i = 1; i <= iSecond; i++)
                {
                    Thread.Sleep(1000);
                }
            }

            public string SelfTest()
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                }
                catch (Exception err)
                {
                    return "";
                }

                spCom.Write("*IDN?" + Constant.CRLF);

                return ReturnValue();
            }
            public bool Set_ACW()
            {
                bool bRes = false;

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                    Thread.Sleep(100);
                    spCom.Write("FL 1" + Constant.CRLF);

                    Thread.Sleep(100);
                    bRes = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return bRes;

            }

            public string Getmeasure_ACW(int fSec)
            {
                string sRes = "";
                try
                {
                    SendData("FL 1");
                    SendData("SS 1");
                    SendData("TEST" + Constant.CRLF);

                    for (int i = 0; i <= fSec; i++)
                    {
                        Application.DoEvents();
                        Thread.Sleep(500);
                    }

                    spCom.DiscardInBuffer();

                    SendData("RD 1?" + Constant.CRLF);
                    Thread.Sleep(50);

                    sRes = ReturnValue();

                    SendData("RESET" + Constant.CRLF);
                    Thread.Sleep(50);

                    string[] sResAry = sRes.Split(new char[] { ',' });

                    if (sResAry.Length < 4)
                        return "-1";
                    else
                        return sResAry[4].Trim();

                }
                catch (Exception ex)
                {
                    spCom.Close();
                    sRes = ex.Message;

                }
                return sRes;
            }

            public bool Set_IR()
            {
                bool bRes = false;

                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                    Thread.Sleep(100);
                    spCom.Write("FL 2" + Constant.CRLF);

                    Thread.Sleep(100);
                    bRes = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return bRes;

            }

            public string Getmeasure_IR(int fSec)
            {
                string sRes = "";
                try
                {

                    SendData("FL 2" + Constant.CRLF);
                    SendData("SS 1" + Constant.CRLF);
                    SendData("TEST" + Constant.CRLF);

                    for (int i = 0; i <= fSec; i++)
                    {
                        Application.DoEvents();
                        Thread.Sleep(1000);
                    }

                    spCom.DiscardInBuffer();

                    Thread.Sleep(1000);
                    SendData("RD 1?" + Constant.CRLF);
                    Thread.Sleep(50);

                    sRes = ReturnValue();

                    SendData("RESET" + Constant.CRLF);
                    Thread.Sleep(50);

                    string[] sResAry = sRes.Split(new char[] { ',' });

                    if (sResAry.Length < 4)
                        return "-1";
                    else
                        return sResAry[4].Trim();

                }
                catch (Exception ex)
                {
                    spCom.Close();
                    sRes = ex.Message;

                }
                return sRes;
            }

            public void SetACW(float fVolt, float fSec)
            {
                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("SAA" + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("EV " + fVolt.ToString() + Constant.CRLF);
                Thread.Sleep(100);
                spCom.Write("EDW " + fSec.ToString() + Constant.CRLF);

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

            }

            public string GetMeasure(float fSec)
            {
                string sRes;

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                spCom.Write("SM 0" + Constant.CRLF);

                Delay((int)fSec);

                SendData("?K");
                sRes = ReturnValue();

                if (!spCom.IsOpen)
                {
                    spCom.Open();
                }

                //spCom.Write("FB" + Constant.CRLF);
                // spCom.Write("FR" + Constant.CRLF);

                if (spCom.IsOpen)
                {
                    spCom.Close();
                }

                return sRes;
            }

            public void Close()
            {
                if (spCom.IsOpen)
                    spCom.Close();
            }

            private void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }

                    string sFlag = sCMD;
                    spCom.Write(sFlag + Constant.LF);

                    Thread.Sleep(1000);  // Command Execution Time Limit(250)
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
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


                spCom.Close();

                return sRet;
            }

        }
        public class FY8300_1
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;
            public FY8300_1(string sPort)
            {
                iLimitSecond = 1.0f;
                PortName = sPort;
                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 115200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.DataBits = 8;
            }
            public string SelfTest()
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                }
                catch (Exception err)
                {
                    return "";
                }
                spCom.Write("UID" + Constant.LF);
                return ReturnValue();
            }
            public void Close()
            {
                if (spCom.IsOpen)
                {
                    spCom.Close();
                }
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
            public void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                    string sFlag = sCMD;
                    spCom.Write(sFlag + Constant.LF);
                    Thread.Sleep(250);  // Command Execution Time Limit(250)                
                }
                catch
                {

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
                spCom.Close();
                return sRet;
            }
        }
        public class FY8300_2
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;
            public FY8300_2(string sPort)
            {
                iLimitSecond = 1.0f;
                PortName = sPort;
                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 115200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.DataBits = 8;
            }
            public string SelfTest()
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                }
                catch (Exception err)
                {
                    return "";
                }
                spCom.Write("UID" + Constant.LF);
                return ReturnValue();
            }
            public void Close()
            {
                if (spCom.IsOpen)
                {
                    spCom.Close();
                }
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
            public void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                    string sFlag = sCMD;
                    spCom.Write(sFlag + Constant.LF);
                    Thread.Sleep(250);  // Command Execution Time Limit(250)
                }
                catch
                {

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
                spCom.Close();
                return sRet;
            }
        }
        public class FY8300_3
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;
            public FY8300_3(string sPort)
            {
                iLimitSecond = 1.0f;
                PortName = sPort;
                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 115200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.DataBits = 8;
            }
            public string SelfTest()
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                }
                catch (Exception err)
                {
                    return "";
                }
                spCom.Write("UID" + Constant.LF);
                return ReturnValue();
            }
            public void Close()
            {
                if (spCom.IsOpen)
                {
                    spCom.Close();
                }
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
            public void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                    string sFlag = sCMD;
                    spCom.Write(sFlag + Constant.LF);
                    Thread.Sleep(250);  // Command Execution Time Limit(250)
                }
                catch
                {

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
                spCom.Close();
                return sRet;
            }
        }

        public class FY8300_4
        {
            private string PortName;
            private SerialPort spCom;
            private float iLimitSecond;
            public FY8300_4(string sPort)
            {
                iLimitSecond = 1.0f;
                PortName = sPort;
                spCom = new SerialPort();
                spCom.PortName = PortName;
                spCom.BaudRate = 115200;
                spCom.StopBits = StopBits.One;
                spCom.Parity = System.IO.Ports.Parity.None;
                spCom.DataBits = 8;
            }
            public string SelfTest()
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                }
                catch (Exception err)
                {
                    return "";
                }
                spCom.Write("UID" + Constant.LF);
                return ReturnValue();
            }
            public void Close()
            {
                if (spCom.IsOpen)
                {
                    spCom.Close();
                }
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
            public void SendData(string sCMD)
            {
                try
                {
                    if (!spCom.IsOpen)
                    {
                        spCom.Open();
                    }
                    string sFlag = sCMD;
                    spCom.Write(sFlag + Constant.LF);
                    Thread.Sleep(250);  // Command Execution Time Limit(250)
                }
                catch
                {

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
                spCom.Close();
                return sRet;
            }
        }
    }
}
