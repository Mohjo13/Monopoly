using System;
using System.Collections.Generic;

// Monopoly namespaces
using Monopoly.Core.Boards;
using Monopoly.Core.Players;
using Monopoly.Core.Players.States;
using Monopoly.Core.Commons;

namespace Monopoly.ConsoleTest
{
    #region Program Entry
    /// <summary>
    /// Simple console test harness for the Player FSM.
    /// This is ONLY for testing the state chain:
    /// StartTurn -> RollDice -> Moving -> EndTurn -> Idle
    ///
    /// It will:
    /// - Create a tiny board
    /// - Create two players (TopHat & Dog)
    /// - Give them each a state machine
    /// - Loop turns and print what happens
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1) Build a tiny test board
            Board board = CreateTestBoard();

            // 2) Create a dice roller (2d6)
            IDice dice = new RandomDice();

            // 3) Create two players
            Player topHat = new Player("TopHat", startingMoney: 200);
            Player dog = new Player("Dog", startingMoney: 200);

            // All players start at GO (index 0) because Player constructor sets Position = 0.

            // 4) Give each player a state machine starting in IdleState
            topHat.InitializeStateMachine(new IdleState());
            dog.InitializeStateMachine(new IdleState());

            // Put players into a list in turn order
            List<Player> players = new List<Player> { topHat, dog };

            int currentPlayerIndex = 0;   // who starts (TopHat)
            int maxTurns = 6;            // how many turns to simulate in total (for testing)

            Console.WriteLine("=== Monopoly FSM Test: StartTurn -> RollDice -> Moving -> EndTurn ===");
            Console.WriteLine();

            // 5) Main turn loop
            for (int turn = 1; turn <= maxTurns; turn++)
            {
                Player currentPlayer = players[currentPlayerIndex];

                Console.WriteLine();
                Console.WriteLine($"=== TURN {turn}: {currentPlayer.Name}'s turn ===");
                Console.WriteLine($"Current position: {currentPlayer.Position}, Money: {currentPlayer.Money}");

                // Make sure the player starts the turn from Idle.
                // (Normally they should already be Idle after EndTurnState.)
                if (currentPlayer.StateMachine == null)
                {
                    // Defensive check: should not happen if we called InitializeStateMachine.
                    Console.WriteLine("ERROR: State machine not initialized for player.");
                    return;
                }

                if (!(currentPlayer.StateMachine.CurrentState is IdleState))
                {
                    Console.WriteLine("WARNING: Player did not start turn in IdleState. Forcing Idle.");
                    currentPlayer.ChangeState(new IdleState());
                }

                // 1) Begin the turn by switching to StartTurnState.
                //    We pass the Board and Dice so the states can use them.
                currentPlayer.ChangeState(new StartTurnState(board, dice));

                // 2) Let the FSM run until it returns to IdleState again.
                //    This will trigger:
                //    StartTurn -> RollDice -> Moving -> EndTurn -> Idle
                while (!(currentPlayer.StateMachine.CurrentState is IdleState))
                {
                    currentPlayer.UpdateState();  // This calls Execute() on the current state.
                }

                Console.WriteLine($"End of {currentPlayer.Name}'s turn.");
                Console.WriteLine($"New position: {currentPlayer.Position}, Money: {currentPlayer.Money}");
                Console.WriteLine("-----------------------------------------------");

                // 3) Move to the next player in the list (wrap around).
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;

                // 4) Pause between turns so you can read the console
                Console.WriteLine("Press any key for next turn...");
                Console.ReadKey(true);
            }

            Console.WriteLine();
            Console.WriteLine("Test finished. Press any key to exit.");
            Console.ReadKey(true);
        }

        #region Board Setup
        /// <summary>
        /// Creates a tiny test board for our FSM test.
        /// We only really need the Board's WrapIndex and a few squares.
        /// The square types are not yet used by the states in Option B,
        /// but it's nice to have something that looks like a real board.
        /// </summary>
        private static Board CreateTestBoard()
        {
            var squares = new List<Square>
            {
                // Index 0: GO
                new GoSquare(index: 0, salary: 0),  // salary not used in this simple test

                // Index 1: Free Parking (does nothing for now)
                new FreeParkingSquare(index: 1),

                // Index 2: Free Parking again (just a placeholder)
                new FreeParkingSquare(index: 2),

                // Index 3: Free Parking again (placeholder)
                new FreeParkingSquare(index: 3)
            };

            return new Board(squares);
        }
        #endregion
    }
    #endregion
}
