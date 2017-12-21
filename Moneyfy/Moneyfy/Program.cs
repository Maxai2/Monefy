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
            Functions.getInstance().TemplateCatName();
            Console.CursorVisible = false;
            Functions.getInstance().AddCol();
            //Functions.getInstance().CatNameStatusLine(100, 50, 5, 67, 34, 56, 34, 2, 4, 6, 23, 5);

            Functions.getInstance().DrawFrame();
            int select = 0;


            while (true)
            {
                Functions.getInstance().TabGo(select);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Tab:
                        //select = select < 5 ? ++select : 0;
                        if (select < 4)
                            select++;
                        else
                            select = 0;
                        break;
                    case ConsoleKey.Enter:
                        if (select == 0)
                        {
                            Functions.getInstance().ReportWindow();
                            Console.Clear();
                        }
                        else
                        if (select == 2)
                        {
                            Functions.getInstance().SettingsWindow();
                            Console.Clear();
                        }
                        break;
                    case ConsoleKey.F10:
                        if (!Functions.getInstance().Exit())
                            goto NOTEXIT;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Clear();
                        Environment.Exit(0);
                        NOTEXIT:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Clear();
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