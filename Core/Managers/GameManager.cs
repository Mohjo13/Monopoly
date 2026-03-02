using Monopoly.Core.Boards;
using Monopoly.Core.Players;
using Monopoly.Core.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Monopoly.Core.Managers
{

    #region GameManager 
    /// <summary>
    /// CENTRALIZED VERSION 
    /// - Handles turns
    /// - Rolls dice
    /// - Moves players by steps (no square effects, no money/jail)
    /// Skeleton only
    /// </summary>
    public sealed class GameManager
    {
        #region State (fields)
        // _players: holds all players participating in the game, in turn order
        private readonly List<Player> _players = new();

        // _board: the Monopoly board used for wrapping and square access
        private Board _board;

        // _currentPlayerIndex: points to the player whose turn it is
        private int _currentPlayerIndex;

        // _dice: dice provider for movement (RandomDice??)
        private IDice _dice;

        // _isStarted: indicates StartGame() has been called successfully
        private bool _isStarted;
        #endregion

        #region Initialization
        /// <summary>
        /// Injects players, the board, and dice roller.
        /// Sets up the initial turn state.
        /// </summary>
        public void StartGame(List<Player> players, Board board, IDice? dice = null)
        {
            if (players == null || players.Count == 0)//players?
                // safety check: must have at least one player
                throw new ArgumentException("Need at least one player.");

            _players.Clear();
            // clear any old data

            _players.AddRange(players);
            //add new players

            _board = board ?? throw new ArgumentNullException(nameof(board));
            //assign and get the board
            _dice = dice ?? new RandomDice();
            //assign and get the dice

        
            _currentPlayerIndex = 0;
            // first player starts
            _isStarted = true;
            //game is offically started here
        }
        #endregion

        #region Centralized (movement-only)
        /// <summary>
        /// Runs exactly one turn:
        /// 1) get current player
        /// 2) roll dice for steps
        /// 3) move the player by steps
        /// 4) advance to next player
        /// </summary>

        // -------------------------------------------------------------
        // MAIN TURN LOGIC (CENTRALIZED)
        // -------------------------------------------------------------
        public void TakeTurn()//main turn logic
        {
            EnsureStarted();        //set to true              // guard: make sure game is running
            Player player = GetCurrentPlayer();   // find whose turn it is

            int steps = _dice.Roll();             // roll dice 
            MovePlayer(player, steps);            // move player on the board

            AdvanceTurn();                        // switch to next player
        }

 
        public void MovePlayer(Player player, int steps)//moves the player by number of rolled steps
        {
            EnsureStarted();                      // guard check
            player.MoveBy(steps, _board);         // update player position
        }

        public void AdvanceTurn()// Advances turn order (wraps back to 0 after the last player).

        {
            EnsureStarted();//turn started
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;//next player and check the player count
        }
        #endregion

        #region Helpers

        public Player GetCurrentPlayer()//Returns whichever player’s turn it currently is.

        {
            EnsureStarted();//check
            return _players[_currentPlayerIndex];
        }

        private void EnsureStarted()//Checks that StartGame() has been called before allowing play.

        {
            if (!_isStarted)
                throw new InvalidOperationException("Game not started. Call StartGame() first.");
        }

        #endregion


        #region Distributed behaviour

        // -------------------------------------------------------------
        // DISTRIBUTED VERSION (optional helper)
        // -------------------------------------------------------------
        /// <summary>
        /// Uses the distributed flow: Player moves itself and triggers square effects.
        /// </summary>
        public void TakeTurnDistributed()//turn logic in distributed fashion
        {
            EnsureStarted();//guard check still intact
            Player player = GetCurrentPlayer();//whos turn it is
            int steps = _dice.Roll();//roll dice?
            player.MoveByDistributed(steps, _board); // triggers OnPass / OnLand internally by the player
            AdvanceTurn();
        }

        /// <summary>
        /// Starts the game for distributed flow as well (same params).
        /// </summary>
        public void StartGameDistributed(List<Player> players, Board board, IDice? dice = null)
        {
            StartGame(players, board, dice); // same initialization
        }

        #endregion
    }
    #endregion
}
