using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture.ops
{
    public class ArrowOps : Ops
    {
        public void doOps(Bitmap bitmap, Pen penBefore, Pen nowPen, Font font, Brush brush, Point startPos, Point endPos, Rectangle region, string content)
        {
            //获取绘图板
            var pic = Graphics.FromImage(bitmap);
            pic.DrawLine(nowPen, startPos, endPos);
            pic.Dispose();
        }

        public OperationType opsType()
        {
            return OperationType.ARROW;
        }
    }
}
