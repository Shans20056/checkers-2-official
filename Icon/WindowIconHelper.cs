using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CheckersGame
{
    public static class WindowIconHelper
    {
        private const uint WM_SETICON = 0x80;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Устанавливает иконку для окна.
        /// </summary>
        /// <param name="hWnd">Дескриптор окна</param>
        /// <param name="iconPath">Путь к файлу .ico</param>
        public static void SetWindowIcon(IntPtr hWnd, string iconPath)
        {
            if (string.IsNullOrEmpty(iconPath))
                throw new ArgumentException("Путь к иконке не может быть пустым.", nameof(iconPath));

            if (!System.IO.File.Exists(iconPath))
                throw new System.IO.FileNotFoundException($"Иконка не найдена: {iconPath}");

            using (var icon = new Icon(iconPath))
            {
                IntPtr iconHandle = icon.Handle;
                // Устанавливаем большую иконку
                SendMessage(hWnd, WM_SETICON, new IntPtr(1), iconHandle);
                // Устанавливаем маленькую иконку
                SendMessage(hWnd, WM_SETICON, IntPtr.Zero, iconHandle);
            }
        }
    }
}
