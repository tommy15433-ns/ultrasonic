using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace _2022_Test.NSTEK.Plot
{
    public class BScanRenderModel: IDisposable
    {
        public Brush[] BrushSets = null;

        private Bitmap _bitmap;
        public Bitmap Bmap
        {
            get { return _bitmap; }
        }
        private Graphics _graphics;
        public Graphics Graphic
        {
            get { return _graphics; }
        }
        public SizeF scale { get; set; }


        private double startAngle = 0.0;
        public double StartAngle
        {
            get => startAngle;
            set
            {
                startAngle = value;
                update_param();
            }
        }
        private double angleResolution = 1.0;
        public double AngleResolution
        {
            get => angleResolution;
            set
            {
                angleResolution = value;
                update_param();
            }
        }
        private double gateStart;
        public double GateStart
        {
            get => gateStart;
            set
            {
                gateStart = value;
                update_param();
            }
        }
        private double gateLength;
        public double GateLength
        {
            get => gateLength;
            set
            {
                gateLength = value;
                update_param();
            }
        }
        private RectangleF rect_gate;
        private Size bitmap_size;


        private Size datasize;
        private float pivot = 0.0f;

        private double RAD(double theta)
        {
            return Math.PI * theta / 180.0;
        }
        private bool fixed_scale = false;
        private void update_param()
        {
            int min = (int)Math.Ceiling(datasize.Height * Math.Sin(RAD(startAngle)));
            int max = (int)Math.Ceiling(datasize.Height * Math.Sin(RAD(startAngle + (angleResolution * (datasize.Width - 1)))));

            double endAngle = startAngle + (angleResolution * (datasize.Width - (datasize.Height - 1)));

            double height_search = 0;
            for (int i = 0; i < datasize.Width; i++)
            {
                double curangle = startAngle + (AngleResolution * i);
                double h = datasize.Height * Math.Cos(RAD(curangle));

                if (h > height_search)
                {
                    height_search = h;
                }
            }

            int new_height = (int)Math.Ceiling(height_search);

            int new_width = 0;
            int new_center = 0;
            if (Math.Sign(min) == Math.Sign(max))
            {
                new_width = Math.Max(Math.Abs(min), Math.Abs(max));
            }
            else
            {
                new_width = Math.Abs(max - min);
            }
            if (min < 0 && max < 0)
            {
                new_center = new_width;
            }
            else if (min < 0 && max >= 0)
            {
                new_center = Math.Abs(min);
            }
            else
            {
                new_center = 0;
            }

            pivot = new_center;
            //datasize.Width = new_width;
            if (fixed_scale == false)
            {
                _bitmap = new Bitmap(new_width, new_height, PixelFormat.Format32bppArgb);

                // _graphics references _bitmap
                _graphics = Graphics.FromImage(_bitmap);
            }

            rect_gate = new RectangleF(0, (float)gateStart, new_width, (float)gateLength);
            bitmap_size = new Size(_bitmap.Width, _bitmap.Height);
        }
        public BScanRenderModel(int width, int height, float scalex = 1.0f, float scaley = 1.0f)
        {
            datasize = new Size(width, height);


            _bitmap = new Bitmap((int)Math.Ceiling(width * scalex), (int)Math.Ceiling(height * scaley), PixelFormat.Format32bppArgb);

            _graphics = Graphics.FromImage(_bitmap);
            scale = new SizeF(scalex, scaley);

            BrushSets = ColorPalette.BeamBrushes1;
        }
        public BScanRenderModel(int width, int height)
        {
            // deprecated
            fixed_scale = true;
            int pix = 96, piy = 1024;

            datasize = new Size(width, height);

            _bitmap = new Bitmap(pix, piy, PixelFormat.Format32bppArgb);

            // _graphics references _bitmap
            _graphics = Graphics.FromImage(_bitmap);
            scale = new SizeF((float)pix / (float)width, (float)piy / (float)height);

            BrushSets = ColorPalette.BeamBrushes1;
        }
        public BitmapImage BitmapToImageSource()
        {
            using (MemoryStream memory = new MemoryStream())
            {
                _bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                bitmapimage.Freeze();
                return bitmapimage;
            }
        }
        public void PaintHeatMapPoint(PolygonF poly, int brushidx, bool en_scale = true)
        {
            //SolidBrush brush = new SolidBrush(colorList[brushidx]);
            if (en_scale)
            {
                poly.SetScale(scale.Width, scale.Height);
            }
            _graphics.FillPolygon(BrushSets[brushidx % BrushSets.Length] , poly.Points);
        }
        public void PaintHeatMapPoint(Graphics g, PolygonF poly, int brushidx, bool en_scale = true)
        {
            //SolidBrush brush = new SolidBrush(colorList[brushidx]);
            if (en_scale)
            {
                poly.SetScale(scale.Width, scale.Height);
            }
            g.FillPolygon(BrushSets[brushidx % BrushSets.Length], poly.Points);
        }
        public PointF cart2rad(double d, double deg_rad)
        {
            return new PointF((float)(d * Math.Sin(deg_rad)) + pivot, (float)(d * Math.Cos(deg_rad)));
        }
        private void updateGate()
        {
            Brush b = new SolidBrush(Color.FromArgb(128, 255, 0, 0));
            _graphics.FillRectangle(b, rect_gate);
        }
        private void updateGate(Graphics g)
        {
            Brush b = new SolidBrush(Color.FromArgb(32, 255, 0, 0));
            g.FillRectangle(b, rect_gate);
        }
        public void UpdateData(double[,] data)
        {
            if (data == null)
            {
                return;
            }

            // Marshal.Copy를 사용하여 배열 데이터를 한 번에 메모리로 전송 (매우 빠름)
            // Marshal.Copy(myDataArray, 0, bmpData.Scan0, myDataArray.Length);

            var nbm = new Bitmap(bitmap_size.Width, bitmap_size.Height);
            using (var g = Graphics.FromImage(nbm))
            {
                for (int i = 1; i < data.GetLength(0); i++)
                {
                    double theta0 = RAD(startAngle + (i - 1) * angleResolution);
                    double theta1 = RAD(startAngle + (i) * angleResolution);


                    for (int j = 1; j < data.GetLength(1); j++)
                    {

                        double d0 = (j - 1); // * lengthScale;
                        double d1 = (j); //  * lengthScale;

                        // polygon order
                        // top left, top right, bottom right, bottom left
                        PointF p0 = cart2rad(d0, theta0);
                        PointF p1 = cart2rad(d0, theta1);
                        PointF p2 = cart2rad(d1, theta0);
                        PointF p3 = cart2rad(d1, theta1);

                        // creating new instance instead of poly.update is faster
                        PolygonF poly = new PolygonF(p0, p1, p3, p2);

                        double avg = Math.Abs((data[i, j] + data[i - 1, j] + data[i, j - 1] + data[i - 1, j - 1]) / 4);

                        this.PaintHeatMapPoint(g, poly, (int)avg, true);
                    }
                    //return;
                }

                if (rect_gate != null)
                {
                    updateGate(g);
                }

                _bitmap.Dispose();
                _bitmap = nbm;
            }

            return;
            //_graphics.Clear(Color.LightGray);
            for (int i = 1; i < data.GetLength(0); i++)
            {
                double theta0 = RAD(startAngle + (i - 1) * angleResolution);
                double theta1 = RAD(startAngle + (i) * angleResolution);


                for (int j = 1; j < data.GetLength(1); j++)
                {

                    double d0 = (j - 1); // * lengthScale;
                    double d1 = (j); //  * lengthScale;

                    // polygon order
                    // top left, top right, bottom right, bottom left
                    PointF p0 = cart2rad(d0, theta0);
                    PointF p1 = cart2rad(d0, theta1);
                    PointF p2 = cart2rad(d1, theta0);
                    PointF p3 = cart2rad(d1, theta1);

                    // creating new instance instead of poly.update is faster
                    PolygonF poly = new PolygonF(p0, p1, p3, p2);

                    double avg = Math.Abs((data[i, j] + data[i - 1, j] + data[i, j - 1] + data[i - 1, j - 1]) / 4);

                    this.PaintHeatMapPoint(poly, (int)avg, true);
                }
                //return;
            }

            if (rect_gate != null)
            {
                updateGate();
            }

            //_bitmap.UnlockBits(bmpData); // 이 시점 전까지는 비트맵 데이터가 고정됨

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            try
            {
                _graphics.Dispose();
                _bitmap.Dispose();
            }
            catch
            {

            }
        }
    }

    public class PolygonF
    {
        public PointF[] Points = null;
        public PolygonF(params PointF[] points)
        {
            Points = points;
        }
        public PolygonF(int size)
        {
            Points = new PointF[size];
        }
        public void SetScale(float x, float y)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i].X *= x;
                Points[i].Y *= y;
            }
        }
        public void update(params PointF[] pointFs)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i].X = pointFs[i].X;
                Points[i].Y = pointFs[i].Y;
            }
        }
        public void Print()
        {
            foreach (PointF p in Points)
            {
                Debug.WriteLine($"{p.ToString()}");
            }
        }
        
    }
}
