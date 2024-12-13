using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
//using System.Windows.Forms.DataVisualization.Charting;

namespace Library
{

    public struct Constant
    {
        public static readonly char NULL = (char)0x00;   // null
        public static readonly char STX = (char)0x02;   // End of Text
        public static readonly char ETX = (char)0x03; //Convert.ToChar(03);   // End of Text
        public static readonly char EOT = (char)0x04; //Convert.ToChar(04);   // End of Transmission
        public static readonly char ENQ = (char)0x05; //Convert.ToChar(05);   // Enquiry
        public static readonly char ACK = (char)0x06; // Convert.ToChar(06);   // Acknowledge
        public static readonly char NAK = (char)0x15; //Convert.ToChar(21);   // Not Acknowledge
        public static readonly char EOF = (char)0x0D; //end of frame

        public static readonly string PLC_WORD = "MW";
        public static readonly string PLC_BIT = "MX";
        public static readonly int PLC_OK = 100;
        public static readonly int PLC_ERROR = 200;

        public static readonly string LF = "\n"; //Convert.ToInt16("10", 16),   // End of Text
        public static readonly string CR = "\r"; //Convert.ToInt16("13", 16),   // End of Text
        public static readonly string CRLF = "\r\n"; //Convert.ToInt16("13", 16) + Convert.ToInt16("10", 16)   // End of Text

        public static readonly string GOOD = "OK";
        public static readonly string NG = "NG";
        public static readonly string PASS = "Pass";
        public static readonly string READY = "Ready";
        public static readonly string ERROR = "Error";

        public static readonly int Timeout = 5;
        public static readonly float AirCalc = 10.197158f;
        public static readonly float PressCalc = 61.182946f;


        public static readonly int TGuideCount = 42;
        public static readonly int TestCount = 1000;

        public static readonly string KorailBroadcastDB = "StandardDB";

        /// <summary>
        /// Time Counter
        /// </summary>
        public static string TimeCountCmd(string sAddr, string sMode, string sUnit, string sValue)
        {
            //                  0x02                00~99   R/W        0000      U/M     0x03   0x0A0D
            string sCMD = STX.ToString() + sAddr + sMode + sValue + sUnit + ETX.ToString() + CRLF.ToString();

            return sCMD;
        }

        public static string TimeCountReset(string sAddr, string sUnit)
        {
            //                  0x02                00~99   R/W   0000                            U/M     0x03   0x0A0D
            string sCMD = STX.ToString() + sAddr + "R" + "RST" + NULL.ToString() + sUnit + ETX.ToString() + CRLF.ToString();
            return sCMD;
        }

        public static string TimeCountStart(string sAddr, string sUnit)
        {
            //                  0x02                00~99   R/W   0000                            U/M     0x03   0x0A0D
            string sCMD = STX.ToString() + sAddr + "R" + "STR" + NULL.ToString() + sUnit + ETX.ToString() + CRLF.ToString();

            return sCMD;
        }

        public static string TimeCountStop(string sAddr, string sUnit)
        {
            //                  0x02                00~99   R/W   0000                            U/M     0x03   0x0A0D
            string sCMD = STX.ToString() + sAddr + "R" + "STO" + NULL.ToString() + sUnit + ETX.ToString() + CRLF.ToString();

            return sCMD;
        } 

        public static string Cmd(string sMode, string sCH, string sValue)
        {        
            //                  0x02                R/W       0~3    000000   0x03   0x0A0D
            string sCMD = STX.ToString() + sMode + sCH + sValue + ETX + CRLF;
            
            return sCMD;
        }

        /// <summary>
        /// CRC
        /// </summary>
        public static readonly ushort[] crc16Table = new ushort[256];
        public static readonly uint[] crc32Table = new uint[256];

        public static readonly ushort POLYNOMIAL_CRC16_CCITT = 0x1021;   //XModem
        public static readonly ushort POLYNOMIAL_CRC16 = 0x8005;
        public static readonly ushort POLYNORMIAL = 0xA001; // Modbus

        public static readonly uint POLYNOMIAL_CRC32 = 0xEDB88320; //0x04C11DB7


        public static string RequestRead(byte btNo)
        {
            //               국번                           명령                          시작번지(상위             하위)                         데이터개수(상위                 하위)
            string sBuff = ((char)btNo).ToString() + ((char)0x04).ToString() + ((char)0x00).ToString() + ((char)0x00).ToString() + ((char)0x00).ToString() + ((char)0x04).ToString();
            ushort crc = CRC16_0(sBuff, sBuff.Length);
            ushort crcUp = (ushort)(crc >> 8 & 0x00FF);
            ushort crcDown = (ushort)(crc >> 0 & 0x00FF);

            string sCMD = sBuff + ((char)crcDown).ToString() + ((char)crcUp).ToString();

            return sCMD;
        } // MT4W Unit Read

        public static string RequestRead2(byte btNo)
        {
            //               국번                           명령                          시작번지(상위             하위)                         데이터개수(상위                 하위)
            string sBuff = ((char)btNo).ToString() + ((char)0x04).ToString() + ((char)0x00).ToString() + ((char)0x00).ToString() + ((char)0x00).ToString() + ((char)0x04).ToString();
            ushort crc = CRC16_0(sBuff, sBuff.Length);
            ushort crcUp = (ushort)(crc >> 8 & 0x00FF);
            ushort crcDown = (ushort)(crc >> 0 & 0x00FF);

            string sCMD = sBuff + ((char)crcDown).ToString() + ((char)crcUp).ToString();

            return sBuff;
        }
        public static ushort CRC16(string puchMsg, int usDataLen)
        {
            int i;
            ushort crc, flag;

            crc = 0xFFFF;

            for (int j = 0; j < usDataLen; j++)
            {
                crc ^= puchMsg[j];
                for (i = 0; i < 8; i++)
                {
                    flag = (ushort)(crc & 0x0001);
                    crc >>= 1;
                    if (flag != 0)
                        crc ^= POLYNORMIAL;
                }
            }

            return crc;
        }

        public static ushort CRC16_0(string nData, int wLength)
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
            ushort wCRCWord = 0xFFFF;
            for (int i = 0; i < wLength; i++)
            {
                nTemp = (byte)(((byte)nData[i]) ^ wCRCWord);
                wCRCWord >>= 8;
                wCRCWord ^= wCRCTable[nTemp];
            }

            return wCRCWord;
        }
        public static ushort CRC16_0_b(byte[] nData, int wLength)
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
            ushort wCRCWord = 0xFFFF;
            for (int i = 0; i < wLength; i++)
            {
                nTemp = (byte)(((byte)nData[i]) ^ wCRCWord);
                wCRCWord >>= 8;
                wCRCWord ^= wCRCTable[nTemp];
            }

            return wCRCWord;
        }


        public static ushort[] Create_CRC_Table(ushort POLYNOMIAL)
        {
            ushort crc_accum;

            for (ushort i = 0; i < 256; i++)
            {
                crc_accum = (ushort)(i << 8);

                for (ushort j = 0; j < 8; j++)
                {
                    if ((crc_accum & 0x8000) != 0)
                        crc_accum = (ushort)((crc_accum << 1) ^ POLYNOMIAL);
                    else
                        crc_accum = (ushort)(crc_accum << 1);
                }

                crc16Table[i] = crc_accum;
            }

            return crc16Table;
        }

        public static ushort CRC16_CCITT(string sBuff)
        {
            ushort i;
            ushort crc_accum = 0x0000;   // Seed
            //ushort crc_accum = 0xFFFF;
            //ushort crc_accum = 0x1D0F;

            for (ushort j = 0; j < sBuff.Length; j++)
            {
                i = (ushort)((crc_accum >> 8 ^ (byte)sBuff[j]) & 0x00FF);

                crc_accum = (ushort)((crc_accum << 8) ^ crc16Table[i]);
            }

            return crc_accum;

        }

        public static uint[] Create_CRC32_Table(uint POLYNOMIAL)
        {
            uint crc_accum;

            for (int i = 0; i < crc32Table.Length; i++)
            {
                crc_accum = (uint)i;

                for (int j = 8; j > 0; j--)
                {
                    if ((crc_accum & 1) == 1)
                        crc_accum = (uint)((crc_accum >> 1) ^ POLYNOMIAL);
                    else
                        crc_accum >>= 1;
                }
                crc32Table[i] = crc_accum;
            }

            return crc32Table;
        }

        public static uint CRC32(string sBuff)
        {
            uint crc_accum = 0xFFFFFFFF;

            for (int i = 0; i < sBuff.Length; i++)
            {
                byte bt = (byte)(((crc_accum) & 0xFF) ^ (byte)sBuff[i]);
                crc_accum = (uint)((crc_accum >> 8) ^ crc32Table[bt]);
            }

            return ~crc_accum;
        }

        public static ushort CRC16_ENSBoard(byte[] btSend, int length)
        {
            ushort crc16 = 0x0000;

            for (int j = 0; j < length; j++)
            {
                crc16 = CRC16_Update(crc16, btSend[j]);
            }

            return crc16;
        }

        private static ushort CRC16_Update(ushort crc16, byte bt)
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

        /// <summary>
        /// Notation Convert
        /// </summary>

        public static string DecToHex(int iDec)
        {
            string sHex = "";

            sHex = iDec.ToString("X");

            return sHex;
        }

        public static int HexToDec(string sHex)
        {
            int iDec = 0;

            iDec = Convert.ToInt32(sHex, 16);

            return iDec;
        }

        public static string HexToBin(string sHex)
        {
            string sBin = "";

            sHex = sHex.ToUpper();
            for (int i = 0; i < sHex.Length; i++)
            {
                switch (sHex[i])
                {
                    case '0': sBin += "0000"; break;
                    case '1': sBin += "0001"; break;
                    case '2': sBin += "0010"; break;
                    case '3': sBin += "0011"; break;
                    case '4': sBin += "0100"; break;
                    case '5': sBin += "0101"; break;
                    case '6': sBin += "0110"; break;
                    case '7': sBin += "0111"; break;
                    case '8': sBin += "1000"; break;
                    case '9': sBin += "1001"; break;
                    case 'A': sBin += "1010"; break;
                    case 'B': sBin += "1011"; break;
                    case 'C': sBin += "1100"; break;
                    case 'D': sBin += "1101"; break;
                    case 'E': sBin += "1110"; break;
                    case 'F': sBin += "1111"; break;
                }
            }

            return sBin;
        }

        public static string BinToHex(string sBin)
        {
            string sHex = "";

            int iDec = Convert.ToInt32(sBin, 2);

            sHex = DecToHex(iDec);

            return sHex;
        }

        public static string DecToBin(int iDec)
        {
            string sBin = "";

            string sHex = iDec.ToString("X");

            sBin = HexToBin(sHex);

            return sBin;
        }

        public static int BinToDec(string sBin)
        {
            int iDec = 0;

            iDec = Convert.ToInt32(sBin, 2);

            return iDec;
        }
    }


    #region Service Class
    [Serializable]


    public class CServiceTestData
    {
        public long lngIDX;
        public DateTime dtTestDate;
        public string sTesterName;
        public string sPounsungNo;
        public string sCarNo;
        public string sSerialNo;
        public string sETC;
        public string sJudgement;
        
        public long lngDeviceSetting;    //환경설정 및 시험기준
        public long lngGuidanceIDX;
        public string[] sTestName1;
        public string[] sTestName2;
        public string[] sTestName3;
        public string[] sTestName4;
        public string[] sTestName5;
        public string[] sTestName6;
        public string[] sTestMethod;
        public string[] sTestData;
        public string[] sTestJudge;

        public CServiceTestData()
        {
            sTestName1 = new string[Constant.TestCount];
            sTestName2 = new string[Constant.TestCount];
            sTestName3 = new string[Constant.TestCount];
            sTestName4 = new string[Constant.TestCount];
            sTestName5 = new string[Constant.TestCount];
            sTestName6 = new string[Constant.TestCount];
            sTestMethod = new string[Constant.TestCount];
            sTestData = new string[Constant.TestCount];
            sTestJudge = new string[Constant.TestCount];
        }
    }


    public class CServiceEnvironment
    {

        public string sPort1;
        public string sPort2;
        public string sPort3;
        public string sPort4;
        public string sPort5;
        public string sPort6;
        public string sPort7;
        public string sPort8;
        public string sPort9;
        public string sPort10;


        public CServiceEnvironment()
        {

        }
    }

    public class CServiceGuidance
    {
        public long lngIDX;

        public string[] sGuide = new string[100];
        public float[] fOffset = new float[100];

      

        public CServiceGuidance()
        {

        }
    }

    public class Device
    {
        public long lngIDX;

        public string[] sDevice = new string[100];
   



        public Device()
        {
            
        }
    }

    public class Name
    {
        public long lngIDX;

        public string[] sName = new string[300];




        public Name()
        {

        }
    }
    #endregion
}
