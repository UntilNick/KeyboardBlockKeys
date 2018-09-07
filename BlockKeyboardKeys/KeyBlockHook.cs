namespace BlockKeyboardKeys
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class KeyBlockHook : IDisposable
    {
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static readonly LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private bool _isDisposed;
        private readonly bool True = true, False = false;

        private const int WH_KEYBOARD_LL = 0xD;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(WH_KEYBOARD_LL, proc, NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public void InstallHook() => _hookID = SetHook(_proc);

        public void Dispose(bool disposing)
        {
            if (this._isDisposed)
            {
                return;
            }

            NativeMethods.UnhookWindowsHookEx(_hookID);
            this._isDisposed = this.True;
        }

        ~KeyBlockHook()
        {
            this.Dispose(this.False);
        }

        public void Dispose()
        {
            this.Dispose(this.True);
            try
            {
                GC.SuppressFinalize(this);
            }
            catch (ArgumentNullException) { }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                var vkCode = Marshal.ReadInt32(lParam);
                if (((nCode >= 0) && (wParam == (IntPtr)WM_KEYDOWN)) || (wParam == (IntPtr)WM_SYSKEYDOWN))
                {
                    if (downKeys.Contains((Keys)vkCode))
                    {
                        Console.WriteLine($"Клавиша: {Convert.ToString((Keys)vkCode)} Блокирована!");
                        return (IntPtr)1; // Данная функция возвращает (IntPtr)1 - это и есть блокировка функции.
                    }
                }
            }
            catch (AccessViolationException) { }
            return NativeMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        /* Список клавиш для блокировки */
        static readonly List<Keys> downKeys = new List<Keys>
        {
           Keys.LWin,
           Keys.RWin,
           Keys.Alt,
           Keys.Tab,
           Keys.CapsLock,
           Keys.Back,
           Keys.ControlKey,
           Keys.Space,
           Keys.LShiftKey,
           Keys.RShiftKey,
           Keys.Q,
           Keys.W,
           Keys.E,
           Keys.R,
           Keys.T,
           Keys.Y,
           Keys.U,
           Keys.I,
           Keys.O,
           Keys.P,
           Keys.A,
           Keys.S,
           Keys.D,
           Keys.F,
           Keys.G,
           Keys.H,
           Keys.J,
           Keys.K,
           Keys.L,
           Keys.Z,
           Keys.X,
           Keys.C,
           Keys.V,
           Keys.B,
           Keys.N,
           Keys.M,
           Keys.Oem1,
           Keys.Oem102,
           Keys.Oem2,
           Keys.Oem3,
           Keys.Oem4,
           Keys.Oem5,
           Keys.Oem6,
           Keys.Oem7,
           Keys.Oem8,
           Keys.OemBackslash,
           Keys.OemClear,
           Keys.OemCloseBrackets,
           Keys.Oemcomma,
           Keys.OemMinus,
           Keys.OemOpenBrackets,
           Keys.OemPeriod,
           Keys.OemPipe,
           Keys.Oemplus,
           Keys.OemQuestion,
           Keys.OemQuotes,
           Keys.OemSemicolon,
           Keys.Oemtilde,
           Keys.Pa1,
           Keys.Packet,
           Keys.Play,
           Keys.Print,
           Keys.PrintScreen,
           Keys.Prior,
           Keys.ProcessKey,
           Keys.Next,
           Keys.Multiply,
           Keys.Modifiers,
           Keys.MediaStop,
           Keys.MediaPreviousTrack,
           Keys.MediaPlayPause,
           Keys.MediaNextTrack,
           Keys.Scroll,
           Keys.Select,
           Keys.SelectMedia,
           Keys.Separator,
           Keys.Sleep,
           Keys.Snapshot,
           Keys.Subtract,
           Keys.Add,
           Keys.Apps,
           Keys.Attn,
           Keys.BrowserBack,
           Keys.BrowserFavorites,
           Keys.BrowserForward,
           Keys.BrowserHome,
           Keys.BrowserRefresh,
           Keys.BrowserSearch,
           Keys.BrowserStop,
           Keys.Clear,
           Keys.Crsel,
           Keys.End,
           Keys.Enter,
           Keys.EraseEof,
           Keys.Escape,
           Keys.Execute,
           Keys.Exsel,
           Keys.F1,
           Keys.F2,
           Keys.F3,
           Keys.F4,
           Keys.F5,
           Keys.F6,
           Keys.F7,
           Keys.F8,
           Keys.F9,
           Keys.F10,
           Keys.F11,
           Keys.F12,
           Keys.F13,
           Keys.F14,
           Keys.F15,
           Keys.F16,
           Keys.F17,
           Keys.F18,
           Keys.F19,
           Keys.F20,
           Keys.F21,
           Keys.F22,
           Keys.F23,
           Keys.F24,
           Keys.D0,
           Keys.D1,
           Keys.D2,
           Keys.D3,
           Keys.D4,
           Keys.D5,
           Keys.D6,
           Keys.D7,
           Keys.D8,
           Keys.D9,
           Keys.Delete,
           Keys.Divide,
           Keys.Down,
           Keys.Decimal,
           Keys.FinalMode,
           Keys.HanguelMode,
           Keys.HangulMode,
           Keys.HanjaMode,
           Keys.Help,
           Keys.IMEAccept,
           Keys.IMEAceept,
           Keys.IMEConvert,
           Keys.IMEModeChange,
           Keys.IMENonconvert,
           Keys.Insert,
           Keys.Home,
           Keys.Control,
           Keys.LControlKey,
           Keys.RControlKey,
           Keys.NumLock,
           Keys.NumPad0,
           Keys.NumPad1,
           Keys.NumPad2,
           Keys.NumPad3,
           Keys.NumPad4,
           Keys.NumPad5,
           Keys.NumPad6,
           Keys.NumPad7,
           Keys.NumPad8,
           Keys.NumPad9,
           Keys.JunjaMode,
           Keys.KanaMode,
           Keys.KanjiMode,
           Keys.KeyCode,
           Keys.LaunchApplication1,
           Keys.LaunchApplication2,
           Keys.LaunchMail,
           Keys.Left,
           Keys.Right,
           Keys.Up,
           Keys.PageUp,
           Keys.VolumeUp,
           Keys.LineFeed,
           Keys.LMenu,
           Keys.RMenu,
           Keys.MButton,
           Keys.MediaNextTrack,
           Keys.XButton1,
           Keys.Zoom,
           Keys.NoName
           //Keys.XButton2
           //Keys.LButton
        };

    }
}