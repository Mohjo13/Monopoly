using System;
using System.Collections.Generic;
using NUnit.Framework;
using Monopoly.Core.Managers;
using Monopoly.Core.Players;
using Monopoly.Core.Boards;
using Monopoly.Core.Commons;

namespace Monopoly.Tests.Managers//WILL FAIL
{
    #region GameManagerTests
    /// <summary>
    /// Specification-style tests for GameManager.
    /// These will fail until GameManager is properly implemented.
    /// </summary>
    [TestFixture]
    public class GameManagerTests
    {
        #region Helper Types
        private class TestSquare : Square
        {
            public TestSquare(int index, string name, SquareType type)
                : base(index, name, type)
            {
            }
        }

        private Board CreateSimpleBoard(int count = 10)
        {
            var squares = new List<Square>();
            for (int i = 0; i < count; i++)
            {
                squares.Add(new TestSquare(i, $"S{i}", SquareType.Property));
            }
            return new Board(squares);
        }

        private List<Player> CreatePlayers(int count = 2)
        {
            var list = new List<Player>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new Player($"P{i}", 1500));
            }
            return list;
        }
        #endregion

        #region Tests
        /// <summary>
        /// We EXPECT StartGame to guard against null/empty player lists.
        /// This is not implemented yet in GameManager, so this test will FAIL
        /// until you add the guard.
        /// </summary>
        [Test]
        public void StartGame_NullPlayers_ThrowsArgumentException()
        {
            var gm = new GameManager();
            var board = CreateSimpleBoard();

            Assert.Throws<ArgumentException>(
                () => gm.StartGame(null, board)
            );
        }

        /// <summary>
        /// We EXPECT StartGame to at least accept valid players and board
        /// without throwing. Once GetCurrentPlayer() is implemented,
        /// we can also assert that it returns the first player.
        /// </summary>
        [Test]
        public void StartGame_ValidPlayersAndBoard_DoesNotThrow()
        {
            var gm = new GameManager();
            var players = CreatePlayers();
            var board = CreateSimpleBoard();

            Assert.DoesNotThrow(() => gm.StartGame(players, board));
        }

        /// <summary>
        /// Intended test for turn rotation:
        /// After calling AdvanceTurn once, current player should change
        /// from index 0 to index 1.
        /// This will fail until both StartGame and AdvanceTurn and
        /// GetCurrentPlayer are properly implemented.
        /// </summary>
        [Test]
        public void AdvanceTurn_MovesToNextPlayer()
        {
            var gm = new GameManager();
            var players = CreatePlayers();
            var board = CreateSimpleBoard();

            gm.StartGame(players, board);

            var first = gm.GetCurrentPlayer();

            gm.AdvanceTurn();

            var second = gm.GetCurrentPlayer();

            Assert.That(second, Is.Not.SameAs(first));
            Assert.That(second.Name, Is.EqualTo("P1"));
        }
        #endregion
    }
    #endregion
}
