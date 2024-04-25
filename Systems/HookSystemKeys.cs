using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Diagnostics;
using System.Runtime.InteropServices;


// Для перечисления Keys 
using System.Windows.Forms; 


namespace ConectoWorkSpace
{
    /// <summary>
    /// Переключатель управления выполнения системных комбинаций клавиш Ctrl+Esc ... <para></para>
    /// WINAPI 32 [user32.dll kernel32.dll]<para></para>
    /// <para></para>
    /// http://www.sql.ru/Forum/actualthread.aspx?tid=632552
    /// </summary>
    class HookSystemKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYUP = 257;// Отпускание любой клавиши

        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        // Поддержка флагов состояния системных клавиш 
        static bool CtrlKey, AltKey, WinKey;
        private static void StateKey(int vkCode, IntPtr wParam)
        {
            switch ((Keys)vkCode)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    if (wParam == (IntPtr)WM_KEYUP)
                        CtrlKey = false;
                    else
                        CtrlKey = true;
                    break;
                case Keys.LMenu:
                case Keys.RMenu:
                    if (wParam == (IntPtr)WM_KEYUP)
                        AltKey = false;
                    else
                        AltKey = true;
                    break;
                case Keys.LWin:
                case Keys.RWin:
                    if (wParam == (IntPtr)WM_KEYUP)
                        WinKey = false;
                    else
                        WinKey = true;
                    break;
            }
        }

        // Общедоступная упаковка оригинала
        /// <summary>
        /// Отключение управления системных клавиш
        /// </summary>
        public static void FunHook()
        {
            _hookID = SetHook(_proc);
        }
        /// <summary>
        /// Включение управления системных клавиш
        /// </summary>
        public static void FunUnHook()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode,
            IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            StateKey(vkCode, wParam);
            if (WinKey                                      // Системная
                || CtrlKey && (Keys)vkCode == Keys.Escape   // Ctrl+Esc
                || AltKey && (Keys)vkCode == Keys.Escape    // Alt+Esc
                || AltKey && (Keys)vkCode == Keys.Tab)      // Alt+Tab
            {
                return (IntPtr)1;
            }
            else
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }

}
