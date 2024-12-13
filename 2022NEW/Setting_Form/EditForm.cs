using System;
using System.IO;
using System.Windows.Forms;
using Library;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace _2022_Test
{
    public partial class EditForm : Form
    {
        ReportForm reportform;
        string[] value = new string[10];
        string save_path;
        int[] data_location;
        string[] test_name = new string[] {"PAU", "COB", "SOB", "비상인터폰", "출력증폭기", "표시기", "모의주행" };
        string[][] test_arr = new string[][] {Setting.ListName0, Setting.ListName1, Setting.ListName2
                                            , Setting.ListName3, Setting.ListName4, Setting.ListName5, Setting.ListName6};
        static Excel.Application excelApp = null;
        static Excel.Workbook WorkBook = null; static Excel.Worksheet WorkSheet = null;
        public EditForm(ReportForm form, string path, int[] location)
        {
            InitializeComponent();
            reportform = form;
            save_path = path;
            data_location = location;
        }

        private void close_btn_MouseUp(object sender, MouseEventArgs e)
        {
            reportform.Opacity = 1;
            this.Close();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            try
            {
                excelApp = new Excel.Application();
                WorkBook = excelApp.Workbooks.Open(save_path);
                WorkSheet = WorkBook.Worksheets.get_Item(1) as Excel.Worksheet;

                for (int i = 0; i < test_arr.Length; i++)
                {
                    datagridview1.Rows.Add("=" + test_name[i] + "시험=");

                    for (int j = 0; j < test_arr[i].Length; j++)
                    {
                        Excel.Range range1 = WorkSheet.Cells[data_location[i] + j, 1];
                        Excel.Range range2 = WorkSheet.Cells[data_location[i] + j, 7];
                        Excel.Range range3 = WorkSheet.Cells[data_location[i] + j, 8];

                        if (range1.Value != null)
                            value[0] = range1.Value.ToString();
                        else
                            value[0] = "";

                        if (range2.Value != null)
                            value[1] = range2.Value.ToString();
                        else
                            value[1] = "";

                        if (range3.Value != null)
                        {
                            if (range3.Value == "OK")
                                value[2] = Constant.GOOD;
                            else
                                value[2] = Constant.NG;
                        }
                        else
                            value[2] = "";

                        datagridview1.Rows.Add(value);
                    }
                }
                datagridview1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

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

        private void edit_btn_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int pan_cnt = 0;
                int cnt = 1;
                string filename = save_path;

                excelApp = new Excel.Application();
                WorkBook = excelApp.Workbooks.Open(save_path);
                WorkSheet = WorkBook.Worksheets.get_Item(1) as Excel.Worksheet;

                for (int i = 0; i < test_arr.Length; i++)
                {
                    for (int j = 0; j < test_arr[i].Length; j++)
                    {
                        if (datagridview1[1, j + cnt].Value != null)
                            WorkSheet.Cells[data_location[i] + j, 7] = datagridview1[1, j + cnt].Value.ToString();
                        else
                            WorkSheet.Cells[data_location[i] + j, 7] = "";

                        if (datagridview1[2, j + cnt].Value != null)
                            WorkSheet.Cells[data_location[i] + j, 8] = datagridview1[2, j + cnt].Value.ToString();
                        else
                            WorkSheet.Cells[data_location[i] + j, 8] = "";

                        if (datagridview1[2, j + cnt].Value != null || datagridview1[2, j + cnt].Value.ToString() == Constant.NG)
                            pan_cnt++;
                    }
                    cnt += test_arr[i].Length + 1;
                }

                if (save_path.IndexOf(Constant.NG) >= 0)
                {
                    if (pan_cnt == 0)
                    {
                        save_path = save_path.Replace("NG", "OK");
                    }
                }
                else
                {
                    if (pan_cnt > 0)
                    {
                        save_path = save_path.Replace("OK", "NG");
                    }
                }

                WorkBook.SaveAs(save_path, Type.Missing); // 성적서 위치 및 비밀번호

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


                if (filename != save_path)
                    File.Delete(filename);

                MessageBox.Show("수정 완료");

                reportform.Opacity = 1;
                this.Close();
            }
            catch
            {

            }
        }
    }
}
