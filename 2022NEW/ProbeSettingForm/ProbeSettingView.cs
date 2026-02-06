using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;

namespace _2022_Test.ProbeSettingForm
{
    public partial class ProbeSettingView : UserControl
    {
        private BindingSource bindingSource = new BindingSource();
        public ProbeSettingView()
        {
            InitializeComponent();
        }
        public void BindProbeSetting(ProbeSetting probeSetting)
        {
            bindingSource.DataSource = typeof(ProbeSetting);
            this.tb_angle_end.DataBindings.Add("Text", probeSetting, "UserName", false, DataSourceUpdateMode.OnPropertyChanged);

        }
    }
}
