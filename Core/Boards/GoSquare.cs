using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region GO Square
    /// <summary>
    /// This class represents the "GO" square — the first square on the Monopoly board.
    /// When players pass or land here, they usually collect salary (like $200).
    /// </summary>
    public sealed class GoSquare : Square  // "sealed" means no other class can inherit from this one.
    {
        #region Properties

        public int Salary { get; }
        // The amount of money a player receives when they pass or land on GO.
        #endregion

        #region Constructor

        public GoSquare(int index, int salary)
            // runs when the game creates a new GoSquare.
            // Parameters:
            // index: the position of this square on the board (START=0).
            // salary: how much money a player gets for passing/landing here.

            : base(index, "GO", SquareType.Go)
        // Calls the base "Square" constructor to set shared info:
        // index
        // name
        // type = SquareType.Go
        {

            Salary = salary;
            // Store the salary amount in this object's property.(player?)
        }
        #endregion
        #region Distributed behaviour (effects)
        /// <summary>
        /// Called when a player PASSES this square.
        /// </summary>
        public override void OnPass(Player player, Board board)
        {
            // should: give the player their salary for passing GO (e.g., player.Credit(Salary))
        }

        /// <summary>
        /// Called when a player LANDS on this square.
        /// </summary>
        public override void OnLand(Player player, Board board)
        {
            // should: give the player salary for landing directly on GO
        }
        #endregion
    }
    #endregion
}
