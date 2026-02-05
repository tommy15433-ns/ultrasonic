using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OlympusNDT.Instrumentation.NET;

namespace WindowsFormsApp1.Device
{
    public class CScanData
    {
        public double Time;
        public IDataRange Range;
        public double CrossingTIme;
        public bool Detection;
        public CScanData(double time, IDataRange range, double crossingTime, bool detection) 
        {
            Time = time;
            Range = range;
            CrossingTIme = crossingTime;
            Detection = detection;
        }
        public override string ToString()
        {
            string tmp = $"{Range.GetFloatingMax().ToString()} {Range.GetFloatingMin().ToString()} {Range.GetMax().ToString()} {Range.GetMin().ToString()} {Range.GetUnit().ToString()} det: {Detection}";

            return $"Time: {Time} " + tmp + "ct: " + CrossingTIme.ToString();
        }
    }
}
