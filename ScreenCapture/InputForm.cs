using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCapture
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
            this.colorCbx.SelectedItem = "红色";
            this.textBox.ForeColor = Color.Red;
            this.fontCbx.SelectedItem = "中号";
            this.textBox.Font = new Font("宋体", 20);
        }

        public InputForm(DrawStringResult result)
        {
            InitializeComponent();
            this.colorCbx.Text = "";
            this.textBox.ForeColor = result.color;
            this.fontCbx.Text = "";
            this.textBox.Font = result.font;
            this.textBox.Text = result.Content;
        }

        private void colorCbx_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (this.colorCbx.SelectedItem.ToString())
            {
                case "红色":
                    this.textBox.ForeColor = Color.Red;
                    break;
                case "橙色":
                    this.textBox.ForeColor = Color.Orange;
                    break;
                case "黄色":
                    this.textBox.ForeColor = Color.Yellow;
                    break;
                case "绿色":
                    this.textBox.ForeColor = Color.Green;
                    break;
                case "蓝色":
                    this.textBox.ForeColor = Color.Blue;
                    break;
                case "紫色":
                    this.textBox.ForeColor = Color.Purple;
                    break;
                case "黑色":
                    this.textBox.ForeColor = Color.Black;
                    break;
                case "白色":
                    this.textBox.ForeColor = Color.White;
                    break;
            }
        }

        private void cacelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fontCbx_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (this.fontCbx.SelectedItem.ToString())
            {
                case "小号":
                    this.textBox.Font = new Font("宋体", 12);
                    break;
                case "中号":
                    this.textBox.Font = new Font("宋体", 20);
                    break;
                case "大号":
                    this.textBox.Font = new Font("宋体", 25);
                    break;
            }
        }

        public void AddConfirmHandler(EventHandler handler)
        {
            this.drawBtn.Click += handler;
        }

        public DrawStringResult SavePoint()
        {
            DrawStringResult result = new DrawStringResult();
            result.color = this.textBox.ForeColor;
            result.font = this.textBox.Font;
            result.Content = this.textBox.Text;
            return result;
        }
    }
}
