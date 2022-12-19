using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    internal static class ThisConsole
    {
        private static readonly int HIDE = 0;
        private static readonly int SHOW = 5;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Hide()
        {
            ShowWindow(GetConsoleWindow(), HIDE);
        }

        public static void Show()
        {
            ShowWindow(GetConsoleWindow(), SHOW);
        }
    }
}
