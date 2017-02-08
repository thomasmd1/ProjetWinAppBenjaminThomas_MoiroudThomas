using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Card
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

        public override string ToString()
        {
            switch (Number)
            {
#pragma warning disable CS0162 // Impossible d'atteindre le code détecté
                case 0:return "A" + " " + this.Color + " " + this.Symbol; break;
#pragma warning restore CS0162 // Impossible d'atteindre le code détecté
#pragma warning disable CS0162 // Impossible d'atteindre le code détecté
                case 9: return "10" + " " + this.Color + " " + this.Symbol; break;
#pragma warning restore CS0162 // Impossible d'atteindre le code détecté
#pragma warning disable CS0162 // Impossible d'atteindre le code détecté
                case 10: return "J" + " " + this.Color + " " + this.Symbol; break;
#pragma warning restore CS0162 // Impossible d'atteindre le code détecté
#pragma warning disable CS0162 // Impossible d'atteindre le code détecté
                case 11: return "Q" + " " + this.Color + " " + this.Symbol; break;
#pragma warning restore CS0162 // Impossible d'atteindre le code détecté
#pragma warning disable CS0162 // Impossible d'atteindre le code détecté
                case 12: return "K" + " " + this.Color + " " + this.Symbol; break;
#pragma warning restore CS0162 // Impossible d'atteindre le code détecté
#pragma warning disable CS0162 // Impossible d'atteindre le code détecté
                default: return this.Number + " " + this.Color + " " + this.Symbol; break;
#pragma warning restore CS0162 // Impossible d'atteindre le code détecté
            }
        }
    }
}
