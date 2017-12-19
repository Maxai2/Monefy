using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    class Functions
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
        public void Line(int x, int y, int length, ConsoleColor frameCol, char type = 'h')
        {
            Console.ForegroundColor = frameCol;

            char symb = type == 'h' ? '—' : '|';

            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.WriteLine(symb);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
        //--------------------------------------------------------
        public void Frame(char type, int x, int y, int height, int length, ConsoleColor frameForCol, ConsoleColor frameBackCol = ConsoleColor.Black, string name = "", ConsoleColor nameCol = ConsoleColor.Cyan)
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

        public enum ExitFrame { ExFChar = 'd', ExFX = (MainFrame.MFL / 2) + (MainFrame.MFX / 2) - 5, ExFY = MainFrame.MFH / 2, ExFH = 5, ExFL = 28, ExFFC = ConsoleColor.Black, ExFBC = ConsoleColor.Gray, [StringValue("")]Quit, ExFNC = ConsoleColor.Black }
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
        }
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
        public void Exit()
        {
            Frame(ExitFrame.ExFChar, ExitFrame.ExFX, ExitFrame.ExFY, ExitFrame.ExFH, ExitFrame.ExFL, ExitFrame.ExFFC, ExitFrame.ExFBC, ExitFrame.Quit.ToString(), ExitFrame.ExFNC);
        }
    }
}
//--------------------------------------------------------