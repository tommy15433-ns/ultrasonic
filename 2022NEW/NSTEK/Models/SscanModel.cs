using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.Models
{
    public class SscanModel : Model
    {
        private double startAngle;
        public double StartAngle
        {
            get => startAngle;
            set
            {
                startAngle = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }// degree
        private double endAngle;
        public double EndAngle
        {
            get => endAngle;
            set
            {
                endAngle = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private double angleResolution;
        public double AngleResolution
        {
            get => angleResolution;
            set
            {
                angleResolution = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private double focusDepth;
        public double FocusDepth
        {
            get => focusDepth;
            set
            {
                focusDepth = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        } //mm
    }
}
