using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.Models
{
    public class MaterialModel : Model
    {
        /// <summary>
        /// m/s
        /// </summary>
        private double velocity = 5890;
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
