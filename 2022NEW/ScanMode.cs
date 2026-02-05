using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2022_Test.NSTEK.Models;

namespace _2022_Test
{
    public class AScan
    {

    }
    public class SScan
    {

    }
    public class CScan : ScanMode
    {
        public ProbeModel Probe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public static class ScanModeAdaptor
    {
        public enum Mode{
            ascan, sscan, cscan
        };

        public static string Parse(Mode mode) => mode.ToString();

    }

    public interface ScanMode
    {
        ProbeModel Probe{ get; set; }

    }
}
