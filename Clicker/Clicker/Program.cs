using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;

namespace Clicker
{
    class Program
    {
        public static long[] BasePrices = new long[10] { 15, 100, 500, 3000, 10000, 40000, 200000, 1666666, 123456789, 4999999999 };
        public static long[] Prices = new long[10] { 15, 100, 500, 3000, 10000, 40000, 200000, 1666666, 123456789, 4999999999 };
        public static int[] ObjectsCount = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] PerSecondPrice = new double[10] { 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double ClickPrice = 1,
                             SecondPrice = 0;
        public static double CookiesCount = 0f;
        public static System.Timers.Timer Second;

        static void Main()
        {
            MessageBox.Show("");
            Second = new System.Timers.Timer(1000);
            Second.Elapsed += new ElapsedEventHandler(Second_Elapsed);
            Second.Start();
            int i = 0;
            while(true)
            {
                Thread.Sleep(5);
                Cookie();
                BuyThings();
                i++;
                if (i > 1000)
                    MessageBox.Show(SecondPrice + ", " + CookiesCount + ", " + ObjectsCount[0]);
            }
        }

        static void BuyThings()
        {
            long min = long.MaxValue;
            foreach (long price in Prices)
            {
                if (price < min)
                    min = price;
            }
            int a = GetBestThingToBuy();
            if (a < 0)
                return;
            BuyObject(a);
        }

        static void Second_Elapsed(object sender, ElapsedEventArgs e)
        {
            CookiesCount += SecondPrice;
        }

        public static int GetBestThingToBuy()
        {
            bool[] CanBuy = new bool[10];
            for(int i = 0; i < 10; i++)
                CanBuy[i] = Prices[i] < CookiesCount;
            for (int i = 9; i >= 0; i--)
                if (CanBuy[i])
                    return i + 1;
            return -1;
        }

        static void Cookie()
        {
            MouseControl.SetCursorPosition(171, 384);

            MouseControl.MouseEvent(MouseControl.MouseEventFlags.LeftDown);
            MouseControl.MouseEvent(MouseControl.MouseEventFlags.LeftUp);
            CookiesCount += ClickPrice;
        }

        public static void BuyObject(int id)
        {
            if (id <= 0)
                return;
            if (CookiesCount > Prices[id - 1])
            {
                MouseControl.SetCursorPosition(1000, 200 + id * 40);

                MouseControl.MouseEvent(MouseControl.MouseEventFlags.LeftDown);
                MouseControl.MouseEvent(MouseControl.MouseEventFlags.LeftUp);

                SecondPrice += PerSecondPrice[id - 1];
                CookiesCount -= Prices[id - 1];
                ObjectsCount[id - 1]++;
                Prices[id - 1] = BasePrices[id - 1] * Convert.ToInt64(Math.Pow(1.15, Convert.ToInt64(ObjectsCount[id - 1])));
            }
        }

    }
}
