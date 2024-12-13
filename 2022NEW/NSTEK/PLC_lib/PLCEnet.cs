using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Library
{
 
    public class PLCEnet
    {
        public Socket client;
        string Address = "192.168.1.2";

        public void WriteBit(string sAddr, string sData)
        {
            try
            {
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddr = IPAddress.Parse(Address);

                IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2004);
                client.Connect(serverEndPoint);

                if (client.Connected)
                {

                    byte[] addr = Encoding.Default.GetBytes("%MX" + sAddr);

                    byte DataLength = Convert.ToByte((addr.Length + 7).ToString(), 16);
                    byte NameLength = Convert.ToByte((addr.Length).ToString(), 16);
                    byte[] bytesBuff = new byte[33 + addr.Length];
                    ////////////헤더///////////////
                    byte[] header = Encoding.Default.GetBytes("LGIS-GLOFA");
                    for (int k = 0; k < header.Length; k++)
                    {
                        bytesBuff[k] = header[k];
                    }
                    bytesBuff[10] = 0x00;
                    bytesBuff[11] = 0x00;
                    bytesBuff[12] = 0x00;
                    bytesBuff[13] = 0x33;
                    bytesBuff[14] = 0x00;
                    bytesBuff[15] = 0x00;
                    bytesBuff[16] = DataLength;//데이터길이
                    bytesBuff[17] = 0x00;//데이터길이
                    bytesBuff[18] = 0x00;//예약
                    bytesBuff[19] = 0x09;//BCC
                    /////////////////////////////////


                    bytesBuff[20] = 0x58; //쓰기
                    bytesBuff[21] = 0x00; //쓰기

                    bytesBuff[22] = 0x00; //데이터타입
                    bytesBuff[23] = 0x00; //데이터타입

                    bytesBuff[24] = 0x00; //Dont care
                    bytesBuff[25] = 0x00; //Dont care

                    bytesBuff[26] = 0x01; //변수갯수
                    bytesBuff[27] = 0x00; //변수갯수

                    bytesBuff[28] = NameLength; //변수명길이
                    bytesBuff[29] = 0x00; //변수명길이
                    int i = 0;
                    for (i = 0; i < NameLength; i++)
                    {
                        bytesBuff[30 + i] = addr[i];

                    }



                    bytesBuff[30 + i] = 0x00; //0 
                    bytesBuff[31 + i] = 0x00;  //0
                    bytesBuff[32 + i] = Convert.ToByte(sData, 16); //1 //데이터


                    client.Send(bytesBuff);
                    client.Close();
                    Thread.Sleep(8);

                }


            }
            catch
            {

            }
        }

        public void WriteQX(string sAddr, string sData)
        {
            try
            {
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddr = IPAddress.Parse(Address);

                IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2004);
                client.Connect(serverEndPoint);

                if (client.Connected)
                {

                    byte[] addr = Encoding.Default.GetBytes("%QX" + sAddr);

                    byte DataLength = Convert.ToByte((addr.Length + 7).ToString(), 16);
                    byte NameLength = Convert.ToByte((addr.Length).ToString(), 16);
                    byte[] bytesBuff = new byte[33 + addr.Length];
                    ////////////헤더///////////////
                    byte[] header = Encoding.Default.GetBytes("LGIS-GLOFA");
                    for (int k = 0; k < header.Length; k++)
                    {
                        bytesBuff[k] = header[k];
                    }
                    bytesBuff[10] = 0x00;
                    bytesBuff[11] = 0x00;
                    bytesBuff[12] = 0x00;
                    bytesBuff[13] = 0x33;
                    bytesBuff[14] = 0x00;
                    bytesBuff[15] = 0x00;
                    bytesBuff[16] = DataLength;//데이터길이
                    bytesBuff[17] = 0x00;//데이터길이
                    bytesBuff[18] = 0x00;//예약
                    bytesBuff[19] = 0x09;//BCC
                    /////////////////////////////////


                    bytesBuff[20] = 0x58; //쓰기
                    bytesBuff[21] = 0x00; //쓰기

                    bytesBuff[22] = 0x00; //데이터타입
                    bytesBuff[23] = 0x00; //데이터타입

                    bytesBuff[24] = 0x00; //Dont care
                    bytesBuff[25] = 0x00; //Dont care

                    bytesBuff[26] = 0x01; //변수갯수
                    bytesBuff[27] = 0x00; //변수갯수

                    bytesBuff[28] = NameLength; //변수명길이
                    bytesBuff[29] = 0x00; //변수명길이
                    int i = 0;
                    for (i = 0; i < NameLength; i++)
                    {
                        bytesBuff[30 + i] = addr[i];

                    }



                    bytesBuff[30 + i] = 0x00; //0 
                    bytesBuff[31 + i] = 0x00;  //0
                    bytesBuff[32 + i] = Convert.ToByte(sData, 16); //1 //데이터


                    client.Send(bytesBuff);
                    client.Close();
                    Thread.Sleep(8);

                }


            }
            catch
            {

            }
        }

        public string ReadBit(string sAddr)
        {
            string receiveStr = "";


            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = IPAddress.Parse(Address);

            IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2004);
            client.Connect(serverEndPoint);

            if (client.Connected)
            {
                byte[] addr = Encoding.Default.GetBytes("%MX" + sAddr);

                byte DataLength = Convert.ToByte((addr.Length + 10).ToString(), 16);
                byte NameLength = Convert.ToByte((addr.Length).ToString(), 16);

                byte[] bytesBuff = new byte[36 + addr.Length];
                ////////////헤더///////////////
                byte[] header = Encoding.Default.GetBytes("LGIS-GLOFA");
                for (int k = 0; k < header.Length; k++)
                {
                    bytesBuff[k] = header[k];
                }
                bytesBuff[10] = 0x00;
                bytesBuff[11] = 0x00;
                bytesBuff[12] = 0x00;
                bytesBuff[13] = 0x33;
                bytesBuff[14] = 0x00;
                bytesBuff[15] = 0x00;
                bytesBuff[16] = DataLength;//데이터길이
                bytesBuff[17] = 0x00;//데이터길이
                bytesBuff[18] = 0x00;//예약
                bytesBuff[19] = 0x0D;//BCC
                /////////////////////////////////


                bytesBuff[20] = 0x54; //읽기
                bytesBuff[21] = 0x00; //쓰기

                bytesBuff[22] = 0x00; //데이터타입
                bytesBuff[23] = 0x00; //데이터타입

                bytesBuff[24] = 0x00; //Dont care
                bytesBuff[25] = 0x00; //Dont care

                bytesBuff[26] = 0x02; //변수갯수
                bytesBuff[27] = 0x00; //변수갯수

                bytesBuff[28] = NameLength;
                bytesBuff[29] = 0x00; //변수명길이
                int i = 0;
                for (i = 0; i < addr.Length; i++)
                {

                    bytesBuff[30 + i] = addr[i]; //변수명길이

                }
                bytesBuff[30 + i] = 0x04; //변수명길이
                bytesBuff[31 + i] = 0x00; //변수명길이
                bytesBuff[32 + i] = 0x25; //변수명길이
                bytesBuff[33 + i] = 0x4d; //변수명길이
                bytesBuff[34 + i] = 0x58; //변수명길이
                bytesBuff[35 + i] = 0x38; //변수명길이





                client.Send(bytesBuff);
                byte[] receipveBUffer = new byte[4096];

                int byteBytesRecvd = client.Receive(receipveBUffer);


                byte[] receipve = new byte[byteBytesRecvd];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);


                client.Close();
                receiveStr = "0" + receipve[32].ToString();
                Thread.Sleep(8);
            }




            return receiveStr;
        }

        public string ReadQX(string sAddr)
        {
            string receiveStr = "";


            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = IPAddress.Parse(Address);

            IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2004);
            client.Connect(serverEndPoint);

            if (client.Connected)
            {
                byte[] addr = Encoding.Default.GetBytes("%QX" + sAddr);

                byte DataLength = Convert.ToByte((addr.Length + 10).ToString(), 16);
                byte NameLength = Convert.ToByte((addr.Length).ToString(), 16);

                byte[] bytesBuff = new byte[36 + addr.Length];
                ////////////헤더///////////////
                byte[] header = Encoding.Default.GetBytes("LGIS-GLOFA");
                for (int k = 0; k < header.Length; k++)
                {
                    bytesBuff[k] = header[k];
                }
                bytesBuff[10] = 0x00;
                bytesBuff[11] = 0x00;
                bytesBuff[12] = 0x00;
                bytesBuff[13] = 0x33;
                bytesBuff[14] = 0x00;
                bytesBuff[15] = 0x00;
                bytesBuff[16] = DataLength;//데이터길이
                bytesBuff[17] = 0x00;//데이터길이
                bytesBuff[18] = 0x00;//예약
                bytesBuff[19] = 0x0D;//BCC
                /////////////////////////////////


                bytesBuff[20] = 0x54; //읽기
                bytesBuff[21] = 0x00; //쓰기

                bytesBuff[22] = 0x00; //데이터타입
                bytesBuff[23] = 0x00; //데이터타입

                bytesBuff[24] = 0x00; //Dont care
                bytesBuff[25] = 0x00; //Dont care

                bytesBuff[26] = 0x02; //변수갯수
                bytesBuff[27] = 0x00; //변수갯수

                bytesBuff[28] = NameLength;
                bytesBuff[29] = 0x00; //변수명길이
                int i = 0;
                for (i = 0; i < addr.Length; i++)
                {

                    bytesBuff[30 + i] = addr[i]; //변수명길이

                }
                bytesBuff[30 + i] = 0x04; //변수명길이
                bytesBuff[31 + i] = 0x00; //변수명길이
                bytesBuff[32 + i] = 0x25; //변수명길이
                bytesBuff[33 + i] = 0x4d; //변수명길이
                bytesBuff[34 + i] = 0x58; //변수명길이
                bytesBuff[35 + i] = 0x38; //변수명길이





                client.Send(bytesBuff);
                byte[] receipveBUffer = new byte[4096];

                int byteBytesRecvd = client.Receive(receipveBUffer);


                byte[] receipve = new byte[byteBytesRecvd];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);


                client.Close();
                receiveStr = "0" + receipve[32].ToString();
                Thread.Sleep(8);
            }




            return receiveStr;
        }

        public void WriteWord(string sAddr, string sData)
        {
            try
            {
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddr = IPAddress.Parse(Address);

                IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2004);
                client.Connect(serverEndPoint);

                if (client.Connected)
                {

                    byte[] addr = Encoding.Default.GetBytes("%MW" + sAddr);

                    byte DataLength = Convert.ToByte((addr.Length + 8).ToString(), 16);
                    byte NameLength = Convert.ToByte((addr.Length).ToString(), 16);
                    byte[] bytesBuff = new byte[34 + addr.Length];
                    ////////////헤더///////////////
                    byte[] header = Encoding.Default.GetBytes("LGIS-GLOFA");
                    for (int k = 0; k < header.Length; k++)
                    {
                        bytesBuff[k] = header[k];
                    }
                    bytesBuff[10] = 0x00;
                    bytesBuff[11] = 0x00;
                    bytesBuff[12] = 0x00;
                    bytesBuff[13] = 0x33;
                    bytesBuff[14] = 0x00;
                    bytesBuff[15] = 0x00;
                    bytesBuff[16] = DataLength;//데이터길이
                    bytesBuff[17] = 0x00;//데이터길이
                    bytesBuff[18] = 0x00;//예약
                    bytesBuff[19] = 0x09;//BCC
                    /////////////////////////////////


                    bytesBuff[20] = 0x58; //쓰기
                    bytesBuff[21] = 0x00; //쓰기

                    bytesBuff[22] = 0x02; //데이터타입
                    bytesBuff[23] = 0x00; //데이터타입

                    bytesBuff[24] = 0x00; //Dont care
                    bytesBuff[25] = 0x00; //Dont care

                    bytesBuff[26] = 0x01; //변수갯수
                    bytesBuff[27] = 0x00; //변수갯수

                    bytesBuff[28] = NameLength; //변수명길이
                    bytesBuff[29] = 0x00; //변수명길이
                    int i = 0;
                    for (i = 0; i < NameLength; i++)
                    {
                        bytesBuff[30 + i] = addr[i];

                    }



                    bytesBuff[30 + i] = 0x00; //0 
                    bytesBuff[31 + i] = 0x00; //0 
                    string kss = Convert.ToInt16(sData).ToString("X4");
                    bytesBuff[32 + i] = Convert.ToByte(kss.Substring(2, 2), 16); //1 //데이터
                    bytesBuff[33 + i] = Convert.ToByte(kss.Substring(0, 2), 16); //1 //데이터


                    client.Send(bytesBuff);
                    client.Close();
                    Thread.Sleep(8);
                }


            }
            catch
            {

            }
        }

        public string ReadWord(string sAddr)
        {
            string receiveStr = "";
            if (sAddr.Length < 3)
            {
                do
                {
                    sAddr = "0" + sAddr;

                } while (sAddr.Length < 3);
            }

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = IPAddress.Parse(Address);

            IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2004);
            client.Connect(serverEndPoint);

            if (client.Connected)
            {
                byte[] addr = Encoding.Default.GetBytes("%MW" + string.Format("{0:X4}", sAddr));

                byte DataLength = Convert.ToByte((addr.Length + 4).ToString(), 16);
                byte NameLength = Convert.ToByte((addr.Length).ToString(), 16);

                byte[] bytesBuff = new byte[30 + addr.Length];
                ////////////헤더///////////////
                byte[] header = Encoding.Default.GetBytes("LGIS-GLOFA");
                for (int k = 0; k < header.Length; k++)
                {
                    bytesBuff[k] = header[k];
                }
                bytesBuff[10] = 0x00;
                bytesBuff[11] = 0x00;
                bytesBuff[12] = 0x00;
                bytesBuff[13] = 0x33;
                bytesBuff[14] = 0x00;
                bytesBuff[15] = 0x00;
                bytesBuff[16] = DataLength;//데이터길이
                bytesBuff[17] = 0x00;//데이터길이
                bytesBuff[18] = 0x00;//예약
                bytesBuff[19] = 0x0D;//BCC
                /////////////////////////////////


                bytesBuff[20] = 0x54; //읽기
                bytesBuff[21] = 0x00; //쓰기

                bytesBuff[22] = 0x02; //데이터타입
                bytesBuff[23] = 0x00; //데이터타입

                bytesBuff[24] = 0x00; //Dont care
                bytesBuff[25] = 0x00; //Dont care

                bytesBuff[26] = 0x01; //변수갯수
                bytesBuff[27] = 0x00; //변수갯수

                bytesBuff[28] = NameLength;
                bytesBuff[29] = 0x00; //변수명길이
                int i = 0;
                for (i = 0; i < addr.Length; i++)
                {

                    bytesBuff[30 + i] = addr[i]; //변수명길이

                }






                client.Send(bytesBuff);
                byte[] receipveBUffer = new byte[4096];

                int byteBytesRecvd = client.Receive(receipveBUffer);


                byte[] receipve = new byte[byteBytesRecvd + 10];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);


                client.Close();

                //receiveStr = Convert.ToString(receipve[33], 16) + Convert.ToString(receipve[32], 16);
                string exception, exception1;

                exception = Convert.ToString(receipve[33], 16);
                exception1 = Convert.ToString(receipve[32], 16); ;

                if (exception.Length < 2)
                {
                    do
                    {
                        exception = "0" + exception;

                    } while (exception.Length < 2);
                }

                if (exception1.Length < 2)
                {
                    do
                    {
                        exception1 = "0" + exception1;

                    } while (exception1.Length < 2);
                }

                receiveStr = exception + exception1;

                if (receiveStr.Length < 4)
                {
                    do
                    {
                        receiveStr = "0" + receiveStr;

                    } while (receiveStr.Length < 4);
                }
                Thread.Sleep(8);
            }




            return receiveStr;
        }

        public void WriteDouble(string sAddr, string sData)
        {
            try
            {
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddr = IPAddress.Parse(Address);

                IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2004);
                client.Connect(serverEndPoint);

                if (client.Connected)
                {

                    byte[] addr = Encoding.Default.GetBytes("%MD" + sAddr);

                    byte DataLength = Convert.ToByte((addr.Length + 10).ToString(), 16);
                    byte NameLength = Convert.ToByte((addr.Length).ToString(), 16);
                    byte[] bytesBuff = new byte[36 + addr.Length];
                    ////////////헤더///////////////
                    byte[] header = Encoding.Default.GetBytes("LGIS-GLOFA");
                    for (int k = 0; k < header.Length; k++)
                    {
                        bytesBuff[k] = header[k];
                    }
                    bytesBuff[10] = 0x00;
                    bytesBuff[11] = 0x00;
                    bytesBuff[12] = 0x00;
                    bytesBuff[13] = 0x33;
                    bytesBuff[14] = 0x00;
                    bytesBuff[15] = 0x00;
                    bytesBuff[16] = DataLength;//데이터길이
                    bytesBuff[17] = 0x00;//데이터길이
                    bytesBuff[18] = 0x00;//예약
                    bytesBuff[19] = 0x09;//BCC
                    /////////////////////////////////


                    bytesBuff[20] = 0x58; //쓰기
                    bytesBuff[21] = 0x00; //쓰기

                    bytesBuff[22] = 0x03; //데이터타입
                    bytesBuff[23] = 0x00; //데이터타입

                    bytesBuff[24] = 0x00; //Dont care
                    bytesBuff[25] = 0x00; //Dont care

                    bytesBuff[26] = 0x01; //변수갯수
                    bytesBuff[27] = 0x00; //변수갯수

                    bytesBuff[28] = NameLength; //변수명길이
                    bytesBuff[29] = 0x00; //변수명길이
                    int i = 0;
                    for (i = 0; i < NameLength; i++)
                    {
                        bytesBuff[30 + i] = addr[i];

                    }



                    bytesBuff[30 + i] = 0x00; //0 
                    bytesBuff[31 + i] = 0x00; //0 
                    string kss = Convert.ToInt32(sData).ToString("X8");
                    bytesBuff[32 + i] = Convert.ToByte(kss.Substring(6, 2), 16); //1 //데이터
                    bytesBuff[33 + i] = Convert.ToByte(kss.Substring(4, 2), 16); //1 //데이터
                    bytesBuff[34 + i] = Convert.ToByte(kss.Substring(2, 2), 16); //1 //데이터
                    bytesBuff[35 + i] = Convert.ToByte(kss.Substring(0, 2), 16); //1 //데이터


                    client.Send(bytesBuff);
                    client.Close();
                    Thread.Sleep(8);
                }


            }
            catch
            {

            }
        }

        public string ReadDouble(string sAddr)
        {
            string receiveStr = "";
            if (sAddr.Length < 3)
            {
                do
                {
                    sAddr = "0" + sAddr;

                } while (sAddr.Length < 3);
            }

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = IPAddress.Parse(Address);

            IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2004);
            client.Connect(serverEndPoint);

            if (client.Connected)
            {
                byte[] addr = Encoding.Default.GetBytes("%MD" + string.Format("{0:X4}", sAddr));

                byte DataLength = Convert.ToByte((addr.Length + 4).ToString(), 16);
                byte NameLength = Convert.ToByte((addr.Length).ToString(), 16);

                byte[] bytesBuff = new byte[30 + addr.Length];
                ////////////헤더///////////////
                byte[] header = Encoding.Default.GetBytes("LGIS-GLOFA");
                for (int k = 0; k < header.Length; k++)
                {
                    bytesBuff[k] = header[k];
                }
                bytesBuff[10] = 0x00;
                bytesBuff[11] = 0x00;
                bytesBuff[12] = 0x00;
                bytesBuff[13] = 0x33;
                bytesBuff[14] = 0x00;
                bytesBuff[15] = 0x00;
                bytesBuff[16] = DataLength;//데이터길이
                bytesBuff[17] = 0x00;//데이터길이
                bytesBuff[18] = 0x00;//예약
                bytesBuff[19] = 0x0D;//BCC
                /////////////////////////////////


                bytesBuff[20] = 0x54; //읽기
                bytesBuff[21] = 0x00; //쓰기

                bytesBuff[22] = 0x03; //데이터타입
                bytesBuff[23] = 0x00; //데이터타입

                bytesBuff[24] = 0x00; //Dont care
                bytesBuff[25] = 0x00; //Dont care

                bytesBuff[26] = 0x01; //변수갯수
                bytesBuff[27] = 0x00; //변수갯수

                bytesBuff[28] = NameLength;
                bytesBuff[29] = 0x00; //변수명길이
                int i = 0;
                for (i = 0; i < addr.Length; i++)
                {

                    bytesBuff[30 + i] = addr[i]; //변수명길이

                }






                client.Send(bytesBuff);
                byte[] receipveBUffer = new byte[4096];

                int byteBytesRecvd = client.Receive(receipveBUffer);


                byte[] receipve = new byte[byteBytesRecvd];
                Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);


                client.Close();
                receiveStr = Convert.ToString(receipve[35], 16) + Convert.ToString(receipve[34], 16) + Convert.ToString(receipve[33], 16) + Convert.ToString(receipve[32], 16);
                if (receiveStr.Length < 8)
                {
                    do
                    {
                        receiveStr = "0" + receiveStr;

                    } while (receiveStr.Length < 8);
                }
                Thread.Sleep(8);
            }




            return receiveStr;
        }
    }
}
