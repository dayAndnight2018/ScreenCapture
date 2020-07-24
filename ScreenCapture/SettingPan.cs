using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCapture
{
    public enum SizeButtons
    {
        小号 = 1,
        中号 = 2,
        大号 = 3
    }

    public enum ColorButtons
    {
        红色 = 1,
        橙色 = 2,
        黄色 = 3,
        绿色 = 4,
        蓝色 = 5,
        紫色 = 6,
        黑色 = 7,
        白色 = 8
    }

    public partial class SettingPan : UserControl
    {
        private Color color = Color.Red;
        private int fontSize = 5;

        public Color GetColor()
        {
            return color;
        }

        public int GetFontSize()
        {
            return fontSize;
        }

        public SettingPan()
        {
            InitializeComponent();
        }

        public SettingPan(SizeButtons size, ColorButtons color)
        {
            InitializeComponent();
            SetSizeButtonOn(size);
            SetColorButtonOn(color);
        }

        public void AddSmallBtnHandler(EventHandler handler)
        {
            this.smallBtn.Click += handler;
        }

        public void AddMiddleBtnHandler(EventHandler handler)
        {
            this.middleBtn.Click += handler;
        }

        public void AddBigBtnHandler(EventHandler handler)
        {
            this.bigBtn.Click += handler;
        }

        public void AddRedBtnHandler(EventHandler handler)
        {
            this.redBtn.Click += handler;
        }

        public void AddOrangeBtnHandler(EventHandler handler)
        {
            this.orangeBtn.Click += handler;
        }

        public void AddYellowHandler(EventHandler handler)
        {
            this.yellowBtn.Click += handler;
        }

        public void AddBlueHandler(EventHandler handler)
        {
            this.blueBtn.Click += handler;
        }

        public void AddPurposeHandler(EventHandler handler)
        {
            this.purposeBtn.Click += handler;
        }

        public void AddBlackHandler(EventHandler handler)
        {
            this.blackBtn.Click += handler;
        }

        public void AddGreenHandler(EventHandler handler)
        {
            this.greenBtn.Click += handler;
        }

        public void AddWhiteHandler(EventHandler handler)
        {
            this.whiteBtn.Click += handler;
        }

        public void SetSizeButtonOn(SizeButtons button)
        {
            Button b = null;
            switch (button)
            {
                case SizeButtons.小号:
                    b = smallBtn;
                    fontSize = 2;
                    break;
                case SizeButtons.中号:
                    b = middleBtn;
                    fontSize = 5;
                    break;
                case SizeButtons.大号:
                    fontSize = 10;
                    b = bigBtn;
                    break;
            }

            b.BackColor = Color.Wheat;

            Button[] buttons = {
               smallBtn,middleBtn,bigBtn
            };

            foreach (Button c in buttons)
            {
                if (c != b)
                {
                    c.BackColor = Color.Transparent;
                }
            }
        }


        public void SetColorButtonOn(ColorButtons button)
        {
            Button b = null;
            switch (button)
            {
                case ColorButtons.红色:
                    b = redBtn;
                    break;
                case ColorButtons.橙色:
                    b = orangeBtn;
                    break;
                case ColorButtons.黄色:
                    b = yellowBtn;
                    break;
                case ColorButtons.绿色:
                    b = greenBtn;
                    break;
                case ColorButtons.蓝色:
                    b = blueBtn;
                    break;
                case ColorButtons.紫色:
                    b = purposeBtn;
                    break;
                case ColorButtons.黑色:
                    b = blackBtn;
                    break;
                case ColorButtons.白色:
                    b = whiteBtn;
                    break;
            }

            color = b.ForeColor;

            b.BackColor = Color.Wheat;

            Button[] buttons = {
                redBtn,
                orangeBtn,
                yellowBtn,
                greenBtn,
                blueBtn,
                purposeBtn,
                blackBtn,
                whiteBtn
            };
            foreach (Button c in buttons)
            {
                if (c != b)
                {
                    c.BackColor = Color.Transparent;
                }
            }
        }
    }
}
