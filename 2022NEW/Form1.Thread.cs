using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OlympusNDT.Instrumentation.NET;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        bool ThreadRunning = false;
        int[][] CollectedData = null;
        BackgroundWorker bgw_acquire = new BackgroundWorker();
        BackgroundWorker bgw_consume = new BackgroundWorker();


        private void InitThread()
        {
            bgw_acquire.DoWork += Bgw_acquire_DoWork;
            bgw_acquire.RunWorkerCompleted += Bgw_acquire_RunWorkerCompleted;
            bgw_acquire.WorkerSupportsCancellation = true;

            bgw_consume.DoWork += Bgw_consume_DoWork;
            bgw_consume.WorkerSupportsCancellation = true;
        }

        private void Bgw_consume_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bgw_consume.CancellationPending == false)
            {
                try
                {
                    var dataResult = focuspx.acquisition.WaitForDataEx();
                    if (dataResult.status == IAcquisition.WaitForDataResultEx.Status.DataAvailable)
                    {
                        using (var cycleData = dataResult.cycleData)
                        {
                            dataResult = focuspx.acquisition.WaitForDataEx();
                            dataResult.Dispose();
                        }
                    }
                }
                catch
                {
                }
            }
            focuspx.acquisition.Stop();
            ThreadRunning = false;
        }

        void acquisition_start()
        {
            ThreadRunning = true;
            bgw_consume.RunWorkerAsync();
            bgw_acquire.RunWorkerAsync();
        }

        void acquisition_stop()
        {
            bgw_acquire.CancelAsync();
        }

        private void Bgw_acquire_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bgw_consume.CancelAsync();
        }
        private void Bgw_acquire_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bgw_acquire.CancellationPending == false)
            {
                var dataResult = focuspx.acquisition.WaitForDataEx();
                if (dataResult != null)
                {
                    if (dataResult.status == IAcquisition.WaitForDataResultEx.Status.DataAvailable)
                    {
                        var cycleData = dataResult.cycleData;
                        var ascans = cycleData.GetAscanCollection();

                        CollectedData = new int[ascans.GetCount()][];

                        for (uint index = 0; index < ascans.GetCount(); index++)
                        {
                            var ascan = ascans.GetAscan(index);

                            var id = ascans.GetAscan(index).GetBeamFiringOrder();
                            //ScanCount[(int)id]++;
                            //Debug.WriteLine($"ascan forder: {id.ToString()}, index: {index}");

                            int[] ascanData = new int[ascan.GetSampleQuantity()];
                            Marshal.Copy(ascan.GetData(), ascanData, 0, (int)ascan.GetSampleQuantity());
                            CollectedData[index] = ascanData;
                        }
                    }
                }
            }
        }
    }
}