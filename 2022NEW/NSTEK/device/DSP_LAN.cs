using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.device
{
    // DSP_LAN
    public class DSP_LAN
    {
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        string IP_Adress;

        public DSP_LAN()
        {

        }
        public DSP_LAN(string sPort)
        {
            IP_Adress = sPort;
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
                IPAddress ipAddr = IPAddress.Parse(IP_Adress);

                IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 5025);

                client.Connect(serverEndPoint);
            }
        }

        public bool IsOpen()
        {
            if (client.Connected)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public string SelfTest()
        {
            SendData("SYSTem:REMote");
            SendData("*IDN?"); ;
            string sRes = ReturnValue();

            return sRes;
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

        public string Status_OUTPUT()
        {
            SendData("SYSTem:REMote");
            SendData("OUTPut?"); ;
            string sRes = ReturnValue();

            return sRes;
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
            byte[] receipveBUffer = new byte[4096];

            int byteBytesRecvd = client.Receive(receipveBUffer);


            byte[] receipve = new byte[byteBytesRecvd];
            Array.ConstrainedCopy(receipveBUffer, 0, receipve, 0, byteBytesRecvd);

            string receiveStr = Encoding.Default.GetString(receipve);

            return receiveStr;
        }
    }
}
