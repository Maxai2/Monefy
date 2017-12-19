using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Functions.getInstance().DrawFrame();
            int select = 0;

            while (true)
            {
                Functions.getInstance().TabGo(select);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Tab:
                        //select < 5 ? select++ : select = 0;
                        if (select < 4)
                            select++;
                        else
                            select = 0;
                        break;
                    case ConsoleKey.Enter:
                        if (select == 0)
                        {

                        }
                        break;
                    case ConsoleKey.F10:
                        if (!Functions.getInstance().Exit())
                            goto NOTEXIT;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(0, 35);
                        Environment.Exit(0);
                        NOTEXIT:
                        
                        break;
                    default:
                        break;
                }

                Functions.getInstance().DrawFrame();
            }


        }
    }
}
//--------------------------------------------------------