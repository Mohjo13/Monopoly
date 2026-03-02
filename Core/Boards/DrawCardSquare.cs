using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region Draw-Card Square (Chance / Community Chest)
    /// <summary>
    /// Landing here triggers a draw from the specified deck type.
    /// Connections: CardDeck 
    /// </summary>
    public class DrawCardSquare : Square
    {
        #region Properties
        public CardType DrawFrom { get; }//inherits from square, gets properties
        #endregion

        #region Constructor
        public DrawCardSquare(int index, string name, CardType type)
            // "base(...)" calls the parent (Square) constructor to set shared info:
            // - index: where this square is on the board
            // - name: the text label ("Chance")
            // - type == CardType.Chance ? SquareType.Chance : SquareType.CommunityChest
            //   If it's a Chance card type, mark this as a Chance square.
            //   Otherwise, mark it as a Community Chest square.
            : base(index, name, type == CardType.Chance ? SquareType.Chance : SquareType.CommunityChest)
            // Store which deck this square draws from (Chance or Community Chest)
            // in the DrawFrom property defined above.
        {
            DrawFrom = type;//remebers which cardtype to use
        }
        #endregion

        #region Distributed behaviour (effects)
        /// <summary>
        /// Called when a player lands on this square.
        /// </summary>
        public override void OnLand(Player player, Board board)
        {
            // should: tell the correct CardDeck (Chance or Community Chest) to draw a card
            // should: apply the card's effect to the player
        }
        #endregion
    }
    #endregion
}
