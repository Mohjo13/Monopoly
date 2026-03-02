using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region Free Parking
    /// <summary>
    /// Free Parking square. House rules may add pot payouts; default is "nothing happens".
    /// </summary>
    public sealed class FreeParkingSquare : Square
    {
        public FreeParkingSquare(int index)//position in board enum
            : base(index, "Free Parking", SquareType.FreeParking) { }
        //base:before I do anything else,
        //call the constructor in my base class (square), and give it these values.”
        //position on the board, name and type


        #region Distributed behaviour (effectss)
        /// <summary>
        /// Called when a player lands on Free Parking.
        /// </summary>
        public override void OnLand(Player player, Board board)
        {
            // should: do nothing (default rule) 
            // optional: could later give a pot of money if using house rules
        }
        #endregion
    }
    #endregion


}
