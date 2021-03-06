﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//--------------------------------------------------------
namespace Monefy
{
    public class Functions
    {
        [DllImport("Bor_Conio_D.dll")]
        static extern int gettext(int __left, int __top, int __right, int __bottom, char[] __destin);

        [DllImport("Bor_Conio_D.dll")]
        static extern int puttext(int __left, int __top, int __right, int __bottom, char[] __source);

        private static Functions instance;

        public static Functions getInstance()
        {
            if (instance == null)
                instance = new Functions();

            return instance;
        }
        //--------------------------------------------------------
        private enum FramePos { TopLeft, Horizontal, TopRight, Vertical, BottomLeft, BottomRight };

        char[] DoubleLine = { (char)0x2554, (char)0x2550, (char)0x2557, (char)0x2551, (char)0x255A, (char)0x255D };

        char[] SingleLine = { (char)0x250C, (char)0x2500, (char)0x2510, (char)0x2502, (char)0x2514, (char)0x2518 };
        //--------------------------------------------------------
        private void Line(int x, int y, int length, ConsoleColor LineCol, char symbol = '—')
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
        private void Clear(int x, int y, int length, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.SetCursorPosition(x + j, y + i);

                    Console.Write(" ");
                }
            }
        }
        //--------------------------------------------------------
        private void Frame(char type, int x, int y, int height, int width, ConsoleColor frameForCol, ConsoleColor frameBackCol = ConsoleColor.Black, string name = "", ConsoleColor nameCol = ConsoleColor.Red)
        {
            Console.ForegroundColor = frameForCol;
            Console.BackgroundColor = frameBackCol;

            char[] Symb;

            if (type == 'd')
                Symb = DoubleLine;
            else
                Symb = SingleLine;

            Console.SetCursorPosition(x, y);
            Console.Write(Symb[(int)FramePos.TopLeft]);

            if (name == "" || name.Length + 2 > width - 2)
            {
                for (int i = 0; i < width - 2; i++)
                    Console.Write(Symb[(int)FramePos.Horizontal]);
            }
            else
            {
                int l1 = (width - 2 - (name.Length + 2)) / 2;
                int l2 = width - 2 - (name.Length + 2) - l1;

                for (int i = 0; i < l1; i++)
                    Console.Write(Symb[(int)FramePos.Horizontal]);

                Console.ForegroundColor = nameCol;
                Console.Write($" {name} ");
                Console.ForegroundColor = frameForCol;

                for (int i = 0; i < l2; i++)
                    Console.Write(Symb[(int)FramePos.Horizontal]);
            }

            Console.Write(Symb[(int)FramePos.TopRight]);

            for (int i = 0; i < height - 2; i++)
            {
                Console.SetCursorPosition(x, y + i + 1);
                Console.Write(Symb[(int)FramePos.Vertical]);

                Console.SetCursorPosition(x + width - 1, y + i + 1);
                Console.Write(Symb[(int)FramePos.Vertical]);

            }

            Console.SetCursorPosition(x, y + height - 1);
            Console.Write(Symb[(int)FramePos.BottomLeft]);

            for (int i = 0; i < width - 2; i++)
                Console.Write(Symb[(int)FramePos.Horizontal]);

            Console.Write(Symb[(int)FramePos.BottomRight]);
        }
        //--------------------------------------------------------
        struct FRAME_P
        {
            public char type;
            public int x, y, height, width;
            public ConsoleColor frameForCol, frameBackCol;
            public string name;
            public ConsoleColor nameCol;
        };

        private void Frame(FRAME_P FP)
        {
            Frame(FP.type, FP.x, FP.y, FP.height, FP.width, FP.frameForCol, FP.frameBackCol, FP.name, FP.nameCol);
        }
        //--------------------------------------------------------
        //private FRAME_P MainFrame = {'d', 17, 2, 30, 80, ConsoleColor.Green, ConsoleColor.Black, [StringValue("")] MONEFY, ConsoleColor.Magenta  };

        private enum MainFrame { MFChar = 'd', MFX = 17, MFY = 2, MFH = 30, MFL = 82, MFFC = ConsoleColor.Green, MFBC = ConsoleColor.Black, [StringValue("")] MONEFY, MFNC = ConsoleColor.Magenta }

        private enum ReportsFrame { RepFChar = 's', RepFX = MainFrame.MFX + 2, RepFY = MainFrame.MFY + 1, RepFH = 5, RepFL = 12, RepFFC = ConsoleColor.White, RepFBC = ConsoleColor.Black, [StringValue("")] Reports, RepFLL = 7, RepFLC = ConsoleColor.Gray }

        private enum TransferFrame { TraFChar = 's', TraFX = MainFrame.MFL - 10, TraFY = MainFrame.MFY + 1, TraFH = 5, TraFL = 12, TraFFC = ConsoleColor.White, TraFBC = ConsoleColor.Black, [StringValue("")] Transfer, TraFLL = 7, TraFLC = ConsoleColor.White }

        private enum SettingsFrame { SetFChar = 's', SetFX = MainFrame.MFL + 3, SetFY = MainFrame.MFY + 1, SetFH = 5, SetFL = 12, SetFFC = ConsoleColor.White, SetFBC = ConsoleColor.Black, [StringValue("")] Settings, SetFDC = ConsoleColor.White }

        private enum SubstractFrame { SubFChar = 's', SubFX = MainFrame.MFX + 4, SubFY = MainFrame.MFH - 4, SubFH = 3, SubFL = 13, SubFFC = ConsoleColor.White, SubFBC = ConsoleColor.Black, [StringValue("")] Substract, SubFLL = 5, SubFLC = ConsoleColor.Yellow }

        private enum AdditionFrame { AddFChar = 's', AddFX = MainFrame.MFL - 1, AddFY = MainFrame.MFH - 4, AddFH = 3, AddFL = 12, AddFFC = ConsoleColor.White, AddFBC = ConsoleColor.Black, [StringValue("")] Addition, AddFLL = 5, AddFLC = ConsoleColor.Yellow }

        private enum BalanceFrame { BalFChar = 'd', BalFX = (MainFrame.MFL / 2) + (MainFrame.MFX / 2) + 3, BalFY = MainFrame.MFH - 6, BalFH = 5, BalFL = 15, BalFFC = ConsoleColor.DarkGreen, BalFBC = ConsoleColor.Black, [StringValue("")] Balance }

        private enum ExitFrame { ExFChar = 'd', ExFX = (MainFrame.MFL / 2) + (MainFrame.MFX / 2) - 5, ExFY = MainFrame.MFH / 2, ExFH = 5, ExFL = 29, ExFFC = ConsoleColor.Black, ExFBC = ConsoleColor.Gray, [StringValue("")] Quit, ExFNC = ConsoleColor.Black }

        private enum AddSubTrParam { ASChar = 's', ASX = MainFrame.MFX + 20, ASY = MainFrame.MFY + 8, ASH = 14, ASL = 42, ASFC = ConsoleColor.Gray, ASBC = ConsoleColor.Black, [StringValue("")] Addition, [StringValue("")] Substract, ASNC = ConsoleColor.Gray, [StringValue("")] Transfer }

        //private enum TransferParam { TrChar = 's', TrX = }

        private enum DateParam { DateX = MainFrame.MFX + 25, DateY = MainFrame.MFY + 6, DateColF = ConsoleColor.Cyan };

        private List<ConsoleColor> CatNameCol = new List<ConsoleColor>();

        private ConsoleColor[] CatColors = {ConsoleColor.Magenta, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.DarkBlue,
                                ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.DarkMagenta,
                                ConsoleColor.Yellow, ConsoleColor.DarkYellow, ConsoleColor.DarkRed, ConsoleColor.DarkGray };


        string[] TemplCategName = { "Food", "Home", "Cafe", "Hygiene", "Sport", "Health", "Phone", "Clothes", "Taxi", "Entertainment", "Transport", "Car" };

        string[] TemplAccName = { "Cash", "Card" };

        string[] ReportsMenuName = { "Change Account", "Date change", "Transaction to txt", "Export to CSV", "Back" };

        string[] SettingsMenuName = { "Redact Category", "Redact Account", "Change Language", "Add/Change Money Limit", "Add/Change Subscription", "Back" };

        string[] DateName = { "Day", "Week", "Month", "Year" };

        double[] tempDouble = new double[Application.Categories.Count];

        private int x = 0, y = 0;
        //--------------------------------------------------------
        public int Menu(int x, int y, string[] arr, int select = 0, int exception = -1)
        {
            string[] tempArr = new string[exception >= 0 ? arr.Length - 1 : arr.Length];

            for (int i = 0; i < tempArr.Length; i++)
            {
                if (i != exception)
                    tempArr[i] = arr[i];
                else
                    tempArr[i] = arr[i + 1];
            }
            
            CONTI:
            for (int i = 0; i < tempArr.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);

                if (i == select)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine(tempArr[i]);
            }

            int temp = MenuGo(select, tempArr.Length);

            if (temp == -2)
                goto ENTER;

            if (temp >= 0)
            {
                select = temp;
                goto CONTI;
            }

            ENTER:
            Console.ForegroundColor = ConsoleColor.Gray;

            if (exception >= 0)
            {
                for (int i = 0; i < arr.Length; i++)
                    for (int j = 0; j < tempArr.Length; j++)
                    {
                        if (arr[i] == tempArr[j])
                            select = i;
                    }
            }

            return select;
        }
        //--------------------------------------------------------
        public int MenuGo(int select, int len)
        {
            ERRORKEY:
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.DownArrow:
                    if (select < len - 1)
                        select++;
                    break;
                case ConsoleKey.UpArrow:
                    if (select > 0)
                        select--;
                    break;
                case ConsoleKey.RightArrow:
                    select = len - 1;
                    break;
                case ConsoleKey.LeftArrow:
                    select = 0;
                    break;
                case ConsoleKey.Enter:
                    select = -2;
                    break;
                case ConsoleKey.Escape:
                    return -1;
                default:
                    goto ERRORKEY;
            }

            return select;
        }
        //--------------------------------------------------------
        public void TabGo(int sel)
        {
            switch (sel)
            {
                case 0: //Reports
                    Frame((char)ReportsFrame.RepFChar, (int)ReportsFrame.RepFX, (int)ReportsFrame.RepFY, (int)ReportsFrame.RepFH, (int)ReportsFrame.RepFL, (ConsoleColor)ReportsFrame.RepFFC, ConsoleColor.Gray, ReportsFrame.Reports.ToString());
                    //Frame(ReportsFrame);
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
        private string MonthToString(int month)
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
        private DateTime[] WeekBeginEnd(DateTime date)
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
        private void Date(char type)
        {
            Console.SetCursorPosition((int)DateParam.DateX, (int)DateParam.DateY);
            Console.ForegroundColor = (ConsoleColor)DateParam.DateColF;

            switch (type)
            {
                case 'd':
                    Console.Write($"{DateTime.Today.DayOfWeek}, {DateTime.Today.Day} {MonthToString(DateTime.Today.Month)}");
                    break;
                case 'w':
                    Console.WriteLine($"{WeekBeginEnd(DateTime.Today)[0].Day} - {WeekBeginEnd(DateTime.Today)[1].Day} {MonthToString(DateTime.Today.Month)}");
                    break;
                case 'm':
                    Console.WriteLine($"{MonthToString(DateTime.Today.Month)}");
                    break;
                case 'y':
                    Console.WriteLine($"{DateTime.Today.Year}");
                    break;
                default:
                    break;
            }
        }
        //--------------------------------------------------------
        private void CategWrite()
        {
            int index = 0;

            int count = Application.Categories.Count;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            while (count != 0)
            {
                Console.SetCursorPosition((int)MainFrame.MFX + 1, (int)MainFrame.MFY + 8 + index);
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
        public void TemplateName()
        {
            //CatNameCol.Add(ConsoleColor.Magenta);
            //CatNameCol.Add(ConsoleColor.Blue);
            //CatNameCol.Add(ConsoleColor.Green);
            //CatNameCol.Add(ConsoleColor.DarkBlue);
            //CatNameCol.Add(ConsoleColor.Cyan);
            //CatNameCol.Add(ConsoleColor.Red);
            //CatNameCol.Add(ConsoleColor.White);
            //CatNameCol.Add(ConsoleColor.DarkMagenta);
            //CatNameCol.Add(ConsoleColor.Yellow);
            //CatNameCol.Add(ConsoleColor.DarkYellow);
            //CatNameCol.Add(ConsoleColor.DarkRed);
            //CatNameCol.Add(ConsoleColor.DarkGray);

            foreach (ConsoleColor item in Enum.GetValues(typeof(ConsoleColor)))
            {
                if (item != ConsoleColor.Black)
                    CatNameCol.Add(item);
            }

            for (int i = 0; i < TemplCategName.Length; i++)
                Application.getInstance().AddCategories(TemplCategName[i], Type.Outcome);

            for (int i = 0; i < TemplAccName.Length; i++)
                Application.getInstance().AddAccount(TemplAccName[i], Currency.AZN, 0.0, false);

            Application.Accounts[0].Active = true;
        }
        //--------------------------------------------------------
        //public void CatNameStatusLine(double food, double home, double cafe, double hygiene, double sport, double health, double phone, double clothes, double taxi, double entertainment, double transport, double car)
        private double[] CatNameStatusLine(char mode, params double[] cats)
        {
            double[] CatValue = new double[Application.Categories.Count];
            for (int i = 0; i < cats.Length; i++)
                CatValue[i] = cats[i];

            //CatValue[0] = food;
            //CatValue[1] = home;
            //CatValue[2] = cafe;
            //CatValue[3] = hygiene;
            //CatValue[4] = sport;
            //CatValue[5] = health;
            //CatValue[6] = phone;
            //CatValue[7] = clothes;
            //CatValue[8] = taxi;
            //CatValue[9] = entertainment;
            //CatValue[10] = transport;
            //CatValue[11] = car;

            if (mode == 'w')
            {
                for (int i = 0; i < Application.Categories.Count; i++)
                {
                    Console.SetCursorPosition((int)MainFrame.MFX + 23, (int)MainFrame.MFY + 8 + i);

                    Console.ForegroundColor = CatNameCol[i];
                    for (int j = 0; j < CatValue[i] / 5; j++)
                        Console.Write((char)0x2593);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write($" {CatValue[i]}%");
                }
            }

            return CatValue;
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
        public string[] GetCurrentName(char whom)
        {
            if (whom == 'a')
            {
                string[] AccName = new string[Application.Accounts.Count];
                for (int i = 0; i < Application.Accounts.Count; i++)
                    AccName[i] = Application.Accounts[i].Name;

                return AccName;
            }
            else
            {
                string[] CategName = new string[Application.Categories.Count];
                for (int i = 0; i < Application.Categories.Count; i++)
                    CategName[i] = Application.Categories[i].Name;
                return CategName;
            }
        }
        //--------------------------------------------------------
        public void ReportWindow()
        {
            Frame((char)ReportsFrame.RepFChar, (int)MainFrame.MFX, (int)MainFrame.MFY, (int)MainFrame.MFH, (int)ReportsFrame.RepFL + 11, (ConsoleColor)ReportsFrame.RepFFC, (ConsoleColor)ReportsFrame.RepFBC, ReportsFrame.Reports.ToString());

            Clear((int)MainFrame.MFX + 1, (int)MainFrame.MFY + 1, (int)ReportsFrame.RepFL + 9, (int)MainFrame.MFH - 3);

            x = (int)MainFrame.MFX + 1;
            y = (int)MainFrame.MFY + 2;

            Console.SetCursorPosition((int)MainFrame.MFX + 1, (int)MainFrame.MFY + 7);
            Console.WriteLine("---------------------");
            
            CHANGEACC:
            Clear((int)MainFrame.MFX + 1, (int)MainFrame.MFY + 8, (int)ReportsFrame.RepFL + 9, 1);
            Console.SetCursorPosition((int)MainFrame.MFX + 1, (int)MainFrame.MFY + 9);
            Console.WriteLine($"Active account: {Application.Accounts[GetActiveAccount()].Name}");

            CONTI:
            int sel = Menu(x, y, ReportsMenuName), ans = 0;

            switch (sel)
            {
                case 0:
                    Frame('s', (int)MainFrame.MFX + 1, (int)MainFrame.MFY + 12, Application.Accounts.Count + 5, (int)ReportsFrame.RepFL + 9, ConsoleColor.Yellow, (ConsoleColor)ReportsFrame.RepFBC, ReportsMenuName[sel]);

                    ans = Menu((int)MainFrame.MFX + 2, (int)MainFrame.MFY + 14, GetCurrentName('a'));

                    Application.Accounts[GetActiveAccount()].Active = false;
                    Application.Accounts[ans].Active = true;

                    Clear((int)MainFrame.MFX + 1, (int)MainFrame.MFY + 12, (int)ReportsFrame.RepFL + 9, Application.Accounts.Count + 5);
                    goto CHANGEACC;
                case 1:
                    Frame('s', (int)MainFrame.MFX + 1, (int)MainFrame.MFY + 12, Application.Accounts.Count + 5, (int)ReportsFrame.RepFL + 9, ConsoleColor.Yellow, (ConsoleColor)ReportsFrame.RepFBC, ReportsMenuName[sel]);

                    ans = Menu((int)MainFrame.MFX + 2, (int)MainFrame.MFY + 14, GetCurrentName('a'));
                    break;
                case 4:
                    goto EXIT;
                default:
                    break;
            }
            goto CONTI;

            EXIT:
            return;
        }
        //--------------------------------------------------------
        public void SettingsWindow()
        {
            Frame((char)SettingsFrame.SetFChar, (int)TransferFrame.TraFX, (int)MainFrame.MFY, (int)MainFrame.MFH, (int)TransferFrame.TraFL + (int)SettingsFrame.SetFL + 5, (ConsoleColor)SettingsFrame.SetFFC, (ConsoleColor)SettingsFrame.SetFBC, SettingsFrame.Settings.ToString());

            Clear((int)TransferFrame.TraFX + 1, (int)MainFrame.MFY + 1, (int)TransferFrame.TraFL + (int)SettingsFrame.SetFL + 3, (int)MainFrame.MFH - 2);

            x = (int)TransferFrame.TraFX + 1;
            y = (int)MainFrame.MFY + 2;

            Menu(x, y, SettingsMenuName);
        }
        //--------------------------------------------------------
        public int GetActiveAccount()
        {
            int index = 0;

            for (int i = 0; i < Application.Accounts.Count; i++)
            {
                if (Application.Accounts[i].Active)
                    index = i;
            }

            return index;
        }
        //--------------------------------------------------------
        public void AddSubWindow(char type)
        {
            Frame((char)AddSubTrParam.ASChar, (int)AddSubTrParam.ASX, (int)AddSubTrParam.ASY, (int)AddSubTrParam.ASH, (int)AddSubTrParam.ASL, (ConsoleColor)AddSubTrParam.ASFC, (ConsoleColor)AddSubTrParam.ASBC, type == 'a' ? AddSubTrParam.Addition.ToString() : AddSubTrParam.Substract.ToString(), (ConsoleColor)AddSubTrParam.ASNC);

            BEGIN:
            Clear((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 1, (int)AddSubTrParam.ASL - 2, (int)AddSubTrParam.ASH - 3);

            if (type == 's' && Application.Accounts[GetActiveAccount()].Money == 0)
            {
                Console.SetCursorPosition((int)AddSubTrParam.ASX + 2, (int)AddSubTrParam.ASY + 5);

                Console.WriteLine($"Your balance is zero in {Application.Accounts[GetActiveAccount()].Name} account,"); 

                Console.SetCursorPosition((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 6);
                Console.WriteLine($"change account or increase your balance");

                Console.ReadKey();
                return;
            }

            Console.CursorVisible = true;
            Console.SetCursorPosition((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 2);
            Console.Write("Sum: ");
            int sum = Convert.ToInt32(Console.ReadLine());

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 6 + sum.ToString().Length, (int)AddSubTrParam.ASY + 2);
            Console.Write($"{Application.Accounts[GetActiveAccount()].Cur}");

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 4);

            Console.Write($"Note(max {(int)AddSubTrParam.ASL - 15}):");

            string note = null;

            for (int i = 0; i < (int)AddSubTrParam.ASL - 15; i++)
            {
                char ctemp = Console.ReadKey().KeyChar;

                if (ctemp == (char)ConsoleKey.Enter)
                    break;

                note += ctemp;
            }

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 6);

            Console.BackgroundColor = ConsoleColor.Blue;

            Console.WriteLine("Select category -> ");

            Console.CursorVisible = false;

            NOENTER:
            var key = Console.ReadKey(true).Key;

            if (key != ConsoleKey.Enter)
                goto NOENTER;

            //char[] destin = new char[(int)AddSubParam.ASH * ((int)AddSubParam.ASL - 25) * 2];
            //gettext((int)AddSubParam.ASX + (int)AddSubParam.ASL + 1, (int)AddSubParam.ASY, (int)AddSubParam.ASX + (int)AddSubParam.ASL + 1 + (int)AddSubParam.ASL, (int)AddSubParam.ASY + (int)AddSubParam.ASH, destin);

            Frame('s', (int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 1, (int)AddSubTrParam.ASY, (int)AddSubTrParam.ASH, (int)AddSubTrParam.ASL - 25, (ConsoleColor)AddSubTrParam.ASFC);

            string[] AccName = GetCurrentName('a');
            string[] CategName = GetCurrentName('c');

            int MenuAns = Menu((int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 2, (int)AddSubTrParam.ASY + 1, type == 'a' ? AccName : CategName);

            Clear((int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 1, (int)AddSubTrParam.ASY, (int)AddSubTrParam.ASL - 25, (int)AddSubTrParam.ASH);

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 20, (int)AddSubTrParam.ASY + 6);

            string str = type == 'a' ? AccName[MenuAns] : CategName[MenuAns];
            Console.Write(str);

            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 8);
            Console.Write("Save or again?");

            ANSAGAIN:
            var ans = Console.ReadKey(true).Key;

            switch (ans)
            {
                case ConsoleKey.Escape:
                    goto BEGIN;
                case ConsoleKey.Enter:
                    break;
                default:
                    goto ANSAGAIN;
            }

            //puttext((int)AddSubParam.ASX + (int)AddSubParam.ASL + 1, (int)AddSubParam.ASY, (int)AddSubParam.ASX + (int)AddSubParam.ASL + 1 + (int)AddSubParam.ASL, (int)AddSubParam.ASY + (int)AddSubParam.ASH, destin);

            if (type == 'a')
            {
                Application.Accounts[GetActiveAccount()].Money += sum;

                Category temp = new Category(str, Type.Income);
                Application.getInstance().AddOutcomes(sum, temp, note, DateTime.Today);
            }
            else
            {
                Category temp = new Category(str, Type.Outcome);
                Application.getInstance().AddOutcomes(sum, temp, note, DateTime.Today);

                tempDouble = CatNameStatusLine('s');

                tempDouble[MenuAns] = sum;
            }
        }
        //--------------------------------------------------------
        public void TransferWindow()
        {
            Frame((char)AddSubTrParam.ASChar, (int)AddSubTrParam.ASX, (int)AddSubTrParam.ASY, (int)AddSubTrParam.ASH, (int)AddSubTrParam.ASL, (ConsoleColor)AddSubTrParam.ASFC, (ConsoleColor)AddSubTrParam.ASBC, AddSubTrParam.Transfer.ToString(), (ConsoleColor)AddSubTrParam.ASNC);

            BEGIN:
            Clear((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 1, (int)AddSubTrParam.ASL - 2, (int)AddSubTrParam.ASH - 3);

            Console.CursorVisible = true;
            Console.SetCursorPosition((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 2);
            Console.Write("Sum: ");
            int sum = Convert.ToInt32(Console.ReadLine());

            int index = 0;

            for (int i = 0; i < Application.Accounts.Count; i++)
            {
                if (Application.Accounts[i].Active)
                    index = i;
            }

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 6 + sum.ToString().Length, (int)AddSubTrParam.ASY + 2);
            Console.Write($"{Application.Accounts[index].Cur}");

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 4);

            Console.Write($"Note(max {(int)AddSubTrParam.ASL - 15}):");

            string note = null;

            for (int i = 0; i < (int)AddSubTrParam.ASL - 15; i++)
            {
                char ctemp = Console.ReadKey().KeyChar;

                if (ctemp == (char)ConsoleKey.Enter)
                    break;

                note += ctemp;
            }

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 10, (int)AddSubTrParam.ASY + 6);

            Console.BackgroundColor = ConsoleColor.Blue;

            Console.WriteLine("From -> ");

            Console.CursorVisible = false;

            NOFROMENTER:
            var key = Console.ReadKey(true).Key;

            if (key != ConsoleKey.Enter)
                goto NOFROMENTER;

            Frame('s', (int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 1, (int)AddSubTrParam.ASY, (int)AddSubTrParam.ASH, (int)AddSubTrParam.ASL - 25, (ConsoleColor)AddSubTrParam.ASFC);

            string[] AccName = GetCurrentName('a');

            int FromAns = Menu((int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 2, (int)AddSubTrParam.ASY + 1, AccName);

            Clear((int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 1, (int)AddSubTrParam.ASY, (int)AddSubTrParam.ASL - 25, (int)AddSubTrParam.ASH);

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 20, (int)AddSubTrParam.ASY + 6);

            Console.Write(AccName[FromAns]);

            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 15, (int)AddSubTrParam.ASY + 7);
            Console.WriteLine("|");
            Console.SetCursorPosition((int)AddSubTrParam.ASX + 15, (int)AddSubTrParam.ASY + 8);
            Console.WriteLine("V");

            Console.BackgroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 10, (int)AddSubTrParam.ASY + 9);
            Console.WriteLine("To   -> ");

            Console.CursorVisible = false;

            NOTOENTER:
            key = Console.ReadKey(true).Key;

            if (key != ConsoleKey.Enter)
                goto NOTOENTER;

            Frame('s', (int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 1, (int)AddSubTrParam.ASY, (int)AddSubTrParam.ASH, (int)AddSubTrParam.ASL - 25, (ConsoleColor)AddSubTrParam.ASFC);

            int ToAns = Menu((int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 2, (int)AddSubTrParam.ASY + 1, AccName, 0, FromAns);

            Clear((int)AddSubTrParam.ASX + (int)AddSubTrParam.ASL + 1, (int)AddSubTrParam.ASY, (int)AddSubTrParam.ASL - 25, (int)AddSubTrParam.ASH);

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 20, (int)AddSubTrParam.ASY + 9);

            Console.Write(AccName[ToAns]);

            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition((int)AddSubTrParam.ASX + 1, (int)AddSubTrParam.ASY + 11);
            Console.Write("Save or again?");

            ANSAGAIN:
            var ans = Console.ReadKey(true).Key;

            switch (ans)
            {
                case ConsoleKey.Escape:
                    goto BEGIN;
                case ConsoleKey.Enter:
                    break;
                default:
                    goto ANSAGAIN;
            }

            Category temp = new Category($"From {AccName[FromAns]} to {AccName[ToAns]}", Type.Income);
            Application.getInstance().AddOutcomes(sum, temp, note, DateTime.Today);
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

            Console.SetCursorPosition((int)AdditionFrame.AddFX + 5, (int)AdditionFrame.AddFY + 1);
            Console.ForegroundColor = (ConsoleColor)AdditionFrame.AddFLC;
            Console.WriteLine("-+-");

            //Balance Frame
            Frame((char)BalanceFrame.BalFChar, (int)BalanceFrame.BalFX, (int)BalanceFrame.BalFY, (int)BalanceFrame.BalFH, (int)BalanceFrame.BalFL, (ConsoleColor)BalanceFrame.BalFFC, (ConsoleColor)BalanceFrame.BalFBC, BalanceFrame.Balance.ToString());

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition((int)BalanceFrame.BalFX + 2, (int)BalanceFrame.BalFY + 2);
            Console.WriteLine($"{Application.Accounts[GetActiveAccount()].Money} {Application.Accounts[GetActiveAccount()].Cur}");

            //Console.ForegroundColor = ConsoleColor.DarkGray;
            //Console.SetCursorPosition((int)BalanceFrame.BalFX + 1, (int)BalanceFrame.BalFY + 2);
            //Console.WriteLine("--------------------------------------");

            //Console.SetCursorPosition((int)BalanceFrame.BalFX + 13, (int)BalanceFrame.BalFY + 3);
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Sum: ");

            //Console.ForegroundColor = ConsoleColor.DarkGray;
            //Console.SetCursorPosition((int)BalanceFrame.BalFX + 1, (int)BalanceFrame.BalFY + 4);
            //Console.WriteLine("--------------------------------------");

            //Console.SetCursorPosition((int)BalanceFrame.BalFX + 11, (int)BalanceFrame.BalFY + 5);
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("Spent: ");

            //Date
            Date('d');

            //CategoryName
            CategWrite();

            CatNameStatusLine('w', tempDouble);
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