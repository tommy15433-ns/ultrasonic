using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;

namespace _2022_Test
{

    public partial class LoadingForm : Form
    {
        private Func<bool> _work;
        public bool Result { get; private set; }
        private PictureBox loaderImage;

        public LoadingForm(Func<bool> work)
        {
            // InitializeComponent(); // 디자인 파일에서 호출
            _work = work;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false; // 닫기 버튼 비활성화

            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // 깔끔한 로딩창을 위해 테두리 제거


            loaderImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Image = Properties.Resources.loading_256,
                Visible = true
            };
            this.Controls.Add(loaderImage);
        }
        public LoadingForm()
        {
            // InitializeComponent(); // 디자인 파일에서 호출
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false; // 닫기 버튼 비활성화

            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // 깔끔한 로딩창을 위해 테두리 제거


            loaderImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Image = Properties.Resources.loading_256,
                Visible = true
            };
            this.Controls.Add(loaderImage);
        }

        protected override async void OnShown(EventArgs e)
        {
            //base.OnShown(e);

            //// 비동기로 작업 수행
            //Result = await Task.Run(() => _work());

            //// 작업 완료 후 폼 닫기
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }
    }



}
