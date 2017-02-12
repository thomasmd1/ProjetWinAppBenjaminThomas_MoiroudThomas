using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            CreateDeck();
        }

        //Création du deck de carte
        public void CreateDeck()
        {
            Cards = new List<Card>();
            for(int i = 0; i < 4; i++)
            {
                for(int o = 0; o < 13; o++)
                {
                    switch (i)
                    {
                        case 0:Cards.Add(new Card(o,"Black","♣")); break;
                        case 1: Cards.Add(new Card(o, "Black", "♠")); break;
                        case 2: Cards.Add(new Card(o, "Red", "♥")); break;
                        case 3: Cards.Add(new Card(o, "Red", "♦")); break;
                    }
                }
            }
        }

        //Mélange des cartes
        public List<Card> Shuffle(List<Card> deck)
        {
            return deck;
        }
    }
}
