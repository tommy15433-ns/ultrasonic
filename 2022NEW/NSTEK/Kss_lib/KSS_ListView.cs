using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KSS_Library
{
    public partial class KSS_ListView : ListView
    {
        [DllImport("uxtheme", CharSet = CharSet.Auto)]

        static extern Boolean SetWindowTheme(IntPtr hWindow, String subAppName, String subIDList);

        private bool transparent = false;

        public bool Transparent
        {
            get { return transparent; }
            set { transparent = value; }


        }
        public KSS_ListView()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            // this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.UserPaint, true);

        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);


           // SetWindowTheme(Handle, "Explorer", null);
        }
        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);

            }

        }

        private void MakeTransparent(Control ctrl)
        {
            Bitmap bMap = new Bitmap(this.Parent.BackgroundImage);

            Color[,] pixelArray = new Color[ctrl.Width + ctrl.Location.X, ctrl.Height + ctrl.Location.Y];

            for (int i = ctrl.Location.X; i < ctrl.Width + ctrl.Location.X; i++)
            {
                for (int j = ctrl.Location.Y; j < ctrl.Height + ctrl.Location.Y; j++)
                {
                    pixelArray[i, j] = bMap.GetPixel(i, j);
                }
            }

            Bitmap bmp = new Bitmap(ctrl.Width + ctrl.Location.X, ctrl.Height + ctrl.Location.Y);

            for (int i = ctrl.Location.X; i < ctrl.Width + ctrl.Location.X; i++)
            {
                for (int j = ctrl.Location.Y; j < ctrl.Height + ctrl.Location.Y; j++)
                {
                    bmp.SetPixel(i - ctrl.Location.X, j - ctrl.Location.Y, pixelArray[i, j]);
                }
            }

            ctrl.BackgroundImage = bmp;

            ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y);
        }
        protected override void InitLayout()
        {
            base.InitLayout();
            if (transparent)
                MakeTransparent(this);
        }





    }
}
