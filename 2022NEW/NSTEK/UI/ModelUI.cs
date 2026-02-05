using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using _2022_Test.NSTEK.Models;
using System.Windows.Forms;

namespace _2022_Test.NSTEK.UI
{
    public class ModelUI: GroupBox
    {
        private const int marginx = 10, marginy = 10;

        private Dictionary<string, Control> uis = new Dictionary<string, Control>();
        private Model m_model;
        
        public ModelUI(Model model)
        {
            m_model = model;
            model.ValueChanged += (s, e) =>
            {
                update();
            };
            initUis();
        }

        
        private void update()
        {
            foreach(string name in m_model.GetNames())
            {
                uis[name].Text = m_model.GetValue(name).ToString();
            }
            // unsub
            // update textboxes
            // sub
        }
        private void initUis()
        {
            const int init_marginx = 10, init_marginy = 10;

            const int padx = 5, pady = 5;

            System.Drawing.Point p = new System.Drawing.Point(padx, pady + 20);

            this.Size = new System.Drawing.Size(marginx, marginy);

            this.Text = m_model.GetModelName();

            foreach (string name in m_model.GetNames())
            {
                Label l = new Label();
                l.Text = $"{name} [{m_model.GetUnit(name)}]";

                Control instance = null;
                Type propType = m_model.GetTypeOf(name);
                if (propType.IsEnum)
                {
                    ComboBox control = new ComboBox();
                    control.Items.AddRange(propType.GetEnumNames());
                    //control.SelectedIndex = 0;
                    // preserve previous value
                    control.Text = m_model.GetValue(name).ToString();
                    control.DropDownStyle = ComboBoxStyle.DropDownList;
                    control.SelectedIndexChanged += (s, e) =>
                    {
                        m_model.ValueChanged = null;
                        m_model.SetValue(name, control.Text);
                        m_model.ValueChanged += (a, b) =>
                        {
                            update();
                        };
                    };
                    control.Size = new System.Drawing.Size(100, 20);
                    instance = control;
                    
                }
                else if (propType == typeof(Boolean))
                {
                    CheckBox cbox = new CheckBox();
                    cbox.Name = name;
                    cbox.Checked = (Boolean)m_model.GetValue(name);
                    cbox.CheckedChanged += (s, e) =>
                    {
                        m_model.ValueChanged = null;
                        m_model.SetValue(name, cbox.Checked);
                        m_model.ValueChanged += (a, b) =>
                        {
                            update();
                        };
                    };
                    cbox.Size = new System.Drawing.Size(50, 20);

                    instance = cbox;
                }
                else
                {
                    //using (var textbox = new System.Windows.Forms.TextBox())
                    //{
                    TextBox textbox = new TextBox();
                    //TextBox textBox = new TextBox();
                    textbox.Name = name;
                    textbox.Text = m_model.GetValue(name).ToString();
                    textbox.TextChanged += (s, e) =>
                    {
                        m_model.ValueChanged = null;
                        m_model.SetValue(name, textbox.Text);
                        m_model.ValueChanged += (a, b) =>
                        {
                            update();
                        };
                    };
                    textbox.Size = new System.Drawing.Size(100, 20);

                    instance = textbox;
                    //}
                }

                l.Size = new System.Drawing.Size(150, 20);
                //instance.Size = new System.Drawing.Size(50, 20);

                l.Location = p;
                p.X = l.Width+ padx;
                instance.Location = p;
                
                p.Y += Math.Max(l.Height, instance.Height) + pady;



                uis[name] = instance;

                this.Controls.Add(l);
                this.Controls.Add(instance);

                this.Height = p.Y + pady;
                this.Width = Math.Max(this.Width, p.X + instance.Width + padx);

                p.X = padx;
            }

            
        }
    }
}
