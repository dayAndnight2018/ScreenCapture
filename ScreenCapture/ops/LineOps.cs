using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture.ops
{
    public class LineOps : Ops
    {
        public void doOps(Bitmap bitmap, Pen penBefore, Pen nowPen, Font font, Brush brush, Point startPos, Point endPos, Rectangle region, string content)
        {
            AdjustTobMosaic(bitmap, region, 5);
        }

        public OperationType opsType()
        {
            return OperationType.LINE;
        }

        private void AdjustTobMosaic(Bitmap bitmap, Rectangle area, int effectWidth)
        {
            // 差异最多的就是以照一定范围取样 玩之后直接去下一个范围
            for (int heightOfffset = area.Y; heightOfffset < area.Y + area.Height; heightOfffset += effectWidth)
            {
                for (int widthOffset = area.X; widthOffset < area.X + area.Width; widthOffset += effectWidth)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < area.X + area.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < area.Y + area.Height); y++)
                        {
                            Color pixel = bitmap.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    // 计算范围平均
                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;


                    // 所有范围内都设定此值
                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < area.X + area.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < area.Y + area.Height); y++)
                        {

                            Color newColor = Color.FromArgb(avgR, avgG, avgB);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                }
            }
            //return bitmap;
        }
    }
}
