using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region Jail Squares
    /// <summary>
    /// Jail / Just Visiting square.
    /// </summary>
    public sealed class JailSquare : Square
    {
        public JailSquare(int index)//position on board get from board
            : base(index, "Jail / Just Visiting", SquareType.Jail) { }//base:before I do anything else,
                                                                      //call the constructor in my base class (square), and give it these values.”
                                                                      //position on the board, name and type

    }

    /// <summary>
    /// Go To Jail square that sends the player to JailIndex.
    /// </summary>
    public sealed class GoToJailSquare : Square//sealed class so that it can not be inherited by any other script or class
    {
        #region Properties
        public int JailIndex { get; }
        #endregion

        #region Constructor
        public GoToJailSquare(int index, int jailIndex)//postin on the board for GOTOJAIL, positon in the jail
            : base(index, "Go To Jail", SquareType.GoToJail)
        {
            JailIndex = jailIndex;//jail postion on the board
        }
        #endregion

        #region Distributed behaviour (effects)
        /// <summary>
        /// Called when a player lands on this square — should send them to jail.
        /// </summary>
        public override void OnLand(Player player, Board board)
        {
            // should: move the player to jail position (player.MoveTo(JailIndex, board))
            // should: mark player as InJail = true
        }
        #endregion
    }
    #endregion
}
