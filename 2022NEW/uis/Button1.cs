using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Security.RightsManagement;
using Sunny.UI;
using System.Runtime.CompilerServices;

namespace _2022_Test.uis
{
    
    public class Button1: CustomButton
    {
        private Color[] _state_colors = new Color[]
        {
            Color.Gainsboro,
            Color.LemonChiffon
        };
        public override StateType Status {
            get => base.Status; 
            set
            {
                this.BackColor = _state_colors[(int)value];
                base.Status = value; 
            }
        }
        public Button1(): base()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.Font = new Font("Dotum", 9.75f, FontStyle.Bold);
            this.Size = new Size(50, 20);
            this.BackColor = _state_colors[(int)StateType.Released];
            this.Text = "buttonname";
        }
        public Button1(string name) : this()
        {
            this.Text = name;
        }
        public Button1(string name, Color color_pressed, Color color_released) : this(name)
        {
            _state_colors[(int)StateType.Released] = color_released;
            _state_colors[(int)StateType.Pressed] = color_pressed;
        }
    }
}
