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
            Functions.getInstance().TemplateName();
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
                        //select = select < 5 ? ++select : 0;
                        if (select < 4)
                            select++;
                        else
                            select = 0;
                        break;
                    case ConsoleKey.Enter:
                        switch (select)
                        {
                            case 0:
                                Functions.getInstance().ReportWindow();
                                Console.Clear();
                                break;
                            case 1:
                                Functions.getInstance().TransferWindow();
                                Console.Clear();
                                break;
                            case 2:
                                Functions.getInstance().SettingsWindow();
                                Console.Clear();
                                break;
                            case 3:
                            case 4:
                                Functions.getInstance().AddSubWindow(select == 3 ? 's' : 'a');
                                Console.Clear();
                                break;
                            default:
                                break;
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