using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  _2022_Test.NSTEK.Models
{
    public class FocalModel : Model
    {
        private double focusLength = 0.03;
        public double FocusLength
        {
            get => focusLength;
            set
            {
                focusLength = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            } 
        }
        private double angleStart = 0.0;
        public double AngleStart
        {
            get => angleStart;
            set
            {
                angleStart = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private double angleStop = 45.0;
        public double AngleStop
        {
            get => angleStop;
            set
            {
                angleStop = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private double angleResolution = 1.0;
        public double AngleResolution
        {
            get => angleResolution;
            set
            {
                angleResolution = value;
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }

    }
}
