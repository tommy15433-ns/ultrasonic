using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using _2022_Test.NSTEK.Models;

namespace _2022_Test.NSTEK.Plot
{
    public class BScanPlot: IDisposable
    {
        public const int RENDER_INTV_MIN = 100;

        BScanRenderModel model = null;
        PictureBox renderArea = null;
        BackgroundWorker renderer = null;
        double[,] Data = null;

        public bool IsRunning
        {
            get => renderer.IsBusy;
        }
        private int renderInterval = RENDER_INTV_MIN;
        public int RenderInterval
        {
            set
            {
                renderInterval = Math.Max(Math.Abs(value), RENDER_INTV_MIN);
            }
        }
        public BScanPlot(PictureBox pbcontrol)
        {
            //model = new BScanRenderModel();
            
            renderArea = pbcontrol;

            renderer = new BackgroundWorker();
            renderer.WorkerReportsProgress = true;
            renderer.DoWork += Renderer_DoWork;
            renderer.WorkerSupportsCancellation = true;
        }

        public void Initialize(int sizew, int sizeh, double angle_start, double angle_res)
        {
            if (model != null)
            {
                model.Dispose();
            }

            model = new BScanRenderModel(sizew, sizeh, 1f, 1f);
            model.StartAngle = angle_start;
            model.AngleResolution = angle_res;
            Data = new double[sizew, sizeh];
        }

        public void RenderStart()
        {
            if (!renderer.IsBusy)
            {
                renderer.RunWorkerAsync();
            }
        }
        public void RenderStop()
        {
            if (renderer.IsBusy)
            {
                renderer.CancelAsync();
            }
        }
        public void UpdateGate(double yStart, double height)
        {
            model.GateLength = height;
            model.GateStart = yStart;
        }
        public void UpdateData(int[,] updateData)
        {
            int maxw = Math.Min(Data.GetLength(0), updateData.GetLength(0));
            int maxh = Math.Min(Data.GetLength(1), updateData.GetLength(1));

            for (int i = 0; i < maxw; i++)
            {
                for (int j = 0; j < maxh; j++)
                {
                    Data[i, j] = updateData[i, j];
                }
            }
        }
        public void UpdateData(int[][] updateData)
        {
            int maxw = Math.Min(Data.GetLength(0), updateData.Length);
            int maxh = Math.Min(Data.GetLength(1), updateData[0].Length);

            try
            {
                for (int i = 0; i < maxw; i++)
                {
                    for (int j = 0; j < maxh; j++)
                    {
                        Data[i, j] = updateData[i][j];
                    }
                }

                model.UpdateData(Data);
                renderArea.Image = model.Bmap;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BScan updatedata err: " + ex.Message);
            }
        }
        private void Renderer_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch st = new Stopwatch();
            while (renderer.CancellationPending == false)
            {
                st.Restart();

                model.UpdateData(Data);
                renderArea.Image = model.Bmap;

                st.Stop();
                
                long freefor = (long)renderInterval - st.ElapsedMilliseconds;
                Debug.WriteLine("BScan render: " + st.ElapsedMilliseconds.ToString());
                if (freefor > 0)
                {
                    Thread.Sleep((int)freefor);
                }
            }
        }

        public void Dispose()
        {
            if (model != null)
            {
                model.Dispose();
            }
            if (renderArea != null)
            {
                renderArea.Dispose();
            }
            if (renderer != null)
            {
                renderer.Dispose();
            }
        }
    }
}
