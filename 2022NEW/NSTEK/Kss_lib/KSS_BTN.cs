using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace KSS_Library
{
    public partial class KSS_BTN : Panel
    {
        private int opcity=255;

        bool mouse_enter = false;
        bool mouse_down = false;

        public int Opcity
        {
            get { return opcity; }
            set { opcity = value; }
        }

        private int borderWidth=1;

        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        private float gradientAngle=45.0f;

        public float GradientAngle
        {
            get { return gradientAngle; }
            set { gradientAngle = value; }
        }

        private int dia = 3;

        public int Dia
        {
            get { return dia; }
            set { dia = value; }
        }


        private Image img;

        public Image Img
        {

            get { return img; }
            set { img = value; }

        }

        private Color c1=Color.FromArgb(102, 114, 128);

        private Color c2 = Color.FromArgb(102, 114, 128);

        private Color borderColor=Color.White;

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value;  }
        }
        private Point _Label_P;

        public Point label_Point
        {
            get { return _Label_P; }
            set { _Label_P = value; }
        }
        private Point _Label_P2;

        public Point label_Point2
        {
            get { return _Label_P2; }
            set { _Label_P2 = value; }
        }
        private Size pic_size;

        public Size Pic_size
        {
            get { return pic_size; }
            set { pic_size = value; }

        }

        private bool pic_Center=true;

        public bool Pic_Center
        {
            get { return pic_Center; }
            set { pic_Center = value; }


        }


        private bool btn_MODE = true;

        public bool BTN_MODE
        {
            get { return btn_MODE; }
            set { btn_MODE = value; }


        }

        private bool label_Center=true;

        public bool Label_Center
        {
            get { return label_Center; }
            set { label_Center = value; }


        }

        private Point _Pic_P;

        public Point pic_P
        {
            get { return _Pic_P; }
            set { _Pic_P = value; }
        }
        private Color _panelColor=Color.FromArgb(102, 114, 128);

        public Color PanelColor
        {
            get { return _panelColor; }
            set { _panelColor = value; c1 = value; }
        }

        private Color _panelColor2 = Color.FromArgb(102, 114, 128);

        public Color PanelColor2
        {
            get { return _panelColor2; }
            set { _panelColor2 = value; c2 = value; }
        }

        private Color enter_panelColor = Color.FromArgb(132, 144, 158);

        public Color enter_PanelColor
        {
            get { return enter_panelColor; }
            set { enter_panelColor = value; }
        }

        private Color enter_panelColor2 = Color.FromArgb(132, 144, 158);

        public Color enter_PanelColor2
        {
            get { return enter_panelColor2; }
            set { enter_panelColor2 = value; }
        }

        private Color down_panelColor = Color.FromArgb(82, 94, 108);

        public Color down_PanelColor
        {
            get { return down_panelColor; }
            set { down_panelColor = value; }
        }

        private Color down_panelColor2 = Color.FromArgb(82, 94, 108);

        public Color down_PanelColor2
        {
            get { return down_panelColor2; }
            set { down_panelColor2 = value; }
        }


  


        private Color label2_color = Color.FromArgb(255, 255, 255);

        public Color Label2_color
        {
            get { return label2_color; }
            set { label2_color = value;  }
        }

        private Font label2_Font = KSS_BTN.DefaultFont;

        public Font Label2_Font
        {
            get { return label2_Font; }
            set { label2_Font = value; }
        }

        private string label_txt;

        public string Label_txt
        {

            get { return label_txt; }
            set { label_txt = value; }
        }

        private string label_txt2="";

        public string Label_txt2
        {

            get { return label_txt2; }
            set { label_txt2 = value; }
        }

        public KSS_BTN()
        {
            this.Opcity = 255;
            this.SetStyle(ControlStyles.UserPaint |
                            ControlStyles.AllPaintingInWmPaint |
                            ControlStyles.DoubleBuffer, true);
            this.BackColor = Color.Transparent;
            
        }

        static public GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = 2 * radius;
            Rectangle arcRect =
                new Rectangle(rect.Location, new Size(diameter, diameter));


            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);

            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();

            return path;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (btn_MODE)
            {
                base.OnMouseEnter(e);
                c1 = enter_panelColor;
                c2 = enter_panelColor2;
                mouse_enter = true;
                this.Invalidate();
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (btn_MODE)
            {
                if (mouse_enter&&mouse_down==false)
                {

                    base.OnMouseMove(e);
                    c1 = enter_panelColor;
                    c2 = enter_panelColor2;
                    mouse_enter = true;
                    this.Invalidate();
                }
                else if(mouse_enter&&mouse_down==true)
                {

                    base.OnMouseMove(e);
                    c1 = down_panelColor;
                    c2 = down_panelColor2;
                    this.Invalidate();

                }
            }
        }

        protected override void OnMove(EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (btn_MODE)
            {
                base.OnMouseLeave(e);
                c1 = _panelColor;
                c2 = _panelColor2;
                mouse_enter = false;
                this.Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (btn_MODE)
            {
                base.OnMouseDown(e);
                c1 = down_panelColor;
                c2 = down_panelColor2;
                mouse_down = true;
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (btn_MODE)
            {
                //if (mouse_enter)
                //{

                //    base.OnMouseUp(e);
                //    c1 = enter_panelColor;
                //    c2 = enter_panelColor2;
                //    mouse_enter = true;
                //    this.Invalidate();
                //}
                //else
                //{
                    base.OnMouseUp(e);
                    c1 = _panelColor;
                    c2 = _panelColor2;
                    this.Invalidate();
                    mouse_down = false;
                //}
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {



            KSS_BTN panel = this;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            int width = panel.ClientRectangle.Width;
            int height = panel.ClientRectangle.Height;

            Rectangle rect = new Rectangle(0, 0, width - 1, height - 1);

            Brush brush = new LinearGradientBrush(
                                           new Rectangle(0, 0, panel.ClientRectangle.Width, panel.ClientRectangle.Height),
                                           Color.FromArgb(opcity, c1),
                                           Color.FromArgb(opcity, c2),
                                           gradientAngle);

            using (GraphicsPath path = KSS_BTN.GetRoundedRectPath(rect, dia))
            {

                {
                    g.FillPath(brush, path);
                }

                g.FillPath(brush, path);
                Pen pen = new Pen(borderColor,borderWidth);
                if(borderWidth!=0)
                    g.DrawPath(pen, KSS_BTN.GetRoundedRectPath(new Rectangle((borderWidth) / 2, (borderWidth)/2, width - borderWidth, height - borderWidth), dia));
                  
                
            }

            if (label_Center)
            {
                g.DrawString(this.label_txt, this.Font, new SolidBrush(this.ForeColor), new PointF(label_Point.X, ((this.Height - 1 - this.Font.Size) / 2) - this.Font.Size/2+1));
            }
            else
            {
                g.DrawString(this.label_txt, this.Font, new SolidBrush(this.ForeColor), label_Point);
            }

            if (label_txt2 != "")
            {

                g.DrawString(this.label_txt2, label2_Font, new SolidBrush(label2_color), label_Point2);

            }
            if (img != null)
            {
                if (pic_Center)
                {
                    Rectangle rc = new Rectangle(new Point(pic_P.X, (this.Height  - this.pic_size.Height) / 2), pic_size);
                    g.DrawImage(this.img, rc);
                }
                else
                {
                    Rectangle rc = new Rectangle(pic_P, pic_size);
                    g.DrawImage(this.img, rc);

                }
            }
        }

    }
}
