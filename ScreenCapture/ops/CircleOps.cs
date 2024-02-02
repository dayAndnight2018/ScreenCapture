using System.Drawing;

namespace ScreenCapture.ops
{
    public class CircleOps : Ops
    {
        public void doOps(Bitmap bitmap, Pen penBefore, Pen nowPen, Font font, Brush brush, Point startPos, Point endPos, Rectangle region, string content)
        {
            //获取绘图板
            var pic = Graphics.FromImage(bitmap);
            pic.DrawEllipse(nowPen, region);
            pic.Dispose();
        }

        public OperationType opsType()
        {
            return OperationType.ELLIPSE;
        }
    }
}
