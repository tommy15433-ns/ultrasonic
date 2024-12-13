using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using lucidio;
using Library;
using System.Linq.Expressions;

namespace Power_Modbus_RTU_SAMPLE
{

    public class Lucid_Control_DO
    { 
        public SerialPort spCOM;
        private float iLimitSecond;
        LucidControlDO8 lucid;
        ValueDI1 v;
        IoReturn ret;

        public Lucid_Control_DO(string sPort)
        {
            try
            {

                lucid = new LucidControlDO8(sPort);

                //spCOM = new SerialPort();
                //spCOM.BaudRate = 9600;
                //spCOM.DataBits = 8;
                //spCOM.StopBits = StopBits.One;
                //spCOM.Parity = Parity.None;
                //spCOM.ReadTimeout = 500;

                //if (sPort == "")
                //{
                //    throw new Exception("현재 시스템에 사용 가능한 포트가 존재하지 않습니다.");
                //}
                //else
                //{
                //    spCOM.PortName = sPort;
                //}

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
                if (lucid.IsOpened() == false)
                {
                    lucid.Open();
                }

                //if (!spCOM.IsOpen)
                //{
                //    spCOM.Open();
                //}
            }
            catch 
            {
            }
        }

        public void Close()
        {
            try
            {
                if (lucid.IsOpened() == true)
                {
                    lucid.Close();
                }

                //if (spCOM.IsOpen)
                //{
                //    spCOM.Close();
                //}
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

        public string SELFTEST()
        {
            IoReturn ret;
            string pandan = "";
            try
            { 

                ret = lucid.Identify(0);

                if (ret == IoReturn.IO_RETURN_OK)
                {
                    pandan = lucid.GetDeviceClassName();

                }
                else
                {
                    pandan = "NG";
                
                }
            }

            catch
            {
                pandan = "NG";
               
            }

            return pandan;
        }

        public void SETTIMING(double ch, bool onoff)
        { 

        }

            public void SETDO(double ch,bool onoff)
        {
            if (onoff)
            {
                v = new ValueDI1();
                v.SetValue(true);
                lucid.SetIo((byte)ch, v);

                //Sig_DO.SETIO(double.Parse(cb_name[2])-1, 1);
            }
            else
            {
                v = new ValueDI1();
                v.SetValue(false);
                lucid.SetIo((byte)ch, v);
                //Sig_DO.SETIO(double.Parse(cb_name[2])-1, 0);
            }

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
            byte[] btData = new byte[15];


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
                    if (ndata == 0x00) //시작
                    {
                        //bcc = ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else ReadCount = 0;
                }
                else if (ReadCount == 1)
                {
                    if (ndata == 0x01) //시작
                    {
                        //bcc = ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else ReadCount = 0;

                }
                else if (ReadCount == 2)
                {
                    if (ndata == 0x00 || ndata == 0x01) //시작
                    {
                        pan = "OK";
                        //bcc ^= ndata;
                        btData[ReadCount] = ndata;

                        btRes = new byte[4];
                        for (int x = 0; x < 3; x++)
                            btRes[x] = btData[x];
                    }

                    ReadCount = 0;

                    // 버퍼삭제,, 추가
                    RevCount_WPt = 0;
                    RevCount_RPt = 0;

                }
                //else if (ReadCount == 3)
                //{
                //    if (ndata == 0x00) //끝
                //    {
                //        pan = "OK";
                //        //bcc ^= ndata;
                //        btData[ReadCount] = ndata;

                //        btRes = new byte[4];
                //        for (int x = 0; x < 4; x++)
                //            btRes[x] = btData[x];

                //    }

                //    ReadCount = 0;

                //    // 버퍼삭제,, 추가
                //    RevCount_WPt = 0;
                //    RevCount_RPt = 0;
                //}
                //else if (ReadCount == 4)
                //{
                //    bcc ^= ndata;
                //    btData[ReadCount] = ndata;
                //    ReadCount++;
                //}
                //else if (ReadCount == 5)
                //{

                //    bcc ^= ndata;
                //    btData[ReadCount] = ndata;
                //    ReadCount++;
                //}

                //else if (ReadCount == 14)
                //{

                //    if (ndata == 0x03) //끝
                //    {
                //        pan = "OK";
                //        //bcc ^= ndata;
                //        btData[ReadCount] = ndata;

                //        btRes = new byte[15];
                //        for (int x = 0; x < 15; x++)
                //            btRes[x] = btData[x];

                //    }

                //    ReadCount = 0;

                //    // 버퍼삭제,, 추가
                //    RevCount_WPt = 0;
                //    RevCount_RPt = 0;
                //}
                //else if (ReadCount == 7)
                //{
                //    if (ndata == bcc)
                //    {
                //        pan = "OK";
                //        // 수신 ok
                //        btRes = new byte[8];
                //        for (int x = 0; x < 8; x++)
                //            btRes[x] = btData[x];
                //    }

                //    ReadCount = 0;

                //    // 버퍼삭제,, 추가
                //    RevCount_WPt = 0;
                //    RevCount_RPt = 0;
                //}

            }

            return pan;
        }


        private string ReturnValue()
        {
            string sRet = "";
            int len = -1;



            sRet += spCOM.ReadExisting();



            spCOM.Close();

            return sRet;
        }

        public void SETIO(double ch, double indata)
        {
            byte[] rev = new byte[13];
            bool pan = true;
            int temp1, temp2;

           


            try
            {
                byte[] data = new byte[6];

                if (!spCOM.IsOpen)
                {
                    spCOM.Open();
                }
                spCOM.DiscardInBuffer();
                spCOM.DiscardOutBuffer();
                data[0] = 0x40; //주소
                data[1] = (byte)ch; //펑션코드
                data[2] = 0x00; //주소상위
                data[3] = 0x01; //주소하위

                if (indata == 1)
                {
                    data[4] = 0x01;
                }
                else
                {
                    data[4] = 0x00;
                }

                 //데이터
                //data[5] = (byte)(value/256); // 데이터
                spCOM.Write(data, 0, data.Length);
                Thread.Sleep(50);
            }
            catch
            { 
            
            }
        }
        public string GETIO(double ch)
        {
            //byte[] rev = new byte[13];
            bool pan = true;
            int temp1, temp2;

            try
            {
                byte[] data = new byte[4];

                if (!spCOM.IsOpen)
                {
                    spCOM.Open();
                }
                spCOM.DiscardInBuffer();
                spCOM.DiscardOutBuffer();
                data[0] = 0x46; //주소
                data[1] = (byte)ch; //펑션코드
                data[2] = 0x00; //주소상위
                data[3] = 0x00; //주소하위
                spCOM.Write(data, 0, data.Length);
                Thread.Sleep(50);

                string panjung = SelfTest_ReturnData();
                Thread.Sleep(50);

                return panjung;
            }
            catch
            {
                return "";
            }
        }



    }
    
}