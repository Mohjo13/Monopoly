using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Player State Interface
    /// <summary>
    /// Common contract for all player states in the FSM.
    /// Every state is like a "mode" the player can be in.
    /// The Player will call:
    /// - Enter(...) when we switch into this state
    /// - Execute(...) when we want the state to "do its thing"
    /// - Exit(...) when we leave the state
    /// </summary>
    public interface IPlayerState
    {
        /// <summary>
        /// Optional: nice for debugging / logging which state is active.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Called once when the player enters this state.
        /// Good place to reset timers, set flags, show messages etc.
        /// </summary>
        void Enter(Player player);

        /// <summary>
        /// Called to perform the main logic of this state.
        /// For example: roll dice, move, decide what to do, etc.
        /// </summary>
        void Execute(Player player);

        /// <summary>
        /// Called once when the player leaves this state.
        /// Good place to clean up or turn off things.
        /// </summary>
        void Exit(Player player);
    }
    #endregion
}
