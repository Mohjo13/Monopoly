using Monopoly.Core.Boards;             // for Board
using Monopoly.Core.Commons;            // for IDice
using Monopoly.Core.Players;
using Monopoly.Core.Players.States; // for other states like InJailState, RollDiceState

namespace Monopoly.Core.Players.States
{
    #region Start Turn State
    /// <summary>
    /// This state represents: "It is now this player's turn."
    ///
    /// Responsibilities:
    /// - Check the player's situation at the start of the turn.
    /// - Decide WHICH next state is correct:
    ///     * If player is in jail  -> go to InJailState.
    ///     * If not in jail        -> go to RollDiceState.
    ///
    /// This state itself does NOT roll dice or move the player.
    /// It only "routes" the turn to the correct next state.
    /// </summary>
    public sealed class StartTurnState : PlayerStateBase
    {
        public override string Name => "StartTurn";

        // The board and dice this state will use to route next states.
        private readonly Board _board;
        private readonly IDice _dice;

        /// <summary>
        /// We inject Board and Dice so this state (and the next ones)
        /// can work without talking directly to GameManager.
        /// </summary>
        public StartTurnState(Board board, IDice dice)
        {
            _board = board;
            _dice = dice;
        }

        public override void Enter(Player player)
        {
            // This runs ONCE when we enter StartTurnState.
            //
            // Nice place to reset any "per turn" stuff later if needed.
            // For now we just leave it empty.
        }

        public override void Execute(Player player)
        {

            // STEP1: If the player is marked as bankrupt, they should not act anymore.
            if (player.IsBankrupt)
            {
                Console.WriteLine($"{player.Name} is bankrupt and cannot take a turn.");
                player.ChangeState(new EndTurnState());
                return;
            }
            // STEP 2: Check if the player is in jail.
            if (player.InJail)
            {
                // The player is in jail.
                // We send them to the "InJail" state
                // where the jail-logic will live later.
                Console.WriteLine($"{player.Name} is jailed ");
                player.ChangeState(new InJailState());
                return;
            }

            // STEP 2: Otherwise, the player is free and can roll dice.
            // So we send them to the "RollDice" state.

            player.ChangeState(new RollDiceState(_board, _dice));/// needs changing???
        }

       /* public override void Execute(Player player)//for testing purposes only
        {
            // For this simple test we ignore jail logic.

            // Go directly to RollDiceState.
            player.ChangeState(new RollDiceState(_board, _dice));
        }*/


        public override void Exit(Player player)
        {
            // This runs ONCE when we LEAVE StartTurnState.
            // No specific cleanup needed right now.
        }
    }
    #endregion
}
