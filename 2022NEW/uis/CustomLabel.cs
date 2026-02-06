using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2022_Test.uis
{
    public class CustomLabel: Label
    {
        public CustomLabel() : base()
        {
            this.Font = new System.Drawing.Font("Malgun Gothic", 11.25f, System.Drawing.FontStyle.Bold);
            this.BackColor = Color.FromArgb(42, 89, 162);
            this.ForeColor = Color.White;
        }

        public CustomLabel(string text = "") : this()
        {
            this.Text = text;
        }
    }
}
