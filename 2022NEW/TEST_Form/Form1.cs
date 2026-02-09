using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
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

using _2022_Test.NSTEK;
using _2022_Test.NSTEK.CustomHeatmap;
using _2022_Test.NSTEK.Database;
using _2022_Test.NSTEK.Device;
using _2022_Test.NSTEK.Models;
using _2022_Test.NSTEK.Plot;
using _2022_Test.NSTEK.UI;
using _2022_Test.NSTEK.Device;
using _2022_Test.TEST_Form;
using Library;


namespace _2022_Test
{
    public partial class Form1 : Form
    {
        private int SelectedAscanIndex = 0;
        private BeamSetEx SelectedBeamset = null;

        string LawFilePath
        {
            get => Directory.GetCurrentDirectory() + "\\LawFiles";
        }
        string selectedLawFileName = "";
        string SelectedLawFileName
        {
            get => selectedLawFileName;
            set => selectedLawFileName = value;
        }
        string SelectedLawFile
        {
            get => LawFilePath + "\\" + selectedLawFileName;
        }
        FocusPx focuspx = null;
        private List<BScanPlot> plot_bscans = new List<BScanPlot>();
        AScanPlot plot_ascan = null;
        CScanPlot plot_cscan = null;
        FocalLawPlot plot_focallaw = null;

        public Form1()
        {
            InitializeComponent();
            InitThread();

            init_scan_sel_combobox();
            focuspx = new FocusPx(new FocusPx.FocusPxEventHandler(null, OnFocusPxConnected, OnFocusPxDisconnected));

            //plot_focallaw = new FocalLawPlot(panel_focallaw);

            //plot_bscan = new BScanPlot(pictureBox_bscan);
            plot_ascan = new AScanPlot(panel_plot_ascan);
            plot_cscan = new CScanPlot(panel_plot_cscan);


            // TODO:
            // 100 should be length of data (or encoder)
            // 16 should be total number of elements used per probe. (1-32, 33-64, 65-96, 97-, they should not be combined)1
            CScanModel = new HeatmapModel(100, 16);

            init_lawfile_combobox();

            Database.ProbConfigs.Add(new ProbConfig("Probe1"));
            Database.ProbConfigs.Add(new ProbConfig("Probe2"));
            Database.ProbConfigs.Add(new ProbConfig("Probe3"));
            Database.ProbConfigs.Add(new ProbConfig("Probe4"));

        }
        private void init_lawfile_combobox()
        {
            // make directory for law files
            if (!Directory.Exists(LawFilePath))
            {
                Directory.CreateDirectory(LawFilePath);
            }

            ComboBox cb = cb_lawfile;
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
                selectedLawFileName = cb.Text;
            };
        }
        private void init_scan_sel_combobox()
        {
            combobox_bscan_sel.DropDown += (s, e) =>
            {
                combobox_bscan_sel.Items.Clear();
                combobox_bscan_sel.Items.AddRange(BeamSetExCollection.GetNames());
            };
            combobox_bscan_sel.SelectedIndexChanged += (s, e) =>
            {
                try
                {
                    SelectedBeamset = BeamSetExCollection.Get(combobox_bscan_sel.Text);
                    display_beam_formation();
                }
                catch
                {
                    SelectedBeamset = null;
                }
            };
            combobox_ascan_sel.DropDown += (s, e) =>
            {
                combobox_ascan_sel.Items.Clear();
                try
                {
                    if (SelectedBeamset != null)
                    {
                        for (int i = 0; i < SelectedBeamset.BeamCount; i++)
                        {
                            int ascan_index = SelectedBeamset.BeamStartIndex + i;
                            combobox_ascan_sel.Items.Add(ascan_index.ToString());
                        }
                    }
                }
                catch 
                {
                }
            };
            combobox_ascan_sel.SelectedIndexChanged += (s, e) =>
            {
                try
                {
                    SelectedAscanIndex = Convert.ToInt32(combobox_ascan_sel.Text);
                }
                catch
                {
                    SelectedAscanIndex = 0;
                }
            };
        }

        public void OnFocusPxConnected(object sender, EventArgs e)
        {
            MessageBox.Show("Focus px connected");
            label_focuspx_status.Text = focuspx.Status;
            panel_control.Enabled = true;
        }
        public void OnFocusPxDisconnected(object sender, EventArgs e)
        {
            //MessageBox.Show(ex.ToString());
            MessageBox.Show("Focus px disconnected");
            panel_control.Enabled = false;
        }
        public void ConnectFocusPx()
        {
            try
            {
                focuspx.Connect("192.168.0.1", 5000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void display_beam_formation()
        {
            if (plot_focallaw == null)
            {
                return;
            }
            try
            {
                plot_focallaw.clear();

                if (SelectedBeamset != null)
                {
                    for (uint i = 0; i < SelectedBeamset.BeamSetPtr.GetBeamCount(); i++)
                    {
                        // display prob setting
                        var formation = SelectedBeamset.BeamSetPtr.GetBeam(i).GetBeamFormation();
                        var collection = formation.GetPulserDelayCollection();
                        List<double> delay_list = new List<double>();

                        for (uint pulser = 0; pulser < collection.GetCount(); pulser++)
                        {
                            delay_list.Add(collection.GetElementDelay(pulser).GetDelay());
                        }

                        plot_focallaw.add_prob_conf($"beam{i.ToString()}", delay_list.ToArray(), (int)0);
                    }
                }
            }
            catch
            {
                MessageBox.Show($"beamset not initialized");
                return;
            }
        }
        HeatmapModel CScanModel;
        public void PlottingCScan()
        {
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
        private void button1_Click(object sender, EventArgs e)
        {
            ConnectFocusPx();
        }
        private void create_lawfile_beamset(BeamSetEx dump, int prob_idx = 0)
        {
            if (File.Exists(SelectedLawFile))
            {
                dump.BeamSetPtr = focuspx.CreateBeamset(dump.Name, SelectedLawFile, ConnectorsPA.one);
                double[] angles = parse_angles_from_law_file(SelectedLawFile);

                dump.AngleStart = angles.First();
                if (angles.Length ==1)
                {
                    dump.AngleResolution = Math.Abs(angles[0]);
                }
                else
                {
                    dump.AngleResolution = Math.Abs(angles[0] - angles[1]);
                }
                    

                dump.BeamCount = angles.Count();
            }
            else
            {
                MessageBox.Show(SelectedLawFile + " does not exist");
            }
        }




        private double scan_index_to_time_mm(int index)
        {
            return (double)index * Database.Material.Velocity / 2.0 / 100000.0;
        }
        

        /// <summary>
        /// start stop acquisition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (BeamSetExCollection.List.Count == 0)
            {
                return;
            }
            
            if (ThreadRunning)
            {
                acquisition_stop();
            }
            else
            {
                acquisition_start();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int beamscount = BeamSetExCollection.TotalNumberOfBeams;
                
                CScanModel = new HeatmapModel(100, beamscount);

                var digitizer = Database.Digitizer;

                foreach(BeamSetEx bse in BeamSetExCollection.List)
                {
                    ScanRangeModel srm = Database.ScanRange;
                    MaterialModel fm = Database.Material;
                    double samplingfreq = DigitizerModel.DefaultSamplingRate / (double)Database.Digitizer.SamplingFactor;

                    bse.UpdateRange(srm.RangeStart_mm, srm.RangeEnd_mm, samplingfreq, fm.Velocity, digitizer.Compression);
                }

                foreach (BeamSetEx bse in BeamSetExCollection.List.ToArray())
                {
                    IBeamSet bs = bse.BeamSetPtr;

                    Debug.WriteLine($"{bse.Name} ascan start: {bse.CalculateAScanStart_ns()}");
                    Debug.WriteLine($"{bse.Name} ascan length: {bse.CalculateAScanEnd_ns() - bse.CalculateAScanStart_ns()}");

                    for (uint i = 0; i < bs.GetBeamCount(); i++)
                    {
                        bs.GetBeam(i).SetGain(digitizer.Gain);
                        bs.GetBeam(i).SetAscanLength(bse.CalculateAScanEnd_ns() - bse.CalculateAScanStart_ns());
                        bs.GetBeam(i).SetAscanStart(bse.CalculateAScanStart_ns());
                        Debug.WriteLine(bs.GetBeam(i).GetAscanLength().ToString());

                        uint gc = bs.GetBeam(i).GetGateCollection().GetCount();
                        //Debug.WriteLine($"beam{i} gate cnt: {gc}");
                        
                        //using (var timesetting = bs.GetDigitizingSettings().GetTimeSettings())
                        //{
                        //    Debug.WriteLine($"compfactor : {timesetting.GetAscanCompressionFactor()}");
                        //    Debug.WriteLine($"deciofactor : {timesetting.GetSamplingDecimationFactor()}");   
                        //}
                    }
                    var voltage_idx = (uint)Database.Voltage.VoltageIndex;
                    double voltage = focuspx.digitizerTechnologyPA.GetPulserVoltageCollection().GetPulserVoltage(voltage_idx);
                    focuspx.digitizerTechnologyPA.SetPulserVoltage(voltage);


                    using (var ampsetting = bs.GetDigitizingSettings().GetAmplitudeSettings())
                    {
                        ampsetting.SetAscanRectification(digitizer.RectificatioNType);
                        ampsetting.SetAscanDataSize(digitizer.AscanDataSize);
                        ampsetting.SetScalingType(digitizer.ScalingType);
                    }
                    using (var timesetting = bs.GetDigitizingSettings().GetTimeSettings())
                    {
                        timesetting.SetAscanCompressionFactor(digitizer.Compression);
                        timesetting.SetSamplingDecimationFactor(digitizer.SamplingFactor);
                    }
                    using (var filterSettings = bs.GetDigitizingSettings().GetFilterSettings())
                    {
                        uint filterindex = (uint)Database.Filter.FilterIndex;
                        var filterSel = focuspx.digitizerTechnologyPA.GetDigitalBandPassFilterCollection().GetDigitalBandPassFilter(filterindex);


                        filterSettings.SetDigitalBandPassFilter(filterSel);
                        var isSmoothFilterEn = Database.Filter.SmoothingFilter;
                        filterSettings.EnableSmoothingFilter(isSmoothFilterEn);
                        if (isSmoothFilterEn)
                        {
                            uint sflt_idx = (uint)Database.Filter.SmoothingFilterIndex;
                            double sflt = focuspx.digitizerTechnologyPA.GetSmoothingFilterCollection().GetSmoothingFilter(sflt_idx);
                            filterSettings.SetSmoothingFilter(sflt);
                        }
                    }
                    using (var pulserSetting = bs.GetPulsingSettings())
                    {
                        
                    }
                }

                //plot_ascan.UpdateAxisRangeX(BeamParameter.AscanLength - BeamParameter.AscanStart);
                //focuspx.ApplyConfig();
                double y_max = Math.Pow(2, (int)Database.Digitizer.AscanDataSize);
                //double velocity = Database.Database.Material.Velocity;
                //double x_max = Database.Database.ActualAscanDataSize * Database.Database.Digitizer.mm_per_sample(velocity);

                double x_mm = BeamSetExCollection.List.First().AngleResolution * (double)BeamSetExCollection.List.First().BeamSetPtr.GetBeam(0).GetAscanSampleQuantity();

                plot_ascan.SetRange(x_mm, y_max);

                int idx = 0;
                foreach(BeamSetEx bse in BeamSetExCollection.List)
                {
                    plot_bscans[idx++].Initialize(
                        (int)bse.BeamCount,
                        (int)bse.BeamSetPtr.GetBeam(0).GetAscanSampleQuantity(),
                        bse.AngleStart,
                        bse.AngleResolution
                    );
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }
        private void button11_Click(object sender, EventArgs e)
        {
            foreach(var beamset in BeamSetExCollection.List.Select(x =>x.BeamSetPtr))
            {
                uint beamcnt = beamset.GetBeamCount();
                for (uint i = 0; i < beamcnt; i++) 
                {
                    using (var gate = beamset.GetBeam(i).GetGateCollection().GetGate(0))
                    {
                        gate.SetThreshold(Database.Gate.Threshold);
                        gate.SetStart(Database.Gate.Start);
                        gate.SetLength(Database.Gate.Length);
                        gate.InCycleData(true);
                    }
                }
            }


            foreach(BScanPlot bp in plot_bscans)
            {
                bp.UpdateGate(Database.Gate.Start / BeamSetExCollection.List.First().Resolution, Database.Gate.Length / BeamSetExCollection.List.First().Resolution);
            }
        }

        private void btn_dbg_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Database.Database.ScanRange.GetUnit("RangeStart_mm"));

            //focuspx.GetAcqState();
            //MessageBox.Show(focuspx.device.GetState().ToString());


            plot_cscan.UpdateDataSize(100, 31);
            int[] arr = new int[] {1,2,3,4,5};
            int summ = arr.Aggregate((cur, sum) => sum += cur);
            MessageBox.Show($"{summ}");




            foreach (BeamSetEx beamset in BeamSetExCollection.List)
            {
                //beamset.BeamSetPtr.GetPulsingSettings().SetPulseWidth(Convert.ToDouble(tb_pulsewidth.Text));
            }

            //Debug.WriteLine($"resolution: {Beamsets.First().Resolution}");
            //Debug.WriteLine($"velocity: {Beamsets.First().Velocity}");
            //Debug.WriteLine($"compression: {Beamsets.First().Compression}");
            //Debug.WriteLine($"astart: {Beamsets.First().CalculateAScanStart_ns()}");
            //Debug.WriteLine($"aend: {Beamsets.First().CalculateAScanEnd_ns()}");

            //int[][] asdf = new int[][]
            //{
            //    new int[] {1,2,3,4 },
            //    new int[] {5,6,7,8 },
            //    new int[] {9,10,11,12},
            //    new int[] {13,14,15,16},
            //    new int[] {17,18,19,20},
            //    new int[] {21,22,23,24}
            //};

            //int[][] a = asdf.Skip(0).Take(3).ToArray();
            //int[][] b = asdf.Skip(3).Take(3).ToArray();
            #region Data to CSV
            //string filePath = "heatmap_data.csv";

            //using (StreamWriter sw = new StreamWriter(filePath))
            //{
            //    int rows = CollectedData.Length;

            //    for (int i = 0; i < rows; i++)
            //    {
            //        int cols = CollectedData[i].Length;
            //        string[] rowValues = new string[cols];
            //        for (int j = 0; j < cols; j++)
            //        {
            //            rowValues[j] = CollectedData[i][j].ToString(CultureInfo.InvariantCulture);
            //        }
            //        // Write the entire row as a comma-separated string
            //        sw.WriteLine(string.Join(",", rowValues));
            //    }
            //}
            #endregion

            #region test
            //Type et = Database.Database.Material.GetTypeOf("Sf");
            //if (et.IsEnum)
            //{

            //    foreach (string name in et.GetEnumNames().ToList())
            //    {
            //        MessageBox.Show(name);
            //    }

            //}


            //MessageBox.Show(Database.Database.Material.GetTypeOf("Sf").ToString());
            #endregion

            //MessageBox.Show(Database.Database.Digitizer.SamplingFactor.ToString());
            //Debug.WriteLine("Ascan size: " + Database.Database.ActualAscanDataSize.ToString());
            //Debug.WriteLine(Database.Database.Digitizer.mm_per_sample(5890.0).ToString());
            //Debug.WriteLine(focuspx.Beamsets[0].GetBeamCount().ToString());
            //MessageBox.Show("comp: " + focuspx.Beamsets[0].GetDigitizingSettings().GetTimeSettings().GetAscanCompressionFactor().ToString());
            //MessageBox.Show("samp: " + focuspx.Beamsets[0].GetDigitizingSettings().GetTimeSettings().GetSamplingDecimationFactor().ToString());

            //MessageBox.Show(focuspx.Beamsets[0].GetDigitizingSettings().GetTimeSettings().GetAscanSamplingResolution().ToString());
            //MessageBox.Show(focuspx.GetBeamSetCount().ToString());
            //focuspx.ResetBeamsets();
            //MessageBox.Show(focuspx.GetBeamSetCount().ToString());

            //focuspx
            //using (var voltages = focuspx.ultrasoundConfiguration.GetDigitizerTechnology(UltrasoundTechnology.PhasedArray).GetPulserVoltageCollection())
            //{
            //    uint size = voltages.GetCount();
            //    for (int i = 0; i < size; i++)
            //    {
            //        Debug.WriteLine($"voltage[{i}]: {voltages.GetPulserVoltage((uint)i).ToString()}");
            //    }
            //}


            //using (var filters = focuspx.ultrasoundConfiguration.GetDigitizerTechnology(UltrasoundTechnology.PhasedArray).GetDigitalBandPassFilterCollection())
            //{
            //    uint size = filters.GetCount();
            //    for (uint i = 0; i < size; i++)
            //    {
            //        Debug.WriteLine($"filter[{i}]: type: {filters.GetDigitalBandPassFilter(i).GetFilterType().ToString()} " +
            //            $"char: {filters.GetDigitalBandPassFilter(i).GetCharacteristic().ToString()} " +
            //            $"high cutoff: {filters.GetDigitalBandPassFilter(i).GetHighCutOffFrequency().ToString()}" +
            //            $"low cutoff: {filters.GetDigitalBandPassFilter(i).GetLowCutOffFrequency().ToString()}");
            //    }
            //}
            ////MessageBox.Show(Database.Database.Filter.EnableFilter.ToString());
            //var sc = focuspx.ultrasoundConfiguration.GetDigitizerTechnology(UltrasoundTechnology.PhasedArray).GetSmoothingFilterCollection();
            //var s = sc.GetCount();
            //for (uint i = 0; i < s; i++)
            //{
            //    Debug.WriteLine($"Smoothing filter[{i}] - {sc.GetSmoothingFilter(i).ToString()}");
            //}

            //foreach(double d in AnglesPerIndex)
            //{
            //    Debug.WriteLine($"angle: {d}");
            //}

            //MessageBox.Show(Database.Database.Digitizer.ToString());

            //Debug.WriteLine($"qty: {focuspx.Beamsets.First().Value.GetBeam(0).GetAscanSampleQuantity()}");
            //Debug.WriteLine($"rec: {focuspx.Beamsets.First().Value.GetBeam(0).GetRecurrence()}");
            //Debug.WriteLine($"alen: {focuspx.Beamsets.First().Value.GetBeam(0).GetAscanLength()}");
            //Debug.WriteLine($"astart: {focuspx.Beamsets.First().Value.GetBeam(0).GetAscanStart()}");

        }   
        private double[] parse_angles_from_law_file(string path)
        {
            List<double> angles = new List<double>();
            if (File.Exists(SelectedLawFile))
            {
                string text = File.ReadAllText(SelectedLawFile);
                string[] asdf = text.Replace("\r\n", "\n").Split(new char[] { '\n' });
                foreach (string str in asdf)
                {
                    string[] tmp = str.Split(new char[] { ' ' });
                    if (tmp.Length > 8)
                    {
                        double angle = Convert.ToDouble(tmp[6]) / 10;
                        angles.Add(angle);
                    }
                }
            }
            return angles.ToArray();
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

        private void update_bscan_plots()
        {
            foreach(BScanPlot p in plot_bscans.ToArray())
            {
                p.Dispose();
            }
            plot_bscans.Clear();

            table_bscans.ColumnStyles.Clear();
            table_bscans.ColumnCount = BeamSetExCollection.BeamsetsCount;

            foreach (var bscan in BeamSetExCollection.List)
            {
                var column = new ColumnStyle(SizeType.Percent, 100 / BeamSetExCollection.TotalNumberOfBeams);
                table_bscans.ColumnStyles.Add(column);

                PictureBox pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                BScanPlot plot = new BScanPlot(pictureBox);
                table_bscans.Controls.Add(pictureBox);
                plot_bscans.Add(plot);


                //if (col_idx == 0)
                //{
                //    BScanPlot bplot = new BScanPlot(pictureBox1);
                //    //pictureBox1.Dock = DockStyle.Fill;
                //    //table_bscans.Controls.Add(pictureBox1, col_idx++, 0);
                //    plot_bscans.Add(bplot);
                //}
                //else
                //{
                //    BScanPlot bplot = new BScanPlot(pictureBox1);
                //    //pictureBox2.Dock = DockStyle.Fill;
                //    //table_bscans.Controls.Add(pictureBox2, col_idx++, 0);
                //    plot_bscans.Add(bplot);
                //}
                //col_idx++;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists(SelectedLawFile))
            {
                BeamSetEx beamset = new BeamSetEx(selectedLawFileName);
                create_lawfile_beamset(beamset);
                if (BeamSetExCollection.List.Count == 0)
                {
                    beamset.BeamStartIndex = 0;
                }
                else
                {
                    beamset.BeamStartIndex = BeamSetExCollection.NextIndex();
                }


                display_beam_formation();

                BeamSetExCollection.Add(beamset);
                update_bscan_plots();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<double[]> delays = new List<double[]>();
            foreach (ProbConfig pc in Database.ProbConfigs)
            {
                if (pc.Enable == true)
                {
                    double angle = pc.FocalLaw.AngleStart;
                    double res = pc.FocalLaw.AngleResolution;

                    BeamSetEx beamset = new BeamSetEx(pc.Name);
                    
                    do
                    {
                        delays.Add(BeamCalc.SearchFocalLaw(pc.Probe, Database.Material, pc.Wedge, angle, pc.FocalLaw.FocusLength));
                        angle += res;
                    } while (angle < pc.FocalLaw.AngleStop + 0.1);


                    IBeamSet ptr = focuspx.CreateBeamset(pc.Name, pc.Probe, delays.ToArray(), ConnectorsPA.one);
                    beamset.BeamSetPtr = ptr;

                    beamset.BeamStartIndex = BeamSetExCollection.NextIndex();
                    beamset.BeamCount = delays.Count;
                    beamset.AngleStart = pc.FocalLaw.AngleStart;
                    beamset.AngleResolution = pc.FocalLaw.AngleResolution;

                    BeamSetExCollection.Add(beamset);
                    delays.Clear();
                }
            }

            display_beam_formation();
            update_bscan_plots();
        }
        private void btn_show_config_Click(object sender, EventArgs e)
        {
            FormFocalLaw ffl = new FormFocalLaw();
            //ffl.AddProb();
            ffl.Show();
        }

        private void btn_clear_beam_Click(object sender, EventArgs e)
        {
            try
            {
                //var arr = Beamsets.ToArray();
                var arr = BeamSetExCollection.List.ToArray();
                foreach (var beamEx in arr)
                {

                    focuspx.RemoveBeamset(beamEx.BeamSetPtr);
                    BeamSetExCollection.Remove(beamEx);
                }
            }
            catch
            {

            }
            
        }
    }
}