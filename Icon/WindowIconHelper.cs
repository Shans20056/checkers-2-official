using System;
using System.Runtime.InteropServices;

namespace CheckersGame
{
    public static class WindowIconHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private extern static bool SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETICON = 0x80;

        public static void SetWindowIcon(IntPtr windowHandle, string iconPath)
        {
            var icon = new System.Drawing.Icon(iconPath);

            // Устанавливаем маленькую иконку
            SendMessage(windowHandle, WM_SETICON, (IntPtr)0, icon.Handle);

            // Устанавливаем большую иконку
            SendMessage(windowHandle, WM_SETICON, (IntPtr)1, icon.Handle);
        }
    }
}
