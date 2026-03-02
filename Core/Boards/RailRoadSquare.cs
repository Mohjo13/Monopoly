using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region Railroad Square
    /// <summary>
    /// Railroad. Rent scales with number of railroads owned by the same owner.
    /// Connections: Player Owner.
    /// </summary>
    public class RailroadSquare : Square
    {
        #region Properties
        public int Price { get; }//price of railroad 
        public Player? Owner { get; set; }//owner of soecifick raoilroad
        #endregion

        #region Constructor
        public RailroadSquare(int index, string name, int price)//propterties to be inherited postion, name, and price or railroad
            : base(index, name, SquareType.Railroad)
        {
            Price = price;//price of railroad
        }
        #endregion

        #region Distributed behaviour (effects)
        /// <summary>
        /// Called when a player lands on this railroad.
        /// </summary>
        public override void OnLand(Player player, Board board)
        {
            // should: if unowned, offer purchase
            // should: if owned by another player, charge rent based on total railroads owned
        }
        #endregion
    }
    #endregion
}
