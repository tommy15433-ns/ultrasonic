using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;


namespace _2022_Test.ProbeSettingForm
{
    public partial class ProbeSetting : ObservableObject
    {
        public enum Directions
        {
            left, right
        }

        /// <summary>
        /// true if beam angle is heading right, false on heading left
        /// </summary>
        [ObservableProperty]
        private bool _beamDirectionRight = true;

        /// <summary>
        /// X-axis position of the probe
        /// </summary>
        [ObservableProperty]
        private double _probePosition;

        [ObservableProperty]
        private double _beamStartAngle;
        [ObservableProperty]
        private double _beamEndAngle;
        [ObservableProperty]
        private double _beamAngleResolution;
        [ObservableProperty]
        private int _gain;

        [ObservableProperty]
        private string _userName; // 컴파일러가 'public string UserName'을 생성함
    }
}
