using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region Utility Square
    /// <summary>
    /// Utility. Rent depends on dice roll and number of utilities owned.
    ///  rules factor dice later.
    /// </summary>
    public class UtilitySquare : Square
    {
        #region Properties
        public int Price { get; }//price of roll
        public Player? Owner { get; set; }//owned by which player
        #endregion

        #region Constructor
        public UtilitySquare(int index, string name, int price)//properties to be inherited fro other classes, specifically prive 
            : base(index, name, SquareType.Utility)//inherits base from parent square
        {
            Price = price;//value to be sent 
        }
        #endregion

        #region Future behavior (commented)
        // rent logic with dice roll to be added
        #endregion
    }
    #endregion
}
