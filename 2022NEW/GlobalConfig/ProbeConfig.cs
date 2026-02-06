using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2022_Test.NSTEK.Models;

namespace _2022_Test.GlobalConfig
{
    public enum ProbeSel
    {
        Probe1, Probe2, Probe3, Probe4
    }

    public static class ProbeConfig
    {
        public static ProbeSel Prob { get; set; }

        public static Lazy<Dictionary<ProbeSel, ProbeModel>> Probes =
            new Lazy<Dictionary<ProbeSel, ProbeModel>>(() => loadDefaultSettings());


        private static Dictionary<ProbeSel, ProbeModel> loadDefaultSettings()
        {
            var ret = new Dictionary<ProbeSel, ProbeModel>();
            // initialize
            foreach (ProbeSel ps in Enum.GetValues(typeof(ProbeSel)))
            {
                ret[ps] = new ProbeModel();
            }

            return ret;
        }
        public static void SaveProbeConfigToFile(string filePath)
        {

        }
        public static void LoadProbeConfigFromFile(string filepath)
        {

        }
    }
}
