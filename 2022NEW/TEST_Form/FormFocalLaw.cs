using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _2022_Test.NSTEK;
using _2022_Test.NSTEK.Database;
using DB = _2022_Test.NSTEK.Database.Database;

namespace _2022_Test
{
    public partial class FormFocalLaw : Form
    {
        const int INIT_PY = 35;
        private int last_max_width = 5;
        private int px = 5;
        private int py = INIT_PY;

        private GroupBox cur_gb;
        public FormFocalLaw()
        {
            InitializeComponent();
            InitializeConfigUI();

            AutoSize = true;

        }

        private void InitializeConfigUI()
        {
            foreach(Control c in this.Controls)
            {
                this.Controls.Remove(c);
            }

            foreach(ProbConfig conf in  DB.ProbConfigs)
            {
                createNewColumn(conf.Name);

                CheckBox cb = new CheckBox();
                cb.Tag = conf.Name;
                cb.Checked = conf.Enable;
                cb.Text = $"Enable {conf.Name}";
                cb.AutoSize = true;

                addUiLine(cb);
                cb.CheckedChanged += (s, arg) =>
                {
                    conf.Enable = cb.Checked;
                };


                NSTEK.UI.ModelUI ui_p = new NSTEK.UI.ModelUI(conf.Probe);
                NSTEK.UI.ModelUI ui_f = new NSTEK.UI.ModelUI(conf.FocalLaw);
                NSTEK.UI.ModelUI ui_w = new NSTEK.UI.ModelUI(conf.Wedge);

                addUiLine(ui_p);
                addUiLine(ui_f);
                addUiLine(ui_w);
                //createNewColumn();
            }

            createNewColumn("Material");
            addUiLine(new NSTEK.UI.ModelUI(DB.Material));
        }
        private void addUiLine(Control ui)
        {
            this.cur_gb.Controls.Add(ui);
            ui.Location = new Point(5, py);

            py += ui.Height + INIT_PY;
            last_max_width = last_max_width < ui.Width ? ui.Width : last_max_width;
        }
        private void createNewColumn(string name = "probe")
        {
            py = INIT_PY;
            px += last_max_width + 15;
            cur_gb = new GroupBox();
            cur_gb.Text = name;
            this.Controls.Add(cur_gb);
            cur_gb.AutoSize = true;
            cur_gb.Location = new Point(px, py);
        }
    }
}
