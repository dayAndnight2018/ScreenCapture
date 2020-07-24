using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCapture
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            bool isRun = false;
            Mutex mutex = new Mutex(true, "My", out isRun);
            if(isRun)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }else
            {
                MessageBox.Show("程序已经运行！");
            }
            
        }
    }
}
