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
    public partial class Settings : Form
    {
        private Form1 form;
        public Keys screenKeys { get; private set; }
        public Keys sniffKeys { get; private set;  }
        public Settings(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        public Settings(Form1 form, Keys screen, Keys sniff)
        {
            InitializeComponent();
            this.form = form;
            this.screenKeys = screen;
            this.sniffKeys = sniff;
            this.screenTbx.Text = screen.ToString();
            this.sniffTbx.Text = sniff.ToString() ;
        }

        private void sceenCbx_CheckedChanged(object sender, EventArgs e)
        {
            if(sceenCbx.Checked)
            {
                screenTbx.Enabled = true;
            }
            else
            {
                screenTbx.Enabled = false;
            }
        }

        private void sniffCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (sniffCbx.Checked)
            {
                sniffTbx.Enabled = true;
            }
            else
            {
                sniffTbx.Enabled = false;
            }
        }

        private void screenBtn_Click(object sender, EventArgs e)
        {
            form.setShortCutKeys(ShortCutKeysType.截图, screenKeys);
            form.SetNotify("截图助手", "截图热键设置成功！", ToolTipIcon.Info);
        }

        private void sniffBtn_Click(object sender, EventArgs e)
        {
            form.setShortCutKeys(ShortCutKeysType.吸取, sniffKeys);
            form.SetNotify("截图助手", "屏幕吸取热键设置成功！", ToolTipIcon.Info);
        }

        private void screenTbx_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            screenKeys = e.KeyData;
            screenTbx.Text = e.KeyData.ToString();
        }

        private void sniffTbx_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            sniffKeys = e.KeyData;
            sniffTbx.Text = e.KeyData.ToString();
        }
    }
}
