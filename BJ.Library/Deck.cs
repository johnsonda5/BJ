using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BJ.Library
{
    public class Deck
    {
        private List<Card> Cards = new List<Card>();

        public Deck()
        {
            GenerateDeck();
        }

        private void GenerateDeck()
        {
            // Clubs
            for (int i = 1; i < 14; i++)
            {
                var card = new Card();
                card.Suit = "C";
                card.Rank = i;
                Cards.Add(card);
            }

            // Diamonds
            for (int i = 1; i < 14; i++)
            {
                var card = new Card();
                card.Suit = "H";
                card.Rank = i;
                Cards.Add(card);
            }

            // Hearts
            for (int i = 1; i < 14; i++)
            {
                var card = new Card();
                card.Suit = "D";
                card.Rank = i;
                Cards.Add(card);
            }

            // Spades
            for (int i = 1; i < 14; i++)
            {
                var card = new Card();
                card.Suit = "S";
                card.Rank = i;
                Cards.Add(card);
            }
        }

        public Card Draw()
        {
            Card drawnCard;          // Card to be returned

            int index = new Random().Next(0, Cards.Count());    // Generate random index for Card list

            drawnCard = Cards[index];       // Select card at index and set drawnCard equal to the card chosen
            Cards.RemoveAt(index);          // Remove the card from the list

            return drawnCard;               // Return the drawn card
        }

        public void Shuffle()
        {
            var random = new Random();

            for (int i = 0; i < Cards.Count(); i++)        // Loop through the remaining cards
            {
                int random_index = random.Next(0, 52 - i);    // Random new index for the remaining positions

                var select_card = Cards[random_index];     // Sets selected card to a random card in the deck
                Cards[random_index] = Cards[i];            // Put the current card where the selected card was taken from
                Cards[i] = select_card;                    // Put the selected card where the current card was
            }
        }

        #region Other Methods
        public int DeckCount()
        {
            return Cards.Count();
        }
        #endregion
    }
}
