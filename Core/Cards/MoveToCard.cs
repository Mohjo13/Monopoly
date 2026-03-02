using Monopoly.Core.Commons;

namespace Monopoly.Core.Cards
{
    #region Move-To Card
    /// <summary>
    /// Moves the player to a target index (passing GO).
    /// </summary>
    public sealed class MoveToCard : Card
    {
        #region Properties
        public int TargetIndex { get; }//which sqaure index should the player move to getter
        public bool PassGo { get; }//pass go based on player properties
        #endregion

        #region Constructor
        public MoveToCard(string title, string description, CardType type, int targetIndex, bool passGo)
            : base(title, description, type)//must have base
            //move to square name, description of square, if its a card what type, placenemnt of square in the board index, does it pass go?
        {
            TargetIndex = targetIndex;//targetinex of the postioniton to move to 
            PassGo = passGo;
        }
        #endregion

        #region Future behavior (commented)
        // Apply(Player and Board)
        #endregion
    }
    #endregion
}
