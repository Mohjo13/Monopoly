using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region End Turn State
    /// <summary>
    /// This state means:
    /// "I am done with everything I can do this turn."
    ///
    /// Responsibilities:
    /// - Tell the GameManager that this player's turn is finished.
    /// - Then return the player to IdleState (waiting mode),
    ///   so the GameManager can safely pick the next player.
    ///
    /// IMPORTANT:
    /// The EndTurnState itself does NOT move to next player.
    /// That is the GameManager's job.
    /// </summary>
    public sealed class EndTurnState : PlayerStateBase
    {
        public override string Name => "EndTurn";

        public override void Enter(Player player)
        {
            // This is called once when entering EndTurnState.
            // Great spot for debugging later, e.g.:
            // Console.WriteLine($"{player.Name} reached EndTurnState.");
        }

        public override void Execute(Player player)
        {
            // STEP 1: Move the player into IdleState.
            // This keeps the FSM clean and consistent: the player
            // is "waiting" while the GameManager switches turns.

            player.ChangeState(new IdleState());

            // STEP 2: We do NOT switch directly to the next player's turn.
            // The GameManager will do that AFTER this player is marked idle.
            //
            // Example later in GameManager (NOT here):
            // _currentPlayerIndex++;
            // GetNextPlayer().ChangeState(new StartTurnState());
        }

        public override void Exit(Player player)
        {
            // Runs when leaving EndTurnState; nothing needed here.
        }
    }
    #endregion
}
