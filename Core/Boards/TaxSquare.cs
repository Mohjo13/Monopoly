using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region Tax Square
    /// <summary>
    /// Tax payment square (Income Tax, Luxury Tax).
    /// </summary>
    public class TaxSquare : Square//inherits base from square
    {
        #region Properties
        public int Amount { get; }//amount of tax to pay
        #endregion

        #region Constructor
        public TaxSquare(int index, string name, int amount)//properites that can be inerited from other classes, amount 
            : base(index, name, SquareType.Tax)//base class properties to be shared from square
        {
            Amount = amount;//amount of tax to be payed based on player holdings
        }
        #endregion

        #region Distributed behaviour (effects)
        /// <summary>
        /// Called when a player lands here — should reduce their money by the tax amount.
        /// </summary>
        public override void OnLand(Player player, Board board)
        {
            // should: player.Debit(Amount)
        }
        #endregion
    }
    #endregion
}
