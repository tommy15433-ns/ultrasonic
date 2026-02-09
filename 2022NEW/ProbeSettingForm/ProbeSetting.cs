using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _2022_Test.NSTEK.Models;

namespace _2022_Test.ProbeSettingForm
{
    public class ProbeSetting
    {
        public const int ELEMENTS_PER_BEAM = 32;
        
        public enum Directions
        {
            left, right
        }

        /// <summary>
        /// true if beam angle is heading right, false on heading left
        /// </summary>
        public bool _beamDirectionRight = true;

        /// <summary>
        /// X-axis position of the probe
        /// </summary>
        public double _probePosition;

        public double _beamStartAngle;
        public double _beamEndAngle;
        public double _beamAngleResolution;
        public int _gain;
        public double _targetLength;
    }

    public class ProbeConfig_
    {
        public ProbeModel probe = new ProbeModel();
        public DigitizerModel digitizer = new DigitizerModel();
        public FocalModel focal = new FocalModel();
        public MaterialModel material = new MaterialModel();
        public VoltageModel voltage = new VoltageModel();
        public WedgeModel wedge = new WedgeModel();
        public FilterModel filter = new FilterModel();
    }
}
