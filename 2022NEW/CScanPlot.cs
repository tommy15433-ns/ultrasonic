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
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using _2022_Test.NSTEK. CustomHeatmap;
using _2022_Test.NSTEK.Models;
using static OlympusNDT.Instrumentation.NET.IDeviceInfo;

namespace _2022_Test
{
    public class CScanPlot
    {
        public SscanGraphModel sscanGraph { get; set; }
        public PlotModel plotModel { get; set; }
        public HeatMapSeries heatMapSeries { get; set; }
        public LineSeries lineSeries { get; set; }
        public double[,] plotData = { };

        public PlotView PlotView { get; set; }

        public LinearColorAxis axis { get; set; }
        public HeatmapModel heatmapModel { get; set; }
        public OxyPalette Palette { get; set; }


        public CScanPlot(Control parent)
        {

            PlotView = new PlotView();
            parent.Controls.Add(PlotView);
            PlotView.Dock = DockStyle.Fill;

            InitCscanPlot();
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
        public void CreateHeatMapFromBitmapSource(BitmapSource source)
        {
            plotModel.Series.Clear();
            plotModel.Axes.Clear();
            // 1. Calculate parameters for pixel extraction
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            // Ensure correct rounding for stride calculation
            int bytesPerPixel = (source.Format.BitsPerPixel + 7) / 8;
            int stride = width * bytesPerPixel;

            byte[] pixels = new byte[height * stride];

            // 2. Copy raw pixels into a flat array
            source.CopyPixels(pixels, stride, 0);

            // 3. Map flat pixel data to 2D double array
            double[,] data = new double[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * stride + x * bytesPerPixel;

                    // Extract intensity (assuming BGRA format)
                    byte blue = pixels[index];
                    byte green = pixels[index + 1];
                    byte red = pixels[index + 2];

                    double intensity = (red + green + blue) / 3.0;

                    // Correct for OxyPlot's bottom-left origin
                    data[x, height - 1 - y] = intensity;
                }
            }

            // 4. Create and configure the PlotModel
            plotModel.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Jet(256),
                Position = AxisPosition.Right
            });

            var hms = new HeatMapSeries
            {
                X0 = 0,
                X1 = width,
                Y0 = 0,
                Y1 = height,
                Data = data,
                Interpolate = true,
                // Set to Bitmap for faster rendering of large data
                RenderMethod = HeatMapRenderMethod.Bitmap
            };

            plotModel.Series.Add(hms);
        }
        public void RenderCScan(double[] array, int index)
        {

        }
        public void RenderCScan(Bitmap bm)
        {
            int width = bm.Width;
            int height = bm.Height;
            double[,] data = new double[height, width];

            // LockBits를 사용하여 고성능으로 픽셀 읽기
            var bmpData = bm.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // 예: RGB 평균값을 데이터로 사용 (그레이스케일 이미지 가정)
            // 2026년 방식: Span<T>를 사용하여 안전하고 빠르게 복사 가능
            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // ARGB 구조에서 밝기값 추출 예시
                        byte b = ptr[(y * bmpData.Stride) + (x * 4)];
                        data[y, x] = b;
                    }
                }
            }
            bm.UnlockBits(bmpData);

            heatMapSeries.Data = data;
            plotModel.InvalidatePlot(true);
            if (!heatMapSeries.IsVisible)
            {
                heatMapSeries.IsVisible = true;
            }
        }
        OxyPlot.Annotations.ImageAnnotation imageAnnotation;
        public void RenderCScan(byte[] img)
        {
            using (MemoryStream ms = new MemoryStream(img))
            {
                // 2. OxyImage 객체 생성 (OxyPlot 전용 이미지 타입)
                var oxyImage = new OxyImage(ms.ToArray());


                if (imageAnnotation == null)
                {
                    // 3. ImageAnnotation 등을 통해 모델에 추가
                    imageAnnotation = new OxyPlot.Annotations.ImageAnnotation
                    {
                        ImageSource = oxyImage,
                        X = new PlotLength(oxyImage.Width/2, PlotLengthUnit.Data),
                        Y = new PlotLength(oxyImage.Height/2, PlotLengthUnit.Data),
                        Width = new PlotLength(oxyImage.Width, PlotLengthUnit.Data),
                        Height = new PlotLength(oxyImage.Height, PlotLengthUnit.Data),
                        //HorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                        //VerticalAlignment = VerticalAlignment.Top
                        // 크기 및 위치 설정
                    };

                    plotModel.Annotations.Add(imageAnnotation);
                }
                imageAnnotation.ImageSource = oxyImage;
                plotModel.Axes[0].Minimum = 0;
                plotModel.Axes[0].Maximum = oxyImage.Width;
                plotModel.Axes[0].AbsoluteMinimum = 0;
                plotModel.Axes[0].AbsoluteMaximum = oxyImage.Width;
                plotModel.Axes[1].AbsoluteMinimum = 0;
                plotModel.Axes[1].AbsoluteMaximum = oxyImage.Height;

            }
            
            
            plotModel.InvalidatePlot(true);
        }
        public void InitCscanPlot()
        {
            plotModel = new PlotModel
            {
                Title = "Cscan Plotting",
            };
            var axis = new LinearAxis()
            {
                //Palette = OxyPalettes.Jet(200)
                //Palette = OxyPalettes.BlueWhiteRed(255),
                Maximum = 100,
                Minimum = 0,
                //Position = AxisPosition.Right

            };
            //Palette = axis.Palette;
            //plotModel.Axes.Add(axis);

            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = 100
            });
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 100
            });

            PlotView.Model = plotModel;
        }
    }
}
