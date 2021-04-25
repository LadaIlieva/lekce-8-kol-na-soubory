using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekce_8__úkol_na_soubory
{
    public class Navsteva
    {
        public string Jmeno { get; set; }
        public int Vek { get; set; }

        public Navsteva(string jmeno, int vek)
        {
            Jmeno = jmeno;
            Vek = vek;
        }

        public Navsteva()
        {

        }
    }
}
