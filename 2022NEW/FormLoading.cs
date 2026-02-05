using System;
using System.Drawing;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2022_Test
{
    internal class FormLoading: Form
    {
        private PictureBox loaderImage;

        public FormLoading(Action onStart, Func<bool> successCheck, Action onFinish)
        {
            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // 깔끔한 로딩창을 위해 테두리 제거

            loaderImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Image = Properties.Resources.loading_256,
                Visible = false
            };

            this.Controls.Add(loaderImage);


            StartLoading(onStart, successCheck, onFinish);
        }

        // successCheck: 성공 여부를 반환하는 콜백 추가
        public async void StartLoading(Action onStart, Func<bool> successCheck, Action onFinish)
        {
            // 1. 시작 콜백 및 이미지 활성화
            onStart?.Invoke();
            loaderImage.Visible = true;

            // 2. 비동기 작업 수행 (예: 데이터 로드)
            bool isSuccess = false;
            await Task.Run(() =>
            {
                // 실제 로직 시뮬레이션
                System.Threading.Thread.Sleep(2000);

                // 전달받은 성공 여부 체크 콜백 실행
                isSuccess = successCheck?.Invoke() ?? false;
            });

            // 3. 결과가 true일 때만 종료 로직 실행
            if (isSuccess)
            {
                loaderImage.Visible = false;
                onFinish?.Invoke();

                // 새 폼 시작 및 현재 폼 닫기
                MainForm mainApp = new MainForm();
                mainApp.Show();
                this.Close();
            }
            else
            {
                // 실패 시 처리 (예: 메시지 박스 출력 후 종료)
                MessageBox.Show("데이터 로드에 실패했습니다.");
                Application.Exit();
            }
        }
    }
}
