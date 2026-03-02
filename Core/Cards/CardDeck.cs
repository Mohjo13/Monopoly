using System.Collections.Generic;
using Monopoly.Core.Commons;

namespace Monopoly.Core.Cards
{
    #region Card Deck
    /// <summary>
    /// deck handling draw/return/shuffle for Chance or Community Chest.
    /// Connections: DrawCardSquare triggers a draw; cards are returned/shuffled by rules.
    /// </summary>
    public class CardDeck
    {
        #region Properties / Fields
        public CardType Type { get; }//get the card type from cardtypes
        private readonly Queue<Card> _cards;//readonly so nobody can manipulate
        #endregion

        #region Constructor
        public CardDeck(CardType type, IEnumerable<Card> cards)//card type and deck as well as deck order
        {
            Type = type;//using this type
            _cards = new Queue<Card>(cards);//Make a new deck with these cards, arranged in a queue
        }
        #endregion

        #region Future behavior (commented)
        // public Card Draw() 
        // public void ReturnToBottom
        // public void Shuffle() 
        #endregion
    }
    #endregion
}
