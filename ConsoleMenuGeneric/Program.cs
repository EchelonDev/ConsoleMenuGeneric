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
            Console.BufferWidth = Console.WindowWidth = 40;
            Console.BufferHeight = Console.WindowHeight = 20;            
            Menu m1 = new Menu(ConsoleColor.Black,ConsoleColor.Green,ConsoleColor.Green,ConsoleColor.Black,"New Game","Continue Game", "About", "Exit");
            Io.Border(ConsoleColor.Green);
            m1.setDisplayMethod(true);
            switch (m1.Display(17, 6,true))
            {
                case 0:
                    {
                        Io.Border(ConsoleColor.Red);
                        Io.Wat("Game is About to start", Io.centerize("Game is About to start"), 6,ConsoleColor.Red);
                    }
                    break;
                case 1:
                    {
                        Io.Border(ConsoleColor.Red);
                        Io.Wat("Game is About to start", Io.centerize("Game is About to start"), 6, ConsoleColor.Red);
                    }
                    break;
                case 2:
                    {
                        Io.Border(ConsoleColor.Cyan);
                        Io.Wat("About", Io.centerize("About") , 2, ConsoleColor.Cyan);
                    }
                    break;
                case 3:
                    {
                        Io.Border(ConsoleColor.Yellow);
                        Io.Wat("The program will now exit.", Io.centerize("The program will now exit."), 2, ConsoleColor.Yellow);
                        System.Threading.Thread.Sleep(1000);
                        return;
                    }
                    break;                
            }
            Console.ReadLine();
        }
    }
}

namespace EchelonLib
{
    /// <summary>
    /// --------- TODO ---------
    /// 1 - DisplayMethod for justify or centered menu text. - (DONE KINDA)
    /// 2 - Make unselectable menu buttons.
    /// 
    /// --------- Notes ---------
    /// 1 - Consider not using Dmethod enum cuz not user-friendly (DONE)
    /// </summary>
    class Menu
    {
        /// <summary>
        /// A console menu with keyboard controls
        /// </summary>        

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
       
        public void setDisplayMethod(bool d)
        {
            if(d == true) { 
                int longest = _values.OrderByDescending(s => s.Length).First().Length;
                for (int i = 0; i < _values.Length; i++)
                {
                    int spacecountper = (longest - _values[i].Length) / 2;
                    string last = "";
                    if (_values[i].Length % 2 == 0)
                    {
                        last += " ";
                    }
                    for (int q = 0; q < spacecountper; q++)
                    {
                        last += " ";
                    }
                    last += _values[i];
                    for (int q = 0; q < spacecountper; q++)
                    {
                        last += " ";
                    }
                    if(_values[i].Length % 2 != 0)
                    {
                        last += " ";
                    }
                    _values[i] = last;
                }
            }
        }

        public int Display(int x, int y) => Display(x, y, false);

        public int Display(int x, int y, bool arrow)
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
        public static int centerize<Type>(Type arg)
        {
            return (Console.WindowWidth / 2) - arg.ToString().Length / 2;
        }
        public static void Wat<Type>(Type arg, int x, int y)
        {
            Console.CursorLeft = x; 
            Console.CursorTop = y;

            Console.Write(arg.ToString());
        }

        public static void Wat<Type>(Type arg, int x, int y,ConsoleColor clr)
        {
            ConsoleColor def = Console.ForegroundColor;
            Console.CursorLeft = x;
            Console.CursorTop = y;

            Console.ForegroundColor = clr;
            Console.Write(arg.ToString());
            Console.ForegroundColor = def;
        }
        public static void Border2()
        {
            int yMax = Console.WindowHeight;
            int xMax = Console.WindowWidth;
            char[,] characters = new char[Console.WindowWidth, Console.WindowHeight];

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                for (int j = 0; j < Console.WindowHeight; j++)
                {
                    char currentChar = ' ';

                    if ((i == 0) || (i == Console.WindowWidth - 1))
                    {
                        currentChar = '║';
                    }
                    else
                    {
                        if ((j == 0) || (j == Console.WindowHeight - 1))
                        {
                            currentChar = '═';
                        }
                    }

                    characters[i, j] = currentChar;
                }
            }

            characters[0, 0] = '╔';
            characters[Console.WindowWidth - 1, 0] = '╗';
            characters[0, Console.WindowHeight - 1] = '╚';
            characters[Console.WindowWidth - 1, Console.WindowHeight - 1] = '╝';

            for (int y = 0; y < yMax; y++)
            {
                string line = string.Empty;
                for (int x = 0; x < xMax; x++)
                {
                    line += characters[x, y];
                }
                Console.SetCursorPosition(0, y);
                Console.Write(line);
            }
            Console.SetCursorPosition(0, 0);
        }    
        public static void Border(ConsoleColor d)
        {
            Console.CursorVisible = false;
            // TODO : Fix this crap of shitbag, shit of crapbag or bag of crapshit... pls...
            int lenX = Console.WindowWidth;
            int lenY = Console.WindowHeight;
            ConsoleColor def = Console.ForegroundColor;
            Console.ForegroundColor = d;
            
            
            for (int i = 1; i < lenY - 1; i++)
                Io.Wat('║', lenX - 1, i);            
            for (int i = 1; i < lenY - 1; i++)
                Io.Wat('║', 0, i);
            Io.Wat('╚', 0, lenY - 1);
            Io.Wat('╝', lenX - 1, lenY - 1);
            for (int i = 1; i < lenX - 1; i++)
                Io.Wat('═', i, 0);
            for (int i = 1; i < lenX - 1; i++)
                Io.Wat('═', i, lenY - 2);
            Io.Wat('╔', 0, 0);
            Io.Wat('╗', lenX - 1, 0);

            //NOTE TO SELF :
            //When writing the last  corner Buffer area shifts 1 down messing the layout
            //Try to fix it with MoveBufferArea or find another solution
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = def;
        }
    }
}
