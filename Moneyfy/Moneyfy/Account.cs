using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Moneyfy
{
    enum Currency { AZN, RUB, EUR, USD }
    
    class Account : ICSVWritable, IConsoleWritable, IComparable
    {
        string Name;
        Currency cur;
        double Money;
        bool Hidden;

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public void WriteConsole()
        {
            throw new NotImplementedException();
        }

        public void WriteCSV()
        {
            throw new NotImplementedException();
        }
    }
}
//--------------------------------------------------------