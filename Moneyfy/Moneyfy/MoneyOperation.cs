using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    class MoneyOperation : ICSVWritable, IConsoleWritable, IComparable
    {
        public double Amount { get; set; }
        public Category cat { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }

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