using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.Models
{
    public enum PulserVoltage
    {
        _4 = 0, _9, _20, _40, _80, _115
    };

    public class VoltageModel: Model
    {
        /*
        voltage[0]: 4
        voltage[1]: 9
        voltage[2]: 20
        voltage[3]: 40
        voltage[4]: 80
        voltage[5]: 115
        */

        private PulserVoltage voltageIndex = PulserVoltage._40;
        public PulserVoltage VoltageIndex
        {
            get { return voltageIndex; }
            set
            {
                voltageIndex = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
