using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJ.Library
{
    public class Hand
    {
        public List<Card> Cards;

        public Hand()
        {
            Cards = new List<Card>();
        }

        public int HandTotal()
        {
            List<int> cardValues = new List<int>();
            int aces = 0;

            foreach (Card card in Cards)
            {
                if (card.Rank != 1)
                {
                    if (card.Rank >= 11)
                    {
                        cardValues.Add(10);
                    }
                    else
                    {
                        cardValues.Add(card.Rank);
                    }
                }
                else
                {
                    aces++;
                }
            }
            for (int i = 0; i < aces; i++)
            {
                if (cardValues.Sum() + 11 <= 21)
                {
                    cardValues.Add(11);
                }
                else
                {
                    cardValues.Add(1);
                }
            }

            return cardValues.Sum();
        }

        public void FlipCards()
        {
            foreach(Card card in Cards)
            {
                card.FaceUp = true;
            }
        }
    }
}
