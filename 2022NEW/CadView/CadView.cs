//using System;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;
//using netDxf;
//using netDxf.Entities;


//namespace _2022_Test.CadView
//{
//    public class CadView
//    {
//        Panel _parent;
//        private DxfDocument _dxfDoc;
//        private Matrix _transformMatrix = new Matrix();
//        private bool _isLoaded = false;
//        public CadView(Panel parent)
//        {
//            _parent = parent;
//            _parent.Paint += _parent_Paint;
//        }


//        public void Display(string filepath)
//        {
//            if (!File.Exists(filepath))
//            {
//                return;
//            }

//            _dxfDoc = DxfDocument.Load(filepath);
//            CalculateTransform();
//            _isLoaded = true;
//            _parent.Invalidate();
//        }
//        private void CalculateTransform()
//        {
//            if (_dxfDoc == null || _parent.Width == 0 || _parent.Height == 0) return;

//            // 1. 모든 엔티티를 포함하는 경계값 계산 (최초 1회 또는 리사이즈 시에만)
//            // netDxf의 GetBounds() 또는 직접 추출
//            var minX = (float)_dxfDoc.Entities.Lines.Min(l => Math.Min(l.StartPoint.X, l.EndPoint.X));
//            var maxX = (float)_dxfDoc.Entities.Lines.Max(l => Math.Max(l.StartPoint.X, l.EndPoint.X));
//            var minY = (float)_dxfDoc.Entities.Lines.Min(l => Math.Min(l.StartPoint.Y, l.EndPoint.Y));
//            var maxY = (float)_dxfDoc.Entities.Lines.Max(l => Math.Max(l.StartPoint.Y, l.EndPoint.Y));

//            float dxfWidth = maxX - minX;
//            float dxfHeight = maxY - minY;

//            // 2. 스케일 계산 (90% 크기로 맞춤)
//            float scale = Math.Min(_parent.Width * 0.9f / dxfWidth, _parent.Height * 0.9f / dxfHeight);

//            // 3. Matrix 객체에 변환 정보 저장 (이동 -> 스케일 -> Y축 반전)
//            _transformMatrix = new Matrix();

//            // 화면 중앙으로 이동
//            _transformMatrix.Translate(_parent.Width / 2f, _parent.Height / 2f);
//            // Y축 반전 및 스케일 적용
//            _transformMatrix.Scale(scale, -scale);
//            // DXF 모델 중심을 (0,0)으로 이동
//            _transformMatrix.Translate(-(minX + dxfWidth / 2f), -(minY + dxfHeight / 2f));
//        }
//        private void _parent_Paint(object sender, PaintEventArgs e)
//        {
//            if (_dxfDoc == null) return;
//            if (_isLoaded == false) return;

//            Graphics g = e.Graphics;
//            g.Transform = _transformMatrix;
//            g.Clear(Color.Black); // 배경색 설정
//            //Pen pen = new Pen(Color.White, 1);
//            float currentScale = _transformMatrix.Elements[0];
//            float penWidth = 1.0f / currentScale;
//            // 2. DXF의 Line 엔티티 반복 처리
//            using (Pen pen = new Pen(Color.White, penWidth))
//            {
//                // _dxfDoc.Entities의 모든 엔티티 순회
//                foreach (var entity in _dxfDoc.Entities.All)
//                {
//                    // 엔티티 타입별 분기 처리
//                    switch (entity.Type)
//                    {
//                        case EntityType.Line:
//                            var line = (netDxf.Entities.Line)entity;
//                            g.DrawLine(pen, (float)line.StartPoint.X, (float)line.StartPoint.Y,
//                                            (float)line.EndPoint.X, (float)line.EndPoint.Y);
//                            break;

//                        case EntityType.Polyline2D:
//                            var poly = (netDxf.Entities.Polyline2D)entity;
//                            var points = poly.Vertexes.Select(v => new PointF((float)v.Position.X, (float)v.Position.Y)).ToArray();
//                            if (points.Length > 1)
//                            {
//                                g.DrawLines(pen, points);
//                                if (poly.IsClosed) // 닫힌 다각형인 경우 마지막점과 시작점 연결
//                                    g.DrawLine(pen, points.Last(), points.First());
//                            }
//                            break;

//                        case EntityType.Circle:
//                            var circle = (netDxf.Entities.Circle)entity;
//                            float r = (float)circle.Radius;
//                            // GDI+는 중심점이 아닌 좌상단 사각형 기준임
//                            g.DrawEllipse(pen, (float)circle.Center.X - r, (float)circle.Center.Y - r, r * 2, r * 2);
//                            break;

//                        case EntityType.Arc:
//                            var arc = (netDxf.Entities.Arc)entity;
//                            float rd = (float)arc.Radius;
//                            float startAngle = (float)arc.StartAngle;
//                            float endAngle = (float)arc.EndAngle;

//                            // 1. 회전각 계산 (DXF는 항상 반시계 방향)
//                            float sweepAngle = endAngle - startAngle;
//                            if (sweepAngle <= 0) sweepAngle += 360;

//                            // 2. GraphicsPath를 사용한 정밀 렌더링
//                            // 이 방식은 GDI+의 DrawArc 각도 왜곡 문제를 우회합니다.
//                            using (GraphicsPath path = new GraphicsPath())
//                            {
//                                RectangleF rect = new RectangleF(
//                                    (float)arc.Center.X - rd,
//                                    (float)arc.Center.Y - rd,
//                                    rd * 2, rd * 2);

//                                // Y축 반전(-scale) 상태에서는 각도의 시작점과 방향을 DXF와 동기화해야 합니다.
//                                // 시작각도에 -를 붙여 위상을 맞추고, 방향(-sweep)을 지정합니다.
//                                path.AddArc(rect, -startAngle, -sweepAngle);
//                                g.DrawPath(pen, path);
//                            }
//                            break;

//                        case EntityType.Point:
//                            var pt = (netDxf.Entities.Point)entity;
//                            g.FillRectangle(Brushes.White, (float)pt.Position.X, (float)pt.Position.Y, penWidth, penWidth);
//                            break;
//                    }
//                }
//            }
//        }
//    }
//}



using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using netDxf;
using netDxf.Entities;


namespace _2022_Test.CadView
{
    public class CadView
    {
        private DxfDocument _dxfDoc;
        private Matrix _transformMatrix = new Matrix();
        private Panel panelViewer;

        public CadView(Panel _parent)
        {
            // Panel의 이름을 'panelViewer'라고 가정합니다.
            //this.panelViewer.DoubleBuffered = true;
            panelViewer = _parent;
            this.panelViewer.Paint += PanelViewer_Paint;
            this.panelViewer.Resize += (s, e) => CalculateScale();
        }

        public void Display(string filePath)
        {
            _dxfDoc = DxfDocument.Load(filePath);
            if (_dxfDoc != null)
            {
                CalculateScale();
                panelViewer.Invalidate();
            }
        }

        private void CalculateScale()
        {
            if (_dxfDoc == null || panelViewer.Width <= 0) return;

            // 1. 도면의 전체 경계(Bounding Box) 계산
            // 간단한 구현을 위해 모든 Line 엔티티의 최소/최대 좌표를 찾습니다.
            var lines = _dxfDoc.Entities.Lines;
            if (!lines.Any()) return;

            float minX = (float)lines.Min(l => Math.Min(l.StartPoint.X, l.EndPoint.X));
            float maxX = (float)lines.Max(l => Math.Max(l.StartPoint.X, l.EndPoint.X));
            float minY = (float)lines.Min(l => Math.Min(l.StartPoint.Y, l.EndPoint.Y));
            float maxY = (float)lines.Max(l => Math.Max(l.StartPoint.Y, l.EndPoint.Y));

            float dxfW = maxX - minX;
            float dxfH = maxY - minY;

            // 2. 패널 크기에 맞춘 스케일 결정 (10% 여백 고려)
            float scale = Math.Min(panelViewer.Width * 0.9f / dxfW, panelViewer.Height * 0.9f / dxfH);

            // 3. Matrix 설정: 중앙 정렬 -> Y축 반전(DXF to GDI) -> 모델 원점 이동
            _transformMatrix = new Matrix();
            _transformMatrix.Translate(panelViewer.Width / 2f, panelViewer.Height / 2f);
            _transformMatrix.Scale(scale, -scale);
            _transformMatrix.Translate(-(minX + dxfW / 2f), -(minY + dxfH / 2f));
        }

        private void PanelViewer_Paint(object sender, PaintEventArgs e)
        {
            if (_dxfDoc == null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Black); // 검은색 배경
            g.Transform = _transformMatrix;

            // 화면 배율에 상관없이 일정한 선 두께(1px) 유지
            float penWidth = 1f / _transformMatrix.Elements[0];
            using (Pen pen = new Pen(Color.White, penWidth))
            {
                // 모든 엔티티 순회 및 타입별 처리
                foreach (var entity in _dxfDoc.Entities.All)
                {
                    switch (entity)
                    {
                        case Line line:
                            g.DrawLine(pen, (float)line.StartPoint.X, (float)line.StartPoint.Y,
                                            (float)line.EndPoint.X, (float)line.EndPoint.Y);
                            break;

                        case Polyline2D poly:
                            var pts = poly.Vertexes.Select(v => new PointF((float)v.Position.X, (float)v.Position.Y)).ToArray();
                            if (pts.Length > 1)
                            {
                                g.DrawLines(pen, pts);
                                if (poly.IsClosed) g.DrawLine(pen, pts.Last(), pts.First());
                            }
                            break;

                        case Circle circle:
                            float r = (float)circle.Radius;
                            g.DrawEllipse(pen, (float)circle.Center.X - r, (float)circle.Center.Y - r, r * 2, r * 2);
                            break;

                        case Arc arc:
                            float ar = (float)arc.Radius;
                            float start = (float)arc.StartAngle;
                            float end = (float)arc.EndAngle;
                            float sweep = end - start;
                            if (sweep <= 0) sweep += 360;

                            // Y축 반전(-scale) 환경에서 DXF의 반시계 방향을 정확히 구현하는 공식
                            g.DrawArc(pen, (float)arc.Center.X - ar, (float)arc.Center.Y - ar,
                                           ar * 2, ar * 2, -start, -sweep);
                            break;

                        case netDxf.Entities.Point pt:
                            g.FillRectangle(Brushes.White, (float)pt.Position.X, (float)pt.Position.Y, penWidth * 2, penWidth * 2);
                            break;
                    }
                }
            }
        }
    }


}
