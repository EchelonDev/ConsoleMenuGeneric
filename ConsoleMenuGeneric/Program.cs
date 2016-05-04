using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchelonLib;

namespace ConsoleMenuGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = Console.WindowWidth = 35;
            Console.BufferHeight = Console.WindowHeight;            
            Menu m1 = new Menu(ConsoleColor.Black,ConsoleColor.Green,ConsoleColor.Green,ConsoleColor.Black,"New Game", "Load Game", "About", "Exit");
            switch (m1.Display(12, 10, true))
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
            //Can we directly '=' two arrays to each other...
            // if yes get rid of these shiet
            _values = new string[values.Length];
            values.CopyTo(_values, 0);
            cFg = Console.ForegroundColor;
            cBg = Console.BackgroundColor;
        }

        public Menu(ConsoleColor Fg, ConsoleColor Bg, params string[] values)
        {
            //Can we directly '=' two arrays to each other...
            // if yes get rid of these shiet
            _values = new string[values.Length];
            values.CopyTo(_values, 0);
            cFg = Console.ForegroundColor;
            cBg = Console.BackgroundColor;
            ItemFg = Fg;
            ItemBg = Bg;
        }
        public Menu(ConsoleColor Fg, ConsoleColor Bg, ConsoleColor uFg, ConsoleColor uBg, params string[] values)
        {
            //Can we directly '=' two arrays to each other...
            // if yes get rid of these shiet
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
            Console.CursorVisible = false;
            do
            {   //Because Console.Clear() is gone, we can have border layouting the window in main process not here , before when clear was a thing in here            
                //I had to re-draw the border every clear. now it is not necerssary
                //AFTER re-writing the io.border (a.k.a. pile of crapbag) delete this message if you want to...
                //  --TLDR--
                //         ┌ This shiet is not necessary anymore
                //         │
                //         ↓
                //Io.Border();
                Console.ForegroundColor = uItemFg;
                if (arrow) Io.Wat('>', x - 2, RowIndex + selectedindex);
                Console.ForegroundColor = cFg;
                foreach (var row in _values)
                {
                    Console.ForegroundColor = uItemFg;
                    Console.BackgroundColor = uItemBg;
                    if (RowIndex == y + selectedindex)
                    {
                        Console.ForegroundColor = ItemFg;
                        Console.BackgroundColor = ItemBg;
                    }
                    else
                    {
                        if (arrow) Io.Wat(' ', x - 2, RowIndex);
                        // Delete other arrows cuz I don't want to have a Console.Clear().
                    }                    
                    Io.Wat(row, x, RowIndex++);
                    Console.ForegroundColor = cFg;
                    Console.BackgroundColor = cBg;
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        {
                            if (selectedindex == 0)
                                selectedindex = _values.Length - 1;
                            else
                                selectedindex--;
                        }
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        {
                            if (selectedindex == _values.Length - 1)
                                selectedindex = 0;
                            else
                                selectedindex++;
                        }
                        break;
                    case ConsoleKey.RightArrow:
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
        public static void Border()
        {
            // TODO : Fix this crap of shitbag, shit of crapbag or bag of crapshit... pls...
            int lenX = Console.WindowWidth;
            int lenY = Console.WindowHeight;
            Io.Wat('╔', 0, 0);
            Io.Wat('╚', 0, lenY - 1);
            Io.Wat('╝', lenX - 1, lenY - 1);
            //Io.Wat('╗', lenX - 1, 0);
            for (int i = 1; i < lenX - 1; i++)
            {
                Io.Wat('═', i, 0);
            }
        }
    }
}
