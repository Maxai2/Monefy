using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    public class Functions
    {
        private static Functions instance;

        public static Functions getInstance()
        {
            if (instance == null)
                instance = new Functions();

            return instance;
        }
        //--------------------------------------------------------
        char[] DoubleLine = { (char)0x2554, (char)0x2550, (char)0x2557, (char)0x2551, (char)0x255A, (char)0x2550, (char)0x255D };

        char[] SingleLine = { (char)0x250C, (char)0x2500, (char)0x2510, (char)0x2502, (char)0x2514, (char)0x2500, (char)0x2518 };
        //--------------------------------------------------------
        public void Line(int x, int y, int length, ConsoleColor LineCol, char symbol = '—')
        {
            Console.ForegroundColor = LineCol;

            //char symb = symbol == 'h' ? '—' : '|';

            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.WriteLine(symbol);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
        //--------------------------------------------------------
        public void Clear(int x, int y, int length, int height)
        {

        }
        //--------------------------------------------------------
        public void Frame(char type, int x, int y, int height, int length, ConsoleColor frameForCol, ConsoleColor frameBackCol = ConsoleColor.Black, string name = "", ConsoleColor nameCol = ConsoleColor.Red)
        {
            int count = 0;

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = frameForCol;
            Console.BackgroundColor = frameBackCol;

            if (type == 'd')
                Console.Write(DoubleLine[count]);
            else
                Console.Write(SingleLine[count]);
            count++;

            for (int i = 0; i < length - 1; i++)
            {
                if (name != "" && i == (length - name.Length) / 2)
                {
                    Console.ForegroundColor = nameCol;
                    Console.Write(name);
                    Console.ForegroundColor = frameForCol;
                    i += name.Length;
                }
                else
                {
                    if (type == 'd')
                        Console.Write(DoubleLine[count]);
                    else
                        Console.Write(SingleLine[count]);
                }
            }
            count++;

            if (type == 'd')
                Console.Write(DoubleLine[count]);
            else
                Console.Write(SingleLine[count]);
            count++;

            for (int i = 0; i < height - 2; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (j == 0 || j == length - 1)
                    {
                        Console.SetCursorPosition(j + x, i + y + 1);

                        if (type == 'd')
                            Console.Write(DoubleLine[count]);
                        else
                            Console.Write(SingleLine[count]);
                    }
                }
            }
            count++;

            Console.SetCursorPosition(x, y + height - 2);
            if (type == 'd')
                Console.Write(DoubleLine[count]);
            else
                Console.Write(SingleLine[count]);
            count++;

            for (int i = 0; i < length - 2; i++)
            {
                if (type == 'd')
                    Console.Write(DoubleLine[count]);
                else
                    Console.Write(SingleLine[count]);
            }
            count++;

            if (type == 'd')
                Console.Write(DoubleLine[count]);
            else
                Console.Write(SingleLine[count]);
            count++;
        }
        //--------------------------------------------------------
        public enum MainFrame { MFChar = 'd', MFX = 15, MFY = 2, MFH = 30, MFL = 70, MFFC = ConsoleColor.Green, MFBC = ConsoleColor.Black, [StringValue("")]MONEFY, MFNC = ConsoleColor.Magenta }

        public enum ReportsFrame { RepFChar = 's', RepFX = MainFrame.MFX + 2, RepFY = MainFrame.MFY + 1, RepFH = 6, RepFL = 10, RepFFC = ConsoleColor.White, RepFBC = ConsoleColor.Black, [StringValue("")]Reports, RepFLL = 6, RepFLC = ConsoleColor.Red }

        public enum TransferFrame { TraFChar = 's', TraFX = MainFrame.MFL - 10, TraFY = MainFrame.MFY + 1, TraFH = 6, TraFL = 11, TraFFC = ConsoleColor.White, TraFBC = ConsoleColor.Black, [StringValue("")]Transfer, TraFLL = 6, TraFLC = ConsoleColor.White }

        public enum SettingsFrame { SetFChar = 's', SetFX = MainFrame.MFL + 2, SetFY = MainFrame.MFY + 1, SetFH = 6, SetFL = 11, SetFFC = ConsoleColor.White, SetFBC = ConsoleColor.Black, [StringValue("")] Settings, SetFDC = ConsoleColor.White  }

        public enum SubstractFrame { SubFChar = 's', SubFX = MainFrame.MFX + 4, SubFY = MainFrame.MFH - 4, SubFH = 4, SubFL = 12, SubFFC = ConsoleColor.White, SubFBC = ConsoleColor.Black, [StringValue("")]Substract, SubFLL = 5, SubFLC = ConsoleColor.Yellow }

        public enum AdditionFrame { AddFChar = 's', AddFX = MainFrame.MFL - 1, AddFY = MainFrame.MFH - 4, AddFH = 4, AddFL = 11, AddFFC = ConsoleColor.White, AddFBC = ConsoleColor.Black, [StringValue("")]Addition, AddFLL = 5, AddFLC = ConsoleColor.Yellow }

        public enum BalanceFrame { BalFChar = 'd', BalFX = (MainFrame.MFL / 2) + (MainFrame.MFX / 2), BalFY = MainFrame.MFH - 6, BalFH = 7, BalFL = 17, BalFFC = ConsoleColor.DarkGreen, BalFBC = ConsoleColor.Black, [StringValue("")]Balance }

        public enum ExitFrame { ExFChar = 'd', ExFX = (MainFrame.MFL / 2) + (MainFrame.MFX / 2) - 5, ExFY = MainFrame.MFH / 2, ExFH = 6, ExFL = 29, ExFFC = ConsoleColor.Black, ExFBC = ConsoleColor.Gray, [StringValue("")]Quit, ExFNC = ConsoleColor.Black }

        public enum DateParam { DateX = MainFrame.MFX + 25, DateY = MainFrame.MFY + 6, DateColF = ConsoleColor.Cyan };

        string[] CategName = { "Food", "Home", "Cafe", "Hygiene", "Sport", "Health", "Phone", "Clothes", "Taxi", "Entertainment", "Transport", "Car" };
        //--------------------------------------------------------
        public void TabGo(int sel)
        {
            switch (sel)
            {
                case 0: //Reports
                    Frame((char)ReportsFrame.RepFChar, (int)ReportsFrame.RepFX, (int)ReportsFrame.RepFY, (int)ReportsFrame.RepFH, (int)ReportsFrame.RepFL, (ConsoleColor)ReportsFrame.RepFFC, ConsoleColor.Gray, ReportsFrame.Reports.ToString());
                    break;
                case 1: //Transfer
                    Frame((char)TransferFrame.TraFChar, (int)TransferFrame.TraFX, (int)TransferFrame.TraFY, (int)TransferFrame.TraFH, (int)TransferFrame.TraFL, (ConsoleColor)TransferFrame.TraFFC, ConsoleColor.Gray, TransferFrame.Transfer.ToString());
                    break;
                case 2: //Settings
                    Frame((char)SettingsFrame.SetFChar, (int)SettingsFrame.SetFX, (int)SettingsFrame.SetFY, (int)SettingsFrame.SetFH, (int)SettingsFrame.SetFL, (ConsoleColor)SettingsFrame.SetFFC, ConsoleColor.Gray, SettingsFrame.Settings.ToString());
                    break;
                case 3: //Substract
                    Frame((char)SubstractFrame.SubFChar, (int)SubstractFrame.SubFX, (int)SubstractFrame.SubFY, (int)SubstractFrame.SubFH, (int)SubstractFrame.SubFL, (ConsoleColor)SubstractFrame.SubFFC, ConsoleColor.Gray, SubstractFrame.Substract.ToString());
                    break;
                case 4: //Add
                    Frame((char)AdditionFrame.AddFChar, (int)AdditionFrame.AddFX, (int)AdditionFrame.AddFY, (int)AdditionFrame.AddFH, (int)AdditionFrame.AddFL, (ConsoleColor)AdditionFrame.AddFFC, ConsoleColor.Gray, AdditionFrame.Addition.ToString());
                    break;

                default:
                    break;
            }
        }
        //--------------------------------------------------------
        public bool Exit()
        {
            Frame((char)ExitFrame.ExFChar, (int)ExitFrame.ExFX, (int)ExitFrame.ExFY, (int)ExitFrame.ExFH, (int)ExitFrame.ExFL, (int)ExitFrame.ExFFC, (ConsoleColor)ExitFrame.ExFBC, ExitFrame.Quit.ToString(), (ConsoleColor)ExitFrame.ExFNC);

            Console.SetCursorPosition((int)ExitFrame.ExFX + 1, (int)ExitFrame.ExFY + 1);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("Do you want to quit Monefy?");
            Line((int)ExitFrame.ExFX + 1, (int)ExitFrame.ExFY + 2, (int)ExitFrame.ExFL - 2, ConsoleColor.Black);

            Line((int)ExitFrame.ExFX + 1, (int)ExitFrame.ExFY + 3, (int)ExitFrame.ExFL - 2, ConsoleColor.Gray, ' ');

            bool sel = false;
            while (true)
            {
                Console.SetCursorPosition((int)ExitFrame.ExFX + 6, (int)ExitFrame.ExFY + 3);

                if (sel)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.Write("{ Yes }");

                Console.SetCursorPosition((int)ExitFrame.ExFX + 18, (int)ExitFrame.ExFY + 3);

                if (!sel)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine("[ No ]");

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.LeftArrow:
                        sel = !sel;
                        break;
                    case ConsoleKey.Enter:
                        if (sel)
                            return true;
                        else
                            return false;
                }
            }
        }
        //--------------------------------------------------------
        public string MonthToString(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return null;
            }
        }
        //--------------------------------------------------------
        public DateTime[] WeekBeginEnd(DateTime date)
        {
            DateTime[] BSDate = new DateTime[2];
            DateTime temp = date;

            while (temp.DayOfWeek != DayOfWeek.Monday)
            {
                temp = temp.AddDays(-1);
            }
            BSDate[0] = temp;
            temp = date;

            while (temp.DayOfWeek != DayOfWeek.Sunday)
            {
                temp = temp.AddDays(1);
            }
            BSDate[1] = temp;

            return BSDate;
        }
        //--------------------------------------------------------
        public void Date()
        {
            Console.SetCursorPosition((int)DateParam.DateX, (int)DateParam.DateY);
            Console.ForegroundColor = (ConsoleColor)DateParam.DateColF;

            Console.Write($"{DateTime.Today.DayOfWeek}, {DateTime.Today.Day} {MonthToString(DateTime.Today.Month)}");

            //Console.SetCursorPosition((int)DateParam.DateX, (int)DateParam.DateY + 1);
            //Console.WriteLine($"{WeekBeginEnd(DateTime.Today)[0].Day} - {WeekBeginEnd(DateTime.Today)[1].Day} {MonthToString(DateTime.Today.Month)}");

            //Console.SetCursorPosition((int)DateParam.DateX, (int)DateParam.DateY + 2);
            //Console.WriteLine($"{MonthToString(DateTime.Today.Month)}");

            //Console.SetCursorPosition((int)DateParam.DateX, (int)DateParam.DateY + 3);
            //Console.WriteLine($"{DateTime.Today.Year}");
        }
        //--------------------------------------------------------
        public void CategWrite()
        {
            int index = 0;

            int count = Application.Categories.Count;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            while (count != 0)
            {
                Console.SetCursorPosition((int)MainFrame.MFX + 1, (int)MainFrame.MFY + 7 + index);
                if (index < 9)
                    Console.Write(" ");
                else
                    Console.Write("");
                Console.WriteLine($"{index + 1}) {Application.Categories[index].Name}");
                index++;
                count--;
            }
        }
        //--------------------------------------------------------
        public void TemplateCatName()
        {
            for (int i = 0; i < CategName.Length; i++)
                Application.getInstance().AddCategories(CategName[i], Type.Income);
        }
        //--------------------------------------------------------
        public void DrawFrame()
        {
            //Main Frame
            Frame((char)MainFrame.MFChar, (int)MainFrame.MFX, (int)MainFrame.MFY, (int)MainFrame.MFH, (int)MainFrame.MFL, (ConsoleColor)MainFrame.MFFC, (ConsoleColor)MainFrame.MFBC, MainFrame.MONEFY.ToString(), (ConsoleColor)MainFrame.MFNC);

            //Reports Frame
            Frame((char)ReportsFrame.RepFChar, (int)ReportsFrame.RepFX, (int)ReportsFrame.RepFY, (int)ReportsFrame.RepFH, (int)ReportsFrame.RepFL, (ConsoleColor)ReportsFrame.RepFFC, (ConsoleColor)ReportsFrame.RepFBC, ReportsFrame.Reports.ToString());

            for (int i = 0; i < 3; i++)
                Line((int)ReportsFrame.RepFX + 2, (int)ReportsFrame.RepFY + 1 + i/*(i == 0 || i == 2 ? i : 0)*/, (int)ReportsFrame.RepFLL, (ConsoleColor)ReportsFrame.RepFLC);

            //Transfer Frame
            Frame((char)TransferFrame.TraFChar, (int)TransferFrame.TraFX, (int)TransferFrame.TraFY, (int)TransferFrame.TraFH, (int)TransferFrame.TraFL, (ConsoleColor)TransferFrame.TraFFC, (ConsoleColor)TransferFrame.TraFBC, TransferFrame.Transfer.ToString());

            Console.SetCursorPosition((int)TransferFrame.TraFX + 2, (int)TransferFrame.TraFY + 1);
            Console.WriteLine('<');
            Line((int)TransferFrame.TraFX + 3, (int)TransferFrame.TraFY + 1, (int)TransferFrame.TraFLL, (ConsoleColor)TransferFrame.TraFLC);

            Line((int)TransferFrame.TraFX + 2, (int)TransferFrame.TraFY + 3, (int)TransferFrame.TraFLL, (ConsoleColor)TransferFrame.TraFLC);
            Console.SetCursorPosition((int)TransferFrame.TraFX + 2 + (int)TransferFrame.TraFL - 5, (int)TransferFrame.TraFY + 3);
            Console.WriteLine('>');

            //Settings Frame
            Frame((char)SettingsFrame.SetFChar, (int)SettingsFrame.SetFX, (int)SettingsFrame.SetFY, (int)SettingsFrame.SetFH, (int)SettingsFrame.SetFL, (ConsoleColor)SettingsFrame.SetFFC, (ConsoleColor)SettingsFrame.SetFBC, SettingsFrame.Settings.ToString());

            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition((int)SettingsFrame.SetFX + 5, (int)SettingsFrame.SetFY + 1 + i);
                Console.WriteLine('*');
            }

            //Substract Frame
            Frame((char)SubstractFrame.SubFChar, (int)SubstractFrame.SubFX, (int)SubstractFrame.SubFY, (int)SubstractFrame.SubFH, (int)SubstractFrame.SubFL, (ConsoleColor)SubstractFrame.SubFFC, (ConsoleColor)SubstractFrame.SubFBC, SubstractFrame.Substract.ToString());

            Console.SetCursorPosition((int)SubstractFrame.SubFX + 5, (int)SubstractFrame.SubFY + 1);
            Console.ForegroundColor = (ConsoleColor)SubstractFrame.SubFLC;
            Console.WriteLine("---");

            //Additional Frame
            Frame((char)AdditionFrame.AddFChar, (int)AdditionFrame.AddFX, (int)AdditionFrame.AddFY, (int)AdditionFrame.AddFH, (int)AdditionFrame.AddFL, (ConsoleColor)AdditionFrame.AddFFC, (ConsoleColor)AdditionFrame.AddFBC, AdditionFrame.Addition.ToString());

            Console.SetCursorPosition((int)AdditionFrame.AddFX + 4, (int)AdditionFrame.AddFY + 1);
            Console.ForegroundColor = (ConsoleColor)AdditionFrame.AddFLC;
            Console.WriteLine("-+-");

            //Balance Frame
            Frame((char)BalanceFrame.BalFChar, (int)BalanceFrame.BalFX, (int)BalanceFrame.BalFY, (int)BalanceFrame.BalFH, (int)BalanceFrame.BalFL, (ConsoleColor)BalanceFrame.BalFFC, (ConsoleColor)BalanceFrame.BalFBC, BalanceFrame.Balance.ToString());

            //Date
            Date();

            //  CategoryName
            CategWrite();
        }
    }
}
//--------------------------------------------------------
        //public void puttext(int x, int y, int height, int length, char[] buffer)
        //{
        //    Console.SetCursorPosition(x, y);

        //    for (int i = 0; i < height; i++)
        //    {
        //        for (int j = 0; j < length; j++)
        //        {
        //            //Console.
        //        }
        //    }

        //    static void gettext(int x, int y, int height, int length, ref char[,] buffer)
        //    {
        //        Console.SetCursorPosition(x, y);

        //        for (int i = 0; i < height; i++)
        //        {
        //            for (int j = 0; j < length; j++)
        //            {
        //            [DllImport("winmm.dll")]
        //bool WINAPI ReadConsoleInput();
        //buffer[i, j] = (char) Console.SetIn(;
        //    }
        //}
        //        }

        //        static void puttext(int x, int y, ref char[,] buffer)
        //{
        //    Console.SetCursorPosition(x, y);

        //    foreach (var item in buffer)
        //    {
        //        Console.WriteLine(item);
        //    }
        //}

        //static void Main(string[] args)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        for (int j = 0; j < 5; j++)
        //        {
        //            Console.Write("*");
        //        }
        //        Console.WriteLine();
        //    }

        //    char[,] buffer = new char[2, 2];

        //    gettext(0, 0, 2, 2, ref buffer);

        //    puttext(0, 9, ref buffer);
        //}
        //}
        //public enum FoodParam           { FoodX = MainFrame.MFX + 15,           FoodY = MainFrame.MFY + 7,          FoodCol = ConsoleColor.Magenta              };
        //public enum HomeParam           { HomeX = MainFrame.MFX + 20,           HomeY = MainFrame.MFY + 7,          HomeCol = ConsoleColor.Blue                 };
        //public enum CafeParam           { CafeX = MainFrame.MFL - 15,           CafeY = MainFrame.MFY + 7,          CafeCol = ConsoleColor.Green                };
        //public enum HygieneParam        { HygieneX = MainFrame.MFL - 10,        HygieneY = MainFrame.MFY + 7,       HygieneCol = ConsoleColor.DarkBlue          };
        //public enum SportParam          { SportX = MainFrame.MFL - 10,          SportY = MainFrame.MFY + 9,         SportCol = ConsoleColor.Cyan                };
        //public enum HealthParam         { HealthX = MainFrame.MFL - 10,         HealthY = MainFrame.MFH - 11,       HealthCol = ConsoleColor.Red                };
        //public enum PhoneParam          { PhoneX = MainFrame.MFL - 10,          PhoneY = MainFrame.MFH - 9,         PhoneCol = ConsoleColor.White               };
        //public enum ClothesParam        { ClothesX = MainFrame.MFL - 15,        ClothesY = MainFrame.MFH - 9,       ClothesCol = ConsoleColor.DarkMagenta       };
        //public enum TaxiParam           { TaxiX = MainFrame.MFX + 20,           TaxiY = MainFrame.MFH - 9,          TaxiCol = ConsoleColor.Yellow               };
        //public enum EntertainmentParam  { EntertainmentX = MainFrame.MFX + 10,  EntertainmentY = MainFrame.MFH - 9, EntertainmentCol = ConsoleColor.DarkYellow  };
        //public enum TransportParam      { TransportX = MainFrame.MFX + 10,      TransportY = MainFrame.MFH - 11,    TransportCol = ConsoleColor.DarkRed         };
        //public enum CarParam            { CarX = MainFrame.MFX + 10,            CarY = MainFrame.MFY + 9,           CarCol = ConsoleColor.DarkGray              };

        //public enum CategoryNameCoord
        //{
        //    FoodX = MainFrame.MFX + 10, FoodY = MainFrame.MFY + 5, FoodCol = ConsoleColor.Magenta,     // Food
        //    HomeX = MainFrame.MFX + 10, HomeY = MainFrame.MFY + 5, HomeCol = ConsoleColor.Blue,        // Home
        //    CafeX = MainFrame.MFL - 15, CafeY = MainFrame.MFY + 5, CafeCol = ConsoleColor.Green,       // Cafe
        //    HygieneX = MainFrame.MFL - 10, HygieneY = MainFrame.MFY + 5, HygieneCol = ConsoleColor.DarkBlue,    // Hygiene
        //    SportX = MainFrame.MFL - 10, SportY = MainFrame.MFY - 7, SportCol = ConsoleColor.Cyan,        // Sport
        //    HealthX = MainFrame.MFL - 10, HealthY = MainFrame.MFH - 9, HealthCol = ConsoleColor.Red,         // Health
        //    PhoneX = MainFrame.MFL - 10, PhoneY = MainFrame.MFH + 11, PhoneCol = ConsoleColor.White,       // Phone
        //    ClothesX = MainFrame.MFL - 15, ClothesY = MainFrame.MFH + 11, ClothesCol = ConsoleColor.DarkMagenta, // Clothes
        //    TaxiX = MainFrame.MFX + 14, TaxiY = MainFrame.MFH + 11, TaxiCol = ConsoleColor.Yellow,      // Taxi
        //    EntertainmentX = MainFrame.MFX + 10, EntertainmentY = MainFrame.MFH + 11, EntertainmentCol = ConsoleColor.DarkYellow,  // Entertainment
        //    TransportX = MainFrame.MFX + 10, TransportY = MainFrame.MFH + 9, TransportCol = ConsoleColor.DarkRed,     // Transport
        //    CarX = MainFrame.MFX + 10, CarY = MainFrame.MFY + 7, CarCol = ConsoleColor.DarkGray     // Car
        //}

        //int CountOfCategoryNameCoord = 36;

        //string[] CategName = { "FoodX",             "FoodY",            "FoodCol",
        //                       "HomeX",             "HomeY",            "HomeCol",
        //                       "CafeX",             "CafeY",            "CafeCol",
        //                       "HygieneX",          "HygieneY",         "HygieneCol",
        //                       "SportX",            "SportY",           "SportCol",
        //                       "HealthX",           "HealthY",          "HealthCol",
        //                       "PhoneX",            "PhoneY",           "PhoneCol",
        //                       "ClothesX",          "ClothesY",         "ClothesCol",
        //                       "TaxiX",             "TaxiY",            "TaxiCol",
        //                       "EntertainmentX",    "EntertainmentY",   "EntertainmentCol",
        //                       "TransportX",        "TransportY",       "TransportCol",
        //                       "CarX",              "CarY",             "CarCol" };

        //--------------------------------------------------------
        //void WriteName(int x, int y, int index, ConsoleColor col)
        //{
        //    Console.SetCursorPosition(x, y);
        //    Console.ForegroundColor = col;
        //    Console.WriteLine(CategFirstName[index]);
        //}
        //--------------------------------------------------------
        //public void CategoryName()
        //{
            //int x, y, index = 0;
            //ConsoleColor col;

            //x = (int)FoodParam.FoodX;
            //y = (int)FoodParam.FoodY;
            //col = (ConsoleColor)FoodParam.FoodCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)HomeParam.HomeX;
            //y = (int)HomeParam.HomeY;
            //col = (ConsoleColor)HomeParam.HomeCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)CafeParam.CafeX;
            //y = (int)CafeParam.CafeY;
            //col = (ConsoleColor)CafeParam.CafeCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)HygieneParam.HygieneX;
            //y = (int)HygieneParam.HygieneY;
            //col = (ConsoleColor)HygieneParam.HygieneCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)SportParam.SportX;
            //y = (int)SportParam.SportY;
            //col = (ConsoleColor)SportParam.SportCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)HealthParam.HealthX;
            //y = (int)HealthParam.HealthY;
            //col = (ConsoleColor)HealthParam.HealthCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)PhoneParam.PhoneX;
            //y = (int)PhoneParam.PhoneY;
            //col = (ConsoleColor)PhoneParam.PhoneCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)ClothesParam.ClothesX;
            //y = (int)ClothesParam.ClothesY;
            //col = (ConsoleColor)ClothesParam.ClothesCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)TaxiParam.TaxiX;
            //y = (int)TaxiParam.TaxiY;
            //col = (ConsoleColor)TaxiParam.TaxiCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)EntertainmentParam.EntertainmentX;
            //y = (int)EntertainmentParam.EntertainmentY;
            //col = (ConsoleColor)EntertainmentParam.EntertainmentCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)TransportParam.TransportX;
            //y = (int)TransportParam.TransportY;
            //col = (ConsoleColor)TransportParam.TransportCol;

            //WriteName(x, y, index, col);
            //index++;

            //x = (int)CarParam.CarX;
            //y = (int)CarParam.CarY;
            //col = (ConsoleColor)CarParam.CarCol;

            //WriteName(x, y, index, col);


            //int x = 0, y = 0;
            //ConsoleColor col = ConsoleColor.Black;
            ////int nameIndex = CategName.Length - 1;
            //int nameIndex = 0;

            ////for (int i = CategFirstName.Length - 1; i >= 0; i--)
            //for (int i = 0; i < CategFirstName.Length; i++)
            //{
            //    int index = 0;

            //    //for (int k = nameIndex; k >= 0; k--)
            //    for (int j = nameIndex; j < CategName.Length; j++)
            //    {
            //        CategoryNameCoord param = (CategoryNameCoord)Enum.Parse(typeof(CategoryNameCoord), CategName[j].ToString());

            //        switch (index)
            //        {
            //            case 0:
            //                col = (ConsoleColor)param;
            //                index++;
            //                break;
            //            case 1:
            //                y = (int)param;
            //                index++;
            //                break;
            //            case 2:
            //                x = (int)param;
            //                index++;
            //                nameIndex = j - 1;
            //                goto Exit;
            //            default:
            //                break;
            //        }
            //    }
            //    Exit:

            //    Console.SetCursorPosition(x, y);
            //    Console.ForegroundColor = col;
            //    Console.WriteLine(CategFirstName[i]);
            //}
        //}