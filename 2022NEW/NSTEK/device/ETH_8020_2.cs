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

    public class ETH_8020_2
    {
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        TcpClient client_socket;
        string IP_Adress;
        public ETH_8020_2(string sPort)
        {
            IP_Adress = sPort;

            client_socket = new TcpClient(IP_Adress, 17495);

        }
        public void Portclose()
        {
            try
            {
                if (client_socket.Connected == true)
                {
                    client_socket.Close();
                }
            }
            catch
            {

            }
        }
        public void Open()
        {
            try
            {


                if (client.Connected == false)
                {
                    IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                    IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 17495);
                    client.Connect(serverEndPoint);
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
                    client_socket = new TcpClient(IP_Adress, 17495);
                }
                //if (client.Connected == false)
                //{
                //    IPAddress ipAddr = IPAddress.Parse(IP_Adress);
                //    IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 2268);
                //    client.Connect(serverEndPoint);
                //}
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
            byte[] bytesBuff = new byte[1];
            bytesBuff[0] = (byte)0x10;
            writeData(bytesBuff, 1);
            byte sRes = ReturnValue();
            return sRes;
        }

        public void writeData(byte[] data, int num)
        {
            //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPAddress ipAddr = IPAddress.Parse(IP_Adress);

            //IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 17494);
            //client.Connect(serverEndPoint);


            NetworkStream serverStream = client_socket.GetStream();

            try
            {
                //client.Send(data);
                //client.Close();


                serverStream.Write(data, 0, num);




            }
            catch (Exception e)
            {
                //timer1.Stop();
                MessageBox.Show("Write time out");
                System.Windows.Forms.Application.Exit();
            }
        }
        public void relay_control(int relay_num, bool onoff)
        {
            //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPAddress ipAddr = IPAddress.Parse(IP_Adress);

            //IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 17494);
            //client.Connect(serverEndPoint);


            NetworkStream serverStream = client_socket.GetStream();

            try
            {
                byte[] SerBuf = new byte[70];

                if (onoff == false) //close
                {
                    SerBuf = Encoding.ASCII.GetBytes(":DOI," + relay_num.ToString() + "," + 0.ToString() + "," + "password");

                }
                else // open
                {
                    SerBuf = Encoding.ASCII.GetBytes(":DOA," + relay_num.ToString() + "," + 0.ToString() + "," + "password");
                }


                //client.Send(data);
                //client.Close();


                serverStream.Write(SerBuf, 0, SerBuf.Length);

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
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddr = IPAddress.Parse(IP_Adress);

                IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 17495);
                client.Connect(serverEndPoint);


                if (client.Connected)
                {
                    byte[] bytesBuff = new byte[1];
                    bytesBuff[0] = (byte)0x10;
                    client.Send(bytesBuff);

                }
                else
                {
                    ipAddr = IPAddress.Parse(IP_Adress);
                    serverEndPoint = new IPEndPoint(ipAddr, 17495);
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
            NetworkStream serverStream = client_socket.GetStream();

            byte[] bytesBuff = new byte[4098];
            byte[] bf = new byte[3];

            serverStream.Read(bytesBuff, 0, bytesBuff.Length);

            bf[0] = bytesBuff[0];


            //byte[] receipveBUffer = new byte[1024];
            //int byteBytesRecvd = client.Receive(receipveBUffer);
            //byte[] receipve = new byte[byteBytesRecvd];
            //Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);
            //byte receiveStr = receipve[0];
            return bf[0];
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