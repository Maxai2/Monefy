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

            for (int i = 0; i < length; i++)
            {
                if (type == 'd')
                    Console.Write(DoubleLine[count]);
                else
                    Console.Write(SingleLine[count]);

                if (name != "" && i == (length / 2) - (name.Length / 2) + 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(name);
                    Console.ForegroundColor = frameCol;
                }
            }
            count++;

            if (type == 'd')
                Console.Write(DoubleLine[count]);
            else
                Console.Write(SingleLine[count]);
            count++;
            Console.SetCursorPosition(x, y + 1);


            int size = name == "" ? length : length + name.Length;

            for (int i = y + 1; i < height - 2; i++)
            {
                for (int j = x; j < size + 2; j++)
                {
                    if (j == 0 || j == size + 1)
                    {
                        Console.SetCursorPosition(j, i);

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

            for (int i = 0; i < size; i++)
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
    }
}
//--------------------------------------------------------