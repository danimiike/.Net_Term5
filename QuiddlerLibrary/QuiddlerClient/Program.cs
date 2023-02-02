/* Program:         QuiddlerClient
 * Module:          Program.cs
 * Author:          Danielle Menezes de Mello Miike
 *                  Priscilla Peron
 * Date:            February 10, 2022
 * Description:     Main class (client)
 */

using System;
using System.Collections.Generic;
using QuiddlerLibrary;

namespace QuiddlerClient
{
    class Program
    {
        private static Deck deckGame;
        private static List<IPlayer> Players;

        static void Main(string[] args)
        {
            do
            {
                //create new deck and players
                deckGame = new Deck();
                Players = new List<IPlayer>();
                
                Console.WriteLine(deckGame.About);
                Console.WriteLine();
                Console.WriteLine(deckGame.ToString());
                Console.WriteLine();

                //initializing the game
                initializeGame();
               
                int playerIndex;
                bool gameWon = false;
                do
                {
                    playerIndex = 1;

                    //each player in the list of players takes a turn
                    foreach (var player in Players)
                    {
                        gameWon = takeTurn(player, playerIndex);
                        if (gameWon)
                            break;
                        playerIndex++;
                    }
                } while (!gameWon);
                
                //Ending game
                Console.WriteLine("\nThe final scores are...");
                Console.WriteLine(new string('-', 60));
                for(int idx = 0; idx < Players.Count; idx++)
                {
                    Console.WriteLine($"Player {idx}: {Players[idx].TotalPoints} points ");
                }
                break;
            } while (true);
        }

        /* Function Name: takeTurn
         * Description: void function, with no parameters to start the game
         */

        static void initializeGame()
        {
            int playersCount, cardsCount;

            do
            {
                Console.Write("How many players are there? (1-8): ");
                if (!int.TryParse(Console.ReadLine(), out playersCount) || (playersCount < 1 || playersCount > 8))
                {
                    Console.WriteLine("[Error]: Number of players must be a number between 1-8.");
                    continue;
                }
                break;
            } while (true);

            do
            {
                Console.Write("How many cards will be dealt to each player? (3-10): ");
                if (!int.TryParse(Console.ReadLine(), out cardsCount))
                {
                    Console.WriteLine("[Error]: Number of cards must be a number between 3-10");
                    continue;
                }
                try
                {
                    deckGame.CardsPerPlayer = cardsCount;
                    break;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            } while (true);

            for (int i = 0; i < playersCount; i++)
            {
                Players.Add(deckGame.NewPlayer());
            }

            Console.WriteLine();
            Console.WriteLine($"Cards were dealt to {playersCount} player(s).");
            Console.WriteLine($"The top card which was '{deckGame.TopDiscard}' was moved to the discard pile.\n");
        }

        /* Function Name: takeTurn
         * Description: recieve a IPlayer and a int to handle with each player turn, and returns a bool
         */
        static bool takeTurn(IPlayer player, int playerIdx)
        {

            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"Player {playerIdx} ({Players[playerIdx-1].TotalPoints} points)");
            Console.WriteLine(new string('-', 60));

            Console.WriteLine(deckGame.ToString());
            Console.WriteLine();

            Console.WriteLine($"Your cards are [{player.ToString()}].");

            string userChoice;

            do
            {
                Console.Write($"Do you want the top card in the discard pile which is '{deckGame.TopDiscard}'? (y/n):");
                userChoice = Console.ReadLine().ToLower();
            } while (userChoice != "y" && userChoice != "n");

            if (userChoice == "y")
                player.PickupTopDiscard();
            else
                Console.WriteLine($"The dealer dealt '{player.DrawCard()}' to you from the deck.");

            Console.WriteLine($"Your cards are [{player.ToString()}].");

            do
            {
                Console.Write("Test a word for its points value (y/n): ");
                userChoice = Console.ReadLine().ToLower();
                if (userChoice != "y" && userChoice != "n")
                    continue;
                if (userChoice == "y")
                {
                    Console.Write($"Enter a word using [{player.ToString()}] leaving a space between cards: ");
                    string word = Console.ReadLine();
                    int points = player.TestWord(word);
                    Console.WriteLine($"The word [{word}] is worth {points} points.");
                    if (points == 0)
                        continue;
                    do
                    {
                        Console.Write($"Do you want to play the word [{word}]? (y/n): ");
                        userChoice = Console.ReadLine().ToLower();
                    } while (userChoice != "y" && userChoice != "n");

                    if (userChoice == "n")
                        continue;

                    player.PlayWord(word);

                    if (player.CardCount == 0)
                    {
                        Console.WriteLine($"***** Player {playerIdx} is out! Game over!! ******");
                        return true;
                    }
                    if (player.CardCount == 1)
                    {
                        Console.WriteLine($"Dropping '{player.ToString().Trim()}' on the discard pile.");
                        Console.WriteLine($"***** Player {playerIdx} is out! Game over!! ******");
                        return true;
                    }
                }

                Console.WriteLine($"Your cards are [{player.ToString()}] and you have {player.TotalPoints} points.");
                do
                {
                    Console.Write("Enter a card from your hand to drop on the discard pile: ");
                    userChoice = Console.ReadLine();
                } while (!player.Discard(userChoice));
                Console.WriteLine($"Your cards are [{player.ToString()}].");
                
                if (playerIdx == Players.Count)
                {
                    do
                    {
                        Console.Write($"\nWould you like each player to take another turn? (y/n): ");
                        userChoice = Console.ReadLine().ToLower();
                    } while (userChoice != "y" && userChoice != "n");
                    if (userChoice == "y")
                        return false;
                    else
                    {
                        Console.WriteLine("\nRetiring the game.\n");
                        return true;
                    }
                       
                }
                return false;
            } while (true);
        }
    }
}
