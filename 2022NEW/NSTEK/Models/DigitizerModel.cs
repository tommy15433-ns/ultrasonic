using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlympusNDT.Instrumentation.NET;
using static OlympusNDT.Instrumentation.NET.ITimeSettings;

namespace _2022_Test.NSTEK.Models
{
    public class DigitizerModel: Model
    {
        /// <summary>
        /// Focus PX default sampling rate 100HMz
        /// </summary>
        public const int DefaultSamplingRate = 100000000;
        /// <summary>
        /// Get the compression factor. This parameter, with the digitizing frequency, influence the AScan resolution, 
        /// i.e. how many nanoseconds each data points covers.
        /// </summary>
        private int compression = 10;
        public int Compression
        {
            get => compression;
            set
            {
                compression = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Get the sampling decimation factor: One, Two, Four, Eight, Sixteen, ThirtyTwo, SixtyFour.
        /// The sampling decimation factor divides the 100Mhz base frequency to set the digitizing frequency.
        /// The digitalizing frequency controls the rate at which data is converted from analog to digital.
        /// A higher frequency means that less time separates each data point.
        /// The digitizing frequency and the compression factor directly impact the AScan resolution.
        /// </summary>
        SamplingDecimationFactor samplingFactor = SamplingDecimationFactor.One;
        public SamplingDecimationFactor SamplingFactor
        {
            get => samplingFactor;
            set
            {
                samplingFactor = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        IAmplitudeSettings.AscanDataSize ascanDataSize = IAmplitudeSettings.AscanDataSize.EightBits;
        public IAmplitudeSettings.AscanDataSize AscanDataSize
        {
            get => ascanDataSize;
            set
            {
                ascanDataSize = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Set the rectification of the AScan : None, Positive, Negative or Full. 
        /// Unipolars show only the positive or negative side of the AScan while Full shows both sides, 
        /// but with the negative side in the positive, this is also called a rectified AScan.
        /// </summary>
        private IAmplitudeSettings.RectificationType rectificatioNType = IAmplitudeSettings.RectificationType.Positive;
        public IAmplitudeSettings.RectificationType RectificatioNType
        {
            get => rectificatioNType;
            set
            {
                rectificatioNType = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private IAmplitudeSettings.ScalingType scalingType = IAmplitudeSettings.ScalingType.Linear;
        public IAmplitudeSettings.ScalingType ScalingType
        {
            get => scalingType;
            set
            {
                scalingType = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private double ascanStart = 0;
        public double AscanStart
        {
            get => ascanStart;
            set
            {
                ascanStart = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private double ascanLength = 50000;
        public double AscanLength
        {
            get => ascanLength;
            set
            {
                ascanLength = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private double gain = 35;
        public double Gain
        {
            get => gain;
            set
            {
                gain = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public double mm_per_sample(double velocity) => 1000 * compression * velocity / 2 / (DefaultSamplingRate / (int)samplingFactor);

    }
}
