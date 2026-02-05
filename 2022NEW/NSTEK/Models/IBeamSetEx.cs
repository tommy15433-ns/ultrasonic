using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OlympusNDT.Instrumentation.NET;

namespace _2022_Test.NSTEK.Models
{
    public static class BeamSetExCollection
    {
        public static List<BeamSetEx> List = new List<BeamSetEx>();
        public static int BeamsetsCount
        {
            get => List.Count;
        }
        public static int TotalNumberOfBeams
        {
            get => List.Select(x => x.BeamCount).Aggregate((cur, sum) => sum += cur);
        }

        public static void Add(BeamSetEx beamset) => List.Add(beamset);
        public static void Remove(BeamSetEx beamset) => List.Remove(beamset);
        public static void Clear(BeamSetEx beamset) => List.Clear();
        public static int NextIndex() => List.Any() ? List.Last().BeamStartIndex + List.Last().BeamCount : 0;
        public static BeamSetEx Get(int index) => List.Count > index ? List[index] : null;
        public static BeamSetEx Get(string name)
        {
            foreach (var item in List)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        public static string[] GetNames() => List.Select(x => x.Name).ToArray();
        
    }
    public class BeamSetEx
    {
        private string name = "";
        public string Name { get => name; }
        public IBeamSet BeamSetPtr;
        public int BeamStartIndex = 0;
        public int BeamCount = 0;
        public double AngleStart = 0.0;
        public double AngleResolution = 0.0;
        /// <summary>
        /// mm per sample
        /// </summary>
        public double Resolution = 1.0;
        /// <summary>
        /// m/s
        /// </summary>
        public double Velocity = 3240.0;
        /// <summary>
        /// Visible range in mm
        /// X = Range start
        /// Y = Range End
        /// </summary>
        public PointD VisibleRange = new PointD()
        {
            X = 0,
            Y = 100.0
        };
        public int Compression = 1;

        public int Ascanlength = 20000;

        public BeamSetEx(string _name)
        {
            name = _name;
        }
        public void UpdateRange(double rangeStart, double rangeEnd, double sampleFreq, double velocity, int compression)
        {
            VisibleRange.X = rangeStart;
            VisibleRange.Y = rangeEnd;
            Velocity = velocity;
            Compression = compression;

            Resolution = velocity * compression * 1000 / sampleFreq / 2;    // multiply by 1000 to convert it to mm
        }
        /// <summary>
        /// call after UpdateRange()
        /// </summary>
        /// <returns></returns>
        public int CalculateAScanStart_ns()
        {
            return (int)(VisibleRange.X * 2 / Velocity * 1000000.0); // [mm] to [ns]

            int calcAscanLength = (int)(VisibleRange.X * Compression * 2 / Resolution * 1000.0);   // ascan is in [ns]. so multiply mm by 1000

            return calcAscanLength;
        }
        public int CalculateAScanEnd_ns()
        {
            return (int)((VisibleRange.X + VisibleRange.Y) * 2 / (Velocity) * 1000000.0); // [mm] to [ns]

            int calcAscanLength = (int)(VisibleRange.Y / Resolution * 1000.0);   // ascan is in [ns]. so multiply mm by 1000

            return calcAscanLength;
        }
        public void UpdateActualAScanLength(int _ascanLength)
        {
            Ascanlength = _ascanLength;
        }
    }
}
