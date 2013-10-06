using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Clicker
{
    class Program
    {
        public static int ClickPrice = 1,
                          SecondPrice = 0;

        static void Main(string[] args)
        {
            Drawer.Init();
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(10);
                //Cookie();
                Console.WriteLine(MouseControl.GetCursorPosition().X + ", " + MouseControl.GetCursorPosition().Y);
                BuyObject(1);
            }
        }  

        static void Cookie()
        {
            MouseControl.SetCursorPosition(171, 384);

            MouseControl.MouseEvent(MouseControl.MouseEventFlags.LeftDown);
            MouseControl.MouseEvent(MouseControl.MouseEventFlags.LeftUp);
        }

        public static void BuyObject(int id)
        {
            MouseControl.SetCursorPosition(1225, 200 + id * 40);

            MouseControl.MouseEvent(MouseControl.MouseEventFlags.LeftDown);
            MouseControl.MouseEvent(MouseControl.MouseEventFlags.LeftUp);
        }

    }
}
