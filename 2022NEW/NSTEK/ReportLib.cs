using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library
{
    public class ReportLib
    {
        public ReportLib()
        {

        }

   




        private static void LineHeight(StreamWriter sw, int iHeight)
        {
            sw.WriteLine("<table>");
            sw.WriteLine("<tr>");
            sw.WriteLine("<td style=\"height:" + iHeight.ToString() + "\"> </td>");
            sw.WriteLine("</tr>");
            sw.WriteLine("</table>");
        }

    }
}
