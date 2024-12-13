using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Library
{
    class IniReadWrite
    {
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            String section,
            String key,
            String def,
            StringBuilder retVal,
            int size,
            String filePath);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(
            String section,
            String key,
            String val,
            String filePath);

        public static void IniWriteValue(
            String Section,
            String Key,
            String Value,
            String avsPath)
        {
            WritePrivateProfileString(Section, Key, Value, avsPath);
        }

        public static String IniReadValue(
            String Section,
            String Key,
            String avsPath)
        {
            StringBuilder temp = new StringBuilder(2000);
            int i = GetPrivateProfileString(Section, Key, "", temp, 2000, avsPath);
            return temp.ToString();
        }
    }
}
