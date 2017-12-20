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
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SubscriptionType St { get; set; }

        public Subscription()
        {

        }

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