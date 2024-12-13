using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;

namespace Library
{

    public class PLCLib
    {
        #region Constructor

        public PLCLib(string sPortName)
        {
            try
            {
                spCOM = new SerialPort();
                spCOM.BaudRate = 115200;
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

                spCOM.DataReceived += new SerialDataReceivedEventHandler(_spCOM_DataReceived);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public PLCLib(string sPortName, int iBaudRate, int iDataBits, StopBits stopBits, Parity parity)
        {
            try
            {
                spCOM = new SerialPort();
                spCOM.BaudRate = iBaudRate; ;
                spCOM.DataBits = iDataBits;
                spCOM.StopBits = stopBits;
                spCOM.Parity = parity;
                spCOM.ReadTimeout = 500;

                if (sPortName == "")
                {
                    throw new Exception("현재 시스템에 사용 가능한 포트가 존재하지 않습니다.");
                }
                else
                {
                    spCOM.PortName = sPortName;
                }

                //spCOM.DataReceived += new SerialDataReceivedEventHandler(_spCOM_DataReceived);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Event Handler Member
        void _spCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
        }
        #endregion


        #region Properties Member
        public bool IsOpen
        {
            get
            {
                return spCOM.IsOpen;
            }
        }

        /// <summary>
        /// 자동/수동여부 확인
        /// </summary>
        public bool IsAuto
        {
            get
            {
                string sRes = ReadBit("0");

                return (Convert.ToInt32(sRes, 16) == 1);
            }
        }

        /// <summary>
        /// PLC 상태확인 
        /// </summary>
        public bool IsStatus
        {
            get
            {
                string sRes = this.ReadWord("10");

                return (Convert.ToInt32(sRes, 16) == 1);
            }
        }

        #endregion

        #region Method Member
        public bool Auto()
        {

            string sRes = ReadBit("0");
            if (sRes != "")
                return (Convert.ToInt32(sRes, 16) == 1);
            else
            {
                return false;
            }

        }

        public bool AutoWord()
        {

            string sRes = ReadWord("0");
            if (sRes != "")
                return (Convert.ToInt32(sRes, 16) == 1);
            else
            {
                return false;
            }
        }

        public bool AutoWT()
        {

            string sRes = ReadWord("0");
            if (sRes != "")
                return (Convert.ToInt32(sRes, 16) == 4);
            else
            {
                return false;
            }
        }

        public bool AutoWC()
        {

            string sRes = ReadWord("0");
            if (sRes != "")
                return (Convert.ToInt32(sRes, 16) == 2);
            else
            {
                return false;
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
                //MessageBox.Show(ex.Message);
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

        /// <summary>
        /// PLC 초기화
        /// </summary>
        public void Initialize()
        {
            if (!IsOpen)
                Open();

            WriteWord("0", "0");
            spCOM.DiscardInBuffer();

            if (IsOpen)
                Close();
        }

        /// <summary>
        /// 단독 1비트 읽기
        /// </summary>
        /// <param name="sAddr">읽어 올 주소(번지)</param>
        public string ReadBit(string sAddr)
        {
            string sRes = "";

            try
            {
                //if (!IsOpen)
                //    Open();

                SendData("MX", "R", "S", sAddr, "1");
          
                sRes = ReturnStatus();

                int iLen = sRes.IndexOf("RSS") + 7;
                if (sRes.Length > 10)
                    sRes = sRes.Substring(iLen, 2);

                if (IsOpen)
                    Close();

            }
            catch
            {
                if (spCOM.IsOpen)
                    spCOM.Close();
            }
            return sRes;
        }

        public string ReadIX(string sAddr)
        {
            string sRes = "";

            try
            {
                //if (!IsOpen)
                //    Open();

                SendData("IX", "R", "S", sAddr, "1");
                Thread.Sleep(80);
                sRes = ReturnStatus();

                int iLen = sRes.IndexOf("RSS") + 7;
                if (sRes.Length > 10)
                    sRes = sRes.Substring(iLen, 2);

                if (IsOpen)
                    Close();

            }
            catch
            {
                if (spCOM.IsOpen)
                    spCOM.Close();
            }
            return sRes;
        }
        /// <summary>
        /// 단독 1비트 쓰기
        /// </summary>
        /// <param name="sAddr">쓸 주소(번지)</param>
        public void WriteBit(string sAddr, string sData)
        {
            try
            {
                SendData("MX", "W", "S", sAddr, sData);
                SendData("MX", "W", "S", sAddr, sData);
                spCOM.DiscardInBuffer();
            }
            catch
            {

            }
        }
        public void WriteQX(string sAddr, string sData)
        {
            try
            {
                SendData("QX", "W", "S", sAddr, sData);
                spCOM.DiscardInBuffer();
            }
            catch
            {

            }
        }
        ///// <summary>
        ///// 연속 비트 읽기
        ///// </summary>
        ///// <param name="sAddr">읽어 올 시작주소(번지)</param>
        ///// <param name="iCount">읽을 주소(번지) 개수</param>
        //public void ReadBits(string sAddr, int iCount)
        //{
        //    SendDatas("MX", "R", sAddr, "", iCount);
        //}

        ///// <summary>
        ///// 연속 비트 쓰기
        ///// </summary>
        ///// <param name="sAddr">쓸 주소(번지)</param>
        //public void WriteBits(string sAddr, string sData, int iCount)
        //{
        //    SendDatas("MX", "W", sAddr, sData, iCount);
        //}


        /// <summary>
        /// 단독 1바이트 읽기
        /// </summary>
        /// <param name="sAddr">읽어 올 주소(번지)</param>
        public string ReadByte(string sAddr)
        {
            SendData("MB", "R", "S", sAddr, "1");
            Thread.Sleep(80);
            string sRes = ReturnStatus();

            int iLen = sRes.IndexOf("RSS") + 7;

            if (sRes.Length > 10)
                sRes = sRes.Substring(iLen, 2);

            if (IsOpen)
                Close();

            return sRes;
        }

        /// <summary>
        /// 단독 1바이트 쓰기
        /// </summary>
        /// <param name="sAddr">쓸 주소(번지)</param>
        public void WriteByte(string sAddr, string sData)
        {
            try
            {
                SendData("MB", "W", "S", sAddr, sData);

                spCOM.DiscardInBuffer();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 연속 바이트 읽기
        /// </summary>
        /// <param name="sAddr">읽어 올 시작주소(번지)</param>
        /// <param name="iCount">읽을 주소(번지) 개수</param>
        public string ReadBytes(string sAddr, int iCount)
        {
            SendDatas("MB", "R", sAddr, "", iCount);

            Thread.Sleep(80);
            string sRes = ReturnStatus();

            if (IsOpen)
                Close();

            return sRes;
        }

        /// <summary>
        /// 연속 바이트 쓰기
        /// </summary>
        /// <param name="sAddr">쓸 주소(번지)</param>
        public void WriteBytes(string sAddr, string sData, int iCount)
        {
            SendDatas("MB", "W", sAddr, sData, iCount);
        }


        /// <summary>
        /// 단독 1워드 읽기
        /// </summary>
        /// <param name="sAddr">읽어 올 주소(번지)</param>
        public string ReadWord(string sAddr)
        {
            if (!IsOpen)
                Open();

            SendData("MW", "R", "S", sAddr, "1");
            Thread.Sleep(80);
            string sRes = ReturnStatus();

            int iLen = sRes.IndexOf("RSS") + 7;
            if (sRes.Length > 10)
                sRes = sRes.Substring(iLen, 4);

            if (IsOpen)
                Close();

            return sRes;
        }

        /// <summary>
        /// 단독 1워드 쓰기
        /// </summary>
        /// <param name="sAddr">쓸 주소(번지)</param>
        public void WriteWord(string sAddr, string sData)
        {
            SendData("MW", "W", "S", sAddr, sData);
            SendData("MW", "W", "S", sAddr, sData);
        }

        /// <summary>
        /// 연속 워드 읽기
        /// </summary>
        /// <param name="sAddr">읽어 올 시작주소(번지)</param>
        /// <param name="iCount">읽을 주소(번지) 개수</param>
        public void ReadWords(string sAddr, int iCount)
        {
            SendDatas("MW", "R", sAddr, "", iCount);
        }

        /// <summary>
        /// 연속 워드 쓰기
        /// </summary>
        /// <param name="sAddr">쓸 주소(번지)</param>
        public void WriteWords(string sAddr, string sData, int iCount)
        {
            SendDatas("MW", "W", sAddr, sData, iCount);
        }

        /// <summary>
        /// 단독 1더블워드 읽기
        /// </summary>
        /// <param name="sAddr">읽어 올 주소(번지)</param>
        public void ReadDouble(string sAddr)
        {
            SendData("MD", "R", "S", sAddr, "1");
        }

        /// <summary>
        /// 단독 1더블워드 쓰기
        /// </summary>
        /// <param name="sAddr">쓸 주소(번지)</param>
        public void WriteDouble(string sAddr, string sData)
        {
            SendData("MD", "W", "S", sAddr, sData);
        }

        /// <summary>
        /// 연속 더블워드 읽기
        /// </summary>
        /// <param name="sAddr">읽어 올 시작주소(번지)</param>
        /// <param name="iCount">읽을 주소(번지) 개수</param>
        public void ReadDoubles(string sAddr, int iCount)
        {
            SendDatas("MD", "R", sAddr, "", iCount);
        }

        /// <summary>
        /// 연속 더블워드 쓰기
        /// </summary>
        /// <param name="sAddr">쓸 주소(번지)</param>
        public void WriteDoubles(string sAddr, string sData, int iCount)
        {
            SendDatas("MD", "W", sAddr, sData, iCount);
        }

        /// <summary>
        /// 단일 데이터 읽기/쓰기 메소드
        /// </summary>
        /// <param name="sDeviceType">장치명</param>
        /// <param name="sMode">읽기/쓰기 구분</param>
        /// <param name="sContinue">단독/연속 구분(무조건 "R"세팅)</param>
        /// <param name="sAddr">읽거나 쓸 주소(번지)</param>
        /// <param name="sData">쓸 자료</param>
        private void SendData(string sDeviceType, string sMode, string sContinue, string sAddr, string sData)
        {
            try
            {
                if (!spCOM.IsOpen)
                    spCOM.Open();

                int iLen = 0;

                //비트 또는 바이트일 경우에는 2로 설정(1byte)
                //워드일 경우에는 4(2bytes)로, 더블일 경우에는 8(4bytes)로 설정
                if (sDeviceType.IndexOf("X") > 0 || sDeviceType.IndexOf("B") > 0)
                {
                    iLen = 2;
                }
                else if (sDeviceType.IndexOf("W") > 0)
                {
                    iLen = 4;
                }
                else if (sDeviceType.IndexOf("D") > 0)
                {
                    iLen = 8;
                }

                // 단독 읽기/쓰기
                string sTemp = "%" + sDeviceType + sAddr.PadLeft(3, '0');
                string sBuff = Constant.ENQ.ToString()
                                    + "00"        // 국번
                                    + sMode     // 명령어(읽기:R, 쓰기:W)
                                    + "S" + sContinue    // 명령어 타입(단독:S, 연속:B)
                                    + "01"        // 블록수
                                    + sTemp.Length.ToString("X2").PadLeft(2, '0')  // 디바이스 지정 길이
                                    + sTemp;       // 디바이스 지정 - 데이터(명령어 및 주소)
                if (sMode == "W")
                    sBuff += Convert.ToString(int.Parse(sData), 16).PadLeft(iLen, '0');  // 읽을 때 참조할 번지 수 및 쓸 때 설정할 값

                //sBuff += Constant.EOT.ToString();
                //byte[] bts = System.Text.Encoding.ASCII.GetBytes(sBuff);

                //for (int i = 0; i < bts.Length; i++)
                //{
                //    spCOM.Write(bts, i, 1);
                //}
                //Thread.Sleep(80);


                sBuff += Constant.EOT.ToString();
                spCOM.Write(sBuff);

          

                Thread.Sleep(100);
            }
            catch
            {
                if (spCOM.IsOpen)
                    spCOM.Close();
            }
        }

        /// <summary>
        /// 연속 데이터 읽기/쓰기 메소드
        /// </summary>
        /// <param name="sDeviceType">장치명</param>
        /// <param name="sMode">읽기/쓰기 구분</param>
        /// <param name="sAddr">읽거나 쓸 주소(번지)</param>
        /// <param name="sData">쓸 자료</param>
        /// <param name="iCount">연속 읽기/쓰기 할 개수</param>
        private void SendDatas(string sDeviceType, string sMode, string sAddr, string sData, int iCount)
        {
            if (!spCOM.IsOpen)
                spCOM.Open();

            int iLen = 0;

            //비트 또는 바이트일 경우에는 2로 설정(1byte)
            //워드일 경우에는 4(2bytes)로, 더블일 경우에는 8(4bytes)로 설정
            if (sDeviceType.IndexOf("X") > 0 || sDeviceType.IndexOf("B") > 0)
            {
                iLen = 2;
            }
            else if (sDeviceType.IndexOf("W") > 0)
            {
                iLen = 4;
            }
            else if (sDeviceType.IndexOf("D") > 0)
            {
                iLen = 8;
            }

            string sTemp = "";
            string sBuff = "";

            if (sMode == "R")
            {
                //연속읽기
                sTemp = "%" + sDeviceType + sAddr.PadLeft(3, '0');
                sBuff = Constant.ENQ.ToString()
                                    + "00"        // 국번
                                    + "R"     // 명령어(읽기:R, 쓰기:W)
                                    + "SB"     // 명령어 타입(단독:S, 연속:B)                                    
                                    + sTemp.Length.ToString("X2")  // 디바이스 지정 길이                                    
                                    + sTemp       // 디바이스 지정 - 데이터(명령어 및 주소)         
                                    + iCount.ToString("X2"); //  쓸 데이터의 개수
                //+ Constant.EOT.ToString();
            }
            else
            {
                //연속쓰기(데이터의 개수와 데이터의 길이가 비례(개수 * 길이)해야한다. 그렇지 않으면 NAK 신호발생
                sTemp = "%" + sDeviceType + sAddr.PadLeft(3, '0');
                sBuff = Constant.ENQ.ToString()
                                    + "00"        // 국번
                                    + "W"     // 명령어(읽기:R, 쓰기:W)
                                    + "SB"     // 명령어 타입(단독:S, 연속:B)                                    
                                    + sTemp.Length.ToString("X2")  // 디바이스 지정 길이                                    
                                    + sTemp       // 디바이스 지정 - 데이터(명령어 및 주소)         
                                    + iCount.ToString("X2")  //  쓸 데이터의 개수
                                    + sData.PadLeft(iLen, '0');  // 읽을 때 참조할 번지 수 및 쓸 때 설정할 값
                //+ Constant.EOT.ToString();
            }

            sBuff += Constant.EOT.ToString();
            byte[] bts = System.Text.Encoding.UTF8.GetBytes(sBuff);

            for (int i = 0; i < bts.Length; i++)
            {
                spCOM.Write(bts, i, 1);
            }
            do
            {

                //데이타를 전부 PLC로 전송 하기 위함..

            } while (spCOM.WriteBufferSize == 0);

            //spCOM.Write(Constant.EOT.ToString());

            //for (int i = 0; i < sBuff.Length; i++)
            //{
            //    spCOM.Write(sBuff.ToCharArray(), i, 1);
            //}
            //spCOM.Write(Constant.EOT.ToString());

            //spCOM.Write(sBuff);
            //Thread.Sleep(100);
        }

        public string ReturnStatus()
        {
            string sRet = "";
            int len = -1;

            DateTime dtStart = new DateTime(DateTime.Now.Ticks);

            do
            {
                Application.DoEvents();

                sRet += spCOM.ReadExisting();

                len = sRet.IndexOf(Constant.ETX);

                TimeSpan ts = DateTime.Now.Subtract(dtStart);
                if (ts.TotalSeconds > (0.05f))
                {
                    throw new Exception((spCOM.ReadTimeout / 1000).ToString() + " Seconds Timeout");
                }

            } while (len == -1);

            if (sRet != "")
            {
                if (sRet[0] == Constant.ACK)
                {
                    return sRet.Substring(1);
                }
                else if (sRet[0] == Constant.NAK)
                {
                    return sRet.Substring(1);
                }
                else
                {
                    return "-1"; //sRet;
                }
            }
            else
            {
                return "-1"; // "알수없는 오류[값없음]";
            }
        }


        #endregion

        #region Field Member
        public SerialPort spCOM;
        #endregion
    }
    
}
