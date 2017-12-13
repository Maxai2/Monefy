using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    enum SubscriptionType { Daily, Weekly, Monthly, Yearly }

    class Subscription : ICSVWritable, IConsoleWritable, IComparable
    {
        string Name;
        double Amount;
        DateTime StartDate;
        DateTime EndDate;
        SubscriptionType st;

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