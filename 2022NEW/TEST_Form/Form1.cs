using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using OlympusNDT.Instrumentation.NET;
using OxyPlot;
using OxyPlot.Series;
using _2022_Test.NSTEK.CustomHeatmap;
using _2022_Test.NSTEK.Database;
using _2022_Test.NSTEK.Device;
using _2022_Test.NSTEK.Models;
using _2022_Test.NSTEK.UI;
using _2022_Test._2022_Test;

namespace _2022_Test
{
    public partial class Form1 : Form
    {
        enum BeamStrat
        {
            lawfile = 0,
            custom1,
            custom2
        }

        BeamStrat SelectedBeamStrategy = BeamStrat.lawfile;

        BeamParam BeamParameter = new BeamParam { 
            Gain = 35, 
            AscanLength = 50000, 
            AscanStart = 0 };

        string LawFilePath
        {
            get => Directory.GetCurrentDirectory() + "\\LawFiles";
        }
        string selectedLawFile = "";

        string SelectedLawFile
        {
            get => selectedLawFile;
            set 
            {
                selectedLawFile = LawFilePath + "\\" + value;
            }

        }

        public double[][] GetSscanDelay()
        {
            // Calculate sscan delays
            // Calculate element positions
            var positions = DelayLawModel.GetElementsPosition(prob);

            // Calculate element delays
            double velocity = 5800; // stainless steel block

            var delays = DelayLawModel.GetSscanDelays(positions, velocity, scan);
            return delays;
        }

        public void CorrectSscan(double[][] delays, int prob_idx = 0)
        {
            for (uint i = 0; i < delays.GetLength(0); i++)
            {
                if (prob.UsedElementsPerBeam % 2 == 1)
                {
                    uint mid = prob.UsedElementsPerBeam / 2 + 1;
                    // set ascan start to the double of middle element's delay
                    focuspx.Beamsets[prob_idx].GetBeam(i).SetAscanStart(delays[i][mid] * 2);
                }
                else
                {
                    uint mid = prob.UsedElementsPerBeam / 2;
                    // set ascan start to the sum of the middle two elements' delays
                    focuspx.Beamsets[prob_idx].GetBeam(i).SetAscanStart(delays[i][mid] + delays[i][mid + 1]);
                }
            }
        }


        #region Amplitude Parameter
        public IAmplitudeSettings.AscanDataSize AscanDataSize
        {
            get
            {
                return (IAmplitudeSettings.AscanDataSize)Enum.Parse(typeof(IAmplitudeSettings.AscanDataSize), cb_ascandatasize.Text);
            }
        }
        public IAmplitudeSettings.RectificationType RectificationType
        {
            get
            {
                return (IAmplitudeSettings.RectificationType)Enum.Parse(typeof(IAmplitudeSettings.RectificationType), cb_rectificationtype.Text);
            }
        }
        public IAmplitudeSettings.ScalingType ScalingType
        {
            get
            {
                return (IAmplitudeSettings.ScalingType)Enum.Parse(typeof(IAmplitudeSettings.ScalingType), cb_scalingtype.Text);
            }
        }
        #endregion

        NSTEK.Models.ProbeModel prob = new NSTEK.Models.ProbeModel()
        {
            TotalElements = 32,
            UsedElementsPerBeam = 32,
            Frequency = 5,
            Pitch = 1
        };

        NSTEK.Models.SscanModel scan = new NSTEK.Models.SscanModel()
        {
            StartAngle = -45,
            EndAngle = 45,
            AngleResolution = 1,
            FocusDepth = 17
        };


        NSTEK.Device.FocusPx focuspx = null;
        ScanPlot plot = null;
        AScanPlot plot_ascan = null;
        CScanPlot plot_cscan = null;

        
        ModelUI mi = null;
        ModelUI scan_ui = null;

        public Form1()
        {
            InitializeComponent();
            InitThread();

            add_panel();

            cb_ascandatasize.Items.AddRange(Enum.GetNames(typeof(IAmplitudeSettings.AscanDataSize)));
            cb_ascandatasize.SelectedIndex = 0;
            cb_rectificationtype.Items.AddRange(Enum.GetNames(typeof(IAmplitudeSettings.RectificationType)));
            cb_rectificationtype.SelectedIndex = 0;
            cb_scalingtype.Items.AddRange(Enum.GetNames(typeof(IAmplitudeSettings.ScalingType)));
            cb_scalingtype.SelectedIndex = 0;

            mi = new NSTEK.UI.ModelUI(prob);
            scan_ui = new ModelUI(scan);

            plot = new ScanPlot(panel_plot, ScanPlot.PlotType.bscan);
            plot_ascan = new AScanPlot(panel_plot_ascan);
            plot_cscan = new CScanPlot(panel_plot_cscan);


            // TODO:
            // 100 should be length of data (or encoder)
            // 16 should be total number of elements used per probe. (1-32, 33-64, 65-96, 97-, they should not be combined)1
            CScanModel = new HeatmapModel(100, 16);

            chart_prob.Series.Clear();

            initialize_beam_strat_ui();

            NSTEK.Database.Database.ProbConfigs.Add(new NSTEK.Database.ProbConfig("Probe1"));
            NSTEK.Database.Database.ProbConfigs.Add(new NSTEK.Database.ProbConfig("Probe2"));
            NSTEK.Database.Database.ProbConfigs.Add(new NSTEK.Database.ProbConfig("Probe3"));
            NSTEK.Database.Database.ProbConfigs.Add(new NSTEK.Database.ProbConfig("Probe4"));

            FormFocalLaw ffl = new FormFocalLaw();
            //ffl.AddProb();
            ffl.Show();
            //panel_control.Enabled= false;
        }
        private void initialize_beam_strat_ui()
        {
            tab_beamStrat.TabPages.Clear();
            tab_beamStrat.TabPages.AddRange(
                Enum.GetNames(typeof(BeamStrat)).Select(x => new TabPage(x)
                {
                    UseVisualStyleBackColor = true
                }).ToArray()
             );
            tab_beamStrat.SelectedIndexChanged += (s, arg) =>
            {
                SelectedBeamStrategy = (BeamStrat)tab_beamStrat.SelectedIndex;
            };

            tab_beamStrat.SelectedIndex = (int)BeamStrat.custom1;

            init_tabpage_law();
            init_tabpage_custom1();
            init_tabpage_custom2();
        }
        private void init_tabpage_custom2()
        {
            TabPage page = tab_beamStrat.TabPages[(int)BeamStrat.custom2];
            page.Controls.Add(panel_custom2);
            panel_custom2.Dock = DockStyle.Fill;
        }
        private void init_tabpage_custom1()
        {
            TabPage page = tab_beamStrat.TabPages[(int)BeamStrat.custom1];

            page.Controls.Add(mi);
            mi.Location = new Point(5, 5);

            page.Controls.Add(scan_ui);
            scan_ui.Location = new Point(5 + mi.Width + 10, 5);

        }
        private void init_tabpage_law()
        {
            // make directory for law files
            if (!Directory.Exists(LawFilePath))
            {
                Directory.CreateDirectory(LawFilePath);
            }

            ComboBox cb = new ComboBox();
            cb.Width = 200;
            cb.Location = new Point(10, 10);
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.DropDown += (s, arg) =>
            {
                cb.Items.Clear();
                foreach (string file in Directory.GetFiles(LawFilePath).Where(x => x.Contains(".law")).ToArray())
                {
                    string[] filter = file.Split('\\');
                    cb.Items.Add(filter[filter.Length - 1]);
                }
            };
            cb.SelectedIndexChanged += (s, arg) =>
            {
                SelectedLawFile = cb.Text;
            };
            tab_beamStrat.TabPages[(int)BeamStrat.lawfile].Controls.Add(cb);
        }

        private void clear_prob_conf()
        {
            chart_prob.Series.Clear();
        }
        private void add_prob_conf(string name, double[] delay, int x_offset = 0)
        {
            
            chart_prob.Series.Add(name);
            chart_prob.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart_prob.Series[name].BorderWidth = 3;

            Guid id = Guid.NewGuid();
            Random r = new Random(id.GetHashCode());

            chart_prob.Series[name].Color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), 0);

            for (int i = 0; i < delay.Length; i++)
            {
                chart_prob.Series[name].Points.AddXY(i+x_offset, delay[i]);
            }
            chart_prob.ChartAreas[0].RecalculateAxesScale();
        }
        public void ConnectFocusPx()
        {
            try
            {
                focuspx = new FocusPx(10000);
                label_focuspx_status.Text = focuspx.Status;
                panel_control.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                focuspx = null;
                panel_control.Enabled = false;
            }
        }
        private void display_beam_formation(int index = 0)
        {
            clear_prob_conf();

            try
            {
                var beamset = focuspx.Beamsets[index];

                for (uint i = 0; i < beamset.GetBeamCount(); i++)
                {
                    // display prob setting
                    var formation = beamset.GetBeam(i).GetBeamFormation();
                    var collection = formation.GetPulserDelayCollection();
                    List<double> delay_list = new List<double>();

                    for (uint pulser = 0; pulser < collection.GetCount(); pulser++)
                    {
                        delay_list.Add(collection.GetElementDelay(pulser).GetDelay());
                    }
                    add_prob_conf($"beam{i.ToString()}", delay_list.ToArray(), (int)0);
                }
            }
            catch
            {
                MessageBox.Show($"beamset{index.ToString()} not initialized");
                return;
            }
        }
        private int get_ascan_index()
        {
            try
            {
                return Convert.ToInt32(textBox_ascan_index.Text);
            }
            catch
            {
                return 0;
            }
        }
        private void PlottingBScan()
        {
            int[][] bscanData;
            int plottingIndex = 0;

            while (true)
            {
                try
                {
                    bscanData = CollectedData;
                    if (bscanData == null)
                    {
                        continue;
                    }
                    if (bscanData.GetLength(0) < 1)
                    { break; }

                    
                    int[] data = bscanData[get_ascan_index()];
                    plot_ascan.lineSeries.Points.Clear();
                    for (int i = 0; i < data.Length; i++)
                    {
                        plot_ascan.lineSeries.Points.Add(new DataPoint(i, data[i]));
                    }
                    plot_ascan.plotModel.InvalidatePlot(true);

                    plot.plotData = new double[bscanData.GetLength(0), bscanData[0].GetLength(0)];

                    for (int i = 0; i < bscanData.GetLength(0); i++)
                    {
                        for (int j = 0; j < bscanData[0].GetLength(0); j++)
                        {
                            plot.plotData[i, j] = (double)bscanData[i][j];
                        }
                    }

                    plot.plotModel.InvalidatePlot(true);
                    plot.heatMapSeries.Data = plot.plotData;

                    plottingIndex += 1;
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex.ToString());
                }
            }
        }
        HeatmapModel CScanModel;
        public void PlottingCScan()
        {
        }
        public void PlottingSscan()
        {
            int[][] rawData;
            int plottingIndex = 0;

            while (true)
            {
                try
                {
                    rawData = focuspx.CollectBscanData();
                    if (rawData.GetLength(0) < 1)
                    { break; }


                    int[] data = rawData[0];
                    plot_ascan.lineSeries.Points.Clear();
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] > 0)
                        {
                            plot_ascan.lineSeries.Points.Add(new DataPoint(i, data[i]));
                        }
                    }
                    plot_ascan.plotModel.InvalidatePlot(true);
                    

                    // Get plot points
                    var plotPoints = plot.sscanGraph.GetPlotPoints();

                    plot.plotData = new double[plotPoints.GetLength(0), plotPoints.GetLength(1)];

                    // Assign plot value to each plot points
                    for (int xIndex = 0; xIndex < plotPoints.GetLength(0); xIndex++)
                    {
                        for (int yIndex = 0; yIndex < plotPoints.GetLength(1); yIndex++)
                        {
                            // current plot point
                            var p = plotPoints[xIndex, yIndex];
                            // X / Y = Tan(angle)
                            double angle = Math.Atan2(p.X, p.Y) * 180 / Math.PI;

                            // if angle is not in range (start angle, end angle)
                            // then assign this plot point value = 0
                            if ((angle < scan.StartAngle) || (angle > scan.EndAngle))
                            {
                                plot.plotData[xIndex, yIndex] = 0;
                            }
                            // if angle is in this range, then assgin it's value according to raw data
                            else
                            {
                                double radius = Math.Sqrt(p.X * p.X + p.Y * p.Y);
                                // find this plot point in which beam
                                int rawXIndex = (int)Math.Round((angle - scan.StartAngle) / scan.AngleResolution);
                                // find nearest Ascan value in this beam
                                double velocity = 5800; // m/s
                                int rawYIndex = (int)Math.Round((radius * 2e5) / velocity);
                                plot.plotData[xIndex, yIndex] = Math.Abs(rawData[rawXIndex][rawYIndex]);
                            }
                        }
                    }
                    plot.plotModel.InvalidatePlot(true);
                    plot.heatMapSeries.Data = plot.plotData;

                    plottingIndex += 1;
                }
                catch (Exception)
                {

                    //throw;
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
        public string parse_device_info(IDevice dev)
        {
            return $"{dev.GetConfiguration().GetDeviceName()}\r\n" +
                $"{dev.GetConfiguration().GetSerialNumber()}\r\n" +
                $"{dev.GetConfiguration().GetPlatform().ToString()}\r\n";
        }

        public void add_panel()
        {
            Control c = new BeamParamUi(0, BeamParameter);
            panel_test.Controls.Add(c);
            c.Location = new Point(0, 0);

            //mc.Dataset = Enumerable.Range(0, 100).Select(x => 0).ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectFocusPx();
        }
        private void create_custom2_beamset(int prob_idx = 0)
        {
            focuspx.CreatePABeamSetLinear(Convert.ToInt32(tb_elementcount.Text), Convert.ToInt32(tb_beamcount.Text), Convert.ToDouble(tb_dlyperpulser.Text), prob_idx);
        }
        private void create_lawfile_beamset(int prob_idx = 0)
        {
            if (File.Exists(SelectedLawFile))
            {
                focuspx.CreatPABeamSetFromLawFile(selectedLawFile, prob_idx);
            }
            else
            {
                MessageBox.Show(SelectedLawFile + " does not exist");
            }
                
        }
        private void create_custom1_beamset(int prob_idx = 0)
        {
            var delays = GetSscanDelay();
            //focuspx.CreatPASscanBeamSet(prob, delays, prob_idx);
            CorrectSscan(delays, 0);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int prob_idx = 0;
            try
            {
                prob_idx = Convert.ToInt32(tb_probsel.Text);
            }
            catch
            {
                prob_idx = 0;
            }
             

            switch (SelectedBeamStrategy)
            {
                case BeamStrat.lawfile:
                    create_lawfile_beamset(prob_idx);
                    break;

                case BeamStrat.custom1:
                    create_custom1_beamset(prob_idx);
                    break;
                case BeamStrat.custom2:
                    create_custom2_beamset(prob_idx);

                    int a = Convert.ToInt32(tb_elementcount.Text);
                    int b = Convert.ToInt32(tb_beamcount.Text);
                    CScanModel = new HeatmapModel(100, 32 - a + 1);

                    break;
            }

            display_beam_formation(0);
        }

        private void PlotCScan(int[][] raw, int index)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            int colorNumber = CScanModel.colorList.Count();

            for (int i = 0; i < raw.GetLength(0); i++)
            {
                // Get cscan value with gate
                var loc = focuspx.CrossGateLocation(raw[i], gate_start, gate_length, gate_thd);

                // Get cscan paint color
                var color = (loc - gate_start) * colorNumber / gate_length;

                // Paint
                CScanModel.PaintHeatMapPoint(new HeatmapPoint(index, i, (int)color));

                //Debug.WriteLine($"[{index},{loc}]");
            }
            plot_cscan.RenderCScan(CScanModel.BitmapToImageSource());
            //plot_cscan.RenderCScan(CScanModel.Bmap);
            st.Stop();
            Debug.WriteLine($"elapsed: {st.ElapsedMilliseconds}");
        }
        System.Timers.Timer render_timer;
        /// <summary>
        /// acquisition start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //focuspx.BindPAConnector();
            if (!ThreadRunning)
            {
                focuspx.InitiateAcquisition();

                foreach (IBeamSet bs in focuspx.Beamsets.Values)
                {
                    for (uint i = 0; i < bs.GetBeamCount(); i++)
                    {
                        bs.GetBeam(i).SetGain(BeamParameter.Gain);
                        bs.GetBeam(i).SetAscanLength(BeamParameter.AscanLength);
                        bs.GetBeam(i).SetAscanStart(BeamParameter.AscanStart);
                    }
                }

                focuspx.acquisition.ApplyConfiguration();
                focuspx.acquisition.Start();

                acquisition_start();


                render_timer = new System.Timers.Timer();
                render_timer.AutoReset = true;
                render_timer.Interval = 1000;

                int plottingIndex = 0;
                render_timer.Elapsed += (o, arg) =>
                {
                    int[][] bscanData;
                    try
                    {
                        bscanData = CollectedData;
                        if (bscanData == null)
                        {
                            return;
                        }
                        if (bscanData.GetLength(0) < 1)
                        { return; }

                        /// Todo:
                        /// make prob class 
                        /// sort raw data indices to match the prob[0:3]
                        /// ex) prob[0] uses 10 beams, prob[1] uses 16 beams
                        /// then rawdata[0:9] is for prob[0], rawdata[10:15] is for prob[1]

                        int[] data = bscanData[get_ascan_index()];
                        plot_ascan.lineSeries.Points.Clear();
                        for (int i = 0; i < data.Length; i++)
                        {
                            if (data[i] >= 0)
                            {
                                plot_ascan.lineSeries.Points.Add(new DataPoint(i, data[i]));
                            }
                        }
                        plot_ascan.plotModel.InvalidatePlot(true);


                        //BScan
                        plot.plotData = new double[bscanData.GetLength(0), bscanData[0].GetLength(0)];

                        for (int i = 0; i < bscanData.GetLength(0); i++)
                        {
                            for (int j = 0; j < bscanData[0].GetLength(0); j++)
                            {
                                plot.plotData[i, j] = (double)bscanData[i][j];
                            }
                        }

                        plot.plotModel.InvalidatePlot(true);
                        plot.heatMapSeries.Data = plot.plotData;



                        // TODO:
                        // encoder data should be plotting index
                        PlotCScan(bscanData, plottingIndex);
                        if (plottingIndex++ > gate_length)
                        {
                            plottingIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine(ex.ToString());
                    }

                };
                render_timer.Start();
            }
            else
            {
                acquisition_stop();
                render_timer.Stop();
                render_timer = null;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (IBeamSet bs in focuspx.Beamsets.Values)
            {
                for (uint i = 0; i < bs.GetBeamCount(); i++)
                {
                    bs.GetBeam(i).SetGain(BeamParameter.Gain);
                    bs.GetBeam(i).SetAscanLength(BeamParameter.AscanLength);
                    bs.GetBeam(i).SetAscanStart(BeamParameter.AscanStart);
                }
            }

            try
            {
                focuspx.acquisition.ApplyConfiguration();
            }
            catch
            {

            }
            
        }

        double gate_start = 0;
        double gate_length = 0;
        double gate_thd = 0;
        private void button11_Click(object sender, EventArgs e)
        {
            gate_start = Convert.ToDouble(tb_gatestart.Text);
            gate_length = Convert.ToDouble(tb_gateLength.Text);
            gate_thd = Convert.ToDouble(tb_gateThreshold.Text);

            plot_ascan.PlotGate(gate_start, gate_length, gate_thd);
        }

        private void btn_dbg_Click(object sender, EventArgs e)
        {
            foreach(ProbConfig pc in NSTEK.Database.Database.ProbConfigs)
            {
                MessageBox.Show(pc.Enable.ToString());
            }
        }

        private double[,] convert_to_2darr(List<double[]> list)
        {
            int rows = list.Count;
            int cols = list[0].Length;
            double[,] array2D = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array2D[i, j] = list[i][j];
                }
            }

            return array2D;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<double[]> delays = new List<double[]>();
            foreach (ProbConfig pc in NSTEK.Database.Database.ProbConfigs)
            {
                if (pc.Enable == true)
                {
                    double angle = pc.FocalLaw.AngleStart;
                    double res = pc.FocalLaw.AngleResolution;

                    do
                    {
                        delays.Add(BeamCalc.SearchFocalLaw(pc.Probe, NSTEK.Database.Database.Material, pc.Wedge, angle, pc.FocalLaw.FocusLength));

                        angle += res;
                    } while (angle < pc.FocalLaw.AngleStop);


                    focuspx.CreatePASscanBeamSet(pc.Probe, delays.ToArray());
                }
            }

            display_beam_formation(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            acquisition_stop();


            //focuspx.AcquisitionStop();

            focuspx.Reset();

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}