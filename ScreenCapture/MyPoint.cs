using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture
{
    public class MyPoint
    {
        public double? x;

        public double? y;

        public static double Multiply(MyPoint other1, MyPoint other2)
        {
            return (double)(other1.x * other2.y - other1.y * other2.x);
        }

        public static bool LineJudge(MyPoint A1, MyPoint A2, MyPoint B1, MyPoint B2)
        {
            return dir(A1, A2, B1) * dir(A1, A2, B2) <= 0 && dir(B1, B2, A1) * dir(B1, B2, A2) <= 0;
        }

        public static double length(MyPoint a,MyPoint b)
        {
            return Math.Sqrt(Math.Pow(Math.Abs((double)b.x - (double)a.x), 2) + Math.Pow(Math.Abs((double)b.y - (double)a.y), 2));
        }

        public static MyPoint Result(MyPoint p1, MyPoint p2, MyPoint q1, MyPoint q2)
        {
            double t = disline(p1, p2, q1) / (disline(p1,p2,q1) + disline(p1,p2,q2));
            MyPoint q1q2 = GetVector(q1, q2);
            return new MyPoint
            {
                x = q1.x + q1q2.x * t,
                y = q1.y + q1q2.y * t
            };


        }

        public static MyPoint GetVector(MyPoint a, MyPoint b)
        {
            return new MyPoint { x = b.x - a.x, y = b.y - a.y };
        }

        public static double GetCross(MyPoint a, MyPoint b,MyPoint p)
        {
            MyPoint x = GetVector(a, b);
            MyPoint y = GetVector(a, p);
            return (double)x.x * (double)y.y - (double)x.y * (double)y.x;
        }

        public static double GetDot(MyPoint a, MyPoint b, MyPoint p)
        {
            MyPoint x = GetVector(a, b);
            MyPoint y = GetVector(a, p);
            return (double)x.x * (double)y.x + (double)x.y * (double)y.y;
        }

        public static int dir(MyPoint a, MyPoint b, MyPoint p)
        {
            if (GetCross(a, b, p) > 0) return -1;
            else if (GetCross(a, b, p) < 0) return 1;
            else if (GetDot(a, b, p) < 0) return -2;
            else if (GetDot(a, b, p) >= 0)
            {
                if (dis2(a, b) < dis2(a, p)) return 2;
                else return 0;
            }
            else
                return -1;
        }

        public static double dis2(MyPoint a, MyPoint b)
        {
            return Math.Pow(Math.Abs((double)b.x - (double)a.x), 2) + Math.Pow(Math.Abs((double)b.y - (double)a.y), 2);
        }

        public static double disline(MyPoint a, MyPoint b,MyPoint p)
        {
            return Math.Abs(GetCross(a,b,p)) / Math.Sqrt(dis2(a,b));
        }
    }
}
