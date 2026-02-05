using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlympusNDT.Instrumentation.NET;

namespace _2022_Test.NSTEK.Models
{
    public enum DigitalBandPassFilter
    {
        none_17M_1M = 0,
        none_12M_600K,
        low_2M_600K,
        low_4M_600K,
        low_10M_600K,
        band_3M_1M,
        band_6M_2M,
        band_8M_2M,
        band_12M_4M,
        band_16M_5M,
        band_17M_6M,
        high_17M_4M,
        high_17M_6M,
        high_17M_8M,
        high_17M_10M
    }

    /// <summary>
    /// The desired smoothing filter in MHz. 
    /// </summary>
    [Description("The filter set should have a frequency that is closest to the frequency of the binded probe.")]
    public enum SmoothingFilter
    {
        _1 = 0, _1_5, _2, _2_25, _4, _5, _7_5, _10, _12, _15, _20
    }
    public class FilterModel: Model
    {
        /* Digital band pass filter
         * 
         * focuspx.ultrasoundConfiguration.GetDigitizerTechnology(UltrasoundTechnology.PhasedArray).GetDigitalBandPassFilterCollection().GetDigitalBandPassFilter(i)
         * 
        filter[0]: type: None char: None high cutoff: 17800000low cutoff: 1000000
        filter[1]: type: None char: None high cutoff: 12200000low cutoff: 600000
        filter[2]: type: LowPass char: None high cutoff: 2000000low cutoff: 600000
        filter[3]: type: LowPass char: None high cutoff: 4000000low cutoff: 600000
        filter[4]: type: LowPass char: None high cutoff: 10000000low cutoff: 600000
        filter[5]: type: BandPass char: None high cutoff: 3500000low cutoff: 1000000
        filter[6]: type: BandPass char: None high cutoff: 6500000low cutoff: 2000000
        filter[7]: type: BandPass char: None high cutoff: 8000000low cutoff: 2500000
        filter[8]: type: BandPass char: None high cutoff: 12000000low cutoff: 4000000
        filter[9]: type: BandPass char: None high cutoff: 16000000low cutoff: 5000000
        filter[10]: type: BandPass char: None high cutoff: 17800000low cutoff: 6000000
        filter[11]: type: HighPass char: None high cutoff: 17800000low cutoff: 4000000
        filter[12]: type: HighPass char: None high cutoff: 17800000low cutoff: 6000000
        filter[13]: type: HighPass char: None high cutoff: 17800000low cutoff: 8000000
        filter[14]: type: HighPass char: None high cutoff: 17800000low cutoff: 10000000
         */

        /* Smoothing Filter
         * focuspx.ultrasoundConfiguration.GetDigitizerTechnology(UltrasoundTechnology.PhasedArray).GetSmoothingFilterCollection().GetSmoothingFilter(i)
            Smoothing filter[0] - 1
            Smoothing filter[1] - 1.5
            Smoothing filter[2] - 2
            Smoothing filter[3] - 2.25
            Smoothing filter[4] - 4
            Smoothing filter[5] - 5
            Smoothing filter[6] - 7.5
            Smoothing filter[7] - 10
            Smoothing filter[8] - 12
            Smoothing filter[9] - 15
            Smoothing filter[10] - 20
        */

        //IDigitalBandPassFilter digitalBandFilter = IDigitalBandPassFilter.

        private DigitalBandPassFilter filterIndex = 0;
        public DigitalBandPassFilter FilterIndex
        {
            get => filterIndex;
            set
            {
                filterIndex = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private bool smoothingFilter = false;
        public bool SmoothingFilter
        {
            get => smoothingFilter;
            set
            {
                smoothingFilter = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private SmoothingFilter smoothingFilterIndex = 0;
        public SmoothingFilter SmoothingFilterIndex
        {
            get => smoothingFilterIndex;
            set
            {
                smoothingFilterIndex = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
