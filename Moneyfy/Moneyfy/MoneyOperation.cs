using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Moneyfy
{
    class MoneyOperation : ICSVWritable, IConsoleWritable, IComparable
    {
        double Amount;
        Category cat;
        string Note;
        DateTime Date;

        public void WriteCSV()
        {
            throw new NotImplementedException();
        }

        public void WriteConsole()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
//--------------------------------------------------------