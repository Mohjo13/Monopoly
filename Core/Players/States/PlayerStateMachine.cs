using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Player State Machine
    /// <summary>
    /// This class is the "brain manager" for the player's states.
    /// It does NOT know Monopoly rules.
    /// It only knows:
    /// - which state we are in right now
    /// - how to switch to another state
    /// - how to tell the current state to "do its thing"
    ///
    /// The Player will later OWN one of these.
    /// Example later:
    ///   player.StateMachine = new PlayerStateMachine(player, new IdleState());
    /// </summary>
    public class PlayerStateMachine
    {
        #region Fields (who do I control?)
        /// <summary>
        /// The player that this state machine belongs to.
        /// Think: "I am the brain for THIS player."
        /// </summary>
        private readonly Player _owner;

        /// <summary>
        /// The state we are currently in (Idle, StartTurn, Moving, etc.).
        /// </summary>
        public IPlayerState CurrentState { get; private set; }

        /// <summary>
        /// The state we just came from (can be null at the beginning).
        /// Useful if we ever want to go back to the previous state.
        /// </summary>
        public IPlayerState? PreviousState { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new State Machine for a player.
        /// We must say:
        /// - who is the owner (which Player)
        /// - what is the starting state (e.g. IdleState)
        /// </summary>
        public PlayerStateMachine(Player owner, IPlayerState startingState)
        {
            _owner = owner;
            CurrentState = startingState;
            PreviousState = null;

            // When we start, we "enter" the starting state once.
            CurrentState.Enter(_owner);
        }
        #endregion

        #region State Change Logic
        /// <summary>
        /// Change from the current state to a new one.
        /// This method:
        /// 1) Asks the old state to Exit (cleanup).
        /// 2) Remembers it as PreviousState.
        /// 3) Sets the new state as CurrentState.
        /// 4) Asks the new state to Enter (setup).
        /// </summary>
        public void ChangeState(IPlayerState newState)
        {
            if (newState == null)
            {
                // Defensive: we never want to switch to "nothing".
                throw new System.ArgumentNullException(nameof(newState));
            }

            // 1) Let the current state clean up.
            CurrentState?.Exit(_owner);

            // 2) Remember what we were before.
            PreviousState = CurrentState;

            // 3) Switch to the new state.
            CurrentState = newState;

            // 4) Let the new state set itself up.
            CurrentState.Enter(_owner);
        }

        /// <summary>
        /// Optional helper: go back to the previous state.
        /// Might be useful later if we have temporary states.
        /// </summary>
        public void RevertToPreviousState()
        {
            if (PreviousState == null)
            {
                // Nothing to go back to.
                return;
            }

            ChangeState(PreviousState);
        }
        #endregion

        #region Execute / Update
        /// <summary>
        /// Tell the current state to "do its work".
        /// In a real game loop, Player (or GameManager) will call this.
        ///
        /// Example later:
        ///   player.StateMachine.Update();
        ///
        /// Inside, this calls:
        ///   CurrentState.Execute(player);
        /// </summary>
        public void Update()
        {
            // If we somehow have no state, do nothing (should not happen if we set it correctly).
            CurrentState?.Execute(_owner);
        }
        #endregion
    }
    #endregion
}
