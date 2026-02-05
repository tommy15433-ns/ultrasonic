using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.Models
{
    public class ScanRangeModel: Model
    {
        private double rangeStart_mm = 0.0;

        public double RangeStart_mm
        {
            get => rangeStart_mm;
            set
            {
                rangeStart_mm = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private uint rangeEnd_mm = 130;
        public uint RangeEnd_mm
        {
            get => rangeEnd_mm;
            set
            {
                rangeEnd_mm = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
