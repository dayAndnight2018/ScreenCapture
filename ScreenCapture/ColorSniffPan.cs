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
    public partial class ColorSniffPan : UserControl
    {
        public ColorSniffPan()
        {
            InitializeComponent();
        }

        public ColorSniffPan(String dex, string hex)
        {
            InitializeComponent();
            this.dexTbx.Text = dex;
            this.HexTbx.Text = hex;
        }

        public String GetDex()
        {
            return this.dexTbx.Text;
        }

        public String GetHex()
        {
            return this.HexTbx.Text;
        }

        public void AddHandler(EventHandler handler1, EventHandler handler2)
        {
            this.DexBtn.Click += handler1;
            this.hexBtn.Click += handler2;
        }
    }
}
