using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_Test.uis
{
    public class ButtonImage : CustomButton
    {
        public override StateType Status 
        { 
            get => base.Status; 
            set
            {
                this.BackgroundImage = status_image[(int)value];
                base.Status = value;
            }
        }
        public override string Text {
            get => "";
            set => base.Text = "";
        }
        private Image[] status_image =
        {
            Properties.Resources.good,
            Properties.Resources.ng
        };
        public Image ImagePressed
        {
            get => status_image[(int)StateType.Pressed];
            set
            {
                status_image[(int)StateType.Pressed] = value;
            }
        }
        public Image ImageReleased
        {
            get => status_image[(int)StateType.Released];
            set
            {
                status_image[(int)StateType.Released] = value;
            }
        }
        public ButtonImage(): base()
        {
            this.Text = "";
            this.BackgroundImage = status_image[(int)StateType.Released];
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }
        public ButtonImage(Image img_pressed, Image img_released): this()
        {
            status_image[(int)StateType.Released] = img_released;
            status_image[(int)StateType.Pressed] = img_pressed;
        }
    }
}
