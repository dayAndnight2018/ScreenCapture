﻿using ScreenCapture;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCapture
{
    public class KeyboardHook
    {
        int hHook;

        WinAPI.HookProc KeyboardHookDelegate;

        public event KeyEventHandler OnKeyDownEvent;

        public event KeyEventHandler OnKeyUpEvent;

        public event KeyPressEventHandler OnKeyPressEvent;

        public KeyboardHook() { }

        public void SetHook()

        {

            KeyboardHookDelegate = new WinAPI.HookProc(KeyboardHookProc);

            Process cProcess = Process.GetCurrentProcess();

            ProcessModule cModule = cProcess.MainModule;

            var mh = WinAPI.GetModuleHandle(cModule.ModuleName);

            hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_KEYBOARD_LL, KeyboardHookDelegate, mh, 0);

        }

        public void UnHook()

        {

            WinAPI.UnhookWindowsHookEx(hHook);

        }

        private List<Keys> preKeysList = new List<Keys>();//存放被按下的控制键，用来生成具体的键

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)

        {

            //如果该消息被丢弃（nCode<0）或者没有事件绑定处理程序则不会触发事件

            if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))

            {

                KeyboardHookStruct KeyDataFromHook = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                Keys keyData = (Keys)KeyDataFromHook.vkCode;

                //按下控制键

                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == WinAPI.WM_KEYDOWN || wParam == WinAPI.WM_SYSKEYDOWN))

                {

                    if (IsCtrlAltShiftKeys(keyData) && preKeysList.IndexOf(keyData) == -1)

                    {

                        preKeysList.Add(keyData);

                    }

                }

                //WM_KEYDOWN和WM_SYSKEYDOWN消息，将会引发OnKeyDownEvent事件

                if (OnKeyDownEvent != null && (wParam == WinAPI.WM_KEYDOWN || wParam == WinAPI.WM_SYSKEYDOWN))

                {

                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));

                    OnKeyDownEvent(this, e);

                }

                //WM_KEYDOWN消息将引发OnKeyPressEvent 

                if (OnKeyPressEvent != null && wParam == WinAPI.WM_KEYDOWN)

                {

                    byte[] keyState = new byte[256];

                    WinAPI.GetKeyboardState(keyState);

                    byte[] inBuffer = new byte[2];

                    if (WinAPI.ToAscii(KeyDataFromHook.vkCode, KeyDataFromHook.scanCode, keyState, inBuffer, KeyDataFromHook.flags) == 1)

                    {

                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);

                        OnKeyPressEvent(this, e);

                    }

                }

                //松开控制键

                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == WinAPI.WM_KEYUP || wParam == WinAPI.WM_SYSKEYUP))

                {

                    if (IsCtrlAltShiftKeys(keyData))

                    {

                        for (int i = preKeysList.Count - 1; i >= 0; i--)

                        {

                            if (preKeysList[i] == keyData) { preKeysList.RemoveAt(i); }

                        }

                    }

                }

                //WM_KEYUP和WM_SYSKEYUP消息，将引发OnKeyUpEvent事件 

                if (OnKeyUpEvent != null && (wParam == WinAPI.WM_KEYUP || wParam == WinAPI.WM_SYSKEYUP))

                {

                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));

                    OnKeyUpEvent(this, e);

                }

            }

            return WinAPI.CallNextHookEx(hHook, nCode, wParam, lParam);

        }

        //根据已经按下的控制键生成key

        private Keys GetDownKeys(Keys key)

        {

            Keys rtnKey = Keys.None;

            foreach (Keys i in preKeysList)

            {

                if (i == Keys.LControlKey || i == Keys.RControlKey) { rtnKey = rtnKey | Keys.Control; }

                if (i == Keys.LMenu || i == Keys.RMenu) { rtnKey = rtnKey | Keys.Alt; }

                if (i == Keys.LShiftKey || i == Keys.RShiftKey) { rtnKey = rtnKey | Keys.Shift; }

            }

            return rtnKey | key;

        }

        private Boolean IsCtrlAltShiftKeys(Keys key)

        {

            if (key == Keys.LControlKey || key == Keys.RControlKey || key == Keys.LMenu || key == Keys.RMenu || key == Keys.LShiftKey || key == Keys.RShiftKey) { return true; }

            return false;

        }

    }
}

