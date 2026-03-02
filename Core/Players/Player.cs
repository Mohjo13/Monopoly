using System;                            // for exceptions like ArgumentNullException
using System.Collections.Generic;
using Monopoly.Core.Boards;
using Monopoly.Core.Players.States;      // NEW: so Player can see the FSM + states



namespace Monopoly.Core.Players
{
    #region Player
    /// <summary>
    /// Player and their properties: owns money, moves on board, can own properties(reaestate).
    /// Connections:
    /// - Owns PropertySquare.
    /// - Position attached to Board square index (board inherits from sqaures).
    /// - Interacts with money flow (Bank,) and rules (rent, jail).
    /// next turn player???
    /// </summary>
    public class Player
        //the actual player identity and all of its properites and functions, nouns and verbs
    {
        #region Identity & State
        public string Name { get; } //name has no function other than reading
        public int Money { get; private set; }//money can be changed and needs to be set from other functinas
        public int Position { get; set; }//positions changes sbased on other functions like dice(dice, future)
        public bool InJail { get; set; }//Jail status 
        public int JailTurns { get; set; }//turns left in jail
        public int GetOutOfJailCards { get; set; }//get out of jail free card in deck
        public int LastRoll { get; private set; } // remembers the last dice roll for this player
        public bool IsBankrupt { get; private set; } // true when player can no longer pay

        /// <summary>All owned buildable properties (no railroads/utilities here by design choice).</summary>
        public List<PropertySquare> OwnedProperties { get; } = new();//all owned buildable property, excluding utility 
        #endregion

        #region Constructor
        //constructor for player
        public Player(string name, int startingMoney)//name of player like tophat or dog, amount of starting money
        {
            Name = name;
            Money = startingMoney;
            Position = 0;
        }
        #endregion

        #region Movement
        /// <summary>
        /// Player movement seportated by a centralized GameManager and a distributed responisbilites structure
        /// </summary>
 

        // -------------------------------------------------------------
        // DISTRIBUTED MOVEMENT  (used when Player handles its own behaviour)
        // -------------------------------------------------------------
        /// <summary>
        /// Moves the player by steps and triggers OnPass/OnLand on squares.
        /// The player itself handles calling these effects on sqaures, no GameManager needed.
        /// </summary>
        public void MoveByDistributed(int steps, Board board)//steps from dice and squares from board
        {
            if (steps == 0) return;
            // If the player isn’t moving, there’s nothing to do.

            int start = Position;
            // Save where we started.

            // ------------------------------
            // Forward movement (+)
            // ------------------------------
            if (steps > 0)
            {
                for (int i = 1; i <= steps; i++)
                // Loop through every square we pass or land on.

                {
                    int idx = board.WrapIndex(start + i);
                    // Calculate the index of the square we’re stepping onto.


                    if (i < steps)
                    {
                        board.GetSquare(idx).OnPass(this, board);
                        // If this isn’t the last step, we’re passing this square.
                        //keep going.
                    }
                    else
                    {
                        // This is the final square we land on.
                        Position = idx;  // update position
                        board.GetSquare(Position).OnLand(this, board);
                        //calls square effects in distributed fashion
                    }
                }
            }
            // ------------------------------
            // Backward movement (-)
            // ------------------------------
            else
            {

                for (int i = -1; i >= steps; i--)
                // Same idea, but we move backwards (−steps)
                {
                    int idx = board.WrapIndex(start + i);

                    if (i > steps)
                    {

                        board.GetSquare(idx).OnPass(this, board);
                        // Passing over squares while moving backward.
                    }
                    else
                    {
                        // Final landing square.
                        Position = idx;
                        board.GetSquare(Position).OnLand(this, board);
                        //same here, calls effects 
                    }
                }
            }
        }


        // -------------------------------------------------------------
        // CENTRALIZED MOVEMENT  (used by GameManager to move players)
        // -------------------------------------------------------------
        /// <summary>
        /// Moves the player by a number of steps.
        /// This is used when the GameManager is controlling everything (centralized).
        /// /// This method ONLY changes the index.
        /// It does NOT call OnPass / OnLand or handle money, jail, etc.
        /// </summary>
        public void MoveBy(int steps, Board board)
        {
            // guard 1: board must exist, otherwise we can't wrap indices
            if (board == null)
                throw new ArgumentNullException(nameof(board));

            // guard 2: if steps is 0, nothing changes, so we can early-exit
            if (steps == 0)
                return;

            // rawIndex = where we would end up without wrapping
            int rawIndex = Position + steps;

            // ask the board to wrap that index into a valid range (0..Squares.Count-1)
            int wrappedIndex = board.WrapIndex(rawIndex);

            // finally, store the new position
            Position = wrappedIndex;
        }
        #endregion

        #region Dice & Roll Memory
        /// <summary>
        /// Store the last dice roll result for this player.
        /// Used by RollDiceState and MovingState.
        /// </summary>
        public void SetLastRoll(int roll)
        {
            LastRoll = roll;
        }
        #endregion

        #region Money 
        /// <summary>
        /// Increase the player's money by a positive amount.
        /// </summary>
        public void Credit(int amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Credit amount must be non-negative.");
            Money += amount;
        }

        /// <summary>
        /// Try to subtract money from the player.
        /// Returns true if the player had enough, false otherwise.
        /// Does NOT mark bankrupt; that is decided by calling code.
        /// </summary>
        public bool TryDebit(int amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Debit amount must be non-negative.");

            if (Money >= amount)
            {
                Money -= amount;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Mark this player as bankrupt (cannot continue playing).
        /// </summary>
        public void MarkBankrupt()
        {
            IsBankrupt = true;
        }
        #endregion////////

        #region FSM (State Machine)
        // This region connects the Player to its "mini brain" (PlayerStateMachine).
        // The FSM decides WHICH state the player is in (Idle, StartTurn, Moving, etc.)
        // and lets the current state run its logic.


        private PlayerStateMachine? _stateMachine;
        /// <summary>
        /// The state machine that controls this player's behaviour.
        /// Can be null until we initialize it.
        /// </summary>


        public PlayerStateMachine? StateMachine => _stateMachine;
        /// <summary>
        /// Read-only access to the state machine from the outside.
        /// Example: GameManager or tests can inspect which state the player is in.
        /// </summary>


        public void InitializeStateMachine(IPlayerState startingState)//initaliazation with state machine
        {
            // Guard: we don't want to start with "no state".
            if (startingState == null)
                throw new ArgumentNullException(nameof(startingState));

            // Create a new FSM that controls THIS player.
            _stateMachine = new PlayerStateMachine(this, startingState);
        }
        /// <summary>
        /// Call this ONCE when setting up the player (for human or AI).
        /// This gives the player a state machine and a starting state,
        /// e.g. new IdleState() or new StartTurnState().
        /// </summary>

        public void UpdateState()
        {
            // If the FSM hasn't been set up yet, do nothing.
            // (In a real game we should always call InitializeStateMachine first.)
            _stateMachine?.Update();
        }

        /// <summary>
        /// Called by GameManager / turn system when we want this player
        /// to let its current state "do its thing".
        /// Internally this just calls CurrentState.Execute(player).
        /// </summary>


        public void ChangeState(IPlayerState newState)
        {
            if (_stateMachine == null)
                throw new InvalidOperationException("State machine not initialized for this player.");//error check

            _stateMachine.ChangeState(newState);//flag for new state 
        }
        /// <summary>
        /// Helper so states (or GameManager) can change the current state
        /// WITHOUT needing to know how the FSM works inside.
        /// This keeps coupling low: they just say "player.ChangeState(new SomeState())".
        /// </summary>
        #endregion//

    }
    #endregion
}
