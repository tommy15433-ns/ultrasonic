using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2022_Test.ProbeSettingForm;

namespace _2022_Test
{
    /*
      					        (probe2)	|      (probe3)
				             ____________	|   ____________	
    		_________________|	    		|		        |_______________
(probe1)	|					            |				                | (probe4)
	    	|					            |				                | 
    */


    public class Probe1: ProbeConfig_
    {
        public Probe1()
        {
            wedge.Enable = false;
            // steel velocity
            material.Velocity = 5890.0;
            focal.IsAngleRight = false;
        }
    }
    public class Probe2 : ProbeConfig_
    {
        public Probe2()
        {
            wedge.Enable = true;
            // steel shear velocity
            material.Velocity = 3240.0;
            focal.IsAngleRight = true;
        }
    }
    public class Probe3 : ProbeConfig_
    {
        public Probe3()
        {
            wedge.Enable = true;
            material.Velocity = 3240.0;
            focal.IsAngleRight = false;
        }
    }
    public class Probe4 : ProbeConfig_
    {
        public Probe4()
        {
            wedge.Enable = false;
            material.Velocity = 5890.0;
            focal.IsAngleRight = true;
        }
    }
}
