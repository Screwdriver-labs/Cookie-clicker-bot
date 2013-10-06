using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Clicker
{
    public static class Drawer
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        public static Graphics g;
        public static IntPtr desktopPtr;

        public static void Init()
        {
            desktopPtr = GetDC(IntPtr.Zero);
            g = Graphics.FromHdc(desktopPtr);
        }

        public static void DrawRectangle(Rectangle rect)
        {
            SolidBrush b = new SolidBrush(Color.White);
            g.FillRectangle(b, rect);

            g.Dispose();
            ReleaseDC(IntPtr.Zero, desktopPtr);
        }

        public static void DrawString(string str, PointF p)
        {
            Init();
            SolidBrush b = new SolidBrush(Color.White);
            Font f = new Font("Arial", 12f);
            
            g.DrawString(str, f, b, p);

            g.Dispose();
            ReleaseDC(IntPtr.Zero, desktopPtr);
        }
    }
}
