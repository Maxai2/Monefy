using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------------------------
namespace Monefy
{
    class FileManager
    {
        public string CSV { get; set; }
        public Data Dat { get; set; }
        public Settings Setting { get; set; }
        public Language Lang { get; set; }
        public Currency Cur { get; set; }
    }
}
//--------------------------------------------------------