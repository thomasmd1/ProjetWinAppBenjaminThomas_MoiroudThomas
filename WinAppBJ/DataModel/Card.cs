using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class Card
    {
        public int Number { get; set; }
        public String Color { get; set; }
        public String Symbol { get; set; }

        public Card(int n,String c,String s)
        {
            this.Number = n;
            this.Color = c;
            this.Symbol = s;
        }
    }
}
