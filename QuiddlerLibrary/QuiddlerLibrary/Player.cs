/* Program:         QuiddlerLibrary
 * Module:          Player.cs
 * Author:          Danielle Menezes de Mello Miike
 *                  Priscilla Peron
 * Date:            February 10, 2022
 * Description:     Player class to handle players hand/cards
 */
using System.Collections.Generic;
using Microsoft.Office.Interop.Word;

namespace QuiddlerLibrary
{
    internal class Player : IPlayer
    {
        // hand is composed by: letter, quantanties (amount of the same card)
        private List<string> hand;
        private Deck deck;
        
        private int playerScore;


        //Constructors
        public Player(Deck d)
        {
            hand = new List<string>();
            this.deck = d;
            playerScore = 0;
            for (int idx = 0; idx < deck.CardsPerPlayer; ++idx)
                DrawCard();
        }

        public int CardCount => hand.Count;

        public int TotalPoints
        {
            get
            {
                return playerScore;
            }
        }

        /* Function Name: DrawCard
         * Description: function with no parameters to draw cards in the game. It returns the card value
         */
        public string DrawCard()
        {
            hand.Add(deck.GetFirstCard());
            return hand[hand.Count - 1];
        }
        /* Function Name: Discard
         * Description: function receive a string to remove cards on hand. It returns a boolean
        */
        public bool Discard(string card)
        {
            deck.SetDiscardPile(card);
            return hand.Remove(card);
        }

        /* Function Name: PickupTopDiscard
         * Description: function to pick the top discard card. It returns a string
        */
        public string PickupTopDiscard()
        {
            hand.Add(deck.TopDiscard);
            return deck.TopDiscard;
        }

        /* Function Name: PlayWord
         * Description: function receives a string to test if it is a valid word. It returns a int
        */
        public int PlayWord(string candidate)
        {
            playerScore += TestWord(candidate);
            string[] wordArr = candidate.Split(' ');
            foreach (var item in wordArr)
            {
                hand.Remove(item);
            }

            return playerScore;
        }

        /* Function Name: TestWord
         * Description: function receives a string to test if it is a valid word. It returns a string
        */
        public int TestWord(string candidate)
        {
            int wordScore = 0;
            string[] wordArr = candidate.Split(' ');
            string word = string.Join("", wordArr);
            bool hasCards = true;
            Application application = new Application();
            if (application.CheckSpelling(word))
            {
                for (int i = 0; i < wordArr.Length; i++)
                {
                    if (hand.Contains(wordArr[i]))
                        wordScore += deck.GetScore(wordArr[i]);
                    else
                    {
                        hasCards = false;
                        break;
                    }
                }
                if (!hasCards)
                {
                    application.Quit();
                    return 0;
                }

            }
            application.Quit();
            return wordScore;
        }
        /* Function Name: ToString
         * Description: function to handle with the hand print. It returns a string
        */
        public override string ToString()
        {
            string playerHandString = "";
            for (int i = 0; i < hand.Count; i++)
            {
                playerHandString += hand[i];
                if (i < hand.Count - 1)
                {
                    playerHandString += " ";
                }
            }
            return playerHandString;
        }


        
    }
}
