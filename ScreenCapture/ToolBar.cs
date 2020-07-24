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
    public partial class ToolBar : UserControl
    {
        public ToolBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddExitEventHandler(EventHandler handler)
        {
            this.exitBtn.Click += handler;
        }


        /// <summary>
        /// 完成事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddFinfishBtnHandler(EventHandler handler)
        {
            this.finishBtn.Click += handler;
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddSaveBtnHandler(EventHandler handler)
        {
            this.saveBtn.Click += handler;
        }

        /// <summary>
        /// 矩形事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddRectangleBtnHandler(EventHandler handler)
        {
            this.rectangleBtn.Click += handler;
        }

        /// <summary>
        /// 箭头事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddArrowBtnHandler(EventHandler handler)
        {
            this.arrowBtn.Click += handler;
        }

        /// <summary>
        /// 矩形按钮颜色
        /// </summary>
        /// <param name="c"></param>
        public void SetRectangleBtnColor(Color c)
        {
            this.rectangleBtn.BackColor = c;
        }

        /// <summary>
        /// 箭头按钮颜色
        /// </summary>
        /// <param name="c"></param>
        public void SetArrowBtnColor(Color c)
        {
            this.arrowBtn.BackColor = c;
        }

        /// <summary>
        /// 圆圈按钮颜色
        /// </summary>
        /// <param name="c"></param>
        public void SetCircleBtnColor(Color c)
        {
            this.circleBtn.BackColor = c;
        }

        /// <summary>
        /// 马赛克背景色
        /// </summary>
        /// <param name="c"></param>
        public void SetLineBtnColor(Color c)
        {
            this.drawBtn.BackColor = c;
        }

        /// <summary>
        /// 撤销按钮事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddCancelBtnHandler(EventHandler handler)
        {
            this.cancelBtn.Click += handler;
        }

        /// <summary>
        /// 圆圈按钮事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddCircleBtnHandler(EventHandler handler)
        {
            this.circleBtn.Click += handler;
        }

        /// <summary>
        /// 马赛克事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddLineBtnHandler(EventHandler handler)
        {
            this.drawBtn.Click += handler;
        }

        /// <summary>
        /// 清除按钮事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddCleanBtnHandler(EventHandler handler)
        {
            this.clearBtn.Click += handler;
        }

        public void AddPenBtnHandler(EventHandler handler)
        {
            this.penBtn.Click += handler;
        }

        public void SetPenBtnColor(Color c)
        {
            this.penBtn.BackColor = c;
        }

        public void AddWordBtnHandler(EventHandler handler)
        {
            this.textBtn.Click += handler;
        }

        public void SetWordBtnColor(Color c)
        {
            this.textBtn.BackColor = c;
        }

    }
}
