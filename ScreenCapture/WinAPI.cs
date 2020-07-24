using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture
{
    public class WinAPI
    {

        public const int WM_KEYDOWN = 0x100;

        public const int WM_KEYUP = 0x101;

        public const int WM_SYSKEYDOWN = 0x104;

        public const int WM_SYSKEYUP = 0x105;

        public const int WH_KEYBOARD_LL = 13;


        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        //安装钩子的函数 

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //卸下钩子的函数 

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern bool UnhookWindowsHookEx(int idHook);

        //下一个钩挂的函数 

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        [DllImport("user32")]

        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]

        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
