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
        public void Line(int x, int y, int length, ConsoleColor frameCol)
        {
            Console.ForegroundColor = frameCol;

            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.WriteLine("--");
            }


            Console.ForegroundColor = ConsoleColor.Gray;
        }
        //--------------------------------------------------------
        public void Frame(char type, int x, int y, int height, int length, ConsoleColor frameCol, string name = "")
        {
            int count = 0;

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = frameCol;

            if (type == 'd')
                Console.Write(DoubleLine[count]);
            else
                Console.Write(SingleLine[count]);
            count++;

            for (int i = 0; i < length - 1; i++)
            {
                if (name != "" && i == (length - name.Length) / 2)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(name);
                    Console.ForegroundColor = frameCol;
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
        public enum MainFrameCoord { MFChar = 'd', MFX = 25, MFY = 2, MFH = 25, MFL = 60, MFC = ConsoleColor.Green, [StringValue("")]
            Monefy }

        public enum ReportsFrameCoord { RFChar = 's', RFX = 28, RFY = 4, RFH = 6, RFL = 10, RFC = ConsoleColor.White, [StringValue("")]
            Reports, RFLL = 5, RFLC = ConsoleColor.Red }

        public enum TransferFrameCoord { TFChar = 's', TFX = 60, TFY = 4, TFH = 6, TFL = 11, TFC = ConsoleColor.White, [StringValue("")]
            Transfer, TFLL = 5, TFLC = ConsoleColor.White }

        public enum SettingsFrameCoord { SFChar = 's', SFX = 72, SFY = 4, SFH = 6, SFL = 11, SFC = ConsoleColor.White, [StringValue("")]
            Settings, SFDC = ConsoleColor.White }

        public enum SubstractFrameCoord { SubFChar = 's', SubFX = 28, SubFY = 4, SubFH = 6, SubFL = 11, SubFC = ConsoleColor.White, [StringValue("")]
            Substract, SubFDC = ConsoleColor.White }

        //--------------------------------------------------------
        public void DrawFrame()
        {
            //Main Frame
            Frame((char)MainFrameCoord.MFChar, (int)MainFrameCoord.MFX, (int)MainFrameCoord.MFY, (int)MainFrameCoord.MFH, (int)MainFrameCoord.MFL, (ConsoleColor)MainFrameCoord.MFC, MainFrameCoord.Monefy.ToString());

            //Reports Frame
            Frame((char)ReportsFrameCoord.RFChar, (int)ReportsFrameCoord.RFX, (int)ReportsFrameCoord.RFY, (int)ReportsFrameCoord.RFH, (int)ReportsFrameCoord.RFL, (ConsoleColor)ReportsFrameCoord.RFC, ReportsFrameCoord.Reports.ToString());

            for (int i = 0; i < 3; i++)
                Line((int)ReportsFrameCoord.RFX + 2, (int)ReportsFrameCoord.RFY + 1 + i/*(i == 0 || i == 2 ? i : 0)*/, (int)ReportsFrameCoord.RFLL, (ConsoleColor)ReportsFrameCoord.RFLC);

            //Transfer Frame
            Frame((char)TransferFrameCoord.TFChar, (int)TransferFrameCoord.TFX, (int)TransferFrameCoord.TFY, (int)TransferFrameCoord.TFH, (int)TransferFrameCoord.TFL, (ConsoleColor)TransferFrameCoord.TFC, TransferFrameCoord.Transfer.ToString());

            Console.SetCursorPosition((int)TransferFrameCoord.TFX + 2, (int)TransferFrameCoord.TFY + 1);
            Console.WriteLine('<');
            Line((int)TransferFrameCoord.TFX + 3, (int)TransferFrameCoord.TFY + 1, (int)TransferFrameCoord.TFLL, (ConsoleColor)TransferFrameCoord.TFLC);

            Line((int)TransferFrameCoord.TFX + 2, (int)TransferFrameCoord.TFY + 3, (int)TransferFrameCoord.TFLL, (ConsoleColor)TransferFrameCoord.TFLC);
            Console.SetCursorPosition((int)TransferFrameCoord.TFX + 2 + (int)TransferFrameCoord.TFLL + 1, (int)TransferFrameCoord.TFY + 3);
            Console.WriteLine('>');

            //Settings Frame
            Frame((char)SettingsFrameCoord.SFChar, (int)SettingsFrameCoord.SFX, (int)SettingsFrameCoord.SFY, (int)SettingsFrameCoord.SFH, (int)SettingsFrameCoord.SFL, (ConsoleColor)SettingsFrameCoord.SFC, SettingsFrameCoord.Settings.ToString());

            //Encoding encoding = Encoding.UTF8; 

            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition((int)SettingsFrameCoord.SFX + 5, (int)SettingsFrameCoord.SFY + 1 + i);
                Console.WriteLine("*");
            }

            //Substract Frame
            //Frame()
        }
    }
}
//--------------------------------------------------------