using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Test.NSTEK.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MeasuredUnitAttribute : Attribute
    {
        public string Unit { get; }
        public MeasuredUnitAttribute(string unit) => Unit = unit;
    }
}
