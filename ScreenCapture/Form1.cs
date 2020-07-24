using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCapture
{
    public enum ShortCutKeysType
    {
        截图 = 1,
        吸取 = 2
    }

    public partial class Form1 : Form
    {
        //没有展现窗体是的整个屏幕
        Bitmap orgin;

        //绘制矩形
        Pen pen = new Pen(Color.Red, 5f);

        bool flag = false;

        Point start;

        bool cap = false;

        KeyboardHook kh;

        bool sniff = false;

        Rectangle region;

        #region rectangle相关

        bool rectangle = false, rectangleFlag = false;
        Point rectangleStartPoint;

        #endregion

        #region arrow相关

        bool arrow = false, arrowFlag = false;
        Point arrowStartPoint;

        #endregion

        #region circle相关

        bool circle = false, circleFlag = false;
        Point circleStartPoint;

        #endregion

        #region 模糊

        bool line = false, lineFlag = false;
        Point lineStartPoint;

        #endregion

        #region 绘制曲线

        Point lastPoint;
        Point cachePoint;
        bool PEN = false, PENFlag = false;

        #endregion

        #region 绘制文字

        Point drawStringStartPoint;
        bool drawString = false, drawStringFlag = false;

        #endregion

        private ColorButtons? cacheColor = null;
        private SizeButtons? cacheSize = null;
        private Color? cacheColorValue = null;
        private int? cacheSizeValue = null;

        private Stack<Bitmap> bitmaps = new Stack<Bitmap>();
        private Stack<Bitmap> results = new Stack<Bitmap>();

        private DrawStringResult drawStringResult;

        private Keys DefaultScreenKeys = Keys.Z | Keys.Control | Keys.Alt;
        private Keys DefaultSniffKeys = Keys.X | Keys.Control | Keys.Alt;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kh = new KeyboardHook();
            kh.SetHook();

            kh.OnKeyDownEvent += kh_OnKeyDownEvent;
            this.ShowInTaskbar = false;
            this.Hide();
            //this.TopMost = true;
        }

        public void setShortCutKeys(ShortCutKeysType type, Keys keys)
        {
            if(keys == Keys.None)
            {
                return;
            }
            else
            {
                switch(type)
                {
                    case ShortCutKeysType.截图:
                        DefaultScreenKeys = keys;
                        break;
                    case ShortCutKeysType.吸取:
                        DefaultSniffKeys = keys;
                        break;
                }
            }
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            //截图
            if (e.KeyData == (DefaultScreenKeys))
            {
                if (!cap)
                {
                    Deal();
                    cap = true;
                }

            }
            else if(e.KeyData == (DefaultSniffKeys))
            {
                //取色器
                if (!sniff)
                {
                    Deal();
                    sniff = true;
                    this.pictureBox1.Cursor = Cursors.Cross;
                }
            }

        }

        /// <summary>
        /// 热键
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == 100 && !cap)
            {
                Deal();
                cap = true;
            }
            base.WndProc(ref m);
        }

        public void SetNotify(string title, string content,ToolTipIcon icon)
        {
            this.notifyIcon.ShowBalloonTip(3000, title, content, icon);
        }

        /// <summary>
        /// 获取当前屏幕截图并全屏显示准备下一步操作。
        /// </summary>
        public void Deal()
        {
            orgin = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(orgin);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);
            pictureBox1.Image = orgin;
            g.Dispose();
            this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
            this.Opacity = 1;
        }

        private Point getColorSniffPanLocation(ColorSniffPan colorSniffPan, Point e)
        {
            Point p = new Point(e.X+1,e.Y+1);

            if (p.X < 0)
            {
                p.X = 0;
            }

            if (p.Y + colorSniffPan.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                p.Y = e.Y - 1 - colorSniffPan.Height;
            }
            if (p.X + colorSniffPan.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                p.X = Screen.PrimaryScreen.WorkingArea.Width - 1 - colorSniffPan.Width;
            }
            return p;
        }


        /// <summary>
        /// 按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (cap)
                {
                    pictureBox1.Controls.Clear();
                    flag = true;
                    start = e.Location;
                }
                else if (sniff)
                {                   
                    pictureBox1.Controls.Clear();
                    int x = e.Location.X;
                    int y = e.Location.Y;
                    
                    Color c = orgin.GetPixel(x, y);

                    ColorSniffPan colorSniffPan = new ColorSniffPan($"R:{c.R},G:{c.G},B:{c.B}",$"#{c.R.ToString("X2")}{c.G.ToString("X2")}{c.B.ToString("X2")}");
                    colorSniffPan.AddHandler((obj,args)=> {
                        Clipboard.Clear();
                        Clipboard.SetText(colorSniffPan.GetDex());
                        pictureBox1.Controls.Clear();
                        this.pictureBox1.Cursor = Cursors.Default;
                        release();
                        sniff = false;
                        cap = false;
                        this.Hide();
                        WindowState = FormWindowState.Minimized;
                    },(obj, args) => {
                        Clipboard.Clear();
                        Clipboard.SetText(colorSniffPan.GetHex());
                        pictureBox1.Controls.Clear();
                        this.pictureBox1.Cursor = Cursors.Default;
                        release();
                        sniff = false;
                        cap = false;
                        this.Hide();
                        WindowState = FormWindowState.Minimized;
                    });

                    colorSniffPan.Location = getColorSniffPanLocation(colorSniffPan, e.Location);
                    pictureBox1.Controls.Add(colorSniffPan);
                }
                else if(rectangle)
                {
                    if(e.X >= start.X && e.X <= start.X + region.Width && e.Y >= start.Y && e.Y <= start.Y+region.Height)
                    {
                        //绘制图形
                        rectangleFlag = true;
                        rectangleStartPoint = e.Location;
                    }                   
                }
                else if(arrow)
                {
                    if (e.X >= start.X && e.X <= start.X + region.Width && e.Y >= start.Y && e.Y <= start.Y + region.Height)
                    {
                        //绘制图形
                        arrowFlag = true;
                        arrowStartPoint = e.Location;
                    }
                }
                else if(circle)
                {
                    if (e.X >= start.X && e.X <= start.X + region.Width && e.Y >= start.Y && e.Y <= start.Y + region.Height)
                    {
                        //绘制图形
                        circleFlag = true;
                        circleStartPoint = e.Location;
                    }
                }
                else if(line)
                {
                    if (e.X >= start.X && e.X <= start.X + region.Width && e.Y >= start.Y && e.Y <= start.Y + region.Height)
                    {
                        //绘制图形
                        lineFlag = true;
                        lineStartPoint = e.Location;
                    }
                }
                else if(PEN)
                {
                    if (e.X >= start.X && e.X <= start.X + region.Width && e.Y >= start.Y && e.Y <= start.Y + region.Height)
                    {
                        //绘制图形
                        PENFlag = true;
                        lastPoint = e.Location;
                        cachePoint = e.Location;
                    }
                }
                else if(drawString)
                {
                    if (e.X >= start.X && e.X <= start.X + region.Width && e.Y >= start.Y && e.Y <= start.Y + region.Height)
                    {
                        //绘制图形
                        drawStringFlag = true;
                        drawStringStartPoint = e.Location;                       
                    }
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                this.pictureBox1.Cursor = Cursors.Default;
                release();
                sniff = false;
                cap = false;
                this.Hide();
                WindowState = FormWindowState.Minimized;
            }
                        
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (rectangle || arrow||circle||line || PEN||drawString)
            {
                if (e.X >= start.X && e.X <= (start.X + region.Width) && e.Y >= start.Y && e.Y <= (start.Y + region.Height))
                    this.pictureBox1.Cursor = Cursors.Cross;
                else
                    this.pictureBox1.Cursor = Cursors.Arrow;
            }

            if (flag)
            {
                //复制一份
                var copy = (Bitmap)orgin.Clone();

                //获取绘图板
                var pic = Graphics.FromImage(copy);
                //获取捕捉区域大小
                var region = new Rectangle(new Point(Math.Min(start.X, e.Location.X), Math.Min(start.Y, e.Location.Y)), new Size(Math.Abs(start.X - e.Location.X), Math.Abs(start.Y - e.Location.Y)));

                pen.Width = 2f;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pen.Color = Color.Red;
                //动态绘制
                pic.DrawRectangle(pen, region);

                pictureBox1.Image = copy;

                pic.Dispose();
            }
            else if(rectangleFlag)
            {
                //复制一份
                var temp = bitmaps.Peek().Clone() as Bitmap;

                //获取绘图板
                var pic = Graphics.FromImage(temp);

                Point tempP = new Point(e.X, e.Y);
                if(e.X < start.X)
                {
                    tempP.X = start.X;
                }

                if (e.X > (start.X + this.region.Width))
                {
                    tempP.X = start.X + this.region.Width;
                }

                if (e.Y > (start.Y + this.region.Height))
                {
                    tempP.Y = start.Y + this.region.Height;
                }

                if (e.Y < start.Y)
                {
                    tempP.Y = start.Y;
                }


                //获取捕捉区域大小
                var region = new Rectangle(new Point(Math.Min(rectangleStartPoint.X, tempP.X), Math.Min(rectangleStartPoint.Y, tempP.Y)), new Size(Math.Abs(rectangleStartPoint.X - tempP.X), Math.Abs(rectangleStartPoint.Y - tempP.Y)));
                pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                //动态绘制
                pic.DrawRectangle(pen, region);
                pictureBox1.Image = temp;
                pic.Dispose();
            }
            else if(arrowFlag)
            {
                //复制一份
                var temp = bitmaps.Peek().Clone() as Bitmap;

                //获取绘图板
                var pic = Graphics.FromImage(temp);

                pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;

                if (e.X <= start.X || e.X >= start.X + region.Width || e.Y <= start.Y || e.Y >= start.Y + region.Height)
                {
                    MyPoint L1P1 = new MyPoint { x = start.X, y = start.Y };
                    MyPoint L1P2 = new MyPoint { x = start.X + region.Width, y = start.Y };

                    MyPoint L2P1 = new MyPoint { x = start.X + region.Width, y = start.Y };
                    MyPoint L2P2 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };

                    MyPoint L3P1 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };
                    MyPoint L3P2 = new MyPoint { x = start.X, y = start.Y + region.Height };

                    MyPoint L4P1 = new MyPoint { x = start.X, y = start.Y + region.Height };
                    MyPoint L4P2 = new MyPoint { x = start.X, y = start.Y };

                    MyPoint LP1 = new MyPoint { x = arrowStartPoint.X, y = arrowStartPoint.Y };
                    MyPoint LP2 = new MyPoint { x = e.X,y = e.Y};

                    MyPoint result = null;

                    if(MyPoint.LineJudge(L1P1,L1P2,LP1,LP2))
                    {
                        result = MyPoint.Result(L1P1, L1P2, LP1, LP2);
                    }
                    else if(MyPoint.LineJudge(L2P1, L2P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L2P1, L2P2, LP1, LP2);
                    }
                    else if(MyPoint.LineJudge(L3P1, L3P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L3P1, L3P2, LP1, LP2);
                    }
                    else if(MyPoint.LineJudge(L4P1, L4P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L4P1, L4P2, LP1, LP2);
                    }

                    if (result != null && result.x != null && result.y != null)
                        pic.DrawLine(pen, arrowStartPoint, new Point { X = (int)result.x, Y = (int)result.y });
                    else
                        pic.DrawLine(pen, arrowStartPoint, e.Location);
                }
                else
                {
                    pic.DrawLine(pen, arrowStartPoint, e.Location);
                }
               

                pictureBox1.Image = temp;

                pic.Dispose();
            }
            else if(circleFlag)
            {
                //复制一份
                var temp = bitmaps.Peek().Clone() as Bitmap;

                Point tempP = new Point(e.X, e.Y);
                if (e.X < start.X)
                {
                    tempP.X = start.X;
                }

                if (e.X > (start.X + this.region.Width))
                {
                    tempP.X = start.X + this.region.Width;
                }

                if (e.Y > (start.Y + this.region.Height))
                {
                    tempP.Y = start.Y + this.region.Height;
                }

                if (e.Y < start.Y)
                {
                    tempP.Y = start.Y;
                }

                var region = new Rectangle(new Point(Math.Min(circleStartPoint.X, tempP.X), Math.Min(circleStartPoint.Y, tempP.Y)), new Size(Math.Abs(circleStartPoint.X - tempP.X), Math.Abs(circleStartPoint.Y - tempP.Y)));
                //获取绘图板
                var pic = Graphics.FromImage(temp);

                pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pic.DrawEllipse(pen, region);

                pictureBox1.Image = temp;

                pic.Dispose();
            }
            else if(lineFlag)
            {
                var penBake = pen.Clone() as Pen;
                //复制一份
                var temp = bitmaps.Peek().Clone() as Bitmap;

                if (e.X <= start.X || e.X >= start.X + region.Width || e.Y <= start.Y || e.Y >= start.Y + region.Height)
                {
                    MyPoint L1P1 = new MyPoint { x = start.X, y = start.Y };
                    MyPoint L1P2 = new MyPoint { x = start.X + region.Width, y = start.Y };

                    MyPoint L2P1 = new MyPoint { x = start.X + region.Width, y = start.Y };
                    MyPoint L2P2 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };

                    MyPoint L3P1 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };
                    MyPoint L3P2 = new MyPoint { x = start.X, y = start.Y + region.Height };

                    MyPoint L4P1 = new MyPoint { x = start.X, y = start.Y + region.Height };
                    MyPoint L4P2 = new MyPoint { x = start.X, y = start.Y };

                    MyPoint LP1 = new MyPoint { x = lineStartPoint.X, y = lineStartPoint.Y };
                    MyPoint LP2 = new MyPoint { x = e.X, y = e.Y };

                    MyPoint result = null;

                    if (MyPoint.LineJudge(L1P1, L1P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L1P1, L1P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L2P1, L2P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L2P1, L2P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L3P1, L3P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L3P1, L3P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L4P1, L4P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L4P1, L4P2, LP1, LP2);
                    }

                    if (result != null && result.x != null && result.y != null)
                    {

                        AdjustTobMosaic(temp, new Rectangle(new Point(Math.Min(lineStartPoint.X, (int)result.x), Math.Min(lineStartPoint.Y, (int)result.y)), new Size(Math.Abs(lineStartPoint.X - (int)result.x), Math.Abs(lineStartPoint.Y - (int)result.y))), 5);
                        //pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                        //pic.DrawLine(pen, lineStartPoint, new Point { X = (int)result.x, Y = (int)result.y });
                    }
                    else
                        AdjustTobMosaic(temp, new Rectangle(new Point(Math.Min(lineStartPoint.X, e.X), Math.Min(lineStartPoint.Y, e.Y)), new Size(Math.Abs(lineStartPoint.X -e.X), Math.Abs(lineStartPoint.Y - e.Y))), 5);
                    //pic.DrawLine(pen, lineStartPoint, e.Location);
                }
                else
                {
                    AdjustTobMosaic(temp, new Rectangle(new Point(Math.Min(lineStartPoint.X, e.X), Math.Min(lineStartPoint.Y, e.Y)), new Size(Math.Abs(lineStartPoint.X - e.X), Math.Abs(lineStartPoint.Y - e.Y))), 5);
                    //pic.DrawLine(pen, lineStartPoint, e.Location);
                }

                pictureBox1.Image = temp;

                //pic.Dispose();

                pen = penBake;
            }
            else if(PENFlag && Math.Abs(e.X - lastPoint.X) + Math.Abs(e.Y - lastPoint.Y) >= 10)
            {
                //复制一份
                var temp = bitmaps.Peek().Clone() as Bitmap;

                //获取绘图板
                var pic = Graphics.FromImage(temp);

                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;

                var ttpic = this.results.Peek().Clone() as Bitmap;
                var tt = Graphics.FromImage(ttpic);

                if (e.X <= start.X || e.X >= start.X + region.Width || e.Y <= start.Y || e.Y >= start.Y + region.Height)
                {
                    MyPoint L1P1 = new MyPoint { x = start.X, y = start.Y };
                    MyPoint L1P2 = new MyPoint { x = start.X + region.Width, y = start.Y };

                    MyPoint L2P1 = new MyPoint { x = start.X + region.Width, y = start.Y };
                    MyPoint L2P2 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };

                    MyPoint L3P1 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };
                    MyPoint L3P2 = new MyPoint { x = start.X, y = start.Y + region.Height };

                    MyPoint L4P1 = new MyPoint { x = start.X, y = start.Y + region.Height };
                    MyPoint L4P2 = new MyPoint { x = start.X, y = start.Y };

                    MyPoint LP1 = new MyPoint { x = lastPoint.X, y = lastPoint.Y };
                    MyPoint LP2 = new MyPoint { x = e.X, y = e.Y };

                    MyPoint result = null;

                    if (MyPoint.LineJudge(L1P1, L1P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L1P1, L1P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L2P1, L2P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L2P1, L2P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L3P1, L3P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L3P1, L3P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L4P1, L4P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L4P1, L4P2, LP1, LP2);
                    }

                    if (result != null && result.x != null && result.y != null)
                    {
                        pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                        pic.DrawLine(pen, lastPoint, new Point { X = (int)result.x, Y = (int)result.y });
                        tt.DrawLine(pen, lastPoint, new Point { X = (int)result.x, Y = (int)result.y });
                        lastPoint = new Point { X = (int)result.x, Y = (int)result.y };
                    }
                    else
                    {                       
                        pic.DrawLine(pen, lastPoint, e.Location);
                        tt.DrawLine(pen, lastPoint, e.Location);
                        lastPoint = e.Location;
                    }
                        
                }
                else
                {
                    pic.DrawLine(pen, lastPoint, e.Location);
                    tt.DrawLine(pen, lastPoint, e.Location);
                    lastPoint = e.Location;
                }

                pictureBox1.Image = temp;

                pic.Dispose();

                this.bitmaps.Push(temp);
                this.results.Push(ttpic);

                tt.Dispose();

                
            }
            else if(drawStringFlag)
            {
                var penBake = pen.Clone() as Pen;
                //复制一份
                var temp = bitmaps.Peek().Clone() as Bitmap;

                //获取绘图板
                var pic = Graphics.FromImage(temp);

                Point tempP = new Point(e.X, e.Y);
                if (e.X < start.X)
                {
                    tempP.X = start.X;
                }

                if (e.X > (start.X + this.region.Width))
                {
                    tempP.X = start.X + this.region.Width;
                }

                if (e.Y > (start.Y + this.region.Height))
                {
                    tempP.Y = start.Y + this.region.Height;
                }

                if (e.Y < start.Y)
                {
                    tempP.Y = start.Y;
                }
                //获取捕捉区域大小
                var region = new Rectangle(new Point(Math.Min(drawStringStartPoint.X, tempP.X), Math.Min(drawStringStartPoint.Y, tempP.Y)), new Size(Math.Abs(drawStringStartPoint.X - tempP.X), Math.Abs(drawStringStartPoint.Y - tempP.Y)));
                
                //动态绘制
                pic.DrawString(drawStringResult.Content,drawStringResult.font,new SolidBrush(drawStringResult.color), region);
                pictureBox1.Image = temp;
                pic.Dispose();

                pen = penBake;
            }
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
          
            ////获取捕捉区域大小
            var region = new Rectangle(new Point(Math.Min(start.X, e.Location.X), Math.Min(start.Y, e.Location.Y)), new Size(Math.Abs(start.X - e.Location.X), Math.Abs(start.Y - e.Location.Y)));           

            if (flag && region.Width > 0 && region.Height > 0)
            {
                this.region = region;
                this.bitmaps.Clear();
                this.bitmaps.Push(this.pictureBox1.Image as Bitmap);
                this.results.Push(orgin.Clone() as Bitmap);

                ToolBar toolBar = new ToolBar();
                SettingPan settingPan;

               if (cacheColor!=null && cacheSize != null)
                {
                    pen.Color = (Color)cacheColorValue;
                    pen.Width = (int)cacheSizeValue;
                    settingPan = new SettingPan((SizeButtons)cacheSize, (ColorButtons)cacheColor);
                }
               else
                {
                    pen.Color = Color.Red;
                    pen.Width = 2;
                    settingPan = new SettingPan(SizeButtons.小号, ColorButtons.红色);
                }
                toolBar.Location = getToolBarLocation(toolBar, settingPan, new Point (Math.Max(start.X,e.X),Math.Max(start.Y,e.Y)), region);               
                settingPan.Location = new Point(toolBar.Location.X, toolBar.Location.Y + toolBar.Height);

                RegisterHandlers(toolBar, settingPan, region);
                pictureBox1.Controls.Add(toolBar);
                pictureBox1.Controls.Add(settingPan);
                settingPan.Hide();

            }
            if(rectangleFlag && region.Width > 0 && region.Height > 0)
            {
                this.bitmaps.Push(this.pictureBox1.Image as Bitmap);

                //复制一份
                var temp = results.Peek().Clone() as Bitmap;
                //获取绘图板
                var pic = Graphics.FromImage(temp);

                Point tempP = new Point(e.X, e.Y);
                if (e.X < start.X)
                {
                    tempP.X = start.X;
                }

                if (e.X > (start.X + this.region.Width))
                {
                    tempP.X = start.X + this.region.Width;
                }

                if (e.Y > (start.Y + this.region.Height))
                {
                    tempP.Y = start.Y + this.region.Height;
                }

                if (e.Y < start.Y)
                {
                    tempP.Y = start.Y;
                }
                //获取捕捉区域大小
                var tempRegion = new Rectangle(new Point(Math.Min(rectangleStartPoint.X, tempP.X), Math.Min(rectangleStartPoint.Y, tempP.Y)), new Size(Math.Abs(rectangleStartPoint.X - tempP.X), Math.Abs(rectangleStartPoint.Y - tempP.Y)));

                pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                //动态绘制
                pic.DrawRectangle(pen, tempRegion);
                pic.Dispose();

                this.results.Push(temp);
            }
            if (arrowFlag && arrowStartPoint!=e.Location)
            {
                this.bitmaps.Push(this.pictureBox1.Image as Bitmap);

                //复制一份
                var temp = results.Peek().Clone() as Bitmap;

                //获取绘图板
                var pic = Graphics.FromImage(temp);

                pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;

                if (e.X <= start.X || e.X >= start.X + region.Width || e.Y <= start.Y || e.Y >= start.Y + region.Height)
                {
                    MyPoint L1P1 = new MyPoint { x = start.X, y = start.Y };
                    MyPoint L1P2 = new MyPoint { x = start.X + region.Width, y = start.Y };

                    MyPoint L2P1 = new MyPoint { x = start.X + region.Width, y = start.Y };
                    MyPoint L2P2 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };

                    MyPoint L3P1 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };
                    MyPoint L3P2 = new MyPoint { x = start.X, y = start.Y + region.Height };

                    MyPoint L4P1 = new MyPoint { x = start.X, y = start.Y + region.Height };
                    MyPoint L4P2 = new MyPoint { x = start.X, y = start.Y };

                    MyPoint LP1 = new MyPoint { x = arrowStartPoint.X, y = arrowStartPoint.Y };
                    MyPoint LP2 = new MyPoint { x = e.X, y = e.Y };

                    MyPoint result = null;

                    if (MyPoint.LineJudge(L1P1, L1P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L1P1, L1P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L2P1, L2P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L2P1, L2P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L3P1, L3P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L3P1, L3P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L4P1, L4P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L4P1, L4P2, LP1, LP2);
                    }

                    if(result != null && result.x !=null && result.y != null)
                        pic.DrawLine(pen, arrowStartPoint, new Point { X = (int)result.x, Y = (int)result.y });
                    else
                        pic.DrawLine(pen, arrowStartPoint, e.Location);
                }
                else
                {
                    pic.DrawLine(pen, arrowStartPoint, e.Location);
                }

                pic.Dispose();

                this.results.Push(temp);
            }
            if (circleFlag && region.Width > 0 && region.Height > 0)
            {
                this.bitmaps.Push(this.pictureBox1.Image as Bitmap);

                //复制一份
                var temp = results.Peek().Clone() as Bitmap;

                Point tempP = new Point(e.X, e.Y);
                if (e.X < start.X)
                {
                    tempP.X = start.X;
                }

                if (e.X > (start.X + this.region.Width))
                {
                    tempP.X = start.X + this.region.Width;
                }

                if (e.Y > (start.Y + this.region.Height))
                {
                    tempP.Y = start.Y + this.region.Height;
                }

                if (e.Y < start.Y)
                {
                    tempP.Y = start.Y;
                }

                var tempRegion = new Rectangle(new Point(Math.Min(circleStartPoint.X, tempP.X), Math.Min(circleStartPoint.Y, tempP.Y)), new Size(Math.Abs(circleStartPoint.X - tempP.X), Math.Abs(circleStartPoint.Y - tempP.Y)));
                //获取绘图板
                var pic = Graphics.FromImage(temp);

                pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pic.DrawEllipse(pen, tempRegion);

                pic.Dispose();

                this.results.Push(temp);
            }
            if(lineFlag && lineStartPoint != e.Location)
            {
                var penBake = pen.Clone() as Pen;
                this.bitmaps.Push(this.pictureBox1.Image as Bitmap);

                //复制一份
                var temp = results.Peek().Clone() as Bitmap;

                if (e.X <= start.X || e.X >= start.X + region.Width || e.Y <= start.Y || e.Y >= start.Y + region.Height)
                {
                    MyPoint L1P1 = new MyPoint { x = start.X, y = start.Y };
                    MyPoint L1P2 = new MyPoint { x = start.X + region.Width, y = start.Y };

                    MyPoint L2P1 = new MyPoint { x = start.X + region.Width, y = start.Y };
                    MyPoint L2P2 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };

                    MyPoint L3P1 = new MyPoint { x = start.X + region.Width, y = start.Y + region.Height };
                    MyPoint L3P2 = new MyPoint { x = start.X, y = start.Y + region.Height };

                    MyPoint L4P1 = new MyPoint { x = start.X, y = start.Y + region.Height };
                    MyPoint L4P2 = new MyPoint { x = start.X, y = start.Y };

                    MyPoint LP1 = new MyPoint { x = lineStartPoint.X, y = lineStartPoint.Y };
                    MyPoint LP2 = new MyPoint { x = e.X, y = e.Y };

                    MyPoint result = null;

                    if (MyPoint.LineJudge(L1P1, L1P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L1P1, L1P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L2P1, L2P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L2P1, L2P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L3P1, L3P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L3P1, L3P2, LP1, LP2);
                    }
                    else if (MyPoint.LineJudge(L4P1, L4P2, LP1, LP2))
                    {
                        result = MyPoint.Result(L4P1, L4P2, LP1, LP2);
                    }

                    if (result != null && result.x != null && result.y != null)
                    {
                        AdjustTobMosaic(temp, new Rectangle(new Point(Math.Min(lineStartPoint.X, (int)result.x), Math.Min(lineStartPoint.Y, (int)result.y)), new Size(Math.Abs(lineStartPoint.X - (int)result.x), Math.Abs(lineStartPoint.Y - (int)result.y))), 5);
                        //pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                        //pic.DrawLine(pen, lineStartPoint, new Point { X = (int)result.x, Y = (int)result.y });
                    }                       
                    else
                        AdjustTobMosaic(temp, new Rectangle(new Point(Math.Min(lineStartPoint.X, e.X), Math.Min(lineStartPoint.Y, e.Y)), new Size(Math.Abs(lineStartPoint.X - e.X), Math.Abs(lineStartPoint.Y - e.Y))), 5);
                    //pic.DrawLine(pen, lineStartPoint, e.Location);
                }
                else
                {
                    AdjustTobMosaic(temp, new Rectangle(new Point(Math.Min(lineStartPoint.X, e.X), Math.Min(lineStartPoint.Y, e.Y)), new Size(Math.Abs(lineStartPoint.X - e.X), Math.Abs(lineStartPoint.Y - e.Y))), 5);
                    //pic.DrawLine(pen, lineStartPoint, e.Location);
                }

                //pic.Dispose();

                this.results.Push(temp);
                pen = penBake;
            }
            if(drawString && e.Location != drawStringStartPoint)
            {
                var penBake = pen.Clone() as Pen;

                this.bitmaps.Push(this.pictureBox1.Image as Bitmap);

                //复制一份
                var temp = results.Peek().Clone() as Bitmap;
                //获取绘图板
                var pic = Graphics.FromImage(temp);

                Point tempP = new Point(e.X, e.Y);
                if (e.X < start.X)
                {
                    tempP.X = start.X;
                }

                if (e.X > (start.X + this.region.Width))
                {
                    tempP.X = start.X + this.region.Width;
                }

                if (e.Y > (start.Y + this.region.Height))
                {
                    tempP.Y = start.Y + this.region.Height;
                }

                if (e.Y < start.Y)
                {
                    tempP.Y = start.Y;
                }
                //获取捕捉区域大小
                var tempRegion = new Rectangle(new Point(Math.Min(drawStringStartPoint.X, tempP.X), Math.Min(drawStringStartPoint.Y, tempP.Y)), new Size(Math.Abs(drawStringStartPoint.X - tempP.X), Math.Abs(drawStringStartPoint.Y - tempP.Y)));

                //动态绘制
                pic.DrawString(drawStringResult.Content, drawStringResult.font, new SolidBrush(drawStringResult.color), tempRegion);

                pic.Dispose();

                this.results.Push(temp);

                this.drawString = false;

                pen = penBake;
            }
            
            flag = false;
            cap = false;
            rectangleFlag = false;
            arrowFlag = false;
            circleFlag = false;
            lineFlag = false;
            PENFlag = false;
            drawStringFlag = false;
        }

        /// <summary>
        /// 退出软件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripMenuItem1)
            {
                this.Close();
            }
            else if(e.ClickedItem == toolStripMenuItem2)
            {
                Settings settings = new Settings(this,DefaultScreenKeys,DefaultSniffKeys);
                settings.StartPosition = FormStartPosition.CenterScreen;
                settings.ShowDialog();
            }
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            kh.UnHook();
        }

        /// <summary>
        /// 计算toolBar的位置
        /// </summary>
        /// <param name="toolBar"></param>
        /// <param name="e"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        private Point getToolBarLocation(ToolBar toolBar, SettingPan settingPan, Point e,Rectangle rectangle)
        {
            Point p;
            if(rectangle.Width >= toolBar.Width)
            {
                p =  new Point(e.X - (rectangle.Width - toolBar.Width) / 2 - toolBar.Width, e.Y + 1);
            }
            else
            {
                p = new Point(e.X - rectangle.Width - (toolBar.Width - rectangle.Width) / 2, e.Y + 1);               
            }

            if (p.X < 0)
            {
                p.X = 0;
            }
            if (p.Y + toolBar.Height + settingPan.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                p.Y = e.Y - 1 - toolBar.Height - settingPan.Height;
            }
            if(p.X  + toolBar.Width > Screen.PrimaryScreen.WorkingArea.Width )
            {
                p.X = Screen.PrimaryScreen.WorkingArea.Width - 1 - toolBar.Width;
            }           
            return p;
        }

        private void RegisterHandlers(ToolBar toolBar,SettingPan settingPan,Rectangle region)
        {
            //退出按钮
            toolBar.AddExitEventHandler((x, y) =>
            {
                this.Visible = false;
                this.WindowState = FormWindowState.Minimized;
                Reset();
                release();
            });

            //完成按钮
            toolBar.AddFinfishBtnHandler(GetFinishBtnHandler(region));

            //保存按钮
            toolBar.AddSaveBtnHandler((x, y) =>
            {
                if (region.Width != 0 && region.Height != 0)
                {
                    var part = this.results.Peek().Clone(region, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.InitialDirectory = "c:\\";
                    dialog.Filter = "图像文件|*.jpg";
                    dialog.FileName = DateTime.Now.Ticks.ToString() + ".jpg";
                    dialog.DefaultExt = "jpg";
                    if(dialog.ShowDialog() == DialogResult.OK)
                    {
                        part.Save(dialog.FileName);

                        this.Visible = false;
                        this.WindowState = FormWindowState.Minimized;
                        Reset();
                        release();
                        this.notifyIcon.ShowBalloonTip(3000, "截图助手", "截图文件保存成功！", ToolTipIcon.Info);
                    }
                }
            });

            //矩形按钮
            toolBar.AddRectangleBtnHandler((x,y)=> {
                if(!rectangle)
                {
                    settingPan.Show();
                    this.rectangle = true;
                    toolBar.SetRectangleBtnColor(Color.Red);
                    this.arrow = false;
                    toolBar.SetArrowBtnColor(Color.Transparent);
                    this.circle = false;
                    toolBar.SetCircleBtnColor(Color.Transparent);
                    this.line = false;
                    toolBar.SetLineBtnColor(Color.Transparent);
                    this.PEN = false;
                    toolBar.SetPenBtnColor(Color.Transparent);
                    this.drawString = false;
                }
                else
                {
                    settingPan.Hide();
                    this.rectangle = false;
                    toolBar.SetRectangleBtnColor(Color.Transparent);
                }
                
            });

            //箭头
            toolBar.AddArrowBtnHandler((x, y) =>
            {
                if (!arrow)
                {
                    settingPan.Show();
                    this.arrow = true;
                    toolBar.SetArrowBtnColor(Color.Red);
                    this.rectangle = false;
                    toolBar.SetRectangleBtnColor(Color.Transparent);
                    this.circle = false;
                    toolBar.SetCircleBtnColor(Color.Transparent);
                    this.line = false;
                    toolBar.SetLineBtnColor(Color.Transparent);
                    this.PEN = false;
                    toolBar.SetPenBtnColor(Color.Transparent);
                    this.drawString = false;
                }
                else
                {
                    settingPan.Hide();
                    this.arrow = false;
                    toolBar.SetArrowBtnColor(Color.Transparent);
                }
            });

            //画圆圈
            toolBar.AddCircleBtnHandler((x, y) =>
            {
                if (!circle)
                {
                    settingPan.Show();
                    this.circle = true;
                    toolBar.SetCircleBtnColor(Color.Red);
                    this.rectangle = false;
                    toolBar.SetRectangleBtnColor(Color.Transparent);
                    this.arrow = false;
                    toolBar.SetArrowBtnColor(Color.Transparent);
                    this.line = false;
                    toolBar.SetLineBtnColor(Color.Transparent);
                    this.PEN = false;
                    toolBar.SetPenBtnColor(Color.Transparent);
                    this.drawString = false;
                }
                else
                {
                    settingPan.Hide();
                    this.circle = false;
                    toolBar.SetCircleBtnColor(Color.Transparent);
                }
            });

            //马赛克
            toolBar.AddLineBtnHandler((x, y) =>
            {
                if (!line)
                {                   
                    this.line = true;
                    toolBar.SetLineBtnColor(Color.Red);
                    this.arrow = false;
                    toolBar.SetArrowBtnColor(Color.Transparent);
                    this.rectangle = false;
                    toolBar.SetRectangleBtnColor(Color.Transparent);
                    this.circle = false;
                    toolBar.SetCircleBtnColor(Color.Transparent);
                    this.PEN = false;
                    toolBar.SetPenBtnColor(Color.Transparent);
                    this.drawString = false;
                    settingPan.Hide();
                }
                else
                {                   
                    this.line = false;
                    toolBar.SetLineBtnColor(Color.Transparent);
                    settingPan.Hide();
                }
            });

            //撤销按钮
            toolBar.AddCancelBtnHandler((x, y) =>
            {
                if(bitmaps.Count > 1)
                {
                    var temp = bitmaps.Pop() ;
                    temp.Dispose();
                    temp = results.Pop();
                    temp.Dispose();
                    this.pictureBox1.Image = bitmaps.Peek();
                }
                else
                {
                    this.Visible = false;
                    this.WindowState = FormWindowState.Minimized;                   
                    rectangle = false;
                    arrow = false;
                    release();
                }
            });

            //清除按钮
            toolBar.AddCleanBtnHandler((x, y) =>
            {
                while (bitmaps.Count > 1)
                {
                    var temp = bitmaps.Pop();
                    temp.Dispose();
                    temp = results.Pop();
                    temp.Dispose();
                }
                this.pictureBox1.Image = bitmaps.Peek();
            });

            toolBar.AddPenBtnHandler((x, y) =>
            {
                if (!PEN)
                {
                    settingPan.Show();
                    this.PEN = true;
                    toolBar.SetPenBtnColor(Color.Red);
                    this.arrow = false;
                    toolBar.SetArrowBtnColor(Color.Transparent);
                    this.rectangle = false;
                    toolBar.SetRectangleBtnColor(Color.Transparent);
                    this.circle = false;
                    toolBar.SetCircleBtnColor(Color.Transparent);
                    this.line = false;
                    toolBar.SetLineBtnColor(Color.Transparent);
                    this.drawString = false;
                }
                else
                {
                    settingPan.Hide();
                    this.PEN = false;
                    toolBar.SetPenBtnColor(Color.Transparent);
                }
            });

            toolBar.AddWordBtnHandler((x, y) =>
            {
                settingPan.Hide();
                this.PEN = false;
                toolBar.SetPenBtnColor(Color.Transparent);
                this.arrow = false;
                toolBar.SetArrowBtnColor(Color.Transparent);
                this.rectangle = false;
                toolBar.SetRectangleBtnColor(Color.Transparent);
                this.circle = false;
                toolBar.SetCircleBtnColor(Color.Transparent);
                this.line = false;
                toolBar.SetLineBtnColor(Color.Transparent);

                InputForm form = null;
                if(drawStringResult == null)
                {
                    form = new InputForm();
                }
                else
                {
                    form = new InputForm(drawStringResult);
                }                
                form.StartPosition = FormStartPosition.CenterScreen;
                form.AddConfirmHandler((x1, y1) =>
                {
                    this.drawStringResult = form.SavePoint();
                    drawString = true;
                    form.Close();
                });
                form.TopMost = true;
                form.ShowDialog();
            });

            settingPan.AddSmallBtnHandler((x, y) =>
            {
                settingPan.SetSizeButtonOn(SizeButtons.小号);
                this.pen.Width = settingPan.GetFontSize();
                this.cacheSize = SizeButtons.小号;
                this.cacheSizeValue = settingPan.GetFontSize();
            });
            settingPan.AddMiddleBtnHandler((x, y) =>
            {
                settingPan.SetSizeButtonOn(SizeButtons.中号);
                this.pen.Width = settingPan.GetFontSize();
                this.cacheSize = SizeButtons.中号;
                this.cacheSizeValue = settingPan.GetFontSize();
            });
            settingPan.AddBigBtnHandler((x, y) =>
            {
                settingPan.SetSizeButtonOn(SizeButtons.大号);
                this.pen.Width = settingPan.GetFontSize();
                this.cacheSize = SizeButtons.大号;
                this.cacheSizeValue = settingPan.GetFontSize();
            });

            settingPan.AddRedBtnHandler((x, y) =>
            {
                settingPan.SetColorButtonOn(ColorButtons.红色);
                this.pen.Brush = new SolidBrush(settingPan.GetColor());
                this.cacheColor = ColorButtons.红色;
                this.cacheColorValue = settingPan.GetColor();
            });

            settingPan.AddOrangeBtnHandler((x, y) =>
            {
                settingPan.SetColorButtonOn(ColorButtons.橙色);
                this.pen.Brush = new SolidBrush(settingPan.GetColor());
                this.cacheColor = ColorButtons.橙色;
                this.cacheColorValue = settingPan.GetColor();
            });

            settingPan.AddYellowHandler((x, y) =>
            {
                settingPan.SetColorButtonOn(ColorButtons.黄色);
                this.pen.Brush = new SolidBrush(settingPan.GetColor());
                this.cacheColor = ColorButtons.黄色;
                this.cacheColorValue = settingPan.GetColor();
            });

            settingPan.AddGreenHandler((x, y) =>
            {
                settingPan.SetColorButtonOn(ColorButtons.绿色);
                this.pen.Brush = new SolidBrush(settingPan.GetColor());
                this.cacheColor = ColorButtons.绿色;
                this.cacheColorValue = settingPan.GetColor();
            });

            settingPan.AddBlueHandler((x, y) =>
            {
                settingPan.SetColorButtonOn(ColorButtons.蓝色);
                this.pen.Brush = new SolidBrush(settingPan.GetColor());
                this.cacheColor = ColorButtons.蓝色;
                this.cacheColorValue = settingPan.GetColor();
            });

            settingPan.AddPurposeHandler((x, y) =>
            {
                settingPan.SetColorButtonOn(ColorButtons.紫色);
                this.pen.Brush = new SolidBrush(settingPan.GetColor());
                this.cacheColor = ColorButtons.紫色;
                this.cacheColorValue = settingPan.GetColor();
            });

            settingPan.AddBlackHandler((x, y) =>
            {
                settingPan.SetColorButtonOn(ColorButtons.黑色);
                this.pen.Brush = new SolidBrush(settingPan.GetColor());
                this.cacheColor = ColorButtons.黑色;
                this.cacheColorValue = settingPan.GetColor();
            });

            settingPan.AddWhiteHandler((x, y) =>
            {
                settingPan.SetColorButtonOn(ColorButtons.白色);
                this.pen.Brush = new SolidBrush(settingPan.GetColor());
                this.cacheColor = ColorButtons.白色;
                this.cacheColorValue = settingPan.GetColor();
            });
        }


        /// <summary>
        /// 获取完成事件
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        private EventHandler GetFinishBtnHandler(Rectangle region)
        {
            return (x, y) =>
            {
                if (region.Width != 0 && region.Height != 0)
                {                   
                    var part = this.results.Peek().Clone(region, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    Clipboard.Clear();
                    Clipboard.SetImage(part);
                }
                this.Visible = false;
                this.WindowState = FormWindowState.Minimized;
                Reset();
                release();
            };
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        private void release()
        {
            this.orgin = null;
            this.pictureBox1.Image = null;
            pictureBox1.Controls.Clear();
            this.bitmaps.Clear();
            this.results.Clear();
        }

        private void Reset()
        {
            cap = false;
            sniff = false;
            rectangle = false;
            circle = false;
            arrow = false;
            drawString = false;
            PEN = false;
            line = false;

            pen.Brush = new SolidBrush(Color.Red);
            pen.Width = 2;
        }

        private void AdjustTobMosaic(Bitmap bitmap, Rectangle area, int effectWidth)
        {
            // 差异最多的就是以照一定范围取样 玩之后直接去下一个范围
            for (int heightOfffset = area.Y; heightOfffset < area.Y+ area.Height; heightOfffset += effectWidth)
            {
                for (int widthOffset = area.X; widthOffset < area.X + area.Width; widthOffset += effectWidth)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    for (int x = widthOffset; (x < widthOffset + effectWidth && x <area.X +  area.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < area.Y +  area.Height); y++)
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
