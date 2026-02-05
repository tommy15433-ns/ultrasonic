using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OlympusNDT.Instrumentation.NET;
using OxyPlot;

using _2022_Test.NSTEK.Database;
using _2022_Test.NSTEK.Models;
using _2022_Test.NSTEK.CustomHeatmap;

namespace _2022_Test
{
    partial class Form1
    {
        bool ThreadRunning
        {
            get => IsBusy;
            set
            {
                ;
            }
        }
        public bool IsBusy
        {
            get => bgw_renderer.IsBusy | bgw_acquire.IsBusy;
        }

        int[][] CollectedData = null;
        double[] CollectedCScanData = null;
        
        BackgroundWorker bgw_acquire = new BackgroundWorker();
        BackgroundWorker bgw_consume = new BackgroundWorker();
        BackgroundWorker bgw_renderer = new BackgroundWorker();


        private void InitThread()
        {
            bgw_acquire.DoWork += Bgw_acquire_DoWork;
            bgw_acquire.RunWorkerCompleted += Bgw_acquire_RunWorkerCompleted;
            bgw_acquire.WorkerSupportsCancellation = true;

            bgw_renderer.DoWork += Bgw_rendrer_DoWork;
            bgw_renderer.WorkerSupportsCancellation = true;
            bgw_renderer.RunWorkerCompleted += Bgw_renderer_RunWorkerCompleted;

            bgw_consume = null;
            //bgw_consume.DoWork += Bgw_consume_DoWork;
            //bgw_consume.WorkerSupportsCancellation = true;
        }

        private void Bgw_renderer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bgw_acquire.CancelAsync();
        }

        private void Bgw_rendrer_DoWork(object sender, DoWorkEventArgs e)
        {
            int cscan_index = 0;

            while (bgw_renderer.CancellationPending == false)
            {
                int[][] bscanData;
                try
                {
                    bscanData = CollectedData.ToArray();
                    if (bscanData == null)
                    {
                        continue;
                    }
                    foreach (int[] a in bscanData)
                    {
                        if (a == null)
                        {
                            continue;
                        }
                    }

                    if (bscanData.GetLength(0) < 1)
                    { continue; }

                    // scanned data 
                    Debug.WriteLine($"scanned size x:{bscanData.Length}, y:{bscanData[0].Length}");
                    /// Todo:
                    /// make prob class 
                    /// sort raw data indices to match the prob[0:3]
                    /// ex) prob[0] uses 10 beams, prob[1] uses 16 beams
                    /// then rawdata[0:9] is for prob[0], rawdata[10:15] is for prob[1]
                    #region ascan plot

                    int[] data = bscanData[SelectedAscanIndex];
                    plot_ascan.lineSeries.Points.Clear();

                    //double scansizeratio = Database.Database.Digitizer.mm_per_sample(Database.Database.Material.Velocity);
                    //double r = scansizeratio * Math.Cos(AnglesPerIndex[SelectedAscanIndex] * Math.PI / 180.0);
                    for (int i = 0; i < data.Length; i++)
                    {
                        //plot_ascan.lineSeries.Points.Add(new DataPoint((double)i * r, (double)data[i]));
                        plot_ascan.lineSeries.Points.Add(new DataPoint(i * BeamSetExCollection.List.First().Resolution, (double)data[i]));
                        plot_ascan.PlotGate(
                            Database.Gate.Start,       // / Beamsets.First().Resolution,
                            Database.Gate.Length,       // / Beamsets.First().Resolution,
                            Database.Gate.Threshold);
                    }
                    plot_ascan.plotModel.InvalidatePlot(true);
                    #endregion


                    int render_index = 0;
                    foreach (BeamSetEx bse in BeamSetExCollection.List)
                    {
                        int[][] subarr = bscanData.Skip(bse.BeamStartIndex).Take(bse.BeamCount).ToArray();
                        //Debug.WriteLine($"sub[{render_index} size: {subarr.Length} {subarr[0].Length} ");
                        plot_bscans[render_index++].UpdateData(subarr.ToArray());
                    }


                    PlotCScanSw(bscanData, cscan_index++);
                    if (CollectedCScanData != null)
                    {
                        //PlotCScan(CollectedCScanData, render_index++);
                    }
                    // render index should be encoder
                    if (cscan_index >= 100)
                    {
                        cscan_index = 0;
                    }
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex.ToString());
                }
            }
        }
        private void PlotCScanSw(int[][] raw, int index)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            int colorNumber = CScanModel.colorList.Count();

            for (int i = 0; i < raw.GetLength(0); i++)
            {
                // Get cscan value with gate
                double gate_start = Database.Gate.Start / BeamSetExCollection.List.First().Resolution;
                double gate_length = Database.Gate.Length / BeamSetExCollection.List.First().Resolution;
                double gate_thd = Database.Gate.Threshold;

                var loc = focuspx.CrossGateLocation(raw[i], gate_start, gate_length, gate_thd);

                if (gate_length != 0)
                {
                    // Get cscan paint color
                    var color = Math.Abs((loc - gate_start)) * colorNumber / gate_length;
                    // Paint
                    CScanModel.PaintHeatMapPoint(new HeatmapPoint(index, i, (int)color));
                }
                //Debug.WriteLine($"[{index},{loc}]");
            }
            plot_cscan.RenderCScan(CScanModel.BitmapToImageSource());
            //plot_cscan.RenderCScan(CScanModel.Bmap);
            st.Stop();
            Debug.WriteLine($"elapsed: {st.ElapsedMilliseconds}");
        }
        int coloridx = 0;
        private void PlotCScan(double[] crosstimes, int index)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            int colorNumber = CScanModel.colorList.Count();

            for (int i = 0; i < crosstimes.Length; i++)
            {
                var color = (int)(colorNumber * crosstimes[i]);
                //CScanModel.PaintHeatMapPoint(new HeatmapPoint(index, i, (int)color));
                CScanModel.PaintHeatMapPoint(new HeatmapPoint(index, i, (coloridx++ % colorNumber)));
            }
            plot_cscan.RenderCScan(CScanModel.BitmapToImageSource());
        }

        private void RenderingStart()
        {
            if (!plot_bscans[0].IsRunning)
            {
                if (focuspx.AcquisitionStart() == false)
                {
                    MessageBox.Show("Acquisition initialize failed");
                    return;
                }
            }
            else
            {
                acquisition_stop();

                // stop focuspx on event OnAcquisitionStopped
                //focuspx.AcquisitionStop();

                foreach (var bp in plot_bscans)
                {
                    bp.RenderStop();
                }
            }
        }



        private void Bgw_consume_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (bgw_consume.CancellationPending == false)
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
                focuspx.acquisition.Stop();
                ThreadRunning = false;
            }
            catch
            {
                //focuspx.acquisition.Stop();


                ThreadRunning = false;
            }
        }

        void acquisition_start()
        {
            ThreadRunning = true;
            if (bgw_consume != null)
            {
                bgw_consume.RunWorkerAsync();
            }

            int ascan_count = 0;
            foreach(var beamset in BeamSetExCollection.List)
            {
                ascan_count += beamset.BeamCount;
            }

            CollectedData = new int[ascan_count][];
            
            for (int i = 0; i < CollectedData.Length; i++)
            {
                CollectedData[i] = new int[BeamSetExCollection.List[0].BeamSetPtr.GetBeam(0).GetAscanSampleQuantity()];
            }

            
            if (focuspx.AcquisitionStart())
            {
                bgw_acquire.RunWorkerAsync();
                bgw_renderer.RunWorkerAsync();
            }
        }

        void acquisition_stop()
        {
            // must stop in order renderer -> aquisition
            // acquisition thread is stopped in renderer's completed event
            bgw_renderer.CancelAsync();
        }

        private void Bgw_acquire_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (bgw_consume != null)
            {
                bgw_consume.CancelAsync();
            }


            //focuspx.AcquisitionDispose();
            ThreadRunning = false;
        }
        private void Bgw_acquire_DoWork(object sender, DoWorkEventArgs e)
        {
            List<double> clist = new List<double>();
            try
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
                            var cscan = cycleData.GetCscanCollection();
                            Debug.WriteLine($"Cscan cnt: {cscan.GetCount()}");

                            //CollectedData = new int[ascans.GetCount()][];

                            int[] ascanData = new int[ascans.GetAscan(0).GetSampleQuantity()];
                            for (uint index = 0; index < ascans.GetCount(); index++)
                            {
                                var ascan = ascans.GetAscan(index);

                                var id = ascans.GetAscan(index).GetBeamFiringOrder();
                                //ScanCount[(int)id]++;
                                //Debug.WriteLine($"ascan forder: {id.ToString()}, index: {index}");

                                
                                //Marshal.Copy(ascan.GetData(), ascanData, 0, (int)ascan.GetSampleQuantity());
                                Marshal.Copy(ascan.GetData(), CollectedData[index], 0, (int)ascan.GetSampleQuantity());
                                //CollectedData[index] = ascanData.ToArray();
                            }

                            
                            for (uint index = 0; index < cscan.GetCount(); index++)
                            {
                                using (var cs = cscan.GetCscan(index))
                                {
                                    // ratio of detected position : gate length
                                    double rate = cs.IsNotDetect() ? 0.0 : ((cs.GetCrossingTime() - Database.Gate.Start) / Database.Gate.Length);
                                    clist.Add(rate);
                                }
                            }

                            CollectedCScanData = clist.ToArray();
                            clist.Clear();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Bgw_acquire_DoWork exception occured: " + ex.Message, "Fatal");
            }
            
        }
    }
}