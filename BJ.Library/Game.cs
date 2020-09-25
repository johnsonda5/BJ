using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BJ.Library
{
    public class Game
    {
        public Hand DealerHand { get; private set; }
        public Hand UserHand { get; private set; }
        public Deck cardDeck;
        public double Pot { get; set; }
        public bool BetPlaced { get; private set; }

        public Game()
        {
            cardDeck = new Deck();      // Get a new deck of cards
            DealerHand = new Hand();           // Set dealerTotal to 0
            UserHand = new Hand();
            Pot = 0;
        }

        public void DealCard(Hand hand, bool faceup)
        {
            var card = cardDeck.Draw();     // Draw a card from the deck
            card.FaceUp = faceup;           // Face it up or down
            if (hand == DealerHand && DealerHand.HandTotal() >= 17)
            {
                return;
            }

            hand.Cards.Add(card);                 // Give it the player's hand
        }
        public string Stand()
        {
            int userTotal = UserHand.HandTotal();
            int dealerTotal = DealerHand.HandTotal();

            if (userTotal <= 21)
            {
                if (Math.Abs(21 - userTotal) < Math.Abs(21 - dealerTotal))
                {
                    return "user";
                }
            }
            if (userTotal <= 21 && userTotal == dealerTotal)
            {
                return "draw";
            }

            return "dealer";
        }

        public void Bet(double amount)
        {
            Pot += amount;
            BetPlaced = true;
        }

        public void ExitGame()
        {
            // Save carreer earnings and stats to text file for load function that will abbed later
            // Or save after every move is made

            // If the game suddenlys end or they close the app, then save the game
            // so they dont quit and come back whenever they lose money

            // End this current session
        }
    }
}
