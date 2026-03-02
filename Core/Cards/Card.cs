using Monopoly.Core.Commons;

namespace Monopoly.Core.Cards
{
    #region Card (abstract)
    /// <summary>
    /// Base for all cards. 
    /// Connections:
    /// - Used by CardDeck (draw/return)...
    /// </summary>
    public abstract class Card : ICard//inhertiable properties
    {
        #region Properties
        public string Title { get; }//card name
        public string Description { get; }//what does it do
        public CardType CardType { get; }//card type chance, comunity...
        #endregion

        #region Constructor
        protected Card(string title, string description, CardType type)//base peoperties for all the cards to share and need to have 
        {
            Title = title;
            Description = description;
            CardType = type;
        }
        #endregion

        #region Future behavior (commented)
        // apply to player and game
        #endregion
    }
    #endregion
}
