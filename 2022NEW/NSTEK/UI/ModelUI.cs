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

        private Dictionary<string, TextBox> uis = new Dictionary<string, TextBox>();
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
                l.Text = name;
                TextBox textBox = new TextBox();
                textBox.Name = name;
                textBox.Text = m_model.GetValue(name).ToString();

                l.Size = new System.Drawing.Size(150, 20);
                textBox.Size = new System.Drawing.Size(50, 20);

                l.Location = p;
                p.X = l.Width+ padx;
                textBox.Location = p;
                
                p.Y += Math.Max(l.Height, textBox.Height) + pady;

                textBox.TextChanged += (s, e) =>
                {
                    m_model.ValueChanged = null;
                    m_model.SetValue(name, textBox.Text);
                    m_model.ValueChanged += (a, b) =>
                    {
                        update();
                    };
                };

                uis[name] = textBox;

                this.Controls.Add(l);
                this.Controls.Add(textBox);

                this.Height = p.Y + pady;
                this.Width = Math.Max(this.Width, p.X + textBox.Width + padx);

                p.X = padx;
            }

            
        }
    }
}
