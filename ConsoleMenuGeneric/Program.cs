using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            EchelonLib.Menu m1 = new EchelonLib.Menu("New Game", "Load Game", "About", "Exit");
            switch (m1.Display(10, 5, true))
            {
                case 0:
                    {
                        Console.WriteLine("You selected Index 0");
                    }
                    break;
                case 1:
                    {
                        Console.WriteLine("You selected Index 1");
                    }
                    break;
                case 2:
                    {
                        Console.WriteLine("You selected Index 2");
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("You selected Index 3");
                    }
                    break;
            }
            Console.ReadLine();
        }
    }
}

namespace EchelonLib
{
    class Menu
    {
        /// <summary>
        /// A console menu with keyboard controls
        /// </summary>
        /// <param name="values"> Menu paramaters as Type.</param>

        private string[] _values;
        private static ConsoleColor
            ItemFg = Console.BackgroundColor,
            ItemBg = Console.ForegroundColor,
            uItemFg = Console.ForegroundColor,
            uItemBg = Console.BackgroundColor,
            cFg, cBg;


        public Menu(params string[] values)
        {
            _values = new string[values.Length];
            values.CopyTo(_values, 0);
            cFg = Console.ForegroundColor;
            cBg = Console.BackgroundColor;
        }

        public Menu(ConsoleColor Fg, ConsoleColor Bg, params string[] values)
        {
            _values = new string[values.Length];
            values.CopyTo(_values, 0);
            cFg = Console.ForegroundColor;
            cBg = Console.BackgroundColor;
            ItemFg = Fg;
            ItemBg = Bg;
        }
        public Menu(ConsoleColor Fg, ConsoleColor Bg, ConsoleColor uFg, ConsoleColor uBg, params string[] values)
        {
            _values = new string[values.Length];
            values.CopyTo(_values, 0);
            cFg = Console.ForegroundColor;
            cBg = Console.BackgroundColor;
            ItemFg = Fg;
            ItemBg = Bg;
            uItemFg = uFg;
            uItemBg = uBg;
        }

        public int Display(int x = 0, int y = 0)
        {
            return Display(x, y, false);
        }

        public int Display(int x = 0, int y = 0, bool arrow = false)
        {
            bool isEnded = false;
            int selectedindex = 0;
            int RowIndex = y;
            int RowLen = 0;
            string ex = "";
            Console.CursorVisible = false;
            if (arrow) Io.Wat('>', x - 2, RowIndex);
            do
            {
                Console.Clear();
                if (arrow) Io.Wat('>', x - 2, RowIndex + selectedindex);
                foreach (var row in _values)
                {
                    Console.ForegroundColor = uItemFg;
                    Console.BackgroundColor = uItemBg;
                    if (RowIndex == selectedindex)
                    {
                        Console.ForegroundColor = ItemFg;
                        Console.BackgroundColor = ItemBg;
                    }
                    Io.Wat(row, x, RowIndex++);
                    Console.ForegroundColor = cFg;
                    Console.BackgroundColor = cBg;
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (selectedindex == 0)
                                selectedindex = _values.Length - 1;
                            else
                                selectedindex--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (selectedindex == _values.Length - 1)
                                selectedindex = 0;
                            else
                                selectedindex++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            isEnded = true;
                        }
                        break;
                }
                RowIndex = y;
            } while (!isEnded);
            Console.CursorVisible = true;
            Console.Clear();
            return selectedindex;
        }
    }

    public class Io
    {
        public static void Wat<Type>(Type arg, int x, int y)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;

            Console.Write(arg.ToString());
        }
    }
}
