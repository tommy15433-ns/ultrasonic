using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using global::_2022_Test.NSTEK.CustomHeatmap;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using _2022_Test.NSTEK.CustomHeatmap;
using _2022_Test.NSTEK.Models;
using static OlympusNDT.Instrumentation.NET.IDeviceInfo;

namespace _2022_Test
{


    namespace _2022_Test
    {
        public class AScanPlot
        {
            public SscanGraphModel sscanGraph { get; set; }
            public PlotModel plotModel { get; set; }
            public HeatMapSeries heatMapSeries { get; set; }
            public LineSeries lineSeries { get; set; }
            public double[,] plotData = { };

            public PlotView PlotView { get; set; }

            public LinearColorAxis axis { get; set; }


            public AScanPlot(Control parent)
            {

                PlotView = new PlotView();
                parent.Controls.Add(PlotView);
                PlotView.Dock = DockStyle.Fill;

                InitAScan();

                PlotView.Model = plotModel;
            }

            private void InitAScan()
            {
                plotModel = new PlotModel
                {
                    Title = "Ascan Plotting",
                };

                LinearAxis xAxis = new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    MajorGridlineStyle = LineStyle.Solid,
                };
                plotModel.Axes.Add(xAxis);

                LinearAxis yAxis = new LinearAxis
                {
                    Position = AxisPosition.Left,
                    MajorGridlineStyle = LineStyle.Solid,
                };
                plotModel.Axes.Add(yAxis);

                lineSeries = new LineSeries
                {
                    Title = "Ascan Data",
                    Color = OxyColors.Blue,
                    StrokeThickness = 1.5,
                };
                plotModel.Series.Add(lineSeries);


                plotModel.Legends.Add(new OxyPlot.Legends.Legend()
                {
                    LegendPosition = OxyPlot.Legends.LegendPosition.RightBottom
                });
            }


            const string GATETAG = "Gate";
            public void PlotGate(double start, double length, double threshold)
            {
                LineSeries gate = new LineSeries
                {
                    Title = "Gate",
                    Color = OxyColors.Red,
                    StrokeThickness = 3,
                    Tag = GATETAG
                };

                try
                {
                    plotModel.Series.Remove(plotModel.Series.Where(x => x.Tag == GATETAG).First());
                }
                catch { }

                gate.Points.Add(new DataPoint(start, threshold));
                gate.Points.Add(new DataPoint(length + start, threshold));
                plotModel.Series.Add(gate);
                plotModel.InvalidatePlot(true);
            }

        }
    }
}
