using System.Drawing;

namespace ScreenCapture.ops
{
    public class RectangleOps : Ops
    {

        public void doOps(Bitmap bitmap, Pen penBefore, Pen nowPen, Font font, Brush brush, Point startPos, Point endPos, Rectangle region, string content)
        {
            //获取绘图板
            var pic = Graphics.FromImage(bitmap);
            //动态绘制
            pic.DrawRectangle(nowPen, region);
            pic.Dispose();
        }

        public OperationType opsType()
        {
            return OperationType.RECT;
        }
    }
}
