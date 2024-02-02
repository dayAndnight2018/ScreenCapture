using System.Drawing;

namespace ScreenCapture.ops
{
    public class WordsOps : Ops
    {
        public void doOps(Bitmap bitmap, Pen penBefore, Pen nowPen, Font font, Brush brush, Point startPos, Point endPos, Rectangle region, string content)
        {
            var pic = Graphics.FromImage(bitmap);
            //动态绘制
            pic.DrawString(content, font, brush, region);
            pic.Dispose();
        }

        public OperationType opsType()
        {
            return OperationType.WORDS;        
        }
    }
}
