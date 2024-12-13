using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO.Ports;
using System.IO;


namespace Library
{
    public class Serial
    {
        public static System.IO.Ports.SerialPort spCOM;

       
        public static void serial_setting()
        {
            foreach (string sPort in System.IO.Ports.SerialPort.GetPortNames())
            {
                spCOM = new System.IO.Ports.SerialPort();
                //spCOM.PortName = sPort;
                spCOM.PortName = "COM18";
                spCOM.BaudRate = 38400;
                spCOM.DataBits = 8;
                spCOM.Parity = Parity.None;
                Thread.Sleep(1000);
               
            }
        }
        

    }














}














