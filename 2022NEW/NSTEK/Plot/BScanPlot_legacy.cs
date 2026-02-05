using System;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using static OlympusNDT.Instrumentation.NET.IDeviceInfo;

namespace _2022_Test.NSTEK.Plot
{
    public class BScanPlot_legacy
    {
        public PlotModel plotModel { get; set; }
        public HeatMapSeries heatMapSeries { get; set; }
        public double[,] plotData = { };
        public PlotView PlotView { get; set; }
        public LinearColorAxis axis { get; set; }
        public OxyPalette Palette { get; set; }


        public BScanPlot_legacy(Control parent)
        {

            PlotView = new PlotView();
            parent.Controls.Add(PlotView);
            PlotView.Dock = DockStyle.Fill;

            InitBscanPlot();
            PlotView.Model = plotModel;
        }
        public double[,] testdata()
        {
            double x0 = -3.1;
            double x1 = 3.1;
            double y0 = -3;
            double y1 = 3;
            int rows = 100;
            int cols = 100;
            var data = new double[rows, cols];
            Func<double, double, double> peaks = (x, y) =>
                3 * (1 - x) * (1 - x) * Math.Exp(-(x * x) - (y + 1) * (y + 1))
                - 10 * (x / 5 - x * x * x - y * y * y * y * y) * Math.Exp(-x * x - y * y)
                - 1.0 / 3 * Math.Exp(-(x + 1) * (x + 1) - y * y);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double x = x0 + (x1 - x0) * i / (rows - 1);
                    double y = y0 + (y1 - y0) * j / (cols - 1);
                    data[i, j] = peaks(x, y);
                }
            }

            return data;
        }
        public void InitBscanPlot()
        {
            plotModel = new PlotModel
            {
                Title = "Bscan Plotting",
            };

            // Add axis
            var axis = new LinearColorAxis()
            {
                //Palette = OxyPalettes.Jet(200)
                Palette = BScanPallete(500),
                Minimum = 0,
                Maximum = 255,
            };
            Palette = axis.Palette;
            plotModel.Axes.Add(axis);

            //plotData = new double[2,2] {
            //    { 1.0, 1.0},
            //    { 1.0, 1.0} };
            plotData = testdata();

            // Add series
            heatMapSeries = new HeatMapSeries
            {
                X0 = 0,
                X1 = 100,
                Y0 = 0,
                Y1 = 100,
                Interpolate = true,
                RenderMethod = HeatMapRenderMethod.Bitmap,
                Data = plotData,
            };
            plotModel.Series.Add(heatMapSeries);
        }

        public static OxyPalette BScanPallete(int numberOfColors)
        {
            return OxyPalette.Interpolate(
                numberOfColors,
                OxyColors.White,
                OxyColors.AliceBlue,
                OxyColors.Blue,
                OxyColors.DarkBlue,
                OxyColors.DarkGreen,
                OxyColors.Green,
                OxyColors.GreenYellow,
                OxyColors.Yellow,
                OxyColors.Orange,
                OxyColors.OrangeRed,
                OxyColors.IndianRed,
                OxyColors.Red);
        }
    }
}
