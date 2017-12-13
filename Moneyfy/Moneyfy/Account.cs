using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    enum Currency { AZN, RUB, EUR, USD }
    
    class Account : ICSVWritable, IConsoleWritable, IComparable
    {
        public string Name { get; set; }
        public Currency cur { get; set; }
        public double Money { get; set; }
        public bool Hidden { get; set; }

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