using NUnit.Framework;                         // nUnit
using Monopoly.Core.Players;                   // Player
using Monopoly.Core.Players.States;            // IPlayerState, PlayerStateMachine

namespace Monopoly.Tests
{
    /// <summary>
    /// Tests for the PlayerStateMachine "mini brain" that controls a player's states.
    /// </summary>
    public class PlayerStateMachineTests
    {
        #region Fake State (test helper)
        /// <summary>
        /// Small fake state that counts how many times Enter/Execute/Exit are called.
        /// This lets us see if the FSM is calling the right methods at the right time.
        /// </summary>
        private class FakeState : IPlayerState
        {
            public string Name { get; }

            public int EnterCalls { get; private set; }
            public int ExecuteCalls { get; private set; }
            public int ExitCalls { get; private set; }

            public Player? LastEnterPlayer { get; private set; }
            public Player? LastExecutePlayer { get; private set; }
            public Player? LastExitPlayer { get; private set; }

            public FakeState(string name)
            {
                Name = name;
            }

            public void Enter(Player player)
            {
                EnterCalls++;
                LastEnterPlayer = player;
            }

            public void Execute(Player player)
            {
                ExecuteCalls++;
                LastExecutePlayer = player;
            }

            public void Exit(Player player)
            {
                ExitCalls++;
                LastExitPlayer = player;
            }
        }
        #endregion

        #region Tests

        /// <summary>
        /// When we create a PlayerStateMachine:
        /// - it should remember the owner player
        /// - it should set CurrentState to the starting state
        /// - it should call Enter() on that starting state once.
        /// </summary>
        [Test]
        public void Constructor_SetsStartingState_AndCallsEnterOnce()
        {
            var player = new Player("Dog", 1500);
            var startingState = new FakeState("Idle");

            var fsm = new PlayerStateMachine(player, startingState);

            Assert.That(fsm.CurrentState, Is.SameAs(startingState));
            Assert.That(startingState.EnterCalls, Is.EqualTo(1));
            Assert.That(startingState.LastEnterPlayer, Is.SameAs(player));
        }

        /// <summary>
        /// ChangeState should:
        /// - call Exit() on the old state
        /// - call Enter() on the new state
        /// - update PreviousState and CurrentState correctly.
        /// </summary>
        [Test]
        public void ChangeState_UpdatesCurrentAndPrevious_AndCallsExitEnter()
        {
            var player = new Player("Hat", 1500);
            var oldState = new FakeState("Old");
            var newState = new FakeState("New");

            var fsm = new PlayerStateMachine(player, oldState);

            // ACT: switch to the new state
            fsm.ChangeState(newState);

            // ASSERT: previous and current pointers
            Assert.That(fsm.PreviousState, Is.SameAs(oldState));
            Assert.That(fsm.CurrentState, Is.SameAs(newState));

            // ASSERT: Exit() called on old, Enter() on new
            Assert.That(oldState.ExitCalls, Is.EqualTo(1));
            Assert.That(oldState.LastExitPlayer, Is.SameAs(player));

            Assert.That(newState.EnterCalls, Is.EqualTo(1));
            Assert.That(newState.LastEnterPlayer, Is.SameAs(player));
        }

        /// <summary>
        /// Update() should call Execute() on the CURRENT state with the owner player.
        /// </summary>
        [Test]
        public void Update_CallsExecuteOnCurrentStateWithOwner()
        {
            var player = new Player("Car", 1500);
            var state = new FakeState("Moving");

            var fsm = new PlayerStateMachine(player, state);

            // ACT: one update tick
            fsm.Update();

            // ASSERT: Execute() was called once with the right player
            Assert.That(state.ExecuteCalls, Is.EqualTo(1));
            Assert.That(state.LastExecutePlayer, Is.SameAs(player));
        }

        #endregion
    }
}
