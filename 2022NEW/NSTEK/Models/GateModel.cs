using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.Models
{
    public enum CScanType
    {
        ToF,
        Peak
    };
    public class GateModel : Model
    {
        private double start = 90;
        private double length = 30;
        private int threshold = 25;
        public CScanType scanType = CScanType.ToF;

        [MeasuredUnit("mm")]
        public double Start
        {
            get => start;
            set
            {
                start = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        [MeasuredUnit("mm")]
        public double Length
        {
            get => length;
            set
            {
                length = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public int Threshold
        {
            get => threshold;
            set
            {
                threshold = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public CScanType ScanType
        {
            get => scanType;
            set
            {
                scanType = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
