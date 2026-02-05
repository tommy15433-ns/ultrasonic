using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2022_Test.NSTEK.Models;

namespace _2022_Test.NSTEK.Database
{
 
    public static class Database
    {
        public static int ProbCount
        {
            get => ProbConfigs.Count;
        }

        public static List<ProbConfig> ProbConfigs = new List<ProbConfig>();
        public static Models.MaterialModel Material = new Models.MaterialModel();

        public static string ParseProbeName(int rayIndex)
        {
            int pos = 0;
            foreach (ProbConfig cf in ProbConfigs)
            {
                // each probe has ray size of (elements per beam * ( |angle start - angle end| + 1))

                int size_of_probe_beam = (int)cf.Probe.UsedElementsPerBeam * (int)Math.Abs(cf.FocalLaw.AngleStart - cf.FocalLaw.AngleStop) + 1;
                pos += size_of_probe_beam;

                if (rayIndex < pos)
                {
                    return cf.Name;
                }
            }

            return null;
        }
        public static ProbeModel ParseProbe(string name)
        {
            return ProbConfigs.Where(x =>  x.Name == name).FirstOrDefault().Probe;
        }
        public static string[] ProbeNames()
        {
            return ProbConfigs.Select(x => x.Name).ToArray();
        }
    }


    public class ProbConfig
    {
        public string Name = "";
        public bool Enable
        {
            get;set;
        }
        public Models.ProbeModel Probe = new Models.ProbeModel();
        public Models.FocalModel FocalLaw = new Models.FocalModel();
        public Models.WedgeModel Wedge = new Models.WedgeModel();

        public ProbConfig()
        {
            Enable = false;
        }
        public ProbConfig(string name)
        {
            Name = name;
        }
    
    }
}
