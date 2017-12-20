using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    public enum SubscriptionType { Daily, Weekly, Monthly, Yearly }

    public class Subscription : ICSVWritable, IConsoleWritable, IComparable
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SubscriptionType St { get; set; }

        public Subscription(string name, double amount, DateTime startdate, DateTime enddate, SubscriptionType st)
        {
            Name = name;
            Amount = amount;
            StartDate = startdate;
            EndDate = enddate;
            St = st;
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