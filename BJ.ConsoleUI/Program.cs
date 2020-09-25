using BJ.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BJ.ConsoleUI
{
    class Program
    {
        static Game game;
        static double chips = 500; 
        static void Main(string[] args)
        {
            Rules();

            JoinTable();

            while (true)
            {
                game = new Game();

                Bet();

                ShuffleDeck();

                // Deal 2 cards to the player, face up
                game.DealCard(game.UserHand, true);
                game.DealCard(game.UserHand, true);

                // Deal 2 cards to the dealer, one face up, one face down
                game.DealCard(game.DealerHand, true);
                game.DealCard(game.DealerHand, false);

                // Display user hand and dealer hand after initial deal
                ShowHand(game.UserHand);
                ShowHand(game.DealerHand);

                // User decides to hit or stand
                string move = Move();

                while (move == "hit")
                {
                    // If the user hits
                    if (move == "hit")
                    {
                        game.DealCard(game.UserHand, true);
                        game.DealCard(game.DealerHand, false);
                        ShowHand(game.UserHand);
                        ShowHand(game.DealerHand);
                    }
                    // If the user stands
                    move = Move();
                }

                Stand();

                // See if the player can make the buy in
                if (chips < 50)
                {
                    Console.WriteLine("Sorry, you need more chips.");
                    Console.WriteLine("Come back later.");
                    Console.WriteLine("Thanks for playing.");
                    Thread.Sleep(2000);
                    return;
                }

                bool stillPlaying = AnotherRound();
                
                if (stillPlaying == false)
                {
                    Console.WriteLine("Thanks for playing.\n");
                    Console.WriteLine("...Leaving table...");
                    Thread.Sleep(2000);
                    return;
                }
            }
        }

        static void Rules()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string rules = "--------------------------Black Jack Rules------------------------\n";
                  rules += "-At the start of each round you will place a bet.\n";
                  rules += "-After a bet is placed you will be dealt 2 cards (both face up).\n";
                  rules += "-The dealer will deal themslves 2 cards (1 face up, 1 face down).\n";
                  rules += "-The goal is to get the total value of your hand to equal 21,\n";
                  rules += " or as close to 21 without going over.\n";
                  rules += "-Enter (hit) to draw a new card. The user will deal themselves a\n";
                  rules += " a new card as well; unless the total of their cards is greater\n";
                  rules += " than or equal to 17.\n";
                  rules += "-When you are confident with your hand, enter (stand).\n";
                  rules += "-When you stand the dealer will flip their face down cards.\n";
                  rules += "-You win if your hand is 21, or closer to 21 than the dealers hand.\n";
                  rules += "-If both hands are greater than, you lose no matter what.\n";
                  rules += "-If both hands are less than 21 and are equal, the round is a draw,\n";
                  rules += " and you receive your bet back.\n";
                  rules += "-If you win the round the round, you will receive 1.5x your bet.\n";
                  rules += "-In-game commands: (hit) (stand) (quit)\n";
                  rules += "------------------------------------------------------------------";
            Console.WriteLine(rules);
        }

        static void JoinTable()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press [ENTER] to join the Blackjack table!");
            Console.ReadLine();
        }

        static void Bet()
        {
            int bet = 0;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"-Chips: ${chips}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-Place Bet:");
                string input = Console.ReadLine();

                try
                {
                    bet = int.Parse(input);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n***Invalid Input***\n");
                    continue;
                }

                if (bet > chips)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n***You only have ${chips} in chips***\n");
                    continue;
                }

                if (bet < 50)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n***Minimum bet is $50***\n");
                    continue;
                }
                chips -= bet;
                game.Pot += bet;
                return;
            }
        }

        static void ShuffleDeck()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n...Shuffling Deck...\n");
            Thread.Sleep(1000);
        }
        
        static void ShowHand(Hand hand)
        {
            if (hand == game.UserHand)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-----Your Hand-----");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----Dealer Hand----");
            }
            foreach (Card card in hand.Cards)
            {
                string output = string.Empty;

                if (card.FaceUp)
                {
                    string rank = "";
                    string suit = card.Suit;

                    if (card.Rank == 1)
                    {
                        rank = "A";
                    }
                    else if (card.Rank == 11)
                    {
                        rank = "J";
                    }
                    else if (card.Rank == 12)
                    {
                        rank = "Q";
                    }
                    else if (card.Rank == 13)
                    {
                        rank = "K";
                    }
                    else
                    {
                        rank = card.Rank.ToString();
                    }

                    if (rank == "10")
                    {
                        output += $" ----- \n";
                        output += $"|{rank}   |\n";
                        output += $"|     |\n";
                        output += $"|  {suit}  |\n";
                        output += $"|     |\n";
                        output += $"|   {rank}|\n";
                        output += $"x-----x";
                        Console.WriteLine(output);
                    }
                    else
                    {
                        output += $" ----- \n";
                        output += $"|{rank}    |\n";
                        output += $"|     |\n";
                        output += $"|  {suit}  |\n";
                        output += $"|     |\n";
                        output += $"|    {rank}|\n";
                        output += $" ----- ";
                        Console.WriteLine(output);
                    } 
                }
                else
                {
                    output += $" ----- \n";
                    output += $"|     |\n";
                    output += $"|     |\n";
                    output += $"|  ?  |\n";
                    output += $"|     |\n";
                    output += $"|     |\n";
                    output += $" ----- ";
                    Console.WriteLine(output);
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
        }

        static string Move()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(hit) or (stand)");
                string input = Console.ReadLine();
                
                if (input.ToLower() != "hit" && input.ToLower() != "stand")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n***Invalid Input***\n");
                    continue;
                }
                else
                {
                    return input;
                }
            }
        }

        static void Stand()
        {
            string winner = game.Stand();

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (winner == "user")
            {
                double winnings = game.Pot * 1.5;
                chips += winnings;                   
                Console.WriteLine("###FINAL HANDS####");
                ShowHand(game.UserHand);
                game.DealerHand.FlipCards();
                ShowHand(game.DealerHand);
                Console.WriteLine($"\nYou bet ${game.Pot}.");
                Console.WriteLine($"\nRound Win. +${winnings}.");
                Console.WriteLine($"Chips: ${chips}");
                Console.WriteLine("##################\n");
            }
            else if (winner == "draw")
            {
                double winnings = game.Pot;
                chips += winnings;
                Console.WriteLine("###FINAL HANDS####");
                ShowHand(game.UserHand);
                game.DealerHand.FlipCards();
                ShowHand(game.DealerHand);
                Console.WriteLine($"\nYou bet ${game.Pot}.");
                Console.WriteLine($"\nDraw... + ${winnings}.");
                Console.WriteLine($"Chips: ${chips}");
                Console.WriteLine("##################\n");
            }
            else
            {
                Console.WriteLine("###FINAL HANDS####");
                ShowHand(game.UserHand);
                game.DealerHand.FlipCards();
                ShowHand(game.DealerHand);
                Console.WriteLine($"\nYou bet ${game.Pot}.");
                Console.WriteLine($"\nRound Loss. - ${game.Pot}.");
                Console.WriteLine($"Chips: ${chips}");
                Console.WriteLine("##################\n");
            }
        }

        static bool AnotherRound()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Another round? (yes) (no):");
                string input = Console.ReadLine();
                Console.WriteLine();
                if (input.ToLower() != "yes" && input.ToLower() != "no")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n***Invalid Input***\n");
                    continue;
                }

                if (input == "yes")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
