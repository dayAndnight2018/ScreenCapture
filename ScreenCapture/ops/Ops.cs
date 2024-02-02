using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture.ops
{
    public interface Ops
    {
        OperationType opsType();

        void doOps(Bitmap bitmap, Pen penBefore, Pen nowPen, Font font, Brush brush, Point startPos, Point endPos, Rectangle region, String content);
    }
}
