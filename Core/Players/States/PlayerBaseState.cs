using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Player State Base
    /// <summary>
    /// Simple base class so we don't have to rewrite empty Enter/Exit in each state.
    /// Child classes only need to override what they actually use.
    /// </summary>
    public abstract class PlayerStateBase : IPlayerState
    {
        public abstract string Name { get; }

        // Default empty implementations: child states can override if needed.

        public virtual void Enter(Player player)
        {
            // Default: do nothing when entering this state.
            // Specific states can override and add behaviour.
        }

        public abstract void Execute(Player player);
        // We force every state to say what it does in Execute.

        public virtual void Exit(Player player)
        {
            // Default: do nothing when exiting this state.
        }
    }
    #endregion
}
