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

namespace Power_Modbus_RTU_SAMPLE
{

    public class ds2824
    {
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        string IP_Adress;
        public ds2824(string sPort)
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
                    IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 17123);
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
        public byte SelfTest()
        {
            SendData("0x30");
            byte sRes = ReturnValue();
            return sRes;
        }

        public void writeData(byte[] data, int num)
        {


            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = IPAddress.Parse(IP_Adress);

            IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 17123);
            client.Connect(serverEndPoint);


            //NetworkStream serverStream = clientSocket.GetStream();
            try
            {
                client.Send(data);
                client.Close();

                //serverStream.Write(data, 0, num);
            }
            catch (Exception e)
            {
                //timer1.Stop();
                MessageBox.Show("Write time out");
                System.Windows.Forms.Application.Exit();
            }
        }


        public void relay_control(int num, bool onoff)
        {


            byte[] stream = new byte[3];

            stream[0] = 0x31;
            stream[1] = (byte)num;

            if (onoff == false)
            {
                stream[2] = 0;

            }
            else
            {
                stream[2] = 1;

            }
          

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = IPAddress.Parse(IP_Adress);

            IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 17123);
            client.Connect(serverEndPoint);


            //NetworkStream serverStream = clientSocket.GetStream();
            try
            {
                client.Send(stream);
                client.Close();

                //serverStream.Write(data, 0, num);
            }
            catch (Exception e)
            {
                //timer1.Stop();
                MessageBox.Show("Write time out");
                System.Windows.Forms.Application.Exit();
            }
        }



        public void SendData(string sCMD)
        {
            try
            {
                if (client.Connected)
                {
                    // byte[] bytesBuff = Encoding.Default.GetBytes(sCMD + "\r\n");
                    byte[] bytesBuff = new byte[1];
                    bytesBuff[0] = (byte)0x30;
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


        private byte ReturnValue()
        {
            byte[] receipveBUffer = new byte[1024];
            int byteBytesRecvd = client.Receive(receipveBUffer);
            byte[] receipve = new byte[byteBytesRecvd];
            Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);
            byte receiveStr = receipve[0];
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

}