using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2022_Test.NSTEK.Models;
using _2022_Test.ProbeSettingForm;

namespace _2022_Test.GlobalConfig
{
    public enum ProbeSel
    {
        Probe1, Probe2, Probe3, Probe4
    }

    public static class ProbeSettings
    {
        public static Dictionary<ProbeSel, ProbeConfig_> Probes = new Dictionary<ProbeSel, ProbeConfig_>
        {
            { ProbeSel.Probe1, new Probe1() },
            { ProbeSel.Probe2, new Probe2() },
            { ProbeSel.Probe3, new Probe3() },
            { ProbeSel.Probe4, new Probe4() },
        };

        public static void SaveProbeConfigs(string filepath)
        {

        }

    }

    //public static class ProbeConfig
    //{
    //    public static ProbeSel Prob { get; set; }

    //    public static Lazy<Dictionary<ProbeSel, ProbeModel>> Probes =
    //        new Lazy<Dictionary<ProbeSel, ProbeModel>>(() => loadDefaultSettings());


    //    private static Dictionary<ProbeSel, ProbeModel> loadDefaultSettings()
    //    {
    //        var ret = new Dictionary<ProbeSel, ProbeModel>();
    //        // initialize
    //        foreach (ProbeSel ps in Enum.GetValues(typeof(ProbeSel)))
    //        {
    //            ret[ps] = new ProbeModel();
    //        }

    //        return ret;
    //    }
    //    public static void SaveProbeConfigToFile(string filePath)
    //    {

    //    }
    //    public static void LoadProbeConfigFromFile(string filepath)
    //    {

    //    }
    //}
}
