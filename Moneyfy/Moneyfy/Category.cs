using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    public enum Type
    {
        Income,
        Outcome
    }

    public struct Category
    {
        string Name;
        Type type;

        public Category(string name, Type ty)
        {
            Name = name;
            type = ty;
        }
    }
}
//--------------------------------------------------------