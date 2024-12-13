using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.device
{
    // SVH300R
    public class SHV300R
    {
        private float iLimitSecond = 5000;
        private string PortName;
        private SerialPort spCom;

        public SHV300R(string sPort)
        {
            iLimitSecond = 0.3f;

            PortName = sPort;

            spCom = new SerialPort();
            spCom.PortName = PortName;
            spCom.BaudRate = 19200;
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

            byte[] btREAD = new byte[6];

            btREAD[0] = 0x02;
            btREAD[1] = 0x40;
            btREAD[2] = 0x41;
            btREAD[3] = 0x40;
            btREAD[4] = 0x40;
            btREAD[5] = 0x03;

            /*
            byte btBCC = 0x00;

            for (int i = 0; i < 8; i++)
            {
                btBCC ^= btREAD[i];
            }
            btREAD[7] = btBCC;
        */
            spCom.Write(btREAD, 0, 6);

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
            byte[] btData = new byte[15];


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
                        bcc = ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else ReadCount = 0;
                }
                else if (ReadCount >= 1 && ReadCount < 14)
                {
                    //bcc ^= ndata;
                    btData[ReadCount] = ndata;
                    ReadCount++;

                }
                //else if (ReadCount == 2)
                //{
                //    bcc^=ndata;
                //    btData[ReadCount] = ndata;
                //    ReadCount++;

                //}
                //else if (ReadCount == 3)
                //{
                //    bcc ^= ndata;
                //    btData[ReadCount] = ndata;
                //    ReadCount++;
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

                else if (ReadCount == 14)
                {
                    if (ndata == 0x03) //끝
                    {
                        bcc = ndata;
                        btData[ReadCount] = ndata;

                        btRes = new byte[15];
                        for (int x = 0; x < 15; x++)
                            btRes[x] = btData[x];

                        //ReadCount++;

                        ReadCount = 0;

                        // 버퍼삭제,, 추가
                        RevCount_WPt = 0;
                        RevCount_RPt = 0;


                    }
                    else ReadCount = 0;

                }
                //else if (ReadCount == 7)
                //{                        
                //    if (ndata == bcc)
                //    {
                //        btData[ReadCount] = ndata;
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
            byte[] btData = new byte[15];


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
                        //bcc = ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else ReadCount = 0;
                }
                else if (ReadCount >= 1 && ReadCount < 14)
                {
                    //bcc ^= ndata;
                    btData[ReadCount] = ndata;
                    ReadCount++;


                }
                //else if (ReadCount == 2)
                //{
                //    //bcc ^= ndata;
                //    btData[ReadCount] = ndata;
                //    ReadCount++;

                //}
                //else if (ReadCount == 3)
                //{
                //    bcc ^= ndata;
                //    btData[ReadCount] = ndata;
                //    ReadCount++;
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

                else if (ReadCount == 14)
                {

                    if (ndata == 0x03) //끝
                    {
                        pan = "OK";
                        //bcc ^= ndata;
                        btData[ReadCount] = ndata;

                        btRes = new byte[15];
                        for (int x = 0; x < 15; x++)
                            btRes[x] = btData[x];

                    }

                    ReadCount = 0;

                    // 버퍼삭제,, 추가
                    RevCount_WPt = 0;
                    RevCount_RPt = 0;
                }
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
        public void Write_VOLTAGE(int duty)
        {
            if (!spCom.IsOpen)
            {
                spCom.Open();
            }
            byte[] btREAD = new byte[8];
            byte btBCC = 0x00;


            int SVH, SVL;
            int MSB = (duty / 64);
            int LSB = (duty % 64);

            SVH = MSB | 64;
            SVL = LSB | 64;

            btREAD[0] = 0x02;
            btREAD[1] = 0x40;
            btREAD[2] = 0x42;
            btREAD[3] = Convert.ToByte(SVH);
            btREAD[4] = Convert.ToByte(SVL);
            btREAD[5] = 0x03;
            //btREAD[6] = 0xF3;

            //for (int i = 0; i < 8; i++)
            //{
            //    btBCC = (byte)(btBCC ^ btREAD[i]);

            //}

            //btREAD[7] = btBCC;

            Thread.Sleep(200);

            spCom.Write(btREAD, 0, 6);

        }
        public void Write_Current(int duty)
        {
            if (!spCom.IsOpen)
            {
                spCom.Open();
            }
            byte[] btREAD = new byte[8];
            byte btBCC = 0x00;


            int SIH, SIL;
            int MSB = (duty / 64);
            int LSB = (duty % 64);

            SIH = MSB | 64;
            SIL = LSB | 64;

            btREAD[0] = 0x02;
            btREAD[1] = 0x40;
            btREAD[2] = 0x43;
            btREAD[3] = Convert.ToByte(SIH);
            btREAD[4] = Convert.ToByte(SIL);
            btREAD[5] = 0x03;
            //btREAD[6] = 0xF3;

            //for (int i = 0; i < 8; i++)
            //{
            //    btBCC = (byte)(btBCC ^ btREAD[i]);

            //}

            //btREAD[7] = btBCC;

            Thread.Sleep(200);

            spCom.Write(btREAD, 0, 6);

        }

        public void POWER_ONOFF(string onoff)
        {
            byte onoff_value = 0x45;

            if (!spCom.IsOpen)
            {
                spCom.Open();
            }
            byte[] btREAD = new byte[8];
            //byte btBCC = 0x00;

            if (onoff == "ON")
            {
                onoff_value = 0x44;


            }
            if (onoff == "OFF")
            {
                onoff_value = 0x45;


            }

            btREAD[0] = 0x02;
            btREAD[1] = 0x40;
            btREAD[2] = onoff_value;
            btREAD[3] = 0x40;
            btREAD[4] = 0x40;
            btREAD[5] = 0x03;
            //btREAD[6] = 0xF3;

            //for (int i = 0; i < 8; i++)
            //{
            //    btBCC = (byte)(btBCC ^ btREAD[i]);

            //}

            //btREAD[7] = btBCC;

            Thread.Sleep(200);

            spCom.Write(btREAD, 0, 6);

        }


        public string READ_voltage() // 1이 전압 2가 전류 
        {
            string duty = "";

            if (!spCom.IsOpen)
            {
                spCom.Open();
            }
            byte[] btREAD = new byte[15];
            byte[] reData = new byte[15];
            byte btBCC = 0x00;

            btREAD[0] = 0x02;
            btREAD[1] = 0x40;
            btREAD[2] = 0x41;
            btREAD[3] = 0x40;
            btREAD[4] = 0x40;
            btREAD[5] = 0x03;

            //for (int i = 0; i < 8; i++)
            //{
            //    btBCC = (byte)(btBCC ^ btREAD[i]);

            //}

            //btREAD[7] = btBCC;

            Thread.Sleep(200);

            spCom.Write(btREAD, 0, 6);

            Thread.Sleep(200);

            reData = ReturnData();

            int convert_voltage = ((Convert.ToInt32(reData[1]) & 63) * 64) + (Convert.ToInt32(reData[2]) & 63);

            //string value = reData[datanumber].ToString("X2") + reData[datanumber + 1].ToString("X2");

            //float real_value = Convert.ToInt32(value, 16);

            duty = (convert_voltage * 60000 / 1000).ToString("F0");

            return duty;
        }

        public string READ_current() // 1이 전압 2가 전류 
        {
            string duty = "";

            if (!spCom.IsOpen)
            {
                spCom.Open();
            }
            byte[] btREAD = new byte[15];
            byte[] reData = new byte[15];
            byte btBCC = 0x00;

            btREAD[0] = 0x02;
            btREAD[1] = 0x40;
            btREAD[2] = 0x41;
            btREAD[3] = 0x40;
            btREAD[4] = 0x40;
            btREAD[5] = 0x03;

            //for (int i = 0; i < 8; i++)
            //{
            //    btBCC = (byte)(btBCC ^ btREAD[i]);

            //}

            //btREAD[7] = btBCC;

            Thread.Sleep(200);

            spCom.Write(btREAD, 0, 6);

            Thread.Sleep(200);

            reData = ReturnData();

            int convert_voltage = ((Convert.ToInt32(reData[3]) & 63) * 64) + (Convert.ToInt32(reData[4]) & 63);

            //string value = reData[datanumber].ToString("X2") + reData[datanumber + 1].ToString("X2");

            //float real_value = Convert.ToInt32(value, 16);

            duty = (convert_voltage * 5.0f).ToString("F0");

            return duty;
        }


    }
}
