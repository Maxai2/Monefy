using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    public class Application
    {
        private static Application instance;

        public static Application getInstance()
        {
            if (instance == null)
                instance = new Application();

            return instance;
        }

        static public List<MoneyOperation> Outcomes = new List<MoneyOperation>();
        static public List<Category> Incomes = new List<Category>();
        static public List<Account> Accounts = new List<Account>();
        static public List<Category> Categories = new List<Category>();
        static public List<Subscription> Subscriptions = new List<Subscription>();

        public void AddCategories(string name, Type ty)
        {
            Category temp = new Category(name, ty);

            Categories.Add(temp);
        }

        public void AddAccount(string name, Currency cur, double money, bool hidden)
        {
            Account temp = new Account(name, cur, money, hidden);

            Accounts.Add(temp);
        }

        public void AddOutcomes(double amount, Category cat, string note, DateTime date)
        {
            MoneyOperation temp = new MoneyOperation(amount, cat, note, date);

            Outcomes.Add(temp);
        }
    }
}
//--------------------------------------------------------