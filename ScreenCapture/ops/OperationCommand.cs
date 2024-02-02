using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture.ops
{
    public static class OperationCommand
    {
        static Dictionary<OperationType, Ops> rectOps = new Dictionary<OperationType, Ops> {
            { OperationType.RECT, new RectangleOps()},
            { OperationType.ARROW, new ArrowOps()},
            { OperationType.ELLIPSE, new CircleOps() },
            { OperationType.LINE, new LineOps()},
            { OperationType.PEN, new PenOps()},
            { OperationType.WORDS, new WordsOps()}
        };

        public static void rePaint(Bitmap img, List<Operation> opses)
        {
            foreach(Operation operation in opses)
            {
                Ops ops = rectOps[operation.operationType];
                ops.doOps(img, operation.penBefore, operation.nowPen, operation.font, operation.brush, operation.startPos, operation.endPos, operation.region, operation.content);
            }
        }
    }
}
