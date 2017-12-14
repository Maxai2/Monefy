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
        char[] DoubleLine = { (char)201, (char)205, (char)187, (char)186, (char)188, (char)205, (char)200, (char)186};

        char[] SingleLine = { (char)218, (char)196, (char)191, (char)179, (char)217, (char)196, (char)192, (char)179 };
        //--------------------------------------------------------
        void MainFrame(char type)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(DoubleLine[0]);
            for (int i = 0; i < length; i++)
            {

            }
        }
    }
}
//--------------------------------------------------------