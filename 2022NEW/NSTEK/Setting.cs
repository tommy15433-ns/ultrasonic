namespace Library
{
    using System.Windows.Forms;

    public class Setting
    {
        public static readonly string Name = "초음파탐상기";
        public static readonly string Report_Route = @"Report";
        public static readonly string Report_File = @"시험성적서Form.xlsx";
        public static readonly string Report_Intergration_File = @"시험성적서Form_통합.xlsx";
        public static readonly string FTP_File = Application.StartupPath + "\\" + "Standard";
        public static readonly string PW = "0000";
        public static readonly string Standard_folder = Application.StartupPath + "\\" + "Standard";
        public static readonly string JPG = @"JPG.jpg";
        public static readonly string dsp_ip = "192.168.80.90";
        public static readonly string plc_ip = "192.168.1.2";
        public static readonly string ds2824_ip = "192.168.0.123";


        public static readonly string[] Equipment = new string[] // 통신
        {
            "focusPX",
          "plc",
          //"timer",


        };

        public static readonly string[] Test_Equipment = new string[] // 대상체종류
        {
             "상용 제동실린더(CK)_1호선",
            "주차 제동실린더(CKP)_1호선",
            "상용 제동실린더(CK)_2호선",
            "주차 제동실린더(CKP)_2호선",
        };
        public static string[] ListName = new string[] // pau
        {
              "10 ~ 14초",
               "4 ~ 6초",
              
        };

        public static string[] ListName0 = new string[] // pau
       {
            "1750 ~ 1780",
            "265 ~ 270",
       };
        public static string[] ListName1 = new string[] // cob
       {
           "GDU1",
           "GDU2",
           "GDV1",
           "GDV2",
           "GDW1",
           "GDW2",
       };
        public static string[] ListName2 = new string[] // sob
        {
         "주파수특성시험",
          "종합감도시험",
          "정격출력 측정",
          "왜율측정",
          "신호대 잡음비측정", // 기준 치 확인 필요
        };
        public static string[] ListName3 = new string[] //비상
        {
         "Up 98N less",
         "Low 44N more",
         "Within 20N"
        };
        public static string[] ListName4 = new string[]//전원측정확인
        {
            "주회로통전시험(무부하)",
            "경부하시험",

        };
        public static string[] ListName5 = new string[] // 표시기
        {
          "승객안내표시기",
          "열차번호표시기",
          "측면행선표시기",
          "정면행선표시기"
        };
        public static string[] ListName6 = new string[] // 모의
       {
         "",
         "",
         "",
         "",



       };
        public static string[] ListName7 = new string[] // 모의
     {
         "loadcell",
         "DI1_1",
         "DI1_2",
         "DI1_3",


     };
        public static string[] ListName8 = new string[] // pau
    {
            "P24V",
            "N24V",
    };

        /////////////시험기준////////////////////////////

        public static string[] ListMethod0 = new string[] // pau
        {
               "Measuring Rising Speed",
               "Measuring Low Speed",
               "DC 77V Less",
               "390kPa Or Less",
               "880 kPa is applied to air hose, 10 min 44kPa",
        };
        public static string[] ListMethod1 = new string[] // cob
        {

        };

        public static string[] ListMethod2 = new string[] // sob
        {
        };
        public static string[] ListMethod3 = new string[] // 비상
        {
         "98N less",
         "44N more",
         "With in 20N",
        };
        public static string[] ListMethod4 = new string[] // 출력
         {

        "100V 전압 무부하 시 출력 파형  71V± 10%",
        "100V 전압 경부하 시 출력 파형  71V± 10%",

         };
        public static string[] ListMethod5 = new string[] // 표시기
        {
          
        };
        public static string[] ListMethod6 = new string[] // 모의
        {
            
        };

        public static string[] ListMethod8 = new string[] // 모의
       {
               "24V±2.4",
               "-24V±2.4",
       };
    }
}
