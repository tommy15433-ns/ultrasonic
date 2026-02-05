
using System;
using System.CodeDom;
using System.ComponentModel;
using System.Windows.Forms;
using OlympusNDT.Instrumentation.NET;

namespace _2022_Test
{
    public class BeamParam: NSTEK.Models.Model, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private double gain;
        private double ascanlength;
        private double ascanstart;

        private IAmplitudeSettings.AscanDataSize ascandatasize;
        private IAmplitudeSettings.RectificationType ascanrectificationtype;
        private IAmplitudeSettings.ScalingType ascanscalingtype;

        public double Gain
        {
            get => gain;
            set
            {
                if (gain != value)
                {
                    gain = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Gain)));
                    }
                }
            }
        }
        public double AscanLength
        {
            get => ascanlength;
            set
            {
                if (ascanlength != value)
                {
                    ascanlength = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AscanLength)));
                    }
                }
            }
        }
        public double AscanStart
        {
            get => ascanstart;
            set
            {
                if (ascanstart != value)
                {
                    ascanstart = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AscanStart)));
                    }
                }
            }
        }
        public IAmplitudeSettings.AscanDataSize AscanDataSize
        {
            get => ascandatasize;
            set
            {
                if (ascandatasize != value)
                {
                    ascandatasize = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AscanDataSize)));
                    }
                }
            }
        }
        public IAmplitudeSettings.RectificationType AscanRectificationType
        {
            get => ascanrectificationtype;
            set
            {
                if (ascanrectificationtype != value)
                {
                    ascanrectificationtype = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AscanRectificationType)));
                    }
                }
            }
        }
        public IAmplitudeSettings.ScalingType AscanScalingType
        {
            get => ascanscalingtype;
            set
            {
                if (ascanscalingtype != value)
                {
                    ascanscalingtype = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AscanScalingType)));
                    }
                }
            }
        }
    }

    public class ConfigUi<T> : Panel
    {
        private T val;
        public T Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                control.Text = val.ToString();
            }
        }
        public Label label;
        public virtual TextBox control { get; set; }
        public ConfigUi(string name, T initValue, TextBox ctrl)
        {
            label = new Label
            {
                Dock = DockStyle.Left,
                Text = name,
                Size = new System.Drawing.Size(100, 20),
                Location = new System.Drawing.Point(0, 0)
            };

            control = ctrl;
            ctrl.Dock = DockStyle.Right;
            ctrl.AutoSize = true;
            ctrl.Text = typeof(T).GetProperty(name).GetValue(initValue).ToString(); //initValue.ToString();
            ctrl.Size = new System.Drawing.Size(50, 20);
            ctrl.Location = new System.Drawing.Point(100, 0);
            

            this.Controls.Add(label);
            this.Controls.Add(control);
            this.AutoSize = false;

            this.Height = 20;
            this.Width = label.Width + control.Width;

            var binding = control.DataBindings;
            binding.Add(new Binding(nameof(TextBox.Text), initValue, name));
        }
    }

    public class BeamParamUi: Panel
    {
        public BeamParam Parameter;
        public int Id;


        private int cursor_y = 0;
        public BeamParamUi(int index, BeamParam param)
        {
            Id = index;
            Parameter = param;

            this.addUi(new Label
            {
                Text = $"ID {Id}"
            });
            //Control tmp = new ConfigUi<double>("gain", param.Gain, new TextBox());
            this.addUi(new ConfigUi<BeamParam>("Gain", param, new TextBox()));
            this.addUi(new ConfigUi<BeamParam>("AscanLength", param, new TextBox()));
            this.addUi(new ConfigUi<BeamParam>("AscanStart", param, new TextBox()));
        }

        private void addUi(Control c)
        {
            this.Controls.Add(c);
            c.Location = new System.Drawing.Point(0, cursor_y);
            cursor_y += 30;
            this.Height = cursor_y;
            if (this.Width < c.Width)
            {
                this.Width = c.Width + 30;
            }
            
        }
    }
}