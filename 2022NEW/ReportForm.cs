using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Library;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace _2022_Test
{
    public partial class ReportForm : Form
    {
        Thread search_thrun;
        Stopwatch sw;
        Odt odt;
        string [] tb;
        string Report_Route;
        string NowName = "";
        int cnt = 4;
        bool run = false;
        int xtf = 0;
        int[] data_location = new int[] { 9, 13, 21, 28, 31, 39, 45 };

        static Excel.Application excelApp = null;
        static Excel.Workbook WorkBook = null; static Excel.Worksheet WorkSheet = null;

        Form form1;

        public ReportForm(Form form)
        {
            InitializeComponent();
            form1 = form;
        }
        private void Exit_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (WorkBook != null)
                {
                    WorkBook.Close();
                    WorkBook = null;
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    excelApp = null;
                }

                ReleaseExcelObject(WorkSheet);
                ReleaseExcelObject(WorkBook);
                ReleaseExcelObject(excelApp);
                KillProcessByName("Excel");
            }
            catch
            {

            }

            form1.Opacity = 1;
            this.Close();
        }
        private void Delete_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (datagridview1.SelectedRows.Count > 0)
                {

                    if (MessageBox.Show("삭제하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string FileName = "";
                        for (int i = 0; i < datagridview1.SelectedRows.Count; i++)
                        {
                            FileName = DateTime.Parse(datagridview1.SelectedRows[i].Cells[0].Value.ToString()).ToString("yyyyMMddHHmmss") + "^";

                            for (int j = 1; j < datagridview1.ColumnCount; j++)
                            {
                                FileName = FileName + datagridview1.SelectedRows[i].Cells[j].Value.ToString() + "^";
                            }
                            FileName = FileName + ".xlsx";

                            NowName = Report_Route + "\\" + FileName;
                            if (File.Exists(NowName))
                            {
                                File.Delete(NowName);
                            }
                        }
                        Search_BTN_MouseUp(null, null);
                        MessageBox.Show("삭제되었습니다.");
                    }
                }
                else
                {
                    MessageBox.Show("1개 선택해 주세요.");
                }
            }
            catch
            {
                MessageBox.Show("삭제에 실패하였습니다. 다시 시도해 주세요.");
            }
        }
        private void search_thread(object arg)
        {
            CheckForIllegalCrossThreadCalls = false;

            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "reportpath.ini");
                string[] fd = sr.ReadToEnd().Split('!');
                sr.Close();
                Report_Route = fd[0];
                CultureInfo provider = CultureInfo.InvariantCulture;
                DirectoryInfo DI = new DirectoryInfo(fd[0]);
                FileInfo[] kdd = DI.GetFiles();

                tb = new string[] { Tester_TextBox.Text, Serial_Number_TextBox.Text, Car_Number_TextBox.Text, PyeonSung_Number_TextBox.Text };

                datagridview1.Rows.Clear();

                datagridview1.ColumnCount = 6;

                for (int i = 0; i < datagridview1.ColumnCount; i++)
                {
                    datagridview1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                datagridview1.Columns[0].Name = "DATE";
                datagridview1.Columns[1].Name = "Train Number";
                datagridview1.Columns[2].Name = "Car Number";
                datagridview1.Columns[3].Name = "Serial Number";
                datagridview1.Columns[4].Name = "Type of Test";
                datagridview1.Columns[5].Name = "Tester";
                // datagridview1.Columns[6].Name = "종류";
                // datagridview1.Columns[7].Name = "CTV 유무";

                for (int i = 0; i < kdd.Length; i++)
                {
                    try
                    {
                        string[] nameTemp = kdd[i].ToString().Split(new char[] { '_' });

                        if (nameTemp.Length >= 2)
                        {
                            string timeStart = DateTime.Parse(Start_Picker.Text).ToString("yyyyMMdd0000");
                            string timeEnd = DateTime.Parse(Stop_Picker.Text).ToString("yyyyMMdd") + "2500";


                            if (long.Parse(nameTemp[0]) <= long.Parse(timeEnd) && long.Parse(nameTemp[0]) >= long.Parse(timeStart))
                            {
                                if (Car_Number_TextBox.Text == "" && PyeonSung_Number_TextBox.Text == "" && Tester_TextBox.Text == "" && Serial_Number_TextBox.Text == "")
                                {
                                    nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                    datagridview1.Rows.Add(nameTemp);
                                }
                                else
                                {

                                    if (Tester_TextBox.Text == nameTemp[1])
                                    {
                                        if (Tester_TextBox.Text != "")
                                        {
                                            nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                            datagridview1.Rows.Add(nameTemp);
                                        }

                                    }
                                    else if (PyeonSung_Number_TextBox.Text == nameTemp[2])
                                    {
                                        if (PyeonSung_Number_TextBox.Text != "")
                                        {
                                            nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                            datagridview1.Rows.Add(nameTemp);
                                        }

                                    }
                                    else if (Car_Number_TextBox.Text == nameTemp[3])
                                    {
                                        if (Car_Number_TextBox.Text != "")
                                        {
                                            nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                            datagridview1.Rows.Add(nameTemp);
                                        }
                                    }
                                    else if (Serial_Number_TextBox.Text == nameTemp[4])
                                    {
                                        if (Serial_Number_TextBox.Text != "")
                                        {
                                            nameTemp[0] = DateTime.ParseExact(nameTemp[0], "yyyyMMddHHmm", provider).ToString();
                                            datagridview1.Rows.Add(nameTemp);
                                        }

                                    }


                                }
                            }
                        }


                    }
                    catch
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                datagridview1.ColumnCount = 8;

                for (int i = 0; i < datagridview1.ColumnCount; i++)
                {
                    datagridview1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                datagridview1.Columns[0].Name = "검사일시";
                datagridview1.Columns[1].Name = "편성번호";
                datagridview1.Columns[2].Name = "차량번호";
                datagridview1.Columns[3].Name = "일련번호";
                datagridview1.Columns[4].Name = "검사종류";
                datagridview1.Columns[5].Name = "검사자";
                datagridview1.Columns[6].Name = "종류";
                datagridview1.Columns[7].Name = "시험대상";

                ////MessageBox.Show(ex.Message);
                //MessageBox.Show("검색에 실패하였습니다. 다시 시도해 주세요.");
            }


        }
        private void Search_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            //Progress_PIC.Visible = true;
            //run = true;

            search_thrun = new Thread(search_thread);
            search_thrun.Start();
        }

        private void Print_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {


                StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "reportpath.ini");
                string[] fd = sr.ReadToEnd().Split('!');
                sr.Close();

                Process[] m2 = Process.GetProcessesByName("soffice");
                if (m2.Length > 0)
                    m2[0].Kill();
                //pansize = 0;
                //panel1.Size = new Size(pansize++, 1);
                Application.DoEvents();
                for (int i = 0; i < datagridview1.SelectedRows.Count; i++)
                {


                    //datagridview1.Columns[0].Name = "검사일시";
                    //datagridview1.Columns[1].Name = "편성번호";
                    //datagridview1.Columns[2].Name = "차량번호";
                    //datagridview1.Columns[3].Name = "일련번호";
                    //datagridview1.Columns[4].Name = "검사종류";
                    //datagridview1.Columns[5].Name = "검사자";
                    //datagridview1.Columns[6].Name = "종류";
                    //datagridview1.Columns[7].Name = "시험대상";

                    datagridview1.Columns[0].Name = "DATE";
                    datagridview1.Columns[1].Name = "Train Number";
                    datagridview1.Columns[2].Name = "Car Number";
                    datagridview1.Columns[3].Name = "Serial Number";
                    datagridview1.Columns[4].Name = "Type of Test";
                    datagridview1.Columns[5].Name = "Tester";


                    try
                    {
                        string FileName = DateTime.Parse(datagridview1.SelectedRows[i].Cells[0].Value.ToString()).ToString("yyyyMMddHHmm") + "_" + //날짜
                               datagridview1.SelectedRows[i].Cells[1].Value.ToString() + "_" + //편성번호
                               datagridview1.SelectedRows[i].Cells[2].Value.ToString() + "_" +//차량번호
                               datagridview1.SelectedRows[i].Cells[3].Value.ToString() + "_" +//일련번호
                               datagridview1.SelectedRows[i].Cells[4].Value.ToString() + "_" +//검사종류
                               datagridview1.SelectedRows[i].Cells[5].Value.ToString() + "_.dat";//검사자1
                               //datagridview1.SelectedRows[i].Cells[6].Value.ToString() + "_" +// 종류
                               //datagridview1.SelectedRows[i].Cells[7].Value.ToString() + "_.dat";//시험대상
                                                                                                 //datagridview1.SelectedRows[i].Cells[7].Value.ToString() + "_.dat";//판정

                        //string FileName = @"C:\odt\201911201615_박준호_21 - 5_1555_2213_KTX산천.dat";

                        // odt.Print(FileName);

                        string name = fd[0] + @"\" + FileName;


                        odt.Print2(name);
                        while (true)
                        {
                            Process[] m = Process.GetProcessesByName("soffice");

                            if (m.Length == 0)
                                break;
                            Thread.Sleep(100);
                        }

                    }
                    catch
                    {

                    }
                }





            }
            catch
            {


            }

        }
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (datagridview1.SelectedRows.Count > 11)
                        datagridview1.SelectedRows[0].Selected = false;
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "reportpath.ini");
                    string[] fd = sr.ReadToEnd().Split('!');
                    sr.Close();
                    string FileName = DateTime.Parse(datagridview1.SelectedRows[0].Cells[0].Value.ToString()).ToString("yyyyMMddHHmm") + "_" + //날짜
                                                   datagridview1.SelectedRows[0].Cells[1].Value.ToString() + "_" + //편성번호
                                                   datagridview1.SelectedRows[0].Cells[2].Value.ToString() + "_" +//차량번호
                                                   datagridview1.SelectedRows[0].Cells[3].Value.ToString() + "_" +//일련번호
                                                   datagridview1.SelectedRows[0].Cells[4].Value.ToString() + "_" +//검사종류
                                                   datagridview1.SelectedRows[0].Cells[5].Value.ToString() + "_.dat";//검사자1


                    string name = fd[0] + @"\" + FileName;
                    odt = new Odt(name);
                    //odt = new Odt(FileName);

                }
                catch
                {

                }


            }
            catch
            {

            }
        }
        private static void ReleaseExcelObject(Object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
        private void PyeonSung_Number_TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            tb.BackColor = SystemColors.Info;
        }
        private void PyeonSung_Number_TextBox_MouseLeave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            tb.BackColor = SystemColors.Window;
        }
        private void ReportForm_Load(object sender, EventArgs e)
        {
            sw = new Stopwatch();
            try
            {
                StreamReader sr = new StreamReader(@"reportpath.ini");
                string temp = sr.ReadToEnd();
                sr.Close();
                string[] Port = temp.Split('!');
                Report_Route = Port[0];
            }
            catch
            {

            }
        }
        private void Save_PDF_btn_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (Convert_to_PDF(NowName))
                    MessageBox.Show("PDF 저장이 완료 되었습니다.");
                else
                    MessageBox.Show("PDF 저장이 실패 되었습니다.");
            }
            catch
            {
                MessageBox.Show("PDF 저장이 실패 되었습니다.");
            }
        }

        private bool Convert_to_PDF(string filepath)
        {
            string msg = "";
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            excelApp = new Excel.Application();
            wb = excelApp.Workbooks.Open(Path.GetFullPath(filepath));
            ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;

            excelApp.DisplayAlerts = false;
            excelApp.Visible = false;
            excelApp.ScreenUpdating = false;
            excelApp.DisplayStatusBar = false;
            excelApp.Calculation = Excel.XlCalculation.xlCalculationManual;
            excelApp.EnableEvents = false;

            try
            {
                string file_name = wb.Name.Replace(".xlsx", "");

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                savefile.Title = "PDF 저장";
                savefile.DefaultExt = "pdf";
                savefile.Filter = "PDF 파일(*.pdf)|*.pdf";
                savefile.AddExtension = true;
                savefile.FileName = file_name;

                if (savefile.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                string save_file_path = savefile.FileName;

                Excel.Sheets sheets = null;
                sheets = wb.Worksheets;
                Excel._Worksheet _ws = (Excel._Worksheet)sheets.get_Item(1);

                string pdf_name = save_file_path;

                ws.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF,
                                            pdf_name, 1, false, false, 1, 2, false);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
                KillProcessByName("Excel");
            }
        }
        private static void KillProcessByName(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);
            if (processList.Length > 0)
            {
                foreach (Process p in processList)
                {
                    p.Kill();
                }
            }
            else
            {
                //Console.WriteLine("[{0}] 해당 프로세스는 실행중 이지 않습니다.", processName);
            }
        }
        private void edit_thread(object arg)
        {
            try
            {
                int pan_cnt = 0;
                string file_name = "";

                excelApp = new Excel.Application();
                WorkBook = excelApp.Workbooks.Open(NowName);
                WorkSheet = WorkBook.Worksheets.get_Item(1) as Excel.Worksheet;

                excelApp.DisplayAlerts = false;
                excelApp.Visible = false;
                excelApp.ScreenUpdating = false;
                excelApp.DisplayStatusBar = false;
                excelApp.Calculation = Excel.XlCalculation.xlCalculationManual;
                excelApp.EnableEvents = false;

                for (int i = 0; i < datagridview2.RowCount - 1; i++)
                {
                    if (datagridview2[1, i].Value != null)
                        WorkSheet.Cells[8 + i, 7] = datagridview2[1, i].Value.ToString();
                    if (datagridview2[2, i].Value != null)
                        WorkSheet.Cells[8 + i, 8] = datagridview2[2, i].Value.ToString();

                    if (datagridview2[2, i].Value.ToString() == Constant.NG)
                        pan_cnt++;
                }
                if (WorkBook.Name.IndexOf(Constant.NG) >= 0)
                {
                    if (pan_cnt == 0)
                    {
                        file_name = Report_Route + "\\" + WorkBook.Name.Replace("NG", "OK");
                    }
                    else
                        file_name = NowName;
                }
                else
                {
                    if (pan_cnt > 0)
                    {
                        file_name = Report_Route + "\\" + WorkBook.Name.Replace("OK", "NG");
                    }
                    else
                        file_name = NowName;
                }

                WorkBook.SaveAs(file_name, Type.Missing); // 성적서 위치 및 비밀번호

                if (WorkBook != null)
                {
                    WorkBook.Close();
                    WorkBook = null;
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    excelApp = null;
                }

                if (file_name != NowName)
                    File.Delete(NowName);

                MessageBox.Show("수정 완료");
            }
            catch
            {

            }
            finally
            {
                ReleaseExcelObject(WorkSheet);
                ReleaseExcelObject(WorkBook);
                ReleaseExcelObject(excelApp);
                KillProcessByName("Excel");

                Search_BTN_MouseUp(null, null);
            }
        }
        private void Edit_btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (NowName != "")
            {
                this.Opacity = 0.8;

                EditForm edit = new EditForm(this, NowName, data_location);
                edit.ShowDialog();

                Search_BTN_MouseUp(null, null);
            }
            else
            {
                MessageBox.Show("선택 된 파일이 없습니다.");
            }
        }
        private void rotateImage(Image hands, float angle, Image hands2)
        {
            Graphics GFX = Graphics.FromImage(hands);
            Matrix matrix = new Matrix();

            // 이미지 중심축 
            Point center = new Point((int)hands.Width / 2, (int)hands.Height / 2);

            // 회전각
            matrix.RotateAt(angle, center);

            GFX.Transform = matrix;
            GFX.Clear(Color.Transparent); // 회전하기전의 이미지 클리어

            // 드로잉
            GFX.DrawImage(hands2, new PointF(0, 0));
            GFX.Dispose();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (run)
            {
                if (xtf <= 355)
                {
                    xtf = xtf + 5;
                    rotateImage(Progress_PIC.Image, xtf, Properties.Resources.icons8_iphone_spinner_55);
                }
                else
                {
                    xtf = 0;
                    rotateImage(Progress_PIC.Image, xtf, Properties.Resources.icons8_iphone_spinner_55);
                }
                Progress_PIC.Refresh();
            }
        }

        private void datagridview1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
