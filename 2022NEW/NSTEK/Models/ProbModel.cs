using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.Models
{
    public class ProbeModel : Model
    {
        /// <summary>
        /// 
        /// </summary>
        private uint totalElement = 32;
        public uint TotalElements
        {
            get => totalElement;
            set
            {
                totalElement = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private uint elementPerBeam = 32;
        public uint ElementPerBeam
        {
            get => elementPerBeam;
            set
            {
                elementPerBeam = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// mm
        /// </summary>
        private double pitch = 0.001;
        public double Pitch 
        {
            get => pitch;
            set
            {
                pitch = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// MHz
        /// </summary>
        private double frequency = 5;
        public double Frequency 
        {
            get => frequency;
            set
            {
                frequency = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
