using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2022_Test.NSTEK.Models
{
    public class WedgeModel : Model
    {
        private double angle = 36.1;
        private double firstElementHeight = 0.011;
        private double firstElementOffset = 0;
        private double velocity = 2330;

        public double Angle
        {
            get => angle;
            set
            {
                angle = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public double FirstElementHeight
        {
            get => firstElementHeight;
            set
            {
                firstElementHeight = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public double FirstElementOffset
        {
            get => firstElementOffset;
            set
            {
                firstElementOffset = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public double Velocity
        {
            get => velocity;
            set
            {
                velocity = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
