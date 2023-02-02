/* Program:         QuiddlerLibrary
 * Module:          Deck.cs
 * Author:          Danielle Menezes de Mello Miike
 *                  Priscilla Peron
 * Date:            February 10, 2022
 * Description:     Deck class to handle deck/cards functions
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace QuiddlerLibrary
{

    public class Deck : IDeck
    {

        //Cards available in the deck
        internal static Dictionary<string, KeyValuePair<int, int>> cardsInDeck = new Dictionary<string, KeyValuePair<int, int>>
        {
           { "a", new KeyValuePair<int, int>(10, 2) },
            { "b", new KeyValuePair<int, int>(2, 8) },
            { "c", new KeyValuePair<int, int>(2, 8) },
            { "d", new KeyValuePair<int, int>(4, 5) },
            { "e", new KeyValuePair<int, int>(12, 2) },
            { "f", new KeyValuePair<int, int>(2, 6) },
            { "g", new KeyValuePair<int, int>(4, 6) },
            { "h", new KeyValuePair<int, int>(2, 7) },
            { "i", new KeyValuePair<int, int>(8, 2) },
            { "j", new KeyValuePair<int, int>(2, 13) },
            { "k", new KeyValuePair<int, int>(2, 8) },
            { "l", new KeyValuePair<int, int>(4, 3) },
            { "m", new KeyValuePair<int, int>(2, 5) },
            { "n", new KeyValuePair<int, int>(6, 5) },
            { "o", new KeyValuePair<int, int>(8, 2) },
            { "p", new KeyValuePair<int, int>(2, 6) },
            { "q", new KeyValuePair<int, int>(2, 15) },
            { "r", new KeyValuePair<int, int>(6, 5) },
            { "s", new KeyValuePair<int, int>(4, 3) },
            { "t", new KeyValuePair<int, int>(6, 3) },
            { "u", new KeyValuePair<int, int>(6, 4) },
            { "v", new KeyValuePair<int, int>(2, 11) },
            { "w", new KeyValuePair<int, int>(2, 10) },
            { "x", new KeyValuePair<int, int>(2, 12) },
            { "y", new KeyValuePair<int, int>(4, 4) },
            { "z", new KeyValuePair<int, int>(2, 14) },
            { "cl", new KeyValuePair<int, int>(2, 10) },
            { "er", new KeyValuePair<int, int>(2, 7) },
            { "in", new KeyValuePair<int, int>(2, 7) },
            { "qu", new KeyValuePair<int, int>(2, 9) },
            { "th", new KeyValuePair<int, int>(2, 9) },
        };

        //Private properties
        private int cardsPerPlayer;
        private List<string> DeckOfCards;
        private static string DiscardPile;
    
        //contructor
        public Deck()
        {
            DeckOfCards = new List<string>();
            DiscardPile = null;
            repopulate();
        }
       
        public string About
        {
            get
            {
                return "Test Client for QuiddlerLibrary (TM) Library © 2022 Priscilla Peron and Danielle Miike";
            }
        }

        public int CardCount
        {
            get
            {
                return DeckOfCards.Count;
            }
        }

        public int CardsPerPlayer
        {
            get
            {
                return cardsPerPlayer;
            }

            set
            {
                if (value < 3 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("CardsPerPlayer must be between 3 and 10");
                }

                cardsPerPlayer = value;
            }
        }
        public string TopDiscard
        {
            get
            {
                if (DiscardPile == null)
                {
                    DiscardPile = GetFirstCard();
                }
                return DiscardPile;
            }
        }

        public IPlayer NewPlayer()
        {
            IPlayer player = new Player(this);
            return player;
        }


        /* Function Name: ToString
         * Description: function to handle print of cards in the deck. It returns a string
         */
        public override string ToString()
        {
            string msg = "";
            if (this.CardCount == 118)
                msg += $"Deck initialized with the following {this.CardCount} cards...\n";
            else
                msg += $"The deck now contains the following {this.CardCount} cards... \n";

            int maxNumberCol = 0;
            foreach (var item in cardsInDeck)
            {
                if (item.Value.Key != 0)
                {
                    msg += $"{item.Key}({item.Value.Key})\t";
                    maxNumberCol++;
                }
                if (maxNumberCol == 12)
                {
                    msg += "\n";
                    maxNumberCol = 0;
                }
            }
            msg += "\n";
            return msg;
        }

        /* Function Name: SetDiscardPile
         * Description: void function, receive a string to update DiscardPile variable
         */
        internal void SetDiscardPile(string cardFromPlayer)
        {
            DiscardPile = cardFromPlayer;
        }

        /* Function Name: Shuffle
         * Description: void function with no parameters to shuffle the deck
         */
        internal void Shuffle()
        {
            Random rng = new Random();
            DeckOfCards = DeckOfCards.OrderBy(card => rng.Next()).ToList();
        }

        /* Function Name: repopulate
         * Description: void function to repopulate DeckOfCards every new game
         */
        internal void repopulate()
        {
            DeckOfCards.Clear();
            foreach (var item in cardsInDeck)
            {
                for (int idx = 0; idx < item.Value.Key; idx++)
                {
                    DeckOfCards.Add(item.Key);
                }
            }

            //Randomize the deck    
            Shuffle();
        }

        /* Function Name: GetScore
         * Description: function receive a string to return the card value. It returns an int
         */
        internal int GetScore(string card)
        {
            return cardsInDeck[card].Value;
        }

        /* Function Name: GetFirstCard
         * Description: function to get the first card and remove it from deck. It returns a string
         */
        internal string GetFirstCard()
        {
            string card = DeckOfCards[0];
            cardsInDeck[card] = new KeyValuePair<int, int>(cardsInDeck[card].Key - 1, cardsInDeck[card].Value);
            DeckOfCards.RemoveAt(0);

            return card;
        }
    }
}
