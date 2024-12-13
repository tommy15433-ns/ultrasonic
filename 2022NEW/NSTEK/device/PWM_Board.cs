using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.device
{
    // PWM_Board
    public class PWM_Board
    {
        private float iLimitSecond = 5000;
        private string PortName;
        private SerialPort spCom;

        public PWM_Board(string sPort)
        {
            iLimitSecond = 0.3f;

            PortName = sPort;

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

        public string SelfTest(int ID)
        {
            string pan = "NG";

            if (!spCom.IsOpen)
            {
                spCom.Open();
            }

            byte[] btREAD = new byte[5];

            btREAD[0] = 0xF2;
            btREAD[1] = (byte) ID;
            btREAD[2] = 0x52;
            btREAD[3] = 0xF3;
            //btREAD[4] = 0x40;
            //btREAD[5] = 0x03;


            byte btBCC = 0x00;

            for (int i = 0; i < 4; i++)
            {
                btBCC ^= btREAD[i];
            }
            btREAD[4] = btBCC;

            spCom.Write(btREAD, 0, 5);

            Thread.Sleep(200);

            pan = SelfTest_ReturnData(ID);

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
        private string SelfTest_ReturnData(int ID)
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
                    if (ndata == 0xF2) //시작
                    {
                        //bcc = ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else ReadCount = 0;
                }
                else if (ReadCount == 1)
                {
                    if (ndata == ID) //시작
                    {
                        //bcc = ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else ReadCount = 0;
                }
                else if (ReadCount == 2)
                {
                    if (ndata == 0x52) //시작
                    {
                        //bcc = ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else ReadCount = 0;
                }
                else if (ReadCount >= 3 && ReadCount < 7)
                {
                    //bcc ^= ndata;
                    btData[ReadCount] = ndata;
                    ReadCount++;


                }
                else if (ReadCount == 7)
                {

                    if (ndata == 0xF3) //시작
                    {
                        //bcc = ndata;
                        btData[ReadCount] = ndata;
                        ReadCount++;
                    }
                    else ReadCount = 0;

                }
                else if (ReadCount == 8)
                { 
                    byte btBCC = 0x00;

                    for (int i = 0; i < 8; i++)
                    {
                        btBCC = (byte)(btBCC ^ btData[i]);

                    }

                    if (ndata == btBCC) //끝
                    {
                        pan = "OK";
                        //bcc ^= ndata;
                        btData[ReadCount] = ndata;

                        btRes = new byte[9];
                        for (int x = 0; x < 9; x++)
                            btRes[x] = btData[x];

                    }

                    ReadCount = 0;

                    // 버퍼삭제,, 추가
                    RevCount_WPt = 0;
                    RevCount_RPt = 0;
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
        public void Write_TACHO(int ID,int ch1, int ch2, int ch3, int ch4) // ENCORDER 주파수 고장 듀티 변환
        {
            if (!spCom.IsOpen)
            {
                spCom.Open();
            }
            byte[] btREAD = new byte[9];
            byte btBCC = 0x00;

            btREAD[0] = 0xF2;
            btREAD[1] = (byte)ID;
            btREAD[2] = 0x47; // 명령 CMD
            btREAD[3] = (byte)ch1;  //ch1 duty  
            btREAD[4] = (byte)ch2;  //ch2 duty
            btREAD[5] = (byte)ch3;                 // ch3 duty
            btREAD[6] = (byte)ch4;                //ch4 duty
            btREAD[7] = 0xF3;
            for (int i = 0; i < 8; i++)
            {
                btBCC = (byte)(btBCC ^ btREAD[i]);

            }

            btREAD[8] = btBCC;

            Thread.Sleep(200);

            spCom.Write(btREAD, 0, 9);

        }
        public void Write_PWM(int ID,int freq) //주파수 각 
        {
            if (!spCom.IsOpen)
            {
                spCom.Open();
            }

            byte[] btREAD = new byte[9];
            byte btBCC = 0x00;
            //int SIH, SIL;
            int MSB = (freq / 256);
            int LSB = (freq % 256);

            //SIH = (degree / 256);
            //SIL = (degree % 256);

            btREAD[0] = 0xF2;
            btREAD[1] = (byte)ID;
            btREAD[2] = 0x53;
            btREAD[3] = Convert.ToByte(MSB);
            btREAD[4] = Convert.ToByte(LSB);
            btREAD[5] = 0x2C;
            btREAD[6] = 0x5A; // 90도고정
            btREAD[7] = 0xF3;
            //btREAD[6] = 0xF3;

            for (int i = 0; i < 8; i++)
            {
                btBCC = (byte)(btBCC ^ btREAD[i]);

            }

            btREAD[8] = btBCC;

            Thread.Sleep(200);

            spCom.Write(btREAD, 0, 9);

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
