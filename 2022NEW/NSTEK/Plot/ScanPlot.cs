using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using _2022_Test.NSTEK.CustomHeatmap;

namespace _2022_Test.NSTEK.Plot
{
    public class ScanPlot
    {
        public enum PlotType
        {
            ascan, bscan, sscan, cscan
        };
        public SscanGraphModel sscanGraph { get; set; }
        public PlotModel plotModel { get; set; }
        public HeatMapSeries heatMapSeries { get; set; }
        public LineSeries lineSeries { get; set; }
        public double[,] plotData = { };

        public PlotView PlotView { get; set; }

        public LinearColorAxis axis { get; set; }

        public PlotModel SecondaryModel { get; set; }
        public HeatmapModel heatmapModel { get; set; }
        private BitmapSource _heatmapGraph;
        public BitmapSource heatmapGraph
        {
            get { return _heatmapGraph; }
            set
            {
                _heatmapGraph = value;
                //NotifyOfPropertyChange(() => heatmapGraph);
            }
        }

        public OxyPalette Palette { get; set; }


        public ScanPlot(Control parent, PlotType type = PlotType.sscan)
        {

            PlotView = new PlotView();
            parent.Controls.Add(PlotView);
            PlotView.Dock = DockStyle.Fill;

            switch (type)
            {
                case PlotType.sscan:
                    InitSscanGraph();
                    InitSscanPlot();

                    PlotView.Model = plotModel;

                    break;

                case PlotType.ascan:
                    InitAScan();

                    PlotView.Model = plotModel;

                    break;

                case PlotType.bscan:

                    InitBscanPlot();
                    PlotView.Model = plotModel;

                    break;

                case PlotType.cscan:
                    //InitCscanPlot();
                    initcscan2();
                    //PlotView.Model = plotModel;


                    break;
                default:
                    throw new Exception("Not implemented");
            }
            
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
            


            //SecondaryModel.Series.Add(gate);
            //SecondaryModel.InvalidatePlot(true);
        }
        public void InitSscanGraph()
        {
            AxisModel XAxis = new AxisModel
            {
                Min = -20, // mm
                Max = 20, // mm
                Resolution = 0.1 // mm
            };

            AxisModel YAxis = new AxisModel
            {
                Min = 0, // mm
                Max = 50, // mm
                Resolution = 0.1 // mm
            };

            sscanGraph = new SscanGraphModel
            {
                XAxis = XAxis,
                YAxis = YAxis,
            };

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
        public void InitSscanPlot()
        {
            plotModel = new PlotModel
            {
                Title = "Sscan Plotting",
            };

            // Add axis
            axis = new LinearColorAxis()
            {
                Palette = OxyPalettes.Jet(200),
                //Minimum = 0,
                //Maximum = 100,
            };
            plotModel.Axes.Add(axis);

            plotData = new double[3, 3]
            {
                { 1.0, double.NaN, 3.0 },
                { double.NaN, 5.0, 6.0 },
                { 7.0, 8.0, 9.0 }
            };

            // Add series
            heatMapSeries = new HeatMapSeries
            {
                X0 = sscanGraph.XAxis.Min,
                X1 = sscanGraph.XAxis.Max,
                Y0 = sscanGraph.YAxis.Min,
                Y1 = sscanGraph.YAxis.Max,
                Interpolate = true,
                RenderMethod = HeatMapRenderMethod.Bitmap,
                Data = plotData,

                //Data = testdata(),
            };
            plotModel.Series.Add(heatMapSeries);

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
        int a = 0;
        public void RenderCScan(Graphics g, Size s)
        {
            var rc = new GraphicsRenderContext(g);

            ((IPlotModel)plotModel).Render(rc, new OxyRect(0, 0, s.Width, s.Height));
            PlotView.Invalidate(true);
            
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
                heatMapSeries.IsVisible=true;   
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
                        X = new PlotLength(0, PlotLengthUnit.Data),
                        Y = new PlotLength(0, PlotLengthUnit.Data),
                        Width = new PlotLength(PlotView.Width, PlotLengthUnit.Data),
                        Height = new PlotLength(PlotView.Height, PlotLengthUnit.Data),
                        //HorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                        //VerticalAlignment = VerticalAlignment.Top
                        // 크기 및 위치 설정
                    };

                    plotModel.Annotations.Add(imageAnnotation);
                }
                imageAnnotation.ImageSource = oxyImage;

            }
            plotModel.InvalidatePlot(true);
            //heatMapSeries.IsVisible = true;
            imageAnnotation.EnsureAxes();

            

            // 1. 기존 인스턴스의 데이터 소스만 변경 (새로운 인스턴스 생성 X)
            // OxyImage는 불변 객체이므로 데이터가 바뀔 때마다 new OxyImage는 필요하지만,
            // Annotation 자체는 기존 것을 그대로 재사용합니다.
            

        }
        public void initcscan2()
        {
            plotModel = new PlotModel { Title = "Annotation Only" };

            // 1. Manually define axes to create a coordinate system
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

            // 2. Add the TextAnnotation
            //plotModel.Annotations.Add(new TextAnnotation
            //{
            //    Text = "Standalone Label",
            //    TextPosition = new DataPoint(50, 50), // Centered at (50, 50)
            //    FontSize = 14,
            //    TextColor = OxyColors.Black,
            //    StrokeThickness = 0,                 // Removes the border box

            //});

            // 3. Update the view
            PlotView.Model = plotModel;
            PlotView.InvalidatePlot(true);
        }
        public void InitCscanPlot()
        {

            //heatmapModel = new HeatmapModel(400, 16);
            //for (int i = 0; i < 400; i++)
            //{
            //    heatmapModel.PaintHeatMapPoint(new HeatmapPoint(i, 0, i%256));
            //}
            

            //heatmapGraph = heatmapModel.BitmapToImageSource();
            //plotModel = CreateHeatMapFromBitmapSource(heatmapGraph);


            plotModel = new PlotModel
            {
                Title = "Cscan Plotting",
            };
            var axis = new LinearColorAxis()
            {
                //Palette = OxyPalettes.Jet(200)
                //Palette = OxyPalettes.BlueWhiteRed(255),
                Palette = OxyPalettes.BlueWhiteRed(1),
                Maximum = 100,
                Minimum = 0,
                MinimumRange = 0.1,
                //Position = AxisPosition.Right
               
            };
            Palette = axis.Palette;
            plotModel.Axes.Add(axis);
            plotData = new double[1,1] { { 0} };
            //plotData = new double[2, 2] { { 0,0 }, { 0, 0 } };
            heatMapSeries = new HeatMapSeries
            {
                X0 = 0,
                X1 = 16,
                Y0 = 0,
                Y1 = 100,
                Interpolate = false,
                RenderMethod = HeatMapRenderMethod.Bitmap,
                //Data = plotData,
                //Data = plotData
                Data = new double[2,2]
            };


            //PlotView.Model = plotModel;

            plotModel = new PlotModel { Title = "Plot with Annotation Only" };

            // 1. Create a TextAnnotation
            var textAnnotation = new TextAnnotation
            {
                Text = "This is an annotation without a series.",
                TextPosition = new DataPoint(50, 50), // Position in data coordinates
                TextColor = OxyColors.Red,
                FontSize = 14,
                // StrokeThickness can be set to 0 if you only want text and no outline/stroke
            };

            // 2. Add the annotation to the PlotModel's Annotations collection
            plotModel.Annotations.Add(textAnnotation);

            // You can add other types of annotations similarly:
            var lineAnnotation = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = 30, // Y-value in data coordinates
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                Text = "Horizontal Line"
            };
            plotModel.Annotations.Add(lineAnnotation);
            //plotModel.Axes.Add(axis);
            PlotView.Model = plotModel;
            PlotView.InvalidatePlot(true);

            //heatMapSeries.IsVisible = false;
            //plotModel.Series.Add(heatMapSeries);
            //PlotView.InvalidatePlot(false);
            //PlotView.InvalidatePlot(false);
            //PlotView.InvalidatePlot(true);

            //if (imageAnnotation == null)
            //{
            //    // 3. ImageAnnotation 등을 통해 모델에 추가
            //    imageAnnotation = new OxyPlot.Annotations.ImageAnnotation
            //    {
            //        //ImageSource = oxyImage,
            //        X = new PlotLength(0, PlotLengthUnit.Data),
            //        Y = new PlotLength(0, PlotLengthUnit.Data),
            //        Width = new PlotLength(PlotView.Width, PlotLengthUnit.Data),
            //        Height = new PlotLength(PlotView.Height, PlotLengthUnit.Data),
            //        //HorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
            //        //VerticalAlignment = VerticalAlignment.Top
            //        // 크기 및 위치 설정
            //    };

            //    plotModel.Annotations.Add(imageAnnotation);
            //}
            //imageAnnotation.EnsureAxes();
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
                Palette = OxyPalettes.BlueWhiteRed(100)
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
                X1 = 25,
                Y0 = 0,
                Y1 = 5000,
                Interpolate = true,
                RenderMethod = HeatMapRenderMethod.Bitmap,
                Data = plotData,
            };
            plotModel.Series.Add(heatMapSeries);


        }
    }

    public class AxisModel
    {
        public double Min { get; set; } // mm
        public double Max { get; set; } // mm
        public double Resolution { get; set; } // mm

        // For example, Min = -3 Max =3, Res = 1
        // We get points: (-3, -2, -1, 0, 1, 2, 3)
        public List<double> GetPoints()
        {
            List<double> points = new List<double>();
            int numberOfPoints = (int)Math.Floor((Max - Min) / Resolution) + 1;
            for (int i = 0; i < numberOfPoints; i++)
            {
                points.Add(Min + i * Resolution);
            }
            return points;
        }
    }
    public class SscanGraphModel
    {
        // Sscan Graph includes XAxis and YAxis
        public AxisModel XAxis { get; set; }
        public AxisModel YAxis { get; set; }

        // Get 2-d array plot points coordinates
        public PointD[,] GetPlotPoints()
        {
            var XPoints = XAxis.GetPoints();
            var YPoints = YAxis.GetPoints();

            PointD[,] points = new PointD[XPoints.Count, YPoints.Count];

            for (int i = 0; i < XPoints.Count; i++)
            {
                for (int j = 0; j < YPoints.Count; j++)
                {
                    points[i, j] = new PointD(XPoints[i], YPoints[j]);
                }
            }

            return points;
        }
    }


}
