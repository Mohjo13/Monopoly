using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Idle State
    /// <summary>
    /// Player is not doing anything right now.
    /// This is like "waiting on the couch" until the GameManager
    /// decides it is this player's turn.
    ///
    /// Behaviour:
    /// - Enter: can be used for logging/debugging.
    /// - Execute: by default does NOTHING (no automatic actions).
    /// - Exit: also does nothing.
    /// </summary>
    public sealed class IdleState : PlayerStateBase
    {
        public override string Name => "Idle";

        public override void Enter(Player player)
        {
            // This runs ONCE when the player enters IdleState.
            // Good moment for debugging.
            //
            // Example (later):
            // Console.WriteLine($"{player.Name} is now Idle.");
        }

        public override void Execute(Player player)
        {
            // IMPORTANT:
            // IdleState does NOT "drive" the game by itself.
            //
            // That means:
            // - We do NOT roll dice here.
            // - We do NOT move the player.
            //
            // The GameManager (or turn system) will decide
            // WHEN to move this player to StartTurnState.
            //
            // So here we simply "do nothing".
        }

        public override void Exit(Player player)
        {
            // This runs ONCE when we LEAVE IdleState.
            // Right before we go into something like StartTurnState.
            //
            // No cleanup needed right now, so we leave it empty.
        }
    }
    #endregion
}
