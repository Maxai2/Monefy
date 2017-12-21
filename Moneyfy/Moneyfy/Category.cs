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
        public string Name { get; set; }
        public Type type { get; set; }

        public Category(string name, Type ty)
        {
            Name = name;
            type = ty;
        }
    }
}
//--------------------------------------------------------